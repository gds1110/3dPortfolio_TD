using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    Idle,
    Attack,
    Dead

}
public class FSM : MonoBehaviour
{
 

    public State curruntState = State.Idle;
    TowerAnim anim;

    private void Start()
    {
        anim = GetComponent<TowerAnim>();
        
    }
    void ChangeState(State newState, int aniNum)
    {
        if(curruntState==newState)
        {
            return;
        }
        anim.ChangeAni(aniNum);
        curruntState = newState;
    }

    void UpdateState()
    {
        switch(curruntState)
        {
            case State.Idle:
                break;
            case State.Attack:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }


}
