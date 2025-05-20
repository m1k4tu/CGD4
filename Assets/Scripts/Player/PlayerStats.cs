using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    int countdown;
    public NetworkVariable<int> playerID = new NetworkVariable<int>(0);
    public NetworkVariable<int> playerScore = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    void Start()
    {
        playerID = new NetworkVariable<int>(NetworkManager.Singleton.ConnectedClients.Count);
        countdown = 60;
    }

    void Update()
    {
        IncreaseScore();
    }

    public void IncreaseScore()
    {
        countdown--;
        if (countdown <= 0)
        {
            playerScore.Value++;
            countdown = 60;
        }
    }
}
