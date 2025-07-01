using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TeamBtn : MonoBehaviour
{
    [SerializeField] private Button RedBtn;
    [SerializeField] private Button YellowBtn;
    [SerializeField] private Button GreenBtn;

    public PlayerStats Player;
    private void Awake()
    {
       // if (NetworkManager.Singleton.IsServer) DisableBTNS();

        RedBtn.onClick.AddListener(() =>
        {
            Player.SetTeam(0);
            NetworkManager.Singleton.StartClient();
            DisableBTNS();                                          //so as to free screen space. theoretically could do a stop func o
        });

        YellowBtn.onClick.AddListener(() =>
        {
            Player.SetTeam(1);
            NetworkManager.Singleton.StartHost();
            DisableBTNS();
        });

        GreenBtn.onClick.AddListener(() =>
        {
            Player.SetTeam(2);
            NetworkManager.Singleton.StartClient();
            DisableBTNS();
        });
    }

    public void DisableBTNS()
    {
        RedBtn.gameObject.SetActive(false);
        YellowBtn.gameObject.SetActive(false);
        GreenBtn.gameObject.SetActive(false);
    }
}
