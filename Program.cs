// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var trie = new Trie();
LoadWordLists();

//test word lookup
var sw2 = new Stopwatch();
sw2.Start();
var theWord = "Mercutio, kinsman to the Prince and friend to Romeo";
var found = trie.Search(theWord);
sw2.Stop();

if(found != null)
{
    Console.WriteLine($"found '{theWord}' in {sw2.ElapsedMilliseconds} milliseconds.");
    Console.WriteLine($"found '{theWord}' in {sw2.ElapsedTicks} ticks.");
}
else
{
    Console.WriteLine("Did not find the word");
}

Console.WriteLine("End.");



void LoadWordLists()
{
    //get wordlists
    var path =  System.IO.Path.Join(Environment.CurrentDirectory, "word-list.json");
    var json = System.IO.File.ReadAllText(path);
    var words = JsonSerializer.Deserialize<List<string>>(json);
    AddWords(words);


    path =  System.IO.Path.Join(Environment.CurrentDirectory, "milestonk-words.txt");
    var text = System.IO.File.ReadAllText(path);
    words = text.Split(Environment.NewLine).Select(t => t.Trim()).ToList();
    AddWords(words);

    path =  System.IO.Path.Join(Environment.CurrentDirectory, "romeo-and-juliet.txt");
    text = System.IO.File.ReadAllText(path);
    words = text.Split(Environment.NewLine).Select(t => t.Trim()).ToList();
    AddWords(words);
}
void AddWords(List<string>? words)
{
    var sw = new Stopwatch();
    sw.Start();
    if (words != null)
    {
        foreach (var word in words)
        {
            if (word == string.Empty) continue;
            trie.Add(word);
        }
    }
    sw.Stop();
    Console.WriteLine($"Loaded {words?.Count} words in {sw.ElapsedMilliseconds} milliseconds.");
}