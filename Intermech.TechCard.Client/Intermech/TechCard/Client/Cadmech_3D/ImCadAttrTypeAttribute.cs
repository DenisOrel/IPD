// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.ImCadAttrTypeAttribute
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Атрибут для описания применяемости свойства (параметра) для атрибутов поверхности
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class ImCadAttrTypeAttribute : Attribute
{
  /// <summary>Типы атрибутов поверхностей</summary>
  private readonly IMTextFaceAttributeType[] _attrTypes;

  /// <summary>Конструктор</summary>
  /// <param name="attrTypes"></param>
  public ImCadAttrTypeAttribute(params IMTextFaceAttributeType[] attrTypes)
  {
    this._attrTypes = attrTypes;
  }

  /// <summary>Типы атрибутов поверхностей</summary>
  public IMTextFaceAttributeType[] AttrTypes => this._attrTypes;
}
