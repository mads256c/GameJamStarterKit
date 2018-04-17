using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [AddComponentMenu("TopDown2D/EnemyBase")]
    [RequireComponent(typeof(AILerp), typeof(Collider2D))]
    public class EnemyBase : MonoBehaviour
    {
        public int Health = 100;
        public float Speed = 1;

        // Use this for initialization
        void Start()
        {
            GetComponent<AILerp>().speed = Speed;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 1)
                Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
