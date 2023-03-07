using UnityEditor;
using UnityEngine;

namespace UnityEditorTools.Align_To_Floor
{
    /// <summary>
    /// In a very simple way, this tool reducing the selected object with physics laws and placing it on the ground.
    /// This tool was developed by Umutcan Bağcı with the aim of facilitating level design.
    /// </summary>
    public class AlignToFloorPhysics : EditorWindow
    {
        public static AlignToFloorPhysics OpenRectWindow() => GetWindowWithRect<AlignToFloorPhysics>(new Rect(0,0,300,50), false, "Physics", true);
        
        [SerializeField] private PhysicsObject targetObject;
        private bool _physicsIsActive;

        #region Unity Event Functions
        
        /// <summary>
        /// Controls buttons and actions in the window
        /// </summary>
        private void OnGUI()
        {
            if (!_physicsIsActive)
            {
                if (GUILayout.Button("Simulate Physics"))
                    SimulatePhysics(targetObject.gameObject);
            }
            else
            {
                if (GUILayout.Button("Stop Simulate"))
                    StopSimulation();
            }
        }

        /// <summary>
        /// It applies force to the target after the simulation is initiated.
        /// Since the global gravity is 0, it is as if only the target is affected by gravity.
        /// </summary>
        private void Update()
        {
            if (!_physicsIsActive) return;
            Physics.Simulate(Time.fixedDeltaTime);
            if(targetObject is not { rigidBody: null })
                targetObject.rigidBody.AddForce(Vector3.down * 0.4f);
        }

        private void OnDestroy() => StopSimulation();
        private void OnDisable() => StopSimulation();

        #endregion

        /// <summary>
        /// It closes gravity for all objects and starts simulation by targeting a single object.
        /// </summary>
        /// <param name="target"> The target object in which the movement will be simulated </param>
        public void SimulatePhysics(GameObject target)
        {
            Physics.gravity = Vector3.zero;
            targetObject = new PhysicsObject(target);
            Physics.autoSimulation = false;
            _physicsIsActive = true;
        }

        /// <summary>
        /// It brings gravity to its normal values and stops simulation.
        /// </summary>
        private void StopSimulation()
        {
            Physics.gravity = new Vector3(0,9.8f,0);
            Physics.autoSimulation = true;
            _physicsIsActive = false;
            targetObject.StopSimulation();
        }
    }
}