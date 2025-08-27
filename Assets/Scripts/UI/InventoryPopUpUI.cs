using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryPopUpUI : PopUpUI
{
    [SerializeField] GameObject layout;
    string slotPrefabPath = "UI/Slot";
    string closeButton = "CloseButton";
    string equipButton = "EquipButton";
    string unEquipButton = "UnEquipButton";
    string addItemButton = "AddItemButton";
    private readonly List<SlotUI> slots = new();
    private ItemData itemDB;
    private SlotUI selectedSlot;
    private void Awake()
    {
        base.Awake();
        itemDB = GameManager.Resource.Load<ItemData>("Data/ItemData");
        buttons[closeButton].onClick.AddListener(OnClosePopUpUI);
        buttons[addItemButton].onClick.AddListener(OnAddItemRandomFromDB);
        SetEquipButtonsActive(false);
        CacheChildrenSlotsAndBind();
    }

    void OnClosePopUpUI()
    {
        GameManager.UI.ClosePopUpUI();
    }
    // ----------------- DB���� ���� ������ �߰�(��ư 1��) -----------------
    void OnAddItemRandomFromDB()
    {
        if (itemDB == null || itemDB.item == null || itemDB.item.Count == 0)
        {
            Debug.LogWarning("[InventoryPopUpUI] ItemData�� ��� �ֽ��ϴ�.", this);
            return;
        }

        int idx = Random.Range(0, itemDB.item.Count);
        var src = itemDB.item[idx];
        if (src == null)
        {
            Debug.LogWarning("[InventoryPopUpUI] ���õ� �������� null �Դϴ�.", this);
            return;
        }

        // SO�� ���� �������� �ʵ��� �����ؼ� ���
        var copy = new ItemSO
        {
            itemName = src.itemName,
            IconPath = src.IconPath,
            count = src.count
        };

        var slot = AddItem(copy);      // ���� ���� + ������ ���ε�
        if (slot != null) RegisterNewSlot(slot); // Ŭ�� �ݹ� ����
    }
    // --------------------------------------------------------------------

    // ���̾ƿ� �ڽ� ���� ĳ�� + onClick ���ε�
    void CacheChildrenSlotsAndBind()
    {
        slots.Clear();
        if (!layout) return;

        var p = layout.transform;
        for (int i = 0; i < p.childCount; i++)
        {
            var child = p.GetChild(i);
            var slot = child.GetComponent<SlotUI>();
            if (!slot) continue;

            slots.Add(slot);

            var btn = child.GetComponent<Button>();
            if (btn != null)
            {
                SlotUI captured = slot;
                btn.onClick.AddListener(() => OnSlotClicked(captured));
            }
        }
    }

    // ���� Ŭ�� �� ��� ��ư ǥ��
    void OnSlotClicked(SlotUI slot)
    {
        selectedSlot = slot;
        SetEquipButtonsActive(slot);
    }

    void SetEquipButtonsActive(SlotUI slot)
    {
        bool ok = (slot != null && slot.HasItem && slot.IsEquippable);

        // Equip
        if (buttons.TryGetValue(equipButton, out var equipBtn))
        {
            equipBtn.onClick.RemoveAllListeners();
            equipBtn.gameObject.SetActive(ok && !slot.IsEquipped);
            if (ok && !slot.IsEquipped)
            {
                equipBtn.onClick.AddListener(() =>
                {
                    slot.OnEquip();
                    SetEquipButtonsActive(slot); // �� ���� �� ��� ��� ����
                });
            }
        }

        // UnEquip
        if (buttons.TryGetValue(unEquipButton, out var unEquipBtn))
        {
            unEquipBtn.onClick.RemoveAllListeners();
            unEquipBtn.gameObject.SetActive(ok && slot.IsEquipped);
            if (ok && slot.IsEquipped)
            {
                unEquipBtn.onClick.AddListener(() =>
                {
                    slot.OnUnEquip();
                    SetEquipButtonsActive(slot); // �� ���� �� ��� ��� ����
                });
            }
        }
    }
    void SetEquipButtonsActive(bool active)
    {
        if (buttons.TryGetValue(equipButton, out var equip))
            equip.gameObject.SetActive(active);
        if (buttons.TryGetValue(unEquipButton, out var unequip))
            unequip.gameObject.SetActive(active);
    }

    // ���� ������ ���� + ������ ���ε� + ����Ʈ ���
    public SlotUI AddItem(ItemSO data)
    {
        if (!layout)
        {
            Debug.LogWarning("[InventoryPopUpUI] layout ���Ҵ�", this);
            return null;
        }

        var prefab = GameManager.Resource.Load<SlotUI>(slotPrefabPath);
        if (!prefab)
        {
            Debug.LogError($"[InventoryPopUpUI] SlotUI ������ �ε� ����: \"{slotPrefabPath}\"", this);
            return null;
        }

        var slot = GameManager.Resource.Instantiate(prefab, layout.transform, pooling: false);
        slot.transform.SetAsLastSibling();
        slot.Set(data);

        slots.Add(slot);
        return slot;
    }

    // �������� �߰��� ������ Ŭ�� �ݹ� ���
    public void RegisterNewSlot(SlotUI slot)
    {
        if (!slot) return;
        var btn = slot.GetComponent<Button>();
        if (btn != null)
        {
            SlotUI captured = slot;
            btn.onClick.AddListener(() => OnSlotClicked(captured));
        }
    }
}
