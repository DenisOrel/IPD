// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseObjectAttrLink
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Object type + attribute to imbase catalog link</summary>
[Serializable]
/// <summary>Constructor.</summary>
/// <param name="objectTypeID"></param>
/// <param name="attributeID"></param>
/// <param name="imbaseObjID"></param>
public struct ImbaseObjectAttrLink(int objectTypeID, int attributeID, long imbaseObjID)
{
  /// <summary>Object type id.</summary>
  public int _objectTypeID = objectTypeID;
  /// <summary>Attribute type id.</summary>
  public int _attribiteID = attributeID;
  /// <summary>Imbase object id.</summary>
  public long _imbaseObjID = imbaseObjID;
}
