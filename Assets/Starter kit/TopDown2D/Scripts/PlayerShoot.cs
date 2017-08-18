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
        [Tooltip("The time between you can shoot again")]
        public float CooldownTime = 0.2f;
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
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerShoot : MonoBehaviour
    {
        public Weapon[] Weapons;

        private int currentWeapon = 0;

        private LineRenderer lineRenderer;

        private float timer = 0f;

        // Use this for initialization
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.positionCount = 2;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (Input.GetButton("Fire1"))
                ShootWeapon();
        }

        public void ShootWeapon()
        {
            ShootWeapon(currentWeapon);
        }

        public void ShootWeapon(int index)
        {
            Weapon w = Weapons[index];
            if (timer >= w.CooldownTime)
            {
                timer = 0f;
                if (w.UseProjectile)
                {
                    GameObject g = Instantiate(w.BulletProjectile, w.GunTip.position, w.GunTip.rotation);
                    Bullet b = g.GetComponent<Bullet>();
                    b.Damage = w.Damage;
                    b.Speed = w.ProjectileSpeed;
                    b.TimeToLive = w.ProjectileTimeToLive;
                }
                else
                {
                    RaycastHit2D r = Physics2D.Raycast(w.GunTip.position, w.GunTip.right, w.HitscanDistance, w.DamageLayer);
                    if (r)
                    {
                        if (r.collider != null)
                        {
                            if (r.collider.GetComponent<EnemyBase>() != null)
                            {
                                r.collider.GetComponent<EnemyBase>().TakeDamage(w.Damage);
                            }
                        }
                        lineRenderer.SetPosition(0, w.GunTip.position);
                        lineRenderer.SetPosition(1, r.point);
                        lineRenderer.enabled = true;
                        StartCoroutine(LineRendererEffect(w.CooldownTime / 2f));
                    }
                }
            }
        }

        IEnumerator LineRendererEffect(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            lineRenderer.enabled = false;
            
        }

    }
}