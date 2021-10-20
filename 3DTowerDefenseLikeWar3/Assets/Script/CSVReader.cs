using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class CSVReader : MonoBehaviour
{
    public List<TextAsset> textAssetList;

    public TextAsset textAssetData;

    public WaveList waveList = new WaveList();

    private void Awake()
    {
        textAssetData = textAssetList[modeSelector.ModeNum];
    }
   
    public void InitReadCSVLevel2(List<WaveEvent> events)
    {

        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 6 - 1;  //row-1;


        for (int i = 0; i < tableSize; i++)
        {
          
            WaveInfo WI = new WaveInfo();
            WI.stage = int.Parse(data[6 * (i + 1)]);
            WI.EnemyIndex = int.Parse(data[6 * (i + 1) + 1]);
            WI.EnemyNum = int.Parse(data[6 * (i + 1) + 2]);
            WI.OffsetSpd = float.Parse(data[6 * (i + 1) + 3]);
            WI.OffsetDef = int.Parse(data[6 * (i + 1) + 4]);
            WI.OffsetHp = int.Parse(data[6 * (i + 1) + 5]);

            WaveEvent we = new WaveEvent();
            we.waveInfo=WI;

            events.Add(we);

        }

    }



}
