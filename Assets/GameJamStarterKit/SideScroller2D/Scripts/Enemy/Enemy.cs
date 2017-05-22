/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace GameJamStarterKit.Sidescroller
{
    public class Enemy : Character
    {


        private IEnemyState currentState;

        public GameObject target { get; set; }

        public GameObject rangedWeapon;

        [SerializeField]
        private float meleeRange = 6, throwRange = 12;

        public float meleeCooldown, rangedCooldown;

        public AudioClip[] swings;

        public AudioClip die, walk;

        public GameObject walkPlayer, swingPlayer;

        public EventTrigger.TriggerEvent onDie;
        bool ivoked = false;

        public bool inMeleeRange
        {
            get
            {
                if (meleeRange == 0)
                {
                    return false;
                }

                if (target != null)
                {
                    return Vector2.Distance(transform.position, target.transform.position) <= meleeRange;
                }

                return false;
            }
        }

        public bool inThrowRange
        {
            get
            {
                if (throwRange == 0)
                {
                    return false;
                }

                if (target != null)
                {
                    return Vector2.Distance(transform.position, target.transform.position) <= throwRange;
                }

                return false;
            }
        }


        public override bool IsDead
        {
            get
            {
                if (health <= 0 && !ivoked)
                {
                    BaseEventData data = new BaseEventData(EventSystem.current);
                    onDie.Invoke(data);
                    ivoked = true;
                }

                return health <= 0;
            }
        }

        public override void Start()
        {
            base.Start();

            int r = UnityEngine.Random.Range(0, 100);

            if (r > 50)
            {
                Flip();
            }

            r = UnityEngine.Random.Range(0, 100);

            if (r > 50)
            {
                ChangeState(new IdleState());
            }
            else
            {
                ChangeState(new PatrolState());
            }

            throwPos = transform;
        }

        void Update()
        {

            if (!IsDead)
            {
                if (!takingDamage)
                {
                    currentState.Execute();
                }

                LookAtTarget();
            }
            else
            {
                if (swordCollider != null)
                    swordCollider.enabled = false;
            }


            if (Anim.GetFloat("speed") == 1)
            {
                if (walkPlayer.GetComponent<AudioSource>().clip == walk)
                {
                    if (!walkPlayer.GetComponent<AudioSource>().isPlaying)
                        walkPlayer.GetComponent<AudioSource>().Play();
                }
                else
                {
                    walkPlayer.GetComponent<AudioSource>().clip = walk;
                    walkPlayer.GetComponent<AudioSource>().Play();
                }
            }
        }

        private void LookAtTarget()
        {
            if (target != null)
            {
                float xDir = target.transform.position.x - transform.position.x;

                if (xDir > 0 && facingRight || xDir < 0 && !facingRight)
                {
                    Flip();
                }

            }
        }

        public void ChangeState(IEnemyState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter(this);
        }

        public void Move()
        {
            if (!attack)
            {
                Anim.SetFloat("speed", 1);

                transform.Translate(Vector2.right * (currentSpeed * Time.deltaTime));
            }

        }


        public override void OnTriggerEnter2D(Collider2D o)
        {
            base.OnTriggerEnter2D(o);
            currentState.OnTriggerEnter(o);
        }

        public override IEnumerator TakeDamage()
        {
            if (!IsDead)
            {
                health -= 10;
                if (IsDead)
                {
                    Anim.SetTrigger("death");
                    walkPlayer.GetComponent<AudioSource>().clip = die;
                    walkPlayer.GetComponent<AudioSource>().Play();
                }
                else
                {
                    Anim.SetTrigger("damage");

                }

            }
            yield return null;
        }

        public void SpawnItem(GameObject item)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}

