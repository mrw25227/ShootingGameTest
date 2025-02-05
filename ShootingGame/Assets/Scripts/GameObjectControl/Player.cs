using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lastShootTime = -1000000f;
    public float shootCooldownTimeSec = 0.2f;
    private bool isMoveing;
    private Vector2 moveInput;
    [SerializeField]
    GameObject bullet;

    private void Awake()
    {
        EventManager.Instance.OnControllerClick += Move;
        EventManager.Instance.OnShootingClick += Shoot;
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
        
        
    }

    void Move(Vector2 input)
    {
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
            
        }        
    }

}
