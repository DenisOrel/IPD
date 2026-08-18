// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IExpertObject
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert;

/// <summary>Main interface of Expert System object</summary>
public interface IExpertObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Объект-условие</summary>
  TempFormula Cond { get; set; }

  /// <summary>Load object data. MUST call prior to using the object</summary>
  void Load();

  /// <summary>Save object data</summary>
  void Save();

  string Name { get; set; }

  ExpertObjType ObjType { get; }

  AttribPair[] usedAttrs { get; }

  string[] attribGUIDs { get; }

  string[] objGUIDs { get; }

  bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode);
}
