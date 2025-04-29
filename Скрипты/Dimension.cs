using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimension
{
    private static Dictionary<string, int[]> CoordinatesOfButtons = new Dictionary<string, int[]>() { { "��������", new int[8] {-450, -324, 450, -324, -450, -432, 450, -432 } },
                                                                                      { "������������", new int [8] { 690,390,690,195,690,0,690,-195} },
                                                                                      { "�������������", new int [8] { 180,340,480,340,180,40,480,40} },
                                                                                      { "���������", new int [8] { -285,-220,285,-220,-285,-410,285,-410} },
                                                                                      { "���������������", new int [8] { -750,330, 750,330, -750,-330, 750,-330} },
                                                                                    };
    private static Dictionary<string, (int, int)> SizesOfButtons = new Dictionary<string, (int, int)>() { {"��������", (900,108) }, { "������������", (360,120) }, { "�������������", (220,220) }, {"���������", (500,120) }, {"���������������", (220,220) } };


    internal static Dictionary<string, (int, int)> CoordinatesOfTextBox = new Dictionary<string, (int, int)>() { {"��������", (0,-216) }, { "������������", (-210, 0) }, { "�������������", (-330,0) }, {"���������", (0,190) }, {"���������������", (0,0) } };
    internal static Dictionary<string, (int, int)> SizesOfTextBox = new Dictionary<string, (int, int)>() { { "��������", (1800, 108) }, {"������������", (1300,900) }, {"�������������", (660, 900) }, {"���������", (1070, 560) }, {"���������������",(1280,880) } };


    internal static Dictionary<string, (int, int)> SizesOfNextSitButton = new Dictionary<string, (int, int)>() { { "��������", (200,100) }, { "������������", (120,120) }, { "�������������", (100,100) }, { "���������", (100, 100) }, { "���������������", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfNextSitButton = new Dictionary<string, (int, int)>() { {"��������", (800, -112) }, { "������������",(810,-390)}, { "�������������", (710,400) }, { "���������", (585, -40)}, { "���������������", (-750, -90) } };

    //internal static Dictionary<string, (int, int)> SizesOfPrevSitButton = new Dictionary<string, (int, int)>() { {"��������", (1,1) }, {"������������", (120,120)}, {"�������������", (120,120) }, { "���������", (120, 120) }, { "���������������", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfPrevSitButton = new Dictionary<string, (int, int)>() { { "��������", (-800, -112) }, { "������������", (570, -390) }, { "�������������", (710, 300) }, { "���������", (585, 60) }, { "���������������", (-750, 90) } };


    internal static Dictionary<string, (int, int)> SizesOfStatsButton = new Dictionary<string, (int, int)>() { { "��������", (1, 1) }, { "������������", (120, 120) }, { "�������������", (100, 100) }, { "���������", (100, 100) }, { "���������������", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfStatsButton = new Dictionary<string, (int, int)>() { { "��������", (2000, 1) }, { "������������", (690, -390) }, { "�������������", (710, -400) }, { "���������", (-585, -40) }, { "���������������", (750, 0) } };

    public static (int, int) GetSizesOfButtons(string Smth)
    {
        return SizesOfButtons[Smth];
    }

    public static int[] GetCoordinatesOfButtons(string Smth)
    {
        return CoordinatesOfButtons[Smth];
    }

    internal static Dictionary<string, (float, float, float, float)> MarginsNoPicture = new Dictionary<string, (float, float, float, float)>() { {"��������", (10f,5f,10f,0f) } };
    internal static Dictionary<string, (float, float, float, float)> MarginsWithPicture = new Dictionary<string, (float, float, float, float)>() { { "��������", (10f, 5f, 10f, 0f) } };

    public static Vector4 TranslateToVector4(string CodeWord, bool Img)
    {
        if (Img)
            return new Vector4(MarginsWithPicture[CodeWord].Item1, MarginsWithPicture[CodeWord].Item2, MarginsWithPicture[CodeWord].Item3, MarginsWithPicture[CodeWord].Item4);
        else
            return new Vector4(MarginsNoPicture[CodeWord].Item1, MarginsNoPicture[CodeWord].Item2, MarginsNoPicture[CodeWord].Item3, MarginsNoPicture[CodeWord].Item4);
    }
}
