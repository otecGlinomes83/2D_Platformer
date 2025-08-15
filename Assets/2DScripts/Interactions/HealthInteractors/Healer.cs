using Assets._2DScripts.Interactions.HealthInteractors;
using UnityEngine;

namespace Assets._2DScripts.Interactions
{
    public class Healer : HealthImpactor
    {
        public override void Impact(Health health) =>
            health.Heal(_impactValue);
    }
}