using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    static class OdeManager
    {
        static int counter;
        static readonly object gate = new object();

        internal static IDisposable ReserveEngine()
        {
            lock (gate)
            {
                if (counter++ == 0)
                {
                    Engine.Init();
                }
            }
            return Disposable.Create(() =>
            {
                lock (gate)
                {
                    if (--counter == 0)
                    {
                        Engine.Close();
                    }
                }
            });
        }
    }
}
