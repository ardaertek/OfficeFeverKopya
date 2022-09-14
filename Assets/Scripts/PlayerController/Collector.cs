using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collector : MonoBehaviour
{
    public List<GameObject> ItemList;
    [SerializeField] Vector3 offSet;
    [SerializeField] public GameObject BagPoint;
    [SerializeField] Transform _targetPosition;
    public void ItemPositionSet(GameObject obj,CollectZone collectZone)
    {
        obj.transform.DOKill();
        obj.transform.parent = BagPoint.transform;
        ItemList.Add(obj);
        obj.transform.DOMove(_targetPosition.position, 0.1f).OnComplete(() =>
        {
            setTargetPosition(ItemList.Count);
            collectZone.GetNext = true;
        });
    }

    private void setTargetPosition(int i)
    {
        if (i % 15 == 0)
        {
            _targetPosition.position = new Vector3(BagPoint.transform.position.x, BagPoint.transform.position.y, _targetPosition.position.z);
            _targetPosition.position -= offSet.z * Vector3.forward;
        }
        else
        {
            _targetPosition.position += offSet.y * Vector3.up;
        }
    }

}
