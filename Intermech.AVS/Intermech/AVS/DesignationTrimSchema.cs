// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DesignationTrimSchema
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

/// <summary>Схема обрезки обозначения в спецификации</summary>
public class DesignationTrimSchema : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  private long _ownerObjectID = -1;
  private DesignationTrimSchema _parent;
  private SettingsLevel _level;
  private bool? sameDesignation;
  private int lengthBasePart = -1;
  private bool? useInDocumentation;
  private bool? useSameDesignationForProducts;
  private bool? useGroupNumberAttribute;
  private bool _readOnly;

  public DesignationTrimSchema(
    DesignationTrimSchema parent,
    long ownerObjectID,
    SettingsLevel level)
  {
    this._parent = parent;
    this._level = level;
    this._ownerObjectID = ownerObjectID;
    this.LoadParams();
  }

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
  public DesignationTrimSchema Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  /// <summary> Сокращать одинаковые обозначения исполнений в записях </summary>
  public bool UseSameProductDesignationsInRows
  {
    get
    {
      if (this.sameDesignation.HasValue)
        return this.sameDesignation.Value;
      return this._parent == null || this._parent.UseSameProductDesignationsInRows;
    }
    set
    {
      if (this._parent != null && value == this._parent.UseSameProductDesignationsInRows)
        this.sameDesignation = new bool?();
      else
        this.sameDesignation = new bool?(value);
    }
  }

  /// <summary> Длина основного обозначения </summary>
  public int LengthBasePart
  {
    get
    {
      if (this.lengthBasePart != -1)
        return this.lengthBasePart;
      return this._parent == null ? 10 : this._parent.LengthBasePart;
    }
    set
    {
      if (this._parent != null && value == this._parent.LengthBasePart)
        this.lengthBasePart = -1;
      else
        this.lengthBasePart = value;
    }
  }

  /// <summary> Использовать в документации </summary>
  public bool UseInDocumentation
  {
    get
    {
      if (this.useInDocumentation.HasValue)
        return this.useInDocumentation.Value;
      return this._parent == null || this._parent.UseInDocumentation;
    }
    set
    {
      if (this._parent != null && value == this._parent.UseInDocumentation)
        this.useInDocumentation = new bool?();
      else
        this.useInDocumentation = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool UseInDocumentationChanged => this._parent != null && this.useInDocumentation.HasValue;

  /// <summary> Использовать аттрибут Идентификатор группового изделия </summary>
  public bool UseGroupNumberAttribute
  {
    get
    {
      if (this.useGroupNumberAttribute.HasValue)
        return this.useGroupNumberAttribute.Value;
      return this._parent == null || this._parent.UseGroupNumberAttribute;
    }
    set
    {
      if (this._parent != null && value == this._parent.UseGroupNumberAttribute)
        this.useGroupNumberAttribute = new bool?();
      else
        this.useGroupNumberAttribute = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool UseGroupNumberAttributeChanged
  {
    get => this._parent != null && this.useGroupNumberAttribute.HasValue;
  }

  /// <summary> Использовать одинаковые обозначения для исполнений специфицируемых изделий </summary>
  public bool UseSameDesignationForProducts
  {
    get
    {
      if (this.useSameDesignationForProducts.HasValue)
        return this.useSameDesignationForProducts.Value;
      return this._parent == null || this._parent.UseSameDesignationForProducts;
    }
    set
    {
      if (this._parent != null && value == this._parent.UseSameDesignationForProducts)
        this.useSameDesignationForProducts = new bool?();
      else
        this.useSameDesignationForProducts = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool UseSameDesignationForProductsChanged
  {
    get => this._parent != null && this.useSameDesignationForProducts.HasValue;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool LengthBasePartChanged => this._parent != null && this.lengthBasePart != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool SameDesignationChanged => this._parent != null && this.sameDesignation.HasValue;

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultParams()
  {
    if (this._parent == null)
    {
      this.sameDesignation = new bool?(true);
      this.useInDocumentation = new bool?(true);
      this.useSameDesignationForProducts = new bool?(true);
      this.useGroupNumberAttribute = new bool?(true);
      this.lengthBasePart = 10;
    }
    else
    {
      this.sameDesignation = new bool?();
      this.useInDocumentation = new bool?();
      this.useSameDesignationForProducts = new bool?();
      this.useGroupNumberAttribute = new bool?();
      this.lengthBasePart = -1;
    }
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public DesignationTrimSchema Clone()
  {
    DesignationTrimSchema designationTrimSchema = new DesignationTrimSchema(this._parent, this._ownerObjectID, this._level);
    designationTrimSchema.CopyParamsFrom(this);
    return designationTrimSchema;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(DesignationTrimSchema copy)
  {
    this.lengthBasePart = copy.lengthBasePart;
    this.useInDocumentation = copy.useInDocumentation;
    this.useSameDesignationForProducts = copy.useSameDesignationForProducts;
    this.sameDesignation = copy.sameDesignation;
    this.useGroupNumberAttribute = copy.useGroupNumberAttribute;
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "LengthBasePart":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.Value != null && readArgs.Reader.Value != string.Empty)
          this.LengthBasePart = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "UseSameProductDesignations":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.Value != null && readArgs.Reader.Value != string.Empty)
          this.UseSameProductDesignationsInRows = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "UseInDocumentation":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.Value != null && readArgs.Reader.Value != string.Empty)
          this.UseInDocumentation = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "SameDesignationForProd":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.Value != null && readArgs.Reader.Value != string.Empty)
          this.UseSameDesignationForProducts = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "UseGroupNumberAttribute":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (readArgs.Reader.Value != null && readArgs.Reader.Value != string.Empty)
          this.UseGroupNumberAttribute = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
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
      if (this._parent == null || this.lengthBasePart != -1)
        xw.WriteAttributeString("LengthBasePart", this.LengthBasePart.ToString());
      if (this._parent == null || this.sameDesignation.HasValue)
        xw.WriteAttributeString("UseSameProductDesignations", this.UseSameProductDesignationsInRows.ToString());
      if (this._parent == null || this.useInDocumentation.HasValue)
        xw.WriteAttributeString("UseInDocumentation", this.useInDocumentation.ToString());
      if (this._parent == null || this.useSameDesignationForProducts.HasValue)
        xw.WriteAttributeString("SameDesignationForProd", this.useSameDesignationForProducts.ToString());
      if (this._parent != null && !this.useGroupNumberAttribute.HasValue)
        return;
      xw.WriteAttributeString("UseGroupNumberAttribute", this.useGroupNumberAttribute.ToString());
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
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, nameof (DesignationTrimSchema));
  }

  /// <summary>Загрузка параметров из объекта с guid-ом = OwnerGuid</summary>
  public void LoadParams()
  {
    if (this.OwnerObjectID.IsUndefinedId())
      return;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_DesignationTrimSchema);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, nameof (DesignationTrimSchema));
          this.LengthBasePart = this.lengthBasePart;
          if (this.sameDesignation.HasValue)
            this.UseSameProductDesignationsInRows = this.sameDesignation.Value;
          if (this.useInDocumentation.HasValue)
            this.UseInDocumentation = this.useInDocumentation.Value;
          if (this.useSameDesignationForProducts.HasValue)
            this.UseSameDesignationForProducts = this.useSameDesignationForProducts.Value;
          if (this.useGroupNumberAttribute.HasValue)
            this.UseGroupNumberAttribute = this.useGroupNumberAttribute.Value;
          this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
        }
        else if (AvsIDCache.Attr_DesignationTrimSchema != -1)
        {
          this._readOnly = false;
          this.LengthBasePart = this.lengthBasePart;
          if (this.sameDesignation.HasValue)
            this.UseSameProductDesignationsInRows = this.sameDesignation.Value;
          if (this.useInDocumentation.HasValue)
            this.UseInDocumentation = this.useInDocumentation.Value;
          if (this.useSameDesignationForProducts.HasValue)
            this.UseSameDesignationForProducts = this.useSameDesignationForProducts.Value;
          if (this.useGroupNumberAttribute.HasValue)
            this.UseGroupNumberAttribute = this.useGroupNumberAttribute.Value;
        }
        else
          this._readOnly = true;
        if (this._readOnly || objectActual.ObjectModifyMode != ObjectModifyModes.CantModify && objectActual.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          return;
        this._readOnly = true;
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
    if (this.OwnerObjectID.IsUndefinedId() || this.ReadOnly || AvsIDCache.Attr_DesignationTrimSchema == -1)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_DesignationTrimSchema);
  }

  /// <summary> Получить схему сортировки по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема сортировки </returns>
  public DesignationTrimSchema GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent != null ? this._parent.GetSchemaByLevel(level) : (DesignationTrimSchema) null;
  }
}
