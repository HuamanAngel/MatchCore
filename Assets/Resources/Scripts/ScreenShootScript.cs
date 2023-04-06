using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class ScreenShootScript : MonoBehaviour
{
    // Para que funcione colocar en la camara Solid Color and alpha = 0
    private string filename;
    public List<GameObject> gameObjectsToCenter;
    private void Start()
    {
        filename = string.Format("Assets/Screenshots/capture_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));
        if (!Directory.Exists("Assets/Screenshots"))
        {
            Directory.CreateDirectory("Assets/Screenshots");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeTransparentScreenshot(Camera.main, Screen.width, Screen.height, filename);
            // Debug.Log("Screenshoot Sucessfull");
        }

    }
    public void TakeTransparentScreenshot(Camera cam, int width, int height, string savePath)
    {
        // Depending on your render pipeline, this may not work.
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
        // Must use 24-bit depth buffer to be able to fill background.
        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        // Simple: use a clear background
        cam.backgroundColor = Color.clear;
        cam.Render();
        tex_transparent.ReadPixels(grab_area, 0, 0);
        tex_transparent.Apply();

        // Encode the resulting output texture to a byte array then write to the file
        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(savePath, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);
        Texture2D.Destroy(tex_transparent);
    }

    public void EjecutandoAlgo()
    {
        foreach (GameObject go in gameObjectsToCenter)
        {
            float valueOldZ = go.transform.position.z;
            // go.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );            
            go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, valueOldZ);
        }

        Debug.Log("aca ejecutando");
    }
}


[CustomEditor(typeof(ScreenShootScript))]
public class ScreenShootScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScreenShootScript myScript = (ScreenShootScript)target;
        if (GUILayout.Button("Center all GameObject to Camera main"))
        {
            myScript.EjecutandoAlgo();
        }
    }
}
