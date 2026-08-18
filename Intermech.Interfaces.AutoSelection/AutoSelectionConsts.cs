// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionConsts
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>Константы для автоподбора</summary>
public sealed class AutoSelectionConsts
{
  /// <summary>Тип объекта "Правило автоподбора"</summary>
  public static Guid objTypeRuleGuid = new Guid("cad001b0-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ид. типа объекта "Правило автоподбора"</summary>
  public static readonly int objTypeRuleID;
  /// <summary>Тип объекта "Сценарий автоподбора"</summary>
  public static Guid objTypeScriptGuid = new Guid("cadd98d5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ид. типа объекта "Сценарий автоподбора"</summary>
  public static readonly int objTypeScriptID;
  /// <summary>Тип аттрибута экспертной системы "ЭТО"</summary>
  public static Guid etoDoubleExpertAttrGuid = new Guid("cad014c8-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Тип аттрибута экспертной системы "ЭТО!" (Выраженный в ед. измерения)
  /// </summary>
  public static Guid etoMeasuredExpertAttrGuid = new Guid("cad014ca-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип аттрибута экспертной системы "ЭТО_" (Строка)</summary>
  public static Guid etoStringExpertAttrGuid = new Guid("cad014c9-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип атрибута "Ссылка на объект IMBASE"</summary>
  public static Guid imbaseObjectAttrGuid = new Guid("cad00209-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип атрибута "Дерево автоподбора"</summary>
  public static Guid attrTypeRuleLinkGuid = new Guid("cad001b1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип атрибута "Данные"</summary>
  public static Guid attrTypeData = new Guid("cad001b2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип атрибута "Привязка к типу объектов"</summary>
  public static Guid attrTypeTypeLink = new Guid("cad009bc-306c-11d8-b4e9-00304f19f545");

  /// <summary>Constructor</summary>
  static AutoSelectionConsts()
  {
    AutoSelectionConsts.objTypeRuleID = MetaDataHelper.GetObjectTypeID(AutoSelectionConsts.objTypeRuleGuid);
    AutoSelectionConsts.objTypeScriptID = MetaDataHelper.GetObjectTypeID(AutoSelectionConsts.objTypeScriptGuid);
  }
}
