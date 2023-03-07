
using UnityEditor;
using UnityEngine;

namespace UnityEditorTools.Align_To_Floor
{
    public static class MenuItems
    {
        [MenuItem("CONTEXT/Transform/Align To Floor With Raycast", false, 1)]
        public static void OPP_AlignToFloors(MenuCommand command) =>
            AlignToFloor.AlignWithRay(command.ToGameObject());


        [MenuItem("CONTEXT/Transform/Align To Floor With Physics Simulation", false, 2)]
        private static void OpenPhysic(MenuCommand command)
        {
            var w = AlignToFloorPhysics.OpenRectWindow();
            w.SimulatePhysics(command.ToGameObject());
        }
    }
}