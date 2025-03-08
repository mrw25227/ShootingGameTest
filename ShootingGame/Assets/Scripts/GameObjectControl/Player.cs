using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    float lastShootTime = -1000000f;
    float shootCooldownTimeSec = 0.2f;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource hitSound, shootSound;
    [SerializeField]
    GameObject destroyEffect, normalSprite;
    private float invincibleTime = 0;

    private void Awake()
    {
        EventManager.Instance.OnControllerClick += Move;
        EventManager.Instance.OnShootingClick += Shoot;
        invincibleTime = 2f;
        animator.SetBool("InvincibleMode", true);
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnControllerClick -= Move;
        EventManager.Instance.OnShootingClick -= Shoot;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime <= 0)
            {
                animator.SetBool("InvincibleMode", false);
                
            }
        }
        
    }

    void Move(Vector2 input)
    {
        if (StaticData.life == 0) return;
        //Debug.Log(input);
        var targetPos = transform.position;
        targetPos.x += input.x * moveSpeed * Time.deltaTime;
        if (targetPos.x >= StaticData.fieldBoundX)
        {
            targetPos.x = StaticData.fieldBoundX;
        }
        else if (targetPos.x <= -StaticData.fieldBoundX)
        {
            targetPos.x = -StaticData.fieldBoundX;
        }
        targetPos.y += input.y * moveSpeed * Time.deltaTime;
        if(targetPos.y >= StaticData.fieldBoundY)
        {
            targetPos.y = StaticData.fieldBoundY;
        }
        else if(targetPos.y <= -StaticData.fieldBoundY)
        {
            targetPos.y = -StaticData.fieldBoundY;
        }
        transform.position = targetPos;
    }

    public void Shoot()
    {
        float currentTime = Time.time;
        if(currentTime - lastShootTime >= shootCooldownTimeSec)
        {
            lastShootTime = currentTime;
            var bulletObject = Instantiate(bullet, transform.position + Vector3.right, Quaternion.identity);
            shootSound.Play();
        }        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Player");
        if ((collision.tag == "Enemy" || collision.tag == "EnemyBullet") && invincibleTime <= 0 && StaticData.life != 0)
        {
            OnBeHit();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay Player");
        if ((collision.tag == "Enemy" || collision.tag == "EnemyBullet") && invincibleTime <= 0 && StaticData.life != 0)
        {
            OnBeHit();
        }
    }

    void OnBeHit()
    {
        Debug.Log("beHit");
        EventManager.Instance.OnPlayerBeHit?.Invoke();
        if (StaticData.life == 0)
        {
            normalSprite.SetActive(false);
            destroyEffect.SetActive(true);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            invincibleTime = 2f;
            animator.SetBool("InvincibleMode", true);
            hitSound.Play();
            
        }
    }

}
