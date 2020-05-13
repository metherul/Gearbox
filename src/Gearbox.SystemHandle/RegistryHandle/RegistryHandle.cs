using Microsoft.Win32;

namespace SystemHandle.RegistryHandle
{
    public class RegistryHandle : IRegistryHandle    
    {
        public T GetValue<T>(string keyName, string valueName)
        {
            return (T)Registry.GetValue(keyName, valueName, string.Empty);
        }

        public void SetValue<T>(string keyName, string valueName, T value)
        {
            Registry.SetValue(keyName, valueName, value);
        }
    }
}