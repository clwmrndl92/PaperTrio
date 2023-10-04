using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    // Stage Management
    [Header("Stages")]
    [SerializeField] int stageNum = 1;
    StageState[] stageStates;
    [SerializeField] private GameObject player;

    [SerializeField] public int CurrentStage = 0;
    [SerializeField] public int CurrentPage = 0;
    [SerializeField] public MapData Map;
    [SerializeField] public PageManager _pageManager;
    private void Awake()
    {
        // Init Stage States
        stageStates = new StageState[stageNum];
        Array.Fill(stageStates, StageState.NotClear);
    }
    public GameObject GetPlayer() => player;

    // Stage Management Methods
    public StageState GetStageState(int index)
    {
        return stageStates[index];
    }

    public void LoadPlayerSpawnPosition()
    {
        Debug.Log(CurrentStage+ " " + CurrentPage);
        player.transform.position = Map.Stages[CurrentStage].Pages[CurrentPage].RespawnPosition;
    }

    public void NextPage()
    {
        Debug.Log("NextPage");
        CurrentPage++;
        if (CurrentPage >= 3)
        {
            CurrentPage = CurrentPage % 3;
            ClearStage(CurrentStage);
            return;
        }
        _pageManager.SavePages();
        _pageManager.LoadPage();
    }

    public void NextStage()
    {
        CurrentStage++;
    }

    public void ClearStage(int stageNum)
    {
        _pageManager.ClearStage();
        stageStates[stageNum] = StageState.Clear;
        Invoke(nameof(SceneChangeTitle), 2f);
    }

    // Scene Change Method
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SceneChangeTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void initScene()
    {
        player = GameObject.FindWithTag("Player");
        _pageManager = GameObject.Find("PageManager").GetComponent<PageManager>();
    }
}
public enum StageState
{
    NotClear,
    Clear,
    None
}
