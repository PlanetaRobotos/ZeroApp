using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Infrastructure.Editor
{
	public class Shortcuts : EditorWindow
	{
		#region Init

		[MenuItem("Tools/Shortcuts")]
		private static void LevelEditorWindow()
		{
			EditorWindow window = GetWindow<Shortcuts>();
			window.titleContent = new GUIContent("Shortcuts");
			window.minSize = new Vector2(65, 30);
		}

		private void NewLevelEditorWindow()
		{
			EditorWindow window = CreateInstance<Shortcuts>();
			window.titleContent = new GUIContent("Shortcuts");
			window.minSize = new Vector2(65, 30);
			window.Show();
		}
		#endregion

		public Object[] Components = { };

		private Vector2 scrollPos;

		private Rect EMPTYRECT = new Rect(0, 0, 1, 1);
		private bool readyToDrag = false;

		private void OnGUI()
		{
			ScriptableObject target = this;
			SerializedObject so = new SerializedObject(target);
			SerializedProperty objectsProperty = so.FindProperty("Components");
			Dictionary<Rect, Object> fields = new Dictionary<Rect, Object>();

			DropAreaGUI();

			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

			while (objectsProperty.NextVisible(true))
			{
				if (objectsProperty.propertyType == SerializedPropertyType.ArraySize)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Label("Size", GUILayout.Width(30));
					EditorGUILayout.PropertyField(objectsProperty, GUIContent.none, true, GUILayout.MinWidth(20));
					GUILayout.EndHorizontal();

				}
				if (objectsProperty.propertyType == SerializedPropertyType.ObjectReference)
				{
					GUILayout.BeginHorizontal();
					if (objectsProperty.objectReferenceValue != null)
					{
						EditorGUILayout.PropertyField(objectsProperty, GUIContent.none, true);
					}
					else
					{
						EditorGUILayout.PropertyField(objectsProperty, GUIContent.none, true, GUILayout.MaxWidth(24));
					}
					GUILayout.EndHorizontal();
					Rect rect = GUILayoutUtility.GetLastRect();
					if (rect != EMPTYRECT)
						fields.Add(GUILayoutUtility.GetLastRect(), objectsProperty.objectReferenceValue);
				}
			}
			so.ApplyModifiedProperties();

			EditorGUILayout.EndScrollView();

			#region DragAndDrop
			Event e = Event.current;

			if (e.button == 0 && e.type == EventType.Used)
			{
				readyToDrag = true;
			}
			if (e.type == EventType.MouseUp)
			{
				readyToDrag = false;
			}
			if (e.type == EventType.MouseDrag && readyToDrag)
			{
				var t = fields.FirstOrDefault(r => r.Key.Contains(e.mousePosition));
				if (t.Key != Rect.zero && t.Value)
				{
					DragAndDrop.PrepareStartDrag();
					DragAndDrop.objectReferences = new Object[] { t.Value };
					if (e.alt)
					{
						Components[fields.ToList().IndexOf(t)] = null;
					}
					DragAndDrop.StartDrag("Dragging");
				}
				readyToDrag = false;
			}
			#endregion

			#region ContextMenu
			if (e.type == EventType.ContextClick)
			{
				GenericMenu menu = new GenericMenu();
				menu.AddItem(new GUIContent("AddSelected"), false, AddSelected);
				menu.AddItem(new GUIContent("SelectAll"), false, SelectAll);
				menu.AddItem(new GUIContent("Clear"), false, Clear);
				menu.AddItem(new GUIContent("ClearContent"), false, ClearContent);
				menu.AddSeparator(string.Empty);
				menu.AddItem(new GUIContent("SetDirtys"), false, SetDirtys);
				menu.AddSeparator(string.Empty);
				menu.AddItem(new GUIContent("NewWindow"), false, NewLevelEditorWindow);

				menu.ShowAsContext();
				e.Use();
			}
			#endregion

		}

		public void DropAreaGUI()
		{
			Event evt = Event.current;

			Rect drop_area = new Rect(Vector2.zero, position.size);
			GUI.Box(drop_area, GUIContent.none, GUIStyle.none);

			switch (evt.type)
			{
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if (!drop_area.Contains(evt.mousePosition))
						return;

					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

					if (evt.type == EventType.DragPerform)
					{
						DragAndDrop.AcceptDrag();
						ArrayUtility.AddRange(ref Components, DragAndDrop.objectReferences);
					}
					break;
			}
		}

		#region Utilities

		public void AddSelected()
		{
			foreach (var obj in Selection.objects)
				ArrayUtility.Add(ref Components, obj);
		}
		public void SelectAll()
		{
			Selection.objects = Components;
		}
		public void Clear()
		{
			ArrayUtility.Clear(ref Components);
		}
		public void ClearContent()
		{
			Components = new Object[Components.Length];
		}

		public void SetDirtys()
		{
			Components.ToList().ForEach(x => EditorUtility.SetDirty(x));
		}

		#endregion
	}
}
