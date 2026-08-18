// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechAcad.TechAcadParamsItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.TechAcad;

/// <summary>Класс для настроек вызова Cadmech-T</summary>
[Serializable]
public class TechAcadParamsItem
{
  /// <summary>
  /// 
  /// </summary>
  protected string _appPath;
  /// <summary>
  /// 
  /// </summary>
  protected string _params;
  /// <summary>
  /// 
  /// </summary>
  protected string _prototypeDraft;
  /// <summary>
  /// 
  /// </summary>
  protected string _fileExtention;

  /// <summary>Инициализация данных класса</summary>
  protected void InitData() => this.Clear();

  /// <summary>Корректировка параметров</summary>
  protected void NormalizeParams()
  {
    this._appPath = this._appPath.Replace("\"", "");
    this._prototypeDraft = this._prototypeDraft.Replace("\"", "");
  }

  /// <summary>Конструктор</summary>
  public TechAcadParamsItem() => this.InitData();

  /// <summary>Путь к редактору эскизов</summary>
  public string ApplPath
  {
    get => this._appPath;
    set => this._appPath = value;
  }

  /// <summary>Параметры вызова редактора эскизов</summary>
  public string Params
  {
    get => this._params;
    set => this._params = value;
  }

  /// <summary>Имя файла-прототипа для эскизов</summary>
  public string PrototypeDraft
  {
    get => this._prototypeDraft;
    set => this._prototypeDraft = value;
  }

  /// <summary>Расширение файла эскиза</summary>
  public string FileExtention
  {
    get => this._fileExtention;
    set => this._fileExtention = value;
  }

  /// <summary>Очистить параметры</summary>
  public void Clear()
  {
    this._appPath = "";
    this._params = "";
    this._prototypeDraft = "";
    this._fileExtention = "dwg";
  }

  /// <summary>Сохранение</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public virtual XmlNode Save(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlElement element1 = xmlDoc.CreateElement(nameof (TechAcadParamsItem));
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("ApplPath");
    element2.InnerText = this.ApplPath;
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("Params");
    element3.InnerText = this.Params;
    XmlNode element4 = (XmlNode) xmlDoc.CreateElement("PrototypeDraft");
    element4.InnerText = this.PrototypeDraft;
    XmlNode element5 = (XmlNode) xmlDoc.CreateElement("FileExtention");
    element5.InnerText = this.FileExtention;
    element1.AppendChild(element2);
    element1.AppendChild(element3);
    element1.AppendChild(element4);
    element1.AppendChild(element5);
    return (XmlNode) element1;
  }

  /// <summary>Загрузка</summary>
  /// <param name="xmlNode"></param>
  public void Load(XmlNode xmlNode)
  {
    if (xmlNode == null || !xmlNode.Name.Equals(nameof (TechAcadParamsItem)))
      return;
    XmlElement xmlElement1 = xmlNode["ApplPath"];
    XmlElement xmlElement2 = xmlNode["Params"];
    XmlElement xmlElement3 = xmlNode["PrototypeDraft"];
    XmlElement xmlElement4 = xmlNode["FileExtention"];
    this._appPath = xmlElement1 != null ? xmlElement1.InnerText : string.Empty;
    this._params = xmlElement2 != null ? xmlElement2.InnerText : string.Empty;
    this._prototypeDraft = xmlElement3 != null ? xmlElement3.InnerText : string.Empty;
    this._fileExtention = xmlElement4 != null ? xmlElement4.InnerText : string.Empty;
    this.NormalizeParams();
  }

  /// <summary>Копирование данных</summary>
  /// <param name="source"></param>
  public void Copy(TechAcadParamsItem source)
  {
    if (source == null)
      return;
    this._appPath = source.ApplPath;
    this._params = source.Params;
    this._prototypeDraft = source.PrototypeDraft;
    this._fileExtention = source.FileExtention;
  }
}
