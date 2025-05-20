using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isServer = true;
    // Start is called before the first frame update
    void Start()
    {
        
        //bool isServer = NetworkManager.Singleton.IsServer;
        if (isServer) NetworkManager.Singleton.StartHost();
        else NetworkManager.Singleton.StartClient();
    }

}
