using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FullScreenSettings : MonoBehaviour
{
    private class FullScreenType
    {
        public FullScreenMode mode;
        public string name;

        public FullScreenType(FullScreenMode mode, string name)
        {
            this.mode = mode;
            this.name = name;
        }
    }

    private FullScreenType[] types =
    {
        new FullScreenType(FullScreenMode.ExclusiveFullScreen, "FullScreen"),
        new FullScreenType(FullScreenMode.FullScreenWindow, "Borderless"),
        new FullScreenType(FullScreenMode.Windowed, "Windowed")
    };

    public GameObject previousButton;
    public GameObject nextButton;
    public TextMeshProUGUI textValue;

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            previousButton.SetActive(index > 0);
            nextButton.SetActive(index < types.Length - 1);
            ChangeFullScreenMode();
            textValue.text = types[index].name;
        }
    }
    private int index;

    private void ChangeFullScreenMode()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, types[Index].mode);
    }

    public void IncrementIndex() { Index++; }

    public void DecrementIndex() { Index--; }

    private void Awake()
    {
        Index = Array.FindIndex(types, type => type.mode == Screen.fullScreenMode);
    }
}
