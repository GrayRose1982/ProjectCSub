using System.Collections;
using System.Collections.Generic;
using Aircraft;
using UnityEditor;
using UnityEngine;

namespace Aircraft
{
    [CustomEditor(typeof(AircraftCenter))]
    public class AircraftCenterEditor : Editor
    {
        private AircraftCenter center;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (center == null)
                center = target as AircraftCenter;

            if (center == null)
                return;

            GUILayout.Space(20);
            GUILayout.Label("Helper");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Test build aircraft"))
            {
                center.TestBuildAircraft();
            }
            if (GUILayout.Button("Update gun barrel"))
            {
                center.SettingGunBarrels();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}