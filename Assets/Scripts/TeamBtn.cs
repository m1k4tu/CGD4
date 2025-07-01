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

    private void Awake()
    {

        RedBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            disableBTNS();                                          //so as to free screen space. theoretically could do a stop func o
        });

        YellowBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            disableBTNS();
        });

        GreenBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            disableBTNS();
        });
    }

    public void disableBTNS()
    {
        RedBtn.gameObject.SetActive(false);
        YellowBtn.gameObject.SetActive(false);
        GreenBtn.gameObject.SetActive(false);
    }
}
