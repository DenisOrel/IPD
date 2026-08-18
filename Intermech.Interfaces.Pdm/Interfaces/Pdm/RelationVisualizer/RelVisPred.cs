// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationVisualizer.RelVisPred
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Pdm.RelationVisualizer;

/// <summary>Класс для констант Визуализатора связей</summary>
public class RelVisPred
{
  /// <summary>Слои Визуализатора связей</summary>
  [Serializable]
  public enum RelVisLayers
  {
    /// <summary>Главный слой (центральый объект)</summary>
    GeneralTree,
    /// <summary>Слой родительского дерева</summary>
    ParentTree,
    /// <summary>Слой дочернего дерева</summary>
    ChildTree,
  }

  /// <summary>Формула именования, если у объекта нет заголовка</summary>
  [Serializable]
  public enum NoCaptionFormula
  {
    /// <summary>номер_версии_объекта</summary>
    [CustomDescription("Attribute.Interfaces.Pdm_23")] Nom,
    /// <summary>тип_объекта+" №"+номер_версии_объекта</summary>
    [CustomDescription("Attribute.Interfaces.Pdm_24")] ObjType_Nom,
    /// <summary>"["+тип_объекта+"] "+номер_версии_объекта</summary>
    [CustomDescription("Attribute.Interfaces.Pdm_25")] St_ObjType_St_Nom,
    /// <summary>"["+номер_версии_объекта+"] "+тип_объекта</summary>
    [CustomDescription("Attribute.Interfaces.Pdm_26")] St_Nom_St_ObjType,
  }
}
