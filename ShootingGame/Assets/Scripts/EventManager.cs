using System;
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
    public Action<Vector2> OnControllerClick;

    public Action OnShootingClick;

    public Action OnBossDeath;

    public Action OnPlayerBeHit;

    public Action<int> OnGetScore;
}
