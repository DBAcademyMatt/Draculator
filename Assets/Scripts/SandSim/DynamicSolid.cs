using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Solid", menuName = "SandSim/Elements/Create New Dynamic Solid")]
public class DynamicSolid : Solid
{
  //  public bool Resting;
    public bool FreeFalling;

    public override void OnCreate()
    {
        FreeFalling = true;
   //     Resting = false;
        //Velocity = Random.insideUnitCircle * 5;
        base.OnCreate();
    }

    public override bool Tick(Vector2Int Position)
    {
        if (AlreadyUpdated)
            return true;

        Velocity += Map.Instance.Gravity * Time.fixedDeltaTime;
        float drag = LinearDrag;

        //#TODO make this 3D
        Velocity.x *= drag;

        Vector2Int VelInt = new Vector2Int(Mathf.RoundToInt(Velocity.x), Mathf.RoundToInt(Velocity.y));
        Vector2Int[] move = Utils.PointsOnLine(Position, Position + VelInt);
        Vector2Int lastPos = move[0];

        bool updated = false;
        for (int i = 0; i < move.GetLength(0); i++)
        {
                bool blocked = ActOnElement(lastPos, move[i]);
                if (!blocked)
                {
                    updated = true;
                   // Color = Color.blue;
                    lastPos = move[i];
                }
                else// if (Velocity.magnitude < 1f)
                {
                    //Color = Color.red;
                }
        }
      
        AlreadyUpdated = true;

        return updated;
    }

    bool ActOnElement(Vector2Int thisPos, Vector2Int theirPos)
    {
        BaseElement target = Map.Instance.GetElementAt(theirPos);
        bool insideMap = Map.Instance.IsWithinBounds(theirPos);

        //// checking self.
        if (thisPos == theirPos && insideMap && FreeFalling)
            return false;

        if (!insideMap)
        {
            if (FreeFalling)
            {
                Debug.Log("Hit Edge Of Map while freefalling");
                HitSolid(target);
            }
                return true;
        }
        else
        {
            if (target == null)
            {
                Map.Instance.SetElementAt(theirPos, this);
                Map.Instance.SetElementAt(thisPos, target);
                return false;
            }
            else if (target.GetType() == typeof(Liquid) ||
                        target.GetType() == typeof(Gas))
            {
                Map.Instance.SetElementAt(theirPos, this);
                Map.Instance.SetElementAt(thisPos, target);
                return false;
            }
            else if (target.GetType() == typeof(DynamicSolid)
                || target.GetType() == typeof(StaticSolid))
            {
                if (FreeFalling)
                {
                    Debug.Log("Hit Solid while free falling");
                    HitSolid(target);
                }

                return true;
            }

            Debug.LogError("HIT SOMETHING I SHOULDNT " + target);
        }

        Debug.Log("Did Nothing To Element - means a case is missed somewhere..?");

        return false;
    }

    void HitSolid(BaseElement hit)
    {
        Debug.Log("Doof " + Velocity);
        //convent the current velocity into a tangent using direction as the normal and apply to a side.

        Vector3 crossSide = Vector3.forward;
        if (Random.Range(0, 2) == 0)
            crossSide = Vector3.back;
        Vector3 cross = Vector3.Cross(Velocity.normalized, crossSide);

        // bounce force
        float bounce = LinearDrag;// * (Mass / hit.Mass);

        Debug.Log("Cross : " + cross + "      bounce: " + bounce);
        Velocity = cross * Velocity.magnitude * bounce;

        //apply our velocity to other dynamic solids
        if (hit != null && hit.GetType() == typeof(DynamicSolid))
        {
            //bounce = Bounciness * (Mass / hit.Mass);
            hit.Velocity = Velocity * bounce;
        }

        FreeFalling = false;
        Debug.Log("End Vel : " + Velocity);
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
