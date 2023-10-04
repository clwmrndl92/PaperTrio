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
                transform.GetChild(0).gameObject.SetActive(true);
                // GetComponent<Image>().color = Color.green;
                break;
            default:
                break;
        }
    }
}
