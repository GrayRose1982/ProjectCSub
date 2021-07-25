using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Factory
{
    public class PartBuilder : MonoBehaviour
    {
        public PartData CurPart;
        public PartBuilder Connect;
        public List<PartBuilder> Next = new List<PartBuilder>();

        public SpriteRenderer Renderer;

        public bool IsSymmetry;

        void Reset()
        {
            var render = new GameObject("Renderer");
            render.transform.localPosition = Vector3.zero;
            Renderer = render.AddComponent<SpriteRenderer>();
            render.transform.SetParent(transform);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            var mainPos = transform.position;
            Gizmos.DrawWireCube(mainPos, Vector3.one * .1f);
            for (int i = 0; i < CurPart.Links.Count; i++)
            {
                var link = CurPart.Links[i];
                var newPos = link.Pos;
                newPos.x *= IsSymmetry ? -1 : 1;
                var endPos = (Vector2)mainPos + newPos;
                var size = link.Root ? .05f : .08f;

                Gizmos.color = link.Root ? Color.red : Color.blue;
                Gizmos.DrawLine(mainPos, endPos);
                Gizmos.DrawWireCube(endPos, Vector3.one * .1f);
            }
        }
#endif

        #region For build aircraft

        public void AddSub(bool isSymmetry = false, params PartData[] partData)
        {
            Clear();
            var links = CurPart.Links.FindAll(l => !l.Root);

            for (int i = 0; i < links.Count; i++)
            {
                var nextLink = links[i];
                var isStartSymmetry = links[i].Connection == TypeConnection.SymmetryLeft;
                var newSymmetry = isSymmetry || isStartSymmetry;
                var data = partData.GetPath(nextLink.TypePart);//(PartLoader.GetPath(nextLink.TypePart));

                if (data == null)
                    continue;

                var posNext = nextLink.Pos;
                posNext.x = posNext.x * (newSymmetry && !isStartSymmetry ? -1 : 1);
                var go = new GameObject(nextLink.TypePart.ToString(), typeof(PartBuilder));

                go.transform.SetParent(transform);
                go.transform.localPosition = posNext;

                var partBuilder = go.GetComponent<PartBuilder>();
                // TODO: Need to set symmetry first
                partBuilder.IsSymmetry = newSymmetry;
                partBuilder.Connect = this;

                partBuilder.SetPartData(data);
                partBuilder.AddSub(newSymmetry, partData);

                Next.Add(partBuilder);
            }
        }

        public void SetPartRenderer()
        {
            Renderer.sprite = CurPart.Main;
            var root = CurPart.GetLink();

            if (root != null && root.Count > 0)
            {
                var pos = root[0].Pos;
                pos.x *= IsSymmetry ? -1 : 1;
                Renderer.transform.localPosition = pos;
            }
            else
                Renderer.transform.localPosition = Vector2.zero;

            Renderer.transform.localScale = Vector3.one * CurPart.Scale;
            Renderer.transform.localRotation = Quaternion.Euler(0, 0, CurPart.Rot * (IsSymmetry ? -1 : 1)); ;
            Renderer.sortingOrder = CurPart.IndexLayer;
            Renderer.flipX = IsSymmetry;
        }

        public void SetPartData(PartData newPart)
        {
            CurPart = newPart;
            SetPartRenderer();
        }

        public void Clear()
        {
            if (Next == null)
                Next = new List<PartBuilder>();
            var childCount = Next.Count;
            for (int i = 0; i < childCount; i++)
            {
                if (Next[i])
                    DestroyImmediate(Next[i].gameObject);
            }

            Next.Clear();
        }

        public void UpdatePositionAndSprite()
        {
            SetPartRenderer();
            Next.ForEach(pb => pb.UpdatePositionAndSprite());
        }

        //public void UpdateOrder()
        //{
        //    Renderer.flipX = IsSymmetry;
        //    for (int i = 0; i < Next.Count; i++)
        //    {
        //        Next[i].UpdateOrder();
        //    }
        //}

        public void RefreshShip()
        {
            if (Connect == null)
                GetComponent<Builder>().Init();
            else Connect.RefreshShip();
        }

        #endregion

        #region Prepare to fight

        public void GetGunBarrel(List<Transform> listGunBarrel)
        {
            if (CurPart.TypePart == TypePart.Gun)
            {
                listGunBarrel.Add(Renderer.transform);
                SetGunBarrel();
            }

            Next.ForEach(part =>part.GetGunBarrel(listGunBarrel));
        }

        public void SetGunBarrel()
        {
            var childCount = Renderer.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                    DestroyImmediate(Renderer.transform.GetChild(i).gameObject);
            }

            var diffSize = Renderer.sprite.bounds.size.y;
            var initPos = Vector3.zero;
            initPos.y = -diffSize / 2;
            var gunBarrel = ResourceLoader.GetObject(((WeaponData) CurPart).GunBarrels);
            var newGunBarrel = Instantiate(gunBarrel, Renderer.transform) ;
            newGunBarrel.transform.localPosition = initPos;
            newGunBarrel.transform.localRotation = Quaternion.identity;
        }

        #endregion
    }
}
