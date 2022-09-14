using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collector : MonoBehaviour
{
    [SerializeField] PlayerInventory Inventory;
    [SerializeField] Vector3 offSet;
    private Vector3 _targetpos;
    public void ItemPositionSet(GameObject obj, CollectZone collectZone)
    {
        if (Inventory._inventory.Count <= Inventory.BagCapacity)
        {
            obj.transform.DOKill();
            Inventory.AddItemInventory(obj);
            obj.transform.DOLocalMove(_targetpos, 0.2f).OnComplete(() =>
            {
                _targetpos = new Vector3(0, offSet.y * Inventory._inventory.Count, 0);
                collectZone.GetNext = true;
                collectZone.paperSpawner.SetLastPoint();
            });
        }
        else
        {
            collectZone.InventoryFull = true;
        }
    }
}
