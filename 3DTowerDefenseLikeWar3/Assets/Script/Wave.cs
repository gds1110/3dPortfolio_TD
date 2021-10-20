using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class WaveLevel
{
    public int stage;
    public int EnemyIndex;
    public int EnemyNum;
    public float OffsetSpd;
    public int OffsetDef;
    public int OffsetHp;
}

[System.Serializable]
public class WaveList
{
    public WaveLevel[] enemyWave;
    public List<WaveLevel> WL;

}


public class Wave : MonoBehaviour
{
    public List<WaveEvent> events = new List<WaveEvent>();
    public int cTime=0;
    public Text countDown;
    public GameObject StartImage;
    private bool isPlaying = false;
    private bool first = false;

    public void InitEvent()
    {
        GetComponent<CSVReader>().InitReadCSVLevel2(events);
        LevelManager.Instance.InitLevelManager(events.Count);
    }

    public void StartWave()
    {

        if (events.Count!=0)
        {
            if (first != true)
            {

                isPlaying = true;
                LevelManager.Instance.isPlay = true;
                events[0].StartEvent();
               

            }
            if (LevelManager.Instance.isRelayMode == true)
            {
                countDown.gameObject.SetActive(true);
                StartImage.SetActive(false);
                first = true;
            }
        }
        else
        {
            Debug.Log("end wave");
            Destroy(this);
        }
    }
    private void Start()
    {
        InitEvent();
      
    }


    private void Update()
    {
      

        if (!isPlaying)
        {
            return;
        }
        if(!events[0].RunEvent())
        {
            events.RemoveAt(0);
            if(events.Count==0)
            {
                Debug.Log("end wave");
                LevelManager.Instance.isEnd = true;
               
               Destroy(this);
            }
            else
            {
                isPlaying = false;
                LevelManager.Instance.isPlay = false;

                if (LevelManager.Instance.isRelayMode == true)
                {
                    UIManager.Instance.ShowGeneralMessage(events[0].duration + "초 후 다음 웨이브 시작", Color.green, 2f);
                    StartCoroutine("CountDown", events[0].duration);
                    StartCoroutine("delayTime");
                }
                else
                {
                    UIManager.Instance.ShowGeneralMessage("웨이브종료!", Color.green, 3f);
                    UIManager.Instance.ShowGeneralMessage("다음 웨이브 시작버튼을 누르세요!!", Color.green, 3f);
                }
            }
        }

    }
    IEnumerator CountDown(int second)
    {
        int count = second;
        while(count>0)
        {
            yield return new WaitForSeconds(1);
            count--;
            countDown.text = count.ToString();

        }
        
    }

    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(events[0].duration);
        isPlaying = true;
        LevelManager.Instance.isPlay = true;

        events[0].StartEvent();

    }


}
[System.Serializable]
public class WaveEvent
{
    public float duration = 10.0f;
    public WaveInfo waveInfo;
    private float startTime;

    public void StartEvent()
    {
        UIManager.Instance.ShowGeneralMessage(waveInfo.stage.ToString() + " 번 째 웨이브 시작", Color.green, 2f);
        LevelManager.Instance.waveInfo = waveInfo;
        LevelManager.Instance.EnemyNums += waveInfo.EnemyNum;
        LevelManager.Instance.currentWave = waveInfo.stage;
        startTime = Time.time;
    }
    public bool RunEvent()
    {
        if (waveInfo.EnemyNum==0)
        {
            return false;
        }
        else if (Time.time - startTime > duration && duration != 0.0f)
        {
            return false;
        }

        waveInfo.ReadyToSpawn();
        
      

        return true;
    }
}
[System.Serializable]

public class WaveInfo
{
    public int stage;
    public int EnemyIndex;
    public int EnemyNum;
    public float OffsetSpd;
    public int OffsetDef;
    public int OffsetHp;
    public float interval = 1.0f;

    private float lastTime;
    public void ReadyToSpawn()
    {
        if (Time.time - lastTime >= interval)
        {
            //SpawnManager.Instance.PoolSpawnOffset(EnemyIndex, this);
            SpawnManager.Instance.PoolSpawnOffset2(EnemyIndex, this);
           
            EnemyNum--;
            lastTime = Time.time;
        }
    }
}
