using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    private bool isMoveing;

    private Vector2 moveInput;

    private void Awake()
    {
        EventManager.Instance.OnControllerClick += Move;
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnControllerClick -= Move;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z")) {
            
        }
        
    }

    void Move(Vector2 input)
    {
        var targetPos = transform.position;
        targetPos.x += input.x * moveSpeed * Time.deltaTime;
        targetPos.y += input.y * moveSpeed * Time.deltaTime;
        transform.position = targetPos;
    }

}
