using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Control : MonoBehaviour
{
    protected bool active;
    protected Battle battle;
    private void Start()
    {
        battle = GetComponent<Battle>();
    }
    public virtual void Activate(bool value) 
    {
        active = value;
    }
    protected virtual void SelectEnemy()
    {
        
    }
}
