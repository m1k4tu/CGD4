using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CreateObject : NetworkBehaviour
{
    [SerializeField]
    private GameObject prefabPillar;
    [SerializeField]
    private GameObject prefabOrb;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (this.gameObject.GetComponent<PlayerStats>().playerScore.Value > 20)
            {
                this.gameObject.GetComponent<PlayerStats>().playerScore.Value -= 2;
                CreatePillarServerRPC();
            }           
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (this.gameObject.GetComponent<PlayerStats>().playerScore.Value > 30)
            {
                this.gameObject.GetComponent<PlayerStats>().playerScore.Value -= 4;
                CreateOrbServerRPC();
            }
        }
    }
    public Vector3 CalculateSpawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        return spawnPosition;
    }

    [ServerRpc]
    public void CreatePillarServerRPC()
    {
        Vector3 a = CalculateSpawn();
        GameObject ob = Instantiate(prefabPillar, a, Quaternion.identity);
        ob.GetComponent<NetworkObject>().Spawn(true);
    }
    [ServerRpc]
    public void CreateOrbServerRPC()
    {
        Vector3 a = Vector3.zero;
        GameObject ob = Instantiate(prefabOrb, a, Quaternion.identity);
        ob.GetComponent<NetworkObject>().Spawn(true);
    }
}
