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
    [Header("Key (�����̸�)")]
    public string key;                

    [Header("���� & �⺻ ����")]
    public StatType? statType;

    [Header("��(�Ʒ� �� �ϳ��� ���)")]
    public int intValue;
    public float floatValue;
    public string stringValue;
    public bool boolValue;

}
