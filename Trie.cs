

public class Trie
{
    public Trie()
    {
        this.RootNode = new TrieNode();
    }

    private TrieNode RootNode;
    private Random rand = new Random();

    public void Add(string value)
    {
        value= value.ToLower(); //force all nodes to lowercase
        Add(value, RootNode);
    }

    public TrieSearchResult Search(string value, bool withSuggestioons)
    {
        value = value.ToLower();
        return Search(value, RootNode, withSuggestioons, 0);
    }


    private void Add(string value, TrieNode node)
    {
        if (value == null || value == string.Empty) return;
        if (node == null) return;

        var firstChar = value.Substring(0, 1);
        var remainder = value.Substring(1, value.Length - 1);

        var child = node.Nodes.FirstOrDefault(n => n.Value == firstChar);
        if (child != null)
        {
            Add(remainder, child);
        }
        else    
        {
            //add a new node and then descend
            var newNode = new TrieNode{
                Value = firstChar
            };

            node.Nodes.Add(newNode);

            Add(remainder, newNode);
        }
    }






    private TrieSearchResult Search(string value, TrieNode node, bool withSuggestions, int depth)
    {
        if (value == null || node == null) return new TrieSearchResult{
            Depth = depth
        };

        var firstChar = value.Substring(0, 1);
        var remainder = value.Substring(1, value.Length - 1);

        //serch for next child node and descend if found
        var child = node.Nodes.FirstOrDefault(n => n.Value == firstChar);
        if (child != null)
        {
            if(remainder == string.Empty)
            {
                var searchResult = new TrieSearchResult{
                    Depth = depth,
                    IsFound = true,
                    Node = child
                };

                if (withSuggestions)
                {
                    //go get random result
                    searchResult.Completions.Add(RandomComplete(node, "", true));
                }
                return searchResult;
                
            }

            return Search(remainder, child, withSuggestions, depth + 1); //keep looking
        }

        return new TrieSearchResult{
            Depth = depth
        }; //NOT FOUND

    }

    public string RandomComplete(TrieNode node, string suffix, bool isFirst)
    {
        //var result = "";
        if (node.Nodes.Count == 0)
        {
            return suffix;
        }
        else //select a random node and descend
        {
            var nodeCount = node.Nodes.Count;
            var nodePosition = rand.Next(0, nodeCount);
            var newNode = node.Nodes.Skip(nodePosition).Take(1).First();

            if(isFirst)
            {
                return RandomComplete(newNode, suffix, false);     
            }
            else
            {
                return newNode.Value + RandomComplete(newNode, suffix, false);
            }
        }
    }
}


public class TrieNode
{
    public TrieNode()
    {
        this.Nodes = new List<TrieNode>();
    }
    public List<TrieNode> Nodes { get; set; } = null!;

    public string Value { get; set; } = "";

}


public class TrieSearchResult
{
    public TrieSearchResult()
    {
        this.Completions = new List<string>();
    }

    public bool IsFound { get; set; }
    public TrieNode Node { get; set; } = null!;
    public int Depth { get; set; }
    public List<string> Completions { get; set; }
}