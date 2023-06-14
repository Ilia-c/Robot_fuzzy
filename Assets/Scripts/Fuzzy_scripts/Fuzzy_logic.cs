using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Fuzzy_logic : MonoBehaviour
{
    public Transform target;
    public Move Move;
    public Lidar Lidar;
    public Transform pos;

    // Ôóíêöèÿ ïðèíàäëåæíîñòè

    public float[,] Speed_func =            {{-0.2f,-0.1f,0.1f,0.2f},
                                             {-14.44394f,-4.314289f,8.928164f,-0.7303023f},
                                             {11.24257f,-3.507036f,35.57816f,17.58041f},
                                             {9.750206f,0.5990793f,8.342168f,15.94938f}};

    public float[,] Dist_func =             {{-17.84678f,-2.138612f,-1.391722f,-17.03558f},
                                             {3.517407f,10.85688f,-5.407549f,26.49566f},
                                             {-14.95609f,-17.48044f,31.16756f,41.73447f},
                                             {26.34652f,5.05504f,42.60051f,184.1234f}};

    public float[,] Dist_to_target_func =   {{0f,1.5f,2f,3f},
                                             {6.592092f,24.0687f,10.58374f,-0.1839929f},
                                             {13.72513f,11.09101f,20.57109f,22.59874f},
                                             {17.16537f,26.1494f,33.60575f,103.1076f}};

    public float[,] Deg_to_target_func =    {{-27.72349f,-22.6893f,26.38056f,27.69765f},
                                             {-111.1983f,-76.7466f,-33.32406f,-5.871965f},
                                             {-200.8891f,-178.2405f,-118.5628f,-122.1865f},
                                             {11.95771f,17.68257f,84.23481f,75.96723f},
                                             {67.29242f,107.6745f,163.7895f,175.5111f}};

    public float[,] Degree_func =           {{0f,0f,0f,0f},
                                             {13.91055f,1.065669f,-18.85572f,5.362016f},
                                             {-0.2122982f,7.954979f,-15.59394f,-3.057594f},
                                             {18.38077f,20.58299f,13.07697f,-14.60007f},
                                             {12.16678f,-28.80918f,-4.448131f,-8.058632f}};



    // Ïåðåìåííûå
    private static string Speed_stop_name = "ÑÒÎÏ";
    private static string Speed_low_name = "ÌÅÄËÅÍÍÎ";
    private static string Speed_medium_name = "ÑÐÅÄÍÅ";
    private static string Speed_fast_name = "ÁÛÑÒÐÎ";

    private static string Distance_low_zero = "ÒÅËÎ";
    private static string Distance_low_name = "ÁËÈÇÊÎ";
    private static string Distance_med_name = "ÑÐÅÄÍÅ";
    private static string Distance_high_name = "ÄÀËÅÊÎ";

    private static string To_target_norm = "ÐÎÂÍÎ";
    private static string To_target_left = "ËÅÂÅÅ";
    private static string To_target_more_left = "ÑÈËÜÍÎ ËÅÂÅÅ";
    private static string To_target_right = "ÏÐÀÂÅÅ";
    private static string To_target_more_right = "ÑÈËÜÍÎ ÏÐÀÂÅÅ";

    private static string Distance_to_target_stop_name = "ÂÏËÎÒÍÓÞ";
    private static string Distance_to_target_low_name = "ÁËÈÇÊÎ";
    private static string Distance_to_target_med_name = "ÑÐÅÄÍÅ";
    private static string Distance_to_target_high_name = "ÄÀËÅÊÎ";
    // Òåêóùåå ñòîñòÿíèå

    // Óïðàâëåíèå
    public string Speed = "ÑÒÎÏ";
    public string Degree = "ÐÎÂÍÎ"; // Çàäàííûé óãîë (óïðàâëåíèå)


    // Ïîëó÷åííûå äàííûå
    public string Distance_45_left = "ÁËÈÇÊÎ";
    public string Distance_45_right = "ÁËÈÇÊÎ";

    public string Distance_left = "ÁËÈÇÊÎ";
    public string Distance_right = "ÁËÈÇÊÎ";
    public string Distance_front = "ÁËÈÇÊÎ";
    public string To_target = "ÐÎÂÍÎ";
    public string Dist_to_target = "ÄÀËÅÊÎ";

    private void FixedUpdate()
    {
        Update_Move();
        Update_param();
        
    }

    private void Update_Move()
    {
        
        if (Speed == "ÑÒÎÏ") Move.speed = (Speed_func[0, 1]+ Speed_func[0, 2])/2;
        if (Speed == "ÌÅÄËÅÍÍÎ") Move.speed = (Speed_func[1, 1] + Speed_func[1, 2]) / 2;
        if (Speed == "ÑÐÅÄÍÅ") Move.speed = (Speed_func[2, 1] + Speed_func[2, 2]) / 2;
        if (Speed == "ÁÛÑÒÐÎ") Move.speed = (Speed_func[3, 1] + Speed_func[3, 2]) / 2;
        

        if (Degree == "ÐÎÂÍÎ") Move.degree = (Degree_func[0, 1] + Degree_func[0, 2]) / 2;
        if (Degree == "ËÅÂÅÅ") Move.degree = (Degree_func[1, 1] + Degree_func[1, 2]) / 2;
        if (Degree == "ÑÈËÜÍÎ ËÅÂÅÅ") Move.degree = (Degree_func[2, 1] + Degree_func[2, 2]) / 2;
        if (Degree == "ÏÐÀÂÅÅ") Move.degree = (Degree_func[3, 1] + Degree_func[3, 2]) / 2;
        if (Degree == "ÑÈËÜÍÎ ÏÐÀÂÅÅ") Move.degree = (Degree_func[3, 1] + Degree_func[3, 2]) / 2;

    }

    public float Rules_y(float[] x_m, float x)
    {
        float y = 0;
        if ((x >= x_m[0]) && (x <= x_m[1])) y = (x - x_m[0]) / (x_m[1] - x_m[0]);
        if ((x > x_m[1]) & (x <= x_m[2])) y = 1;
        if ((x > x_m[2]) & (x <= x_m[3])) y = (x_m[3] - x) / (x_m[3] - x_m[2]);
        return y;
    }

    public void Update_param()
    {
        // Ðàññ÷åò ïðèíàäëåæíîñòè ñêîðîñòè äëÿ îïðåäåëåíèÿ ïåðåìåííîé
        float y_speed_stop = Rules_y(Get_rows(Speed_func, 0), Move.speed);
        float y_speed_low = Rules_y(Get_rows(Speed_func, 1), Move.speed);
        float y_speed_medium = Rules_y(Get_rows(Speed_func, 2), Move.speed);
        float y_speed_fast = Rules_y(Get_rows(Speed_func, 3), Move.speed);

        float[] mass_speed = { y_speed_stop, y_speed_low, y_speed_medium, y_speed_fast };
        Array.Sort(mass_speed);

        if (y_speed_low == mass_speed[3]) Speed = Speed_low_name;
        if (y_speed_medium == mass_speed[3]) Speed = Speed_medium_name;
        if (y_speed_fast == mass_speed[3]) Speed = Speed_fast_name;
        if (y_speed_stop == mass_speed[3]) Speed = Speed_stop_name;

        // Ðàññ÷åò ðàñòîÿíèÿ äëÿ îïðåäåëåíèÿ ïåðåìåííîé
        // Ñëåâà
        float y_dist_zero = Rules_y(Get_rows(Dist_func, 0), Lidar.left_dist);
        float y_dist_low = Rules_y(Get_rows(Dist_func, 1), Lidar.left_dist);
        float y_dist_med = Rules_y(Get_rows(Dist_func, 2), Lidar.left_dist);
        float y_dist_hig = Rules_y(Get_rows(Dist_func, 3), Lidar.left_dist);
        float[] mass_dist_l = { y_dist_low, y_dist_med, y_dist_hig, y_dist_zero };
        Array.Sort(mass_dist_l);
        if (y_dist_low == mass_dist_l[3]) Distance_left = Distance_low_name;
        if (y_dist_med == mass_dist_l[3]) Distance_left = Distance_med_name;
        if (y_dist_hig == mass_dist_l[3]) Distance_left = Distance_high_name;
        if (y_dist_zero == mass_dist_l[3]) Distance_left = Distance_low_zero;

        // Ñïðàâà
        y_dist_zero = Rules_y(Get_rows(Dist_func, 0), Lidar.left_dist);
        y_dist_low = Rules_y(Get_rows(Dist_func, 1), Lidar.right_dist);
        y_dist_med = Rules_y(Get_rows(Dist_func, 2), Lidar.right_dist);
        y_dist_hig = Rules_y(Get_rows(Dist_func, 3), Lidar.right_dist);
        float[] mass_dist_r = { y_dist_low, y_dist_med, y_dist_hig, y_dist_zero };
        Array.Sort(mass_dist_r);
        if (y_dist_low == mass_dist_r[3]) Distance_right = Distance_low_name;
        if (y_dist_med == mass_dist_r[3]) Distance_right = Distance_med_name;
        if (y_dist_hig == mass_dist_r[3]) Distance_right = Distance_high_name;
        if (y_dist_zero == mass_dist_r[3]) Distance_right = Distance_low_zero;

        //////////////////////////////////////////////
        // Ñïðàâà 45
        y_dist_zero = Rules_y(Get_rows(Dist_func, 0), Lidar.right_45_dist);
        y_dist_low = Rules_y(Get_rows(Dist_func, 1), Lidar.right_45_dist);
        y_dist_med = Rules_y(Get_rows(Dist_func, 2), Lidar.right_45_dist);
        y_dist_hig = Rules_y(Get_rows(Dist_func, 3), Lidar.right_45_dist);
        float[] mass_dist_r_45 = { y_dist_low, y_dist_med, y_dist_hig, y_dist_zero };
        Array.Sort(mass_dist_r_45);
        if (y_dist_low == mass_dist_r_45[3]) Distance_45_right = Distance_low_name;
        if (y_dist_med == mass_dist_r_45[3]) Distance_45_right = Distance_med_name;
        if (y_dist_hig == mass_dist_r_45[3]) Distance_45_right = Distance_high_name;
        if (y_dist_zero == mass_dist_r_45[3]) Distance_45_right = Distance_low_zero;

        //////////////////////////////////////////////
        // Ñëåâà 45
        y_dist_zero = Rules_y(Get_rows(Dist_func, 0), Lidar.left_45_dist);
        y_dist_low = Rules_y(Get_rows(Dist_func, 1), Lidar.left_45_dist);
        y_dist_med = Rules_y(Get_rows(Dist_func, 2), Lidar.left_45_dist);
        y_dist_hig = Rules_y(Get_rows(Dist_func, 3), Lidar.left_45_dist);
        float[] mass_dist_l_45 = { y_dist_low, y_dist_med, y_dist_hig, y_dist_zero };
        Array.Sort(mass_dist_l_45);
        if (y_dist_low == mass_dist_l_45[3]) Distance_45_left = Distance_low_name;
        if (y_dist_med == mass_dist_l_45[3]) Distance_45_left = Distance_med_name;
        if (y_dist_hig == mass_dist_l_45[3]) Distance_45_left = Distance_high_name;
        if(y_dist_zero == mass_dist_l_45[3]) Distance_45_left = Distance_low_zero;

        // Ïðÿìî
        y_dist_zero = Rules_y(Get_rows(Dist_func, 0), Lidar.forward_dist);
        y_dist_low = Rules_y(Get_rows(Dist_func, 1), Lidar.forward_dist);
        y_dist_med = Rules_y(Get_rows(Dist_func, 2), Lidar.forward_dist);
        y_dist_hig = Rules_y(Get_rows(Dist_func, 3), Lidar.forward_dist);
        float[] mass_dist_f = { y_dist_low, y_dist_med, y_dist_hig, y_dist_zero };
        Array.Sort(mass_dist_f);
        if (y_dist_low == mass_dist_f[3]) Distance_front = Distance_low_name;
        if (y_dist_med == mass_dist_f[3]) Distance_front = Distance_med_name;
        if (y_dist_hig == mass_dist_f[3]) Distance_front = Distance_high_name;
        if (y_dist_zero == mass_dist_f[3]) Distance_front = Distance_low_zero;

        // Ðàññòîÿíèå äî öåëè
        float _dist_to_traget = Vector3.Distance(target.position, Lidar.transform.position);
        float y_dist_s = Rules_y(Get_rows(Dist_to_target_func, 0), _dist_to_traget);
        y_dist_low = Rules_y(Get_rows(Dist_to_target_func, 1), _dist_to_traget);
        y_dist_med = Rules_y(Get_rows(Dist_to_target_func, 2), _dist_to_traget);
        y_dist_hig = Rules_y(Get_rows(Dist_to_target_func, 3), _dist_to_traget);


        float[] mass_dist_to_target = { y_dist_s, y_dist_low, y_dist_med, y_dist_hig };
        Array.Sort(mass_dist_to_target);
        if (y_dist_s == mass_dist_to_target[3]) Dist_to_target = Distance_to_target_stop_name;
        if (y_dist_low == mass_dist_to_target[3]) Dist_to_target = Distance_to_target_low_name;
        if (y_dist_med == mass_dist_to_target[3]) Dist_to_target = Distance_to_target_med_name;
        if (y_dist_hig == mass_dist_to_target[3]) Dist_to_target = Distance_to_target_high_name;


        float angle = (Vector3.SignedAngle(target.transform.position- pos.transform.position, pos.transform.forward, Vector3.up)+90)*-1;
        if (angle < -180)
        {
            angle += 360;
        }
        //UnityEngine.Debug.Log(angle);



        float y_angle_norm = Rules_y(Get_rows(Deg_to_target_func, 0), angle);
        float y_angle_left = Rules_y(Get_rows(Deg_to_target_func, 1), angle);
        float y_angle_m_left = Rules_y(Get_rows(Deg_to_target_func, 2), angle);
        float y_angle_right = Rules_y(Get_rows(Deg_to_target_func, 3), angle);
        float y_angle_m_right = Rules_y(Get_rows(Deg_to_target_func, 4), angle);

        float[] mass_angle = { y_angle_norm, y_angle_left, y_angle_m_left, y_angle_right, y_angle_m_right };
        Array.Sort(mass_angle);

        if (y_angle_norm == mass_angle[4]) To_target = To_target_norm;
        if (y_angle_left == mass_angle[4]) To_target = To_target_left;
        if (y_angle_m_left == mass_angle[4]) To_target = To_target_more_left;
        if (y_angle_right == mass_angle[4]) To_target = To_target_right;
        if (y_angle_m_right == mass_angle[4]) To_target = To_target_more_right;


    }

    public float[] Get_rows(float[,] mass, int Row)
    {
        float[] result = new float[mass.GetLength(1)];
        for (int i = 0; i < mass.GetLength(1); i++) 
        {
            result[i] = mass[Row, i]; 
        }
        return result;
    }



    public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        dirA = dirA - Vector3.Project(dirA, axis);
        dirB = dirB - Vector3.Project(dirB, axis);
        float angle = Vector3.Angle(dirA, dirB);
        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
    }


}
