// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.ObjectIsNotDBResolutionException
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Office.Server;

[Serializable]
public class ObjectIsNotDBResolutionException : InvalidCastException, ISerializable, IObjectException
{
  [CanBeNull]
  private string _objectTypeName;

  public long ObjectID { get; }

  [NotNull]
  public string ObjectTypeName
  {
    get
    {
      if (this._objectTypeName != null)
        return this._objectTypeName;
      this._objectTypeName = MetaDataHelper.GetObjectTypeFullName(Session.GetObjectInfo(this.ObjectID).ObjectTypeID);
      return this._objectTypeName;
    }
  }

  public ObjectIsNotDBResolutionException([NotNull] IDBObject dbObject)
  {
    this.ObjectID = dbObject.ID;
  }

  public ObjectIsNotDBResolutionException([NotEmpty] long objectID) => this.ObjectID = objectID;

  protected ObjectIsNotDBResolutionException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  public override string Message
  {
    get
    {
      return $"Object with id={this.ObjectID} and type='{this.ObjectTypeName}' is not office resolution";
    }
  }
}
