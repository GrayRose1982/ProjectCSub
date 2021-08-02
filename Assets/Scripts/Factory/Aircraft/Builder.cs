using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Factory
{
	public class Builder : MonoBehaviour
	{
		[SerializeField] private PartData[] cache;
		[Header("Part builder")]
		public PartBuilder BodyBuilder;

		public PartData[] Cache => cache;

        public void Init()
		{
			StartBuildAircraft();
        }

        public void StartBuildAircraft(PartData[] newParts = null)
        {
            if (newParts != null)
                cache = newParts;

            BodyBuilder = GetComponent<PartBuilder>();
            if (!BodyBuilder)
                BodyBuilder = gameObject.AddComponent<PartBuilder>();
            
            BodyBuilder.CurPart = cache.GetPath(TypePart.Body);
            BodyBuilder.AddSub(false, cache);
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            BodyBuilder.UpdatePositionAndSprite();
        }

        public List<Transform> GetGunBarrels()
        {
            var gunBarrels = new List<Transform>();

            BodyBuilder.GetGunBarrel(gunBarrels);
            return gunBarrels;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (Event.current.alt)
            {

            }
        }
#endif
    }
}

