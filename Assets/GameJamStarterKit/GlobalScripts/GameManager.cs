using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool isPaused
    {
        get
        {
            return ispaused;
        }
        set
        {
            ispaused = value;

            OnPause();
        }
    }

    private static bool ispaused = false;

    private static void OnPause()
    {
        Debug.Log("isPaused is now: " + ispaused);
    }

}
