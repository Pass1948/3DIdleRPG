using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public List<PlayerDataSO> stats = new();
    public bool TryGet(StatType type, out float value)
    {
        value = 0f;
        if (type == StatType.None) return false;

        for (int i = 0; i < stats.Count; i++)
        {
            var data = stats[i];
            if (data != null && data.statType == type)
            {
                value = data.value; return true; 
            }
        }

        return false;
    }
}
