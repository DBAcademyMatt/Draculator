using UnityEngine;

public abstract class BaseElement : ScriptableObject
{
    public Chunk Chunk;
    //[HideInInspector]
    //public Vector2Int Position;
    public Gradient ColourRange;
    [HideInInspector]
    public Color Color;

    public int Bouyancy = 1;
    //public float InertialResistance = 0.1f;
    public float Friction;
    public float LinearDrag;
    public float Bounciness;
    public float Mass;

    public float fireResistance;
    public bool OnFire;

    [HideInInspector]
    public Vector2 Velocity;

    public abstract bool Tick(Vector2Int Position);
    public abstract void OnCreate();
    public abstract void Die();
    public abstract void DieAndReplace(BaseElement element);
    //public abstract bool ReceiveHeat(float heat);

    public bool AlreadyUpdated = false;

    public virtual bool ReceiveHeat(float heat)
    {
        if (OnFire)
            return false;

        fireResistance -= (Random.value * heat);
        CheckIfOnFire();
        return true;
    }

    void CheckIfOnFire()
    {

    }
}
