using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles buttons in Stage Select scene.
/// 
/// @author Thanh Tung Nguyen
/// </summary>
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
