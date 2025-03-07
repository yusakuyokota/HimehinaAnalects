using MasterMemory;
using MessagePack;

[MemoryTable("Question"), MessagePackObject(true)]
public sealed class QuestionMaster
{
    public QuestionMaster(string id, string kanji, string reading, int difficulty, int idByDifficulty)
    {
        Id = id;
        Kanji = kanji;
        Reading = reading;
        Difficulty = difficulty;
        IdByDifficulty = idByDifficulty;
    }

    [PrimaryKey]
    public string Id { get; private set; }           // id
    public string Kanji { get; private set; }        // 出題する漢字
    public string Reading { get; private set; }      // 漢字の読み 
    [SecondaryKey(0), NonUnique]
    public int Difficulty { get; private set; }      // 難易度
    public int IdByDifficulty { get; private set; }  // 難易度別ID
}
