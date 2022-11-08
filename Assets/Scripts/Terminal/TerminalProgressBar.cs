using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalProgressBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image fillImage;
    [SerializeField] private UnityEngine.UI.Image maskImage;

    [SerializeField] Color startingColor;
    [SerializeField] Color filledColor;


    public void UpdateFill(float progress)
    {
        progress = Mathf.Min(progress, 1); // I think this isn't needed but just in case to prevent weirdness
        fillImage.color = Color.Lerp(startingColor, filledColor, progress);
        maskImage.fillAmount = progress;
    }
}
