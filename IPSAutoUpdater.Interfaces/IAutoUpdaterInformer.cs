// Decompiled with JetBrains decompiler
// Type: IPSAutoUpdater.Interfaces.IAutoUpdaterInformer
// Assembly: IPSAutoUpdater.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74369E9B-3C90-46D5-99C8-30597004F5A5
// Assembly location: D:\IPS\Client\IPSAutoUpdater.Interfaces.dll

using System;


namespace IPSAutoUpdater.Interfaces;

public interface IAutoUpdaterInformer
{
  Guid ID { get; }

  int Revision { get; }

  string Name { get; }

  string Description { get; }

  Guid PutMessage(IAutoUpdaterInformerData data);
}
