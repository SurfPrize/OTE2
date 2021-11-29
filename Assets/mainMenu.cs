using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;






public class mainMenu : MonoBehaviour
{
    private TMP_Text volumeTextValue = null;
    private Slider volumeSlider = null;
    private float defaultvolume = 1.0f;
    // Start is called before the first frame update
    public void  PlayGame()
    {
      

    }

    public void Exit()
    {

        Application.Quit();
    }

    //public void reset(string Menu)
    //{

    //    if (Menu == "Audio")
    //    {
    //        AudioListener.volume = defaultVolume;
    //    }
    //}
} 
