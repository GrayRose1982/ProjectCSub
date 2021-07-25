using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEditor;
using UnityEngine;

namespace Factory
{
    [CustomEditor(typeof(PartBuilder))]
    public class PartBuilderEditor : Editor
    {
        private PartBuilder t;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (t == null)
                t = target as PartBuilder;
            if (t == null)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.Label("Editor");

            if (GUILayout.Button("Refresh"))
            {
                t.RefreshShip();
                return;
            }

            t.CurPart.IndexLayer = EditorGUILayout.IntField("Layer: ", t.CurPart.IndexLayer);
            t.CurPart.Rot = EditorGUILayout.FloatField("Rotation: ", t.CurPart.Rot);
            t.CurPart.Scale = EditorGUILayout.FloatField("Scale: ", t.CurPart.Scale);

            t.SetPartRenderer();
        }

        protected virtual void OnSceneGUI()
        {
            if (t == null)
                return;

            if (Event.current.shift)
            {
                var root = t.CurPart.GetLink();
                if (root != null && root.Count > 0)
                    DrawFollowLink(root[0]);

                var next = t.CurPart.GetLink(false);

                if (next != null && next.Count > 0)
                    for (int i = 0; i < next.Count; i++)
                    {
                        var nextLink = next[i];

                        DrawFollowLink(nextLink, i);
                    }
            }
        }

        private void DrawFollowLink(Link link, int indexLink = -1)
        {
            var multi = (t.IsSymmetry ? -1f : 1f);
            var diffPos = Vector2.right * .1f * multi;
            var nextPos = link.Pos;

            nextPos.x *= multi;

            Vector2 posNext = (Vector2)t.transform.position + nextPos + diffPos;
            nextPos = (Vector2)Handles.PositionHandle(posNext, Quaternion.identity) - (Vector2)t.transform.position - diffPos;

            Handles.Label(posNext, link.TypePart.ToString());

            if (indexLink == -1)
            {
                var rot = Quaternion.Euler(0, 0, t.CurPart.Rot * multi);
                t.CurPart.Rot = Handles.RotationHandle(rot, posNext).eulerAngles.z * multi;

                t.Renderer.transform.localPosition = nextPos;
                t.Renderer.transform.localRotation = rot;
            }
            else
            {
                t.Next[indexLink].transform.localPosition = nextPos;
            }

            nextPos.x *= multi;
            link.Pos = nextPos;

            if (link.Connection == TypeConnection.SymmetryLeft || link.Connection == TypeConnection.SymmetryRight)
            {
                var allLink = t.CurPart.GetLink(false);
                var linkNeeded = allLink.FindAll(s => s.TypePart == link.TypePart);
                var other = link.Connection == TypeConnection.SymmetryLeft
                    ? linkNeeded.Find(s => s.Connection == TypeConnection.SymmetryRight)
                    : linkNeeded.Find(s => s.Connection == TypeConnection.SymmetryLeft);

                if(other ==null)
                    return;

                nextPos.x *= -1f;
                other.Pos = nextPos;
            }
        }
    }
}
