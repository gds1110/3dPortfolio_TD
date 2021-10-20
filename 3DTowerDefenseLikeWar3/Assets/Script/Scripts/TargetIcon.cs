using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour
{

    private void Update()
    {
        if(GameManager.Instance.SelObj!=null)
        {
            transform.position = new Vector3(GameManager.Instance.SelObj.transform.position.x, 0.55f, GameManager.Instance.SelObj.transform.position.z);
        }
        if(GameManager.Instance.SelObj == null)
        {
            gameObject.SetActive(false);
        }
    }

}
