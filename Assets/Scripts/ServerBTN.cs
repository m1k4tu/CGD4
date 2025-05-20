using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerBTN : MonoBehaviour
{
    public void StartServer()
    {
        // Start the server
        if (!NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StartServer();
        }
    }

    public void StartClient()
    {
        try
        {
            NetworkManager.Singleton.StartClient();
        } catch (System.Exception e)
        {
            Debug.LogError("Failed to start client: " + e.Message);
        } 
    }
}
