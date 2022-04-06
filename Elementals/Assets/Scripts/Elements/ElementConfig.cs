using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(menuName = "Element Config")]
    public class ElementConfig : ScriptableObject
    {
        private static ElementConfig _instance;

        public static ElementConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ElementConfig>("ElementConfig");
                }
                return _instance;
            }
        }
    }
}
