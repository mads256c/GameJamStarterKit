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
    public class PatrolState : IEnemyState
    {

        private Enemy enemy;

        private float patrolTimer;
        private float patrolDuration = 10f;

        public void Enter(Enemy enemy)
        {
            this.enemy = enemy;
            patrolTimer = 0;

            patrolDuration = Random.Range(2, 20);
        }

        public void Execute()
        {
            Patrol();

            enemy.Move();

            if (enemy.target != null && enemy.inMeleeRange)
            {
                enemy.ChangeState(new MeleeState());
            }

            if (enemy.target != null && enemy.inThrowRange)
            {
                enemy.ChangeState(new RangedState());
            }
        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider2D o)
        {
            if (o.tag == "edge")
            {
                enemy.ChangeState(new IdleState());
            }
        }

        private void Patrol()
        {
            enemy.Anim.SetFloat("speed", 1);

            patrolTimer += Time.deltaTime;

            if (patrolTimer > patrolDuration)
            {
                enemy.ChangeState(new IdleState());
            }
        }
    }
}
