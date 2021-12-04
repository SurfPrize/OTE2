using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;






public class mainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultvolume = 1.0f;

    [SerializeField] private GameObject ConfirmationPrompt = null;

    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private bool _isFullScreen;

    // Start is called before the first frame update
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentresolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {

                currentresolutionIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentresolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }
    public void SetResolution(int resolutionIndex)

    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void PlayGame()
    {


    }

    public void Exit()
    {

        Application.Quit();
    }

    public void Setvolume(float volume)
    {


        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void volumeApply()
    {

        PlayerPrefs.SetFloat("Mastervolume ", AudioListener.volume);
        StartCoroutine(ConfirmationBox());

    }
    public void setFullscreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;

    }
    public void resetButton(string Menutype)
    {

        if (Menutype == "Audio")
        {
            AudioListener.volume = defaultvolume;
            volumeSlider.value = defaultvolume;
            volumeTextValue.text = defaultvolume.ToString("0.0");
            volumeApply();
        }
    }
    public IEnumerator ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ConfirmationPrompt.SetActive(false);
    }

}
