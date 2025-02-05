using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 moveInput;
    private Vector2 mouseMoveInput;
    private bool controllerIsDrag = false;
    private bool shootingBtnIsDown = false;
    private float controllerBoundRadius = 1.2f;
    private int controlFingerId = -1;

    [SerializeField]
    Transform controlBtn;
    Vector2 BtnInitPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BtnInitPos = controlBtn.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && controllerIsDrag) //«ö¤U·Æ¹«¥ªÁä
        {
            int i = 0;
            bool findFinger = false;
            Touch touch;
            for (; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                Debug.LogWarning("finger index : " + i + " id: " + touch.fingerId);
                if (touch.fingerId == controlFingerId)
                {                    
                    findFinger = true;
                    break;
                }
            }
            if (!findFinger)
            {
                Debug.LogWarning("controlFingerId is fail");
            }
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dist = Vector2.Distance(pos, BtnInitPos);
            if (dist > controllerBoundRadius)
            {
                mouseMoveInput.x = BtnInitPos.x + (pos.x - BtnInitPos.x) * (controllerBoundRadius / dist);
                mouseMoveInput.y = BtnInitPos.y + (pos.y - BtnInitPos.y) * (controllerBoundRadius / dist);
                controlBtn.position = mouseMoveInput;
            }
            else
            {
                mouseMoveInput.x = pos.x;
                mouseMoveInput.y = pos.y;
                controlBtn.position = mouseMoveInput;
            }
            moveInput = (mouseMoveInput - BtnInitPos) / controllerBoundRadius;
            //Debug.Log(moveInput);
            EventManager.Instance.OnControllerClickInvoke(moveInput);

        } 
        else
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            if (moveInput != Vector2.zero)
            {
                //Debug.Log("X: " + moveInput.x + ", Y: " + moveInput.y);
                EventManager.Instance.OnControllerClickInvoke(moveInput);
            }
        }

        if (Input.GetKey("z") || shootingBtnIsDown)
        {
            EventManager.Instance.OnShootingClickInvoke();
        }
    }
       

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogWarning("OnContollerBtnDown eventData.pointerId: " + eventData.pointerId);
        controlFingerId = eventData.pointerId;
#if UNITY_ANDROID && !UNITY_EDITOR
            controllerIsDrag = true;
#else
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            controllerIsDrag = true;
        }
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogWarning("OnContollerBtnUp eventData.pointerId: " + eventData.pointerId);
#if UNITY_ANDROID && !UNITY_EDITOR
        controllerIsDrag = false;
        controlBtn.position = BtnInitPos;
#else
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            controllerIsDrag = false;
        }
        controlBtn.position = BtnInitPos;
#endif
    }

    public void OnShootingBtnDown(BaseEventData eventData)
    {
        shootingBtnIsDown = true;
    }
    public void OnShootingBtnUp(BaseEventData eventData) 
    {
        shootingBtnIsDown =false;
    }
}
