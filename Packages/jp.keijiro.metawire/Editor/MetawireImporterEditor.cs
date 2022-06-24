using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace Metawire {

[CustomEditor(typeof(MetawireImporter))]
sealed class MetawireImporterEditor : ScriptedImporterEditor
{
    SerializedProperty _shape;
    SerializedProperty _line;
    SerializedProperty _quad;
    SerializedProperty _circle;
    SerializedProperty _box;
    SerializedProperty _ticks;
    SerializedProperty _grid;
    SerializedProperty _readWrite;

    public override void OnEnable()
    {
        base.OnEnable();
        _shape = serializedObject.FindProperty("_shape");
        _line = serializedObject.FindProperty("_line");
        _quad = serializedObject.FindProperty("_quad");
        _circle = serializedObject.FindProperty("_circle");
        _box = serializedObject.FindProperty("_box");
        _ticks = serializedObject.FindProperty("_ticks");
        _grid = serializedObject.FindProperty("_grid");
        _readWrite = serializedObject.FindProperty("_readWrite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_shape);

        switch ((Shape)_shape.enumValueIndex)
        {
            case Shape.Line : EditorGUILayout.PropertyField(_line); break;
            case Shape.Quad : EditorGUILayout.PropertyField(_quad); break;
            case Shape.Circle : EditorGUILayout.PropertyField(_circle); break;
            case Shape.Box : EditorGUILayout.PropertyField(_box); break;
            case Shape.Ticks : EditorGUILayout.PropertyField(_ticks); break;
            case Shape.Grid : EditorGUILayout.PropertyField(_grid); break;
        }

        EditorGUILayout.PropertyField(_readWrite);

        serializedObject.ApplyModifiedProperties();
        ApplyRevertGUI();
    }

    [MenuItem("Assets/Create/Metawire")]
    public static void CreateNewAsset()
      => ProjectWindowUtil.CreateAssetWithContent("New Metawire.metawire", "");
}

} // namespace Metawire
