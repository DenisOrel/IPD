// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackSettingData
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
internal class CompositionTrackSettingData : 
  IComparable,
  ISerializable,
  IComparable<CompositionTrackSettingData>,
  IEquatable<CompositionTrackSettingData>
{
  private readonly IObjectTypeApplicabilityContext _objectTypeContext;

  public CompositionTrackSettingData(int objTypeId, int inObjTypeId = -1, int relTypeId = -1)
  {
    this._objectTypeContext = (IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(objTypeId, inObjTypeId, relTypeId);
  }

  public CompositionTrackSettingData(IObjectTypeApplicabilityContext objectTypeContext)
  {
    this._objectTypeContext = (IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(objectTypeContext);
  }

  protected CompositionTrackSettingData(SerializationInfo information, StreamingContext context)
  {
    this._objectTypeContext = (IObjectTypeApplicabilityContext) new ObjectTypeApplicabilityContext(information.GetInt32("objTypeID"), information.GetInt32("inObjTypeID"), information.GetInt32("relTypeID"));
  }

  public IObjectTypeApplicabilityContext ObjectTypeContext => this._objectTypeContext;

  public int CompareTo(
    CompositionTrackSettingData obj,
    IComparer<CompositionTrackSettingData> comparer)
  {
    comparer = comparer ?? CompositionTrackingSettingDirectComparer.Instance;
    return comparer.Compare(this, obj);
  }

  public override int GetHashCode()
  {
    return this.ObjectTypeContext.ObjectTypeId ^ this.ObjectTypeContext.InObjectTypeId ^ this.ObjectTypeContext.RelationTypeId;
  }

  public int CompareTo(object obj)
  {
    return this.CompareTo(obj as CompositionTrackSettingData, (IComparer<CompositionTrackSettingData>) null);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("relTypeID", this.ObjectTypeContext.RelationTypeId);
    info.AddValue("objTypeID", this.ObjectTypeContext.ObjectTypeId);
    info.AddValue("inObjTypeID", this.ObjectTypeContext.InObjectTypeId);
  }

  public int CompareTo(CompositionTrackSettingData other)
  {
    return this.CompareTo(other, (IComparer<CompositionTrackSettingData>) null);
  }

  public bool Equals(CompositionTrackSettingData other) => this.CompareTo(other) == 0;
}
