using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour
{
    // 레이어에 따른 기준 캔버스 좋음
    private Canvas windowCanvas;
    private Canvas popUpCanvas;
    private Stack<PopUpUI> popUpStack;
    private string canvasPath = "UI/Base/Canvas";
    private string eventSystemPath = "UI/Base/EventSystem";
    private void Awake()
    {
        InstantsWindowUI();
        InstantsPopUpUI();
        EnsureEventSystem();
    }

    // Scene 변경시 UI를 재생성 메소드
    public void Recreated()
    {
        Clear();
        InstantsWindowUI();
        InstantsPopUpUI();
    }
    public void Clear()
    {      
        // PopUp 정리
        popUpStack.Clear();

        // Canvas 정리
        GameManager.Resource.Destroy(windowCanvas);
        GameManager.Resource.Destroy(popUpCanvas);
    }
    
    // window, popUp, toast 기능 동일 - 재사용 가능하게구성 
    public void InstantsWindowUI()
    {
        if (windowCanvas == null)
        {
            // 메인 경로는 상수로
            windowCanvas = GameManager.Resource.Instantiate<Canvas>(canvasPath);
            windowCanvas.gameObject.name = "WindowCanvas";
            windowCanvas.sortingOrder = 10;
        }
    }
    public void InstantsPopUpUI()
    {
        if (popUpCanvas == null)
        {
            popUpCanvas = GameManager.Resource.Instantiate<Canvas>(canvasPath);
            popUpCanvas.gameObject.name = "PopUpCanvas";
            popUpCanvas.sortingOrder = 100;
            popUpStack = new Stack<PopUpUI>();
        }
    }

    // 체크 좋음
    public void EnsureEventSystem()
    {
        if (EventSystem.current != null)
            return;

        EventSystem eventSystem = GameManager.Resource.Load<EventSystem>(eventSystemPath);
        GameManager.Resource.Instantiate(eventSystem,transform);
        DontDestroyOnLoad(eventSystem.gameObject);
    }

    // --------------[WindowUI]--------------

    public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    {
        T ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);
        return ui;
    }

    // 사용하는데서 패스를 넘기면 활용이 힘듬
    // 특히 경로는 매니저에서 관리, key 값만 전달하자
    public T ShowWindowUI<T>(string path) where T : WindowUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowWindowUI(ui);
    }

    public void CloseWindowUI(WindowUI windowUI)
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }

    public void SelectWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowUI.transform.SetAsLastSibling();
    }

    // --------------[PopUpUI]--------------
    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
            popUpStack.Peek().gameObject.SetActive(false);

        T ui = GameManager.Pool.GetUI(popUpUI);
        ui.transform.SetParent(popUpCanvas.transform, false);
        
        // 스택형 팝업에 대한 이해 좋음
        popUpStack.Push(ui);
        return ui;
    }

    public T ShowPopUpUI<T>(string path) where T : PopUpUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpUI(ui);
    }

    public void ClosePopUpUI()
    {
        PopUpUI ui = popUpStack.Pop();
        GameManager.Pool.Release(ui.gameObject);

        if (popUpStack.Count > 0)
        {
            popUpStack.Peek().gameObject.SetActive(true);
        }
    }
    
    // 팝업 클리어가 필요한 경우가 종종 있음 - Good
    // 모두 팝업말고 특정 팝업이 나올때까지 Pop 하는 기능도 있으면 좋음
    public void PopUpUIClear()
    {
        // PopUpUI 스택을 비우고 모든 PopUpUI를 반환
        while (popUpStack.Count > 0)
            GameManager.Pool.ReleaseUI(popUpStack.Pop().gameObject);
    }
}

