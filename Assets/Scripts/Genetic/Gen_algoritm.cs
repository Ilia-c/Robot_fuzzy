using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Gen_algoritm : MonoBehaviour
{
    // Создает особей и ведет отбор и мутации

    public float[,] Speed_func =            {{-0.2f, -0.1f, 0.1f, 0.2f},    // Стоп
                                             {0, 2, 3, 4},                  // Медлено
                                             {3.5f, 5, 8, 10},              // Средне
                                             {8.5f, 12, 15, 20}};           // Быстро

    public float[,] Dist_func =             {{0, 1f, 2.5f, 4f},              // Тело
                                             {3.5f, 5f, 6f, 8f},             // Близко
                                             {7.5f, 10f, 14f, 15f},          // Средне
                                             {14.5f, 16, 50, 200}};          // Далеко

    public float[,] Dist_to_target_func =   {{0, 1.5f, 2f, 3},              // ВПЛОТНУЮ
                                             {2.5f, 5f, 8f, 10},            // Близко
                                             {9.5f, 11f, 20f, 30},          // Средне
                                             {25f, 30, 50, 100}};           // Далеко

    public float[,] Deg_to_target_func =    {{-20, -18.5f, 18.5f, 20},      // Норма
                                             {-100, -90, -25, -18.5f},      // Левее
                                             {-180, -170, -110, -95},       // Сильно левее
                                             {18.5f, 25, 90, 100},          // Правее
                                             {95, 110, 170, 180}};          // Сильно правее

    public float[,] Degree_func =           {{0, 0, 0, 0},                  // Норма
                                             {-1, -1, -1, -1},              // Левее
                                             {-2, -2, -2, -2},              // Сильно левее
                                             {1, 1, 1, 1},                  // Правее
                                             {2, 2, 2, 2}};                 // Сильно правее

    public GameObject Robot;
    public GameObject Collect_robot;
    public Transform Target;
    public Transform Spawn_point;
    private List<float> Gen_vect = new List<float>(); // Все гены

    public float chance_mutation = 0.01f;
    public int max_population = 100;

    //private List<GameObject> Population = new List<GameObject>(); // Список особей
    //private List<GameObject> Population_next = new List<GameObject>();

    //private List<float> Population_next_time = new List<float>();

    public float max_rand = 5;
    public int population_size_start = 10;
    // Таймер
    private float start_time = 0.0f;
    public float period = 30f;
    public struct Population
    {
        public GameObject gameobj;
        public float y;
    }
    private List<Population> Pop_st = new List<Population>(); // Список особей
    private List<Population> Pop_next = new List<Population>(); // Список особей

    private void Start()
    {
        //read_genetic();
        create_first_population();

        start_time = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - start_time >= period)
        {

            Debug.Log(1);
            Population_selection();
            start_time = Time.time;
        }
    }

    private void Population_selection()
    {

        //Pop_st.Clear();
        // Изменение стоимсоти скрещивания
        float time_summ = 0;

        for (int i = 0; i < Pop_next.Count; i++)
        {
            time_summ += 1 / Pop_next[i].y;
        }
        
        for (int i = 0; i < Pop_next.Count; i++)
        {
            Population next_p = Pop_next[i];
            next_p.y = 1 / Pop_next[i].y / time_summ;
            Pop_next[i] = next_p;
        }


        List<float[]> new_gen = new List<float[]>();
        for (int i = 0; i < Pop_next.Count; i++)
        {
            for (int j = 0; j < Pop_next.Count; j++)
            {
                float ver = UnityEngine.Random.Range(0, 1.0f);
                if (Pop_next[j].y > ver)
                {
                    float[] gen = cross(Pop_next[i], Pop_next[j], i,j);
                    new_gen.Add(gen);
                }
            }
        }
        for (int i = 0; i < Pop_st.Count; i++)
        {
            Destroy(Pop_st[i].gameobj);
        }
        Pop_st.Clear();
        Pop_next.Clear();
        for (int i = max_population; i < new_gen.Count; i++)
        {
            new_gen.RemoveAt(i);
        }

        
        if (new_gen.Count < population_size_start)
        {
            int s = population_size_start;
            population_size_start = population_size_start - new_gen.Count;
            create_first_population();
            population_size_start = s;
        }
        

        population_create(new_gen);
        start_time = Time.time;
    }

    private void population_create(List<float[]> new_gen)
    {
        for (int i = 0; i < new_gen.Count; i++)
        {
            GameObject new_robot = Instantiate(Robot, new Vector3(Spawn_point.position.x, 0, Spawn_point.position.z), Quaternion.identity);
            new_robot.transform.SetParent(Collect_robot.transform);
            new_robot.GetComponent<Fuzzy_logic>().target = Target;


            float[,] Speed = write_func(new_gen[i], 0, 4);
            float[,] Dist = write_func(new_gen[i], 16, 4);
            float[,] Dist_to_target = write_func(new_gen[i], 32, 4);
            float[,] Deg_to_target = write_func(new_gen[i], 48, 5);
            float[,] Degree = write_func(new_gen[i], 68, 5);


            new_robot.GetComponent<Gen>().gen_accept(Speed, Dist, Dist_to_target, Deg_to_target, Degree);

            new_robot.GetComponent<Move>().Gen_alg = Collect_robot;
            new_robot.GetComponent<Fuzzy_rules>().Gen_alg = Collect_robot;

            int a = Pop_st.Count;
            new_robot.name = "Robot " + a.ToString();

            Population next_p = new Population();
            next_p.gameobj = new_robot;
            Pop_st.Add(next_p);
        }
    }


    private float[] cross(Population x, Population y, int first, int second)
    {
        //Population result = new Population();
        //  public float chance_mutation = 0.1f;
        //  public int max_population = 100;

        int gens_count = x.gameobj.GetComponent<Gen>().Gen_vect.Count;
        float[] new_gen = new float[gens_count];
        var array = new[] { 0, 1, 2, 3, 32, 33, 34, 35, 68, 69, 70, 71 }; // Исключение из мутации


        for (int i = 0; i<gens_count; i++)
        {
            if (first != second)
            {
                if (UnityEngine.Random.Range(0, 1.0f) < x.y)
                {
                    new_gen[i] = x.gameobj.GetComponent<Gen>().Gen_vect[i];
                }
                else
                {
                    new_gen[i] = y.gameobj.GetComponent<Gen>().Gen_vect[i];
                }

                // Мутация
                if ((UnityEngine.Random.Range(0, 1) < chance_mutation) & !array.Contains(i))
                {
                    new_gen[i] += UnityEngine.Random.Range(-5, 5);
                }
            }
            else
            {
                new_gen[i] = x.gameobj.GetComponent<Gen>().Gen_vect[i];
            }
        }


        return new_gen;
    }

    public void target_accept(float time, string name)
    {
        for (int i = 0; i < Pop_st.Count; i++)
        {
            if (Pop_st[i].gameobj.name == name)
            {
                Population next_p = Pop_st[i];
                next_p.y = time;
                //Pop_st[i] = next_p;
                Pop_next.Add(next_p);
                //Population_next_time.Add(time);
            }
        }
        Debug.Log(name + " " + time);
    }

    private void create_first_population()
    {
        for (int i = 0; i < population_size_start; i++)
        {
            GameObject new_robot = Instantiate(Robot, new Vector3(Spawn_point.position.x, 0, Spawn_point.position.z), Quaternion.identity);
            new_robot.transform.SetParent(Collect_robot.transform);

            new_robot.GetComponent<Fuzzy_logic>().target = Target;

            float[,] Speed = random_func(Speed_func, 1);
            float[,] Dist = random_func(Dist_func, 0);
            float[,] Dist_to_target = random_func(Dist_to_target_func, 1);
            float[,] Deg_to_target = random_func(Deg_to_target_func, 0);
            float[,] Degree = random_func(Degree_func, 1);
            new_robot.GetComponent<Gen>().gen_accept(Speed, Dist, Dist_to_target, Deg_to_target, Degree);

            new_robot.GetComponent<Move>().Gen_alg = Collect_robot;
            new_robot.GetComponent<Fuzzy_rules>().Gen_alg = Collect_robot;

            int a = Pop_st.Count;
            new_robot.name = "Robot " + a.ToString();

            Population next_p = new Population();
            next_p.gameobj = new_robot;
            Pop_st.Add(next_p);
        }
    }




    public void robot_destroy(string name)
    {
        for (int i = 0; i < Pop_st.Count; i++) 
        { 
            if (Pop_st[i].gameobj.name == name)
            {
                Destroy(Pop_st[i].gameobj);
                Pop_st.RemoveAt(i);
            } 
        }

    }



    private float[,] random_func(float[,] func, int first)
    {
        float[,] ret = new float[func.GetLength(0), func.GetLength(1)];


        for (int i = 0; i < ret.GetLength(0); i++)
        {
            for (int j = 0; j < ret.GetLength(1); j++)
            {
                if (first == 1)
                {
                    ret[i, j] = func[i, j];
                }
                else
                {
                    ret[i, j] = func[i, j] + + UnityEngine.Random.Range(-max_rand, max_rand);
                } 
            }
            first = 0;
        }
        return ret;
    }

    private float[,] write_func(float[] new_gen, int first, int size)
    {
        float[,] ret = new float[size, 4];

        int count = first;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                ret[i, j] = new_gen[count];
                count++;
            }
        }
        return ret;
    }

}
