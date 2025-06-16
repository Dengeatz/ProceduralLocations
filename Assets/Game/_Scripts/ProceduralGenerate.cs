using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerate : MonoBehaviour
{
    [SerializeField] List<Room> _roomsPrefabs = new();
    List<(RoomType, Room)> _rooms = new();
    List<(RoomType, Room)> _activeRooms = new();

    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
        var room = GenerateRoom();
        _rooms.Add((room.GetRoomType(), room));
        _activeRooms.Add((room.GetRoomType(), room));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GenerateRooms();
        }
    }

    private void GenerateRooms()
    {
        List<Room> generatedRooms = new();
        List<Room> needToRemove = new();
        foreach (var r in _activeRooms)
        {
            var room = r.Item2;
            var freeConnectors = room.GetFreeConnectors();
            if(freeConnectors.Count > 0)
            {
                foreach(var connector in freeConnectors)
                {
                    var connectingRoom = GenerateRoom();
                    generatedRooms.Add(connectingRoom);
                    room.SetConnector(connectingRoom.FindFirstFreeConnector());
                }
            }
            else
            {
                needToRemove.Add(room);
            }
        }
        if (generatedRooms.Count > 0)
        {
            foreach(var room in generatedRooms)
            {
                bool isDeleted = false;
                foreach (var r in _rooms)
                {
                    if (room.GetComponent<Renderer>().bounds.Intersects(r.Item2.GetComponent<Renderer>().bounds))
                    {
                        room.ClearAllConnectors();
                        Debug.Log("Destroyed" + room.transform.position);
                        Destroy(room.gameObject);
                        isDeleted = true;
                        break;
                    }
                }
                
                if (isDeleted)
                    continue;

                foreach (var r in generatedRooms)
                {
                    if (r.Equals(room))
                        continue;

                    if (room.GetComponent<Renderer>().bounds.Intersects(r.GetComponent<Renderer>().bounds))
                    {
                        room.ClearAllConnectors();
                        Debug.Log("Destroyed" + room.transform.position);
                        Destroy(room.gameObject);
                        isDeleted = true;
                        break;
                    }
                }

                if (isDeleted)
                    continue;

                _activeRooms.Add((room.GetRoomType(), room));
                _rooms.Add((room.GetRoomType(), room));
                
            }
        }
        if(needToRemove.Count > 0)
        {
            foreach(var room in needToRemove)
            {
                _activeRooms.Remove((room.GetRoomType(), room));
            }
        }
        needToRemove.Clear();
        generatedRooms.Clear();
    }

    private Room GenerateRoom()
    {
        int randomPrefab = _random.Next(0, _roomsPrefabs.Count);
        var gObj = GameObject.Instantiate(_roomsPrefabs[randomPrefab], Vector3.zero, Quaternion.identity).GetComponent<Room>();
        foreach(var connector in gObj.GetConnectors())
        {
            connector.IsFree = true;
        }
        return gObj;
    }
}
