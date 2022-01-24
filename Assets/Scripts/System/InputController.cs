using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GetComponent<GameController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            gc.EC.ElementIndex++;
            if (gc.EC.ElementIndex > gc.EC.Elements.Count)
                gc.EC.ElementIndex = 0;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            gc.EC.ElementIndex--;
            if (gc.EC.ElementIndex < 0)
                gc.EC.ElementIndex = gc.EC.Elements.Count;
        }

    }
}
