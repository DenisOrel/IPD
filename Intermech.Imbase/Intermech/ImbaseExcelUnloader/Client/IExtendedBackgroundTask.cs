// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.IExtendedBackgroundTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public interface IExtendedBackgroundTask : IBackgroundTask
{
  new string Name { get; set; }

  bool IsProcessStoped { get; }

  void IncProgress();
}
