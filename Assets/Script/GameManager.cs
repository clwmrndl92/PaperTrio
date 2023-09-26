using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string CurrentStageAndPage;
    [SerializeField] MapData Map;
    [SerializeField] PageManager _pageManager;

    // Stage Management
    [Header("Stages")]
    [SerializeField] int stageNum = 1;
    StageState[] stageStates;

    void Start()
    {
        // Init Stage States
        stageStates = new StageState[stageNum];
        Array.Fill(stageStates, StageState.NotClear);
    }

    void Update()
    {
        
    }

    public void LoadCurrentStageAndPage()
    {
        int stageIndex = 0;
        int pageIndex = 1;

        _pageManager.ResetPages();
        for (int i = 0; i <= pageIndex; i++)
        {
            var PageSections = Map.Stages[stageIndex].Pages[i].Sections;
            for (int j = 0; j < PageSections.Count; j++)
            {
                if (j == 0)
                    _pageManager.leftPages.Add(PageSections[j]);
                else if (j == 1)
                    _pageManager.middlePages.Add(PageSections[j]);
                else if (j == 2)
                    _pageManager.rightPages.Add(PageSections[j]);
                if (i != pageIndex)
                {
                    PageSections[j].SetActive(false);
                }
                else
                {
                    PageSections[j].SetActive(true);
                }
            }
        }
    }

    // Stage Management Methods
    public StageState GetStageStae(int index){
        return stageStates[index];
    }

    // Scene Change Method
    public void SceneChange(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}

public enum StageState
{
    NotClear,
    Clear,
    StarClear,
    None
}