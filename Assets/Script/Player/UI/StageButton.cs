using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : Button
{
    public int stageNum;
    private void Start() {
        switch (GameManager.Instance.GetStageState(stageNum))
        {
            case StageState.NotClear:
                break;
            case StageState.Clear:
                GetComponent<Image>().color = Color.green;
                break;
            case StageState.StarClear:
                GetComponent<Image>().color = Color.yellow;
                break;
            default:
                break;
        }
    }
}
