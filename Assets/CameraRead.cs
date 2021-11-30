using OpenCvSharp;
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
    public float mint = 70;
    public int edges = 10;
    public float clow = 15;
    public float chigh = 150;
    public double minarea = 4000;
    public double maxarea = 8000;
    public GameObject test;
    private List<GameObject> tests = new List<GameObject>();
    private Color[] Initial;

    public float colorfilter = 0.3f;

    private Mat gray;
    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogWarning("No cameras detected");
        }
        else
        {

            for (int i = 0; i < devices.Length; i++)
            {
                Dropdown.OptionData thisDevice = new Dropdown.OptionData(devices[i].name);

                devicesDropdown.options.Add(thisDevice);

            }
        }


        gray = new Mat();
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
        Initial = webcam.GetPixels();
        for (int i = 0; i < 50; i++)
        {
            tests.Add(Instantiate(test));
        }

    }

    public void ResetBack()
    {
        if (webcamvalid)
            Initial = webcam.GetPixels();
    }

    void ProcessCameraImage()
    {
        frame = new Texture2D(img.texture.width, img.texture.height);
        frame2 = new Texture2D(img.texture.width, img.texture.height);
        Vector2 framesize = new Vector2(img.texture.width, img.texture.height);




        //Color[] coll;
        //coll = webcam.GetPixels();
        //for (int y = 0; y < framesize.y; y++)
        //{
        //    for (int x = 0; x < framesize.x; x++)
        //    {
        //        coll[(int)framesize.x * y + x] = new Color(coll[(int)framesize.x * y + x].r, coll[(int)framesize.x * y + x].r, coll[(int)framesize.x * y + x].r);

        //    }
        //}

        float h = 0;
        float s, v, hi, si, vi;

        Color[] col = webcam.GetPixels();

        for (int x = 0; x < framesize.x; x++)
        {
            for (int y = 0; y < framesize.y; y++)
            {
                Color.RGBToHSV(col[(int)framesize.x * y + x], out h, out s, out v);
                Color.RGBToHSV(Initial[(int)framesize.x * y + x], out hi, out si, out vi);
                if (Mathf.Abs(h - hi) < colorfilter && Mathf.Abs(s - si) < colorfilter)
                {
                    col[(int)framesize.x * y + x] = Color.black;
                }

            }
        }
        Debug.Log(h);
        frame.SetPixels(col);

        gray = OpenCvSharp.Unity.TextureToMat(frame);
        Cv2.CvtColor(gray, gray, ColorConversionCodes.BGR2GRAY);
        Cv2.Flip(gray, gray, FlipMode.Y);
        //Mat canny = new Mat();

        //Cv2.Canny(gray, canny, clow, chigh);
        //Mat res = new Mat();
        //Cv2.Threshold(gray, res, mint, 255, ThresholdTypes.Triangle);
        //var kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(20, 20));
        //Mat morph = new Mat();
        //Cv2.MorphologyEx(res, morph, MorphTypes.Close, kernel);


        //Mat dil = new Mat();
        //Cv2.Dilate(canny, dil, new Mat(), null, edges);

        //Mat erode = new Mat();
        //Cv2.Erode(dil, erode, new Mat(), null, edges);

        //Cv2.Add(erode, gray, erode, gray, -2);

        //Cv2.Threshold(erode, erode, mint, 255, ThresholdTypes.Binary);
        //Point[][] contours = Cv2.FindContoursAsArray(erode, RetrievalModes.List, ContourApproximationModes.ApproxTC89KCOS);

        //Mat contours = new Mat();
        //contours= Cv2.FindContoursAsMat(dil, RetrievalModes.List, ContourApproximationModes.ApproxSimple)[1];

        for (int i = 0; i < tests.Count; i++)
        {
            tests[i].SetActive(false);
        }



        //for (int i = 0; i < contours.Length; i++)
        //{
        //    if (Cv2.ContourArea(contours[i]) > minarea && Cv2.ContourArea(contours[i]) < maxarea)
        //    {
        //        Cv2.FillConvexPoly(dil, contours[i], Scalar.Red);
        //        //var M = Cv2.Moments(contours[i]);
        //        //Debug.Log((int)M.M01 / M.M00 + " " + (int)M.M10 / M.M00);
        //        //if (tests.Count > i)
        //        //{
        //        //    tests[i].SetActive(true);
        //        //    tests[i].transform.position = new Vector2((float)(M.M10 / M.M00), (float)(M.M01 / M.M00));
        //        //}
        //    }
        //}


        Cv2.Erode(gray, gray, new Mat(), null, edges);
        Cv2.Dilate(gray, gray, new Mat(), null, edges);
        Cv2.Threshold(gray, gray, mint, 255, ThresholdTypes.Binary);

        Point[][] contourss = Cv2.FindContoursAsArray(gray, RetrievalModes.List, ContourApproximationModes.ApproxTC89KCOS);

        for (int i = 0; i < contourss.Length; i++)
        {
            if (Cv2.ContourArea(contourss[i]) > minarea && Cv2.ContourArea(contourss[i]) < maxarea)
            {
                Cv2.FillConvexPoly(gray, contourss[i], Scalar.Red);
                var M = Cv2.Moments(contourss[i]);
                Debug.Log((int)M.M01 / M.M00 + " " + (int)M.M10 / M.M00);
                if (tests.Count > i)
                {
                    tests[i].SetActive(true);
                    tests[i].transform.position = new Vector2((float)(M.M10 / M.M00), (float)(M.M01 / M.M00));
                }
            }
        }

        frame.Apply();
        frame2 = OpenCvSharp.Unity.MatToTexture(gray);
        frame2.Apply();
        result.texture = frame;
        result2.texture = frame2;
    }

    private void Update()
    {
        if (webcamvalid == true)
        {
            ProcessCameraImage();

        }
    }
}
