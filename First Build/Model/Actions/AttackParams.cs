using System;

namespace First_Build
{
    public class AttackParams
    {
        public float M;
        public float C;
        public float S;
        public float apCost;
        public Character attacker;

        public AttackParams(float M, float C, float S, float apCost)
        {
            this.M = M;
            this.C = C;
            this.S = S;
            this.apCost = apCost;
        }

        public bool AttackerKnown
        {
            get
            {
                if (attacker != null) { return true; } else { return false; }
            }
        }
        public float E
        {
            get
            {
                return Convert.ToSingle(M * Math.Pow(C, 2));
            }
        }
        public bool IsActive
        {
            get
            {
                if (E <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public float EperSquare
        {
            get
            {
                return Convert.ToSingle(E / S);
            }
        }
        public void GetAbsorbed(float E)
        {
            if (E > this.E) { C = 0; return; }

            var n = (float)Math.Sqrt((this.E - E) / M);

            C = n;
        }
        public override string ToString()
        {
            return E + ", " + EperSquare;
        }
    }
}
