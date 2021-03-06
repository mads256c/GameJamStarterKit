﻿using UnityEngine;
using Prime31;
using TeamUtility.IO;

namespace GameJamStarterKit.TopDown2D
{

    #region Inspector Code
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
            SerializedProperty playerID = serializedObject.FindProperty("playerID");

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

            SerializedProperty HasAnimation = serializedObject.FindProperty("HasAnimation");
            SerializedProperty AnimationBool = serializedObject.FindProperty("AnimationBool");

            EditorGUILayout.PropertyField(playerID);
            EditorGUILayout.PropertyField(CanMove);

            if (CanMove.boolValue) //Only show variables related to move if the CanMove variable is true
            {
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(AccelerationSpeed);
                EditorGUILayout.PropertyField(AccelerationCurve);
                EditorGUILayout.PropertyField(MoveInputAxisX);
                EditorGUILayout.PropertyField(MoveInputAxisY);
            }

            EditorGUILayout.PropertyField(CanRotate);

            if (CanRotate.boolValue) //Only show variable related to rotation if CanRotate is true
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

            EditorGUILayout.PropertyField(HasAnimation);
            if (HasAnimation.boolValue)
            {
                EditorGUILayout.PropertyField(AnimationBool);
            }


            serializedObject.ApplyModifiedProperties(); //Apply changes to variables from inspector.
        }
    }

#endif
    #endregion

    [AddComponentMenu("TopDown2D/PlayerMovement")]
    [RequireComponent(typeof(Rigidbody2D), typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("What player this script should respond to")]
        public PlayerID playerID = PlayerID.One;

        [Header("Movement:")]
        [Tooltip("Can the player move?")]
        public bool CanMove = true;
        [Tooltip("How fast should the player move")]
        public float Speed = 10f;
        [Tooltip("How fast we go through the Acceleration Curve")]
        public float AccelerationSpeed = 1f;
        [Tooltip("How the player should Accelerate")]
        public AnimationCurve AccelerationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f)); //Used for accelerating the player. Could be used creativly.
        [Tooltip("The axis that move the player left and right")]
        public string MoveInputAxisX = "Horizontal";
        [Tooltip("The axis that move the player up and down")]
        public string MoveInputAxisY = "Vertical";

        [Header("Rotation:")]
        [Tooltip("Can the player rotate?")]
        public bool CanRotate = true;
        [Tooltip("Should the player be controlled with the mouse?")]
        public bool UseMouse = true;
        [Tooltip("The camera you can see the player in")]
        public Camera PlayerCamera;
        public string RotationInputAxisX = "Horizontal";
        public string RotationInputAxisY = "Vertical";

        [Header("Animation:")]
        [Tooltip("Do your player have an animation?")]
        public bool HasAnimation = false;
        [Tooltip("The animation bool to set")]
        public string AnimationBool = "IsMoving";


        

        private float moveTimer = 0f; //For how long have we moved?
        private Animator animator;
        private CharacterController2D controller;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController2D>();
            AccelerationCurve.preWrapMode = WrapMode.Clamp;
            AccelerationCurve.postWrapMode = WrapMode.Clamp;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanMove)
                if (InputManager.GetAxisRaw(MoveInputAxisX, playerID) != 0 | InputManager.GetAxisRaw(MoveInputAxisY, playerID) != 0)
                {
                    Vector2 moveDir = new Vector3(InputManager.GetAxisRaw(MoveInputAxisX, playerID), InputManager.GetAxisRaw(MoveInputAxisY, playerID));
                    if (moveDir.magnitude > 1f) //We have to check if the magnitude of the vector is higher than 1, because of JoyStick analog input. If the player only want to move half as fast this is requied.
                        moveDir.Normalize();


                    controller.move((Vector3)moveDir * Speed * Mathf.Clamp01(AccelerationCurve.Evaluate(moveTimer)) * Time.deltaTime);
                    moveTimer += Time.deltaTime * AccelerationSpeed;

                    if (HasAnimation)
                    animator.SetBool(AnimationBool, true);
                }
                else
                {
                    moveTimer = 0;

                    if (HasAnimation)
                    animator.SetBool(AnimationBool, false);
                }

            if (CanRotate)
            {
                float angle = 0;
                if (UseMouse)
                {
                    Vector3 cursorInWorldPos = PlayerCamera.ScreenToWorldPoint(InputManager.mousePosition);
                    angle = Mathf.Rad2Deg * Mathf.Atan2(cursorInWorldPos.y - transform.position.y, cursorInWorldPos.x - transform.position.x);

                }
                else
                {
                    if (InputManager.GetAxisRaw(RotationInputAxisY) == 0 && InputManager.GetAxisRaw(RotationInputAxisX) == 0)
                        return;
                    angle = Mathf.Rad2Deg * Mathf.Atan2(InputManager.GetAxisRaw(RotationInputAxisY), InputManager.GetAxisRaw(RotationInputAxisX));
                }
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}