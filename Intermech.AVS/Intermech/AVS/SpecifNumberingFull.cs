// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifNumberingFull
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Класс, хранящий информацию о порядке нумерации в спецификации
/// Полная версия включающая в себя настройки для частей, разделов и типов объектов
/// </summary>
public class SpecifNumberingFull : SpecifNumbering, ICloneable, IWriteReadXml
{
  internal long[] _NonNumneringParts = new long[0];
  internal long[] _NonNumneringRazdels = new long[0];
  public long _OwnerObjectID;
  protected SpecifNumberingFull _ParentLevel;
  protected bool _NonNumneringPartsChanged;
  protected bool _NonNumneringRazdelsChanged;
  protected bool _ReadOnly;
  protected SettingsLevel _level;
  protected SpecifRazdelNumbering _SpecifRazdelNumbering;
  protected CompareDesignationSchema _CompareDesignationSchema;
  protected bool _WasCheckedOutForEdit;

  public SpecifNumberingFull(
    SpecifNumberingFull parentLevel,
    long ownerObjectID,
    SettingsLevel level)
    : this()
  {
    this._ParentLevel = parentLevel;
    this._level = level;
    base._ParentLevel = (SpecifNumbering) parentLevel;
    if (this._ParentLevel == null)
      this.LoadRootParams();
    this.OwnerObjectID = ownerObjectID;
  }

  public SpecifNumberingFull()
  {
    this._SpecifRazdelNumbering = new SpecifRazdelNumbering(this);
    this._CompareDesignationSchema = new CompareDesignationSchema(this);
  }

  /// <summary>
  /// Идентификатор объекта, в настройках которого должны храниться настройки
  /// </summary>
  public long OwnerObjectID
  {
    get => this._OwnerObjectID;
    set
    {
      this._OwnerObjectID = value;
      this.LoadParams();
    }
  }

  /// <summary> Дексриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Специальные настройки нумерации для типов объектов </summary>
  public SpecifRazdelNumbering SpecifRazdelNumbering => this._SpecifRazdelNumbering;

  /// <summary> Признак того, что список ненумеруемых частей спецификации был изменён </summary>
  public bool NonNumneringPartsChanged
  {
    get => this._NonNumneringPartsChanged;
    set => this._NonNumneringPartsChanged = value;
  }

  /// <summary> Признак того, что список ненумеруемых разделов спецификации был изменён </summary>
  public bool NonNumneringRazdelsChanged
  {
    get => this._NonNumneringRazdelsChanged;
    set => this._NonNumneringRazdelsChanged = value;
  }

  /// <summary> Свод правил для определения "похожести" обозначений </summary>
  public CompareDesignationSchema CompareDesignationSchema => this._CompareDesignationSchema;

  /// <summary> Вышестоящий уровень настроек </summary>
  public SpecifNumberingFull ParentLevel => this._ParentLevel;

