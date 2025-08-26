using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public List<PlayerDataSO> stats = new();
#if UNITY_EDITOR
    // 에디터에서 간단 유효성 체크(중복 키/중복 StatType 경고)
    private void OnValidate()
    {
        var keySet = new HashSet<string>();
        var enumSet = new HashSet<StatType>();

        foreach (var s in stats)
        {
            if (s == null) continue;

            if (!string.IsNullOrEmpty(s.key))
            {
                if (!keySet.Add(s.key))
                    Debug.LogWarning($"[PlayerDataSO] key 중복: \"{s.key}\"", this);
            }

            if (s.statType.HasValue)
            {
                if (!enumSet.Add(s.statType.Value))
                    Debug.LogWarning($"[PlayerDataSO] StatType 중복: {s.statType.Value}", this);
            }
        }
    }
#endif
}
