using UnityEngine;

namespace Kamgam.BikeRacing25D
{
    /// <summary>
    /// Implementation base for ILevel. Used by the demo levels.<br />
    /// The Level object is reponsible for initializing everything the level needs.
    /// Once the initialization is done IsReady() should return true.
    /// </summary>
	public class Level : MonoBehaviour, ILevel
	{
		public Camera Camera;
        public Transform BikeSpawnPosition;
        public Goal Goal;
        public Light Light;
        public SpriteRenderer Sky;

        protected bool isReady;

        public Camera GetCamera()
        {
            return Camera;
        }

        public Transform GetBikeSpawnPosition()
        {
            return BikeSpawnPosition;
        }

        public Goal GetGoal()
        {
            return Goal;
        }

        void Awake()
        {
            isReady = false;
            GetCamera().gameObject.SetActive(false);
        }

        public void InitAfterLoad()
        {
            
            // Nothing special to prepare in this level, thus it's ready immediately.
            isReady = true;
        }

        public bool IsReady()
        {
            return isReady;
        }
        
    }
}

