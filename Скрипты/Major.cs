using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Major :  ICharacter
{
    public int[] Stats { get; set; }
    public string[] Names { get; set; }

    public List<string> Important = new List<string>();
    public void Settings()
    {
        this.Stats = new int[5];
        for (int i = 0; i < Stats.Length; i++)
            Stats[i] = 5;
        SetNames();
    }

    public void SetNames()
    {
        this.Names = new string[5];
        Names[0] = "Бюджет";
        Names[1] = "Полиция";
        Names[2] = "Государство";
        Names[3] = "Компании";
        Names[4] = "Народ";
    }

    public void AddImportant(string Imp, int N)
    {
        if (Imp == "-")
            return;
        List<string> TestList = Imp.Split(",").ToList();
        if (TestList[N] != "-")
            this.Important.Add(TestList[N]);
    }

    public bool CheckPossibility(string Smth, string Cons)
    {

        if (Smth == "-" || Smth == "" || Smth == null || Smth.Length == 0)
            Debug.Log("");
        else
        {
            List<string> TestList = Smth.Split(", ").ToList();
            foreach (var item in TestList)
            {
                if (!Important.Contains(item))
                    return false;
            }
        }
        if (Cons == "" || Cons == null || Cons.Length == 0 || Cons == "-")
            Debug.Log("");
        else
        {
            List<string> Norm = Cons.Split(",").ToList();
            foreach (var item in Norm)
            {
                if (Important.Contains(item))
                    return false;
            }
        }
        return true;
    }

    public bool CheckForChoises(string Smth)
    {
        if (Smth == "" || Smth == "-" || Smth.Length == 0 || Smth.Length == 0)
            return true;
        else
        {
            List<string> TestList = Smth.Split(",").ToList();
            foreach (var item in TestList)
                if (!Important.Contains(item))
                    return false;
        }
        return true;
    }

    public List<string> GetImportant()
    {
        return Important;
    }
    public void SetImportant(List<string> important)
    {
        Important = important;
    }

    public ((float, float), (float, float)) ResizeImage()
    {
        return ((1020, 360), (0, -75));
    }

    public Vector4 GetRegularMargins()
    {
        return new Vector4(30, 15, 30, 25);
    }

    public Vector4 GetUnusualMargins()
    {
        return new Vector4(25, 15, 25, 400);
    }

}
