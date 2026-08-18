// Decompiled with JetBrains decompiler
// Type: IPSAutoUpdater.Interfaces.IAutoUpdaterInformerData
// Assembly: IPSAutoUpdater.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74369E9B-3C90-46D5-99C8-30597004F5A5
// Assembly location: D:\IPS\Client\IPSAutoUpdater.Interfaces.dll

using System;


namespace IPSAutoUpdater.Interfaces;

public interface IAutoUpdaterInformerData
{
  Guid ID { get; }

  InfoType InfoType { get; }

  DateTime Stamp { get; }

  string Caption { get; }

  string[] Text { get; }

  TimeSpan Span { get; }
}
