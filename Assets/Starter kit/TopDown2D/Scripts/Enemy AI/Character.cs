/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameJamStarterKit.TopDown2D
{
    public abstract class Character : MonoBehaviour
    {

        [HideInInspector]
        public GameObject[] players;


        [HideInInspector]
        public GameObject target;

        [SerializeField]
        public int health = 100;

        public abstract bool IsDead { get; }

        [SerializeField]
        private List<string> damageSources;

        public Animator Anim { get; set; }

        [SerializeField]
        protected Collider damageCollider;


        [SerializeField]
        protected float walkSpeed = 2, runSpeed = 4;

        protected float currentSpeed;

        public bool attack { get; set; }

        public bool takingDamage { get; set; }

        public float rotationDamping;

        public virtual void Start()
        {
            Anim = GetComponent<Animator>();
            currentSpeed = walkSpeed;
        }


        public void SetRunning(bool ir)
        {
            if (ir)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }

        public abstract IEnumerator TakeDamage(int amount);

        public void MeleeAttack()
        {

            damageCollider.enabled = !damageCollider.enabled;

        }

        public virtual void OnTriggerEnter(Collider c)
        {

        }

    }
}