  /// <summary>Получить схему нумерации по уровню настроек</summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема нумерации </returns>
  public SpecifNumberingFull GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._ParentLevel != null ? this._ParentLevel.GetSchemaByLevel(level) : (SpecifNumberingFull) null;
  }

  /// <summary> Признак того, что объект был взят на изменение для того, чтобы редактировать схему </summary>
  public bool WasCheckedOutForEdit
  {
    get => this._WasCheckedOutForEdit;
    set => this._WasCheckedOutForEdit = value;
  }

  /// <summary> Признак того, что схема недоступна для редактирования </summary>
  public bool ReadOnly => this._ReadOnly;

  /// <summary> Загрузка списка ненумеруемых разделов по умолчанию </summary>
  public long[] LoadDefaultNonNumneringRazdels()
  {
    if (this.ParentLevel != null)
      return (long[]) this.ParentLevel._NonNumneringRazdels.Clone();
    return new long[2]
    {
      AVSDocument.ObjID_SectionDocumentation,
      AVSDocument.ObjID_SectionComplex
    };
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public new bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    bool flag = false;
    if (readArgs.Reader.LocalName == "NonNumneringRazdels2")
    {
      Guid[] guidArray = (Guid[]) WriteReadXmlHelper.ReadArrayFromXml(typeof (Guid), readArgs);
      int num = 0;
      foreach (Guid guid in guidArray)
      {
        if (guid == Guid.Empty)
          ++num;
      }
      this._NonNumneringRazdels = new long[guidArray.Length - num];
      int index = 0;
      foreach (Guid razdelGuid in guidArray)
      {
        if (razdelGuid != Guid.Empty)
        {
          this._NonNumneringRazdels.SetValue((object) SpecifRazdelNumbering.GetRazdelIDByGuid(razdelGuid), index);
          ++index;
        }
      }
      this.NonNumneringRazdelsChanged = true;
      flag = true;
    }
    if (!flag)
      flag = base.ReadFieldFromXml(readArgs);
    if (this._SpecifRazdelNumbering != null && readArgs.Reader.LocalName == "SpecifRazdelNumbering")
      ((IWriteReadXml) this._SpecifRazdelNumbering).ReadFromXml(readArgs);
    if (this._CompareDesignationSchema != null && readArgs.Reader.LocalName == "CompareDesignationSchema")
      ((IWriteReadXml) this._CompareDesignationSchema).ReadFromXml(readArgs);
    return flag;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public new void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    base.WriteToXml(elementName, xw, objectRefId);
    if (this.NonNumneringRazdelsChanged || this.ParentLevel == null)
    {
      Guid[] array = new Guid[this._NonNumneringRazdels.Length];
      Guid empty = Guid.Empty;
      for (int index = 0; index < this._NonNumneringRazdels.Length; ++index)
      {
        Guid razdelGuidById = SpecifRazdelNumbering.GetRazdelGuidByID(this._NonNumneringRazdels[index]);
        array.SetValue((object) razdelGuidById, index);
      }
      WriteReadXmlHelper.WriteArrayToXml("NonNumneringRazdels2", (IList) array, "NonNumneringRazdel", xw, objectRefId);
    }
    if (this._SpecifRazdelNumbering != null && (this._SpecifRazdelNumbering.Changed || this.ParentLevel == null))
      ((IWriteReadXml) this._SpecifRazdelNumbering).WriteToXml("SpecifRazdelNumbering", xw, objectRefId);
    if (this._CompareDesignationSchema != null && (this._CompareDesignationSchema.Changed || this.ParentLevel == null))
      ((IWriteReadXml) this._CompareDesignationSchema).WriteToXml("CompareDesignationSchema", xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public override void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (!this.NonNumneringRazdelsChanged)
      this._NonNumneringRazdels = this.LoadDefaultNonNumneringRazdels();
    Array array1 = ArrayEditHelper.DeleteValues((Array) this._NonNumneringRazdels, (object) 0);
    if (array1.Length != this._NonNumneringRazdels.Length)
      this._NonNumneringRazdels = (long[]) array1;
    Array array2 = ArrayEditHelper.DeleteValues((Array) this._NonNumneringParts, (object) 0);
    if (array2.Length == this._NonNumneringParts.Length)
      return;
    this._NonNumneringParts = (long[]) array2;
  }

  /// <summary>
  /// Прочитать параметры объекта из потока, содержащего XML документ
  /// </summary>
  /// <param name="stream">Поток, содержащий XML документ</param>
  public void LoadFromXmlDocument(Stream stream)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, stream, (IWriteReadXml) this, this.GetType().ToString());
  }

  /// <summary>
  /// Сохранить параметры объекта в поток, содержащий XML документ
  /// </summary>
  /// <param name="stream">Поток, содержащий XML документ</param>
  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, this.GetType().ToString());
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public new SpecifNumbering Clone()
  {
    SpecifNumberingFull instance = (SpecifNumberingFull) Activator.CreateInstance(this.GetType());
    instance.CopyParamsFrom(this);
    return (SpecifNumbering) instance;
  }

  /// <summary>Копировать параметры в другую схему</summary>
  /// <returns>Копия схемы</returns>
  public void CopyParamsFrom(SpecifNumberingFull copy)
  {
    this.CopyParamsFrom((SpecifNumbering) copy);
    this._NonNumneringParts = (long[]) copy._NonNumneringParts.Clone();
    this._NonNumneringRazdels = (long[]) copy._NonNumneringRazdels.Clone();
    this._SpecifRazdelNumbering = copy._SpecifRazdelNumbering.Clone();
    this._CompareDesignationSchema = copy.CompareDesignationSchema.Clone();
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
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, false);
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_NumberingSchema);
        if (attributeById != null)
        {
          this._ReadOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            this.LoadFromXmlDocument((Stream) aDestStream);
          if (this._CompareDesignationSchema.SubStrs.Length == 0)
            this._CompareDesignationSchema.LoadDefaultSchema();
          if (!this.SpecifRazdelNumbering.Changed)
            this.SpecifRazdelNumbering.LoadDefaultSchema();
          if (!this.NonNumneringRazdelsChanged)
            this._NonNumneringRazdels = this.LoadDefaultNonNumneringRazdels();
        }
        else
          this._ReadOnly = false;
        if (this._ReadOnly || objectActual.ObjectModifyMode != ObjectModifyModes.CantModify && objectActual.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          return;
        this._ReadOnly = true;
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
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_NumberingSchema);
  }

  /// <summary> Сбросить настройки к значениям по умолчанию </summary>
  public override void Clear()
  {
    if (this.ReadOnly)
      return;
    base.Clear();
    this._NonNumneringParts = new long[0];
    this._NonNumneringRazdels = new long[0];
    this.NonNumneringRazdelsChanged = false;
    this._NonNumneringRazdels = this.LoadDefaultNonNumneringRazdels();
    this._WasCheckedOutForEdit = false;
    this._SpecifRazdelNumbering = new SpecifRazdelNumbering(this);
    this._CompareDesignationSchema = new CompareDesignationSchema(this);
  }

  /// <summary>
  /// Загрузка параметров по-умолчанию для корня дерева настроек
  /// </summary>
  protected new void LoadRootParams() => base.LoadRootParams();
}
