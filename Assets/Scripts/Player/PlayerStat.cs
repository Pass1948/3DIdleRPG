using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerData data;

    public float hp { get; private set; }
    public float mp { get; private set; }
    public float exp { get; private set; }
    public float speed { get; private set; }
    public float attack { get; private set; }
    public float defense { get; private set; }
    public float dex { get; private set; }

    private void Awake()
    {
        data = GameManager.Resource.Load<PlayerData>("Data/PlayerData");
        Init();
    }

    void Init()
    {
        // 기본 0으로 초기화
        hp =  mp = exp = speed =  attack = defense = dex = 0f;
        for (int i = 0; i < data.stats.Count; i++)
        {
            var e = data.stats[i];
            if (e == null) continue;

            switch (e.statType)
            {
                case StatType.HP: hp = e.value; break;
                case StatType.MP: mp = e.value; break;
                case StatType.EXP: exp = e.value; break;
                case StatType.Speed: speed = e.value; break;
                case StatType.ATK: attack = e.value; break;
                case StatType.DEF: defense = e.value; break;
                case StatType.DEX: dex = e.value; break;
                case StatType.None: break;
            }
        }
    }
    public float Get(StatType type)
    {
        return type switch
        {
            StatType.HP => hp,
            StatType.MP => mp,
            StatType.EXP => exp,
            StatType.Speed => speed,
            StatType.ATK => attack,
            StatType.DEF => defense,
            StatType.DEX => dex,
        };
    }
    public void Set(StatType type, float value)
    {
        value = Mathf.Max(0f, value);
        switch (type)
        {
            case StatType.HP: hp = value; break;
            case StatType.MP: mp = value; break;
            case StatType.EXP: exp = value; break;
            case StatType.Speed: speed = value; break;
            case StatType.ATK: attack = value; break;
            case StatType.DEF: defense = value; break;
            case StatType.DEX: dex = value; break;
        }
    }

    // 스텟 상호작용 메서드들
    public void ApplyDamage(float damage)
    {
        float final = Mathf.Max(1f, damage - dex);
        Set(StatType.HP, hp - final);
    }
}

