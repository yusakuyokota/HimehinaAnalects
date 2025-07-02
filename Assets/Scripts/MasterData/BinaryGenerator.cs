using System.IO;
using MessagePack;
using MessagePack.Resolvers;
using QuestionMasterData;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class BinaryGenerator
{
#if UNITY_EDITOR
    [MenuItem("Tools/Master/Generate Binary")]
    private static async void Run()
    {
        // MessagePackの初期化（ボイラープレート）
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance, // 自動生成されたResolver（Namespaceごとに作られる）
            GeneratedResolver.Instance, // 自動生成されたResolver
            StandardResolver.Instance // MessagePackの標準Resolver
        );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var csvFile = Addressables.LoadAssetAsync<TextAsset>("QuestionMasterCsv");
        await csvFile.Task;
        StringReader reader = new StringReader(csvFile.Result.text);
        string questionMasters = reader.ReadToEnd();

        var databaseBuilder = new BinaryBuilder();
        var binary = databaseBuilder.Run(questionMasters, "Question");

        // できたバイナリは永続化しておく
        var path = "Assets/Scripts/MasterData/Binary/QuestionMaster.bytes";
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        File.WriteAllBytes(path, binary);
        AssetDatabase.Refresh();
    }
#endif
}
