using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuestionResultView : MonoBehaviour
{
    // 〇のスプライト
    private Sprite _circleSymbol;
    // ✕のスプライト
    private Sprite _crossSymbol;
    // 記号表示イメージ
    private Image _resultSymbolImage;
    // 表示時間（ミリ秒）
    private const int _displayMilliseconds = 500;

    /// <summary>
    /// 正解・不正解に応じて〇・✕を表示する関数
    /// </summary>
    /// <param name="result"<true>正解</true><false>不正解</false></param>
    public async void DisplayResultSymbol(bool result)
    {
        // 正解
        if (result)
        {
            // 表示するマークを"〇"に設定する
            _resultSymbolImage.sprite = _circleSymbol;
        }
        // 不正解
        else
        {
            // 表示するマークを"✕"に設定する
            _resultSymbolImage.sprite= _crossSymbol;
        }

        // マークを表示する
        _resultSymbolImage.gameObject.SetActive(true);
        // ディレイ
        await UniTask.Delay(_displayMilliseconds);
        // マークを非表示にする
        _resultSymbolImage.gameObject.SetActive(false);
    }
}
