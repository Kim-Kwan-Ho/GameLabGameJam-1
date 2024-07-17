using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicPatternStart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float EpicPatternTime = 15f;
    private int specialCount = 1;
    private int maxCount = 3;
    public GameObject[] special;
    private void Start()
    
    {
        GameSceneManager.Instance.GameSceneEvent.EpicPatternStart += StartEpicPatternStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartEpicPatternStart(GameSceneEventArgs gameSceneEventArgs)
    {
        StartCoroutine(StartEpicPattern());
    }

    private IEnumerator StartEpicPattern()
    {
        if (specialCount == 1)
        {
            specialCount++;
            int count = 0;
            int dirRandom; //0=PX, 1=Y, 2=Z 
            RazerMaker.isSpecial = false;
            RazerMaker.genCooltime = 1.5f;
            if(CameraMoving.viewState == CameraMoving.ViewState.PX || CameraMoving.viewState == CameraMoving.ViewState.NX)
            {
                dirRandom = Random.Range(1, 3);
            }
            else if(CameraMoving.viewState == CameraMoving.ViewState.PY || CameraMoving.viewState == CameraMoving.ViewState.NY)
            {
                dirRandom = Random.Range(0, 2);
                if(dirRandom == 1)
                {
                    dirRandom = 2;
                }
            }
            else
            {
                dirRandom = Random.Range(0, 2);
            }
            switch (dirRandom)
            {
                case 0:
                    Vector3 position = new Vector3(-20, 10, 0);
                    GameObject wall = Instantiate(special[0], position, Quaternion.Euler(0, 90, 0));
                    wall.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.PX;
                    position = new Vector3(20, 10, 0);
                    GameObject wall1 = Instantiate(special[0], position, Quaternion.Euler(0, 90, 0));
                    wall1.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.NX;

                    while(count < 5)
                    {
                        position = new Vector3(Random.Range(-20, 20), 10, Random.Range(-20, 20));
                    }
                    break;
                case 1:
                    Vector3 position1 = new Vector3(0, 30, 0);
                    GameObject wall2 = Instantiate(special[0], position1, Quaternion.Euler(90, 0, 0));
                    wall2.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.NY;
                    position1 = new Vector3(0, -10, 0);
                    GameObject wall3 = Instantiate(special[0], position1, Quaternion.Euler(90, 0, 0));
                    wall3.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.PY;
                    break;
                case 2:
                    Vector3 position2 = new Vector3(0, 10, 20);
                    GameObject wall4 = Instantiate(special[0], position2, Quaternion.identity);
                    wall4.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.NZ;
                    position2 = new Vector3(0, 10, -20);
                    GameObject wall5 = Instantiate(special[0], position2,Quaternion.identity );
                    wall5.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.PZ;
                    break;                
            }
            yield return new WaitForSeconds(12f);
            RazerMaker.isSpecial = true;
            RazerMaker.genCooltime = 3f;
            yield return new WaitForSeconds(3f);
        }

        else if(specialCount == 2)
        {
            specialCount++;
            int count = 0;
            while(count < 20)
            {
                int dirRandom = Random.Range(0, 6); 
                float randY = Random.Range(1, 20);
                float randX = Random.Range(-9, 10);
                float randZ = Random.Range(-9, 10);
                switch(dirRandom)
                {
                    case 0:
                        Vector3 position = new Vector3(randX, 20, randZ);
                        GameObject rocket = Instantiate(special[1], position, Quaternion.identity);
                        break;
                    case 1:
                        Vector3 position1 = new Vector3(randX, 0, randZ);
                        GameObject rocket1 = Instantiate(special[1], position1, Quaternion.identity);
                        break;
                    case 2:
                        Vector3 position2 = new Vector3(10, randY, randZ);
                        GameObject rocket2 = Instantiate(special[1], position2, Quaternion.identity);
                        break;
                    case 3:
                        Vector3 position3 = new Vector3(-10, randY, randZ);
                        GameObject rocket3 = Instantiate(special[1], position3, Quaternion.identity);
                        break;
                    case 4:
                        Vector3 position4 = new Vector3(randX, randY, 10);
                        GameObject rocket4 = Instantiate(special[1], position4, Quaternion.identity);
                        break;
                    case 5:
                        Vector3 position5 = new Vector3(randX, randY, -10);
                        GameObject rocket5 = Instantiate(special[1], position5, Quaternion.identity);
                        break;
                }
                yield return new WaitForSeconds(0.4f);
                count++;
            }
            yield return new WaitForSeconds(3f);
        }

        else if(specialCount == 3)
        {
            specialCount++;
            Vector3 position;
            RazerMaker.isSpecial = false;
            int count = 0;
            while(count < 3)
            {
                int randXYZ = Random.Range(0, 6);
                int randRot = Random.Range(0, 4);

                if(CameraMoving.viewState == CameraMoving.ViewState.PZ || CameraMoving.viewState == CameraMoving.ViewState.NZ)
                {
                    randXYZ = Random.Range(2, 6);
                }
                else if(CameraMoving.viewState == CameraMoving.ViewState.PY || CameraMoving.viewState == CameraMoving.ViewState.NY)
                {
                    randXYZ = Random.Range(0, 4);
                    if(randXYZ == 2 || randXYZ == 3)
                    {
                        randXYZ = Random.Range(4, 6);
                    }
                }
                else if(CameraMoving.viewState == CameraMoving.ViewState.PX || CameraMoving.viewState == CameraMoving.ViewState.NX)
                {
                    randXYZ = Random.Range(0, 4);
                }
                Quaternion zRot;
                if (randRot == 0)
                {
                    zRot = Quaternion.Euler(0, -90, -90);
                }
                else if (randRot == 1)
                {
                    zRot = Quaternion.Euler(-90, -90, -90);
                }
                else if (randRot == 2)
                {
                    zRot = Quaternion.Euler(-180, -90, -90);
                }
                else
                {
                    zRot = Quaternion.Euler(0, 270, 0);
                }
                switch(randXYZ)
                {
                    case 0:
                        position = new Vector3(0, 10, -20);
                        GameObject Wall0 = Instantiate(special[2], position, Quaternion.Euler(-90*randRot, -90,-90));
                        Wall0.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.PZ;
                        Debug.Log("PZ");
                        break;
                    case 1:
                        position = new Vector3(0, 10, 20);
                        GameObject Wall1 = Instantiate(special[2], position, Quaternion.Euler(-90*randRot, -90 ,-90));
                        Wall1.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.NZ;
                        Debug.Log("NZ");
                        break;
                    case 2:
                        position = new Vector3(0, -10, 0);
                        GameObject Wall2 = Instantiate(special[2], position, Quaternion.Euler(0,90*randRot,0));
                        Wall2.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.PY;
                        Debug.Log("PY");
                        break;
                    case 3:
                        position = new Vector3(0, 30, 0);
                        GameObject Wall3 = Instantiate(special[2], position, Quaternion.Euler(0,90*randRot,0));
                        Wall3.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.NY;
                        Debug.Log("NY");
                        break;
                    case 4:
                        position = new Vector3(20, 10, 0);
                        GameObject Wall4 = Instantiate(special[2], position, Quaternion.Euler(90*randRot, 0, 90));
                        Wall4.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.NX;
                        Debug.Log("NX");
                        break;
                    case 5:
                        position = new Vector3(-20, 10, 0);
                        GameObject Wall5 = Instantiate(special[2], position, Quaternion.Euler(90*randRot, 0, 90));
                        Wall5.GetComponent<Wall1_4Moving>().wallType = Wall1_4Moving.WallType.PX;
                        Debug.Log("PX");
                        break;
                }
                count++;
                yield return new WaitForSeconds(5f);
            }
            RazerMaker.isSpecial = true;
            yield return new WaitForSeconds(3f);
        }
        if(specialCount > maxCount)
        {
            specialCount = 1;
        }
        EndEpicPattern();
    }

    private void EndEpicPattern()
    {
        GameSceneManager.Instance.GameSceneEvent.CallEpicPatternEnd();
    }

}
