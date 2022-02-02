using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Solid", menuName = "SandSim/Elements/Create New Dynamic Solid")]
public class DynamicSolid : Solid
{
    public bool Resting;
    public bool FreeFalling;

    public override void OnCreate()
    {
        FreeFalling = true;
        Resting = false;
        //Velocity = Random.insideUnitCircle * 5;
        base.OnCreate();
    }

    public override bool Tick(Vector2Int Position)
    {
        if (AlreadyUpdated)
            return true;

        bool updated = false;
        if (!Resting)
        {
            updated = true;

            Velocity += Map.Instance.Gravity;// * Time.fixedDeltaTime;
            //#TODO make this 3D
            Velocity.x *= (1 - LinearDrag);

            Vector2Int VelInt = new Vector2Int(Mathf.RoundToInt(Velocity.x), Mathf.RoundToInt(Velocity.y));
            Vector2Int[] move = Utils.PointsOnLine(Position, Position + VelInt);
            Vector2Int lastPos = move[0];

            if (move.Length == 2 
                && move[1] == Position + Vector2Int.down
                && Map.Instance.IsSolid(move[1]))
            {
                FreeFalling = false;
                Velocity = Vector3.zero;
                Resting = true;
            }
            else
            {
                FreeFalling = true;
                for (int i = 1; i < move.GetLength(0); i++)
                {
                    if (!ActOnElement(lastPos, move[i]))
                        break;
                    else
                        lastPos = move[i];
                }
            }
           // Debug.Log("Velocity : " + Velocity);
        }
      
        AlreadyUpdated = true;

        return updated;
    }

    bool ActOnElement(Vector2Int thisPos, Vector2Int theirPos)
    {
        //activate any neighbours if not resting
        if (!Resting)
            ActivateNeighbours(thisPos);

        if (thisPos == theirPos)
            return true;

        BaseElement target = Map.Instance.GetElementAt(theirPos);
        bool insideMap = Map.Instance.IsWithinBounds(theirPos);

        if (!insideMap)
        {
            HitSolid(target);
            return false;
        }
        else
        {
            if (target == null)
            {
                Map.Instance.SetElementAt(theirPos, this);
                Map.Instance.SetElementAt(thisPos, target);
            }
            else 
            if (target.GetType() == typeof(Liquid) ||
                        target.GetType() == typeof(Gas))
            {
                Map.Instance.SetElementAt(theirPos, this);
                Map.Instance.SetElementAt(thisPos, target);
            }
            else if (target.GetType() == typeof(DynamicSolid)
                || target.GetType() == typeof(StaticSolid))
            {
                HitSolid(target);
                return false;
            }
        }

        return true;

        //if (Velocity.magnitude < 1)
        //    Resting = true;
    }

    void HitSolid(BaseElement hit)
    {
        //  Debug.Log("Doof " + Velocity);

        //Vector3 bounceNormal = Quaternion.AngleAxis(Random.Range(-45f,45f), Vector3.forward) * -Map.Instance.Gravity;
        //Vector3 reflect = Vector3.Reflect(Velocity.normalized, bounceNormal.normalized);
        //Velocity = reflect * Velocity.magnitude * Bounciness;

        bool dontBounce = false;
        if (hit != null && hit.GetType() == typeof(DynamicSolid))
        {
            if ((hit as DynamicSolid).FreeFalling)
            {
                dontBounce = true;
            }
        }

        if (!dontBounce)
        {
            //convent the current velocity into a tangent using direction as the normal and apply to a side.
            //Vector3 crossSide = Vector3.forward;
            //if (Random.Range(0, 2) == 0)
            //    crossSide = Vector3.back;
            //Vector3 cross = Vector3.Cross(Velocity.normalized, crossSide);
            //Velocity = cross * Bounciness ;

            Vector3 bounceNormal = Quaternion.AngleAxis(Random.Range(-45f, 45f), Vector3.forward) * -Map.Instance.Gravity;
            Vector3 reflect = Vector3.Reflect(Velocity.normalized, bounceNormal.normalized);
            Velocity = reflect * Bounciness * Velocity.magnitude;

            if (hit != null && hit.GetType() == typeof(DynamicSolid))
            {
                (hit as DynamicSolid).FreeFalling = true;
                hit.Velocity = -Velocity;
            }

            // bounce force
            //float bounce = Bounciness * Random.value;// * (Mass / hit.Mass);

            //Debug.Log("Cross : " + cross );
            
        }
        //apply our velocity to other dynamic solids
       // if (hit != null && hit.GetType() == typeof(DynamicSolid))
       // {
            // float bounce = Bounciness * (Mass / hit.Mass);
            // float otherBounce = hit.Bounciness * (hit.Mass / Mass);
           //hit.Velocity = Velocity;// * hit.Bounciness;// * bounce;
           // Velocity *= bounce;
      //  }

   //     FreeFalling = false;
     //   Debug.LogError("End Vel : " + Velocity);
    }

    void ActivateNeighbours(Vector2Int Position)
    { 
        Vector2Int[] neighbours = new Vector2Int[4]
        {
            Position + Vector2Int.up,
            Position + Vector2Int.left,
            Position + Vector2Int.right,
            Position + Vector2Int.down,
        };

        for (int i = 0; i < neighbours.Length; i++)
        {
            BaseElement element = Map.Instance.GetElementAt(neighbours[i]);
            if (element != null && element.GetType() == typeof(DynamicSolid))
            {
                (element as DynamicSolid).Resting = false;
            }
        }
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
