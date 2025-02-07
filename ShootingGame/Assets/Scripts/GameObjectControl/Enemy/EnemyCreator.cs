using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public float bossTime = 100;
    private float time = 0;
    bool isBossBorn = false;

    [SerializeField]
    GameObject boos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > bossTime && !isBossBorn)
        {
            Instantiate(boos, new Vector3(StaticData.fieldBoundX + 2, 0, 0), Quaternion.identity);
            isBossBorn = true;
        }
    }

    
}
