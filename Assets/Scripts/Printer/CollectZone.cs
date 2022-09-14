using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectZone : MonoBehaviour
{
    [SerializeField] PaperSpawner paperSpawner;
    [SerializeField] float collectSpeed = 3f;
    bool collecting;
    private void OnTriggerEnter(Collider other)
    {
        PlayerEventManager.onPlayerStartCollect += PlayerCollecting;
        PlayerEventManager.onPlayerStopCollect += PlayerNotCollecting;
        PlayerEventManager.FireOn_StartCollect();

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
            GetNext = false;
            GameObject obj = paperSpawner.GetItemFromList();
            collector.ItemPositionSet(obj,this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Collector collector = other.GetComponent<Collector>();
        paperSpawner.PlayerCollecting = false;
        collecting = false;
        StopCoroutine(GiveObject(collector));
        paperSpawner.SetLastPoint();

        PlayerEventManager.FireOn_StopCollect();
        PlayerEventManager.onPlayerStartCollect -= PlayerCollecting;
        PlayerEventManager.onPlayerStopCollect -= PlayerNotCollecting;
    }
    private void PlayerCollecting()
    {
        
        
    }
    private void PlayerNotCollecting()
    {
        
        
    }
}
