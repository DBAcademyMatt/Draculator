using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public InputController Input;
    public ElementController EC;

    public Map Map;

    private void Awake()
    {
        Input = GetComponent<InputController>();
        EC = GetComponent<ElementController>();
    }

    private void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        Map = new Map();
    }
}
