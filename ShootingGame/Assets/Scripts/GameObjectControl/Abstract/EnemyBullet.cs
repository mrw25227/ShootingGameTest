using UnityEngine;

public abstract class EnemyBullet : MonoBehaviour
{
    protected float speed = 4;
    protected float lifeTime = 2;
    protected int attack = 1;
    protected float angle = 180;
    public float GetBulletSpeed() {  return speed; }

    public int getAttack()
    {
        return attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeAngle(float newAngle)
    {
        angle = newAngle;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
