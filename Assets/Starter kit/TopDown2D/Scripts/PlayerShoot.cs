using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [System.Serializable]
    public class Weapon
    {
        [Tooltip("The damage of the projectile or hitscan")]
        public float Damage = 100f;
        [Tooltip("Use a projectile or use hitscan")]
        public bool UseProjectile = true;
        [Tooltip("The projectile that gets fired")]
        public GameObject BulletProjectile;
        [Tooltip("The speed the projectile is fired at")]
        public float ProjectileSpeed = 10f;



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

        }
    }
}