using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private InputField _inputField;
    [SerializeField]
    private QuestionSelecter _questionSelecter;

    public void Init()
    {
        _inputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _inputField.Select();
        }
    }

    public async void PlayerInputAsync()
    {
        // InputField にフォーカスする
        _inputField.ActivateInputField();
        // プレイヤーが Enter キーを押すまで待機
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        // 正誤判定をする
        _questionSelecter.CheckCorrect(_inputField.text);
        // 入力された文字をクリアする
        _inputField.text = "";
    }
}
