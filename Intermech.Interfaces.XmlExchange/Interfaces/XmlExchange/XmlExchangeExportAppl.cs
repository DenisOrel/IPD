// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportAppl
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Settings.Export.Common;
using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Класс для хранения настроек applicability при экспорте объектов
/// </summary>
[XmlRoot("applicability")]
[Serializable]
public class XmlExchangeExportAppl : XmlExchangeExportItem
{
  /// <summary>Гл. ид. типа связи</summary>
  private Guid _relTypeGuid;
  /// <summary>Гл. ид. родительского объекта</summary>
  private Guid _projTypeGuid;
  /// <summary>Гл. ид. дочернего объекта</summary>
  private Guid _partTypeGuid;

  /// <summary>Конструктор</summary>
  public XmlExchangeExportAppl()
    : this(-1)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="relTypeId"> Ид. типа связи</param>
  /// <param name="projTypeId">Ид. типа родительского объекта</param>
  /// <param name="partTypeId"> Ид. типа дочернего объекта</param>
  public XmlExchangeExportAppl(int relTypeId, int projTypeId = -1, int partTypeId = -1)
  {
    this.RelTypeID = relTypeId;
    this.ProjTypeID = projTypeId;
    this.PartTypeID = partTypeId;
  }

  /// <summary>Конструктор</summary>
  /// <param name="relTypeGuid">Гл. ид. типа связи</param>
  public XmlExchangeExportAppl(Guid relTypeGuid)
    : this(relTypeGuid, Guid.Empty)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="relTypeGuid">Гл. ид. типа связи</param>
  /// <param name="projTypeGuid">Гл. ид. типа родительского объекта</param>
  public XmlExchangeExportAppl(Guid relTypeGuid, Guid projTypeGuid)
    : this(relTypeGuid, projTypeGuid, Guid.Empty)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="relTypeGuid"> Гл. ид. типа связи</param>
  /// <param name="projTypeGuid">Гл. ид. типа родительского объекта</param>
  /// <param name="partTypeGuid"> Гл. ид. типа дочернего объекта</param>
  public XmlExchangeExportAppl(Guid relTypeGuid, Guid projTypeGuid, Guid partTypeGuid)
  {
    this.RelTypeGuid = relTypeGuid;
    this.ProjTypeGuid = projTypeGuid;
    this.PartTypeGuid = partTypeGuid;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode) || xmlNode.Attributes == null || xmlNode.Attributes.Count == 0)
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes["reltypeid"];
    if (attribute1 != null)
    {
      int result;
      this.RelTypeID = !int.TryParse(attribute1.Value, out result) ? -1 : result;
      this._relTypeGuid = MetaDataHelper.GetRelationTypeGuid(this.RelTypeID);
    }
    XmlAttribute attribute2 = xmlNode.Attributes["projtypeid"];
    if (attribute2 != null)
    {
      int result;
      this.ProjTypeID = !int.TryParse(attribute2.Value, out result) ? -1 : result;
      this._projTypeGuid = MetaDataHelper.GetObjectTypeGuid(this.ProjTypeID);
    }
    XmlAttribute attribute3 = xmlNode.Attributes["parttypeid"];
    if (attribute3 != null)
    {
      int result;
      this.PartTypeID = !int.TryParse(attribute3.Value, out result) ? -1 : result;
      this._partTypeGuid = MetaDataHelper.GetObjectTypeGuid(this.PartTypeID);
    }
    XmlAttribute attribute4 = xmlNode.Attributes["reltype_guid"];
    if (attribute4 != null && GuidHelper.IsGuid(attribute4.Value))
      this._relTypeGuid = new Guid(attribute4.Value);
    XmlAttribute attribute5 = xmlNode.Attributes["projtype_guid"];
    if (attribute5 != null && GuidHelper.IsGuid(attribute5.Value))
      this._projTypeGuid = new Guid(attribute5.Value);
    XmlAttribute attribute6 = xmlNode.Attributes["parttype_guid"];
    if (attribute6 != null && GuidHelper.IsGuid(attribute6.Value))
      this._partTypeGuid = new Guid(attribute6.Value);
    XmlAttribute attribute7 = xmlNode.Attributes["applmode"];
    int result1;
    if (attribute7 != null && int.TryParse(attribute7.Value, out result1))
      this.ApplMode = (XmlExportApplMode) result1;
    XmlAttribute attribute8 = xmlNode.Attributes["dirmode"];
    int result2;
    if (attribute8 != null && int.TryParse(attribute8.Value, out result2))
      this.DirMode = (XmlExportApplDirection) result2;
    XmlAttribute attribute9 = xmlNode.Attributes["flags"];
    int result3;
    if (attribute9 != null && int.TryParse(attribute9.Value, out result3))
      this.Flags = (XmlExportApplFlags) result3;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fixMode"></param>
  /// <returns></returns>
  public override bool ValidateData(bool fixMode = true)
  {
    IMSObjectType objectType1 = MetaDataHelper.GetObjectType(this.PartTypeGuid);
    IMSObjectType objectType2 = MetaDataHelper.GetObjectType(this.ProjTypeGuid);
    IMSRelationType relationType = MetaDataHelper.GetRelationType(this.RelTypeGuid);
    if (fixMode)
    {
      this.ProjTypeGuid = Guid.Empty;
      if (objectType2 != null)
        this.ProjTypeGuid = objectType2.Guid;
      this.PartTypeGuid = Guid.Empty;
      if (objectType1 != null)
        this.PartTypeGuid = objectType1.Guid;
      this.RelTypeGuid = Guid.Empty;
      if (relationType != null)
        this.RelTypeGuid = relationType.Guid;
      return base.ValidateData();
    }
    if (!base.ValidateData(false) || !(this.RelTypeGuid == Guid.Empty) && (relationType == null || relationType.RelationTypeID != this.RelTypeID) || !(this.ProjTypeGuid == Guid.Empty) && (objectType2 == null || objectType2.ObjectTypeID != this.ProjTypeID))
      return false;
    if (this.PartTypeGuid == Guid.Empty)
      return true;
    return objectType1 != null && objectType1.ObjectTypeID == this.PartTypeID;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode == null)
      return (XmlNode) null;
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("reltypeid");
    attribute1.Value = this.RelTypeID.ToString();
    xmlNode.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("reltype_guid");
    attribute2.Value = this.RelTypeGuid.ToString();
    xmlNode.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDoc.CreateAttribute("projtypeid");
    attribute3.Value = this.ProjTypeID.ToString();
    xmlNode.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDoc.CreateAttribute("projtype_guid");
    attribute4.Value = this.ProjTypeGuid.ToString();
    xmlNode.Attributes.Append(attribute4);
    XmlAttribute attribute5 = xmlDoc.CreateAttribute("parttypeid");
    attribute5.Value = this.PartTypeID.ToString();
    xmlNode.Attributes.Append(attribute5);
    XmlAttribute attribute6 = xmlDoc.CreateAttribute("parttype_guid");
    attribute6.Value = this.PartTypeGuid.ToString();
    xmlNode.Attributes.Append(attribute6);
    XmlAttribute attribute7 = xmlDoc.CreateAttribute("applmode");
    int applMode = (int) this.ApplMode;
    attribute7.Value = applMode.ToString();
    xmlNode.Attributes.Append(attribute7);
    XmlAttribute attribute8 = xmlDoc.CreateAttribute("dirmode");
    int dirMode = (int) this.DirMode;
    attribute8.Value = dirMode.ToString();
    xmlNode.Attributes.Append(attribute8);
    XmlAttribute attribute9 = xmlDoc.CreateAttribute("flags");
    int flags = (int) this.Flags;
    attribute9.Value = flags.ToString();
    xmlNode.Attributes.Append(attribute9);
    return xmlNode;
  }

  /// <summary>Ид. типа связи</summary>
  public int RelTypeID { get; set; } = -1;

  /// <summary>Гл. идентификатор типа связи</summary>
  public Guid RelTypeGuid
  {
    [DebuggerStepThrough] get => this._relTypeGuid;
    [DebuggerStepThrough] set
    {
      if (this._relTypeGuid == value)
        return;
      this._relTypeGuid = value;
      this.RelTypeID = MetaDataHelper.GetRelationTypeID(value);
    }
  }

  /// <summary>Ид. типа родительского объекта</summary>
  public int ProjTypeID { get; set; } = -1;

  /// <summary>Гл. ид. типа родительского объекта</summary>
  public Guid ProjTypeGuid
  {
    [DebuggerStepThrough] get => this._projTypeGuid;
    [DebuggerStepThrough] set
    {
      if (this._projTypeGuid == value)
        return;
      this._projTypeGuid = value;
      if (value == Guid.Empty)
      {
        this.ProjTypeID = -1;
      }
      else
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(value);
        this.ProjTypeID = objectType != null ? objectType.ObjectTypeID : -10000;
      }
    }
  }

  /// <summary>Ид. типа дочернего объекта</summary>
  public int PartTypeID { get; set; } = -1;

  /// <summary>Гл. ид. типа дочернего объекта</summary>
  public Guid PartTypeGuid
  {
    [DebuggerStepThrough] get => this._partTypeGuid;
    [DebuggerStepThrough] set
    {
      if (this._partTypeGuid == value)
        return;
      this._partTypeGuid = value;
      if (value == Guid.Empty)
      {
        this.PartTypeID = -1;
      }
      else
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(value);
        this.PartTypeID = objectType != null ? objectType.ObjectTypeID : -10000;
      }
    }
  }

  /// <summary>
  /// Режим проверки применяемости / раскрытия состава для объектов
  /// </summary>
  public XmlExportApplMode ApplMode { get; set; }

  /// <summary>Направление действия правила / настроек</summary>
  public XmlExportApplDirection DirMode { get; set; }

  /// <summary>Флаги</summary>
  public XmlExportApplFlags Flags { get; set; }
}
