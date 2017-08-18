using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [AddComponentMenu("TopDown2D/Bullet")]
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {

        [Tooltip("How fast should the bullet be?")]
        public float Speed = 20f;
        [Tooltip("How long the bullet should be in the world before it disappears.")]
        public float TimeToLive = 10f;
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
            rigidbody.position += (Vector2)transform.right * Speed * Time.deltaTime;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<EnemyBase>() != null)
            {
                collision.gameObject.GetComponent<EnemyBase>().TakeDamage(Damage);
            }
        }
    }
}