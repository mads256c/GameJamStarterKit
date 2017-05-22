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
    public class IdleState : IEnemyState
    {
        private Enemy enemy;

        private float idleTimer;
        private float idleDuration = 5f;



        public void Enter(Enemy enemy)
        {
            this.enemy = enemy;
            idleTimer = 0;
            idleDuration = UnityEngine.Random.Range(1f, 10f);
        }

        public void Execute()
        {
            Idle();

            if (enemy.target != null)
            {
                if (enemy.inMeleeRange)
                    enemy.ChangeState(new MeleeState());
                else
                    enemy.ChangeState(new RangedState());
            }
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter(Collider2D o)
        {

        }

        private void Idle()
        {
            enemy.Anim.SetFloat("speed", 0);

            idleTimer += Time.deltaTime;


            if (idleTimer > idleDuration)
            {
                enemy.Flip();
                enemy.ChangeState(new PatrolState());
            }
        }
    }
}
