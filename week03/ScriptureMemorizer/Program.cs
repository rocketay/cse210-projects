/*
 * EXCEEDING REQUIREMENTS:
 * 1. Smart hiding — only hides words not yet hidden (stretch challenge)
 * 2. Progress indicator showing remaining visible words
 * 3. Multiple scriptures to choose from at random
 * 4. Play again loop after completing a scripture
 */

using ScriptureMemorizer;

var scriptures = new List<Scripture>
{
    new Scripture(new Reference("John", 3, 16),
        "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life."),
    new Scripture(new Reference("Proverbs", 3, 5, 6),
        "Trust in the Lord with all your heart and lean not on your own understanding in all your ways submit to him and he will make your paths straight."),
    new Scripture(new Reference("Philippians", 4, 13),
        "I can do all this through him who gives me strength."),
    new Scripture(new Reference("Romans", 8, 28),
        "And we know that in all things God works for the good of those who love him who have been called according to his purpose."),
    new Scripture(new Reference("Joshua", 1, 9),
        "Have I not commanded you be strong and courageous do not be afraid do not be discouraged for the Lord your God will be with you wherever you go."),
};

var random = new Random();
bool keepPlaying = true;

while (keepPlaying)
{
    var scripture = scriptures[random.Next(scriptures.Count)];

    while (true)
    {
        Console.Clear();
        Console.WriteLine("════════════════════════════════════════");
        Console.WriteLine("         SCRIPTURE MEMORIZER");
        Console.WriteLine("════════════════════════════════════════\n");
        Console.WriteLine(scripture);
        Console.WriteLine();

        if (scripture.IsCompletelyHidden)
        {
            Console.WriteLine("✓ All words hidden — you've got it!");
            break;
        }

        Console.WriteLine($"[{scripture.VisibleWordCount} words remaining]");
        Console.Write("\nPress Enter to hide more words, or type 'quit': ");
        string? input = Console.ReadLine()?.Trim().ToLower();

        if (input == "quit") return;

        scripture.HideRandomWords(3);
    }

    Console.Write("\nPlay again with a new scripture? (Enter / quit): ");
    keepPlaying = Console.ReadLine()?.Trim().ToLower() != "quit";
}