using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCanvas : MonoBehaviour {

    public Text speedText;
    public Button normalButton;
    public Button fastButton;

    private void Start()
    {
        normalButton.interactable = false;
    }

    public void NormalTime()
    {
        Time.timeScale = 1;
        speedText.text = "Speed: normal";
        normalButton.interactable = false;
        fastButton.interactable = true;
    }

    public void FasterTime()
    {
        Time.timeScale = 2;
        speedText.text = "Speed: x2";
        fastButton.interactable = false;
        normalButton.interactable = true;
    }
}
