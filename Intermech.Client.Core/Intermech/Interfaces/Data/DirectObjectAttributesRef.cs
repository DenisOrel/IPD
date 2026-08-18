
// Type: Intermech.Interfaces.Data.DirectObjectAttributesRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Interfaces.Data;

public sealed class DirectObjectAttributesRef : IDBAttributableTypeRef
{
  private readonly int objectType;

  public DirectObjectAttributesRef(int objectType)
  {
    this.objectType = objectType != -1 ? objectType : throw new ArgumentException();
  }

  public IDBAttribute4TypeCollection GetAttributableType(IUserSession session)
  {
    return session.GetObjectType(this.objectType, true).Attributes;
  }

  public AttributeSourceTypes GetAttributeSourceType() => AttributeSourceTypes.Object;

  public int GetCaptionAttribute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectType(this.objectType, true).CaptionAttribute;
  }
}
