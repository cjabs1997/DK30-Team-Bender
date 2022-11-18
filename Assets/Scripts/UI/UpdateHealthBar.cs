using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthBar : MonoBehaviour
{
    // Ryan made me make this gross :(
    UnityEngine.UI.Image _image;
    [SerializeField] private PlayerStats _stats;
    float maxFill;

    private void Awake()
    {
        _image = this.GetComponent<UnityEngine.UI.Image>();
        maxFill = _stats.CurrentHealth;
    }

    private void Start()
    {
        _image.fillAmount = 1;
    }

    public void UpdateFill()
    {
        _image.fillAmount = _stats.CurrentHealth / maxFill;
    }
}
