using Collectible.Linker;
using UnityEditor;
using UnityEngine;

namespace Editor.Collectible.Linker {
    [CustomEditor(typeof(LinkerPooler))]
    public class LinkerPoolerEditor : UnityEditor.Editor
    {
        private LinkerPooler linkerPooler;

        private void OnEnable()
        {
            linkerPooler = (LinkerPooler) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Save"))
            {
                linkerPooler.SaveLinkerTransforms();
            }
        }
    }
}
