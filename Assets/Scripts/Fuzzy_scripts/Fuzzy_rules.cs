using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy_rules : MonoBehaviour
{
    public Fuzzy_logic l;

    private void FixedUpdate()
    {
        Rules();
    }

    private void Rules()
    {
        // �������� � ������

        if ((l.To_target == "�����") && (l.Distance_front == "������") && (l.Dist_to_target == "������")) { l.Speed = "������"; }
        if ((l.To_target == "�����") && ((l.Distance_front == "������") || (l.Distance_front == "������")) && (l.Dist_to_target == "������")) { l.Speed = "������"; }
        if ((l.To_target == "�����") && ((l.Distance_front == "������") || (l.Distance_front == "������") || (l.Distance_front == "������")) && (l.Dist_to_target == "������")) {l.Speed = "��������"; }
        if ((l.To_target == "�����") && ((l.Distance_front == "������") || (l.Distance_front == "������") || (l.Distance_front == "������")) && (l.Dist_to_target == "��������")) { l.Speed = "����"; }

        //�������� �� �����������
        if ((l.To_target == "�����") && (l.Distance_front == "������") && ((l.Distance_left == "������") || ((l.Distance_left == "������")))) { l.Degree = "�����"; }
        if ((l.To_target == "�����") && (l.Distance_front == "������") && ((l.Distance_right == "������") || ((l.Distance_right == "������")))) { l.Degree = "������"; }

        //�������� �� ������������
        if ((l.To_target == "�����") && ((l.Distance_front == "������") || (l.Distance_front == "������")) && (l.Distance_left == "������")) { l.Speed = "������"; }
        if ((l.To_target == "������") && ((l.Distance_front == "������") || (l.Distance_front == "������")) && (l.Distance_right == "������")) { l.Speed = "������"; }

        //���������� �������
        if (((l.To_target == "�����") || (l.To_target == "������ �����")) && ((l.Distance_left == "������") || (l.Distance_left == "������"))) { l.Degree = "�����"; }
        if (((l.To_target == "������") || (l.To_target == "������ ������")) && ((l.Distance_right == "������") || (l.Distance_right == "������"))) { l.Degree = "������"; }

        //����� �� ��������
        if (((l.To_target == "�����") || (l.To_target == "�����")) && ((l.Distance_front == "������") || (l.Distance_front == "������"))) { l.Speed = "������"; }
        if (((l.To_target == "������") || (l.To_target == "�����")) && ((l.Distance_front == "������") || (l.Distance_front == "������"))) { l.Speed = "������"; }

        //������������ �� ��������
        //if ((l.To_target == "�����") || ()) 

        /*
        if ((l.To_target == "�����") && (l.Distance_front == "������") && (l.Distance_left == "������")) 
            {
            l.Degree = "������ ������";
                if ((l.To_target == "�����") && (l.Distance_front == "������") && (l.Distance_left == "������")) { l.Speed = "������";}
            }
        if ((l.To_target == "�����") && (l.Distance_front == "������")) { l.Degree = "�����"; }
        if ((l.To_target == "������") && (l.Distance_front == "������")) { l.Degree = "������"; }
        if ((l.To_target == "������ ������") && (l.Distance_left == "������")) { l.Degree = "������ ������"; } else if (l.Distance_front == "������") { l.Speed = "������"; }
        if ((l.To_target == "������ �����") && (l.Distance_right == "������")) { l.Degree = "������ �����"; } else if (l.Distance_front == "������") { l.Speed = "������"; }
        */
    }
}
