using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    //public RoomType Type { get; set; }

    public abstract List<Connector> GetFreeConnectors();
    public abstract void SetConnector(Connector connector);
    public abstract Connector FindFirstFreeConnector();

    public abstract RoomType GetRoomType();
    public abstract Connector[] GetConnectors();
}
