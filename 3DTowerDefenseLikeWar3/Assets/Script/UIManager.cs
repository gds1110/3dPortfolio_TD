using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI")]
    public GameObject root;
    private List<GameObject> allUI = new List<GameObject>();

    [Header("Wave")]
    public GameObject waveInfo;
    private Text[] waveInfoText;

    [Header("LIFE")]
    public GameObject LifeInfo;
    public Text lifeText;

    [Header("Resource")]
    public GameObject ManaHole;
    private Text[] manaInfo;


    [Header("Menu")]
    public GameObject gameMenuObject;
    private bool gameMenuShowing = false;

    [Header("Gold")]
    public GameObject GoldInfo;
    public Text gold;


    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
        recapInfoText = recapInfo.GetComponentsInChildren<Text>();
        manaInfo = ManaHole.GetComponentsInChildren<Text>();
        foreach (Transform t in root.transform)
        {
            allUI.Add(t.gameObject);
        }
        generalMessages = new List<GeneralMessage>();
        foreach(Transform t in generalMessageContainer.transform)
        {
            GeneralMessage gm = new GeneralMessage();
            gm.go = t.gameObject;
            gm.txt = t.GetComponentInChildren<Text>();
            generalMessages.Add(gm);
        }
    }
    private void Start()
    {
        DrawLife();
        DrawGold();
    }

    private void Update()
    {
        DrawMana();
        DrawGold();
        DrawWaveInfo();  // 나중에 레벨 매니저에서 실행하기.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameMenu();
        }

        foreach(GeneralMessage gm in generalMessages)
        {
            gm.UpdateGeneralMessage();
        }
    }

    public void DrawGold()
    {
        gold.text = GameManager.Instance.Gold.ToString();
    }
    public void DrawMana()
    {
        manaInfo[0].text = GameManager.Instance.Mana.ToString();
    }
    public void DrawLife()
    {
        lifeText.text = "LIFE : " + LevelManager.Instance.lifePoint.ToString();
    }
    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave : " + LevelManager.Instance.GetWaveInfo();

    }

    public void ToggleGameMenu()
    {
        gameMenuShowing = !gameMenuShowing;
        foreach (GameObject go in allUI)
        {
            go.SetActive(!gameMenuShowing);
        }
        recapInfo.SetActive(false);

        gameMenuObject.SetActive(gameMenuShowing);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void ReturnToHUB()
    {

    }

    public void ExitToDeskTop()
    {
        Application.Quit();
    }


    #region RecapinforMation
    [Header("RecapInfo")]
    public GameObject recapInfo;
    public GameObject[] vicOrDefeat;
    private Text[] recapInfoText;

    public void PopRecapInfo(bool victroy, string[] texts)
    {
        foreach (GameObject go in allUI)
        {
            go.SetActive(false);
        }
        recapInfo.SetActive(true);

        vicOrDefeat[0].SetActive(victroy);
        vicOrDefeat[1].SetActive(!victroy);

        recapInfoText[0].text = texts[0];
        recapInfoText[1].text = texts[1];
        recapInfoText[2].text = texts[2];

    }

    #endregion


    #region GeneralMessage

    [Header("GenralMessage")]
    public GameObject generalMessageContainer;
    [SerializeField]
    private List<GeneralMessage> generalMessages;
    private class GeneralMessage
    {
        public bool isActive = false;
        public GameObject go;
        public Text txt;
        public float duration;
        public float lastShow;

        public void UpdateGeneralMessage()
        {
            if(!isActive)
            {
                return;
            }
            if(Time.time-lastShow>duration)
            {
                isActive = false;
                go.SetActive(false);
            }
        }

    }
    public void ShowGeneralMessage(string msg,Color color,float duration)
    {
        GeneralMessage gm = generalMessages.Find(m => m.go == generalMessageContainer.transform.GetChild(2).gameObject);

        gm.txt.text = msg;
        gm.txt.color = color;
        gm.duration = duration;
        gm.lastShow = Time.time;
        gm.isActive = true;
        gm.go.SetActive(true);
        gm.go.transform.SetAsFirstSibling();
    }



    #endregion
}
