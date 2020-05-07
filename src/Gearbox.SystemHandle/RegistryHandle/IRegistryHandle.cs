namespace SystemHandle.RegistryHandle
{
    public interface IRegistryHandle
    {
        string GetValue(string key);
        void SetValue(string key);
    }
}