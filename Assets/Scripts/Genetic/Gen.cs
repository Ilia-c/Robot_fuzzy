using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gen : MonoBehaviour
{
    public Fuzzy_logic logic;
    // Описывает гены конкретного робота

    public List<float> Gen_vect = new List<float>(); // Все гены

    private void Start()
    {
        read_genetic();
    }


    public void gen_accept(float[,] speed,float[,] Dist,float[,] Dist_to_target,float[,] Deg_to_target,float[,] Degree)
    {
        logic.Speed_func = speed;
        logic.Dist_func = Dist;
        logic.Dist_to_target_func = Dist_to_target;
        logic.Deg_to_target_func = Deg_to_target;
        logic.Degree_func = Degree;
        read_genetic();
    }


    public void read_genetic()
    {
        Gen_vect.Clear();


        for (int i = 0; i < logic.Speed_func.GetLength(0); i++)
        {
            for(int j = 0; j<4; j++)
            {
                Gen_vect.Add(logic.Speed_func[i, j]);
            }

        }
        
        for (int i = 0; i < logic.Dist_func.GetLength(0); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Gen_vect.Add(logic.Dist_func[i, j]);
            }

        }

        for (int i = 0; i < logic.Dist_to_target_func.GetLength(0); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Gen_vect.Add(logic.Dist_to_target_func[i, j]);
            }
        }


        for (int i = 0; i < logic.Deg_to_target_func.GetLength(0); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Gen_vect.Add(logic.Deg_to_target_func[i, j]);
            }
        }

        for (int i = 0; i < logic.Degree_func.GetLength(0); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Gen_vect.Add(logic.Degree_func[i, j]);
            }
        }
    }
    
}
