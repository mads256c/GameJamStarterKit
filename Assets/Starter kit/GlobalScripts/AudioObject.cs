using UnityEngine;

public class AudioObject : MonoBehaviour {


    private AudioSource _audioSource;
    
    private void _initObject()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip _audioClip, float _volume = 1.0f, bool _is2D = false, int _priority = 128)
    {
        _initObject();

        if (_audioClip != null)
        {
            _audioSource.clip = _audioClip;
            _audioSource.volume = Mathf.Clamp01(_volume);
            _audioSource.priority = Mathf.Clamp(_priority, 0, 256);
            _audioSource.spatialBlend = System.Convert.ToInt32(_is2D);
            _audioSource.Play();
            Destroy(gameObject, _audioClip.length);
            return;
        }
        Destroy(gameObject);

    }
}
