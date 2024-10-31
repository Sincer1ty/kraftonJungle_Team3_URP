using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SpectatorCamera : MonoBehaviour
{
    public float mouseSensitivity = 50f;
    private List<GameObject> remainingPlayers = new List<GameObject>();
    private GameObject spectatingTarget;
    private GameObject TargetHead;
    private int currentPlayerIndex = 0;

    private PlayerInput playerInput;
    private InputAction prevPlayerAction;
    private InputAction nextPlayerAction;

    private CinemachineFreeLook FreeLockCamera;

    [SerializeField] private GameObject canvasPrefab;

    private GameObject canvasInstance;
    private SpectatorText spectatorText;

    private void FollowAndLookAtTarget(GameObject spectatingTarget)
    {
        TargetHead = spectatingTarget.transform.Find("Head").gameObject; // 플레이어의 머리 위치 찾기
        FreeLockCamera.Follow = TargetHead.transform; // 버츄얼 카메라가 관전 대상의 머리를 따라다니도록 설정
        FreeLockCamera.LookAt = TargetHead.transform; // 버츄얼 카메라가 관전 대상의 머리를 바라보도록 설정
    }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        prevPlayerAction = playerInput.actions["PrevPlayer"];
        nextPlayerAction = playerInput.actions["NextPlayer"];
        FreeLockCamera = GetComponentInChildren<CinemachineFreeLook>();

        Debug.Log("Spectator Camera Awake");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨김

        remainingPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")); // 모든 플레이어를 찾아서 리스트에 추가
        spectatingTarget = remainingPlayers[currentPlayerIndex]; // 초기 관전 대상 설정
        FollowAndLookAtTarget(spectatingTarget); // 버츄얼 카메라가 관전 대상을 따라다니도록 설정        

        canvasInstance = Instantiate(canvasPrefab); // 캔버스 생성
        GameManager.instance.spectatorCanvas = canvasInstance;
        spectatorText = canvasInstance.GetComponentInChildren<SpectatorText>(); // 관전 대상 텍스트
        spectatorText.SetSpectatingTarget(spectatingTarget); // UI에 관전 대상 표시        

        Debug.Log("Spectator Camera Start");
    }

    // Update로 관전 대상 변경 감지, 버츄얼 카메라 위치 업데이트
    void Update()
    {
        // Handle switching between players
        if (prevPlayerAction.triggered)
        {
            SwitchPlayer(-1);
        }
        if (nextPlayerAction.triggered)
        {
            SwitchPlayer(1);
        }
    }

    // 관전 대상 변경
    private void SwitchPlayer(int idx_change)
    {
        if (remainingPlayers.Count == 0)
            return;

        currentPlayerIndex += idx_change;
        if (currentPlayerIndex < 0)
            currentPlayerIndex = remainingPlayers.Count - 1;
        else if (currentPlayerIndex >= remainingPlayers.Count)
            currentPlayerIndex = 0;

        spectatingTarget = remainingPlayers[currentPlayerIndex];
        spectatorText.SetSpectatingTarget(spectatingTarget); // UI에 관전 대상 표시
        FollowAndLookAtTarget(spectatingTarget); // 버츄얼 카메라가 관전 대상을 따라다니도록 설정

        Debug.Log("Switching to player " + spectatingTarget.name);
    }

    // 다른 플레이어가 죽었을 때 SpectatorManager가 호출
    public void RemovePlayer(GameObject player)
    {
        remainingPlayers.Remove(player);
        if (spectatingTarget == player)
        {
            currentPlayerIndex = currentPlayerIndex % remainingPlayers.Count;
            spectatingTarget = remainingPlayers[currentPlayerIndex]; // 관전 대상을 남아있는 플레이어 중 하나로 변경
            spectatorText.SetSpectatingTarget(spectatingTarget); // UI에 관전 대상 표시
            FollowAndLookAtTarget(spectatingTarget); // 버츄얼 카메라가 관전 대상을 따라다니도록 설정
        }
    }
}
