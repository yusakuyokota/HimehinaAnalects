using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private InputField _inputField;
    [SerializeField]
    private QuestionSelecter _questionSelecter;
    private bool _beAcceptInput;

    public void Init()
    {
        _inputField = GetComponent<InputField>();
        _inputField.ActivateInputField();
    }

    public async void PlayerInputAsync()
    {
        // プレイヤーが Enter キーを押すまで待機
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        // 入力の受付を拒否する
        _beAcceptInput = false;
        // 入力された文字をクリアする
        _inputField.text = "";
        // InputField にフォーカスする
        _inputField.ActivateInputField();
        // 正誤判定をする
        await _questionSelecter.CheckCorrect(_inputField.text);
        // 入力の受付フラグを上げる
        _beAcceptInput = true;
        
    }
}
