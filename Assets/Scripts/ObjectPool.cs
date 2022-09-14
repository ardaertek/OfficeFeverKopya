using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _poolList = new List<GameObject>();
    [SerializeField]
    public GameObject _poolPrefab;
    [SerializeField]
    [Range(10, 1000)]
    private int _poolCount = 100;

    private void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            GameObject temp = Instantiate(_poolPrefab, transform);
            temp.transform.parent = transform;
            _poolList.Add(temp);
            temp.SetActive(false);

        }
    }

    public GameObject GetItem()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            if (!_poolList[i].activeInHierarchy)
            {
                return _poolList[i];
            }
        }
        GameObject temp = Instantiate(_poolPrefab, transform);
        _poolList.Add(temp);
        temp.SetActive(false);
        return temp;
    }

}
