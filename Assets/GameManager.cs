using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurrentStageAndPage;
    [SerializeField] MapData Map;
    [SerializeField] PageManager _pageManager;
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
}
