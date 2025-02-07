using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour
{

    [SerializeField]
    GameObject endUI;

    [SerializeField]
    Text lifeText;

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
        StaticData.life = 5;
        lifeText.text = "life: " + StaticData.life;
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnBossDeath -= OnBossDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        lifeText.text = "life: " + StaticData.life;
        if (StaticData.life == 0) 
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endUI.SetActive(true);
    }
}
