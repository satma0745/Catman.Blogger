namespace Catman.Blogger.WebUI.Data.Loadable
{
    using Catman.Blogger.WebUI.Exceptions;

    internal class Loadable<T> : ILoadable<T>
    {
        public bool IsLoaded { get; private set; }

        public T Value { get; private set; }

        public void Unload()
        {
            IsLoaded = false;
            Value = default;
        }
        
        public void Loaded(T value)
        {
            if (IsLoaded)
            {
                throw new LoadableResourceAlreadyLoadedException();
            }
            
            IsLoaded = true;
            Value = value;
        }
    }
}
