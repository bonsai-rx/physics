using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public struct CollisionHandlerKey : IEquatable<CollisionHandlerKey>
    {
        public string Material1;
        public string Material2;

        public CollisionHandlerKey(string material1, string material2)
        {
            Material1 = material1;
            Material2 = material2;
        }

        public bool Equals(CollisionHandlerKey other)
        {
            return (Material1 == other.Material1 && Material2 == other.Material2) ||
                   (Material1 == other.Material2 && Material2 == other.Material1);
        }

        public override bool Equals(object obj)
        {
            if (obj is CollisionHandlerKey)
            {
                return Equals((CollisionHandlerKey)obj);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Material1) ^
                   EqualityComparer<string>.Default.GetHashCode(Material2);
        }
    }
}
