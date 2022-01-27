using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Solid", menuName = "SandSim/Elements/Create New Dynamic Solid")]
public class DynamicSolid : Solid
{
    bool FreeFalling; // bool flag to handle free falling blocks different from physically simulated blocks.

    public override void OnCreate()
    {
        FreeFalling = true;
        Velocity = Random.insideUnitCircle * 3;
        base.OnCreate();
    }

    public override bool Step(Vector2Int Position)
    {
        Velocity += Map.Instance.Gravity;// * Time.deltaTime;

        Vector2Int VelInt = new Vector2Int(Mathf.RoundToInt(Velocity.x), Mathf.RoundToInt(Velocity.y));
        Vector2Int[] move = Utils.PointsOnLine(Position, Position + VelInt);
        Vector2Int lastPos = move[0];

        bool updated = false;
        for (int i = 0; i < move.GetLength(0); i++)
        {
            if (ActOnElement(lastPos, move[i]))
            {
                lastPos = move[i];
                updated = true;
            }
            //else
            //    FreeFalling = false;
        }

        //ApplyHeatToSurroundings();

        return updated;
    }

    bool ActOnElement(Vector2Int thisPos, Vector2Int theirPos)
    {
        //falling, but velocity is not great enough to increment a block 
        //sampling our current position
        if (thisPos == theirPos && FreeFalling)
            return true;

        BaseElement target = Map.Instance.GetElementAt(theirPos);
        bool insideMap = Map.Instance.IsWithinBounds(theirPos);

        //Element at the test position can be passed through - Empty, Liquid or Gas
        if (insideMap)
        {
            if (target == null)
            {
                Map.Instance.SetElementAt(theirPos, this);
                Map.Instance.SetElementAt(thisPos, target);
                return true;
            }
            else
            {
                if (insideMap && (target.GetType() == typeof(Gas)
                    || target.GetType() == typeof(Liquid)))
                {
                    Map.Instance.SetElementAt(theirPos, this);
                    Map.Instance.SetElementAt(thisPos, target);
                    return true;
                }
                else if (target.GetType() == typeof(Solid))
                {
                    if (FreeFalling)
                    {
                        //convent the current velocity into a tangent using direction as the normal and apply to a side.
                        Vector3 crossSide = Vector3.forward;
                        if (Random.Range(0, 2) == 0)
                            crossSide = Vector3.back;
                        Vector3 cross = Vector3.Cross(Velocity.normalized, crossSide);
                        Velocity = cross * Velocity.magnitude;

                        //apply our velocity to other dynamic solids
                        if (target != null && target.GetType() == typeof(DynamicSolid))
                            target.Velocity = Velocity;
                    }

                    return true;
                }

                Velocity *= Friction * target.Friction;
            }
        }

        FreeFalling = false;

        return false;
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
