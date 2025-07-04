using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    int countdown;
    public NetworkVariable<int> playerID = new NetworkVariable<int>(0);
    public NetworkVariable<int> playerScore = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> Team = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private TextMeshProUGUI text;
    void Start()
    {
        playerID = new NetworkVariable<int>(NetworkManager.Singleton.ConnectedClients.Count);
        countdown = 60;
        
    }

    void Update()
    {
        IncreaseScore();

        Debug.Log(Team);
    }

    public void IncreaseScore()
    {
        countdown--;
        if (countdown <= 0)
        {
            playerScore.Value++;
            countdown = 60;
        }
        text.text = playerScore.Value.ToString();
    }

    public void SetTeam(int team)
    {
        Team = new NetworkVariable<int>(team);
    }
}
