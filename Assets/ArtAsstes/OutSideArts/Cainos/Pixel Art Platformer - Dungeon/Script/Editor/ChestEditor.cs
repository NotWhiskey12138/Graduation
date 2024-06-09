using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Whiskey.Interaction;

namespace Cainos.PixelArtPlatformer_Dungeon
{

    [CustomEditor(typeof(Chest))]
    public class ChestEditor : Editor
    {
        private Chest instance;

        private PropertyField IsOpened;

        private IInteractable interactable;

        private void OnEnable()
        {
            instance = target as Chest;
        }


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 10);
            if (GUILayout.Button("Open")) instance.Open(interactable);
            if (GUILayout.Button("Close")) instance.Close();
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(instance);
                EditorSceneManager.MarkSceneDirty(instance.gameObject.scene);
            }
        }
    }
}
