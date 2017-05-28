using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{
    [System.Serializable]
    public class Weapon
    {
        public bool UseProjectile = true;
        public GameObject BulletProjectile;
        public float Damage = 100f;

    }


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