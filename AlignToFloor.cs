using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityEditorTools.Align_To_Floor
{
    /// <summary>
    /// In a very simple way, this tool reduces the selected object with the Ray and placed it on the ground.
    /// This tool was developed by Umutcan Bağcı with the aim of facilitating level design.
    /// </summary>
    public static class AlignToFloor 
    {
        public static void AlignWithRay(GameObject source)
        {
            float extraHeight = 0;
            int sourceLayer = source.layer;
            
            source.transform.SetLayerAllChildren(LayerMask.NameToLayer ("Ignore Raycast"));
            
            Transform t = source.transform;
            Debug.DrawRay(t.position, Vector3.down, Color.blue,35);
            if (Physics.Raycast(new Ray(t.position, Vector3.down), out RaycastHit r, 10000))
            {
                source.transform.SetLayerAllChildren(sourceLayer);
                Debug.DrawRay(r.point, Vector3.up, Color.red,35);
                
                if (Physics.Raycast(new Ray(r.point, Vector3.up), out RaycastHit rDown, 10000))
                    extraHeight = source.transform.position.y - rDown.point.y;
                
                t.position = new Vector3(r.point.x, r.point.y + extraHeight, r.point.z);
            }

            source.transform.SetLayerAllChildren(sourceLayer);
            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        }
    }
}