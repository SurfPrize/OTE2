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
    public int edges = 8;
    public double minarea = 10;
    public double maxarea = 90;
    public GameObject SpawnedCollider;
    private List<GameObject> ColliderList = new List<GameObject>();
    private Color32[] Initial;

    public float colorfilter = 0.97f;
    public float satfilter = 0.91f;
    public float vibrancefilter = 0.2f;

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

        webcam = new WebCamTexture(640, 480, 24);
        img.texture = webcam;
        webcam.Play();
        Initial = webcam.GetPixels32();
        for (int i = 0; i < 20; i++)
        {
            ColliderList.Add(Instantiate(SpawnedCollider));
        }

    }

    public void ResetBack()
    {
        if (webcamvalid)
            Initial = webcam.GetPixels32();
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
        float v = 0;
        float vi = 0;
        float s, hi, si;

        Color32[] col = webcam.GetPixels32();
        for (int x = 0; x < framesize.x; x++)
        {
            for (int y = 0; y < framesize.y; y++)
            {
                Color.RGBToHSV(col[(int)framesize.x * y + x], out h, out s, out v);
                Color.RGBToHSV(Initial[(int)framesize.x * y + x], out hi, out si, out vi);
                if (Mathf.Abs(h - hi) < colorfilter && Mathf.Abs(s - si) < satfilter
                    && Mathf.Abs(v - vi) < vibrancefilter
                    )
                {
                    col[(int)framesize.x * y + x] = Color.black;
                }
            }
        }

        frame.SetPixels32(col);
        gray = OpenCvSharp.Unity.TextureToMat(frame);

        Cv2.Flip(gray, gray, FlipMode.XY);

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

        for (int i = 0; i < ColliderList.Count; i++)
        {
            ColliderList[i].SetActive(false);
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
        Cv2.CvtColor(gray, gray, ColorConversionCodes.BGR2GRAY);
        Point[][] contourss = Cv2.FindContoursAsArray(gray, RetrievalModes.List, ContourApproximationModes.ApproxNone);

        for (int i = 0; i < contourss.Length; i++)
        {
            // if (Cv2.ContourArea(contourss[i]) > minarea && Cv2.ContourArea(contourss[i]) < maxarea)
            {
                Cv2.FillConvexPoly(gray, contourss[i], Scalar.Red);
                var M = Cv2.Moments(contourss[i]);
                // Debug.Log((int)M.M01 / M.M00 + " " + (int)M.M10 / M.M00);
                if (ColliderList.Count > i)
                {
                    Vector2 res = new Vector2((float)(M.M10 / M.M00), (float)(M.M01 / M.M00));

                    res = new Vector2(map(res.x, 0, 640, -1000, 1000), map(res.y, 0, 480, -500, 500));
                    ColliderList[i].SetActive(true);
                    ColliderList[i].transform.position =  res +
                         (Vector2.one * Camera.main.transform.position)
                         //+ (Vector2.left * Camera.main.orthographicSize * 2)
                         ;
                }
            }
        }

        frame.Apply();
        frame2 = OpenCvSharp.Unity.MatToTexture(gray);
        frame2.Apply();
        result.texture = frame;
        result2.texture = frame2;
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    private void Update()
    {
        if (webcamvalid == true)
        {
            ProcessCameraImage();

        }
    }
}
