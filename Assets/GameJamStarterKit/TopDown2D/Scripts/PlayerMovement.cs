using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(PlayerMovement))]
    [CanEditMultipleObjects]
    public class PlayerMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty CanMove = serializedObject.FindProperty("CanMove");
            SerializedProperty Speed = serializedObject.FindProperty("Speed");
            SerializedProperty AccelerationSpeed = serializedObject.FindProperty("AccelerationSpeed");
            SerializedProperty AccelerationCurve = serializedObject.FindProperty("AccelerationCurve");
            SerializedProperty MoveInputAxisX = serializedObject.FindProperty("MoveInputAxisX");
            SerializedProperty MoveInputAxisY = serializedObject.FindProperty("MoveInputAxisY");

            SerializedProperty CanRotate = serializedObject.FindProperty("CanRotate");


            EditorGUILayout.PropertyField(CanMove);

            if (CanMove.boolValue)
            {
                EditorGUILayout.PropertyField(MoveInputAxisX);
                EditorGUILayout.PropertyField(MoveInputAxisY);
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(AccelerationSpeed);
                EditorGUILayout.PropertyField(AccelerationCurve);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }

#endif


    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove = true;
        public float Speed = 10f;
        public float AccelerationSpeed = 1f;
        public AnimationCurve AccelerationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f));
        public string MoveInputAxisX = "Horizontal";
        public string MoveInputAxisY = "Vertical";

        public bool CanRotate = true;
        public bool UseMouse = true;
        public string RotationInputAxisX = "Horizontal";
        public string RotationInputAxisY = "Vertical";


        // Use this for initialization
        void Start()
        {
            AccelerationCurve.preWrapMode = WrapMode.Clamp;
            AccelerationCurve.postWrapMode = WrapMode.Clamp;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}