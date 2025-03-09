using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    // 表示テキスト
    private Text _questionText;

    private void Start()
    {
        _questionText = GetComponent<Text>();
    }

    /// <summary>
    /// 出題する漢字を表示する関数
    /// </summary>
    /// <param name="question">出題する漢字</param>
    public void SetQuestionText(string question)
    {
        // 問題の表記を更新する
        _questionText.text = question;
    }
}
