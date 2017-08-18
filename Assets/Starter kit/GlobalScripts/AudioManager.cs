using UnityEngine;

namespace GameJamStarterKit
{
    public static class AudioManager
    {
        /// <summary>
        /// Plays a sound at a position.
        /// </summary>
        /// <param name="position">The position the sound should play at.</param>
        /// <param name="audioClip">The sound that should play.</param>
        /// <param name="volume">How load the sound is.</param>
        /// <param name="is2D">Global or local sound.</param>
        /// <param name="priority">The sounds priority.</param>
        public static void PlaySound(Vector3 position, AudioClip audioClip, float volume = 1.0f, bool is2D = false, int priority = 128)
        {
            GameObject audioObject = new GameObject("AudioObject", typeof(AudioSource), typeof(AudioObject));
            audioObject.transform.position = position;
            audioObject.GetComponent<AudioObject>().Play(audioClip, volume, is2D, priority);

        }
        /// <summary>
        /// Plays a sound that follows parent.
        /// </summary>
        /// <param name="parent">The Transform the sound should follow.</param>
        /// <param name="audioClip">The sound that should play.</param>
        /// <param name="volume">How load the sound is.</param>
        /// <param name="is2D">Global or local sound.</param>
        /// <param name="priority">The sounds priority.</param>
        public static void PlaySound(Transform parent, AudioClip audioClip, float volume = 1.0f, bool is2D = false, int priority = 128)
        {
            GameObject audioObject = new GameObject("AudioObject", typeof(AudioSource), typeof(AudioObject));
            audioObject.transform.SetParent(parent);
            audioObject.GetComponent<AudioObject>().Play(audioClip, volume, is2D, priority);

        }
    }
}