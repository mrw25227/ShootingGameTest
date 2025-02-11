using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1Motion : EnemyMotion
{
    Tweener objectTweener;

    float cooldown = 1f;
    float lastShootTime = -1000000f;
    int shootMode = 0;
    
    private void Awake()
    {
        isStart = false;
        destroyTime = 2.5f;
    }

    
    public override void StartAction()
    {
        objectTweener = transform.DOMoveX(5, 1.5f);
        objectTweener.OnStepComplete(() => isStart = true);
    }

    public override void ShootAtion()
    {
        float currentTime = Time.time;
        if (currentTime - lastShootTime >= cooldown)
        {
            lastShootTime = currentTime;
            var pos = transform.position;
            GameObject bulletObject;
            switch (shootMode)
            {
                case 0:
                    for (int i = -2; i <= 2; i++)
                    {
                        bulletObject = Instantiate(bullet[0], new Vector2(pos.x - 1.5f, pos.y - i * 0.2f), Quaternion.Euler(new Vector3(0, 0, 180 + i * 30)));
                        bulletObject.GetComponent<Boss1Bulltet>().ChangeAngle(180 + i * 30);
                    }
                    break;
                case 1:
                    for (int i = -2; i <= 2; i++)
                    {
                        bulletObject = Instantiate(bullet[0], new Vector2(pos.x - 1.5f, pos.y - i * 0.3f), Quaternion.identity);
                        bulletObject.GetComponent<Boss1Bulltet>().ChangeSpeed(10);
                    }
                    break;
            }

        }
        
        
    }

    public override void LifeReduceAction()
    {
        if (life < 100)
        {
            shootMode = 1;
            cooldown = 0.5f;
        }
    }

    public override void DestroyAction()
    {
        base.DestroyAction();
        EventManager.Instance.OnBossDeathInvoke();
    }

}
