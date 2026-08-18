
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Reflection;
using System.Resources;


namespace Intermech.Localization
{
    internal class LocalizationHolder
    {
      public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.Resources.InterfacesResources", Assembly.GetExecutingAssembly());
      public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
    }
}
