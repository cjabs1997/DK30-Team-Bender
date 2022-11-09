using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    private CountdownTimer timer;
    private Slider slider;
    private Text text;

    void Start()
    {
        this.slider = this.GetComponent<Slider>();
        this.timer = this.GetComponent<CountdownTimer>();
        this.text = this.GetComponentInChildren<Text>();
        this.slider.maxValue = this.timer.CountdownLength;
        this.text.text = this.timer.CountdownLength.ToString("0");;
    }

    void FixedUpdate()
    {
        this.slider.value = this.timer.CountdownLength - this.timer.SecondsElapsed;
        this.text.text = (this.timer.CountdownLength - this.timer.SecondsElapsed).ToString("0");
    }

}
