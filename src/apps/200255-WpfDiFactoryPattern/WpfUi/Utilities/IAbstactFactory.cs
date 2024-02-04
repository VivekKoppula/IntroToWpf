namespace WpfUi.Utilities
{
    public interface IAbstactFactory<T>
    {
        T Create();
    }
}