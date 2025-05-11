using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoreExitRoom : Room
{
    public RoomType Type = RoomType.OneExit;

    [SerializeField] private Connector[] _connectors;

    public override List<Connector> GetFreeConnectors()
    {
        List<Connector> freeConnectors = new ();
        foreach(var connector in _connectors)
        {
            if(connector.IsFree)
            {
                freeConnectors.Add(connector);
            }
        }
        return freeConnectors;
    }

    public override void SetConnector(Connector connector)
    {
        var freeConnector = FindFirstFreeConnector();

        if (freeConnector == null)
            return;

        connector.ParentRoom.transform.forward = freeConnector.transform.forward;
        connector.ParentRoom.transform.position = (freeConnector.transform.position - connector.transform.position);

        freeConnector.SetConnect(connector);
    }

    public override Connector[] GetConnectors()
    {
        return _connectors;
    }

    public override Connector FindFirstFreeConnector()
    {
        foreach (var connector in _connectors)
        {
            if (connector.IsFree)
                return connector;
        }
        return null;
    }

    public override RoomType GetRoomType()
    {
        return Type;
    }
}
