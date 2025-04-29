using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimension
{
    private static Dictionary<string, int[]> CoordinatesOfButtons = new Dictionary<string, int[]>() { { "Интервью", new int[8] {-450, -324, 450, -324, -450, -432, 450, -432 } },
                                                                                      { "КомпКорпорат", new int [8] { 690,390,690,195,690,0,690,-195} },
                                                                                      { "БумагаГенерал", new int [8] { 180,340,480,340,180,40,480,40} },
                                                                                      { "ГазетаМэр", new int [8] { -285,-220,285,-220,-285,-410,285,-410} },
                                                                                      { "ТелефонАктивист", new int [8] { -750,330, 750,330, -750,-330, 750,-330} },
                                                                                    };
    private static Dictionary<string, (int, int)> SizesOfButtons = new Dictionary<string, (int, int)>() { {"Интервью", (900,108) }, { "КомпКорпорат", (360,120) }, { "БумагаГенерал", (220,220) }, {"ГазетаМэр", (500,120) }, {"ТелефонАктивист", (220,220) } };


    internal static Dictionary<string, (int, int)> CoordinatesOfTextBox = new Dictionary<string, (int, int)>() { {"Интервью", (0,-216) }, { "КомпКорпорат", (-210, 0) }, { "БумагаГенерал", (-330,0) }, {"ГазетаМэр", (0,190) }, {"ТелефонАктивист", (0,0) } };
    internal static Dictionary<string, (int, int)> SizesOfTextBox = new Dictionary<string, (int, int)>() { { "Интервью", (1800, 108) }, {"КомпКорпорат", (1300,900) }, {"БумагаГенерал", (660, 900) }, {"ГазетаМэр", (1070, 560) }, {"ТелефонАктивист",(1280,880) } };


    internal static Dictionary<string, (int, int)> SizesOfNextSitButton = new Dictionary<string, (int, int)>() { { "Интервью", (200,100) }, { "КомпКорпорат", (120,120) }, { "БумагаГенерал", (100,100) }, { "ГазетаМэр", (100, 100) }, { "ТелефонАктивист", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfNextSitButton = new Dictionary<string, (int, int)>() { {"Интервью", (800, -112) }, { "КомпКорпорат",(810,-390)}, { "БумагаГенерал", (710,400) }, { "ГазетаМэр", (585, -40)}, { "ТелефонАктивист", (-750, -90) } };

    //internal static Dictionary<string, (int, int)> SizesOfPrevSitButton = new Dictionary<string, (int, int)>() { {"Интервью", (1,1) }, {"КомпКорпорат", (120,120)}, {"БумагаГенерал", (120,120) }, { "ГазетаМэр", (120, 120) }, { "ТелефонАктивист", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfPrevSitButton = new Dictionary<string, (int, int)>() { { "Интервью", (-800, -112) }, { "КомпКорпорат", (570, -390) }, { "БумагаГенерал", (710, 300) }, { "ГазетаМэр", (585, 60) }, { "ТелефонАктивист", (-750, 90) } };


    internal static Dictionary<string, (int, int)> SizesOfStatsButton = new Dictionary<string, (int, int)>() { { "Интервью", (1, 1) }, { "КомпКорпорат", (120, 120) }, { "БумагаГенерал", (100, 100) }, { "ГазетаМэр", (100, 100) }, { "ТелефонАктивист", (100, 100) } };
    internal static Dictionary<string, (int, int)> CoordinatesOfStatsButton = new Dictionary<string, (int, int)>() { { "Интервью", (2000, 1) }, { "КомпКорпорат", (690, -390) }, { "БумагаГенерал", (710, -400) }, { "ГазетаМэр", (-585, -40) }, { "ТелефонАктивист", (750, 0) } };

    public static (int, int) GetSizesOfButtons(string Smth)
    {
        return SizesOfButtons[Smth];
    }

    public static int[] GetCoordinatesOfButtons(string Smth)
    {
        return CoordinatesOfButtons[Smth];
    }

    internal static Dictionary<string, (float, float, float, float)> MarginsNoPicture = new Dictionary<string, (float, float, float, float)>() { {"Интервью", (10f,5f,10f,0f) } };
    internal static Dictionary<string, (float, float, float, float)> MarginsWithPicture = new Dictionary<string, (float, float, float, float)>() { { "Интервью", (10f, 5f, 10f, 0f) } };

    public static Vector4 TranslateToVector4(string CodeWord, bool Img)
    {
        if (Img)
            return new Vector4(MarginsWithPicture[CodeWord].Item1, MarginsWithPicture[CodeWord].Item2, MarginsWithPicture[CodeWord].Item3, MarginsWithPicture[CodeWord].Item4);
        else
            return new Vector4(MarginsNoPicture[CodeWord].Item1, MarginsNoPicture[CodeWord].Item2, MarginsNoPicture[CodeWord].Item3, MarginsNoPicture[CodeWord].Item4);
    }
}
