using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereItem : MonoBehaviour
{
    public int quantityBonus = 1;
    private LogicGame _logicGame;
    public Spheres.TypeOfSpheres typeSphere = Spheres.TypeOfSpheres.SPHERE_BLUE;
    void Start()
    {
        _logicGame = LogicGame.GetInstance();

    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HeroController>() != null)
        {
            Character h1 = other.gameObject.GetComponent<HeroController>();
            switch (typeSphere)
            {
                case Spheres.TypeOfSpheres.SPHERE_BLUE:
                    UserPlayerController.GetInstance().BlueSphereG += quantityBonus;
                    break;
                case Spheres.TypeOfSpheres.SPHERE_RED:
                    UserPlayerController.GetInstance().RedSphereG += quantityBonus;
                    break;
                case Spheres.TypeOfSpheres.SPHERE_YELLOW:
                    UserPlayerController.GetInstance().YellowSphereG += quantityBonus;
                    break;
            }
            h1.UpdateAllStats();
            Vector3 positioToFloating = other.gameObject.transform.position + new Vector3(0, 0, -2);
            _logicGame.StartCoroutineTextFloating(positioToFloating, "+" + quantityBonus, new Color32(23, 22, 222, 255), new Color32(23, 22, 222, 0));
            Destroy(this.gameObject);

        }
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            Character h1 = other.gameObject.GetComponent<EnemyController>();
            switch (typeSphere)
            {
                case Spheres.TypeOfSpheres.SPHERE_BLUE:
                    EnemyPlayerController.GetInstance().BlueSphereG += quantityBonus;
                    break;
                case Spheres.TypeOfSpheres.SPHERE_RED:
                    EnemyPlayerController.GetInstance().RedSphereG += quantityBonus;
                    break;
                case Spheres.TypeOfSpheres.SPHERE_YELLOW:
                    EnemyPlayerController.GetInstance().YellowSphereG += quantityBonus;
                    break;
            }
            h1.UpdateAllStats();
            Vector3 positioToFloating = other.gameObject.transform.position + new Vector3(0, 0, -2);
            _logicGame.StartCoroutineTextFloating(positioToFloating, "+" + quantityBonus, new Color32(23, 22, 222, 255), new Color32(23, 22, 222, 0));
            Destroy(this.gameObject);

        }
    }
}
