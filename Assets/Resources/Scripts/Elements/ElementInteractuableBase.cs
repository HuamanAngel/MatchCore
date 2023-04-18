using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteractuableBase : MonoBehaviour
{

    public List<GameObject> gameObjectsToShine;
    public GameObject prefabTextFloating;
    protected List<List<Color>> _oldColors;
    protected Animator _myAnim;
    protected bool _isInteractuable = true;
    protected Color32 _colorShine;
    protected GameObject _goParent;
    void Start()
    {
        _colorShine = new Color32(0x50, 0x77, 0xFD, 0xFF);
        _myAnim = gameObject.GetComponent<Animator>();
        _goParent = transform.parent.gameObject;
        _oldColors = new List<List<Color>>();
        foreach (GameObject go in gameObjectsToShine)
        {
            List<Color> allColorsFromMesh = new List<Color>();
            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (meshRenderer.materials[i].HasProperty("_Color"))
                {
                    allColorsFromMesh.Add(meshRenderer.materials[i].color);
                }
                else if (meshRenderer.materials[i].HasProperty("_ColorTint"))
                {
                    allColorsFromMesh.Add(meshRenderer.materials[i].GetColor("_ColorTint"));
                }
                else if (meshRenderer.materials[i].HasProperty("_DeepWaterColor"))
                {
                    allColorsFromMesh.Add(meshRenderer.materials[i].GetColor("_DeepWaterColor"));
                }

                // Debug.Log(MaterialEditor.GetMaterialProperties(meshRenderer.materials[i]));
            }
            _oldColors.Add(allColorsFromMesh);
        }

        // meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void OnMouseEnter()
    {
        if (_isInteractuable)
        {
            foreach (GameObject go in gameObjectsToShine)
            {
                MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    if (meshRenderer.materials[i].HasProperty("_Color"))
                    {
                        meshRenderer.materials[i].color = _colorShine;
                    }
                    else if (meshRenderer.materials[i].HasProperty("_ColorTint"))
                    {
                        meshRenderer.materials[i].SetColor("_ColorTint", _colorShine);
                    }
                    else if (meshRenderer.materials[i].HasProperty("_DeepWaterColor"))
                    {
                        meshRenderer.materials[i].SetColor("_DeepWaterColor", _colorShine);
                    }
                }
            }

        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < gameObjectsToShine.Count; i++)
        {
            List<Color> allColorsFromMesh = new List<Color>();
            MeshRenderer meshRenderer = gameObjectsToShine[i].GetComponent<MeshRenderer>();
            for (int j = 0; j < meshRenderer.materials.Length; j++)
            {
                allColorsFromMesh = _oldColors[i];
                if (meshRenderer.materials[j].HasProperty("_Color"))
                {
                    meshRenderer.materials[j].SetColor("_Color", allColorsFromMesh[j]);
                }
                else if (meshRenderer.materials[j].HasProperty("_ColorTint"))
                {
                    meshRenderer.materials[j].SetColor("_ColorTint", allColorsFromMesh[j]);
                }
                else if (meshRenderer.materials[j].HasProperty("_DeepWaterColor"))
                {
                    meshRenderer.materials[j].SetColor("_DeepWaterColor", allColorsFromMesh[j]);
                }
            }
        }
    }
}
