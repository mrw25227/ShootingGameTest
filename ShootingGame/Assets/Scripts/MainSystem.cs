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

    [SerializeField]
    LocalDataSaveModel saveData;

    [SerializeField]
    Text endMenuText;

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
            
            if(isGameOver && scoreQueue.Count == 0 && score > saveData.GetMinScore())
            {
                saveData.AddNewScore(score);
                scoreText.text = score + " \n " + "<color=red> �s����!! </color>";
            }
            else
            {
                scoreText.text = score.ToString();
            }
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
        endMenuText.text = isWin ? "��  ��!!" : "�C������";
        isGameOver = true;
        if(scoreQueue.Count == 0 && score > saveData.GetMinScore())
        {
            saveData.AddNewScore(score);
            scoreText.text = score + " \n " + "<color=red> �s����!! </color>";

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
