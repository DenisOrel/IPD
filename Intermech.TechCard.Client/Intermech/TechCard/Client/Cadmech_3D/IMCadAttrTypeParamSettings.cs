// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IMCadAttrTypeParamSettings
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Interfaces;
using System;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Настройка соответствия парамета CAD-системы и атрибута IPS
/// </summary>
internal class IMCadAttrTypeParamSettings : 
  IIMCadAttrTypeParamSettings,
  IEquatable<IIMCadAttrTypeParamSettings>
{
  /// <summary>
  /// 
  /// </summary>
  private string _code;
  /// <summary>
  /// 
  /// </summary>
  private string _name;
  /// <summary>
  /// 
  /// </summary>
  private IMCadFaceAttrPropType _paramType;
  /// <summary>
  /// 
  /// </summary>
  private IMTextFaceAttributeType _attrType;
  /// <summary>
  /// 
  /// </summary>
  private bool _isSystem;
  /// <summary>
  /// 
  /// </summary>
  private Guid _ipsAttrType = Guid.Empty;

  /// <summary>Конструктор</summary>
  internal IMCadAttrTypeParamSettings()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="code"></param>
  /// <param name="name"></param>
  /// <param name="paramType"></param>
  /// <param name="attrType"></param>
  /// <param name="isSystem"></param>
  public IMCadAttrTypeParamSettings(
    string code,
    string name,
    IMCadFaceAttrPropType paramType,
    IMTextFaceAttributeType attrType,
    bool isSystem = false)
  {
    this._code = code;
    this._name = name;
    this._paramType = paramType;
    this._attrType = attrType;
    this._isSystem = isSystem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  internal XmlNode SaveToXml(XmlDocument xmlDoc)
  {
    XmlElement xml = xmlDoc != null ? xmlDoc.CreateElement(nameof (IMCadAttrTypeParamSettings)) : throw new ArgumentNullException(nameof (xmlDoc));
    XmlNode element1 = (XmlNode) xmlDoc.CreateElement("Code");
    element1.InnerText = this.Code;
    xml.AppendChild(element1);
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("Name");
    element2.InnerText = this.Name;
    xml.AppendChild(element2);
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("ParamType");
    XmlNode xmlNode1 = element3;
    int num = (int) this.ParamType;
    string str1 = num.ToString();
    xmlNode1.InnerText = str1;
    xml.AppendChild(element3);
    XmlNode element4 = (XmlNode) xmlDoc.CreateElement("AttrType");
    XmlNode xmlNode2 = element4;
    num = (int) this.AttrType;
    string str2 = num.ToString();
    xmlNode2.InnerText = str2;
    xml.AppendChild(element4);
    XmlNode element5 = (XmlNode) xmlDoc.CreateElement("IsSystem");
    element5.InnerText = this.IsSystem ? "1" : "0";
    xml.AppendChild(element5);
    XmlNode element6 = (XmlNode) xmlDoc.CreateElement("IpsAttrType");
    element6.InnerText = this.IpsAttrType.ToString();
    xml.AppendChild(element6);
    return (XmlNode) xml;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  internal bool LoadFromXml(XmlNode xmlNode)
  {
    if (xmlNode == null)
      throw new ArgumentNullException(nameof (xmlNode));
    if (!xmlNode.Name.Equals(nameof (IMCadAttrTypeParamSettings)))
      return false;
    try
    {
      this._code = xmlNode["Code"].InnerText;
      this._name = xmlNode["Name"].InnerText;
      this._paramType = (IMCadFaceAttrPropType) Convert.ToInt32(xmlNode["ParamType"].InnerText);
      this._attrType = (IMTextFaceAttributeType) Convert.ToInt32(xmlNode["AttrType"].InnerText);
      this._isSystem = xmlNode["IsSystem"].InnerText == "1";
      string innerText = xmlNode["IpsAttrType"].InnerText;
      if (GuidHelper.IsGuid(innerText))
        this._ipsAttrType = new Guid(innerText);
    }
    catch (FormatException ex)
    {
      return false;
    }
    return true;
  }

  /// <summary>Проверка допустимости редактирования параметра</summary>
  public void CheckIfParamsIsEditable()
  {
    if (this.IsSystem)
      throw new IMCadSystemAttrParamModificationException();
  }

  /// <summary>Идентификатор (код) параметра</summary>
  public string Code
  {
    get => this._code;
    set
    {
      this.CheckIfParamsIsEditable();
      this._code = value;
    }
  }

  /// <summary>Наименование</summary>
  public string Name
  {
    get => this._name;
    set
    {
      this.CheckIfParamsIsEditable();
      this._name = value;
    }
  }

  /// <summary>Тип данных параметра</summary>
  public IMCadFaceAttrPropType ParamType
  {
    get => this._paramType;
    set
    {
      this.CheckIfParamsIsEditable();
      this._paramType = value;
    }
  }

  /// <summary>Тип атрибута (принадлежность параметра)</summary>
  public IMTextFaceAttributeType AttrType
  {
    get => this._attrType;
    set
    {
      this.CheckIfParamsIsEditable();
      this._attrType = value;
    }
  }

  /// <summary>
  /// Признак системного парамента - жестко определен в Cadmech
  /// </summary>
  public bool IsSystem => this._isSystem;

  /// <summary>
  /// 
  /// </summary>
  public Guid IpsAttrType
  {
    get => this._ipsAttrType;
    set => this._ipsAttrType = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(IIMCadAttrTypeParamSettings other)
  {
    return other != null && this.Code == other.Code && this.AttrType == other.AttrType;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    return obj is IIMCadAttrTypeParamSettings other ? this.Equals(other) : base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this.Code.GetHashCode() & this.AttrType.GetHashCode() << 16 /*0x10*/;
  }
}
