using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonSkill : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isPressed = false;
    private Skill theSkill;
    private GameObject _characterBelong;
    public Skill TheSkill { get => theSkill; set => theSkill = value; }
    public GameObject CharacterBelong { get => _characterBelong; set => _characterBelong = value; }

    // public List<Vector2> positions;
    // private Renderer Rend;
    // Vector3 PositionOfGameObject;

    private Color c1 = Color.yellow;
    private Color c2 = Color.red;
    private Color32 colorDisabled = new Color32(0x6F, 0x6F, 0x6F, 0xFF);
    private Color32 colorEnabled = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    private Color32 colorInsufficient = new Color32(0xFF, 0x23, 0x23, 0xFF);
    private int lengthOfLineRenderer = 2;
    private GameObject myLine;
    private LineRenderer lr;
    private int turnActivateSkill;
    private GameObject objIconSkill;
    private int turnTemp;
    private int quantityMatchTotal;
    private Dictionary<string, GameObject> _elementsInformation;
    public Dictionary<string, GameObject> ElementsInformation { get => _elementsInformation; set => _elementsInformation = value; }
    private int _prevRed;
    private int _prevBlue;
    private int _prevYellow;
    void Start()
    {
        objIconSkill = UtilitiesClass.FindChildByName(this.gameObject, "ImageSkill");
        turnActivateSkill = -1;
        turnTemp = LogicGame.GetInstance().Turn;
        EvaluateSkillAvaible();
        quantityMatchTotal = LogicGame.GetInstance().QuantityMatchTotal;
        _prevRed = _characterBelong.GetComponent<Character>().ThePlayer.RedSphereG;
        _prevBlue = _characterBelong.GetComponent<Character>().ThePlayer.BlueSphereG;
        _prevYellow = _characterBelong.GetComponent<Character>().ThePlayer.YellowSphereG;
        //     Rend = GetComponent<Renderer>();
        //     lineRenderer.numPositions = lengthOfLineRenderer;
        //     //Set your GameObject position from where you want draw a line
        //     PositionOfGameObject = new Vector3(0f, 0f, 0f);
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        // lr.SetColors(color, color);
        // lr.SetWidth(0.1f, 0.1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        // GameObject.Destroy(myLine, 3.0f);

        // Vector3 start = new Vector3(0, 0, 0);
        // Vector3 end = new Vector3(20, 20, 20);
        // GameObject myLine = new GameObject();
        // myLine.transform.position = start;
        // myLine.AddComponent<LineRenderer>();
        // LineRenderer lr = myLine.GetComponent<LineRenderer>();
        // // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        // // lr.SetColors(color, color);
        // lr.SetWidth(0.1f, 0.1f);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);
        // GameObject.Destroy(myLine, 3.0f);
        // Vector3 start = new Vector3(transform.position.x, transform.position.y, -1);
        // Vector3 end = new Vector3(20, 20, 20);
        // myLine.transform.position = start;
        // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        // lr.SetColors(color, color);
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);

    }

    // Update is called once per frame
    void Update()
    {

        // for(int i = 0; i < lengthOfLineRenderer; i++) {
        //     lr.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
        // }                            
        // lr.SetPosition(0, start);
        // lr.SetPosition(1, end);        
        if (_isPressed)
        {
            // Vector3 start = new Vector3(transform.position.x, transform.position.y, -1);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 end = new Vector3(cursorPosition.x, cursorPosition.y, -1);
            // var t = Time.time;
            // lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            // Vector3 start = new Vector3(transform.position.x, transform.position.y, -1);
            // Vector3 end = new Vector3(20, 20, 20);
            // myLine.transform.position = start;
            // // lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            // // lr.SetColors(color, color);
            // lr.SetWidth(0.1f, 0.1f);
            // lr.SetPosition(0, start);
            // lr.SetPosition(1, end);

            // var t = Time.time;
            // for(int i = 0; i < lengthOfLineRenderer; i++) {
            //     _lineRendered.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
            // }            
            // LR.SetPosition(0, PositionOfGameObject);
            // Debug.Log("Good");
            // Vector2 CursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // LR.SetPosition(1, CursorPosition);
            // Rend.material.mainTextureScale = new Vector2((int)Vector3.Distance(PositionOfGameObject, CursorPosition),1);
            // set your Texture Wrap Mode to Repeat in case you want some texture to repeat over the line.
            // Rend.material.mainTextureScale = new Vector2((int)Vector2.Distance(PositionOfGameObject, CursorPosition, 1));
        }

        if (turnTemp != LogicGame.GetInstance().Turn)
        {
            if (turnActivateSkill == -1 || (turnActivateSkill + theSkill.cooldown) <= LogicGame.GetInstance().Turn)
            {
                objIconSkill.GetComponent<RawImage>().color = colorEnabled;
            }
        }
        if (_characterBelong.GetComponent<Character>().ThePlayer.YellowSphereG != _prevYellow || _characterBelong.GetComponent<Character>().ThePlayer.RedSphereG != _prevRed || _characterBelong.GetComponent<Character>().ThePlayer.BlueSphereG != _prevBlue)
        {
            EvaluateSkillAvaible();
            // Debug.Log("Exist match");
        }
    }
    public void EvaluateSkillAvaible()
    {
        // _elementsInformation['yellow']
        if (_characterBelong.GetComponent<Character>().OnlyCheckIfCanAttack(theSkill))
        {
            _elementsInformation["yellow"].GetComponent<TMP_Text>().color = colorEnabled;
            _elementsInformation["red"].GetComponent<TMP_Text>().color = colorEnabled;
            _elementsInformation["blue"].GetComponent<TMP_Text>().color = colorEnabled;
            if (objIconSkill.GetComponent<RawImage>().color != colorDisabled)
            {
                objIconSkill.GetComponent<RawImage>().color = colorEnabled;
            }
        }
        else
        {
            if (objIconSkill.GetComponent<RawImage>().color != colorDisabled)
            {
                objIconSkill.GetComponent<RawImage>().color = colorInsufficient;
            }
            _elementsInformation["yellow"].GetComponent<TMP_Text>().color = _characterBelong.GetComponent<Character>().ThePlayer.YellowSphereG < theSkill.yellow ? colorInsufficient : colorEnabled;
            _elementsInformation["red"].GetComponent<TMP_Text>().color = _characterBelong.GetComponent<Character>().ThePlayer.RedSphereG < theSkill.red ? colorInsufficient : colorEnabled;
            _elementsInformation["blue"].GetComponent<TMP_Text>().color = _characterBelong.GetComponent<Character>().ThePlayer.BlueSphereG < theSkill.blue ? colorInsufficient : colorEnabled;
        }
    }
    public void UseSkill()
    {
        if (turnActivateSkill == -1 || (turnActivateSkill + theSkill.cooldown) <= LogicGame.GetInstance().Turn)
        {
            if (_characterBelong.GetComponent<Character>().OnlyCheckIfCanAttack(theSkill))
            {
                myLine = new GameObject();
                myLine.AddComponent<LineRenderer>();
                lr = myLine.GetComponent<LineRenderer>();
                lr.positionCount = lengthOfLineRenderer;
                lr.SetWidth(0.1f, 0.1f);
                Vector3 end = new Vector3(transform.position.x, transform.position.y, -1);
                // myLine.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                lr.SetPosition(0, end);

                LogicGame.GetInstance().IsCurrentSelectedSkill = true;
                LogicGame.GetInstance().SkillSelected = theSkill;
                LogicGame.GetInstance().ButtonSkillSelected = this.gameObject;
                _isPressed = true;
            }
            else
            {
                Debug.Log("Nees more resources for the attack");
            }
        }
        else
        {
            Debug.Log("Aun en coldown");
        }
    }

    public void DeselectedSkill(int mode = 0)
    {
        if (mode == 0)
        {
            LogicGame.GetInstance().IsCurrentSelectedSkill = false;
            _isPressed = false;
            Destroy(myLine);

        }
        else if (mode == 1)
        {

            LogicGame.GetInstance().IsCurrentSelectedSkill = false;
            _isPressed = false;
            turnActivateSkill = LogicGame.GetInstance().Turn;

            objIconSkill.GetComponent<RawImage>().color = colorDisabled;
            Destroy(myLine);
        }
    }
}
