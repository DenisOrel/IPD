// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IExpertFormula
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert;

/// <summary>Formula interface</summary>
public interface IExpertFormula : 
  IExpertFormulable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  /// <summary>Attribute and object type that the formula returns</summary>
  AttribPair Result { get; set; }

  /// <summary>GUID of result attribute</summary>
  string resAttrGuid { get; set; }

  /// <summary>GUID of result object type</summary>
  string resObjTypeGuid { get; set; }

  /// <summary>Name of the result (read-only)</summary>
  string resName { get; }
}
