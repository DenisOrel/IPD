// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ObjectIsNotIDBResolutionException
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Office.Interfaces;

[Serializable]
public class ObjectIsNotIDBResolutionException : 
  ObjectNotSupportInterfaceException<IDBResolution>,
  ISerializable,
  IObjectException
{
  public ObjectIsNotIDBResolutionException([NotNull] IDBObject dbObject)
    : base(dbObject)
  {
  }

  public ObjectIsNotIDBResolutionException(long objectID)
    : base(objectID)
  {
  }

  protected ObjectIsNotIDBResolutionException([NotNull] SerializationInfo info, StreamingContext context)
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
