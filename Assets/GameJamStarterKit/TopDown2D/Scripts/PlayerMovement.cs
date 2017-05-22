using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit.TopDown2D
{


//If we are in unity editor. Add the ability to edit the acceleration curve and display values more nicely.
#if UNITY_EDITOR
    using UnityEditor;


    /// <summary>
    /// It is in this class we set which variables we show in inspector. It is the only way to display the AnimationCurve
    /// </summary>
    [CustomEditor(typeof(PlayerMovement))]
    [CanEditMultipleObjects]
    public class PlayerMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update(); //Needed for some types.

            //Get all the variables that should be displayed in PlayerMovement
            SerializedProperty CanMove = serializedObject.FindProperty("CanMove");
            SerializedProperty Speed = serializedObject.FindProperty("Speed");
            SerializedProperty AccelerationSpeed = serializedObject.FindProperty("AccelerationSpeed");
            SerializedProperty AccelerationCurve = serializedObject.FindProperty("AccelerationCurve");
            SerializedProperty MoveInputAxisX = serializedObject.FindProperty("MoveInputAxisX");
            SerializedProperty MoveInputAxisY = serializedObject.FindProperty("MoveInputAxisY");

            SerializedProperty CanRotate = serializedObject.FindProperty("CanRotate");
            SerializedProperty UseMouse = serializedObject.FindProperty("UseMouse");
            SerializedProperty PlayerCamera = serializedObject.FindProperty("PlayerCamera");
            SerializedProperty RotationInputAxisX = serializedObject.FindProperty("RotationInputAxisX");
            SerializedProperty RotationInputAxisY = serializedObject.FindProperty("RotationInputAxisY");

            EditorGUILayout.PropertyField(CanMove);

            if (CanMove.boolValue)
            {
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(AccelerationSpeed);
                EditorGUILayout.PropertyField(AccelerationCurve);
                EditorGUILayout.PropertyField(MoveInputAxisX);
                EditorGUILayout.PropertyField(MoveInputAxisY);
            }

            EditorGUILayout.PropertyField(CanRotate);

            if (CanRotate.boolValue)
            {
                EditorGUILayout.PropertyField(UseMouse);
                if (UseMouse.boolValue)
                {
                    EditorGUILayout.PropertyField(PlayerCamera);
                }
                else
                {
                    EditorGUILayout.PropertyField(RotationInputAxisX);
                    EditorGUILayout.PropertyField(RotationInputAxisY);
                }
            }


            serializedObject.ApplyModifiedProperties(); //Apply changes to variables from inspector.
        }
    }

#endif


    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove = true;
        public float Speed = 10f;
        public float AccelerationSpeed = 1f;
        public AnimationCurve AccelerationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f)); //Used for accelerating the player. Could be used creativly.
        public string MoveInputAxisX = "Horizontal";
        public string MoveInputAxisY = "Vertical";

        public bool CanRotate = true;
        public bool UseMouse = true;
        public Camera PlayerCamera;
        public string RotationInputAxisX = "Horizontal";
        public string RotationInputAxisY = "Vertical";



        private float moveTimer = 0f; //For how long have we moved?

        // Use this for initialization
        void Start()
        {
            AccelerationCurve.preWrapMode = WrapMode.Clamp;
            AccelerationCurve.postWrapMode = WrapMode.Clamp;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanMove)
                if (Input.GetAxisRaw(MoveInputAxisX) != 0 | Input.GetAxisRaw(MoveInputAxisY) != 0)
                {
                    Vector3 moveDir = new Vector3(Input.GetAxisRaw(MoveInputAxisX), Input.GetAxisRaw(MoveInputAxisY), 0f);
                    moveDir.Normalize();

                    transform.position += moveDir * Speed * AccelerationCurve.Evaluate(moveTimer) * Time.deltaTime;
                    moveTimer += Time.deltaTime * AccelerationSpeed;
                }
                else
                {
                    moveTimer = 0;
                }
            if (CanRotate)
            {
                float angle = 0;
                if (UseMouse)
                {
                    Vector3 cursorInWorldPos = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);
                    angle = Mathf.Rad2Deg * Mathf.Atan2(cursorInWorldPos.y - transform.position.y, cursorInWorldPos.x - transform.position.x);

                }
                else
                {
                    angle = Mathf.Rad2Deg * Mathf.Atan2(Input.GetAxisRaw(RotationInputAxisY), Input.GetAxisRaw(RotationInputAxisX));
                }
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}