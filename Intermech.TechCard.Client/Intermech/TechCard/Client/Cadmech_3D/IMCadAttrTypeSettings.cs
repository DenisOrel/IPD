// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IMCadAttrTypeSettings
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Настройка соответствий атрибутов IPS - параметров CAD-системы
/// </summary>
internal class IMCadAttrTypeSettings : IIMCadAttrTypeSettings
{
  /// <summary>Настройки параметров</summary>
  private List<IIMCadAttrTypeParamSettings> _params = new List<IIMCadAttrTypeParamSettings>();

  /// <summary>Добавление параметра (настроек)</summary>
  /// <param name="code"></param>
  /// <param name="name"></param>
  /// <param name="paramType"></param>
  /// <param name="attrType"></param>
  /// <returns></returns>
  public IIMCadAttrTypeParamSettings AddParam(
    string code,
    string name,
    IMCadFaceAttrPropType paramType,
    IMTextFaceAttributeType attrType)
  {
    IIMCadAttrTypeParamSettings typeParamSettings = (IIMCadAttrTypeParamSettings) new IMCadAttrTypeParamSettings(code, name, paramType, attrType);
    this._params.Add(typeParamSettings);
    return typeParamSettings;
  }

  /// <summary>Удаление параметра (настроек)</summary>
  /// <param name="param"></param>
  /// <remarks>Системные параметры не допускают удаления</remarks>
  public void DeleteParam(IIMCadAttrTypeParamSettings param)
  {
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    if (param.IsSystem)
      throw new IMCadSystemAttrParamModificationException();
    this._params.Remove(param);
  }

  /// <summary>Получение списка настроек параметров</summary>
  public IIMCadAttrTypeParamSettings[] Params => this._params.ToArray();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  internal XmlNode SaveToXml(XmlDocument xmlDoc)
  {
    XmlNode xml1 = xmlDoc != null ? (XmlNode) xmlDoc.CreateElement(nameof (IMCadAttrTypeSettings)) : throw new ArgumentNullException(nameof (xmlDoc));
    XmlNode element = (XmlNode) xmlDoc.CreateElement("IMCadAttrTypeParamSettingList");
    foreach (IMCadAttrTypeParamSettings typeParamSettings in this._params)
    {
      XmlNode xml2 = typeParamSettings.SaveToXml(xmlDoc);
      if (xml2 != null)
        element.AppendChild(xml2);
    }
    xml1.AppendChild(element);
    return xml1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNode"></param>
  internal void LoadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      throw new ArgumentNullException(nameof (xmlNode));
    if (!xmlNode.Name.Equals(nameof (IMCadAttrTypeSettings)))
      return;
    this._params.Clear();
    foreach (XmlNode childNode in xmlNode["IMCadAttrTypeParamSettingList"].ChildNodes)
    {
      IMCadAttrTypeParamSettings typeParamSettings = new IMCadAttrTypeParamSettings();
      if (typeParamSettings.LoadFromXml(childNode))
        this._params.Add((IIMCadAttrTypeParamSettings) typeParamSettings);
    }
    IMCadAttrTypeSettings attrTypeSettings = new IMCadAttrTypeSettings();
    attrTypeSettings.LoadSystemParams();
    foreach (IMCadAttrTypeParamSettings typeParamSettings in attrTypeSettings._params)
    {
      if (!this._params.Contains((IIMCadAttrTypeParamSettings) typeParamSettings))
        this._params.Add((IIMCadAttrTypeParamSettings) typeParamSettings);
    }
  }

  /// <summary>Загрузка "системных" параметров (настроек)</summary>
  public void LoadSystemParams()
  {
    foreach (FieldInfo field in typeof (IMCadFaceAttrPropNames).GetFields())
    {
      if (field.FieldType.Equals(typeof (string)) && field.IsStatic)
      {
        object[] customAttributes1 = field.GetCustomAttributes(typeof (ImCadPropTypeAttribute), false);
        ImCadPropTypeAttribute propTypeAttribute = customAttributes1.Length == 1 ? customAttributes1[0] as ImCadPropTypeAttribute : (ImCadPropTypeAttribute) null;
        if (propTypeAttribute != null)
        {
          object[] customAttributes2 = field.GetCustomAttributes(typeof (ImCadAttrTypeAttribute), false);
          ImCadAttrTypeAttribute attrTypeAttribute = customAttributes2.Length == 1 ? customAttributes2[0] as ImCadAttrTypeAttribute : (ImCadAttrTypeAttribute) null;
          if (attrTypeAttribute != null)
          {
            string code = field.GetValue((object) null).ToString();
            object[] customAttributes3 = field.GetCustomAttributes(typeof (DescriptionAttribute), false);
            string name = customAttributes3.Length != 0 ? (customAttributes3[0] as DescriptionAttribute).Description : string.Empty;
            foreach (IMTextFaceAttributeType attrType in attrTypeAttribute.AttrTypes)
            {
              IIMCadAttrTypeParamSettings typeParamSettings = (IIMCadAttrTypeParamSettings) new IMCadAttrTypeParamSettings(code, name, propTypeAttribute.PropertyType, attrType, true);
              if (!this._params.Contains(typeParamSettings))
                this._params.Add(typeParamSettings);
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal void LoadDefaultSettings() => this.LoadSystemParams();
}
