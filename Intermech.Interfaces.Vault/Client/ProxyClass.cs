// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.Client.ProxyClass
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Net.Sockets;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.Vault.Interfaces.Client;

public class ProxyClass
{
  private IIntermechVault vault;
  private string serverName;
  private string serverPort = "8010";

  public ProxyClass(string url)
  {
    this.vault = RemotingServices.Connect(typeof (IIntermechVault), url) as IIntermechVault;
    if (this.vault == null)
      throw new Exception(LocalizationHolder.rm.GetString("Vault.Interfaces_1"));
  }

  public ProxyClass(string serverName, string port)
  {
    if (!string.IsNullOrEmpty(port))
      this.serverPort = port;
    this.serverName = serverName;
    this.vault = RemotingServices.Connect(typeof (IIntermechVault), $"tcp://{serverName}:{this.serverPort}/VaultClass") as IIntermechVault;
    if (this.vault == null)
      throw new Exception(LocalizationHolder.rm.GetString("Vault.Interfaces_1"));
  }

  /// <summary>подключиться к шкафу</summary>
  /// <param name="storageGuid">guid шкафа в ips</param>
  /// <param name="storageName">имя шкафа в ips</param>
  /// <param name="password"></param>
  /// <param name="mName"></param>
  public IDiskFileStorage Login(
    string storageGuid,
    string storageName,
    string password,
    string mName)
  {
    return this.vault.Login(storageGuid, storageName, password, mName) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Vault.Interfaces_2"), (object) storageGuid));
  }

  /// <summary>
  /// подключение к службе
  /// (редактирование параметров и настроек администратором)
  /// </summary>
  /// <param name="password"></param>
  /// <returns></returns>
  public IVaultSettings Login(string password) => this.vault.Login(password);

  /// <summary>
  /// создать шкаф. если такой уже есть,
  /// подключиться к существующему
  /// </summary>
  /// <param name="storageName"></param>
  /// <param name="storageGuid"></param>
  /// <param name="password"></param>
  /// <param name="mName"></param>
  public IDiskFileStorage CreateStorage(
    string storageGuid,
    string storageName,
    string password,
    string mName)
  {
    try
    {
      return this.vault.CreateStorage(storageGuid, storageName, password, mName);
    }
    catch (SocketException ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Vault.Interfaces_3"), (object) this.serverName, (object) this.serverPort, (object) ex.Message), (Exception) ex);
    }
    catch (Exception ex)
    {
      throw ex;
    }
  }
}
