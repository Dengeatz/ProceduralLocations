using System.Collections.Generic;
using UnityEngine;

public class NoneExitRoom : Room
{
    public override Connector FindFirstFreeConnector()
    {
        return null;
    }

    public override List<Connector> GetFreeConnectors()
    {
        return null;
    }

    public override RoomType GetRoomType()
    {
        return RoomType.ZeroExit;
    }

    public override void SetConnector(Connector connector)
    {
    }

    public override Connector[] GetConnectors()
    {
        return null;
    }

    public override void ClearAllConnectors()
    {
        throw new System.NotImplementedException();
    }
}
