using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 1;
    protected float lifeTime;
    protected int attack = 5;
    public float GetBulletSpeed() {  return speed; }

    public int getAttack()
    {
        return attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bullet trigger");
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
