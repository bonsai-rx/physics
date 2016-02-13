using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CollisionHandlerCollection : KeyedCollection<CollisionHandlerKey, CollisionHandler>
    {
        protected override CollisionHandlerKey GetKeyForItem(CollisionHandler item)
        {
            return new CollisionHandlerKey(item.Material1, item.Material2);
        }
    }
}
