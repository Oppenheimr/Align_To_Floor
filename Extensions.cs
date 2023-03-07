using UnityEditor;
using UnityEngine;

namespace UnityEditorTools.Align_To_Floor
{
    public static class Extensions
    {
        /// <summary>
        /// Turns the object of the "MenuCommand" class into "GameObject" and returns.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static GameObject ToGameObject(this MenuCommand command)
        {
            SerializedObject sObject = new SerializedObject(command.context);
            return ((Component)sObject.targetObject).gameObject;
        }
        
        /// <summary>
        /// It changes the layers of all sub -objects in the object.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="layer"> Target Layer </param>
        public static void SetLayerAllChildren(this Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.layer = layer;
                if(child.childCount > 0) SetLayerAllChildren(child, layer);
            }
        }
    }
}