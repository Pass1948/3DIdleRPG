using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    StateMachineManager fsm = GameManager.StateMachine;
    Idle idle;
    Chase chase; 
    Attack attack;

    private void Awake()
    {
        fsm.ChangeState(idle);
    }

    // ====== »óÅÂµé ======
    class Idle : IState
    {
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
           
        }

        public void OnUpdate()
        {
            
        }

    }

    class Chase : IState
    {
        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public void OnUpdate()
        {

        }

    }

    class Attack : IState
    {
        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public void OnUpdate()
        {

        }

    }




}
