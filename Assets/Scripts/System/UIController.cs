using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ElementImage;

    public void SetElementImage(BaseElement element)
    {
        ElementImage.color = element.GetColor();
    }
}
