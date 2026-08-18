// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackSett
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.CompositionTracking.Server;

[Serializable]
internal class CompositionTrackSett : CompositionTrackSettingData
{
  public CompositionTrackSett(int objTypeId, int inObjTypeId = -1, int relTypeId = -1)
    : base(objTypeId, inObjTypeId, relTypeId)
  {
  }

  protected CompositionTrackSett(SerializationInfo information, StreamingContext context)
    : base(information, context)
  {
  }
}
