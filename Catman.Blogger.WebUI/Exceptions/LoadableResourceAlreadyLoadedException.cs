namespace Catman.Blogger.WebUI.Exceptions
{
    using System;

    internal class LoadableResourceAlreadyLoadedException : Exception
    {
        public override string Message => "Loadable resource already loaded!";
    }
}
