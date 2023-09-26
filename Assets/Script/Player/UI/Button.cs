using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void GoToTitle(){
        GameManager.Instance.SceneChange("TitleScene");
    }
    public void GoToStage(int stageNum){
        GameManager.Instance.SceneChange("Stage" + stageNum.ToString());
    }
}
