using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentQuizDifficulty = 1;

    [SerializeField]
    private QuizSelecter _selecter;

    private void Update()
    {
        // 正解
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 出題する難易度を上げる
            _currentQuizDifficulty++;
            _selecter.SelectQuiz(_currentQuizDifficulty);
        }
        // 不正解
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 出題する難易度を下げる
            _currentQuizDifficulty--;
            _selecter.SelectQuiz(_currentQuizDifficulty);
        }
    }
}
