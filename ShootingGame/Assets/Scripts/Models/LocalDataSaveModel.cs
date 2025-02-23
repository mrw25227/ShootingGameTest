using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Local Data Save Model", menuName = "Local Data Save Model")]
public class LocalDataSaveModel:ScriptableObject
{
    private const int MAX_SCORE_RECORD = 10;
    const string LOCAL_DATA_SAVE_PREF = "LocalDataSavePref";
    private SaveDataJson saveDataJson;

    [SerializeField]
    private List<int> scoreList = new List<int>() { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    public void Init()
    {
        Debug.Log("LocalDataSaveModel Init");
        if (PlayerPrefs.HasKey(LOCAL_DATA_SAVE_PREF))
        {
            string saveData = PlayerPrefs.GetString(LOCAL_DATA_SAVE_PREF);
            saveDataJson = JsonUtility.FromJson<SaveDataJson>(saveData);
            scoreList = saveDataJson.scoreList;
        }
        else
        {
            saveDataJson = new SaveDataJson();
            saveDataJson.scoreList = scoreList;
        }
    }

    [ContextMenu("ResetScore")]
    public void ResetScore()
    {
        scoreList = new List<int>() { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    }

    [ContextMenu("ClearScore")]
    public void ClearScore()
    {
        scoreList = new List<int>();
    }

    public void AddNewScore(int score)
    {
        scoreList.RemoveAt(0);
        
        if(score > scoreList[scoreList.Count - 1])
        {
            scoreList.Add(score);
        }
        else
        {
            int index = scoreList.BinarySearch(score);
            Debug.Log("score index: " + index);
            if(index < 0)
            {
                scoreList.Insert(~index, score);
            }
            else
            {
                scoreList.Insert(index, score);
            }
            
        }
        
        saveDataJson.scoreList = scoreList;
        PlayerPrefs.SetString(LOCAL_DATA_SAVE_PREF, JsonUtility.ToJson(saveDataJson));
        PlayerPrefs.Save();
    }
    public List<int> GetScoreList()
    {
        return scoreList;
    }
    public int GetMinScore()
    {
        return scoreList[0];
    }
}

[Serializable]
public class SaveDataJson
{
    public List<int> scoreList;
}