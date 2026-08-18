// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IIntermechVault
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IIntermechVault
{
  /// <summary>
  /// подключиться к службе intermech document service
  /// (использется  админом, для полчения и измененяи настроек всего хранилища,
  /// а не отдельных шкафов)
  /// </summary>
  /// <param name="password"> пароль подключения</param>
  /// <returns>объект для работы с шкафом</returns>
  IVaultSettings Login(string password);

  /// <summary>подключиться к шкафу</summary>
  /// <param name="storageGuid">guid шкафа в ips</param>
  /// <param name="storageName">имя шкафа в ips</param>
  /// <param name="password">пароль подключения </param>
  /// <param name="mName"> машина с которой произошло подключения (сервер ips)</param>
  /// <returns></returns>
  IDiskFileStorage Login(string storageGuid, string storageName, string password, string mName);

  /// <summary>
  /// создает файловый шкаф с именем и гуидом и вызывает для созданного шкафа
  /// ф-цию Login. Одна служба может обслуживать несколько файловых шкафов.
  /// </summary>
  /// <param name="storageGuid">guid создаваемого шкафа</param>
  /// <param name="storageName"></param>
  /// <param name="password">пароль подключения</param>
  /// <param name="mName"></param>
  /// <returns>объект для работы с шкафом</returns>
  IDiskFileStorage CreateStorage(
    string storageGuid,
    string storageName,
    string password,
    string mName);
}
