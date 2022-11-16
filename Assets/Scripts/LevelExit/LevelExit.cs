using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private Collider2D _collider2D;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] int _sceneIndexToLoad = 0;
    [SerializeField] private SimpleAudioEvent _closeAudio;

    private void Awake()
    {
        _collider2D = this.GetComponent<Collider2D>();
        _animator = this.GetComponent<Animator>();
        _audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    public void PlayCloseAudio()
    {
        _closeAudio.Play(_audioSource);
    }

    public void ActivateCollider()
    {
        _collider2D.enabled = true;
    }

    public void OpenExit()
    {
        _animator.SetTrigger("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Move to the next level...
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneIndexToLoad);
        }
    }
}
