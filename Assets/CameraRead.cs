using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRead : MonoBehaviour
{

    public RawImage img;
    private WebCamTexture webcam;
    public Dropdown devicesDropdown;
    public RawImage result;

    private WebCamDevice device;
    public bool webcamvalid = false;
    // Start is called before the first frame update
    void Start()
    {


        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            webcamvalid = false;
            Debug.LogWarning("No cameras detected");
        }
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

        webcam = new WebCamTexture(640, 480, 30);
        img.texture = webcam;
        webcam.Play();


    }

    void ProcessCameraImage()
    {
        Texture2D frame = new Texture2D(img.texture.width, img.texture.height);
        Vector2 framesize = new Vector2(img.texture.width, img.texture.height);

        for (int y = 0; y < framesize.y; y++)
        {
            for (int x = 0; x < framesize.x; x++)
            {
                frame.SetPixel(x, y, webcam.GetPixel(x, y));
            }
        }


        result.texture = frame;


    }

    private void Update()
    {
        if (webcamvalid)
        {
            ProcessCameraImage();
        }
    }
}
