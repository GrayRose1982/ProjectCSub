using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    private static Dictionary<string, GameObject> _dictionaryData;

    private static Dictionary<string, GameObject> dictionary => _dictionaryData?? (_dictionaryData = new Dictionary<string, GameObject>());

    public static GameObject GetObject(string path)
    {
        if (!dictionary.ContainsKey(path))
        {
            var obj = Resources.Load<GameObject>(path);
            dictionary.Add(path,obj);
        }

        return dictionary[path];
    }
}

