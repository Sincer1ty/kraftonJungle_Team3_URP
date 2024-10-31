using Michsky.UI.Dark;
using UnityEngine;

public enum CharacterType
{
    Scientist,
    Monster,
    NPC
}


public class GameManager : Singleton<GameManager>
{
    public GameObject hungerSliderPrefab;
    public GameObject canvasPrefab;
    public GameObject spectatorCanvas;

    public delegate void TwoGameObjects(GameObject o1, GameObject o2);

    public TwoGameObjects OnKilled;

    GameObject canvas;
    GameObject hungerSlider;

    public void SetPlayer(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Scientist:
                break;
            case CharacterType.Monster:
                break;
            default:
                Debug.LogError("GAMEMANAGER.CSS : UNDEFINED CHARACTER TYPE!!");
                break;
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Start");
        canvas = Instantiate(canvasPrefab);
        if (NetworkManager.Instance.IsMonster())
        {
            hungerSlider = Instantiate(hungerSliderPrefab, canvas.transform);
        }
        TimeManager.instance.isStarted = true;
        TimeManager.instance.SetDay();

        OnKilled += OnKilledSoundPlay;
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (spectatorCanvas != null)
        {
            FindObjectOfType<SpectatorText>().EndSpectating();
        }
        if (hungerSlider != null)
        {
            Destroy(hungerSlider);
        }

        if (canvas != null)
        {
            Destroy(canvas);
        }

            //FindObjectOfType<SpectatorText>().EndSpectating();

        TimeManager.instance.isEnd = true;
    }

    private void OnKilledSoundPlay(GameObject killer, GameObject victim)
    {
        killer.GetComponent<Player>().PlayKillSound();
        victim.GetComponent<Player>().PlayDeathSound();
    }
}
