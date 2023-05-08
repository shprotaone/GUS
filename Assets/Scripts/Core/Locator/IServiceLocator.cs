using System.Collections;
using System.Collections.Generic;

namespace GUS.Core.Locator
{
    public interface IServiceLocator
    {
        public void Register<TService>(TService service);
        public TService Get<TService>();
        public IEnumerable<TService> GetAll<TService>();
    }
}