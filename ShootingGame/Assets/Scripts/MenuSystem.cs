using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MenuSystem : MonoBehaviour
{

    [SerializeField]
    CanvasGroup scoreTab, menuTab;

    [SerializeField]
    LocalDataSaveModel saveData;

    [SerializeField]
    Text scoreListText;

    [SerializeField]
    Text playerText;

    [SerializeField]
    Text DebugText;

    [SerializeField]
    Button serBtn, cliBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData.Init();
        GetLocalIPAddress();
        serBtn.onClick.AddListener(serBtnClick);
        cliBtn.onClick.AddListener(cliBtnClick);

    }

    private void serBtnClick()
    {
        var unityTransport = FindAnyObjectByType<UnityTransport>();
        unityTransport.ConnectionData.Address = GetLocalIPAddress();
        NetworkManager.Singleton.StartHost();
    }
    private void cliBtnClick()
    {
        NetworkManager.Singleton.StartClient();
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                DebugText.text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndBtnClick()
    {
        Application.Quit();
    }

    public void OnScoreClick()
    {
        scoreTab.alpha = 1;
        scoreTab.interactable = true;
        scoreTab.blocksRaycasts = true;

        menuTab.alpha = 0;
        menuTab.interactable = false;
        menuTab.blocksRaycasts = false;
        SetScoreList();
    }

    public void OnBackMenuBtnClick()
    {
        scoreTab.alpha = 0;
        scoreTab.interactable = false;
        scoreTab.blocksRaycasts = false;

        menuTab.alpha = 1;
        menuTab.interactable = true;
        menuTab.blocksRaycasts = true;
    }

    public void OnStartGameBtnClick()
    {
        StaticData.isNetwork = false;
        SceneManager.LoadScene("MainScene");
    }

    void SetScoreList()
    {
        var scoreList = saveData.GetScoreList();
        string scoreText = "";
        for (int i = 1; i <= scoreList.Count; i++)
        {
            scoreText += "No " + i.ToString("00") + ". ------------- " + scoreList[scoreList.Count - i].ToString("000,000") + "\n";
        }
        scoreListText.text = scoreText;
    }


    public void OnPlayerChangeBtnClick()
    {
        if(StaticData.playNo == 1)
        {
            StaticData.playNo = 2;
            playerText.text = "我是2P";
        }
        else
        {
            StaticData.playNo = 1;
            playerText.text = "我是1P";
        }
    }

    public void OnNetworkPlayBtnClick()
    {
        StaticData.isNetwork = true;
        SceneManager.LoadScene("NetworkMainScene");
    }
}
