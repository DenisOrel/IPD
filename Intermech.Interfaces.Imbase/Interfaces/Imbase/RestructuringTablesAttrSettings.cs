// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.RestructuringTablesAttrSettings
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RestructuringTablesAttrSettings
{
  /// <summary>
  /// 
  /// </summary>
  public Guid AttributeGuid { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public int AttributeID { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public string AttributeName { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public object DefaultValue { get; set; }

  /// <summary>Формула.</summary>
  public string Formula { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int Options { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public FieldTypes Type { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public int Required { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int Unique { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Units { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="guid"></param>
  /// <param name="ID"></param>
  /// <param name="name"></param>
  /// <param name="type"></param>
  /// <param name="required"></param>
  /// <param name="unique"></param>
  /// <param name="defaultValue"></param>
  /// <param name="options"></param>
  /// <param name="units"></param>
  public RestructuringTablesAttrSettings(
    Guid guid,
    int ID,
    string name,
    FieldTypes type,
    int required,
    int unique,
    object defaultValue,
    int options,
    string units)
  {
    this.AttributeGuid = guid;
    this.AttributeID = ID;
    this.AttributeName = name;
    this.Type = type;
    this.Required = required;
    this.Unique = unique;
    this.DefaultValue = defaultValue;
    this.Options = options;
    this.Units = units;
  }
}
