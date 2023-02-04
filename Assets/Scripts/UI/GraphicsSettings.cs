using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    private class GraphicsIndex
    {
        public int index;
        public string name;

        public GraphicsIndex(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }

    private GraphicsIndex[] graphics =
    {
        new GraphicsIndex(0, "Very Low"),
        new GraphicsIndex(1, "Low"),
        new GraphicsIndex(2, "Medium"),
        new GraphicsIndex(3, "High"),
        new GraphicsIndex(4, "Very High"),
        new GraphicsIndex(5, "Ultra"),
    };

    public GameObject previousButton;
    public GameObject nextButton;
    public TextMeshProUGUI textValue;

    private int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            previousButton.SetActive(index > 0);
            nextButton.SetActive(index < graphics.Length - 1);
            ChangeGraphics();
            textValue.text = graphics[index].name;
        }
    }
    private int index;

    private void ChangeGraphics()
    {
        QualitySettings.SetQualityLevel(Index);
    }

    public void IncrementIndex() { Index++; }

    public void DecrementIndex() { Index--; }

    private void Awake()
    {
        Index = QualitySettings.GetQualityLevel();
    }
}
