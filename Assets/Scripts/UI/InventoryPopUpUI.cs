using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopUpUI : PopUpUI
{
    [SerializeField] GameObject layout;
    string CloseButton = "CloseButton";
    

    private void Awake()
    {
        base.Awake();
        buttons[CloseButton].onClick.AddListener(OnClosePopUpUI);
    }

    void OnClosePopUpUI()
    {
        GameManager.UI.ClosePopUpUI();
    }





}
