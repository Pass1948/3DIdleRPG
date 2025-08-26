using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    private float[] cur = new float[(int)StatType.Count];
    private void Awake()
    {
        if (!data)
            data = GameManager.Resource.Load<PlayerData>("Data/PlayerData");
        ResetAllFromSO();
    }
    public void ZeroAll()
    {
        System.Array.Clear(cur, 0, cur.Length); // 전부 0으로
    }

    public void ResetAllFromSO()
    {
        ZeroAll();

        if (!data) // 데이터가 없으면 0으로 유지
        {
            Debug.LogWarning("[PlayerStat] PlayerData가 없습니다. 기본값(0) 유지", this);
            return;
        }
        for (int i = 1; i < (int)StatType.Count; i++)
        {
            var type = (StatType)i;
            if (data.TryGet(type, out var value))
                cur[i] = Mathf.Max(0f, value);  // 음수 방지
        }
    }

    public float Get(StatType type) => cur[(int)type];
    public void Set(StatType type, float value) => cur[(int)type] = Mathf.Max(0f, value);
    public void Add(StatType type, float value) => Set(type, Get(type) + value);

    public void ApplyDamage(float value)
    {
        float final = Mathf.Max(1f, value - Get(StatType.DEF));
        Set(StatType.HP, Get(StatType.HP) - final);
    }
}

