using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.UI;

[System.Serializable]
public class Situation
{
    [SerializeField]
    internal string Description;//описание
    [SerializeField]
    internal string Condition;//условия для того, чтобы ситуация произошла
    [SerializeField]
    internal int ReferNumber;//не очень уникальный номер
    [SerializeField, Tooltip("Характеристики для каждого выбора, заполнять через точку без пробелов")]
    internal List<string> Conditions = new List<string>(); //условия выборов
    [SerializeField, Tooltip("Последствия каждого выбора, заполнять через точку без пробелов. Указывать все 5 характеристик, даже если значение =0")]
    internal List<string> Cons = new List<string>();//последствия выборов

    [SerializeField]
    internal List<string> TextOfChoises = new List<string>(); //тексты выборов

    [SerializeField]
    internal List<string> AfterWords = new List<string>(); //Послесловие с выводом изменений

    //[SerializeField]
    //internal List<string> BeforeWords = new List<string>();//Предисловие в основном для обязательных сюжетных ситуаций

    [SerializeField, Tooltip("Список необходимых СОБЫТИЙ, чтобы эта ситцауия произошла, заполнять через запятую без пробелов")]
    internal string SpecialConditions;//Список особых условий (ключевые события) для проишествия ситуации
    [SerializeField, Tooltip("Особые последствия выборов (События), заполнять через запятую без пробелов, если последствия есть не у всех выборов, то на их месте ставить прочерк (-)")]
    internal string SpecialCons;//особые последствия выборов
    [SerializeField, Tooltip("Списки необходимых СОБЫТИЙ для каждого выбора, заполнять через запятую без пробелов")]
    internal List<string> SpecialConditionsForChoises = new List<string>();//Особые условия для каждого выбора

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
