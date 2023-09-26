using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{   

    // Scene Change Method
    public void SceneChange(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}