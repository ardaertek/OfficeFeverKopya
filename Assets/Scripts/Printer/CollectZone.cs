using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectZone : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] public PaperSpawner paperSpawner;
    [SerializeField] float collectSpeed = 3f;
    bool collecting;
    public bool InventoryFull;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        Collector collector = other.GetComponent<Collector>();
        paperSpawner.PlayerCollecting = true;
        collecting = true;
        StartCoroutine(GiveObject(collector));
    }

    public bool GetNext = true;
    private IEnumerator GiveObject(Collector collector)
    {
        while (collecting)
        {
            yield return new WaitUntil(() => GetNext);
            if (!InventoryFull)
            {
                paperSpawner.InventoryFull = true;
                if (paperSpawner.PaperList.Count != 0)
                {
                    GetNext = false;
                    GameObject obj = paperSpawner.GetItemFromList();
                    collector.ItemPositionSet(obj, this);
                }
            }
            else
            {
                paperSpawner.InventoryFull = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Collector collector = other.GetComponent<Collector>();
        paperSpawner.PlayerCollecting = false;
        collecting = false;
        StopCoroutine(GiveObject(collector));
    }
}
