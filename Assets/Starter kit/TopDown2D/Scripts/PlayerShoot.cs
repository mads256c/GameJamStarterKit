using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [System.Serializable]
    public class Weapon
    {
        [Tooltip("The tip of the gun")]
        public Transform GunTip;
        [Tooltip("The damage of the projectile or hitscan")]
        public int Damage = 100;
        [Tooltip("Use a projectile or use hitscan")]
        public bool UseProjectile = true;
        [Tooltip("The projectile that gets fired")]
        public GameObject BulletProjectile;
        [Tooltip("The speed the projectile is fired at")]
        public float ProjectileSpeed = 10f;
        [Tooltip("The amount of time the projectile is existing, before it gets removed")]
        public float ProjectileTimeToLive = 10f;
        [Tooltip("Hitscan distance")]
        public float HitscanDistance = 100f;
        [Tooltip("What the shot should damage")]
        public LayerMask DamageLayer = -1;


    }

    
    [AddComponentMenu("TopDown2D/PlayerShoot")]
    public class PlayerShoot : MonoBehaviour
    {
        public Weapon[] Weapons;

        private int currentWeapon = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Jump"))
                ShootWeapon();
        }

        public void ShootWeapon()
        {
            ShootWeapon(currentWeapon);
        }

        public void ShootWeapon(int index)
        {
            Weapon w = Weapons[index];
            if (w.UseProjectile)
            {
                GameObject g = Instantiate(w.BulletProjectile, w.GunTip.position, w.GunTip.rotation);
                Bullet b = g.GetComponent<Bullet>();
                b.Damage = w.Damage;
                b.Speed = w.ProjectileSpeed;
                b.DamageLayer = w.DamageLayer;
                b.TimeToLife = w.ProjectileTimeToLive;
            }
            else
            {
                RaycastHit2D r = Physics2D.Raycast(w.GunTip.position, w.GunTip.right, w.HitscanDistance, w.DamageLayer);
                if (r.collider != null)
                {
                    if (r.collider.tag == "Enemy")
                    {
                        r.collider.GetComponent<Enemy>().TakeDamage(w.Damage);
                    }
                }
            }
        }

    }
}