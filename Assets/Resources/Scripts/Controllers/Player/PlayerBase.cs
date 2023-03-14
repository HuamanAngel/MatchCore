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
    protected UserController _dataUserGameObject;
    public GameObject[] player1Tiers;
    public GameObject objectQuantitySpheresBlue;
    public GameObject objectQuantitySpheresYellow;
    public GameObject objectQuantitySpheresRed;
    public int YellowSphereG { get => yellowSphere; set => yellowSphere = value; }
    public int RedSphereG { get => redSphere; set => redSphere = value; }
    public int BlueSphereG { get => blueSphere; set => blueSphere = value; }
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
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + YellowSphereG;
    }

}
