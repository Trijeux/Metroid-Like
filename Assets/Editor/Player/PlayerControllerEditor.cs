using UnityEditor;
using UnityEngine;

namespace Editor.Player
{
    [CustomEditor(typeof(Script.Player.PlayerController))]
    public class PlayerStatsEditor : UnityEditor.Editor
    {
        // Variable pour le Foldout
        private bool _showPlayerSettings = true;
        private bool _showStates = true;
        private bool _showStats = true;

        private bool _showNameString = false;
        private bool _showNameAnimation = false;

        private bool _showDragAndDropComponent = false;
        private bool _showGameObjectUI = false;
        private bool _showScript = false;


        public override void OnInspectorGUI()
        {
            var playerController = (Script.Player.PlayerController)target;

            CreatFoldout("Player Settings", ref _showPlayerSettings);
            if (_showPlayerSettings)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                CreatFoldout("Stats", ref _showStats);
                EditorGUILayout.EndHorizontal();

                if (_showStats)
                {
                    DrawIntField("Life", ref playerController.life);
                    DrawFloatField("Speed", ref playerController.speed);
                    DrawFloatField("Power Jump", ref playerController.powerJump);
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                CreatFoldout("States", ref _showStates);
                EditorGUILayout.EndHorizontal();

                if (_showStates)
                {
                    DrawBoolField("Hit", ref playerController.isHit);
                    DrawBoolField("Dead", ref playerController.isDead);
                    DrawBoolField("Pause", ref playerController.isPause);
                    DrawBoolField("End", ref playerController.isEnd);
                }
            }

            CreatFoldout("Name String", ref _showNameString);
            if (_showNameString)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                CreatFoldout("Animation", ref _showNameAnimation);
                EditorGUILayout.EndHorizontal();
                if (_showNameAnimation)
                {
                    DrawStringField("Run", ref playerController.run);
                    DrawStringField("Ground", ref playerController.ground);
                }
            }

            CreatFoldout("Drag And Drop Component", ref _showDragAndDropComponent);
            if (_showDragAndDropComponent)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                CreatFoldout("UI", ref _showGameObjectUI);
                EditorGUILayout.EndHorizontal();

                if (_showGameObjectUI)
                {
                    DrawGameObjectField("Heart 1", ref playerController.heart1);
                    DrawGameObjectField("Heart 2", ref playerController.heart2);
                    DrawGameObjectField("Heart 3", ref playerController.heart3);
                    DrawGameObjectField("UI In Game", ref playerController.uiInGame);
                    DrawGameObjectField("UI Game Over", ref playerController.uiGameOver);
                    DrawGameObjectField("UI Pause Game", ref playerController.uiPauseGame);
                    DrawGameObjectField("Text GameOver", ref playerController.textGameOver);
                    DrawGameObjectField("Text End Level", ref playerController.textEndLevel);
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                CreatFoldout("Script", ref _showScript);
                EditorGUILayout.EndHorizontal();

                if (_showScript)
                {
                    DrawScriptField("Check Is Ground", ref playerController.checkIsGround);
                    DrawScriptField("Spawn", ref playerController.spawn);
                    DrawScriptField("Dead", ref playerController.dead);
                    DrawScriptField("Kill Enemy", ref playerController.killEnemy);
                    DrawScriptField("Finish Level", ref playerController.finishLevel);
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(playerController);
            }
        }

        #region Behaviors

        private static void CreatFoldout(string label, ref bool value)
        {
            value = EditorGUILayout.Foldout(value, label);
        }

        private static void DrawIntField(string label, ref int value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = EditorGUILayout.IntField(label, value);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawFloatField(string label, ref float value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = EditorGUILayout.FloatField(label, value);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawBoolField(string label, ref bool value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = EditorGUILayout.Toggle(label, value);
            EditorGUILayout.EndHorizontal();
        }
        
        private static void DrawStringField(string label, ref string value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = EditorGUILayout.TextField(label, value);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawGameObjectField(string label, ref GameObject value)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = (GameObject)EditorGUILayout.ObjectField(label, value, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawScriptField<T>(string label, ref T value) where T : MonoBehaviour
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            value = (T)EditorGUILayout.ObjectField(label, value, typeof(T), true);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
