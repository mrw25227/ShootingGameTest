using UnityEngine;


public class EnemyMotion: MonoBehaviour
{
    public int life = 1;
    private bool isDeath = false;

    public MoveType moveType = MoveType.MOTIONLESS;
    public float moveAngle = 0;
    public float moveSpeed = 5;

    public GameObject[] bullet;

    public Vector2 bornPos;

    private float moveBack = 1;
    protected float destroyTime = 0.5f;
    protected bool isStart = true;
    AudioSource destroySound;
    GameObject explosionEffect;
    Animator animator;
    float beHitTime = 0f;


    private void Start()
    {
        destroySound = GetComponent<AudioSource>();
        explosionEffect = transform.Find("ExplosionEffect").gameObject;
        animator = GetComponent<Animator>();
        StartAction();
    }

    private void Update()
    {
        if (!isDeath && isStart)
        {

            MoveAction();
            ShootAtion();
            if (beHitTime > 0 && animator != null)
            {
                Debug.Log("beHitAnimation beHitTime: " + beHitTime);
                beHitTime -= Time.deltaTime;
                if(beHitTime <= 0)
                {
                    animator.SetBool("beHit", false);
                }
            }
        }
    }

    public void BeHitAction(int attack)
    {
        if (isDeath) return;

        life -= attack;
        Debug.Log("get attack life: " + life);
        LifeReduceAction();
        if (life <= 0) 
        {
            if (animator != null)
            {
                animator.SetBool("beHit", false);
            }
            isDeath = true;
            DestroyAction();
        }
        else
        {
            Debug.Log("beHitAnimation" );
            if (animator != null) 
            {
                beHitTime = 1f;
                animator.SetBool("beHit", true);
            }
        }
    }

    public virtual void LifeReduceAction()
    {

    }

    public virtual void DestroyAction()
    {
        Destroy(gameObject, destroyTime);
        if (destroySound != null)
        {
            destroySound.Play();
        }
        if(explosionEffect != null)
        {
            explosionEffect.SetActive(true);
        }
    }
    public virtual void StartAction()
    {

    }
    public virtual void MoveAction()
    {
        switch (moveType)
        {
            case MoveType.MOTIONLESS:
                break;
            case MoveType.STRAIGHT:
                MoveStraight();
                break;
            case MoveType.MOVE_ON_2_POINT:
                MoveOn2point();
                break;
        }
    }
    void MoveStraight()
    {
        var pos = transform.position;
        var radians = moveAngle * Mathf.Deg2Rad;
        pos.x += Mathf.Cos(radians) * moveSpeed * Time.deltaTime;
        pos.y += Mathf.Sin(radians) * moveSpeed * Time.deltaTime;


        transform.position = pos;
    }

    void MoveOn2point()
    {

        var pos = transform.position;
        var radians = moveAngle * Mathf.Deg2Rad;
        pos.x += moveBack * Mathf.Cos(radians) * moveSpeed * Time.deltaTime;
        pos.y += moveBack * Mathf.Sin(radians) * moveSpeed * Time.deltaTime;
        if (pos.x > StaticData.fieldBoundX || pos.x < -StaticData.fieldBoundX || pos.y > StaticData.fieldBoundY || pos.y < -StaticData.fieldBoundY)
        {
            moveBack *= -1;
        }
        else
        {
            transform.position = pos;
        }
        
    }

    public virtual void ShootAtion()
    {
        //Debug.Log("ShootAtion");
    }


    [System.Serializable]
    public enum MoveType
    {
        MOTIONLESS,
        STRAIGHT,
        MOVE_ON_2_POINT,
        OTHER
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if(bullet != null)
            {
                BeHitAction(bullet.getAttack());
            }
            
        }
    }
}
