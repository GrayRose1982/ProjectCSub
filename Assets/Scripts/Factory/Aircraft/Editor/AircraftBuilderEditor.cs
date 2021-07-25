using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Factory
{
    [CustomEditor(typeof(Builder))]
    public class AircraftBuilderEditor : Editor
    {
        private Builder t;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (t == null)
                t = target as Builder;

            //if (t?.Body != null)
            //    t.Renderer.sprite = t.CurPart.Main;

            if (t == null)
                return;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.Label("Builder");

            //_typeNext = (TypePart)EditorGUILayout.EnumPopup("Type part", _typeNext);
            //isSymmetry = EditorGUILayout.Toggle("isSymmetry", isSymmetry);

            if (GUILayout.Button("Refresh"))
                t.Init();

         
            if (GUILayout.Button("Update position"))
                t.UpdatePosition();

            if (GUILayout.Button("Create link"))
            {
            }
        }

        protected virtual void OnSceneGUI()
        {
            if (t == null)
                return;

            //if (t.CurPart == null)
            //    return;

            //var links = t.CurPart.Links;

            //for (int i = 0; i < links.Count; i++)
            //{
            //    if (Event.current.alt)
            //    {
            //        DrawPath(i, links[i]);

            //        if (links[i].TypeLink == TypeLink.Symmetry)
            //        {
            //            var curLinkPos = links[i].Pos;
            //            curLinkPos.x = -curLinkPos.x;
            //            i++;
            //            links[i].Pos = curLinkPos;
            //            t.transform.GetChild(i).GetComponent<PartBuilder>().IsSymmetry = true;
            //            DrawPath(i, links[i]);
            //        }
            //    }
            //}

            //ShowLinkInfo();
            //if (Event.current.shift)
            //{
            //    Vector2 positionStart = new Vector2(t.transform.position.x + t.Point.Start, 0);
            //    t.Point.Start = (UnityEditor.Handles.PositionHandle(positionStart, Quaternion.identity) - t.transform.position).x;
            //    UnityEditor.Handles.Label(positionStart, "Left barriel");

            //    Vector2 positionEnd = new Vector2(t.transform.position.x + t.Point.End, 0);
            //    t.Point.End = (UnityEditor.Handles.PositionHandle(positionEnd, Quaternion.identity) - t.transform.position).x;
            //    UnityEditor.Handles.Label(positionEnd, "Right barriel");
            //}

            //if (_isConservations)
            //    for (int i = 0; i < t.Conservations.Count; i++)
            //    {
            //        var c = t.Conservations[i];
            //        c.Position = UnityEditor.Handles.PositionHandle(c.Position, Quaternion.identity);
            //        UnityEditor.Handles.Label(c.Position, c.Key.ToString());
            //    }
        }
    }


}
