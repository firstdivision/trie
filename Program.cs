// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text.Json;

var trie = new Trie();

Console.WriteLine("Hello, World!");

//get wordlists
var path =  System.IO.Path.Join(Environment.CurrentDirectory, "word-list.json");
var json = System.IO.File.ReadAllText(path);
var words = JsonSerializer.Deserialize<List<string>>(json);
AddWords(words);


path =  System.IO.Path.Join(Environment.CurrentDirectory, "milestonk-words.txt");
var text = System.IO.File.ReadAllText(path);
words = text.Split(Environment.NewLine).Select(t => t.Trim()).ToList();
AddWords(words);





var sw = new Stopwatch();
sw.Reset();
sw.Start();
var theWord = "tomorrow";
var found = trie.Search(theWord);
sw.Stop();

if(found != null)
{
    Console.WriteLine($"found '{theWord}' in {sw.ElapsedMilliseconds} milliseconds.");
}
else
{
    Console.WriteLine("Did not find the word");
}

Console.WriteLine("End.");

void AddWords(List<string>? words)
{
    var sw = new Stopwatch();
    sw.Start();
    if (words != null)
    {
        foreach (var word in words)
        {
            trie.Add(word);
        }
    }
    sw.Stop();
    Console.WriteLine($"Loaded {words?.Count} words in {sw.ElapsedMilliseconds} milliseconds.");
}