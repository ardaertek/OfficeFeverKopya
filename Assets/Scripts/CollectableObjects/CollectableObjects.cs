using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableObjects : MonoBehaviour
{
    [SerializeField] string ObjectName;
    [SerializeField] int MoneyValue;
    protected void Update()
    {
        transform.forward = transform.parent.forward;
    }
}
