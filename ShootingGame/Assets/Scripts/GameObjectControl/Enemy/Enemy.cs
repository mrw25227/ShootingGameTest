using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected int life = 10;

    [SerializeField]
    public UnityEvent motionEvent;

    [SerializeField]
    int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        motionEvent?.Invoke();
    }

    
}
