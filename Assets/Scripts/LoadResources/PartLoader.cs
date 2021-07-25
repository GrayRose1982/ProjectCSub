using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModelAirCraft;
using UnityEngine;

public class PartLoader
{
    private static List<PartData> _parts;
    private static Dictionary<string, Sprite> _sprites;

    private static Dictionary<string, Sprite> Sprites => _sprites ?? (_sprites = new Dictionary<string, Sprite>());

    private static List<PartData> Parts
    {
        get { return _parts ?? (_parts = Resources.LoadAll<PartData>("PartData").ToList()); }
    }

    public static List<PartData> GetPath(TypePart type)
    {
        return Parts.FindAll(p => p.TypePart == type);
    }

    public static List<string> GetPartsName()
    {
        return Parts.Select(p => p.Key).ToList();
    }

    public static Sprite GetSprite(string path)
    {
        if (!Sprites.ContainsKey(path))
        {
            Sprites.Add(path, Resources.Load<Sprite>(path));
        }

        return Sprites[path];
    }
}
