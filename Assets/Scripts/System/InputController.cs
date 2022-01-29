using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gc;

    Vector2Int lastInputPos;

    int Radius = 2;

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

            UIController.Instance.SetElementImage(ElementController.Instance.CurrentElement);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            gc.EC.ElementIndex--;
            if (gc.EC.ElementIndex < 0)
                gc.EC.ElementIndex = gc.EC.Elements.Elements.Count - 1;

            UIController.Instance.SetElementImage(ElementController.Instance.CurrentElement);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            //get world position of click to the pixel.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// / Camera.main.orthographicSize / 2;// Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos *= GameData.ChunkSize;
            Vector2Int pos = new Vector2Int((int)mousePos.x, (int)mousePos.y);

            Vector2Int[] points = Utils.PointsInCircle(pos.x, pos.y, 3);// Radius);
            for (int i = 0; i < points.Length; i++)
            {
                if (Map.Instance.GetElementAt(points[i]) == null)
                {
                    BaseElement element = Instantiate(ElementController.Instance.CurrentElement);
                    element.OnCreate();
                    Map.Instance.SetElementAt(points[i], element);
                }
            }
        }

        if (Input.GetMouseButton(2))
        {
            //drag
        }
    }
}
