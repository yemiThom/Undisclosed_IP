using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float k_WaitTime = 5f;
    public bool m_RunOnAwake, m_RunOnce, m_RunLoop, m_ShowTimer = false;
    public UnityEvent OnTimerEnd;

    void OnEnable(){
        // if the timer is to execute a function right away
        if(m_RunOnAwake)
            Invoke("Execute", 0);
        
        // if the timer is to execute a function on a loop
        if(m_RunLoop){
            InvokeRepeating("Execute", k_WaitTime, k_WaitTime);
        }
    }

    void Update(){
        // if the timer is to execute a function after a bool trigger
        if(m_RunOnce)
            Invoke("Execute", k_WaitTime);

    }

    // plug in methods to be run with Unity Events
    public void Execute(){
        OnTimerEnd.Invoke();

        // reset m_RunOnBool to false if set to true
        if(m_RunOnce){
            m_RunOnce = false;
            CancelInvoke("Execute");
        }
    }
}
