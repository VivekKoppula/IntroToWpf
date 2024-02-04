using Microsoft.Extensions.DependencyInjection;

namespace WpfUi.Utilities;

public static class ServiceExtensions 
{
    public static void AddFormFactory<TForm>(this IServiceCollection serviceCollection)
        where TForm : class
    {
        serviceCollection.AddTransient<TForm>();
        // This Func<TForm> is a delegate. And this delegate will create that form when ever we need. 
        // So here, the delegate is added as a service, not the form. 
        serviceCollection.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
        serviceCollection.AddSingleton<IAbstactFactory<TForm>, AbstactFactory<TForm>>();
    }
}