using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        meshRenderer.materials[0].color = new Color32(0x50, 0x77, 0xFD, 0xFF);
    }

    private void OnMouseExit()
    {
        meshRenderer.materials[0].color = Color.white;
    }
}
