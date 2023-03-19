using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBase : MonoBehaviour
{
    // Start is called before the first frame update
    protected int redSphere = 0;
    protected int blueSphere = 0;
    protected int yellowSphere = 0;
    protected int quantityMovementBoard = 1;
    protected int quantityMovementBoardCurrent;
    protected UserController _dataUserGameObject;
    public GameObject[] player1Tiers;
    public GameObject objectQuantitySpheresBlue;
    public GameObject objectQuantitySpheresYellow;
    public GameObject objectQuantitySpheresRed;
    public TMP_Text textQuantityMoves;
    protected Dictionary<GameObject, bool> _allCharactersAlive;
    public int QuantityMovementBoard { get => quantityMovementBoard; set => quantityMovementBoard = value; }
    public int QuantityMovementBoardCurrent { get => quantityMovementBoardCurrent; set => quantityMovementBoardCurrent = value; }
    public int YellowSphereG { get => yellowSphere; set => yellowSphere = value; }
    public int RedSphereG { get => redSphere; set => redSphere = value; }
    public int BlueSphereG { get => blueSphere; set => blueSphere = value; }
    public PlayerBase()
    {
        quantityMovementBoardCurrent = quantityMovementBoard;
        _allCharactersAlive = new Dictionary<GameObject, bool>();
    }
    public virtual void ResetSphere()
    {
        redSphere = 0;
        blueSphere = 0;
        yellowSphere = 0;
    }

    public virtual void RandomSphere(int min, int max)
    {
        RandomHelper random = new RandomHelper();
        redSphere = random.RandomInt(min, max + 1);
        blueSphere = random.RandomInt(min, max + 1);
        yellowSphere = random.RandomInt(min, max + 1);
    }

    public void SetQuantitySpheres()
    {
        Color32 redColor = new Color32(0xFF, 0x00, 0x00, 0xFF);
        Color32 defaultColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);


        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + YellowSphereG;
        textQuantityMoves.text = "x" + quantityMovementBoardCurrent;

        objectQuantitySpheresRed.GetComponent<TMP_Text>().color = RedSphereG > 0 ? defaultColor : redColor;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().color = BlueSphereG > 0 ? defaultColor : redColor;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().color = YellowSphereG > 0 ? defaultColor : redColor;
        textQuantityMoves.color = quantityMovementBoardCurrent > 0 ? defaultColor : redColor;

    }
    public void ResetQuantityMovementBoard()
    {
        quantityMovementBoardCurrent = quantityMovementBoard;
    }

    public bool EvaluateIfExistStillAliveCharacter()
    {
        foreach (var characterObject in _allCharactersAlive)
        {
            if(!characterObject.Value){
                return false;
            }
        }
        return true;
    }

    public void ChangeStateAliveCharacter(GameObject goCharacter, bool isAlive)
    {
        _allCharactersAlive[goCharacter] = isAlive;
    }
}
