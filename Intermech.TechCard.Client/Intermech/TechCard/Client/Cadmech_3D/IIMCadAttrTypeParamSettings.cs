// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IIMCadAttrTypeParamSettings
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Настройка соответствия парамета CAD-системы и атрибута IPS
/// </summary>
internal interface IIMCadAttrTypeParamSettings
{
  /// <summary>Идентификатор (код) параметра</summary>
  string Code { get; set; }

  /// <summary>Наименование</summary>
  string Name { get; set; }

  /// <summary>Тип данных параметра</summary>
  IMCadFaceAttrPropType ParamType { get; set; }

  /// <summary>Тип атрибута (принадлежность параметра)</summary>
  IMTextFaceAttributeType AttrType { get; set; }

  /// <summary>
  /// Признак системного параметра - жестко определен в Cadmech
  /// </summary>
  /// <remarks>Не допускает редактирования свойств параметра, за исключеним атрибута IPS</remarks>
  bool IsSystem { get; }

  /// <summary>Глобальный идентификатор атрибута IPS</summary>
  Guid IpsAttrType { get; set; }
}
