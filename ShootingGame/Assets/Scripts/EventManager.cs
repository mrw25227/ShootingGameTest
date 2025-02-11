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

    public delegate void OnNormalGameEvent();
    public event OnNormalGameEvent OnShootingClick;
    public void OnShootingClickInvoke()
    {
        OnShootingClick?.Invoke();
    }

    public event OnNormalGameEvent OnBossDeath;
    public void OnBossDeathInvoke()
    {
        OnBossDeath?.Invoke();
    }

    public event OnNormalGameEvent OnPlayerBeHit;
    public void OnPlayerBeHitInvoke() 
    {
        OnPlayerBeHit?.Invoke();
    }

    public delegate void OnGetScoreEvent(int value);
    public event OnGetScoreEvent OnGetScore;
    public void OnGetScoreInvoke(int value)
    {
        OnGetScore?.Invoke(value);
    }
}
