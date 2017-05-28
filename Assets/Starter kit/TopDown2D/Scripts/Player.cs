using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamStarterKit.Constants.Enums;

namespace GameJamStarterKit.TopDown2D
{

    public class Player : Character
    {
        public override bool IsDead
        {
            get
            {
                return health <= 0;
            }
        }

        public PlayerID playerId;

        public int maxHealth;

        private int health;

        public override IEnumerator TakeDamage(int amount)
        {
            health -= amount;

            if (playerId == PlayerID.One)
            {
                GameObject.Find("cam1").GetComponentInChildren<BloodEffect>().BloodAndShake();
            }

            if (playerId == PlayerID.Two)
            {
                GameObject.Find("cam2").GetComponentInChildren<BloodEffect>().BloodAndShake();
            }

            if (playerId == PlayerID.Three)
            {
                GameObject.Find("cam3").GetComponentInChildren<BloodEffect>().BloodAndShake();
            }

            if (playerId == PlayerID.Four)
            {
                GameObject.Find("cam4").GetComponentInChildren<BloodEffect>().BloodAndShake();
            }



            yield return null;
        }

        // Use this for initialization
        void Start()
        {
            health = maxHealth;
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
