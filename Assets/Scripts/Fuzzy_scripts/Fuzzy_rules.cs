using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy_rules : MonoBehaviour
{
    public Fuzzy_logic l;
    public GameObject Gen_alg;
    public string Fuzzy_Number;
    public float start_time;
    private bool stop = false;
    private int cycle = 0;

    private void Start()
    {
        start_time = Time.time;
    }
    private void FixedUpdate()
    {
        if (!stop) Rules();
        else
        {
            if (cycle<200)
            {
                if ((l.Speed != "ярно") & (l.Degree != "пнбмн")) Gen_alg.GetComponent<Gen_algoritm>().robot_destroy(transform.name);
                cycle++;
                l.Speed = "ярно";
                l.Degree = "пнбмн";
                //Debug.Log(cycle);
            }
            else
            {
                if (cycle == 200)
                {
                    Gen_alg.GetComponent<Gen_algoritm>().target_accept(Time.time - start_time, transform.name);
                    cycle++;
                }
                
            }

        }
    }

    private void Rules()
    {
        Fuzzy_Number = "";
        //нрбнпнрш нр ярем
        if (l.Distance_45_left == "акхгйн") { l.Speed = "ледкеммн"; l.Degree = "яхкэмн опюбее"; Fuzzy_Number = "онбнпнр 45 опюбн"; }
        if (l.Distance_left == "акхгйн") { l.Speed = "ярно"; l.Degree = "опюбее"; Fuzzy_Number = "онбнпнр ОН АНЙС ОПЮБН"; }
        if (l.Distance_45_right == "акхгйн") { l.Speed = "ярно"; l.Degree = "яхкэмн кебее"; Fuzzy_Number = "онбнпнр 45 кебн"; }
        if (l.Distance_right == "акхгйн") { l.Speed = "ледкеммн"; l.Degree = "кебее"; Fuzzy_Number = "онбнпнр ОН АНЙС кебн"; }


        //дбхцюрэяъ боепед
        if ((l.Distance_front == "дюкейн") && (l.Distance_45_right != "акхгйн") && (l.Distance_45_left != "акхгйн")) { l.Speed = "ашярпн"; }
        if ((l.Distance_front != "дюкейн")) { l.Speed = "япедме"; }
        if ((l.Distance_front == "рекн")) { l.Speed = "ярно"; }

        //онбнпнрш мю рнвйс
        if (((l.To_target == "кебее") || (l.To_target == "яхкэмн кебее")) && (l.Distance_front != "акхгйн") && (l.Distance_front != "рекн") && (l.Distance_45_left != "акхгйн")) { l.Degree = "кебее"; l.Speed = "япедме"; Fuzzy_Number = "онбнпнр мю рнвйс кебн"; }
        if (((l.To_target == "опюбее") || (l.To_target == "яхкэмн опюбее")) && (l.Distance_front != "акхгйн") && (l.Distance_front != "рекн") && (l.Distance_45_right != "акхгйн")) { l.Degree = "опюбее"; l.Speed = "япедме"; Fuzzy_Number = "онбнпнр мю рнвйс опюбн"; }
        if (l.To_target == "пнбмн") { l.Degree = "пнбмн"; l.Speed = "япедме"; Fuzzy_Number = "еякх пнбмн, гмювхр пнбмнБ"; }

        //нрбнпнрш
        if ((l.Distance_front == "акхгйн"))
        {
            if ((l.Distance_left != "акхгйн") && (l.Distance_left != "рекн")) { l.Speed = "ярно"; l.Degree = "яхкэмн кебее"; Fuzzy_Number = "нрбнпнр кебн"; }
            if ((l.Distance_right != "акхгйн") && (l.Distance_right != "рекн")) { l.Speed = "ледкеммн"; l.Degree = "опюбее"; Fuzzy_Number = "нрбнпнр опюбн"; }

        }

        //дбхфемхе он яремйе аег йнкеаюмхи
        if ((l.Distance_right == "акхгйн") && (l.Distance_45_right == "акхгйн"))
        {
            Fuzzy_Number = "дбхфемхе он яреме опюбн";
            l.Speed = "ледкеммн";
            if ((l.Distance_45_right == "рекн") || (l.Distance_front == "акхгйн")) { l.Degree = "яхкэмн кебее"; l.Speed = "япедме"; Fuzzy_Number = "дбхфемхе он яреме опюбн ХЯЙКЧВЕМХЕ"; }
        }
        if ((l.Distance_left == "акхгйн") && (l.Distance_45_left == "акхгйн"))
        {
            Fuzzy_Number = "дбхфемхе он яреме кебн";
            l.Speed = "ледкеммн";
            if ((l.Distance_45_left == "рекн") || (l.Distance_front == "акхгйн")) { l.Degree = "яхкэмн опюбее"; l.Speed = "япедме"; Fuzzy_Number = "дбхфемхе он яреме кебн ХЯЙКЧВЕМХЕ"; }

        }



        //днбндйю дн жекх
        if ((l.Dist_to_target == "акхгйн") || (l.Dist_to_target == "япедме") && (l.Distance_front != "рекн") && (l.Distance_front != "акхгйн"))
        {
            if (((l.To_target == "кебее") || (l.To_target == "яхкэмн кебее")) && (l.Distance_front != "рекн")) { l.Degree = "кебее"; l.Speed = "ледкеммн"; }
            if (((l.To_target == "опюбее") || (l.To_target == "яхкэмн опюбее")) && (l.Distance_front != "рекн")) { l.Degree = "опюбее"; l.Speed = "ледкеммн"; }
        }
        //еЯКХ БНЬКХ РЕКНЛ
        if ((l.Distance_right == "рекн") || (l.Distance_45_right == "рекн")) { l.Degree = "кебее"; l.Speed = "ледкеммн"; }
        if ((l.Distance_left == "рекн") || (l.Distance_45_left == "рекн")) { l.Degree = "опюбее"; l.Speed = "ледкеммн"; }

        if (l.Dist_to_target == "бокнрмсч") { 
            l.Speed = "ярно";
            l.Degree = "пнбмн"; 
            Fuzzy_Number = "онкмюъ нярюмнбйю"; 
            stop = true; 
        }
    }
    
}
