// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientSession
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for IClientSession.</summary>
public interface IClientSession : IUserSession
{
  /// <summary>Интерфейс, позволяющий получить инфу о кэше сервера</summary>
  IClientCache ClientCache { get; }

  IUserSession Session { get; }
}
