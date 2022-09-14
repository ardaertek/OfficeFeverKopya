using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ObjectPool _paperPool;
    public ObjectPool PaperPool { get { return _paperPool; } }












    //Instantie GameManager
    public static GameManager _instance;

    private void Awake()
    {
        _instance = this;
        PlayerEventManager.FireOn_GameStarted();
    }
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                obj.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        PlayerEventManager.FireOn_GameStopped();
    }

}
