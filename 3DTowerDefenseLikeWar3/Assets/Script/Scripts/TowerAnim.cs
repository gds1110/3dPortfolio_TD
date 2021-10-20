using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnim : MonoBehaviour
{
    public const int A_IDLE =1;
    public const int A_ATTACK =2;
    public const int A_DIE =3;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAni(int aniNum)
    {
        anim.SetInteger("TowerAnim", aniNum);
       // anim.SetFloat("AttackSpeed", 1f);  //���߿� �ִϸ��̼� �ӵ� �����ʿ��Ҷ� ������ �ٲ㼭 ����.
    }

}
