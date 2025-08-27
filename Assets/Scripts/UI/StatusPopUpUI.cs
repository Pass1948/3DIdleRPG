using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPopUpUI : PopUpUI
{
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
