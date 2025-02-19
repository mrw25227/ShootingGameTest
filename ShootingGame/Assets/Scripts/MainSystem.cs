using System.Collections.Generic;
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

    int score = 0;
    Queue<int> scoreQueue = new Queue<int>();

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
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnBossDeath -= OnBossDeath;
        EventManager.Instance.OnPlayerBeHit -= OnLifeChange;
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreQueue.Count != 0)
        {
            score += scoreQueue.Dequeue();
            scoreText.text = score.ToString();
        }
    }

    void OnBossDeath()
    {
        EndGame();
    }

    public void OnEndBtnClick()
    {
        Application.Quit();
    }

    public void OnResetBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnLifeChange()
    {
        if(StaticData.life == 0)
        {
            return;
        }
        StaticData.life--;
        lifeText.text = "Life: " + StaticData.life;
        if (StaticData.life == 0) 
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endUI.alpha = 1;
        endUI.interactable = true;
        endUI.blocksRaycasts = true;
    }

    void OnGetScore(int value)
    {
        scoreQueue.Enqueue(value);
    }
}
