using UnityEngine;

namespace Elementals
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public bool DataFields { get; set; } = true;
        public bool FlexibleFields { get; set; } = true;
        public bool Bound { get; set; } = true;
        public bool Round { get; set; } = true;
        public MinMaxAttribute(float min, float max)
        {
            this.Min = min;
            this.Max = max;
            MinLabel = "Min";
            MaxLabel = "Max";
        }

        public MinMaxAttribute(float min, float max, string minLabel, string maxLabel)
        {
            this.Min = min;
            this.Max = max;
            this.MinLabel = minLabel;
            this.MaxLabel = maxLabel;
        }

        public string MinLabel { get; set; }

        public string MaxLabel { get; set; }

        public float Max { get; set; }

        public float Min { get; set; }
    }
}