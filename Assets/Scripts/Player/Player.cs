using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerStat playerStat;
    PlayerStateMachine state;
    private void Awake()
    {
        GameManager.Character.Player = this;
        state = GetComponent<PlayerStateMachine>();
        playerStat= GetComponent<PlayerStat>();
    }

}
