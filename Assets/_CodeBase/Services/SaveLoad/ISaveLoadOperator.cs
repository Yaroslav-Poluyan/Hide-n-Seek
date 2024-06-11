namespace _CodeBase.Services.SaveLoad
{
    public interface ISaveLoadOperator
    {
        public string MainDataKey { get; }
        public void SaveAll();
        public void LoadAll();
    }
}