using UnityEngine;
using GameJamStarterKit;

namespace TestScripts
{
    public class AudioTest : MonoBehaviour
    {
        public AudioClip audioClip;

        private void Update()
        {
            if (Input.anyKeyDown)
            AudioManager.PlaySound(new Vector3(0, 0, 0), audioClip);
        }
    }
}
