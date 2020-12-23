using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;

    private Image buttonImg;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        buttonImg = GetComponent<Image>();
        //isPlaying = AudioManager.isMusicEnabled();
        isPlaying = true;
        toggleIcon(isPlaying);
    }

    private void toggleIcon(bool isOn)
    {
        if (isOn)
            buttonImg.sprite = musicOnIcon;
        else
            buttonImg.sprite = musicOffIcon;
    }

    public void toggleMusic()
    {
        isPlaying = !isPlaying;

        AudioManager.EnableMusic(isPlaying);
        toggleIcon(isPlaying);
    }
}
