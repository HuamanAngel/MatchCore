using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;
    private int identity = -1;
    public int Identity { get => identity; set => identity = value; }
    private SpriteRenderer render;
    private bool isSelected = false;
    private LogicGame _logicGame;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int quantityMatch = 0;
    private Spheres.TypeOfSpheres myTypeSphere;
    public Spheres.TypeOfSpheres MyTypeSphere { get => myTypeSphere; set => myTypeSphere = value; }
    private Spheres.TypeOfSpheres prevTypeSphere;
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("LogicGame");
        _logicGame = go[0].GetComponent<LogicGame>();
    }
    private void Select()
    {
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
        SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect()
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected = null;
    }

    void OnMouseDown()
    {
        // Debug.Log("La identidad es : " + identity);
        // Debug.Log("la identidad anterior es : " + previousSelected.gameObject.GetComponent<Tile>().Identity);
        // Not Selectable conditions
        if (render.sprite == null || BoardManager.instance.IsShifting)
        {
            return;
        }
        if (!LogicGame.GetInstance().IsCurrentSelectedSkill)
        {
            if (isSelected)
            { // Is it already selected?
                Deselect();
            }
            else
            {
                if (previousSelected == null)
                {
                    // Is it the first tile selected?
                    Select();
                }
                else
                {
                    if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                    {
                        // Is it an adjacent tile?
                        SwapSprite(previousSelected.render);
                        prevTypeSphere = previousSelected.MyTypeSphere;
                        previousSelected.ClearAllMatches(MyTypeSphere);
                        previousSelected.Deselect();
                        ClearAllMatches(prevTypeSphere);
                        // prevTypeSphere = null;
                    }
                    else
                    {
                        previousSelected.GetComponent<Tile>().Deselect();
                        Select();
                    }
                }
            }
        }

    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (render.sprite == render2.sprite)
        {
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = render.sprite;
        render.sprite = tempSprite;
        myTypeSphere = InsertTypeSphereByNameSprite(render2.sprite.name);
        previousSelected.MyTypeSphere = InsertTypeSphereByNameSprite(render.sprite.name);
        SFXManager.instance.PlaySFX(Clip.Swap);
        GUIManager.instance.MoveCounter--; // Add this line here
    }
    public Spheres.TypeOfSpheres InsertTypeSphereByNameSprite(string nameSprite)
    {
        if (nameSprite == "ability_with_dimension")
        {
            return Spheres.TypeOfSpheres.SPHERE_YELLOW;
        }
        else if (nameSprite == "attack_with_dimension")
        {
            return Spheres.TypeOfSpheres.SPHERE_RED;
        }
        else if (nameSprite == "movement_with_dimension")
        {
            return Spheres.TypeOfSpheres.SPHERE_BLUE;
        }
        else
        {
            return Spheres.TypeOfSpheres.SPHERE_RED;
        }

    }
    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit hit;

        // Debug.DrawRay(transform.position, castDir, Color.red, 30, false);
        if (Physics.Raycast(transform.position, castDir, out hit))
        {
            if (hit.collider != null)
            {
                return hit.collider.gameObject;
            }

        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, castDir, out hit))
        {
            while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
            {
                matchingTiles.Add(hit.collider.gameObject);
                if (Physics.Raycast(hit.collider.transform.position, castDir, out hit))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        return matchingTiles;
    }

    private void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++) { matchingTiles.AddRange(FindMatch(paths[i])); }
        if (matchingTiles.Count >= 2)
        {
            // Debug.Log("Quantity of match : " + matchingTiles.Count );
            quantityMatch += matchingTiles.Count;
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true;
        }
    }

    private bool matchFound = false;
    public void ClearAllMatches(Spheres.TypeOfSpheres theSphere = Spheres.TypeOfSpheres.SPHERE_RED)
    {
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        // Debug.Log("name actual : " + MyTypeSphere);
        // Debug.Log("name previes : " + prevTypeSphere);
        if (quantityMatch != 0)
        {
            _logicGame.GiveSpheres(quantityMatch, theSphere);
        }
        // Debug.Log("Quantity total match : " + quantityMatch);
        quantityMatch = 0;
        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;
            // StopCoroutine(BoardManager.instance.FindNullTiles());
            // StartCoroutine(BoardManager.instance.FindNullTiles());
            SFXManager.instance.PlaySFX(Clip.Clear);
        }
    }
}