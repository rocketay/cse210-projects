using System;

// Exceeding Requirements:
// I added a mood tracking feature so users can record their emotional state
// with each journal entry. I also added extra prompts to give the user
// more variety when writing in the journal.

Journal journal = new Journal();
PromptGenerator promptGenerator = new PromptGenerator();

int choice = 0;

while (choice != 5)
{
    Console.WriteLine("Please select one of the following choices:");
    Console.WriteLine("1. Write");
    Console.WriteLine("2. Display");
    Console.WriteLine("3. Load");
    Console.WriteLine("4. Save");
    Console.WriteLine("5. Quit");
    Console.Write("What would you like to do? ");

    choice = int.Parse(Console.ReadLine());

    if (choice == 1)
    {
        string prompt = promptGenerator.GetRandomPrompt();

        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        Console.Write("How are you feeling today? ");
        string mood = Console.ReadLine();

        DateTime theCurrentTime = DateTime.Now;
        string dateText = theCurrentTime.ToShortDateString();

        Entry newEntry = new Entry();

        newEntry._date = dateText;
        newEntry._promptText = prompt;
        newEntry._entryText = response;
        newEntry._mood = mood;

        journal.AddEntry(newEntry);
    }
    else if (choice == 2)
    {
        journal.DisplayAll();
    }
    else if (choice == 3)
    {
        Console.Write("What is the filename? ");
        string filename = Console.ReadLine();

        journal.LoadFromFile(filename);
    }
    else if (choice == 4)
    {
        Console.Write("What is the filename? ");
        string filename = Console.ReadLine();

        journal.SaveToFile(filename);
    }
}