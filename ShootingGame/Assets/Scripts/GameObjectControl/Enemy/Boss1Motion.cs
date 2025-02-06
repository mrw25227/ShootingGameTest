using Unity.VisualScripting;
using UnityEngine;

public class Boss1Motion : EnemyMotion
{

    public override void MoveAction()
    {
        Debug.Log("override MoveAction");
    }

    public override void ShootAtion()
    {
        Debug.Log("override ShootAtion");
    }
}
