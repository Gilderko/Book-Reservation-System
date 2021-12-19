using Newtonsoft.Json;

namespace MVCProject.StateManager
{
    public static class SerialisSettings
    {
        private static JsonSerializerSettings _settings = null;

        public static JsonSerializerSettings GetSettings()
        {
            if (_settings == null)
            {
                _settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            }

            return _settings;
        }
    }
}
