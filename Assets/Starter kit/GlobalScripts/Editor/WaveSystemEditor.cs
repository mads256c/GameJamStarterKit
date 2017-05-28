using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If we are in unity editor. Add the ability to edit the acceleration curve and display values more nicely.
#if UNITY_EDITOR
using UnityEditor;


/// <summary>
/// It is in this class we set which variables we show in inspector. It is the only way to display the AnimationCurve
/// </summary>
[CustomEditor(typeof(WaveSystem))]
[CanEditMultipleObjects]
public class WaveSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); //Needed for some types.

        //Get all the variables that should be displayed in PlayerMovement
        SerializedProperty clearType = serializedObject.FindProperty("clearType");
        SerializedProperty manager = serializedObject.FindProperty("waveManager");

        SerializedProperty time = serializedObject.FindProperty("time");
        SerializedProperty enemies = serializedObject.FindProperty("EnemyCount");

        EditorGUILayout.PropertyField(clearType);

        if (clearType.enumNames[clearType.enumValueIndex].ToString() == "time")
        {
            EditorGUILayout.PropertyField(time, true);
        }
        else
        {
            EditorGUILayout.PropertyField(enemies, true);
        }

        EditorGUILayout.PropertyField(manager, true);
        serializedObject.ApplyModifiedProperties(); //Apply changes to variables from inspector.

    }
}

#endif
