using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class RazerMaker : MonoBehaviour
{
    public Transform center;
    public GameObject[] razers; //0 for z, 1 for y
    private bool cooltime = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cooltime)
        {
            StartCoroutine(RandRazer());
            cooltime = true;
        }
    }

    IEnumerator RandRazer()
    {

        int randomXYZ = Random.Range(0, 3);
        float randY = Random.Range(1, 20);
        float randXZ = Random.Range(-9, 10);
        if (randomXYZ == 0)
        {
            int dirRandom = Random.Range(0, 4); //0=PY, 1=NY, 2=NZ, 3=PZ

            Vector3 position;
            switch (dirRandom)
            {
            
                case 0://Z-razer and PY
                    position = new Vector3(0, -10, randXZ);

                    GameObject razer = Instantiate(razers[0], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 1:
                    position = new Vector3(0, 30, randXZ);
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
        else if(randomXYZ == 1)
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
                    position = new Vector3(randXZ, -10, 0);
                    GameObject razer2 = Instantiate(razers[1], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 3:
                    position = new Vector3(randXZ, 30, 0);
                    GameObject razer3 = Instantiate(razers[1], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
            }    
        }
        else if (randomXYZ == 2)
        {
            int dirRandom = Random.Range(0, 4); //0=PX, 1=NX, 2=PZ, 3=NZ
            Vector3 position;
            switch (dirRandom)
            {
                case 0:
                    position = new Vector3(-20, 10, randXZ);
                    GameObject razer = Instantiate(razers[2], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 1:
                    position = new Vector3(20, 10, randXZ);
                    GameObject razer1 = Instantiate(razers[2], position, Quaternion.identity);
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 2:
                    position = new Vector3(randXZ, 10, -20);
                    GameObject razer2 = Instantiate(razers[2], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 3:
                    position = new Vector3(randXZ, 10, 20);
                    GameObject razer3 = Instantiate(razers[2], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
            }

        }
        yield return new WaitForSeconds(1.5f);
        cooltime = false;
    }
}
