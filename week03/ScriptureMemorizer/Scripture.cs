namespace ScriptureMemorizer;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public bool IsCompletelyHidden => _words.All(w => w.IsHidden);
    public int VisibleWordCount => _words.Count(w => !w.IsHidden);

    // Stretch: only picks from words not yet hidden
    public void HideRandomWords(int count = 3)
    {
        var visible = _words.Where(w => !w.IsHidden).ToList();
        foreach (var word in visible.OrderBy(_ => _random.Next()).Take(count))
            word.Hide();
    }

    public override string ToString()
    {
        return $"{_reference}\n{string.Join(" ", _words)}";
    }
}