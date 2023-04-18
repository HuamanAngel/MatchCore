using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomEffect : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    private Vector3 scaleInitial;
    private Vector3 scaleFinal;
    private Vector3 positionInitial;
    private Vector3 currentMousePosition;
    private Vector3 oldMousePosition;
    private void Awake()
    {
        scaleInitial = transform.localScale;
        scaleFinal = new Vector3(15, 15, 15);
        oldMousePosition = Vector3.zero;
        positionInitial = transform.localPosition;
    }

    private void Update()
    {
        if (scaleFinal == transform.localScale)
        {
            currentMousePosition = Input.mousePosition;
            if (oldMousePosition != currentMousePosition)
            {
                if (oldMousePosition.y >= currentMousePosition.y)
                {
                    transform.position += new Vector3(0, 2, 0);
                }
                else if (oldMousePosition.y <= currentMousePosition.y)
                {
                    transform.position += new Vector3(0, -2, 0);
                }
            }
            oldMousePosition = currentMousePosition;

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (scaleInitial == transform.localScale)
        {
            transform.localScale = scaleFinal;
        }
        else
        {
            transform.localScale = scaleInitial;
            transform.localPosition = positionInitial;

        }
    }

}
