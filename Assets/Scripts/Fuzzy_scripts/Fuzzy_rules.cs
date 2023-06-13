using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy_rules : MonoBehaviour
{
    public Fuzzy_logic l;
    public string Fuzzy_Number;
    private void FixedUpdate()
    {
        Rules();
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
        if (l.Dist_to_target == "��������") { l.Speed = "����"; l.Degree = "�����"; Fuzzy_Number = "������ ���������"; }
    }
    
}
