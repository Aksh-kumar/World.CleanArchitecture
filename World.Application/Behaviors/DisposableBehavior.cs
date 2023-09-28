using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts;

namespace World.Application.Behaviors
{
    public class DisposableBehavior : IDisposable
    {
        protected internal bool _disposedValue;
        protected internal readonly List<IDisposable> disposableObjects;
        public DisposableBehavior()
        {
            _disposedValue = false;
            disposableObjects = new();
        }

        public DisposableBehavior(params object?[] list) : this()
        {
            foreach (var obj in list)
            {
                if (obj is IDisposable disposeObj)
                {
                    disposableObjects.Add(disposeObj);
                }
            }
        }
        #region Dispose and Destructor
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            if (disposing)
            {
                foreach (IDisposable disposeObj in disposableObjects)
                {
                    disposeObj.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }

        ~DisposableBehavior() => Dispose(false);
        #endregion
    }
}
