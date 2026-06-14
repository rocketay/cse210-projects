public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _timesCompleted = 0;
    }

    // Second constructor used when loading from file
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int timesCompleted)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _timesCompleted = timesCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete()) return 0;
        _timesCompleted++;

        // Give bonus points when the goal is fully completed
        if (_timesCompleted >= _target)
            return _points + _bonus;

        return _points;
    }

    public override bool IsComplete() => _timesCompleted >= _target;

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";
        return $"{status} {_name} ({_description}) -- Completed {_timesCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_name},{_description},{_points},{_target},{_bonus},{_timesCompleted}";
    }
}