using System;

public class LevelSystem
{
    public Action onLevelChanged;
    public Action onExperienceChanged;

    private int level;
    private int experience;
    private int experienceToNextLevel;
    public int Level { get => level; }
    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            onLevelChanged?.Invoke();
        }
        onExperienceChanged?.Invoke();
    }


}
