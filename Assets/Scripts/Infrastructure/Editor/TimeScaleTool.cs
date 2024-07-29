using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Editor

{
    public class TimeScaleTool : EditorWindow
    {
        private List<float> speeds = new List<float>() { 0f, 0.25f, 0.5f, 1f, 2f, 50f };

        [MenuItem("Tools/TimeScale Tool")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<TimeScaleTool>();
            window.minSize = new Vector2(10f, 20f);
            window.titleContent = new GUIContent("TimeScale Tool");
            window.ShowUtility();
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            Scale = (float)Math.Round(Mathf.Pow(GUILayout.HorizontalSlider(Mathf.Sqrt(Scale), 0, Mathf.Sqrt(80)), 2), 1);
            Scale = EditorGUILayout.FloatField(Scale, GUILayout.MaxWidth(30f));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            for (int i = 0; i < speeds.Count; i++)
            {
                if (GUILayout.Button($"x{speeds[i]}", GUILayout.Width(50), GUILayout.Height(35)))
                {
                    Scale = speeds[i];
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Change FPS count");
            GUILayout.BeginHorizontal();
            Fps = (int)GUILayout.HorizontalSlider(Fps, -1, 300);
            Fps = EditorGUILayout.IntField(Fps, GUILayout.MaxWidth(30f));
            GUILayout.EndHorizontal();
        }

        float Scale
        {
            set
            {
                Time.timeScale = value;
            }
            get
            {
                return Time.timeScale;
            }
        }

        public int Fps
        {
            get
            {
                return Application.targetFrameRate;
            }

            set
            {
                Application.targetFrameRate = value;
            }
        }

        private void OnDestroy()
        {
            if (Scale != 1)
                Scale = 1;
            if (Fps > 0)
                Fps = -1;
        }
    }
}
