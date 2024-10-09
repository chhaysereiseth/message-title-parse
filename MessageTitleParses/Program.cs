// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
Console.WriteLine("");

/**
  Generate 10 Random MessageTitle List
*/
var posibleMessageTitle = new List<string>
        {
            "[BE]",
            "[FE]",
            "[QA]",
            "[Urgent]",
            "there is error",
            "some error",
            "no reply",
            "[LK]",
            "[BY]",
            "[MC]",
            "[DB]",
            "ottoperr"
        };
var inputs = GenerateRandomCombinations(posibleMessageTitle, 10, 3, 5);

Console.WriteLine("=> Begin MessageTitleParse\n");

/**
  Parse the Channerls
*/
inputs.ForEach((input) =>
{
  Console.WriteLine($"=> Input \t\t: {input}");
  Console.WriteLine($"=> Receive channels\t: {ParseChannels(input)}");
  Console.WriteLine("");
});

Console.WriteLine("\n=> Finish");

/**
  Example using LINQ and Regex with Valid Channel Filtering
*/
static string ParseChannels(string input)
{
  // Regex pattern to match text within brackets
  var matches = Regex.Matches(input, @"\[(.*?)\]");

  // List of valid channels
  var validChannelList = new List<string> { "BE", "FE", "QA", "Urgent" };

  // Define valid channel criteria
  Func<string, bool> isValidChannel = channel =>
        !string.IsNullOrWhiteSpace(channel)
        && validChannelList.Contains(channel);

  // Use LINQ to filter and select valid channels
  var validChannels = matches
      .Cast<Match>()
      .Select(m => m.Groups[1].Value)
      .Where(isValidChannel) // Filter only valid channels
      .ToList();

  if (validChannels.Count == 0)
  {
    return "No Channel";
  }

  return string.Join(", ", validChannels);
}

static List<string> GenerateRandomCombinations(List<string> items, int count, int minSize, int maxSize)
{
  Random random = new Random();
  var combinations = new List<string>();

  for (int i = 0; i < count; i++)
  {
    // Generate a random size between minSize and maxSize
    int size = random.Next(minSize, maxSize + 1);
    // Shuffle the items and take the first 'size' items
    var randomItems = items.OrderBy(x => random.Next()).Take(size).ToList();
    // Create a formatted string from the selected items
    string combination = string.Join("", randomItems);
    combinations.Add(combination);
  }

  return combinations;
}
