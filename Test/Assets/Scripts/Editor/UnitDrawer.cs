using UnityEditor;
using UnityEngine;

namespace BitStrap
{
    [CustomPropertyDrawer(typeof(UnitAttribute))]
    public class UnitDrawer : PropertyDrawer
    {
        float widthGUIStyle,widthFromEditorStyle;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnitAttribute labelAttribute = attribute as UnitAttribute;
            EditorGUI.PropertyField(position, property, label);
            
            if (Event.current.type != EventType.Repaint)
            {
                //((GUIStyle)"Label").CalcMinMaxWidth(label, out widthGUIStyle, out _ );
                widthFromEditorStyle = EditorStyles.label.CalcSize(label).x;
                //Debug.Log($"Event:{Event.current.type}: {widthFromEditorStyle} vs {widthGUIStyle}");
            }
            //((GUIStyle)"Label").CalcMinMaxWidth(label, out widthGUIStyle, out _);
            //Debug.Log($"Event:{Event.current.type}: {EditorStyles.label.CalcSize(label).x} vs {widthGUIStyle}");
            position.x = position.x + widthFromEditorStyle + 0f;
            GUI.Label(position, labelAttribute.label, labelAttribute.labelStyle);
        }
    }
}