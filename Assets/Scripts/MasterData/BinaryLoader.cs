using System.IO;
using System.Text;
using QuestionMasterData;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BinaryLoader : MonoBehaviour
{
    [MenuItem("Tools/Master/Load Binary")]
    private static async void Run()
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

        var memoryDatabase = new MemoryDatabase(binary);

        foreach (var question in memoryDatabase.QuestionMasterTable.All)
            Debug.Log(question.Id);

        var metaDb = MemoryDatabase.GetMetaDatabase();
        foreach (var table in metaDb.GetTableInfos())
        {
            // for example, generate CSV header
            var sb = new StringBuilder();
            foreach (var prop in table.Properties)
            {
                if (sb.Length != 0) sb.Append(",");

                // Name can convert to LowerCamelCase or SnakeCase.
                sb.Append(prop.NameSnakeCase);
            }
            File.WriteAllText(table.TableName + ".csv", sb.ToString(), new UTF8Encoding(false));
        }
    }
}