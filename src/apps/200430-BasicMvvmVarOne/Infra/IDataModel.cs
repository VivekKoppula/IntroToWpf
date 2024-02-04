namespace BasicMvvmVarOne.Infra
{
    public interface IDataModel
    {
        string Data { get; set; }
        string? Reverse();
    }
}
