/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System.Collections;

namespace GameJamStarterKit.TopDown2D
{
    public class HuntState : IEnemyState
    {

        private Enemy enemy;

        public void Enter(Enemy enemy)
        {
            this.enemy = enemy;

            //enemy.Anim.SetInteger("speed", 2);

            enemy.canMove = true;

        }

        public void Execute()
        {
            if (enemy.target != null)
            {
                Vector3 pos = new Vector3(enemy.target.transform.position.x - 0.5f, enemy.target.transform.position.y, enemy.target.transform.position.z - 0.5f);
                enemy.MoveToward(pos);
            }

            if (enemy.inMeleeRange)
            {
                //enemy.ChangeState(new MeleeState());
            }
        }

        public void Exit()
        {

        }

        public void OnTriggerEnter(Collider o)
        {

        }

    }
}
