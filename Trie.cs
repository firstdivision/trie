

public class Trie
{
    public Trie()
    {
        this.RootNode = new TrieNode();
    }

    private TrieNode RootNode;

    public void Add(string value)
    {
        value= value.ToLower(); //force all nodes to lowercase
        Add(value, RootNode);
    }

    public TrieNode? Search(string value)
    {
        value = value.ToLower();
        return Search(value, RootNode);
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






    private TrieNode? Search(string value, TrieNode node)
    {
        if (value == null || node == null) return null;
        var firstChar = value.Substring(0, 1);
        var remainder = value.Substring(1, value.Length - 1);

        //if  (remainder == null || remainder == "") return node;

        //serch for next child node and descend if found
        var child = node.Nodes.FirstOrDefault(n => n.Value == firstChar);
        if (child != null)
        {
            if(remainder == string.Empty) return child;
            return Search(remainder, child); //keep looking
        }

        return null; //NOT FOUND

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