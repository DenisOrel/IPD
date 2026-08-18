// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IElectricalComponent
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Интерфейс, описывающий компонент платы или схемы</summary>
public interface IElectricalComponent : 
  IPropertiesCollection,
  IFunctionalGroupComponent,
  IImbaseComponent,
  IValueBagContainer
{
  /// <summary>Уникальный идентификатор внутри проекта</summary>
  string UID { get; }

  /// <summary>Глобальный идентификатор позиции</summary>
  Guid PosGuid { get; }

  /// <summary>Наименование компонента</summary>
  string PartNumber { get; }

  /// <summary>Позиционное обозначение</summary>
  string PosDesignation { get; }

  /// <summary>
  /// Ссылка на схему или плату на котором расположен компонент
  /// </summary>
  IDocumentFile Parent { get; set; }

  /// <summary>Позиционное обозначение ДС</summary>
  string ASPosDesignation { get; }
}
