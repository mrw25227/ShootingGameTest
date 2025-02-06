using UnityEngine;


public class EnemyMotion: MonoBehaviour
{
    public int life = 1;
    private bool isDeath = false;

    public MoveType moveType = MoveType.MOTIONLESS;
    public float moveAngle = 0;
    public float moveSpeed = 5;

    public GameObject[] bullet;

    public Vector2 bornPos;

    private float moveBack = 1;

    private void Update()
    {
        if (!isDeath)
        {

            MoveAction();
            ShootAtion();
        }
    }

    public void HittedAction(int attack)
    {
        if (isDeath) return;
        life -= attack;
        if (life <= 0) 
        {
            isDeath = true;
            DestroyAction();
        }
    }

    public virtual void DestroyAction()
    {
        Destroy(gameObject, 3);
    }
    public virtual void StartAction()
    {

    }
    public virtual void MoveAction()
    {
        switch (moveType)
        {
            case MoveType.MOTIONLESS:
                break;
            case MoveType.STRAIGHT:
                MoveStraight();
                break;
            case MoveType.MOVE_ON_2_POINT:
                MoveOn2point();
                break;
        }
    }
    void MoveStraight()
    {
        var pos = transform.position;
        var radians = moveAngle * Mathf.Deg2Rad;
        pos.x += Mathf.Cos(radians) * moveSpeed * Time.deltaTime;
        pos.y += Mathf.Sin(radians) * moveSpeed * Time.deltaTime;


        transform.position = pos;
    }

    void MoveOn2point()
    {

        var pos = transform.position;
        var radians = moveAngle * Mathf.Deg2Rad;
        pos.x += moveBack * Mathf.Cos(radians) * moveSpeed * Time.deltaTime;
        pos.y += moveBack * Mathf.Sin(radians) * moveSpeed * Time.deltaTime;
        if (pos.x > StaticData.fieldBoundX || pos.x < -StaticData.fieldBoundX || pos.y > StaticData.fieldBoundY || pos.y < -StaticData.fieldBoundY)
        {
            moveBack *= -1;
            Debug.Log("MoveOn2point MoveBack: " + moveBack + " ori: " + transform.position + " pos: "  + pos);
        }
        else
        {
            transform.position = pos;
        }
        
    }

    public virtual void ShootAtion()
    {
        //Debug.Log("ShootAtion");
    }


    [System.Serializable]
    public enum MoveType
    {
        MOTIONLESS,
        STRAIGHT,
        MOVE_ON_2_POINT,
        OTHER
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter ");
        if (other.tag == "Bullet")
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if(bullet != null)
            {
                HittedAction(bullet.getAttack());
                Debug.Log("get attack life: " + life);
            }
            
        }
    }
}
