using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{

    [SerializeField]
    CanvasGroup scoreTab, menuTab;

    [SerializeField]
    LocalDataSaveModel saveData;

    [SerializeField]
    Text scoreListText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData.Init();
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

}
