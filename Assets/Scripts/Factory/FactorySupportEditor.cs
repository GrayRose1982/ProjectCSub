using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Factory
{
    [CustomEditor(typeof(FactorySupporter))]
    public class FactorySupportEditor : Editor
    {
        private FactorySupporter f;
        public override void OnInspectorGUI()
        {
            if (f == null)
                f = target as FactorySupporter;
            if (f == null)
                return;
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear"))
            {
                f.F.Clear();
            }
            if (GUILayout.Button("Load storage"))
            {
                f.F.LoadPart();
            }
            if (GUILayout.Button("Load all ship within storage"))
            {
                f.F.CreateAirCraftBuilder();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}