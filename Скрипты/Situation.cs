using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.UI;

[System.Serializable]
public class Situation
{
    [SerializeField]
    internal string Description;//��������
    [SerializeField]
    internal string Condition;//������� ��� ����, ����� �������� ���������
    [SerializeField]
    internal int ReferNumber;//�� ����� ���������� �����
    [SerializeField, Tooltip("�������������� ��� ������� ������, ��������� ����� ����� ��� ��������")]
    internal List<string> Conditions = new List<string>(); //������� �������
    [SerializeField, Tooltip("����������� ������� ������, ��������� ����� ����� ��� ��������. ��������� ��� 5 �������������, ���� ���� �������� =0")]
    internal List<string> Cons = new List<string>();//����������� �������

    [SerializeField]
    internal List<string> TextOfChoises = new List<string>(); //������ �������

    [SerializeField]
    internal List<string> AfterWords = new List<string>(); //����������� � ������� ���������

    //[SerializeField]
    //internal List<string> BeforeWords = new List<string>();//����������� � �������� ��� ������������ �������� ��������

    [SerializeField, Tooltip("������ ����������� �������, ����� ��� �������� ���������, ��������� ����� ������� ��� ��������")]
    internal string SpecialConditions;//������ ������ ������� (�������� �������) ��� ����������� ��������
    [SerializeField, Tooltip("������ ����������� ������� (�������), ��������� ����� ������� ��� ��������, ���� ����������� ���� �� � ���� �������, �� �� �� ����� ������� ������� (-)")]
    internal string SpecialCons;//������ ����������� �������
    [SerializeField, Tooltip("������ ����������� ������� ��� ������� ������, ��������� ����� ������� ��� ��������")]
    internal List<string> SpecialConditionsForChoises = new List<string>();//������ ������� ��� ������� ������

    [SerializeReference]
    internal string CodeWord;
    [SerializeField]
    internal string Picture;

    internal (int,int,int,int,int) Numbers()
    { 
        List<int> ToReturn = (Condition.Split(".")).Select(x => Int32.Parse(x)).ToList();
        return (ToReturn[0], ToReturn[1], ToReturn[2], ToReturn[3], ToReturn[4]);
    }

    internal List<int> GetCons(int N)
    {
        List<int> ToReturn = (Cons[N].Split(".")).Select(x => Int32.Parse(x)).ToList();
        return ToReturn;
    }

    internal List<List<int>> GetConditions()
    {
        int i = 0;
        List<List<int>> ToReturn = new List<List<int>>();
        foreach (var x in Conditions) 
        {
            ToReturn.Add((Conditions[i].Split(".")).Select(x => Int32.Parse(x)).ToList());
            i++;
        }
        return ToReturn;
    }


    //internal List<string> GetSpecialConditions()
    //{
    //    List<String> ToReturn = new List<string>();
    //    return ToReturn;
    //}
}
