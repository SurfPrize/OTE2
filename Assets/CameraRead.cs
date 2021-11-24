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
    public RawImage result2;
    private Texture2D frame;
    private Texture2D frame2;
    private WebCamDevice device;
    public bool webcamvalid = false;
    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();

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


        frame2 = new Texture2D(img.texture.width, img.texture.height);
    }
    void InitializeCamera()
    {


    }

    public void StartCamera(Dropdown cameraoptions)
    {
        string selectedCmera = cameraoptions.options[cameraoptions.value].text;
        Debug.Log(selectedCmera);
        webcamvalid = true;
        webcam.deviceName = selectedCmera;

        webcam = new WebCamTexture(640, 480, 30);
        img.texture = webcam;
        webcam.Play();


    }

    void ProcessCameraImage()
    {
        frame = new Texture2D(img.texture.width, img.texture.height);
        Vector2 framesize = new Vector2(img.texture.width, img.texture.height);

        frame.SetPixels(webcam.GetPixels());
        frame.Apply();

        Color[] col;
        col = webcam.GetPixels();
        for (int y = 0; y < framesize.y; y++)
        {
            for (int x = 0; x < framesize.x; x++)
            {
                col[(int)framesize.x * y + x] = new Color(col[(int)framesize.x * y + x].r,
                    col[(int)framesize.x * y + x].r,
                    col[(int)framesize.x * y + x].r);

            }
        }

        frame2.SetPixels(col);
        frame2.Apply();
        result.texture = frame;
        result2.texture = frame2;
    }

    private void Update()
    {
        if (webcamvalid == true && webcam != null)
        {
            ProcessCameraImage();
        }
    }
}
