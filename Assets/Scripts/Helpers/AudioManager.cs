using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _timeToFade;

    [SerializeField] private AudioSource _chillLoop;
    [SerializeField] private AudioSource _medLoop;
    [SerializeField] private AudioSource _intenseLoop;
    [SerializeField] private PlayerStats _stats;


    private AudioSource _currentlyPlaying;
    private int _currentIntensity;

    private void Awake()
    {
        _medLoop.volume = 0;
        _intenseLoop.volume = 0;

        _currentlyPlaying = _chillLoop;
        StartCoroutine(IntroFade()); // If we get a call before this finishes there's a bug but I really don't think it'll happen
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

    public void PlayerHealthIntensityUpdate()
    {
        if (_currentIntensity >= 2)
            return;

        if(_stats.CurrentHealth / _stats.MaxHealth < 0.3f )
        {
            SetIntensity(2);
        }
        else if (_stats.CurrentHealth / _stats.MaxHealth < 0.7f)
        {
            SetIntensity(1);
        }

    }

    private IEnumerator IntroFade()
    {
        float timeElapsed = 0f;

        while (_currentlyPlaying.volume < 1)
        {
            _currentlyPlaying.volume = Mathf.Lerp(0, 1, timeElapsed / 2f);

            timeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FadeTransition(AudioSource source)
    {
        float timeElapsed = 0f;
        float startVol = _currentlyPlaying.volume;

        while (_currentlyPlaying.volume > 0)
        {
            _currentlyPlaying.volume = Mathf.Lerp(startVol, 0, timeElapsed / _timeToFade);
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
