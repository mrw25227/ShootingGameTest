using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 mouseMoveInput;
    private bool controllerIsDrag = false;
    private float controllerBoundRadius = 1.8f;

    [SerializeField]
    Transform controlBtn;
    Vector2 BtnInitPos, BtnInitPosMax, BtnInitPosMin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BtnInitPos = controlBtn.position;
        BtnInitPosMax.x = controlBtn.position.x + controllerBoundRadius;
        BtnInitPosMin.x = controlBtn.position.x - controllerBoundRadius;
        BtnInitPosMax.y = controlBtn.position.y + controllerBoundRadius;
        BtnInitPosMin.y = controlBtn.position.y - controllerBoundRadius;
        Debug.Log(BtnInitPosMax + "  " +  BtnInitPosMin);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && controllerIsDrag) {
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
            /*if (BtnInitPosMax.x < pos.x)
            {
                mouseMoveInput.x = BtnInitPosMax.x;
            }
            else if (BtnInitPosMin.x > pos.x)
            {
                mouseMoveInput.x = BtnInitPosMin.x;
            }
            else
            {
                mouseMoveInput.x = pos.x;
            }
            if (BtnInitPosMax.y < pos.y)
            {
                mouseMoveInput.y = BtnInitPosMax.y;
            }
            else if (BtnInitPosMin.y > pos.y)
            {
                mouseMoveInput.y = BtnInitPosMin.y;
            }
            else
            {
                mouseMoveInput.y = pos.y;
            }*/
            Debug.Log(mouseMoveInput);
            

        } //«ö¤U·Æ¹«¥ªÁä
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
    }

   


    public void OnPointerUp(BaseEventData eventData)
    {
        Debug.Log("OnContollerBtnUp");
        controllerIsDrag = false;
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        
        Debug.Log("OnContollerBtnDown");
        controllerIsDrag = true;
    }


}
