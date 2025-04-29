using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;
using Unity.VisualScripting;


[System.Serializable]
public class Plot : MonoBehaviour
{
    internal ICharacter Dude;
    private Dictionary<int, ICharacter> Dudes = new Dictionary<int, ICharacter> { { 0, new Major() }, { 1, new Corporate() }, { 2, new Activist() }, { 3, new Colonel() } };
    private Dictionary<int, List<FewSituations>> Scenario = new Dictionary<int, List<FewSituations>>();
    [SerializeField]
    internal List<FewSituations> SituationsMajor = new List<FewSituations>();
    [SerializeField]
    internal List<FewSituations> SituationsCorporate = new List<FewSituations>();
    [SerializeField]
    internal List<FewSituations> SituationsActivist = new List<FewSituations>();
    [SerializeField]
    internal List<FewSituations> SituationsColonel = new List<FewSituations>();
    private List<FewSituations> CurrentPlot;

    [SerializeField]
    private List<Situation> Situations = new List<Situation>();

    private int CurrentNumber = 0;
    private int Counter = 0;

    [SerializeField]
    internal Button[] ChoiseButtons = new Button[4];
    [SerializeField]
    internal TextMeshProUGUI[] ChoiseTextes = new TextMeshProUGUI[4];

    [SerializeField]
    internal TMP_Text TextBox;

    private Situation CurrentSituation;

    [SerializeField]
    private Button NextSitButton;
    [SerializeField]
    private Button PrevSitButton;
    [SerializeField]
    private Button ShowStatsButton;
    [SerializeField]
    private Button CloseAllButton;

    private int Character;

    public void SelectCharacter(int N) //Выбор персонажа
    {
        Dude = Dudes[N];
        Dude.Settings();
        Character = N;
        CurrentPlot = Scenario[N];
        Play();
    }

