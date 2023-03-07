using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityEditorTools.Align_To_Floor
{
    /// <summary>
    /// Struct that defines the object to be simulated in editor mode.
    /// It was developed by Umutcan Bağcı.
    /// </summary>
    [Serializable]
    public struct PhysicsObject
    {
        //Private fields
        private Collider _collider;
        private bool _customCollider;
        private bool _customRigidBody;
        //Public fields
        public Rigidbody rigidBody;
        public GameObject gameObject;
        
        public PhysicsObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
            
            _customRigidBody = !gameObject.TryGetComponent(out rigidBody);
            if(_customRigidBody)
                rigidBody = gameObject.AddComponent<Rigidbody>();
            
            _customCollider = !gameObject.TryGetComponent(out _collider);
            if (!_customCollider) return;
            if(gameObject.TryGetComponent(out MeshRenderer renderer))
            {
                var meshCollider = gameObject.AddComponent<MeshCollider>();
                meshCollider.convex = true;
                _collider = meshCollider;
            }
            else
            {
                _collider = gameObject.AddComponent<BoxCollider>();
            }
        }

        /// <summary>
        /// At the end of the simulation, it removes later added components.
        /// </summary>
        public void StopSimulation()
        {
            if (_customRigidBody)
                Object.DestroyImmediate(rigidBody);
            if (_customCollider) 
                Object.DestroyImmediate(_collider);
        }
    }
}