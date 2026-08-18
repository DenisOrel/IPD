
// Type: Intermech.Interfaces.Data.DirectRelationAttributesRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Interfaces.Data;

public sealed class DirectRelationAttributesRef : IDBAttributableTypeRef
{
  private readonly int relationType;

  public DirectRelationAttributesRef(int relationType)
  {
    this.relationType = relationType != -1 ? relationType : throw new ArgumentException();
  }

  public IDBAttribute4TypeCollection GetAttributableType(IUserSession session)
  {
    return session.GetRelationType(this.relationType, true).Attributes;
  }

  public AttributeSourceTypes GetAttributeSourceType() => AttributeSourceTypes.Relation;

  public int GetCaptionAttribute() => 0;
}
