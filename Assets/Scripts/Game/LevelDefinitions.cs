static class LevelDefinitions
{
    /// <summary>The number of levels for different difficulties</summary>
    public static int LevelDistribution(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return 2;
            case Difficulty.Medium:
                return 1;
            case Difficulty.Difficult:
                return 1;
        }
        return 0;
    }

    public struct Level
    {
        /// <summary>This describes the difficulty of the level</summary>
        Difficulty Difficulty;
        /// <summary>When set to true, beating this level will end the run</summary>
        bool IsFinalLevel;
        bool IsBossLevel;
        string SceneID;
    }

    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Difficult = 2,
    }
}