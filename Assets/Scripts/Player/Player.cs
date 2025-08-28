using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public PlayerStat playerStat;
    public PlayerStateMachine state;
    private void Awake()
    {
        GameManager.Character.Player = this;
        state = GetComponent<PlayerStateMachine>();
        playerStat= GetComponent<PlayerStat>();
    }

}
