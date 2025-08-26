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
    [Header("Key (�����̸�)")]
    public string key;                

    [Header("����")]
    public StatType statType;

    [Header("�ʱ� �� �Է�")]
    public float value;

}
