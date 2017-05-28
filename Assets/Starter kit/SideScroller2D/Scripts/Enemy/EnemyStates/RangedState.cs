/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;


namespace GameJamStarterKit.Sidescroller
{


    public class RangedState : IEnemyState
    {

        private Enemy enemy;

        private float throwTimer = 2.5f, throwCooldown = 5f;

        private bool canThrow = false;

        public void Enter(Enemy enemy)
        {
            this.enemy = enemy;

            throwCooldown = enemy.rangedCooldown;
        }

        public void Execute()
        {
            if (enemy.rangedWeapon != null)
                ThrowObject();

            if (enemy.inMeleeRange)
            {
                enemy.ChangeState(new MeleeState());
            }
            else
            {
                if (enemy.target != null)
                {
                    enemy.Move();
                }
                else
                {
                    enemy.ChangeState(new IdleState());
                }
            }

        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider2D o)
        {

        }

        private void ThrowObject()
        {
            throwTimer += Time.deltaTime;

            if (throwTimer > throwCooldown)
            {
                canThrow = true;
                throwTimer = 0;
            }

            if (canThrow)
            {
                canThrow = false;
                enemy.Anim.SetTrigger("throw");
                GameObject g = enemy.ThrowObject(enemy.rangedWeapon);

                g.GetComponent<Bullet>().dir = enemy.dir;
            }

        }
    }
}
