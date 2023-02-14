
public interface ILevelInfo
{
        LevelType LevelType { get; }
        int DisplayLevel { get; }
        int GetCurrentLevel();
}
