// Eternal Quest Program
// Extra: Level system — as you accumulate points you rank up:
// Rookie -> Apprentice -> Warrior -> Champion -> Hero -> Legend -> Unicorn Ninja

using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            manager.DisplayScore();
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine("1. Create goal");
            Console.WriteLine("2. View goals");
            Console.WriteLine("3. Record event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Quit");
            Console.Write("\nOption: ");

            switch (Console.ReadLine())
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.ListGoals(); break;
                case "3": manager.RecordEvent(); break;
                case "4": manager.SaveGoals(); break;
                case "5": manager.LoadGoals(); break;
                case "6": running = false; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }
}