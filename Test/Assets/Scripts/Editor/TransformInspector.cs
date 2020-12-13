using UnityEngine;
using UnityEditor;
using System.Collections;
//using UnityEngine.Localization;

// Reverse engineered UnityEditor.TransformInspector
// Base of code  taken from http://wiki.unity3d.com/index.php?title=TransformInspector&oldid=19215
// Made workaround regarding the use of method LocalizationDatabase.GetLocalizedString(text)
// Set customization the scaleProperty field replaced by "common slider" and changed const FIELD_WIDTH
[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
    // original value private const float FIELD_WIDTH = 212.0f;
    private const float FIELD_WIDTH = 400.0f;
    private const bool WIDE_MODE = true;
    private const float POSITION_MAX = 100000.0f;

    private static GUIContent positionGUIContent = new GUIContent(LocalString("Position")
        , LocalString("The local position of this Game Object relative to the parent."));
    private static GUIContent rotationGUIContent = new GUIContent(LocalString("Rotation")
        , LocalString("The local rotation of this Game Object relative to the parent."));
    private static GUIContent scaleGUIContent = new GUIContent(LocalString("Scale")
        , LocalString("The local scaling of this Game Object relative to the parent."));

    private static string positionWarningText = LocalString("Due to floating-point precision limitations," +
        " it is recommended to bring the world coordinates of the GameObject within a smaller range.");

    private SerializedProperty positionProperty;
    private SerializedProperty rotationProperty;
    private SerializedProperty scaleProperty;

    // It's a Temprorary workaround because in current realize UnityEngine.Localization didn't enter be default
    //private static string LocalString(string text)
    //{
    //    return LocalizationDatabase.GetLocalizedString(text);
    //}
    private static string LocalString(string text) => text;

    public void OnEnable()
    {
        this.positionProperty = this.serializedObject.FindProperty("m_LocalPosition");
        this.rotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
        this.scaleProperty = this.serializedObject.FindProperty("m_LocalScale");
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.wideMode = TransformInspector.WIDE_MODE;
    
        // align field to right of inspector
        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - TransformInspector.FIELD_WIDTH;

        this.serializedObject.Update();

        EditorGUILayout.PropertyField(this.positionProperty, positionGUIContent);
        this.RotationPropertyField(this.rotationProperty, rotationGUIContent);
        
        // original value EditorGUILayout.PropertyField(this.scaleProperty, scaleGUIContent);
        // the scaleProperty field replaced by "common slider"
        float commonScale = this.scaleProperty.vector3Value.x;
        commonScale = EditorGUILayout.Slider("Scale", commonScale, 0, 10f);
        this.scaleProperty.vector3Value = new Vector3(commonScale, commonScale, commonScale);

        if (!ValidatePosition(((Transform)this.target).position))
        {
            EditorGUILayout.HelpBox(positionWarningText, MessageType.Warning);
        }

        this.serializedObject.ApplyModifiedProperties();
    }

    private bool ValidatePosition(Vector3 position)
    {
        if (Mathf.Abs(position.x) > TransformInspector.POSITION_MAX) return false;
        if (Mathf.Abs(position.y) > TransformInspector.POSITION_MAX) return false;
        if (Mathf.Abs(position.z) > TransformInspector.POSITION_MAX) return false;
        return true;
    }

    private void RotationPropertyField(SerializedProperty rotationProperty, GUIContent content)
    {
        Transform transform = (Transform)this.targets[0];
        Quaternion localRotation = transform.localRotation;
        foreach (UnityEngine.Object t in (UnityEngine.Object[])this.targets)
        {
            if (!SameRotation(localRotation, ((Transform)t).localRotation))
            {
                EditorGUI.showMixedValue = true;
                break;
            }
        }

        EditorGUI.BeginChangeCheck();

        Vector3 eulerAngles = EditorGUILayout.Vector3Field(content, localRotation.eulerAngles);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(this.targets, "Rotation Changed");
            foreach (UnityEngine.Object obj in this.targets)
            {
                Transform t = (Transform)obj;
                t.localEulerAngles = eulerAngles;
            }
            rotationProperty.serializedObject.SetIsDifferentCacheDirty();
        }

        EditorGUI.showMixedValue = false;
    }

    private bool SameRotation(Quaternion rot1, Quaternion rot2)
    {
        if (rot1.x != rot2.x) return false;
        if (rot1.y != rot2.y) return false;
        if (rot1.z != rot2.z) return false;
        if (rot1.w != rot2.w) return false;
        return true;
    }
}

