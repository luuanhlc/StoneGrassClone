namespace Unicorn
{
    public class UnicornLevelLoader: ILevelLoader
    {
        public void LoadLevel(IDataLevel dataLevel)
        {
            
        }
    }

    public interface ILevelLoader
    {
        public void LoadLevel(IDataLevel dataLevel);
    }
}