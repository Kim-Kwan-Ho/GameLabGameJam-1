using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class RazerMaker : MonoBehaviour
{
    public Transform center;
    public GameObject[] razers; //0 for z, 1 for y
    private bool cooltime = false;
    public static bool isSpecial = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cooltime && !isSpecial)
        {
            StartCoroutine(RandRazer());
            cooltime = true;
        }
    }

    IEnumerator RandRazer()
    {

        int randPattern = Random.Range(0, 4);
        float randY = Random.Range(1, 20);
        float randX = Random.Range(-9, 10);
        float randZ = Random.Range(-9, 10);
        if (randPattern == 0)
        {
            int dirRandom = Random.Range(0, 4); //0=PY, 1=NY, 2=NZ, 3=PZ

            Vector3 position;
            switch (dirRandom)
            {
            
                case 0://Z-razer and PY
                    position = new Vector3(0, -10, randZ);

                    GameObject razer = Instantiate(razers[0], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 1:
                    position = new Vector3(0, 30, randZ);
                    GameObject razer1 = Instantiate(razers[0], position, Quaternion.identity);
                    
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
                case 2:
                    position = new Vector3(0, randY, 20);
                    GameObject razer2 = Instantiate(razers[0], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
                case 3:
                    position = new Vector3(0, randY, -20);
                    GameObject razer3 = Instantiate(razers[0], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
            }
        }
        else if(randPattern == 1)
        {
            int dirRandom = Random.Range(0, 4); //0=PX, 1=NX, 2=PY, 3=NY

            Vector3 position;
            switch(dirRandom)
            {
                case 0:
                    position = new Vector3(-20, randY, 0);
                    GameObject razer = Instantiate(razers[1], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 1:
                    position = new Vector3(20, randY, 0);
                    GameObject razer1 = Instantiate(razers[1], position, Quaternion.identity);
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 2:
                    position = new Vector3(randX, -10, 0);
                    GameObject razer2 = Instantiate(razers[1], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 3:
                    position = new Vector3(randX, 30, 0);
                    GameObject razer3 = Instantiate(razers[1], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
            }    
        }
        else if (randPattern == 2)
        {
            int dirRandom = Random.Range(0, 4); //0=PX, 1=NX, 2=PZ, 3=NZ
            Vector3 position;
            switch (dirRandom)
            {
                case 0:
                    position = new Vector3(-20, 10, randZ);
                    GameObject razer = Instantiate(razers[2], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 1:
                    position = new Vector3(20, 10, randZ);
                    GameObject razer1 = Instantiate(razers[2], position, Quaternion.identity);
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 2:
                    position = new Vector3(randX, 10, -20);
                    GameObject razer2 = Instantiate(razers[2], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 3:
                    position = new Vector3(randX, 10, 20);
                    GameObject razer3 = Instantiate(razers[2], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
            }

        }
        else if (randPattern == 3)
        {
            int dirRandom = Random.Range(0, 6); 
            switch(dirRandom)
            {
                case 0:
                    Vector3 position = new Vector3(randX, 20, randZ);
                    GameObject razer = Instantiate(razers[3], position, Quaternion.identity);
                    break;
                case 1:
                    Vector3 position1 = new Vector3(randX, 0, randZ);
                    GameObject razer1 = Instantiate(razers[3], position1, Quaternion.identity);
                    break;
                case 2:
                    Vector3 position2 = new Vector3(10, randY, randZ);
                    GameObject razer2 = Instantiate(razers[3], position2, Quaternion.identity);
                    break;
                case 3:
                    Vector3 position3 = new Vector3(-10, randY, randZ);
                    GameObject razer3 = Instantiate(razers[3], position3, Quaternion.identity);
                    break;
                case 4:
                    Vector3 position4 = new Vector3(randX, randY, 10);
                    GameObject razer4 = Instantiate(razers[3], position4, Quaternion.identity);
                    break;
                case 5:
                    Vector3 position5 = new Vector3(randX, randY, -10);
                    GameObject razer5 = Instantiate(razers[3], position5, Quaternion.identity);
                    break;
            }
        }
        yield return new WaitForSeconds(1.5f);
        cooltime = false;
    }
}
