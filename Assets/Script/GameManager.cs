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
        //LoadCurrentStageAndPage();
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
    //public void LoadCurrentStageAndPage()
    //{
    //    int stageIndex = CurrentStage;
    //    int pageIndex = CurrentPage;

    //    LoadPlayerSpawnPosition();
    //    _pageManager.ResetPages();
    //    _pageManager.UpdatePagesIndex(pageIndex);
    //    for (int i = 0; i <= pageIndex; i++)
    //    {
    //        var PageSections = Map.Stages[stageIndex].Pages[i].Sections;
    //        for (int j = 0; j < PageSections.Count; j++)
    //        {
    //            if (j == 0)
    //                _pageManager.leftPages.Add(PageSections[j]);
    //            else if (j == 1)
    //                _pageManager.middlePages.Add(PageSections[j]);
    //            else if (j == 2)
    //                _pageManager.rightPages.Add(PageSections[j]);
    //            if (i != pageIndex)
    //            {
    //                PageSections[j].SetActive(false);
    //            }
    //            else
    //            {
    //                PageSections[j].SetActive(true);
    //            }
    //        }
    //    }
    //}

    public void LoadPlayerSpawnPosition()
    {
        player.transform.position = Map.Stages[CurrentStage].Pages[CurrentPage].RespawnPosition;
    }

    public void NextPage()
    {
        CurrentPage++;
        if (CurrentPage >= 3)
        {
            CurrentPage = CurrentPage % 3;
            NextStage();
        }
        _pageManager.SavePages();
        _pageManager.LoadPage();
        //LoadCurrentStageAndPage();
    }

    public void NextStage()
    {
        CurrentStage++;
    }

    public void ClearStage(int stageNum)
    {
        stageStates[stageNum] = StageState.Clear;
        Invoke(nameof(SceneChangeTitle), 3f);
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
        //LoadCurrentStageAndPage();
    }
}
public enum StageState
{
    NotClear,
    Clear,
    StarClear,
    None
}
