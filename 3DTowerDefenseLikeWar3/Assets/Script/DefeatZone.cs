using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            LevelManager.Instance.EnemyCrossed();
            //Destroy(other.gameObject);
            UIManager.Instance.ShowGeneralMessage("LIFE COUNT -1", Color.red, 0.2f);
            //SpawnManager.Instance.DestroyEnemy(other.gameObject);
            other.GetComponent<EnemyCombat>().OnCross();
        }
    }

}
