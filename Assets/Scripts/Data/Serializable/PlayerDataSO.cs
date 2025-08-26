using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    None,
    HP,
    MP,
    EXP,
    Speed,
    ATK,
    DEF,
    DEX,
}

[System.Serializable]
public class PlayerDataSO 
{
    [Header("Key (스탯이름)")]
    public string key;                

    [Header("종류")]
    public StatType statType;

    [Header("초기 값 입력")]
    public float value;

}
