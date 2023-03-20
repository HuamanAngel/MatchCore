using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMove : MonoBehaviour
{
    public enum OptionMovements { LEFT, RIGHT, UP, BOTTOM };
    private OptionMovements typeArrow;
    public OptionMovements TypeArrow { get => typeArrow; set => typeArrow = value; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotateByTypeArrow()
    {
        switch (typeArrow)
        {
            case OptionMovements.UP:
                transform.Rotate(0, 0, 0);
                break;
            case OptionMovements.BOTTOM:
                transform.Rotate(0, 0, -180);
                break;
            case OptionMovements.RIGHT:
                transform.Rotate(0, 0, 90);
                break;
            case OptionMovements.LEFT:
                transform.Rotate(0, 0, -90);
                break;
        }
    }

    private void OnMouseDown()
    {
        HeroInMovement.GetInstance().ChangeDirectionMovement(typeArrow);
    }
}
