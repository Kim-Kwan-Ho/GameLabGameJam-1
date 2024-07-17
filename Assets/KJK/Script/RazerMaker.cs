using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RazerMaker : MonoBehaviour
{
    public GameObject player;
    public Transform center;
    public GameObject[] razers; //0 for z, 1 for y
    private bool cooltime = false;
    public static float genCooltime = 3f;
    public static bool isSpecial = false;
    private bool _continue = true;
    void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.WarningSignal += OnWarningSignal;
        GameSceneManager.Instance.GameSceneEvent.EpicPatternEnd += OnEpicPatternEnd;
        GameSceneManager.Instance.GameSceneEvent.GameOver += OnGameOver;
        GameSceneManager.Instance.GameSceneEvent.GameResume += OnGameResume;
    }

    // Update is called once per frame
    void Update()
    {
        if(_continue)
        {
            if(PlayerMoving.playerMode == PlayerMoving.PlayerMode.D3)
            {
                if (!cooltime && !isSpecial)
                {
                    StartCoroutine(RandRazer());
                    cooltime = true;
                }
            }
            else if(PlayerMoving.playerMode == PlayerMoving.PlayerMode.D2)
            {
                if (!cooltime && !isSpecial)
                {
                    StartCoroutine(TwoDRandRazer());
                    cooltime = true;
                }
            }
        }
    }
    private void OnWarningSignal(GameSceneEventArgs gameSceneEventArgs)
    {
        isSpecial = true;
    }
    private void OnEpicPatternEnd(GameSceneEventArgs gameSceneEventArgs)
    {
        _continue = false;
    }
    private void OnGameOver(GameSceneEventArgs gameSceneEventArgs)
    {
        _continue = false;
    }
    private void OnGameResume(GameSceneEventArgs gameSceneEventArgs)
    {
        _continue = true;
        isSpecial = false;
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
        yield return new WaitForSeconds(3f);
        cooltime = false;
    }

    IEnumerator TwoDRandRazer()
    {
        int randPattern = Random.Range(0, 3);
        int randX = Random.Range(-9, 10);
        int randY = Random.Range(1, 20);
        int randZ = Random.Range(-9, 10);
        if(randPattern == 0)
        {
            int dirRandom = Random.Range(0, 6); 
            switch(dirRandom)
            {
                case 0:
                    Vector3 position0 = new Vector3(randX, 20, randZ);
                    GameObject razer = Instantiate(razers[3], position0, Quaternion.identity);
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
        Vector3 position;
        if(CameraMoving.viewState == CameraMoving.ViewState.PZ || CameraMoving.viewState == CameraMoving.ViewState.NZ)
        {
            randPattern = Random.Range(0, 8);
            switch(randPattern)
            {
                case 0:
                    position = new Vector3(-20, 10, player.transform.position.z);
                    GameObject razer = Instantiate(razers[2], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 1:
                    position = new Vector3(20, 10, player.transform.position.z);
                    GameObject razer1 = Instantiate(razers[2], position, Quaternion.identity);
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
                case 4:
                    position = new Vector3(-20, randY, 0);
                    GameObject razer4 = Instantiate(razers[1], position, Quaternion.identity);
                    razer4.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 5:
                    position = new Vector3(20, randY, 0);
                    GameObject razer5 = Instantiate(razers[1], position, Quaternion.identity);
                    razer5.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 6:
                    position = new Vector3(0, -10, player.transform.position.z);
                    GameObject razer6 = Instantiate(razers[0], position, Quaternion.identity);
                    razer6.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 7:
                    position = new Vector3(0, 30, player.transform.position.z);
                    GameObject razer7 = Instantiate(razers[0], position, Quaternion.identity);
                    razer7.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
            
            }
        }
        else if (CameraMoving.viewState == CameraMoving.ViewState.PY || CameraMoving.viewState == CameraMoving.ViewState.NY)
        {
            randPattern = Random.Range(0, 8);
            switch (randPattern)
            {
                case 0:
                    position = new Vector3(0, player.transform.position.y, 20);
                    GameObject razer = Instantiate(razers[0], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
                case 1:
                    position = new Vector3(0, player.transform.position.y, -20);
                    GameObject razer1 = Instantiate(razers[0], position, Quaternion.identity);
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 2:
                    position = new Vector3(20, player.transform.position.y, 0);
                    GameObject razer2 = Instantiate(razers[1], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 3:
                    position = new Vector3(-20, player.transform.position.y, 0);
                    GameObject razer3 = Instantiate(razers[1], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 4:
                    position = new Vector3(-20, 10, randZ);
                    GameObject razer4 = Instantiate(razers[2], position, Quaternion.identity);
                    razer4.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PX;
                    break;
                case 5:
                    position = new Vector3(20, 10, randZ);
                    GameObject razer5 = Instantiate(razers[2], position, Quaternion.identity);
                    razer5.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NX;
                    break;
                case 6:
                    position = new Vector3(randX, 10, -20);
                    GameObject razer6 = Instantiate(razers[2], position, Quaternion.identity);
                    razer6.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 7:
                    position = new Vector3(randX, 10, 20);
                    GameObject razer7 = Instantiate(razers[2], position, Quaternion.identity); 
                    razer7.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
            }
        }
        else
        {
            randPattern = Random.Range(0, 8);
            switch (randPattern)
            {
                case 0:
                    position = new Vector3(0, randY, 20);
                    GameObject razer = Instantiate(razers[0], position, Quaternion.identity);
                    razer.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
                case 1:
                    position = new Vector3(0,randY, -20);
                    GameObject razer1 = Instantiate(razers[0], position, Quaternion.identity);
                    razer1.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 2:
                    position = new Vector3(0, 30, randZ);
                    GameObject razer2 = Instantiate(razers[0], position, Quaternion.identity);
                    razer2.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
                case 3:
                    position = new Vector3(0, -10, randZ);
                    GameObject razer3 = Instantiate(razers[0], position, Quaternion.identity);
                    razer3.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
                case 4:
                    position = new Vector3(player.transform.position.x, 10, 20);
                    GameObject razer4 = Instantiate(razers[2], position, Quaternion.identity);
                    razer4.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NZ;
                    break;
                case 5:
                    position = new Vector3(player.transform.position.x, 10, -20);
                    GameObject razer5 = Instantiate(razers[2], position, Quaternion.identity);
                    razer5.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PZ;
                    break;
                case 6:
                    position = new Vector3(player.transform.position.x, 20, 0);
                    GameObject razer6 = Instantiate(razers[1], position, Quaternion.identity);
                    razer6.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.NY;
                    break;
                case 7:
                    position = new Vector3(player.transform.position.x, -20, 0);
                    GameObject razer7 = Instantiate(razers[1], position, Quaternion.identity);  
                    razer7.GetComponent<RazerMoving>().razerType = RazerMoving.RazerType.PY;
                    break;
            }
        }

           
        yield return new WaitForSeconds(genCooltime);
        cooltime = false;
    }


}
