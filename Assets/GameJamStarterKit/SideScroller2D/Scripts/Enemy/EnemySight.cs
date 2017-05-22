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
    public class EnemySight : MonoBehaviour
    {

        [SerializeField]
        private Enemy enemy;

        void OnTriggerEnter2D(Collider2D o)
        {
            if (o.tag == "Player")
            {
                enemy.target = o.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D o)
        {
            enemy.target = null;
        }
    }
}
