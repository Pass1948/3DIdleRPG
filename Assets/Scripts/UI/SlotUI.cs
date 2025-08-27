using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class SlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    public ItemSO Item { get; private set; }
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text equipText;
    public bool IsEquippable = true;
    public bool IsEquipped { get; private set; }
    public bool HasItem => Item != null;
    public void Set(ItemSO data)
    {
        Item = data;
        if (Item == null)
        {
            Clear();
            return;
        }

        var sprite = string.IsNullOrEmpty(Item.IconPath)? null : GameManager.Resource.Load<Sprite>(Item.IconPath);

      icon.sprite = sprite;
      countText.text = Item.count.ToString();
    }

    public void Clear()
    {
        Item = null;
        if (icon) { icon.sprite = null; icon.enabled = false; }
        if (countText) countText.text = "";
    }

    public void OnEquip()
    {
        if (!IsEquippable || !HasItem) return;
        IsEquipped = true;
        RefreshEquipMark();
    }

    public void OnUnEquip()
    {
        if (!IsEquippable || !HasItem) return;
        IsEquipped = false;
        RefreshEquipMark();
    }

    void RefreshEquipMark()
    {
        if (equipText) equipText.text = IsEquipped ? "E" : "";
    }

}
