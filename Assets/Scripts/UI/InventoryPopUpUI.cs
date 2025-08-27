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
    // ----------------- DB에서 랜덤 아이템 추가(버튼 1개) -----------------
    void OnAddItemRandomFromDB()
    {
        if (itemDB == null || itemDB.item == null || itemDB.item.Count == 0)
        {
            Debug.LogWarning("[InventoryPopUpUI] ItemData가 비어 있습니다.", this);
            return;
        }

        int idx = Random.Range(0, itemDB.item.Count);
        var src = itemDB.item[idx];
        if (src == null)
        {
            Debug.LogWarning("[InventoryPopUpUI] 선택된 아이템이 null 입니다.", this);
            return;
        }

        // SO를 직접 수정하지 않도록 복제해서 사용
        var copy = new ItemSO
        {
            itemName = src.itemName,
            IconPath = src.IconPath,
            count = src.count
        };

        var slot = AddItem(copy);      // 슬롯 생성 + 데이터 바인딩
        if (slot != null) RegisterNewSlot(slot); // 클릭 콜백 연결
    }
    // --------------------------------------------------------------------

    // 레이아웃 자식 슬롯 캐싱 + onClick 바인딩
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

    // 슬롯 클릭 시 장비 버튼 표시
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
                    SetEquipButtonsActive(slot); // ★ 장착 후 즉시 토글 갱신
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
                    SetEquipButtonsActive(slot); // ★ 해제 후 즉시 토글 갱신
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

    // 슬롯 프리팹 생성 + 데이터 바인딩 + 리스트 등록
    public SlotUI AddItem(ItemSO data)
    {
        if (!layout)
        {
            Debug.LogWarning("[InventoryPopUpUI] layout 미할당", this);
            return null;
        }

        var prefab = GameManager.Resource.Load<SlotUI>(slotPrefabPath);
        if (!prefab)
        {
            Debug.LogError($"[InventoryPopUpUI] SlotUI 프리팹 로드 실패: \"{slotPrefabPath}\"", this);
            return null;
        }

        var slot = GameManager.Resource.Instantiate(prefab, layout.transform, pooling: false);
        slot.transform.SetAsLastSibling();
        slot.Set(data);

        slots.Add(slot);
        return slot;
    }

    // 동적으로 추가된 슬롯의 클릭 콜백 등록
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
