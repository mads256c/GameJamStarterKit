using UnityEngine;

namespace GameJamStarterKit
{
    public class AudioObject : MonoBehaviour
    {
        public void Play(AudioClip audioClip, float volume = 1.0f, bool is2D = false, int priority = 128)
        {
            AudioSource audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClip;
                audioSource.volume = Mathf.Clamp01(volume);
                audioSource.priority = Mathf.Clamp(priority, 0, 256);
                audioSource.spatialBlend = System.Convert.ToInt32(is2D);
                audioSource.Play();
                Destroy(gameObject, audioClip.length);
                return;
        }
    }
}
