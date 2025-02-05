using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 1;
    protected BulletComeFrom ComeFrom;
    protected float lifeTime;

    public float GetBulletSpeed() {  return speed; }
    public BulletComeFrom GetComeFrom() { return ComeFrom; }
    public enum BulletComeFrom
    {
        PLAYER,
        ENEMY
    }
}
