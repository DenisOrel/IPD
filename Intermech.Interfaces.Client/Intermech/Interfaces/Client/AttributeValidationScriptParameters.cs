// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AttributeValidationScriptParameters
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class AttributeValidationScriptParameters
{
  public IUserSession UserSession { get; set; }

  public long ObjectID { get; set; }

  public long RelationID { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public List<AttributeValues> ObjectAttributeValues { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public List<AttributeValues> RelationAttributeValues { get; set; }
}
