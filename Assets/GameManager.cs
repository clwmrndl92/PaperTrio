using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurrentStageAndPage;
    [SerializeField] MapData Map;
    [SerializeField] PageManager PageManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LoadCurrentStageAndPage()
    {
        int stageIndex = 0;
        int pageIndex = 0;

        for (int i = 0; i <= pageIndex; i++)
        {
            foreach (var section in Map.Stages[stageIndex].Pages[pageIndex].Sections)
            {
                Vector3 position = Vector3.zero; //TODO: position 설정하기
                GameObject.Instantiate(section, position, new(0,0,0,0));
            }
        }
    }
}
