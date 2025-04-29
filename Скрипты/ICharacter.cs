using System.Collections.Generic;
using System.Numerics;
using Vector4 = UnityEngine.Vector4;

internal interface ICharacter
{
    public int[] Stats {get; set;}
    public string[] Names {get; set;}

    //public List<string> Important { get; set; }

    public void Settings();
    internal void ChangeStats(List<int> Numbers)
    {
        for (int i = 0; i < Numbers.Count; i++)
        {
            Stats[i] += Numbers[i];
            if (Stats[i] < 0)
                Stats[i] = 0;
            if (Stats[i] > 20)
                Stats[i] = 20;
        }
    }

    internal int CheckLose()
    {
        for (int i = 0; i < Stats.Length; i++)
            if (Stats[i] == 0)
                return i;
        return 9;
    }

    public void AddImportant(string Imp, int N);
    public bool CheckPossibility(string Smth, string Cons);
    public bool CheckForChoises(string Smth);

    public List<string> GetImportant();

    public void SetImportant(List<string> important);

    public void SetNames();

    public ((float, float), (float, float)) ResizeImage();

    public Vector4 GetRegularMargins();
    public Vector4 GetUnusualMargins();

}
