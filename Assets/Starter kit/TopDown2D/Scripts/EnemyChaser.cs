using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamStarterKit.TopDown2D;

public class EnemyChaser : EnemyBase {

    public GameObject Body;

    public override void Die()
    {
        Instantiate(Body, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
