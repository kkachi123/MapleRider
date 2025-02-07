using UnityEngine;
public abstract class Node : ScriptableObject
{
    public enum STATE
    {
        FAILED,
        SUCCESS,
        RUN
    }
    protected GameObject target;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    public abstract void SetTarget(GameObject target);
    public abstract Node.STATE Evaluate();
    public virtual Node Clone()
    {
        return Instantiate(this);
    }
}
