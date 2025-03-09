using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private QuestionSelecter _questionSelecter;
    [SerializeField]
    private PlayerController _playerController;

    private void Start()
    {
        StartGame();
    }

    private async void StartGame()
    {
        await _questionSelecter.InitDatabase();
        _questionSelecter.SelectQuestion();
        _playerController.Init();
        _playerController.PlayerInputAsync();
    }

    public void Hoge()
    {
        _playerController.PlayerInputAsync();
    }
}
