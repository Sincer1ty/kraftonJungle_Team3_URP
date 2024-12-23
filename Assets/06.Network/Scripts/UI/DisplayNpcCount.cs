using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNpcCount : MonoBehaviour
{
    public TMP_Text npcText;  // Ping 정보를 표시할 UI Text

    private void Start()
    {
        ApplyUI();
    }
    public void ApplyUI()
    {
        // PhotonNetwork.GetPing()를 통해 현재 Ping 값을 가져와 UI에 표시
        npcText.text = $"NPC: {NetworkManager.Instance.NPCCount}";
    }

    // 최적화 작업
    //private void Update()
    //{
        // PhotonNetwork.GetPing()를 통해 현재 Ping 값을 가져와 UI에 표시
        //int npcCount = NetworkManager.Instance.NPCCount;
        //npcText.text = $"NPC: {npcCount}";
    //}
}
