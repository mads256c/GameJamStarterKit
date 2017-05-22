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

namespace GameJamStarterKit.Sidescroller
{
    public abstract class Character : MonoBehaviour
    {

        [SerializeField]
        public int health = 100;

        public abstract bool IsDead { get; }

        [SerializeField]
        private List<string> damageSources;

        public Animator Anim { get; set; }

        [SerializeField]
        protected Transform throwPos;

        [SerializeField]
        protected Collider2D swordCollider;


        [SerializeField]
        protected float walkSpeed = 2, runSpeed = 4;

        protected float currentSpeed;

        protected bool facingRight;

        public int dir { get; set; }
        public int l_dir { get; set; }

        public bool attack { get; set; }

        public bool takingDamage { get; set; }

        public virtual void Start()
        {
            Anim = GetComponent<Animator>();
            facingRight = true;
            currentSpeed = walkSpeed;

            if (throwPos == null)
            {
                throwPos = transform;
            }

        }

        public virtual GameObject ThrowObject(GameObject obj)
        {
            GameObject r_obj = Instantiate(obj, throwPos.position, Quaternion.identity) as GameObject;

            r_obj.GetComponent<Bullet>().parent = gameObject;

            return r_obj;
        }

        public void Flip()
        {

            Debug.Log(facingRight);
            facingRight = !facingRight;
            Debug.Log(facingRight);

            if (facingRight)
                transform.eulerAngles = new Vector2(0, 0);

            else if (!facingRight)
                transform.eulerAngles = new Vector2(0, 180);

        }

        public void IsRunning(bool ir)
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

        public abstract IEnumerator TakeDamage();

        public void MeleeAttack()
        {

            swordCollider.enabled = !swordCollider.enabled;

        }

        public virtual void OnTriggerEnter2D(Collider2D c)
        {
            if (damageSources.Contains(c.tag))
            {
                StartCoroutine(TakeDamage());

                if (c.tag == "axe" || c.tag == "enemyAxe")
                {
                    Destroy(c.gameObject, 0.2f);
                }
            }
        }

    }
}
