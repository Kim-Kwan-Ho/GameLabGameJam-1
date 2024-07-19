using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMagnetic : MonoBehaviour
{
    [SerializeField] private float _magPower = 1f;

    private Dictionary<int, Transform> _itemDic = new Dictionary<int, Transform>();
    private bool _isScoreItem = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_itemDic.ContainsKey(other.GetInstanceID()))
        {
            return;
        }
        else
        {
            _itemDic[other.GetInstanceID()] = other.transform;
            StartCoroutine(CoPullToPlayer(other.GetInstanceID(), other.transform));
        }


    }

    private IEnumerator CoPullToPlayer(int id, Transform trs)
    {
        float time = 0;
        Vector3 startPos = trs.position;
        while (time < _magPower)
        {
            if (trs == null)
            {
                _itemDic.Remove(id);
                
                break;
            }
            else
            {
                trs.position = Vector3.Slerp(startPos, transform.position, time / _magPower);
                time += Time.deltaTime;
            }
            yield return null;
        }
        yield break;
    }




}
