using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 1;
    protected BulletComeFrom ComeFrom;
    protected float lifeTime;
    protected int attack = 5;
    public float GetBulletSpeed() {  return speed; }
    public BulletComeFrom GetComeFrom() { return ComeFrom; }

    public int getAttack()
    {
        return attack;
    }
    public enum BulletComeFrom
    {
        PLAYER,
        ENEMY
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
