using UnityEngine;

namespace FeedbackEvents
{
    [CreateAssetMenu(fileName = "New Spell FX", menuName = "Feedback/Spell FX", order = 0)]
    public class SpellEffectFeedback : ScriptableObject
    {
        
    }

    public enum SpellStage
    {
        Buildup,
        Climax,
        Dissipation
    }
}