using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Items = array
        };
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Items = list.ToArray()
        };
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Items = array
        };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public static class JsonTool
{
    public static T JsonDeserial<T>(string jsonData)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static Dictionary<string, object> JsonToDictionary(string jsonData)
    {
        try
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int JsonToInt(string jsonData)
    {
        try
        {
            return JsonConvert.DeserializeObject<int>(jsonData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static float JsonToFloat(string jsonData)
    {
        try
        {
            return JsonConvert.DeserializeObject<float>(jsonData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static bool JsonToBool(string jsonData)
    {
        try
        {
            return JsonConvert.DeserializeObject<bool>(jsonData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
