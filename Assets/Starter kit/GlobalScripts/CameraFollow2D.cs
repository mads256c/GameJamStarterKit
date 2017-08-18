using UnityEngine;

namespace GameJamStarterKit
{
    #region Inspector code
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(CameraFollow2D))]
    [CanEditMultipleObjects]
    public class CameraFollow2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var Target = serializedObject.FindProperty("Target");
            var Offset = serializedObject.FindProperty("Offset");
            var Lerp = serializedObject.FindProperty("Lerp");
            var UseCameraBounds = serializedObject.FindProperty("UseCameraBounds");
            var CameraBounds = serializedObject.FindProperty("CameraBounds");

            EditorGUILayout.PropertyField(Target);
            EditorGUILayout.PropertyField(Offset);
            EditorGUILayout.PropertyField(Lerp);

            EditorGUILayout.PropertyField(UseCameraBounds);
            if (UseCameraBounds.boolValue)
                EditorGUILayout.PropertyField(CameraBounds);

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
#endregion

    public class CameraFollow2D : MonoBehaviour
    {
        [Tooltip("The object the camera should follow.")]
        public GameObject Target;

        [Tooltip("The offset the camera should be following with.")]
        public Vector3 Offset = new Vector3(0, 0, -10);

        [Tooltip("How smooth the motion should be.")]
        public float Lerp = 0.5f;

        [Header("Use camera bounds?")]
        [Tooltip("Should the camera be limited by a rectangle.")]
        public bool UseCameraBounds = false;

        [Tooltip("The BoxCollider2D that sets the bounds.")]
        public BoxCollider2D CameraBounds;


        private new Camera camera;

        private float height, width;

        private float minX, maxX, minY, maxY;

        // Calculate the values needed to do bounds checking
        void Start()
        {
            if (UseCameraBounds)
            {
                camera = GetComponent<Camera>();
                if (!camera.orthographic)
                    Debug.LogError("Camera has to be orthographic, to use bounds");


                height = camera.orthographicSize;
                width = height * camera.aspect;

                minX = (width - (CameraBounds.size.x * CameraBounds.transform.lossyScale.x / 2.0f)) + CameraBounds.transform.position.x;
                maxX = ((CameraBounds.size.x * CameraBounds.transform.lossyScale.x / 2.0f) - width) + CameraBounds.transform.position.x;
                minY = (height - (CameraBounds.size.y * CameraBounds.transform.lossyScale.y / 2.0f)) + CameraBounds.transform.position.y;
                maxY = ((CameraBounds.size.y * CameraBounds.transform.lossyScale.y / 2.0f) - height) + CameraBounds.transform.position.y;
            }
        }

        // LateUpdate is called after Update each frame
        void LateUpdate()
        {
            if (UseCameraBounds)
            {
                Vector3 pos = Target.transform.position;
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);

                transform.position = Vector3.Lerp(transform.position, pos + Offset, Lerp);
            }
            else
            {
                // Set the position of the camera's transform to be the same as the player's, but offset.
                transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Lerp);
            }

        }
    }
}