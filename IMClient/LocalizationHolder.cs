
// Type: IMClient.LocalizationHolder




using System.Reflection;
using System.Resources;


namespace IMClient
{
    internal class LocalizationHolder
    {
      public static ResourceManager rm = new ResourceManager("IMClient.Resources.IMClientResources", Assembly.GetExecutingAssembly());
      public static ResourceManager rma = new ResourceManager("IMClient.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
    }
}
