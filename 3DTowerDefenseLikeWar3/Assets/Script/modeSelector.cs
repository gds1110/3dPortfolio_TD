using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class modeSelector : MonoBehaviour
{

    [SerializeField]
    public static int ModeNum;
    [SerializeField]
    public static bool isRelay;

    public GameObject levelSel;
    public GameObject modeSel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

     

    public void LevelSelect(int num)
    {
        
        ModeNum = num;
        //levelSel.SetActive(false);
        //modeSel.SetActive(true);
    }
    public void ModeSelectInt(int mode)
    {
       if(mode>0)
        {

            isRelay = true;
            Debug.Log("Relay" + isRelay);
        }
       else
        {
            isRelay = false;
            Debug.Log("NORMAL" + isRelay);

        }
    }

    public void ModeSelect(bool mode)
    {
        isRelay = mode;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);

    }

}
