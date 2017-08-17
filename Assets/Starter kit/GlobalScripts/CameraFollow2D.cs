using UnityEngine;

namespace GameJamStarterKit
{
    public class CameraFollow2D : MonoBehaviour
    {
        [Tooltip("The object the camera should follow.")]
        public GameObject Target;

        [Tooltip("The offset the camera should be following with.")]
        public Vector3 Offset = new Vector3(0, 0, -10);

        [Tooltip("How smooth the motion should be.")]
        public float Lerp = 0.5f;

        // LateUpdate is called after Update each frame
        void LateUpdate()
        {
            // Set the position of the camera's transform to be the same as the player's, but offset.
            transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Lerp);
        }
    }
}