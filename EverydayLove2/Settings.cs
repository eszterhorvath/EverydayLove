using Newtonsoft.Json;

namespace EverydayLove2
{
	public static class Settings
	{
        private static string _path = "settings.json";

        public static void Initialize()
        {
            var jsonString = File.ReadAllText(_path);
            var s = JsonConvert.DeserializeObject<S>(jsonString);
            HomeBackground = s.Background1;
            SavedBackground = s.Background2;
        }

        public static void UpdateSettings()
        {
            var s = new S
            {
                Background1 = HomeBackground,
                Background2 = SavedBackground
            };
            var jsonString = JsonConvert.SerializeObject(s);
            File.WriteAllText(_path, jsonString);
        }

        public static string HomeBackground { get; set; }

        public static string SavedBackground { get; set; }

        private class S
        {
            public string Background1 { get; set; }
            public string Background2 { get; set; }
        }
    }
}

