using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [AddComponentMenu("TopDown2D/Bullet")]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {

        [Tooltip("How fast should the bullet be?")]
        public float Speed = 20f;
        [Tooltip("How long the bullet should be in the world before it disappears.")]
        public float TimeToLive = 10f;
        [Tooltip("What should the bullet damage?")]
        public LayerMask DamageLayer = -1;
        [Tooltip("How much damage should this bullet inflict?")]
        public int Damage = 100;

        private new Rigidbody2D rigidbody;

        // Use this for initialization
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            Destroy(gameObject, TimeToLive);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.right * Speed * Time.deltaTime;
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