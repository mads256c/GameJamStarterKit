using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WaveSystem : MonoBehaviour {

    [Tooltip("How should the wave be stated as comeplete")]
    public ClearType clearType;

    public float[] time;

    public int[] EnemyCount;

    public List<WaveManager> waveManager = new List<WaveManager>();

    public static int currentEnemies;


    public enum ClearType
    {
        time,
        enemyCount
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class WaveManager
{
    GameObject[] enemy;
    public int spawnsAfter;
}
