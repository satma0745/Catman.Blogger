namespace Catman.Blogger.WebUI.Data.Common.Storage
{
    using System;
    using System.Threading.Tasks;

    internal interface IResourceStorage
    {
        event Action OnChange;
        
        Task Initialize();
    }
}
