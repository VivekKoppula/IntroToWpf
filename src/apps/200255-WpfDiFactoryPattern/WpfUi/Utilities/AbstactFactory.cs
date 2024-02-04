using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUi.Utilities;

public class AbstactFactory<T> : IAbstactFactory<T>
{
    private readonly Func<T> _factory;

    public AbstactFactory(Func<T> factory)
    {
        _factory = factory;
    }

    public T Create()
    {
        // Here we are running the delegate that is reprsented by this factory Func<T>
        // This will create the object of type T.
        T t = _factory();
        return t;
    }
}
