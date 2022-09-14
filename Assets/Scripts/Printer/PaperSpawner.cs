using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] GameManager gameManager;
    public List<GameObject> PaperList;
    [SerializeField] public Transform TargetPosition;
    [SerializeField] Vector3 _offSet, TargetFirstPosition;
    float _zCounter, _xCounter, _yCounter;
    public bool CanSpawn = true, PlayerCollecting,InventoryFull;

    private void Start()
    {
        PaperList = new List<GameObject>();
        TargetFirstPosition = TargetPosition.position;
        StartCoroutine(paperSpawner());
    }


    public GameObject GetItemFromList()
    {
        GameObject obj = PaperList[PaperList.Count - 1];
        PaperList.Remove(obj);
        return obj;
    }


    private IEnumerator paperSpawner()
    {
        while (CanSpawn)
        {
            yield return new WaitForSeconds(0.7f);
            GameObject obj = pool.GetItem();
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            PaperList.Add(obj);
            obj.SetActive(true);
            if (PlayerCollecting && !InventoryFull)
            {
                obj.transform.DOMove(gameManager.BagPoint.position, 10f).SetSpeedBased().OnComplete(() => 
                {
                    
                });
            }
            else
            {
                obj.transform.DOMove(TargetPosition.position, 10f).SetSpeedBased();
                SetNextPosition(PaperList.Count);
            }
        }
    }

    int a;
    private void SetNextPosition(int i)
    {
        if (i % 28 == 0)
        {
            TargetPosition.position = new Vector3(TargetFirstPosition.x, TargetPosition.position.y + _offSet.y, TargetFirstPosition.z);
        }
        else if (i % 7 == 0)
        {
            TargetPosition.position = new Vector3(TargetPosition.position.x, TargetPosition.position.y, TargetFirstPosition.z);
            TargetPosition.position += _offSet.x * Vector3.right;
        }
        else
        {
            TargetPosition.position += _offSet.z * Vector3.forward;
        }
    }
    public void SetLastPoint()
    {
        TargetPosition.position = TargetFirstPosition;
        if (PaperList.Count > 0)
        {
            int i = PaperList.Count;
            if (i / 28 > 0)
            {
                int k = i / 28;
                i -= 28 * k;
                while (k > 0)
                {
                    TargetPosition.position += _offSet.y * Vector3.up;
                    k--;
                }
            }
            if (i / 7 > 0)
            {
                int k = i / 7;
                i %= 7;
                while (k > 0)
                {
                    TargetPosition.position += _offSet.x * Vector3.right;
                    k--;
                }
            }
            while (i >= 1)//count 0 dan baþlýyor
            {
                TargetPosition.position += _offSet.z * Vector3.forward;
                i--;
            }
        }
    }
}

