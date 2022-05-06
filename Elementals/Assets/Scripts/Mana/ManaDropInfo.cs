using System;
using UnityEngine;

namespace ManaSystem
{
    /// <summary>
    /// data structure to define editor parameters which control the randomness values of a mana particular drop.  If no drop info is specified when mana drop
    /// is called, a default configuration will be used instead.  The default configuration can be edited in scene on the ManaDropper object <see cref="ManaDropper"/>
    /// </summary>
    [Serializable]
    public struct ManaDropInfo
    {
        [SerializeField] internal Vector2 dropPosition;
        [SerializeField] internal RangeI dropAmount;
        [SerializeField] internal RangeF radius;
        [SerializeField] internal RangeF angle;

        public int DropAmount
        {
            set => dropAmount = new RangeI(value, value);
        }

        public float Radius
        {
            set => radius = new RangeF(value, value);
        }

        public int MinDropAmount
        {
            set => dropAmount = new RangeI(value, Mathf.Max(dropAmount.max, value));
        }

        public int MaxDropAmount
        {
            set => dropAmount = new RangeI(Mathf.Min(value, dropAmount.min), value);
        }
       
       
        public static ManaDropInfo CopyToPosition(Vector2 position, ManaDropInfo dropInfo)
        {
            return new ManaDropInfo()
            {
                dropPosition = position,
                dropAmount =  dropInfo.dropAmount,
                radius = dropInfo.radius,
                angle = dropInfo.angle
            };
        }

        public ManaDropInfo ToPosition(Vector2 position)
        {
            return CopyToPosition(position, this);
        }
        private void InitDrops(int amount, Vector2[] pnts)
        {
            for (int i = 0; i < amount + 1; i++)
            {
                var pnt = dropPosition;
                var angle = this.angle.GetRand();
                var rot = Quaternion.Euler(0, 0, angle);
                var dist = this.radius.GetRand();
                var offset = rot * (Vector2.right * dist);
                pnts[i] = pnt + (Vector2) offset;
            }
        }

        public Vector2[] GetDrops()
        {
            int amount = dropAmount.GetRand();
            var pnts = new Vector2[amount+1];
            InitDrops(amount, pnts);
            return pnts;
        }

        public int GetDropsNonAlloc(Vector2[] dropPoints)
        {
            if (dropPoints == null)
            {
                dropPoints = GetDrops();
                return dropPoints.Length;
            }
            int amount = Mathf.Min( dropAmount.GetRand(), dropPoints.Length);
            InitDrops(amount, dropPoints);
            return amount;
        }
        
        
        public ManaDropInfo(Vector2 dropPosition, int amount, float radius) : this(dropPosition, 
            amount,amount, 
            radius, radius,
            0, 360) {}
        public ManaDropInfo(Vector2 dropPosition, int minAmount,int maxAmount, float radius) : this(dropPosition, 
            minAmount,maxAmount, 
            radius, radius,
            0, 360) {}
        public ManaDropInfo(Vector2 dropPosition,
            int minDropAmount, int maxDropAmount, 
            float minRadius, float maxRadius, 
            float minAngle, float maxAngle) 
        {
            this.dropPosition = dropPosition;
            this.dropAmount = new RangeI(minDropAmount, maxDropAmount);
            this.radius = new RangeF(minRadius, maxRadius);
            this.angle = new RangeF(minAngle, maxAngle);
        }
    }
}