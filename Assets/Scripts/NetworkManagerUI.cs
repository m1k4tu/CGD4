using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button ServerBtn;
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;

    private void Awake()
    {

        ServerBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            disableBTNS();                                          //so as to free screen space. theoretically could do a stop func o
        });

        HostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            disableBTNS();
        });

        ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            disableBTNS();
        });
    }

    public void disableBTNS()
    {
        ServerBtn.gameObject.SetActive(false);
        HostBtn.gameObject.SetActive(false);
        ClientBtn.gameObject.SetActive(false);
    }
}
