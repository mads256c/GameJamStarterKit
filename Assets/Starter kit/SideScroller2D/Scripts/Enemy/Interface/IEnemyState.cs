/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/

using UnityEngine;

namespace GameJamStarterKit.Sidescroller
{
    public interface IEnemyState
    {
        void Execute();
        void Enter(Enemy enemy);
        void Exit();
        void OnTriggerEnter(Collider2D o);
    }
}
