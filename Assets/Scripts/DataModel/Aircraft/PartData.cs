using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
    //[CreateAssetMenu(fileName = "Data", menuName = "PartData/Part", order = 1)]
    public class PartData: ScriptableObject
    {
        [Header("Specification")]
        public string Key;
        public Sprite Main;
        public TypePart TypePart;
        public float Rot = 0;
        public float Scale = 1;
        public int IndexLayer;

        [SerializeField] protected List<Link> _links;

        public List<Link> Links => _links ?? (_links = new List<Link>());

        public List<Link> GetLink(bool isRoot = true)
        {
            return Links.FindAll(s => !(s.Connection == TypeConnection.Root ^ isRoot));
        }

        public void CopyData(PartData oldPart)
        {
            Main = oldPart.Main;
            TypePart = oldPart.TypePart;
            Rot = oldPart.Rot;
            Scale = oldPart.Scale;
            IndexLayer = oldPart.IndexLayer;
            _links = oldPart.Links;
        }
    }

    [Serializable]
    public class Link
    {
        public TypeConnection Connection;
        public TypePart TypePart;
        public Vector2 Pos;
        public bool Root => Connection == TypeConnection.Root;

        public override string ToString()
        {
            return $"{TypePart.ToString()}";
        }
    }
}
