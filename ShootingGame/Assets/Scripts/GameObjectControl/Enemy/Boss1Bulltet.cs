using UnityEngine;

public class Boss1Bulltet : EnemyBullet
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 15 / speed);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var radians = angle * Mathf.Deg2Rad;
        pos.x += Mathf.Cos(radians) * speed * Time.deltaTime;
        pos.y += Mathf.Sin(radians) * speed * Time.deltaTime;
        transform.position = pos;
    }
}
