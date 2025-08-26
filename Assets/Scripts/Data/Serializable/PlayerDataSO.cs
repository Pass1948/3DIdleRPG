using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    HP,
    MP,
    EXP,
    Speed,
    ATK,
    DEF,
    DEX
}
public enum DataType
{
    Int,
    Float,
    String,
    Bool,
}

[System.Serializable]
public class PlayerDataSO 
{
    [Header("Key (스탯이름)")]
    public string key;                

    [Header("종류 & 기본 볼륨")]
    public StatType? statType;

    [Header("값(아래 중 하나만 사용)")]
    public int intValue;
    public float floatValue;
    public string stringValue;
    public bool boolValue;

}
