using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperSpawner : MonoBehaviour
{
    GameManager gameManager;
    public List<GameObject> PaperList;
    [SerializeField] public Transform TargetPosition;
    [SerializeField] Vector3 _offSet, TargetFirstPosition;
    float _zCounter, _xCounter, _yCounter, _spawnerFreq = 0.75f;
    public bool CanSpawn = true, PlayerCollecting;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
            yield return new WaitForSeconds(_spawnerFreq);
            GameObject obj = gameManager.PaperPool.GetItem();
            obj.transform.parent = transform;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            PaperList.Add(obj);
            if (PlayerCollecting)
            {
                obj.transform.position = transform.position;
            }
            else
            {
                obj.transform.DOMove(TargetPosition.position, 2f);
                SetNextPosition(PaperList.Count);
            }
        }
    }

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

    int a;

    public void SetLastPoint()
    {
        TargetPosition.position = TargetFirstPosition;
        Debug.Log(PaperList.Count);
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
            Debug.Log("i = " + i);
            while (i > 1)//count 0 dan baþlýyor
            {
                a++;
                TargetPosition.position += _offSet.z * Vector3.forward;
                i--;
            }
            a = 0;
            Debug.Log("a = " + a);
        }
    }
}
