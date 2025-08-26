using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]       // GameManager가 다른 Script보다 먼저 호출되게 설정
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    // Managers=========================
    private static ResourceManager resourceManager;
    public static ResourceManager Resource => resourceManager;

    private static PoolManager poolManager;
    public static PoolManager Pool => poolManager;

    private static SceneManager sceneManager;
    public static SceneManager Scene => sceneManager;

    private static UIManager uiManager;
    public static UIManager UI => uiManager;

    private static EventManager eventManager;
    public static EventManager Event => eventManager;

    private static SoundManager soundManager;
    public static SoundManager Sound=> soundManager;

    private static CharacterManager characterManager;
    public static CharacterManager Character => characterManager;

    private static StateMachineManager stateMachineManager;
    public static StateMachineManager StateMachine=> stateMachineManager;
    private void Awake()
    {
        if (instance != null) { Destroy(this); return; }
        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        resourceManager = CreateChildManager<ResourceManager>("ResourceManager");
        poolManager = CreateChildManager<PoolManager>("PoolManager");
        sceneManager = CreateChildManager<SceneManager>("SceneManager");
        uiManager = CreateChildManager<UIManager>("UIManager");
        eventManager = CreateChildManager<EventManager>("EventManager");
        soundManager = CreateChildManager<SoundManager>("SoundManager");
        characterManager = CreateChildManager<CharacterManager>("CharacterManager");
        stateMachineManager = CreateChildManager<StateMachineManager>("StateMachineManager");
    }
    private T CreateChildManager<T>(string goName) where T : Component
    {
        var go = new GameObject(goName);
        go.transform.SetParent(transform, false);
        return go.AddComponent<T>();
    }

}
