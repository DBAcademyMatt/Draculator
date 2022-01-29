using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ElementImage;

    public static UIController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void SetElementImage(BaseElement element)
    {
        ElementImage.color = element.ColourRange.Evaluate(Random.value);
    }
}
