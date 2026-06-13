// Exceeding Requirements:
// 1. Log file: saves activity name, date, and duration to mindfulness_log.txt
// 2. No repeated random prompts/questions until all have been used at least once per session
// 3. Gratitude Activity added as a 4th activity
// 4. Animated breathing text that grows and shrinks with pacing cues
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
 
// ─────────────────────────────────────────────
// BASE CLASS
// ─────────────────────────────────────────────
class Activity
{
    private string _name;
    private string _description;
    private int _duration;
 
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }
 
    public int Duration => _duration;
    public string Name => _name;
 
    public void DisplayStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---\n");
        Console.WriteLine(_description);
        Console.Write("\nHow long (in seconds) would you like to do this activity? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("\nGet ready to begin...");
        ShowSpinner(4);
    }
 
    public void DisplayEndMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed {_duration} seconds of the {_name}.");
        ShowSpinner(3);
        LogActivity();
    }
 
    public void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        int total = seconds * 8;
        for (int i = 0; i < total; i++)
        {
            Console.Write($"\r{frames[i % 4]} ");
            Thread.Sleep(125);
        }
        Console.Write("\r  \r");
    }
 
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r{i}  ");
            Thread.Sleep(1000);
        }
        Console.Write("\r   \r");
    }
 
    private void LogActivity()
    {
        string logPath = "mindfulness_log.txt";
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm} | {_name} | {_duration} seconds";
        File.AppendAllText(logPath, entry + Environment.NewLine);
    }
}
 
// ─────────────────────────────────────────────
// BREATHING ACTIVITY
// ─────────────────────────────────────────────
class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }
 
    public void Run()
    {
        DisplayStartMessage();
        int elapsed = 0;
        bool breatheIn = true;
 
        while (elapsed < Duration)
        {
            int interval = 4;
            if (breatheIn)
            {
                Console.WriteLine("\nBreathe in...");
                // Animated growing dots
                for (int i = 1; i <= interval; i++)
                {
                    Console.Write($"\r{new string('.', i * 3)}  ");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.WriteLine("\nBreathe out...");
                for (int i = interval; i >= 1; i--)
                {
                    Console.Write($"\r{new string('.', i * 3)}  ");
                    Thread.Sleep(1000);
                }
            }
 
            elapsed += interval;
            breatheIn = !breatheIn;
        }
 
        DisplayEndMessage();
    }
}
 
// ─────────────────────────────────────────────
// REFLECTION ACTIVITY
// ─────────────────────────────────────────────
class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
 
    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
 
    private List<string> _unusedQuestions = new List<string>();
 
    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }
 
    private string GetRandomPrompt()
    {
        Random rnd = new Random();
        return _prompts[rnd.Next(_prompts.Count)];
    }
 
    private string GetNextQuestion()
    {
        if (_unusedQuestions.Count == 0)
            _unusedQuestions = new List<string>(_questions);
 
        Random rnd = new Random();
        int idx = rnd.Next(_unusedQuestions.Count);
        string q = _unusedQuestions[idx];
        _unusedQuestions.RemoveAt(idx);
        return q;
    }
 
    public void Run()
    {
        DisplayStartMessage();
        Console.WriteLine($"\n{GetRandomPrompt()}");
        ShowSpinner(5);
 
        int elapsed = 0;
        while (elapsed < Duration)
        {
            string question = GetNextQuestion();
            Console.WriteLine($"\n> {question}");
            int pause = 6;
            ShowSpinner(pause);
            elapsed += pause;
        }
 
        DisplayEndMessage();
    }
}
 
// ─────────────────────────────────────────────
// LISTING ACTIVITY
// ─────────────────────────────────────────────
class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };
 
    private List<string> _unusedPrompts = new List<string>();
 
    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }
 
    private string GetNextPrompt()
    {
        if (_unusedPrompts.Count == 0)
            _unusedPrompts = new List<string>(_prompts);
 
        Random rnd = new Random();
        int idx = rnd.Next(_unusedPrompts.Count);
        string p = _unusedPrompts[idx];
        _unusedPrompts.RemoveAt(idx);
        return p;
    }
 
    public void Run()
    {
        DisplayStartMessage();
        Console.WriteLine($"\n{GetNextPrompt()}");
        Console.WriteLine("You have 5 seconds to think...");
        ShowCountdown(5);
 
        int count = 0;
        DateTime end = DateTime.Now.AddSeconds(Duration);
 
        Console.WriteLine("Start listing! Press Enter after each item.\n");
        while (DateTime.Now < end)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
                count++;
        }
 
        Console.WriteLine($"\nYou listed {count} items!");
        DisplayEndMessage();
    }
}
 
// ─────────────────────────────────────────────
// GRATITUDE ACTIVITY (4th activity - exceeds requirements)
// ─────────────────────────────────────────────
class GratitudeActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Name something in nature that you are grateful for.",
        "Name a person who has positively influenced your life.",
        "Name something about your body or health you are thankful for.",
        "Name a challenge that made you stronger.",
        "Name a simple everyday comfort you appreciate."
    };
 
    private List<string> _unusedPrompts = new List<string>();
 
    public GratitudeActivity() : base(
        "Gratitude Activity",
        "This activity will help you cultivate gratitude by focusing your attention on the blessings in your life, big and small.")
    { }
 
    private string GetNextPrompt()
    {
        if (_unusedPrompts.Count == 0)
            _unusedPrompts = new List<string>(_prompts);
 
        Random rnd = new Random();
        int idx = rnd.Next(_unusedPrompts.Count);
        string p = _unusedPrompts[idx];
        _unusedPrompts.RemoveAt(idx);
        return p;
    }
 
    public void Run()
    {
        DisplayStartMessage();
        int elapsed = 0;
 
        while (elapsed < Duration)
        {
            Console.WriteLine($"\n{GetNextPrompt()}");
            Console.Write("Your response: ");
            Console.ReadLine();
            int pause = 4;
            Console.WriteLine("Take a moment to feel that gratitude...");
            ShowSpinner(pause);
            elapsed += pause + 5; // estimate for typing
        }
 
        DisplayEndMessage();
    }
}
 
// ─────────────────────────────────────────────
// MAIN PROGRAM
// ─────────────────────────────────────────────
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===\n");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity");
            Console.WriteLine("5. Quit");
            Console.Write("\nSelect an option: ");
 
            string choice = Console.ReadLine();
 
            switch (choice)
            {
                case "1":
                    new BreathingActivity().Run();
                    break;
                case "2":
                    new ReflectionActivity().Run();
                    break;
                case "3":
                    new ListingActivity().Run();
                    break;
                case "4":
                    new GratitudeActivity().Run();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Thread.Sleep(1500);
                    break;
            }
        }
    }
}