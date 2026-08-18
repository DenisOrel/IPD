// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.ICopyingSessionServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal interface ICopyingSessionServices
{
  IDCache IntegratorsIDCache { get; }

  IFileVault FileVaultService { get; }

  INotificationService NotificationService { get; }
}