    internal bool LoadData() //Загрузка прогресса
    {
        if (PlayerPrefs.HasKey("Ситуации") && (PlayerPrefs.HasKey("Характеристики")) && (PlayerPrefs.HasKey("События")) && (PlayerPrefs.HasKey("Уровень")) && (PlayerPrefs.HasKey("Персонаж"))) 
        {
            Dude = Dudes[PlayerPrefs.GetInt("Персонаж")];
            Dude.Stats = (int[])JsonConvert.DeserializeObject(PlayerPrefs.GetString("Характеристики"));
            Dude.SetImportant((List<string>)(JsonConvert.DeserializeObject(PlayerPrefs.GetString("События"))));
            Situations.AddRange((List<Situation>)(JsonConvert.DeserializeObject(PlayerPrefs.GetString("Ситуации"))));
            CurrentNumber = PlayerPrefs.GetInt("Уровень");
            return true;
        }
        else
            return false;
    }
    public void Start()
    {
        ChoiseButtons[0].onClick.AddListener(() => StartCoroutine(Choose(0)));
        ChoiseButtons[1].onClick.AddListener(() => StartCoroutine(Choose(1)));
        ChoiseButtons[2].onClick.AddListener(() => StartCoroutine(Choose(2)));
        ChoiseButtons[3].onClick.AddListener(() => StartCoroutine(Choose(3)));
        NextSitButton.onClick.AddListener(() => Play());
        PrevSitButton.onClick.AddListener(() => PreviousSentence());

        Scenario.Add(0, SituationsMajor);
        Scenario.Add(1, SituationsCorporate);
        Scenario.Add(2, SituationsActivist);
        Scenario.Add(3, SituationsColonel);
        TBImage = TextBox.GetComponentInChildren<Image>();

        foreach (Button B in ChoiseButtons)
            B.gameObject.SetActive(false);
        NextSitButton.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Play() //Движение по сюжету
    {
        foreach (var button in ChoiseButtons)
            button.gameObject.SetActive(false);

        NextSitButton.gameObject.SetActive(false);
        if (Counter >= Situations.Count)
        {
            Situations.AddRange(AddSituations());
            CurrentNumber++;
        }
        if (Counter < Situations.Count)
        {
            CurrentSituation = Situations[Counter];
            Sentence = CurrentSituation.Description.Split("#").ToList();
            Counter++;
            StartCoroutine(NextSentence());
        }
        else 
        {
            //Тут будет выход на концовку
        }
        return;
    }

    public List<Situation> AddSituations() //Добавление ситуаций из нового сюжетного витка
    {
        (int, int, int, int, int) Omega;
        List<Situation> S = new List<Situation>();
        if (!(CurrentNumber >= CurrentPlot.Count))
            S = CurrentPlot[CurrentNumber].Situations;
        else
        {
            Debug.Log("Всё");
            return new List<Situation>();
        }
        List<Situation> S1 = new List<Situation>();
        if (S.Count == 1)
            return S;
        else 
        {
            foreach (Situation s in S) 
            {
                Omega = s.Numbers();
                if (Dude.Stats[0] >= Omega.Item1 && Dude.Stats[1] >= Omega.Item2 && Dude.Stats[2] >= Omega.Item3 && Dude.Stats[3] >= Omega.Item4 && Dude.Stats[4] >= Omega.Item5)
                    if (Dude.CheckPossibility(s.SpecialConditions, s.SpecialCons))
                        S1.Add(s);
            }
            return ((S1.GroupBy(obj => obj.ReferNumber / 100).Select(group => group.OrderByDescending(obj => obj.ReferNumber % 100).First()).ToList()).OrderBy(x => Guid.NewGuid()).ToList());
        }
    }


    private List<string> Sentence;
    private int SentenceCounter = 0;
    private int StepForward = 1, StepBackward = 1;


    private RectTransform rectTransform, Imagetransform, NSRT, PSRT, SSB;
    Image TBImage;
    IEnumerator NextSentence() //Переход к следующей ситуации или к следующему тексту
    {
        (int, int) Sizes = Dimension.SizesOfTextBox[CurrentSituation.CodeWord];
        (int, int) Coordinates = Dimension.CoordinatesOfTextBox[CurrentSituation.CodeWord];
        rectTransform = TextBox.GetComponent<RectTransform>();
        
        rectTransform.sizeDelta = new Vector2(Sizes.Item1, Sizes.Item2);
        rectTransform.anchoredPosition = new Vector2(Coordinates.Item1, Coordinates.Item2);
        TBImage.enabled = true;
        TBImage.gameObject.SetActive(true);
        (int, int) NSRT_2 = Dimension.CoordinatesOfNextSitButton[CurrentSituation.CodeWord];
        (int, int) NSRT_1 = Dimension.SizesOfNextSitButton[CurrentSituation.CodeWord];
        NSRT = NextSitButton.GetComponent<RectTransform>();
        NSRT.sizeDelta = new Vector2(NSRT_1.Item1, NSRT_1.Item2);
        NSRT.anchoredPosition = new Vector2(NSRT_2.Item1, NSRT_2.Item2);


        PSRT = PrevSitButton.GetComponent<RectTransform>();
        (int,int) PSRT_2 = Dimension.CoordinatesOfPrevSitButton[CurrentSituation.CodeWord];
        PSRT.sizeDelta = new Vector2(NSRT_1.Item1, NSRT_1.Item2);
        PSRT.anchoredPosition = new Vector2(PSRT_2.Item1, PSRT_2.Item2);

        SSB = ShowStatsButton.GetComponent<RectTransform>();
        (int, int) SSB_Size = Dimension.SizesOfStatsButton[CurrentSituation.CodeWord];
        SSB.sizeDelta = new Vector2(SSB_Size.Item1, SSB_Size.Item2);
        (int, int) SSB_Coordinates = Dimension.CoordinatesOfStatsButton[CurrentSituation.CodeWord];
        SSB.anchoredPosition = new Vector2(SSB_Coordinates.Item1, SSB_Coordinates.Item2);
        ShowStatsButton.enabled = true;
        ShowStatsButton.gameObject.SetActive(true);

        TextBox.text = "";
        if (CurrentSituation.Picture == "-" || CurrentSituation.Picture == null || CurrentSituation.Picture == "")
        {
            TextBox.margin = Dude.GetRegularMargins();
            TBImage.gameObject.SetActive(false);
            //TBImage.enabled = false;
        }
        else
        {
            TextBox.margin = Dude.GetUnusualMargins();
            TBImage.gameObject.SetActive(true);
            TBImage.enabled = true;
            Imagetransform = TBImage.GetComponent<RectTransform>();
            ((float, float), (float, float)) Params = Dude.ResizeImage();
            Imagetransform.sizeDelta = new Vector2(Params.Item1.Item1, Params.Item1.Item2);
            Imagetransform.anchoredPosition = new Vector2(Params.Item2.Item1, Params.Item2.Item1);
        }

        SentenceCounter += StepForward;
        StepForward = 1;
        StepBackward = 2;
        if (SentenceCounter > 1)
        {
            PrevSitButton.gameObject.SetActive(true);
            PrevSitButton.enabled = true;
        }

        if (SentenceCounter >= Sentence.Count)
        {
            NextSitButton.onClick.RemoveAllListeners();
            NextSitButton.onClick.AddListener(() => StartCoroutine(DisplayChooses()));
            NextSitButton.enabled = false;
            NextSitButton.gameObject.SetActive(false);
            SentenceCounter = 0;
            StartCoroutine(DisplayChooses());
            yield break;
        }

        TextBox.text = Sentence[SentenceCounter-1];

        NextSitButton.enabled = true;
        NextSitButton.gameObject.SetActive(true);
        NextSitButton.onClick.RemoveAllListeners();
        NextSitButton.onClick.AddListener(() => StartCoroutine(NextSentence()));

        yield break;
    }

    void PreviousSentence() //Возврат к предыдущему тексту
    {
        SentenceCounter-= StepBackward;
        StepBackward = 1;

        StepForward = 2;
        if (SentenceCounter < 0)
            SentenceCounter = 0;
        TextBox.text = Sentence[SentenceCounter];
        if (SentenceCounter == 0)
        {
            PrevSitButton.gameObject.SetActive(false);
            PrevSitButton.enabled = false;
        }

        NextSitButton.enabled = true;
        NextSitButton.gameObject.SetActive(true);
        NextSitButton.onClick.RemoveAllListeners();
        NextSitButton.onClick.AddListener(() => StartCoroutine(NextSentence()));

        return;
    }

    IEnumerator DisplayChooses() //Отображение возможных выборов
    {
        TextBox.text = Sentence[Sentence.Count-1];
        SentenceCounter = Sentence.Count;

        List<List<int>> Omega = CurrentSituation.GetConditions();
        int i = 0;
        (int, int) sus = Dimension.GetSizesOfButtons(CurrentSituation.CodeWord);
        int[] bepis = Dimension.GetCoordinatesOfButtons(CurrentSituation.CodeWord);

        foreach (var Amogus in Omega)
        {
            if (Dude.Stats[0]>= Amogus[0] && Dude.Stats[1] >= Amogus[1] && Dude.Stats[2] >= Amogus[2] && Dude.Stats[3] >= Amogus[3] && Dude.Stats[4] >= Amogus[4]) 
            {
                if (Dude.CheckForChoises(CurrentSituation.SpecialConditionsForChoises[i]))
                {
                    ChoiseButtons[i].gameObject.SetActive(true);
                    ChoiseTextes[i] = ChoiseButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    ChoiseTextes[i].text = CurrentSituation.TextOfChoises[i];
                    RectTransform buttonRect = ChoiseButtons[i].GetComponent<RectTransform>();
                    buttonRect.sizeDelta = new Vector2(sus.Item1, sus.Item2);
                    buttonRect.anchoredPosition = new Vector2(bepis[i * 2], bepis[i * 2 + 1]);
                }
        }
            i++;
        }
        yield break;
    }


    IEnumerator Choose(int N) //Выбор действия
    {
        SentenceCounter = 0;
        Dude.ChangeStats(CurrentSituation.GetCons(N));
        if (CurrentSituation.SpecialCons != "-")
            Dude.AddImportant(CurrentSituation.SpecialCons, N);
        foreach (Button B in ChoiseButtons)
            B.gameObject.SetActive(false);
        Task.Delay(800).Wait();
        StartCoroutine(ShowAfterWords(N));
        //TBImage.gameObject.SetActive(false);
        yield break;
    }

    IEnumerator ShowAfterWords(int N) //Отображение последствий выбора
    {
        PrevSitButton.enabled = false;
        PrevSitButton.gameObject.SetActive(false);

        TextBox.text = CurrentSituation.AfterWords[N];
        Task.Delay(800).Wait();
        NextSitButton.gameObject.SetActive(true);
        NextSitButton.enabled = true;

        NextSitButton.onClick.RemoveAllListeners();
        NextSitButton.onClick.AddListener(() => Play());
        yield break;
    }

    private void SaveData() //Сохранение прогресса
    {
        List<Situation> SaveSits = new List<Situation>();
        for (int i = Counter; i < Situations.Count; i++)
        {
            SaveSits.Add(Situations[i]);
        }
        PlayerPrefs.SetString("Ситуации", JsonConvert.SerializeObject(SaveSits));
        PlayerPrefs.SetString("Характеристики", JsonConvert.SerializeObject(Dude.Stats));
        PlayerPrefs.SetString("События", JsonConvert.SerializeObject(Dude.GetImportant()));
        PlayerPrefs.SetInt("Уровень", CurrentNumber);
        PlayerPrefs.SetInt("Персонаж", Character);
    }

    IEnumerator ShowStats()
    {

        yield break;
    }

    //public void OnDestroy() //Ещё дойду до этого
    //{
    //    SaveData();
    //}
}