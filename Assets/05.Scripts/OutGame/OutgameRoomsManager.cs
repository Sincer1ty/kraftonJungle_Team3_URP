using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutgameRoomsManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject serverButton;
    [SerializeField] private Transform serverList;

    [SerializeField] TMP_InputField roomCodeInput;

    List<RoomInfo> roomList = new List<RoomInfo>();

    public override void OnJoinedLobby()
    {
        DestroyAllServerButtons();
    }
    void DestroyAllServerButtons()
    {
        ServerButton[] buttons = serverList.GetComponentsInChildren<ServerButton>(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }
    }

    // 서버에서 방 목록을 받아와서 씬에 업데이트 합니다.
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.roomList = roomList;
        UpdateRoomList();
    }

    public void UpdateRoomList()
    {
        // Server에서 public으로 오픈된 모든 방들을 로딩합니다.
        // foreach를 통해서, 각각을 AddServerButtonOnServerList 함수를 실행하면 됩니다.
        DestroyAllServerButtons();


        foreach (RoomInfo room in roomList)
        {
            // 방 설정에 IsPublic이 없거나 Private로 설정되어있을 경우
            if (!room.CustomProperties.ContainsKey("IsPublic") ||
                (bool)room.CustomProperties["IsPublic"] == false)
                continue;

            ServerButtonElements buttonElement = new ServerButtonElements(room);

            AddServerButtonOnServerList(buttonElement);
        }
    }


    // 인자로 데이터를 넘겨줘야 합니다. 
    // 인자 목록
    // 1. 서버 이름 -> Server Title
    // 2. 서버 참여 인원 / 전체 참여 가능 인원 -> serverPlayerNum / serverTotalPlayerNum
    // 3. 서버 주인 -> Server Owner
    // 4. Ping -> ping
    // 자세한 사항은 ServerButtonElements를 찾아보세요.
    private void AddServerButtonOnServerList(ServerButtonElements sbe)
    {
        Instantiate(serverButton, serverList).GetComponent<ServerButton>().SetupServerButton(sbe);
    }

    public void PrivateCodeAccess()
    {
        string value = roomCodeInput.text;
        RoomInfo r = roomList.Find(r =>
        {
            return (int)r.CustomProperties["AccessCode"] != 0 &&
            (int)r.CustomProperties["AccessCode"] == int.Parse(value);
        });
        if (r != null)
        {
            PhotonNetwork.JoinRoom(r.Name);


            // 참여하게 되는 경우이므로, Guest로 참여합니다.
            FindObjectOfType<MyRoomManager>().OnRoomCreateOrJoin(false);
        }
        else
        {
            Debug.Log("해당 코드의 방이 존재하지 않습니다.");
        }
    }
}
 