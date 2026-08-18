// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseApplicablityException
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Imbase.Server;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[Serializable]
public class ImbaseApplicablityException : Exception
{
  public long ParentObjectId { get; }

  public string ParentObjectName { get; }

  public ApplicabilityStatusEnum[] Applicabilities { get; }

  public Tuple<long, int>[] ChildObjectInfo { get; }

  protected ImbaseApplicablityException(SerializationInfo info, StreamingContext context)
  {
    this.ParentObjectId = (long) info.GetValue(nameof (ParentObjectId), typeof (long));
    this.ParentObjectName = (string) info.GetValue("ParentObjectInfo", typeof (string));
    this.Applicabilities = (ApplicabilityStatusEnum[]) info.GetValue("Applicability", typeof (ApplicabilityStatusEnum[]));
    this.ChildObjectInfo = (Tuple<long, int>[]) info.GetValue(nameof (ChildObjectInfo), typeof (Tuple<long, int>[]));
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("ParentObjectId", (object) this.ParentObjectId, typeof (long));
    info.AddValue("ParentObjectInfo", (object) this.ParentObjectName, typeof (string));
    info.AddValue("Applicability", (object) this.Applicabilities, typeof (ApplicabilityStatusEnum[]));
    info.AddValue("ChildObjectInfo", (object) this.ChildObjectInfo, typeof (Tuple<long, int>[]));
  }

  public ImbaseApplicablityException(
    ApplicabilityStatusEnum[] applicabilities,
    long parentObjId,
    string parentObjecName,
    Tuple<long, int>[] childIdsInfo)
  {
    this.ParentObjectId = parentObjId;
    this.ParentObjectName = parentObjecName;
    this.Applicabilities = applicabilities;
    this.ChildObjectInfo = childIdsInfo;
  }

  public override string Message
  {
    get
    {
      return $"В составе объекта {this.ParentObjectName}[{this.ParentObjectId}] есть объекты с недопустимой для использования применяемостью";
    }
  }
}
