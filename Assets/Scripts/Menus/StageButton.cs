using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    [SerializeField]
    private string StageScene;

    public void LoadStage()
    {
        if (StageScene != "")
            SceneManager.LoadScene(StageScene);
    }
}
