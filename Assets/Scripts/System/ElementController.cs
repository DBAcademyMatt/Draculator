using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public static ElementController Instance;
    public ElementCollection Elements;

    public int ElementIndex = 0;

    public BaseElement CurrentElement { get { return Elements.Elements[ElementIndex]; } }

    private void Awake()
    {
        Instance = this;
    }
}
