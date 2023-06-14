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
                if ((l.Speed != "����") & (l.Degree != "�����")) Gen_alg.GetComponent<Gen_algoritm>().robot_destroy(transform.name);
                cycle++;
                l.Speed = "����";
                l.Degree = "�����";
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
        //�������� �� ����
        if (l.Distance_45_left == "������") { l.Speed = "��������"; l.Degree = "������ ������"; Fuzzy_Number = "������� 45 �����"; }
        if (l.Distance_left == "������") { l.Speed = "����"; l.Degree = "������"; Fuzzy_Number = "������� �� ���� �����"; }
        if (l.Distance_45_right == "������") { l.Speed = "����"; l.Degree = "������ �����"; Fuzzy_Number = "������� 45 ����"; }
        if (l.Distance_right == "������") { l.Speed = "��������"; l.Degree = "�����"; Fuzzy_Number = "������� �� ���� ����"; }


        //��������� ������
        if ((l.Distance_front == "������") && (l.Distance_45_right != "������") && (l.Distance_45_left != "������")) { l.Speed = "������"; }
        if ((l.Distance_front != "������")) { l.Speed = "������"; }
        if ((l.Distance_front == "����")) { l.Speed = "����"; }

        //�������� �� �����
        if (((l.To_target == "�����") || (l.To_target == "������ �����")) && (l.Distance_front != "������") && (l.Distance_front != "����") && (l.Distance_45_left != "������")) { l.Degree = "�����"; l.Speed = "������"; Fuzzy_Number = "������� �� ����� ����"; }
        if (((l.To_target == "������") || (l.To_target == "������ ������")) && (l.Distance_front != "������") && (l.Distance_front != "����") && (l.Distance_45_right != "������")) { l.Degree = "������"; l.Speed = "������"; Fuzzy_Number = "������� �� ����� �����"; }
        if (l.To_target == "�����") { l.Degree = "�����"; l.Speed = "������"; Fuzzy_Number = "���� �����, ������ ������"; }

        //��������
        if ((l.Distance_front == "������"))
        {
            if ((l.Distance_left != "������") && (l.Distance_left != "����")) { l.Speed = "����"; l.Degree = "������ �����"; Fuzzy_Number = "������� ����"; }
            if ((l.Distance_right != "������") && (l.Distance_right != "����")) { l.Speed = "��������"; l.Degree = "������"; Fuzzy_Number = "������� �����"; }

        }

        //�������� �� ������ ��� ���������
        if ((l.Distance_right == "������") && (l.Distance_45_right == "������"))
        {
            Fuzzy_Number = "�������� �� ����� �����";
            l.Speed = "��������";
            if ((l.Distance_45_right == "����") || (l.Distance_front == "������")) { l.Degree = "������ �����"; l.Speed = "������"; Fuzzy_Number = "�������� �� ����� ����� ����������"; }
        }
        if ((l.Distance_left == "������") && (l.Distance_45_left == "������"))
        {
            Fuzzy_Number = "�������� �� ����� ����";
            l.Speed = "��������";
            if ((l.Distance_45_left == "����") || (l.Distance_front == "������")) { l.Degree = "������ ������"; l.Speed = "������"; Fuzzy_Number = "�������� �� ����� ���� ����������"; }

        }



        //������� �� ����
        if ((l.Dist_to_target == "������") || (l.Dist_to_target == "������") && (l.Distance_front != "����") && (l.Distance_front != "������"))
        {
            if (((l.To_target == "�����") || (l.To_target == "������ �����")) && (l.Distance_front != "����")) { l.Degree = "�����"; l.Speed = "��������"; }
            if (((l.To_target == "������") || (l.To_target == "������ ������")) && (l.Distance_front != "����")) { l.Degree = "������"; l.Speed = "��������"; }
        }
        //���� ����� �����
        if ((l.Distance_right == "����") || (l.Distance_45_right == "����")) { l.Degree = "�����"; l.Speed = "��������"; }
        if ((l.Distance_left == "����") || (l.Distance_45_left == "����")) { l.Degree = "������"; l.Speed = "��������"; }

        if (l.Dist_to_target == "��������") { 
            l.Speed = "����";
            l.Degree = "�����"; 
            Fuzzy_Number = "������ ���������"; 
            stop = true; 
        }
    }
    
}
