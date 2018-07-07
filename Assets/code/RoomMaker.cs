using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaker : MonoBehaviour {

    float roomIndexFloat = 0;
    int roomIndex = 0; 
    [SerializeField]
    Room[] rooms;

    Room displayedRoom; 

    private void LateUpdate()
    {
        ShowBuildling(); 
        HandleScrollWheel();
        Viewer.RoomRotTrans.position = Viewer.RoomPlaceTrans.position;
        RotateBuildilng();
        UpdateRoomTrans();
        PlaceRoom(); 
    }

    void ShowBuildling()
    {
        if(InputManager.IsMaking && displayedRoom == null)
        {
            CreateDisplayRoom(); 
        }
        if(!InputManager.IsMaking && displayedRoom != null)
        {
            RemoveOldDisplayedRoom(); 
        }
    }

    void RotateBuildilng()
    {
        if (InputManager.IsRotatingBuildling)
        {
            float xDir = 0;
            if (Input.GetKeyDown(KeyCode.A)) xDir--;
            if (Input.GetKeyDown(KeyCode.D)) xDir++; 
            if (xDir != 0)
            {
                Viewer.RoomRotTrans.Rotate(Vector3.up, 90 * xDir); 
            }
            float yDir = 0;
            if (Input.GetKeyDown(KeyCode.W)) yDir--;
            if (Input.GetKeyDown(KeyCode.S)) yDir++;
            if (yDir != 0)
            {
                Viewer.RoomRotTrans.Rotate(Vector3.right, 90 * yDir);
            }
            float zDir = 0;
            if (Input.GetKeyDown(KeyCode.Q)) zDir--;
            if (Input.GetKeyDown(KeyCode.E)) zDir++;
            if (zDir != 0)
            {
                Viewer.RoomRotTrans.Rotate(Vector3.forward, 90 * zDir);
            }
        }
    }

    void HandleScrollWheel()
    {
        float scrollAmount =  Input.GetAxis("Mouse ScrollWheel");
        if(scrollAmount != 0){
            roomIndexFloat += scrollAmount * 5;
            int mod = 0; 
            if(roomIndexFloat < -1)
            {
                if(roomIndex - 1 == -1)
                {
                    roomIndex = rooms.Length - 1;
                }
                else
                {
                    roomIndex--;
                }
                roomIndexFloat = 0;
                CreateDisplayRoom(); 
            }if(roomIndexFloat > 1)
            {
                roomIndex++;
                roomIndex = (roomIndex + mod) % rooms.Length;
                roomIndexFloat = 0;
                CreateDisplayRoom(); 
            }
        }

    }

    void RemoveOldDisplayedRoom()
    {
        if(displayedRoom != null)
        {
            Destroy(displayedRoom.gameObject);
        }
    }
    void CreateDisplayRoom()
    {
        if (InputManager.IsMaking)
        {
            RemoveOldDisplayedRoom();
            displayedRoom = Instantiate(rooms[roomIndex], Viewer.RoomPlaceTrans.position, Viewer.RoomRotTrans.rotation);
            displayedRoom.StartPlacing(); 
        }
    }
    void UpdateRoomTrans()
    {
        if(displayedRoom != null)
        {
            displayedRoom.transform.position = new Vector3(
                Mathf.Round(Viewer.RoomPlaceTrans.position.x),
                Mathf.Round(Viewer.RoomPlaceTrans.position.y),
                Mathf.Round(Viewer.RoomPlaceTrans.position.z)
                );
            displayedRoom.transform.rotation = Viewer.RoomRotTrans.rotation; 
        }
    }

    void PlaceRoom()
    {
        if(displayedRoom != null 
            && displayedRoom.CanPlace()
            && Input.GetMouseButtonDown(0))
        {
            displayedRoom.Place();
            displayedRoom = null;
        }
    }
}
