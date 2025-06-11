using UnityEngine;

public abstract class Animationtrigger_Enemy : MonoBehaviour
{
    protected Enemy enemy;
    protected void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public void Animtiontrigger() => enemy.AnimationtriggerBase();
}
