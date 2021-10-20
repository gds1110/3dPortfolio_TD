using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public int lifePoint = 10;
    public int currentWave;
    private int ammWave;
    public bool isPlay = false;
    public bool isRelayMode=false;

    public bool isWin = false;
    public bool isEnd = false;
    public int EnemyNums = 0;
    public WaveInfo waveInfo;
    public override void Init()
    {
        base.Init();

        currentWave = 0;
    }
    public void Start()
    {
        isRelayMode = modeSelector.isRelay;
        UIManager.Instance.DrawWaveInfo();
    }

    public void EnemyCrossed()
    {
        lifePoint--;
        UIManager.Instance.DrawLife();
        if(lifePoint<=0)
        {
            Defeat();
        }
    }
    public void Update()
    {
        if(currentWave==ammWave&&isWin==false)
        {
            
            if(lifePoint>0 && SpawnManager.Instance.GetLeftEnemys() <= 0&&isEnd==true)
            {
                Victory();
                isWin = true;
            }
        }
    }

    public string GetWaveInfo()
    {
        return currentWave + " / " + ammWave;
    }


    public void InitLevelManager(int _ammWave)
    {
        ammWave = _ammWave;
    }

    public void Victory()
    {
        string[] texts = new string[3];
        texts[0] = "LIFE COUNT : " + lifePoint ;
        texts[1] = "Tower built : " + BuildManager.Instance.TowerList.Count;
        texts[2] = "Good Job!";

        UIManager.Instance.PopRecapInfo(true,texts);
    }

    private void Defeat()
    {
        Debug.Log("Defeat");

        string[] texts = new string[3];
        texts[0] = "You Die";
        texts[1] = "Tower built : "+BuildManager.Instance.TowerList.Count;
        texts[2] = "Try Again";

        UIManager.Instance.PopRecapInfo(false, texts);

    }

}