// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DynamicGroupHeaderSettings
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary>Настройки динамических заголовков групп записей </summary>
public class DynamicGroupHeaderSettings : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  private const string XMLRootElementName = "DynamicGroupHeaderSettings";
  private const int DefaultMinRowsForDynamicHeaderGroup = 2;
  private long _ownerObjectID = -1;
  private DynamicGroupHeaderSettings _parent;
  private SettingsLevel _level;
  private bool _readOnly;
  private int _minRowsForDynamicHeaderGroup = -1;
  private DynamicHeaderCaptionSettings _dynamicHeaderCaptionSettings;

  /// <summary> Идентификатор объекта, в атрибутах которого хранится схема </summary>
  public long OwnerObjectID
  {
    get => this._ownerObjectID;
    set
    {
      this._ownerObjectID = value;
      this.LoadParams();
    }
  }

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public DynamicGroupHeaderSettings Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  public int MinRowsForDynamicHeaderGroup
  {
    get
    {
      if (this._minRowsForDynamicHeaderGroup != -1)
        return this._minRowsForDynamicHeaderGroup;
      return this._parent == null ? 2 : this._parent.MinRowsForDynamicHeaderGroup;
    }
    set
    {
      if (this._parent != null && value == this._parent.MinRowsForDynamicHeaderGroup)
        this._minRowsForDynamicHeaderGroup = -1;
      else
        this._minRowsForDynamicHeaderGroup = value;
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool MinRowsForDynamicHeaderGroupChanged
  {
    get => this._parent != null && this._minRowsForDynamicHeaderGroup != -1;
  }

  /// <summary>Настройка для заголовка группы</summary>
  public DynamicHeaderCaptionSettings DynamicHeaderCaptionSettings
  {
    get
    {
      DynamicHeaderCaptionSettings headerCaptionSettings = this._dynamicHeaderCaptionSettings;
      if (headerCaptionSettings != null)
        return headerCaptionSettings;
      return this.Parent?.DynamicHeaderCaptionSettings;
    }
    set => this._dynamicHeaderCaptionSettings = value;
  }

  public DynamicGroupHeaderSettings(
    DynamicGroupHeaderSettings parent,
    long ownerObjectID,
    SettingsLevel level)
  {
    this._parent = parent;
    this._level = level;
    this._ownerObjectID = ownerObjectID;
    this.LoadParams();
  }

  /// <summary> Загрузка схемы по умолчанию </summary>
  public void LoadDefaultParams(bool forceOverride)
  {
    if (this._parent == null | forceOverride)
    {
      this._minRowsForDynamicHeaderGroup = 2;
      if (this._dynamicHeaderCaptionSettings == null)
        this._dynamicHeaderCaptionSettings = new DynamicHeaderCaptionSettings();
      this._dynamicHeaderCaptionSettings.LoadDefaultSettings();
    }
    else
    {
      this._minRowsForDynamicHeaderGroup = -1;
      this._dynamicHeaderCaptionSettings = (DynamicHeaderCaptionSettings) null;
    }
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public DynamicGroupHeaderSettings Clone()
  {
    DynamicGroupHeaderSettings groupHeaderSettings = new DynamicGroupHeaderSettings(this._parent, this._ownerObjectID, this._level);
    groupHeaderSettings.CopyParamsFrom(this);
    return groupHeaderSettings;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(DynamicGroupHeaderSettings copy)
  {
    this._minRowsForDynamicHeaderGroup = copy._minRowsForDynamicHeaderGroup;
    this.DynamicHeaderCaptionSettings.CopyParamsFrom((OutputAttributeMappingScheme) copy.DynamicHeaderCaptionSettings);
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Reader.LocalName == "MinRowsForDynamicHeaderGroup")
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.MinRowsForDynamicHeaderGroup = Convert.ToInt32(readArgs.Reader.Value);
      return true;
    }
    if (!(readArgs.Reader.LocalName == "DynamicHeaderCaptionSettings"))
      return false;
    DynamicHeaderCaptionSettings headerCaptionSettings = new DynamicHeaderCaptionSettings();
    headerCaptionSettings.ReadFromXml(readArgs);
    this._dynamicHeaderCaptionSettings = headerCaptionSettings.CellMaping.Count != 0 ? headerCaptionSettings : (DynamicHeaderCaptionSettings) null;
    return true;
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      xw.WriteAttributeString("MinRowsForDynamicHeaderGroup", this._minRowsForDynamicHeaderGroup.ToString());
      (this._dynamicHeaderCaptionSettings ?? new DynamicHeaderCaptionSettings()).WriteToXml("DynamicHeaderCaptionSettings", xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, nameof (DynamicGroupHeaderSettings));
  }

  /// <summary> Загрузка параметров из объекта с guid-ом = OwnerGuid </summary>
  public void LoadParams()
  {
    if (this.OwnerObjectID.IsUndefinedId())
      throw new ArgumentException("OwnerObjectID");
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
        if (objectActual != null)
        {
          IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_DynamicGroupHeaderSettings);
          if (attributeById != null)
          {
            new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
            aDestStream.Position = 0L;
            if (aDestStream.Length != 0L)
              WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, nameof (DynamicGroupHeaderSettings));
            this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
          }
          else
          {
            this._readOnly = AvsIDCache.Attr_DynamicGroupHeaderSettings == -1;
            this.LoadDefaultParams(false);
          }
          if (!this._readOnly && (objectActual.ObjectModifyMode == ObjectModifyModes.CantModify || objectActual.ObjectModifyMode == ObjectModifyModes.CreateVersion))
            this._readOnly = true;
        }
        if (this._dynamicHeaderCaptionSettings != null || this._parent != null)
          return;
        this.LoadDefaultParams(true);
      }
    }
    finally
    {
      aDestStream.Close();
    }
  }

  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  public void SaveParams()
  {
    if (this.ReadOnly)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_DynamicGroupHeaderSettings);
  }

  /// <summary> Получить схему групп заголовков по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема групп заголовков </returns>
  public DynamicGroupHeaderSettings GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent?.GetSchemaByLevel(level);
  }
}
