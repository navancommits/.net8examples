using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Frozen;
using System.Buffers;

namespace MyApp
{
    internal class Program
    {
        public record Name(string Common, string Official);

        static void Main(string[] args)
        {
            Console.WriteLine("Example 1: JsonInclude for non-public properties");
            string json = JsonSerializer.Serialize(new AppLogic(10,10));
            Console.WriteLine(json);
            Console.WriteLine("Example 1: Complete");

            Console.WriteLine("Example 2: Stream - based ZipFile methods");
            AppLogic logic = new();

            Stream stream= logic.GetZipStream("c:\\vvv");
            logic.CreateArtifactFromStream(stream, "c:\\empty");
            Console.WriteLine("Example 2: Complete");

            Console.WriteLine("Example 3: Serialise enum fields as strings - JsonStringEnumConverter<T> converter attribute");
            var cityMonthClimate = new CityMonthClimate
            {
                Country = "Australia",
                City = "Melbourne",
                Month = Month.March,
                Climate = Climate.AllinOne
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = Context1.Default,
            };
            string? jsonString = JsonSerializer.Serialize(cityMonthClimate, options);

            Console.WriteLine(jsonString);
            Console.WriteLine("Example 3: Complete");

            Console.WriteLine("Example 4 and 5: Interface hierarchies and Snake case");

            ICat persian = new Persian { No_of_Legs = 4, Is_Furry = true };
            Console.WriteLine("Cat with legs:" + JsonSerializer.Serialize(persian));

            ICat siamese = new Siamese { No_of_Legs = 4, Is_Furry = false };
            Console.WriteLine("Cat with legs:" + JsonSerializer.Serialize(siamese));

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower//for jsonnamingpolicy feature
            };

            ICat cat = new Siamese();
            cat.No_of_Legs = 4;
            cat.Is_Furry = true;
            Console.WriteLine(JsonSerializer.Serialize(cat, jsonOptions));

            long providerTimestamp1 = TimeProvider.System.GetTimestamp();
            long providerTimestamp2 = TimeProvider.System.GetTimestamp();

            var period = GetElapsedTime(providerTimestamp1, providerTimestamp2);

            Console.WriteLine("Example 6: FrozenDictionary");
            FrozenDictionary<string, string> fdict = s_configurationData;
            if (fdict.TryGetValue("1", out string? output)) { Console.WriteLine(output); }
            //fdict.TryAdd<string, string>("4", "f");
            Console.WriteLine("Example 6: Complete");

            //Dictionary<string, string> d = new Dictionary<string, string>();
            //d.TryAdd<string, string>("5", "g");

            Console.WriteLine("Example 11: SearchValues");
            string ExampleText = "Sql@!ject";

            if (ExampleText.AsSpan().IndexOfAny(ListofSplcharArr) > -1) { Console.WriteLine("Invalid char detected"); };

            if (ExampleText.AsSpan().IndexOfAny(Base64SearchVal) > -1) { Console.WriteLine("Invalid char detected using SearchValues"); };
            Console.WriteLine("Example 11: Complete");

        }

        private const string Base64Chars = "@+/!";

        private static readonly SearchValues<char> Base64SearchVal = SearchValues.Create(Base64Chars);

        private static readonly char[] ListofSplcharArr =
   [
       
       '@',
       '+',
       '/',
       '!'
   ];

        private static readonly FrozenDictionary<string, string> s_configurationData = LoadConfigurationData();

        private static FrozenDictionary<string,string> LoadConfigurationData()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { "1", "navan" },
                { "2", "z" },
                { "3", "y" }
            };

            return dict.ToFrozenDictionary();
        }


        private static long GetElapsedTime(long providerTimestamp1, long providerTimestamp2)
        {
            return providerTimestamp2 - providerTimestamp1;
        }
    }

    public interface IAnimal
    {
        public int No_of_Legs { get; set; }
    }

    public interface ICat : IAnimal
    {
        public bool Is_Furry { get; set; }
    }

    public class Siamese : ICat
    {
        public bool Is_Furry { get; set; }
        public int No_of_Legs { get; set; }
    }

    public class Persian : ICat
    {
        public bool Is_Furry { get; set; }
        public int No_of_Legs { get; set; }
    }


    [JsonSerializable(typeof(CityMonthClimate))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    public partial class Context1 : JsonSerializerContext { }

    public class CityMonthClimate
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public Month? Month { get; set; }
        public Climate? Climate { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter<Climate>))]
    public enum Climate
    {
        Summer, Winter, Autumn, Spring,AllinOne
    }

    [JsonConverter(typeof(JsonStringEnumConverter<Month>))]
    public enum Month
    {
        January, February, March, April, May, June, July, August, September,October, November,December
    }

    internal class AppLogic
    {
        internal Stream GetZipStream(string inputDirectory)
        {
            var sav = "test.zip"; //blank zip
            Stream stream = new FileStream(sav, FileMode.OpenOrCreate, FileAccess.ReadWrite); // just opening the blank stream
            ZipFile.CreateFromDirectory(inputDirectory, stream); //load the stream from contents of diff directory

            return stream;
        }

        internal void CreateArtifactFromStream(Stream memoryStream,string outputDirectory)
        {
            ZipFile.ExtractToDirectory(memoryStream, outputDirectory);
        }

        internal AppLogic()
        { }

        [JsonConstructor]
        internal AppLogic(int length, int breadth)
        {
            Length = length;
            Breadth=breadth;
            Area = length * breadth;
        }

        [JsonInclude]
        internal int Length { get; set; }

        [JsonInclude]
        internal int Breadth { get; set; }

        [JsonInclude]
        private int Area { get; }//include calculated field in json output

        [AllowedValues("apple", "banana", "mango")]
        internal string Fruit
        {
            get; set;
        }
    }

    
}