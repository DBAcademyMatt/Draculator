using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public InputController Input;
    [HideInInspector]
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
        GameObject mapObj = new GameObject("Map");
        Map = mapObj.AddComponent<Map>();
    }
}
