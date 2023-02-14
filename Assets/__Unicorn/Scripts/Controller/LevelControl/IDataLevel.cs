namespace Unicorn
{
    public interface IDataLevel: ILevelInfo
    {
        LevelConstraint LevelConstraint { get; set; }
        void SetLevel(LevelType levelType, int level);
        void SetLevel(int buildIndex);
        int GetBuildIndex();
        void IncreaseLevel();
        void Save();
    }
}