using System;
using System.Collections.Generic;

namespace First_Build
{

    public class AttackType : IEquatable<AttackType>
    {
        public readonly string name;

        public AttackType(string name)
        {
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AttackType);
        }

        public bool Equals(AttackType other)
        {
            return other != null &&
                    name == other.name;
        }

        public override int GetHashCode()
        {
            return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
        }

        public static bool operator ==(AttackType left, AttackType right)
        {
            return EqualityComparer<AttackType>.Default.Equals(left, right);
        }

        public static bool operator !=(AttackType left, AttackType right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
