using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;


    // Singleton pattern implementation
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<GameManager>();
    //            if (_instance == null)
    //            {
    //                GameObject go = new GameObject();
    //                go.name = typeof(GameManager).Name;
    //                _instance = go.AddComponent<GameManager>();
    //                DontDestroyOnLoad(go);
    //            }
    //        }
    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        TableManager tableManager = TableManager.Instance;
        CustomerManager customerManager = CustomerManager.Instance;
        PuddleManager puddleManager = PuddleManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
