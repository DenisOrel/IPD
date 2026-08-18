// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTypeSettingDataList
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces.CompositionTracking;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.CompositionTracking.Server;

[Serializable]
internal class CompositionTypeSettingDataList : 
  Dictionary<CompositionTrackSettingData, CompositionsTrackingSettings>
{
  public CompositionTypeSettingDataList()
  {
  }

  public CompositionTypeSettingDataList(int capacity)
    : base(capacity)
  {
  }

  private CompositionTypeSettingDataList(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
