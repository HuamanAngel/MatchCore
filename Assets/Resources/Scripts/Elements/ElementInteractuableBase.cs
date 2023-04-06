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
                allColorsFromMesh.Add(meshRenderer.materials[i].color);
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
                    meshRenderer.materials[i].color = _colorShine;
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
                meshRenderer.materials[j].color = allColorsFromMesh[j];
            }
        }
    }
}
