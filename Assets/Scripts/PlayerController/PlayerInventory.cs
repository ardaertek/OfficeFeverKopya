using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int BagCapacity;
    public List<GameObject> _inventory;
    private void Start()
    {
        _inventory = new List<GameObject>();
    }
    public void AddItemInventory(GameObject obj)
    {
        _inventory.Add(obj);
        obj.transform.parent = transform;
    }
}
