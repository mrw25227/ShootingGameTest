using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 moveInput;
    private bool controllerIsDrag = false;
    private bool shootingBtnIsDown = false;
    private float controllerBoundRadius = 1.2f;
    private int controlFingerId = -1;
    private Vector3 screenMoveInput = Vector3.zero;

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
        if (controllerIsDrag && screenMoveInput != Vector3.zero) //拖曳畫面操控方向
        {
            DragMoveControl(screenMoveInput);

        } 
        else //鍵盤操控方向
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
#if !UNITY_ANDROID || UNITY_EDITOR
        if (eventData.button == PointerEventData.InputButton.Left)
        {
#endif
            controllerIsDrag = true;
            screenMoveInput = Camera.main.ScreenToWorldPoint(eventData.position);
#if !UNITY_ANDROID || UNITY_EDITOR
        }
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogWarning("OnContollerBtnUp eventData.pointerId: " + eventData.pointerId);
#if !UNITY_ANDROID || UNITY_EDITOR
        if (eventData.button == PointerEventData.InputButton.Left)
        {
#endif
            controllerIsDrag = false;
            controlBtn.position = BtnInitPos;
            screenMoveInput = Vector3.zero;

#if !UNITY_ANDROID || UNITY_EDITOR
        }
#endif
    }
    public void OnDrag(PointerEventData eventData)
    {
        screenMoveInput = Camera.main.ScreenToWorldPoint(eventData.position);        
    }

    private void DragMoveControl(Vector3 pos)
    {
        float dist = Vector2.Distance(pos, BtnInitPos);
        Vector2 InputTemp = Vector2.zero;
        if (dist > controllerBoundRadius)
        {
            InputTemp.x = BtnInitPos.x + (pos.x - BtnInitPos.x) * (controllerBoundRadius / dist);
            InputTemp.y = BtnInitPos.y + (pos.y - BtnInitPos.y) * (controllerBoundRadius / dist);            
        }
        else
        {
            InputTemp.x = pos.x;
            InputTemp.y = pos.y;
        }
        controlBtn.position = InputTemp;
        moveInput = (InputTemp - BtnInitPos) / controllerBoundRadius;
        EventManager.Instance.OnControllerClickInvoke(moveInput);
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
