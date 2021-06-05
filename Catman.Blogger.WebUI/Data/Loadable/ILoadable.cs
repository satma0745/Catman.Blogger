namespace Catman.Blogger.WebUI.Data.Loadable
{
    internal interface ILoadable<T>
    {
        bool IsLoaded { get; }
        
        T Value { get; }

        void Loaded(T value);
    }
}
