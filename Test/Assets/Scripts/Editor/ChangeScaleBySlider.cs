using UnityEngine;
using UnityEditor;
using System.Collections;

// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and Prefab overrides.
[CustomEditor(typeof(Transform))]
[CanEditMultipleObjects]
public class ChangeScaleBySlider : Editor
{
    const string scalePropertySerializedName = "m_LocalScale";
    SerializedProperty scaleProperty;

    void OnEnable()
    {
        // Setup the SerializedProperties
        this.scaleProperty = this.serializedObject.FindProperty(scalePropertySerializedName);
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // Show the default GUI controls except scaleProperty
        DrawPropertiesExcluding(serializedObject, scalePropertySerializedName);

        // Show the custom GUI controls
        float commonScale = this.scaleProperty.vector3Value.x;
        commonScale = EditorGUILayout.Slider("Scale", commonScale, 0, 10f);
        this.scaleProperty.vector3Value = new Vector3(commonScale, commonScale, commonScale);

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}

