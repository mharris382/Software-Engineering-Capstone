using System;
using UnityEngine;

namespace FeedbackEvents
{
    [CreateAssetMenu(fileName = "FeedbackEventLibrary", menuName = "Feedback/Library/FeedbackEventLibrary", order = 0)]
    public class FeedbackEventLibrary : ScriptableObject
    {
        
        
        
        [Serializable]
        public class FeedbackEvent
        {
            public string name;
            
        }
    }


    
    
}