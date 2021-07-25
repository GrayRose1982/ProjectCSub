using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelAirCraft;
using UnityEngine;

namespace Factory
{
    public class Factory : MonoBehaviour
    {
        public string PathParts;
        public Dictionary<TypePart, PartData[]> PartStorage;
        public List<Dictionary<TypePart, PartData>> AllShipCreated;
        //public List<PartData> oldPart;
        //public List<PartData> newPart;

        public Vector2 size = new Vector2(2, 2f);
        public int eachLine = 5;

        [ContextMenu("Load all ship within storage")]
        public void CreateAirCraftBuilder()
        {
            var lastPos = new Vector2(0f, 0f);
            var currentLine = 0;
            for (int i = 0; i < AllShipCreated.Count; i++)
            {
                var ship = new GameObject("Ship");
                ship.transform.SetParent(transform);
                ship.transform.position = lastPos;

                var ab = ship.AddComponent<Builder>();
                var dic = AllShipCreated[i];
                var list = dic.Select(s => s.Value).ToArray();
                ab.StartBuildAircraft(list);

                if (currentLine < eachLine)
                {
                    currentLine++;
                    lastPos.x += size.x;
                }
                else
                {
                    currentLine = 0;
                    lastPos.y += size.y;
                    lastPos.x = 0;
                }
            }
        }



        public void GetNextPart(int i, ref Dictionary<TypePart, PartData> allPart)
        {
            if (i >= PartStorage.Count)
                return;

            var pair = PartStorage.ElementAt(i);
            var count = pair.Value.Length;
            for (int j = 0; j < count; j++)
            {
                allPart.Add(pair.Key, pair.Value[j]);

                if (i >= PartStorage.Count - 1)
                    AllShipCreated.Add(new Dictionary<TypePart, PartData>(allPart));

                GetNextPart(i + 1, ref allPart);
                allPart.Remove(pair.Key);
            }
        }

        [ContextMenu("Load storage")]
        public void LoadPart()
        {
            var allPart = Resources.LoadAll<PartData>(PathParts).ToList();

            for (int i = 0; i < PartStorage.Count; i++)
            {
                var pair = PartStorage.ElementAt(i).Key;
                var list = allPart.FindAll(s => s.TypePart == pair).ToArray();
                PartStorage[pair] = list;
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            var childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        [ContextMenu("Load ship")]
        public void CreateAllAirCraft()
        {
            var sb = new StringBuilder();
            AllShipCreated = new List<Dictionary<TypePart, PartData>>();
            var allPart = new Dictionary<TypePart, PartData>();

            GetNextPart(0, ref allPart);
        }

        //[ContextMenu("Change data")]
        //public void ExchangeData()
        //{
        //    for (int i = 0; i < newPart.Count && i < oldPart.Count; i++)
        //    {
        //        newPart[i].CopyData(oldPart[i]);
        //    }
        //}
    }

    [Serializable]
    public struct PartSave
    {
        public TypePart PathParts;
        public PartData[] Parts;
    }
}