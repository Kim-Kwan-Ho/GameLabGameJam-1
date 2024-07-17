using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicPatternStart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float EpicPatternTime = 15f;
    private int specialCount = 1;
    private int maxCount = 2;
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
            int dirRandom = Random.Range(0, 3); //0=PX, 1=Y, 2=Z 
            switch (dirRandom)
            {
                case 0:
                    Vector3 position = new Vector3(-20, 10, 0);
                    GameObject wall = Instantiate(special[0], position, Quaternion.Euler(0, 90, 0));
                    wall.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.PX;
                    position = new Vector3(20, 10, 0);
                    GameObject wall1 = Instantiate(special[0], position, Quaternion.Euler(0, 90, 0));
                    wall1.GetComponent<MovingWallMoving>().wallType = MovingWallMoving.WallType.NX;
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
            yield return new WaitForSeconds(10f);
        }

        if(specialCount == 2)
        {
            RazerMaker.isSpecial = true;
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
                yield return new WaitForSeconds(0.5f);
                count++;
            }
            yield return new WaitForSeconds(1f);
            RazerMaker.isSpecial = false;
        }
        specialCount++;
        if(specialCount > maxCount)
        {
            specialCount = 1;
        }
        Time.timeScale += 0.1f;
        EndEpicPattern();
    }

    private void EndEpicPattern()
    {
        GameSceneManager.Instance.GameSceneEvent.CallOnGameStart();
    }

}
