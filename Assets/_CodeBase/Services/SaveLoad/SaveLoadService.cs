namespace _CodeBase.Services.SaveLoad
{
    public static class SaveLoadService
    {
        public static void Save<T>(string filenam, T data)
        {
            /*
            ES3.Save<T>(filenam, data);
        */
        }

        public static T Load<T>(string filename)
        {
            throw new System.NotImplementedException();
            //return !HasSave(filename) ? default : ES3.Load<T>(filename);
        }

        public static bool HasSave(string saveKey)
        {
            throw new System.NotImplementedException();
            //return ES3.KeyExists(saveKey);
        }
    }
}