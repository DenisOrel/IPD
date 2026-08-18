// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectNotSupportInterfaceException`1
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class ObjectNotSupportInterfaceException<IDbObjectInterface> : 
  InvalidCastException,
  ISerializable,
  IObjectException
  where IDbObjectInterface : IDBObject
{
  [CanBeNull]
  [NotWhitespace]
  private string _objectTypeName;

  [NotEmpty]
  public long ObjectID { get; }

  [NotNull]
  [NotWhitespace]
  public string ObjectTypeName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._objectTypeName == null)
      {
        int objectTypeId = Session.GetObjectInfo(this.ObjectID).ObjectTypeID;
        this._objectTypeName = MetaDataHelper.GetObjectTypeFullName(objectTypeId);
        if (string.IsNullOrWhiteSpace(this._objectTypeName))
          this._objectTypeName = $"Unknown object type with id = {objectTypeId}";
      }
      return this._objectTypeName;
    }
  }

  public ObjectNotSupportInterfaceException([NotNull] IDBObject dbObject)
  {
    this.ObjectID = dbObject.ObjectID;
  }

  public ObjectNotSupportInterfaceException(long objectID) => this.ObjectID = objectID;

  protected ObjectNotSupportInterfaceException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ObjectID = info.GetInt64(nameof (ObjectID));
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ObjectID", this.ObjectID);
  }

  [NotNull]
  public override string Message
  {
    get
    {
      return $"Object with id={this.ObjectID} of type '{this.ObjectTypeName}' don`t support {typeof (IDbObjectInterface)} interface.";
    }
  }
}
