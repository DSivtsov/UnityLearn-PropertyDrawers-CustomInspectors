# UnityLearn-PropertyDrawers-CustomInspectors
Code files for Unity Learn "Property Drawers and Custom Inspectors" updated for 2019.4.14f1 (2019 LTS)

In Test\Assets\Scripts\Editor updated MakeUIImage.cs (must put in Editor folder):
- Updated coresponding to obsolete some members in Class TextureImporter
- Made Console output after Import is finished
- Added comments, deleted old commented code

In Test/Assets/Scripts/Editor/ChangeScaleBySlider.cs (must put in Editor folder):
- Created script which customize Scale properties of Transform component for every object in Editor and change it value through the property Editor.serializedObject that demands to use methods for SerializedObject & SerializedProperty objects, but have some advantage - "Automatic handling of multi-object editing, undo, and Prefab overrides".
Note. The rotation field property drawer is handled a bit differently, because for rotation field the internal store format is Quaternion, but the Editor showing the euler rotation format. Therefore the use of DrawPropertiesExcluding(...) (and DrawDefaultInspector() also) doesn't give possibility to receive standard GUI format. It is required to completely rewrite the graphical interface for the Transform component, if you what to receive GUI one to one with original(see next Example).

Test/Assets/Scripts/Editor/TransformInspector.cs
- Script created by reverse engineered UnityEditor.TransformInspector. This is currently a much better alternative to using Editor.DrawDefaultInspector in your custom Transform inspector class since DrawDefaultInspector does not preserve the inspector's unique GUI layout.
- Script Base of code  taken from http://wiki.unity3d.com/index.php?title=TransformInspector&oldid=19215
- Made workaround regarding the use of method LocalizationDatabase.GetLocalizedString(text)
- Set customization the scaleProperty field replaced by "common slider" and changed const FIELD_WIDTH