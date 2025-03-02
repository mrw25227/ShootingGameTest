using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour
{

    [SerializeField]
    CanvasGroup endUI;

    [SerializeField]
    Text lifeText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    LocalDataSaveModel saveData;

    [SerializeField]
    Text endMenuText, debugText;

    [SerializeField]
    NetworkManager networkManager;
    [SerializeField]
    UnityTransport unityTransport;

    int score = 0;
    Queue<int> scoreQueue = new Queue<int>();
    bool isGameOver = false;

    private void Awake()
    {
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;
        StaticData.fieldBoundX = horzExtent;
        StaticData.fieldBoundY = vertExtent;
    }

    void Start()
    {
        EventManager.Instance.OnBossDeath += OnBossDeath;
        EventManager.Instance.OnPlayerBeHit += OnLifeChange;
        EventManager.Instance.OnGetScore += OnGetScore;
        StaticData.life = 5;
        score = 0;
        lifeText.text = "Life: " + StaticData.life;
        scoreText.text = score.ToString();
        if (StaticData.isNetwork)
        {
            unityTransport = FindAnyObjectByType<UnityTransport>();
            //unityTransport.ConnectionData.Address = "";
            if (StaticData.playNo == 1)
            {
                
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                NetworkManager.Singleton.StartClient();
                
            }
            NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvent;
        }
    }


    private void OnConnectionEvent(NetworkManager manager, ConnectionEventData data)
    {
        Debug.LogWarning("OnConnectionEvent: " + data.EventType  + "  "+ data.ToString());
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnBossDeath -= OnBossDeath;
        EventManager.Instance.OnPlayerBeHit -= OnLifeChange;
        EventManager.Instance.OnGetScore -= OnGetScore;
        if (StaticData.isNetwork)
        {
            NetworkManager.Singleton?.Shutdown();
            NetworkManager.Singleton.OnConnectionEvent -= OnConnectionEvent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreQueue.Count != 0)
        {
            score += scoreQueue.Dequeue();
            
            if(isGameOver && scoreQueue.Count == 0 && score > saveData.GetMinScore())
            {
                saveData.AddNewScore(score);
                scoreText.text = score + " \n " + "<color=red> 新紀錄!! </color>";
            }
            else
            {
                scoreText.text = score.ToString();
            }
        }
        if(StaticData.isNetwork)
        {
            debugText.text = NetworkManager.Singleton.DisconnectReason;
            /*var instance = Instantiate(gameObject);
            var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();*/
        }
    }

    void OnBossDeath()
    {
        if (isGameOver)
        {
            return;
        }
        EndGame(true);
    }

    public void OnEndBtnClick()
    {
        Application.Quit();
    }

    public void OnResetBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackMenuBtnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnLifeChange()
    {
        if(isGameOver)
        {
            return;
        }
        StaticData.life--;
        lifeText.text = "Life: " + StaticData.life;
        if (StaticData.life == 0) 
        {
            EndGame(false);
        }
    }

    void EndGame(bool isWin)
    {
        endUI.alpha = 1;
        endUI.interactable = true;
        endUI.blocksRaycasts = true;
        endMenuText.text = isWin ? "恭  喜!!" : "遊戲結束";
        isGameOver = true;
        if(scoreQueue.Count == 0 && score > saveData.GetMinScore())
        {
            saveData.AddNewScore(score);
            scoreText.text = score + " \n " + "<color=red> 新紀錄!! </color>";

        }
        
    }

    void OnGetScore(int value)
    {
        if (isGameOver)
        {
            return;
        }
        scoreQueue.Enqueue(value);
    }
}
