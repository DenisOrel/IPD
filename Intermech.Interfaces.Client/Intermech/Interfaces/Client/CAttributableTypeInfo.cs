// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributableTypeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Базовый класс для метаданных с атрибутами</summary>
internal abstract class CAttributableTypeInfo : MetadataInfoObject, IDBAttributableTypeInfo
{
  protected IDBAttribute4TypeInfoCollection _Attributes;

  public CAttributableTypeInfo(MetadataInfoParentContext serviceContext, int metadataID)
    : base(serviceContext, metadataID)
  {
  }

  public abstract IDBAttribute4TypeInfoCollection Attributes { get; }

  public abstract IDBAttribute4TypeInfoCollection VisibleAttributes { get; }

  public bool AnyAttributes
  {
    [DebuggerStepThrough] get => Convert.ToBoolean(this.paramsTable[0]["F_ANY_ATTRIBUTES"]);
  }

  public IDBAttributeTypeInfo GetAttributeType(int attributeID)
  {
    return (IDBAttributeTypeInfo) this.Attributes.GetAttributeByID(attributeID);
  }

  public IDBAttributeTypeInfo GetAttributeType(string attributeName)
  {
    return (IDBAttributeTypeInfo) this.Attributes.GetAttributeByName(attributeName);
  }

  public bool HasAttribute(int attributeID)
  {
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Object;
    return this.AnyAttributes || this.Attributes.GetAttributeByID(attributeID) != null;
  }
}
