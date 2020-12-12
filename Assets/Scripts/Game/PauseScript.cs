using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

        // Change header based on if win or fail
        pauseScreen.transform.Find("Header Text").GetComponent<Text>().text = "Game Paused";
        // Show Buttons appropriately
        pauseScreen.transform.Find("Resume Btn").GetComponent<Button>().gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
