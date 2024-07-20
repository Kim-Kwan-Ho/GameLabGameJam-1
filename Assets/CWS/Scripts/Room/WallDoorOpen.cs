using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WallDoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _wallClosedCollider;

    public DoorPassColliderEvent DoorPassEvent;
    public bool isDoorActive = true;
    public Vector3 passDirection = Vector3.zero;

    void Start()
    {
        //DoorPassEvent.DoorPass += asdf;
    }

    void OnDestroy()
    {

    }

    void Update()
    {
        
    }

    public void OpenDoor()
    {
        isDoorActive = !isDoorActive;
        _door.SetActive(isDoorActive);
        _wallClosedCollider.SetActive(isDoorActive);
    }

    public void OpenDoor(bool _isDoorActive)
    {
        if (_isDoorActive)
        {
            isDoorActive = _isDoorActive;
            _door.SetActive(isDoorActive);
            _wallClosedCollider.SetActive(isDoorActive);
        }
    }
}
