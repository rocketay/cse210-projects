using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    // Level names and the points required to reach each one
    private string[] _levels = {
        "Rookie", "Apprentice", "Warrior", "Champion",
        "Hero", "Legend", "Unicorn Ninja"
    };

    private int[] _levelThresholds = { 0, 500, 1500, 3000, 5000, 8000, 12000 };

    // Returns the current level name based on the score
    public string GetLevel()
    {
        string current = _levels[0];
        for (int i = 0; i < _levelThresholds.Length; i++)
        {
            if (_score >= _levelThresholds[i])
                current = _levels[i];
        }
        return current;
    }

    public void DisplayScore()
    {
        Console.WriteLine($"\nScore: {_score} pts  |  Level: {GetLevel()}");
    }

    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nGoal type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        Console.Write("Option: ");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("How many times do you need to complete it? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus points on completion: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    public void RecordEvent()
    {
        ListGoals();
        Console.Write("\nWhich goal did you complete? (number): ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid number.");
            return;
        }

        string prevLevel = GetLevel();
        int earned = _goals[index].RecordEvent();
        _score += earned;
        string newLevel = GetLevel();

        Console.WriteLine($"\n+{earned} points!");

        // Notify the user if they leveled up
        if (prevLevel != newLevel)
            Console.WriteLine($"You leveled up: {prevLevel} --> {newLevel}!");
    }

    public void SaveGoals()
    {
        Console.Write("File name: ");
        string filename = Console.ReadLine();

        using (StreamWriter file = new StreamWriter(filename))
        {
            file.WriteLine(_score);
            foreach (Goal g in _goals)
                file.WriteLine(g.GetStringRepresentation());
        }

        Console.WriteLine("Saved!");
    }

    public void LoadGoals()
    {
        Console.Write("File name: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(":");
            string type = parts[0];
            string[] data = parts[1].Split(",");

            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2]), bool.Parse(data[3])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                    break;
                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5])));
                    break;
            }
        }

        Console.WriteLine("Loaded!");
    }
}