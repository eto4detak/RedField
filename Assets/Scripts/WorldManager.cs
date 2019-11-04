using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private HumanWarrior humanWarrior;
    

    void Start()
    {
        
        CreateHumanWarrior();
    }

    void Update()
    {
        
    }

    public void CreateHumanWarrior()
    {

        GameObject prefab = HumanWarrior.GetPrefab();
        if (prefab != null)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 position = transform.position;
                position.x += i * 1.0f;
                position.y = 5f;
                GameObject prefabWrapper =  Instantiate(prefab, position, Quaternion.identity);
                HumanWarrior warrion = prefabWrapper.GetComponent<HumanWarrior>();

               // warrion.NewPosition = position +  new Vector3(i * 1.0f, 0, i * 1.0f);
                warrion.NewPosition = new Vector3(10f, 5f, 10f);

            }

            
        }

    }
}
