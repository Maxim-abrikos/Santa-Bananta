using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    internal UnityEngine.UI.Button[] Buttons = new UnityEngine.UI.Button[4];
    [SerializeField]
    internal Plot plot;
    [SerializeField]
    private Image phone;

    // Start is called before the first frame update
    public void Start()
    {
        if (!plot.LoadData()) //если нет сохранений - новая игра
        {
            Buttons[0].gameObject.SetActive(true);
            Buttons[1].gameObject.SetActive(true);
            Buttons[2].gameObject.SetActive(true);
            Buttons[3].gameObject.SetActive(true);
            phone.gameObject.SetActive(true);
            phone.enabled = true;
            Buttons[0].onClick.AddListener(() => { plot.SelectCharacter(0); CloseAll(); phone.enabled = false; });
            Buttons[1].onClick.AddListener(() => { plot.SelectCharacter(1); CloseAll(); phone.enabled = false; });
            Buttons[2].onClick.AddListener(() => { plot.SelectCharacter(2); CloseAll(); phone.enabled = false; });
            Buttons[3].onClick.AddListener(() => { plot.SelectCharacter(3); CloseAll(); phone.enabled = false; });
        }
        else
            //plot.InvalidStart();
            plot.Play();
        //StartCoroutine( StartPlay());
    }

    //public IEnumerator StartPlay()
    //{
    //    yield return StartCoroutine(plot.Play());
    //}
    private void CloseAll()
    {
        Buttons[0].gameObject.SetActive(false);
        Buttons[1].gameObject.SetActive(false);
        Buttons[2].gameObject.SetActive(false);
        Buttons[3].gameObject.SetActive(false);
        Buttons[3].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
