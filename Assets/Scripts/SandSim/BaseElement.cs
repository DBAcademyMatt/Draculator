using UnityEngine;

public abstract class BaseElement : ScriptableObject
{
    public Chunk Chunk;
    //[HideInInspector]
    //public Vector2Int Position;
    public Gradient ColourRange;
    [HideInInspector]
    public Color Color;

    public float fireResistance;
    public bool OnFire;

    [HideInInspector]
    public Vector2 Velocity;

    public abstract Vector2Int Step(Vector2Int Position);
    public abstract void OnCreate();
    public abstract void Die();
    public abstract void DieAndReplace(BaseElement element);
    public abstract Color GetColor();
    //public abstract bool ReceiveHeat(float heat);

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
