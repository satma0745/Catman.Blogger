namespace Catman.Blogger.WebUI.Data.Loadable
{
    internal interface ILoadable<T>
    {
        bool IsLoaded { get; }
        
        T Value { get; }

        void Unload();

        void Loaded(T value);
    }
}
