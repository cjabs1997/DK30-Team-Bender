using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalProgressBar : MonoBehaviour
{
    public UnityEngine.UI.Image fillImage;
    public UnityEngine.UI.Image maskImage;

    [SerializeField] Color startingColor;
    [SerializeField] Color filledColor;

    [Range(0, 1)]
    [SerializeField] private float fillProgress;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.color = Color.Lerp(startingColor, filledColor, fillProgress / 1);
        maskImage.fillAmount = fillProgress / 1;
    }
}
