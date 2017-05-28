using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {

        [Tooltip("How fast should the bullet be?")]
        public float Speed = 20f;
        [Tooltip("How long the bullet should be in the world before it disappears.")]
        public float TimeToLife = 10f;
        [Tooltip("What should the bullet damage?")]
        public LayerMask DamageLayer;
        [Tooltip("How much damage should this bullet inflict?")]
        public float Damage = 100f;

        private new Rigidbody2D rigidbody;

        // Use this for initialization
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody.velocity = transform.right * Speed * Time.deltaTime;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == DamageLayer)
            {
                //Take damage
            }
        }
    }
}