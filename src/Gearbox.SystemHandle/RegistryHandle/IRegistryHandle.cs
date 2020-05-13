namespace SystemHandle.RegistryHandle
{
    public interface IRegistryHandle
    {
        T GetValue<T>(string keyName, string valueName);
        void SetValue<T>(string keyName, string valueName, T value);
    }
}