using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Solid", menuName = "SandSim/Elements/Create New Dynamic Solid")]
public class DynamicSolid : Solid
{
    public float Friction;
    public float LinearDrag;
    public float Bounciness;

    float LinearBounce = 0.25f;

    bool FreeFalling; // bool flag to handle free falling blocks different from physically simulated blocks.

    public override bool Step(Vector2Int Position)
    {
        bool updatedThisFrame = false;

        

        // is there anything immediately below us and we are not free falling?
        Vector2Int oneDown = Position + Vector2Int.down;
        Vector2Int oneDownLeft = oneDown + Vector2Int.left;
        Vector2Int oneDownRight = oneDown + Vector2Int.right;

        if (FreeFalling)
        {
            updatedThisFrame = true;
            Velocity.y -= GameData.Gravity * Time.deltaTime;
        }
        else
        {
            Vector2Int[] tests = new Vector2Int[3];
            tests[0] = oneDown;
            if (Random.Range(0, 2) == 0)
            {
                tests[1] = oneDownLeft;
                tests[2] = oneDownRight;
            }
            else
            {
                tests[1] = oneDownRight;
                tests[2] = oneDownLeft;
            }

            for (int i = 0; i < tests.Length; i++)
            {
                if (Map.Instance.IsWithinBounds(tests[i]))
                {
                    BaseElement element = Map.Instance.GetElementAt(tests[i]);
                    if (element == null)
                    {
                        updatedThisFrame = true;
                        FreeFalling = true;
                        Velocity.y = -1;
                        Velocity.x = (tests[i].x - Position.x) * LinearBounce;
                        break;
                    }
                }
            }
        }

        // check along the velocity
        Vector2Int VelInt = new Vector2Int(Mathf.RoundToInt(Velocity.x), Mathf.RoundToInt(Velocity.y));
        Vector2Int[] move = Utils.PointsOnLine(Position, Position + VelInt);
        Vector2Int lastPos = move[0]; // or Position

        for (int i = 1; i < move.GetLength(0); i++)
        {
            if (Map.Instance.IsWithinBounds(move[i]))
            {
                //updatedThisFrame = true;
                FreeFalling = false;

                BaseElement element = Map.Instance.GetElementAt(move[i]);
                if (element != null)
                {
                    if (element.GetType() == typeof(Liquid) || element.GetType() == typeof(Gas))
                    {  
                        //swap places.
                        Map.Instance.SetElementAt(move[i], this);
                        Map.Instance.SetElementAt(lastPos, element);
                    }
                    else
                    {
                        //stop here.
                        Map.Instance.SetElementAt(lastPos, this);
                        Velocity = Vector2Int.zero;
                        break;
                    }
                }
                else
                {
                    Map.Instance.SetElementAt(lastPos, null);
                    Map.Instance.SetElementAt(move[i], this);
                    lastPos = move[i];
                }
            }
        }

        Velocity.x *= LinearDrag;

        if (!updatedThisFrame)
            Velocity = Vector2.zero;

        return updatedThisFrame;
    }

    public override Color GetColor()
    {

        return Color;
    }

    public override void Die()
    {
        // do whatever happens when this dies.
    }

    public override void DieAndReplace(BaseElement element)
    {
        throw new System.NotImplementedException();
    }
}
