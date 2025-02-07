using UnityEngine;

public class BulletNormal : Bullet
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8.0f;
        attack = 10;
        Destroy(gameObject, 20 / speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    

}
