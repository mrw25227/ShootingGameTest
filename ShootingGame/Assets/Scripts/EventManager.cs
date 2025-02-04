using UnityEngine;

public class EventManager
{
    static EventManager instance;
    public static EventManager Instance {
        get {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance; 
        } 
    }
    public delegate void OnControllerClickEvent(Vector2 input);
    public event OnControllerClickEvent OnControllerClick;
    public void OnControllerClickInvoke(Vector2 input)
    {
        OnControllerClick?.Invoke(input);
    }
}
