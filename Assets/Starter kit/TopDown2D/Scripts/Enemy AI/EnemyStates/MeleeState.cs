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

namespace GameJamStarterKit.TopDown2D
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
            enemy.agent.isStopped = true;
        }

        public void Execute()
        {

            Attack();

            if (enemy.target == null || !enemy.inMeleeRange)
            {
                enemy.ChangeState(new HuntState());
            }

            enemy.LookAt(enemy.target.transform);
        }

        public void Exit()
        {
            enemy.agent.isStopped = false;
        }

        public void OnTriggerEnter(Collider o)
        {
        }

        private void Attack()
        {
            attackTimer += Time.deltaTime;
            enemy.canMove = true;

            if (attackTimer > attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }

            if (canAttack)
            {
                Debug.Log("just attacked.");
                canAttack = enemy.canMove = false;
                /*
                enemy.Anim.SetTrigger("attack");
                enemy.Anim.SetInteger("speed", 0);
                */
                enemy.agent.isStopped = true;
                enemy.isAttacking = true;
            }

        }
    }
}
