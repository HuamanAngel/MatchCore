using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // private MeshRenderer meshRenderer;
    public List<GameObject> gameObjectsToShine;
    private List<Color> _oldColors;
    private GameObject _goParent;
    private Animator _myAnim;
    private bool _isInteractuable = true;
    void Start()
    {
        _myAnim = gameObject.GetComponent<Animator>();
        _goParent = transform.parent.gameObject;
        _oldColors = new List<Color>();
        foreach (GameObject go in gameObjectsToShine)
        {
            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            _oldColors.Add(meshRenderer.materials[0].color);
        }

        // meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (_isInteractuable)
        {
            foreach (GameObject go in gameObjectsToShine)
            {
                MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                meshRenderer.materials[0].color = new Color32(0x50, 0x77, 0xFD, 0xFF);
            }

        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < gameObjectsToShine.Count; i++)
        {
            MeshRenderer meshRenderer = gameObjectsToShine[i].GetComponent<MeshRenderer>();
            meshRenderer.materials[0].color = _oldColors[i];
        }
    }

    private void OnMouseDown()
    {
        bool isConsumedKey = false;
        isConsumedKey = HeroInMovement.GetInstance().ConsumeKey(1, _goParent.GetComponent<BrigdeLogic>().typeObstacleOverHere);
        if (!isConsumedKey)
        {
            Debug.Log("Te faltan mas llaves");
        }
        else
        {
            _isInteractuable = false;            
            StartCoroutine(ProcessDie());
        }
    }

    public IEnumerator ProcessDie()
    {
        _myAnim.SetBool("Open", true);
        _goParent.GetComponent<BrigdeLogic>().SetNothingOverHere();
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        HeroInMovement.GetInstance().TryCreationArrowDirection();
        Destroy(this.gameObject);
    }
}
