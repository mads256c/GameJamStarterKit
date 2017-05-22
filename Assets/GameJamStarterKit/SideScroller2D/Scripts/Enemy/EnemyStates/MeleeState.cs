/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;
using System;

namespace GameJamStarterKit.Sidescroller
{
    public class MeleeState : IEnemyState
    {

        private Enemy enemy;

        private float attackTimer, attackCooldown = 1f;

        private bool canAttack = true;

        public void Enter(Enemy enemy)
        {
            this.enemy = enemy;
            attackCooldown = enemy.meleeCooldown;
        }

        public void Execute()
        {
            Attack();

            if (enemy.inThrowRange && !enemy.inMeleeRange)
            {
                enemy.ChangeState(new RangedState());
            }
            else if (enemy.target == null)
            {
                enemy.ChangeState(new PatrolState());
            }
        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider2D o)
        {
        }

        private void Attack()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }

            if (canAttack)
            {
                canAttack = false;
                enemy.Anim.SetTrigger("attack");

                enemy.swingPlayer.GetComponent<AudioSource>().clip = enemy.swings[UnityEngine.Random.Range(0, enemy.swings.Length - 1)];
                enemy.swingPlayer.GetComponent<AudioSource>().Play();

            }

        }
    }
}
