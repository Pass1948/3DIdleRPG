using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindowUI : WindowUI
{
    string statusButton = "StatusButton";
    string path1 = "UI/StatusPopUpUI";
    string inventoryButton = "InventoryButton";
    string path2 = "UI/InventoryPopUpUI";

    private void Awake()
    {
        base.Awake();
        buttons[statusButton].onClick.AddListener(OnStatusButton);
        buttons[inventoryButton].onClick.AddListener(OnInventoryButton);
    }

   public void OnStatusButton()
    {
        GameManager.UI.ShowPopUpUI<StatusPopUpUI>(path1);
    }
    public void OnInventoryButton()
    {
        GameManager.UI.ShowPopUpUI<InventoryPopUpUI>(path2);
    }
}
