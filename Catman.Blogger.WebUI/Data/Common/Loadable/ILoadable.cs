namespace Catman.Blogger.WebUI.Data.Common.Loadable
{
    internal interface ILoadable<T>
    {
        bool IsLoaded { get; }
        
        T Value { get; }

        void Unload();

        void Loaded(T value);
    }
}
