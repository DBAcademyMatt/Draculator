using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public List<BaseElement> Elements = new List<BaseElement>();

    public int ElementIndex = 0;

    private void Awake()
    {
        CreateElements();
    }

    void CreateElements()
    {
        Elements.Add(new StaticSolid("Stone", Color.grey));
    }
}
