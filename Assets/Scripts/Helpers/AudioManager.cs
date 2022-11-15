using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _timeToFade;

    [SerializeField] private AudioSource _chillLoop;
    [SerializeField] private AudioSource _medLoop;
    [SerializeField] private AudioSource _intenseLoop;

    private AudioSource _currentlyPlaying;
    private int _currentIntensity;

    private void Awake()
    {
        _chillLoop.volume = 1;
        _medLoop.volume = 0;
        _intenseLoop.volume = 0;

        _currentlyPlaying = _chillLoop;
        _currentIntensity = 0;
    }

    public void ChangeIntensity(int change)
    {
        if (_currentIntensity + change > 2 || _currentIntensity + change < 0)
            return;

        _currentIntensity = Mathf.Max(0, Mathf.Min(2, _currentIntensity + change)); // just in case

        TransitionTo(GetSourceFromIntensity(_currentIntensity));
    }

    public void SetIntensity(int value)
    {
        if (value > 2 || value < 0)
            return;

        _currentIntensity = Mathf.Max(0, Mathf.Min(2, value)); // just in case

        TransitionTo(GetSourceFromIntensity(_currentIntensity));
    }

    public void TransitionTo(AudioSource source)
    {
        if (_currentlyPlaying != source)
        {
            StartCoroutine(FadeTransition(source));
        }

        return;
    }

    private IEnumerator FadeTransition(AudioSource source)
    {
        float timeElapsed = 0f;

        while(_currentlyPlaying.volume > 0)
        {
            _currentlyPlaying.volume = Mathf.Lerp(1, 0, timeElapsed / _timeToFade);
            source.volume = Mathf.Lerp(0, 1, timeElapsed / _timeToFade);

            timeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _currentlyPlaying = source;
    }

    private AudioSource GetSourceFromIntensity(int intensity) // OMG THIS IS SO BAD BUT IT'S THE LAST WEEK OF THE JAM SUE ME!!!
    {
        if(intensity == 0)
        {
            return _chillLoop;
        }
        else if (intensity == 1)
        {
            return _medLoop;
        }
        else if (intensity == 2)
        {
            return _intenseLoop;
        }
        else
        {
            Debug.LogError("SOMETHING WENT TERRIBLY WRONG");
            return null;
        }
    }
}
