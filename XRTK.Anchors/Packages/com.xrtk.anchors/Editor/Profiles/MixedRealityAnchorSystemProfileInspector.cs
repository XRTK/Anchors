// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using XRTK.Definitions.Anchors;
using XRTK.Services;

namespace XRTK.Editor.Profiles.TeleportSystem
{
    [CustomEditor(typeof(AnchorSystemProfile))]
    public class MixedRealityAnchorSystemProfileInspector : MixedRealityServiceProfileInspector
    {
        private SerializedProperty spatialManagerGameObject;

        protected override void OnEnable()
        {
            base.OnEnable();

            spatialManagerGameObject = serializedObject.FindProperty(nameof(spatialManagerGameObject));
        }

        public override void OnInspectorGUI()
        {
            RenderHeader("The anchor system profile defines behaviour for the anchor system.");

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(spatialManagerGameObject);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck() &&
                MixedRealityToolkit.IsInitialized)
            {
                EditorApplication.delayCall += () => MixedRealityToolkit.Instance.ResetProfile(MixedRealityToolkit.Instance.ActiveProfile);
            }

            base.OnInspectorGUI();
        }
    }
}
