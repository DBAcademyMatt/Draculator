using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Solid", menuName = "SandSim/Elements/Create New Dynamic Solid")]
public class DynamicSolid : Solid
{
    public override Vector2Int Step(Vector2Int Position)
    {
        // check for something underneath...
        if (Map.Instance.GetElementAt(Position + Vector2Int.down) == null)
        {
            Velocity.y -= GameData.Gravity * Time.fixedDeltaTime;
        }
        else
        {
            //check either side and down 1
            if (Map.Instance.GetElementAt(Position + Vector2Int.down + Vector2Int.left) == null)
            {
                Velocity.y -= GameData.Gravity * Time.fixedDeltaTime;
            }
            else if (Map.Instance.GetElementAt(Position + Vector2Int.down + Vector2Int.right) == null)
            {
                Velocity.y -= GameData.Gravity * Time.fixedDeltaTime;
            }
        }

        // check along the velocity
        Vector2Int VelInt = new Vector2Int(Mathf.RoundToInt(Velocity.x), Mathf.RoundToInt(Velocity.y));
        Vector2Int[] move = Utils.PointsOnLine(Position, Position + VelInt);
        Vector2Int lastPos = move[0]; // or Position

        for (int i = 1; i < move.GetLength(0); i++)
        {
            if (Map.Instance.GetElementAt(move[i]) == null)
            {
                lastPos = move[i];
            }
            else
            {
                Velocity.y = 0;
                break;
            }
        }
        return lastPos;
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
