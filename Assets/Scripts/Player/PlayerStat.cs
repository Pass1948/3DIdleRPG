using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    public int curHP { get; private set; }
    public int curMP { get; private set; }
    public int curEXP { get; private set; }
    public int curSpeed { get; private set; }
    public int curATK { get; private set; }
    public int curDFS { get; private set; }
    public int curDEX { get; private set; }

    private void Awake()
    {
        data = GameManager.Resource.Load<PlayerData>("Data/PlayerData");



        void Init()
        {
        }


    }
}
