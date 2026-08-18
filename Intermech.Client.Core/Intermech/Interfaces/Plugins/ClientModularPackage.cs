
// Type: Intermech.Interfaces.Plugins.ClientModularPackage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Interfaces.Plugins;

/// <summary>
/// Базовый класс для клиентского модуля расширения IPS, который сам состоит из отдельных подмодулей.
/// </summary>
public abstract class ClientModularPackage : ModularPackage
{
  /// <summary>Создает объект.</summary>
  /// <param name="name">Имя модуля расширения</param>
  /// <exception cref="T:System.ArgumentNullException">name</exception>
  public ClientModularPackage(string name)
    : base(name)
  {
  }
}
