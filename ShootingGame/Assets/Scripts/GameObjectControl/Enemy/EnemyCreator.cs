using System.Collections;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public float bossTime = 100;
    private float time = 0;
    bool isBossBorn = false;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject[] enemy;
    [SerializeField]
    int[] enemyNoArray = { 0, 0, 1, 1, 2, 3 };

    [SerializeField]
    float enemyCreateIntervalTime = 1;

    private IEnumerator RadomEnemyCreateCoroutine;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        RadomEnemyCreateCoroutine = RadomEnemyCreateEnumerator(enemyCreateIntervalTime);
        StartCoroutine(RadomEnemyCreateCoroutine);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > bossTime && !isBossBorn)
        {
            Instantiate(boss, new Vector3(StaticData.fieldBoundX + 2, 0, 0), Quaternion.identity);
            isBossBorn = true;
        }
    }

    void RadomEnemyCreate()
    {
        
        if (enemy.Length == 0) return;
        int enemyNo = enemyNoArray[Random.Range(0, enemyNoArray.Length)];
        float angle;
        Vector2 enemyPos, playerPos;
        GameObject enemyObject;
        switch (enemyNo)
        {
            case 0:
                enemyPos.x = Random.Range(0, StaticData.fieldBoundX);
                int randomY = Random.Range(0, 2);
                enemyPos.y = (randomY == 0 ? 1f : -1f) * (StaticData.fieldBoundY + 0.1f);
                angle = Random.Range(45, 90);
                
                enemyObject = Instantiate(enemy[enemyNo], enemyPos, Quaternion.Euler(new Vector3(0, 0, 180)));
                enemyObject.GetComponent<EnemyMotion>().moveAngle = angle;
                
                break;
            case 1:
                enemyPos.x = StaticData.fieldBoundX + 0.1f;
                enemyPos.y = Random.Range(-StaticData.fieldBoundY - 0.1f, StaticData.fieldBoundY + 0.1f);
                playerPos = player.transform.position;
                //Vector2 vector2 = ().normalized; 
                angle = Vector2.SignedAngle(Vector2.right, playerPos - enemyPos);
                //Debug.Log("playerPos: " + playerPos + " enemyPos: " + enemyPos + " vector2: " + vector2 + " SignedAngle: " + angle + " angle: " + Vector2.Angle(vector2, Vector2.right));
                enemyObject = Instantiate(enemy[enemyNo], enemyPos, Quaternion.Euler(new Vector3(0, 0, angle)));
                enemyObject.GetComponent<EnemyMotion>().moveAngle = angle;
                break;
            case 2:
                enemyPos.y = Random.Range(-StaticData.fieldBoundY * 2f / 3f, StaticData.fieldBoundY * 2f / 3f);
                enemyPos.x = StaticData.fieldBoundX + 0.1f;
                angle = Random.Range(170, 190);
                enemyObject = Instantiate(enemy[enemyNo], enemyPos, Quaternion.identity);
                enemyObject.GetComponent<EnemyMotion>().moveAngle = angle;
                break;
            case 3:
                enemyPos.y = Random.Range(-StaticData.fieldBoundY * 2f / 3f, StaticData.fieldBoundY * 2f / 3f);
                enemyPos.x = StaticData.fieldBoundX + 0.1f;
                enemyObject = Instantiate(enemy[enemyNo], enemyPos, Quaternion.identity);
                enemyObject.GetComponent<EnemyMotion>().moveAngle = 180;
                break;

        }

        

    }

    private IEnumerator RadomEnemyCreateEnumerator(float intervalTime)
    {
        while (!isBossBorn)
        {
            yield return new WaitForSeconds(intervalTime);
            RadomEnemyCreate();
        }
    }
}
