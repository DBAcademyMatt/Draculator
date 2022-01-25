using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gc;

    Vector2Int lastInputPos;

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
            if (gc.EC.ElementIndex >= gc.EC.Elements.Elements.Count)
                gc.EC.ElementIndex = 0;

            Debug.Log("Element Index : " + ElementController.Instance.ElementIndex);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            gc.EC.ElementIndex--;
            if (gc.EC.ElementIndex < 0)
                gc.EC.ElementIndex = gc.EC.Elements.Elements.Count - 1;

            Debug.Log("Element Index : " + ElementController.Instance.ElementIndex);
        }

        if (Input.GetMouseButton(0))
        {
            //get world position of click to the pixel.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// / Camera.main.orthographicSize / 2;// Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos *= GameData.ChunkSize;
            Vector2Int pos = new Vector2Int((int)mousePos.x, (int)mousePos.y);
            //Vector2Int worldPos = new Vector2Int((int)Camera.main.transform.position.x, (int)Camera.main.transform.position.y) + pos;

            Debug.Log("World Pos : " + pos + "     mouse Pos : " + mousePos + "    Input Pos: " + Input.mousePosition);
            BaseElement element = Instantiate(ElementController.Instance.CurrentElement);
            element.OnCreate();
            Map.Instance.SetElementAt(pos, element);
        }

        if (Input.GetMouseButton(2))
        {
            //drag
        }
    }
}
