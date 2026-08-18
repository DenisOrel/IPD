// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IIMCadAttrTypeSettings
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Настройка соответствий атрибутов IPS - параметров CAD-системы
/// </summary>
internal interface IIMCadAttrTypeSettings
{
  /// <summary>Добавление параметра (настроек)</summary>
  /// <param name="code"></param>
  /// <param name="name"></param>
  /// <param name="paramType"></param>
  /// <param name="attrType"></param>
  /// <returns></returns>
  IIMCadAttrTypeParamSettings AddParam(
    string code,
    string name,
    IMCadFaceAttrPropType paramType,
    IMTextFaceAttributeType attrType);

  /// <summary>Удаление параметра (настроек)</summary>
  /// <param name="param"></param>
  /// <remarks>Системные параметры не допускают удаления</remarks>
  void DeleteParam(IIMCadAttrTypeParamSettings param);

  /// <summary>Получение списка настроек параметров</summary>
  IIMCadAttrTypeParamSettings[] Params { get; }
}
