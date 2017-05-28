using UnityEngine;


public class AudioManager : MonoBehaviour {

    public GameObject audioObject;

    private static GameObject _AudioObject;


    void Start()
    {
        _AudioObject = audioObject;
    }

    public static void PlaySound(Vector3 _position, AudioClip _audioClip, float _volume = 1.0f, bool _is2D = false, int _priority = 128)
    {
        AudioObject _audioObject = ((GameObject)Instantiate(_AudioObject, _position, Quaternion.identity)).GetComponent<AudioObject>();
        _audioObject.Play(_audioClip, _volume, _is2D, _priority);
    }

    public static void PlaySound(Transform _parent, AudioClip _audioClip, float _volume = 1.0f, bool _is2D = false, int _priority = 128)
    {
        AudioObject _audioObject = ((GameObject)Instantiate(_AudioObject, _parent)).GetComponent<AudioObject>();
        _audioObject.Play(_audioClip, _volume, _is2D, _priority);
    }
}
