using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] public Room ParentRoom;

    [NonSerialized] public Connector ConnectedConnector;
    
    public bool IsFree = true;

    public void SetConnect(Connector connector)
    {
        Debug.Log("1");
        this.IsFree = false;
        connector.IsFree = false;
        this.ConnectedConnector = connector;
        connector.ConnectedConnector = this;
    }

    public void RemoveConnect()
    {
        if (IsFree) return;

        IsFree = true;
        ConnectedConnector.IsFree = true;
        ConnectedConnector.ConnectedConnector = null;
        ConnectedConnector = null;
    }
}
