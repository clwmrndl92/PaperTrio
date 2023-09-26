using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string CurrentStageAndPage;
    [SerializeField] MapData Map;
    [SerializeField] PageManager _pageManager;

    // Stage Management
    [Header("Stages")]
    [SerializeField] int stageCount = 1;
    GameObject[] stageButtons;
    StageState[] stageStates;

    private void Awake() {
        // Init Stage States
        stageStates = new StageState[stageCount];
        Array.Fill(stageStates, StageState.NotClear);
    }
    void Start()
    {
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

    public void ClearStage(int stageNum){
        stageStates[stageNum] = StageState.Clear;
    }
    // Stage Management Methods
    public StageState GetStageState(int index){
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