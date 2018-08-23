using System;

namespace ctacke.Xamarin.MVVM
{
    public class ViewTypeNotRegisteredException : Exception
    {
        public ViewTypeNotRegisteredException(Type viewType)
            : base(string.Format("View type '{0}' not registered", viewType.Name))
        {
        }
    }
}