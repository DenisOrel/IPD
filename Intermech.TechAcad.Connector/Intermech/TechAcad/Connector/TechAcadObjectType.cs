// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadObjectType
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Interfaces;
using Intermech.Runtime.ComInterop.LocalServer;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadObjectType : SingleThreadedObject, ITPObjectType
{
  public TechAcadObjectType(int objectTypeId)
  {
    this.ObjTypeID = objectTypeId;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeId);
    if (objectType == null)
      return;
    this.Name = objectType.ObjectTypeName;
    this.ObjTypeGuid = objectType.Guid.ToString();
  }

  public string Name { get; } = "";

  public int ObjTypeID { get; }

  public string ObjTypeGuid { get; } = string.Empty;
}
