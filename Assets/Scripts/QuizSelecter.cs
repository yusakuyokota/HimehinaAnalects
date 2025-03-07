using MessagePack.Resolvers;
using MessagePack;
using QuestionMasterData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class QuizSelecter : MonoBehaviour
{
    private MemoryDatabase _questionDatabase;

    [SerializeField]
    private Text _questionText;

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

    public void SelectQuiz(int difficulty)
    {
        var questions = _questionDatabase.QuestionMasterTable.FindRangeByDifficulty(difficulty, difficulty);
        var randQ = Random.Range(0, questions.Count);
        _questionText.text = questions[randQ].Kanji;
    }
}
