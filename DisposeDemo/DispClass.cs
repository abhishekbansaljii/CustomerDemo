using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposeDemo
{
    public class DispClass : IDisposable
    {
        bool disposed = false;
        public DispClass() {
            Console.WriteLine("Memory allocated");
        }

        ~DispClass() {
            Console.WriteLine("Memory Deallocated as Finalize executed..");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    Console.WriteLine("Managed Resource released..");
                }
                Console.WriteLine("UnManaged Resource released..");
                disposed = true;
            }
        }
    }
}
