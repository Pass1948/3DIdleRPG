using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPopUpUI : PopUpUI
{
    string CloseButton = "CloseButton";
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text atkText;
    [SerializeField] TMP_Text defText;
    [SerializeField] TMP_Text dexText;

    private void Awake()
    {
        base.Awake();
        buttons[CloseButton].onClick.AddListener(OnClosePopUpUI);
    }

    void OnEnable()
    {
        healthText.text = GameManager.Character.Player.playerStat.Get(StatType.HP).ToString();
        atkText.text = GameManager.Character.Player.playerStat.Get(StatType.ATK).ToString();
        defText.text = GameManager.Character.Player.playerStat.Get(StatType.DEF).ToString();
        dexText.text = GameManager.Character.Player.playerStat.Get(StatType.DEX).ToString();
    }

    void OnClosePopUpUI()
    {
        GameManager.UI.ClosePopUpUI();
    }   



}
