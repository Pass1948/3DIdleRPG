using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public List<PlayerDataSO> stats = new();
#if UNITY_EDITOR
    // �����Ϳ��� ���� ��ȿ�� üũ(�ߺ� Ű/�ߺ� StatType ���)
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
                    Debug.LogWarning($"[PlayerDataSO] key �ߺ�: \"{s.key}\"", this);
            }

            if (s.statType.HasValue)
            {
                if (!enumSet.Add(s.statType.Value))
                    Debug.LogWarning($"[PlayerDataSO] StatType �ߺ�: {s.statType.Value}", this);
            }
        }
    }
#endif
}
