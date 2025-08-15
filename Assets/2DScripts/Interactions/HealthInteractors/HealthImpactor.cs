using UnityEngine;
using UnityEngine.Video;

namespace Assets._2DScripts.Interactions.HealthInteractors
{
    public abstract class HealthImpactor : MonoBehaviour
    {
        [SerializeField] protected float _impactValue = 5f;

        public float ImpactValue => _impactValue;

        public abstract void Impact(Health health);
    }
}