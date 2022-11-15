using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthBar : MonoBehaviour
{
    UnityEngine.UI.Image _image;
    [SerializeField] private PlayerStats _stats;

    private void Awake()
    {
        _image = this.GetComponent<UnityEngine.UI.Image>();
    }

    private void Start()
    {
        UpdateFill();
    }

    public void UpdateFill()
    {
        _image.fillAmount = _stats.CurrentHealth / _stats.MaxHealth;
    }
}
