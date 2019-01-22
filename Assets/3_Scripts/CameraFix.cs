using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFix : MonoBehaviour
{
    [Range(1f, 4f)]
    public float pixelScale = 1;
    private float oldPixelScale;
    public float OldPixelScale { get { return oldPixelScale; } set { oldPixelScale = value; } }
    private int pixelsPerUnit = 100;
    private float halfScreen = 0.5f;
    private Camera mainCamera;
    public Camera MainCamera{ get { return GetComponent<Camera>(); } }

    private void Start()
    {
        SetCamera();  
    }

    void Update()
    {
        if (OldPixelScale == pixelScale)
        {
            return;
        }
        else
        {
            OldPixelScale = pixelScale;
            SetCamera();
        }
    }

    public void SetCamera()
    {
        MainCamera.orthographic = true;
        MainCamera.orthographicSize = Screen.height * ((halfScreen / pixelsPerUnit) / pixelScale);
    }
}