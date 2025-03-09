using MessagePack.Resolvers;
using MessagePack;
using QuestionMasterData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class QuestionSelecter : MonoBehaviour
{
    // 問題データベース
    private MemoryDatabase _questionDatabase;
    private string _answerStr;
    private int _nowQuestionId;
    private int _currentDifficulty;
    private bool _preQuestionResult;
    private QuestionView _questionView;
    private QuestionResultView _questionResultView;

    private async void Start()
    {
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance,
            GeneratedResolver.Instance,
            StandardResolver.Instance
        );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var asset = Addressables.LoadAssetAsync<TextAsset>("QuestionMasterData");
        await asset.Task;
        var binary = asset.Result.bytes;

        _questionDatabase = new MemoryDatabase(binary);
    }

    public void CheckCorrect(string inputAnswer)
    {
        bool result = inputAnswer == _answerStr;

        // 正解
        if (result)
        {
            _preQuestionResult = true;

            if (_preQuestionResult)
            {
                _currentDifficulty++;
            }
        }
        // 不正解
        else
        {
            _preQuestionResult = false;

            if (_preQuestionResult)
            {
                _currentDifficulty++;
            }
        }

        _questionResultView.DisplayResultSymbol(result);
    }

    /// <summary>
    /// 問題を選択する関数
    /// </summary>
    public void SelectQuestion()
    {
        // 現在出題する難易度の問題を取得する
        var questions = _questionDatabase.QuestionMasterTable.FindRangeByDifficulty(_currentDifficulty, _currentDifficulty);

        _nowQuestionId = Random.Range(0, questions.Count);
        _answerStr = questions[_nowQuestionId].Reading;
        _questionView.SetQuestionText(questions[_nowQuestionId].Kanji);
    }
}
