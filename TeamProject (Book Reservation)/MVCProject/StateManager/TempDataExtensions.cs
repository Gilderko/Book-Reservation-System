﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace MVCProject.StateManager
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value, SerialisSettings.GetSettings());
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData[key];
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o, SerialisSettings.GetSettings());
        }
    }
}
