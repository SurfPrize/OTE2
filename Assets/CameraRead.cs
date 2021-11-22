using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRead : MonoBehaviour
{

    public RawImage img;
    public WebCamTexture webcam;
    public Dropdown devicesDropdown;

    private WebCamDevice device;
    public bool webcamvalid = false;
    // Start is called before the first frame update
    void Start()
    {


        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
            webcamvalid = false;
        else
        {
            webcamvalid = true;
            for (int i = 0; i < devices.Length; i++)
            {
                Dropdown.OptionData thisDevice = new Dropdown.OptionData(devices[i].name);

                devicesDropdown.options.Add(thisDevice);

            }
        }
    }
    void InitializeCamera()
    {


    }

    // Update is called once per frame
    public void StartCamera(Dropdown cameraoptions)
    {
        string selectedCmera = cameraoptions.options[cameraoptions.value].text;

        webcamvalid = true;
        webcam.deviceName = selectedCmera;

        img.texture = webcam;
        webcam.Play();


    }
}
