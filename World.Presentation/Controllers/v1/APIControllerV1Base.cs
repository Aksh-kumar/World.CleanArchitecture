using Microsoft.AspNetCore.Mvc;

namespace World.Presentation.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class APIControllerV1Base : ControllerBase, IDisposable
    {
        protected internal bool _disposedValue;
        protected internal readonly List<IDisposable> _disposableObjects;
        public APIControllerV1Base()
        {
            _disposedValue = false;
            _disposableObjects = new();
        }

        public APIControllerV1Base(params object[] lists) : this()
        {
            foreach (var obj in lists)
            {
                if (obj is IDisposable disposeObj)
                {
                    _disposableObjects.Add(disposeObj);
                }
            }
        }
        #region Dispose and Destructor
        [ApiExplorerSettings(IgnoreApi = true)]
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
                foreach (IDisposable disposeObj in _disposableObjects)
                {
                    disposeObj.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }

        ~APIControllerV1Base() => Dispose(false);
        #endregion
    }
}