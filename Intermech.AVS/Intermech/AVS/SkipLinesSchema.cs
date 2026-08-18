// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SkipLinesSchema
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

/// <summary>Схема пропуска строк в спецификации</summary>
public class SkipLinesSchema : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  private long _ownerObjectID = -1;
  private SkipLinesSchema _parent;
  private SettingsLevel _level;
  private bool _readOnly;
  private int _betweenDifferentDesignations = -1;
  private int _betweenSameDesignations = -1;
  private int _betweenArtVariants = -1;
  private int _betweenDifferentObjTypes = -1;
  private int _betweenSameObjTypes = -1;
  private int _beforeSectionName = -1;
  private int _afterSectionName = -1;
  private int _beforeVariableData = -1;
  private int _afterVariableData = -1;
  private int _beforeVariantNumber = -1;
  private int _afterVariantNumber = -1;
  private int _beforeNote = -1;
  private int _afterNote = -1;
  private int _beforeAdd1 = -1;
  private int _afterAdd1 = -1;
  private int _beforeAdd2 = -1;
  private bool? _nonSkipBeforeAtStartPage;
  private int _beforeAdditional = -1;
  private int _afterAdditional = -1;
  private int _beforeDynamicGroup = -1;
  private int _afterDynamicGroup = -1;
  private NumberingPositionsEnum? _numberingPositions;
  private CompareDesignationSchema _compareDesignationSchema;

  public SkipLinesSchema(SkipLinesSchema parent, long ownerObjectID, SettingsLevel level)
  {
    this._parent = parent;
    this._level = level;
    this._ownerObjectID = ownerObjectID;
    this._compareDesignationSchema = new CompareDesignationSchema(this);
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
  public SkipLinesSchema Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  /// <summary> Между различными обозначаниями </summary>
  public int BetweenDifferentDesignations
  {
    get
    {
      if (this._betweenDifferentDesignations != -1)
        return this._betweenDifferentDesignations;
      return this._parent == null ? 0 : this._parent.BetweenDifferentDesignations;
    }
    set
    {
      if (this._parent != null && value == this._parent.BetweenDifferentDesignations)
        this._betweenDifferentDesignations = -1;
      else
        this._betweenDifferentDesignations = value;
    }
  }

  /// <summary> Между похожими обозначениями </summary>
  public int BetweenSameDesignations
  {
    get
    {
      if (this._betweenSameDesignations != -1)
        return this._betweenSameDesignations;
      return this._parent == null ? 0 : this._parent.BetweenSameDesignations;
    }
    set
    {
      if (this._parent != null && value == this._parent.BetweenSameDesignations)
        this._betweenSameDesignations = -1;
      else
        this._betweenSameDesignations = value;
    }
  }

  /// <summary> Между исполнениями детали </summary>
  public int BetweenArtVariants
  {
    get
    {
      if (this._betweenArtVariants != -1)
        return this._betweenArtVariants;
      return this._parent == null ? 0 : this._parent.BetweenArtVariants;
    }
    set
    {
      if (this._parent != null && value == this._parent.BetweenArtVariants)
        this._betweenArtVariants = -1;
      else
        this._betweenArtVariants = value;
    }
  }

  /// <summary> Между объектами с различными типами </summary>
  public int BetweenDifferentObjTypes
  {
    get
    {
      if (this._betweenDifferentObjTypes != -1)
        return this._betweenDifferentObjTypes;
      return this._parent == null ? 0 : this._parent.BetweenDifferentObjTypes;
    }
    set
    {
      if (this._parent != null && value == this._parent.BetweenDifferentObjTypes)
        this._betweenDifferentObjTypes = -1;
      else
        this._betweenDifferentObjTypes = value;
    }
  }

  /// <summary> Между объектами с одинаковыми типами </summary>
  public int BetweenSameObjTypes
  {
    get
    {
      if (this._betweenSameObjTypes != -1)
        return this._betweenSameObjTypes;
      return this._parent == null ? 0 : this._parent.BetweenSameObjTypes;
    }
    set
    {
      if (this._parent != null && value == this._parent.BetweenSameObjTypes)
        this._betweenSameObjTypes = -1;
      else
        this._betweenSameObjTypes = value;
    }
  }

  /// <summary> Перед заголовком раздела </summary>
  public int BeforeSectionName
  {
    get
    {
      if (this._beforeSectionName != -1)
        return this._beforeSectionName;
      return this._parent == null ? 2 : this._parent.BeforeSectionName;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeSectionName)
        this._beforeSectionName = -1;
      else
        this._beforeSectionName = value;
    }
  }

  /// <summary> После заголовка раздела </summary>
  public int AfterSectionName
  {
    get
    {
      if (this._afterSectionName != -1)
        return this._afterSectionName;
      return this._parent == null ? 1 : this._parent.AfterSectionName;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterSectionName)
        this._afterSectionName = -1;
      else
        this._afterSectionName = value;
    }
  }

  /// <summary> Перед переменными данными </summary>
  public int BeforeVariableData
  {
    get
    {
      if (this._beforeVariableData != -1)
        return this._beforeVariableData;
      return this._parent == null ? 2 : this._parent.BeforeVariableData;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeVariableData)
        this._beforeVariableData = -1;
      else
        this._beforeVariableData = value;
    }
  }

  /// <summary> После переменных данных </summary>
  public int AfterVariableData
  {
    get
    {
      if (this._afterVariableData != -1)
        return this._afterVariableData;
      return this._parent == null ? 1 : this._parent.AfterVariableData;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterVariableData)
        this._afterVariableData = -1;
      else
        this._afterVariableData = value;
    }
  }

  /// <summary> Перед номером исполнения </summary>
  public int BeforeVariantNumber
  {
    get
    {
      if (this._beforeVariantNumber != -1)
        return this._beforeVariantNumber;
      return this._parent == null ? 1 : this._parent.BeforeVariantNumber;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeVariantNumber)
        this._beforeVariantNumber = -1;
      else
        this._beforeVariantNumber = value;
    }
  }

  /// <summary> После номера исполнения </summary>
  public int AfterVariantNumber
  {
    get
    {
      if (this._afterVariantNumber != -1)
        return this._afterVariantNumber;
      return this._parent == null ? 1 : this._parent.AfterVariantNumber;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterVariantNumber)
        this._afterVariantNumber = -1;
      else
        this._afterVariantNumber = value;
    }
  }

  /// <summary> Перед  динамической группой </summary>
  public int BeforeDynamicGroup
  {
    get
    {
      if (this._beforeDynamicGroup != -1)
        return this._beforeDynamicGroup;
      return this._parent == null ? 0 : this._parent.BeforeDynamicGroup;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeDynamicGroup)
        this._beforeDynamicGroup = -1;
      else
        this._beforeDynamicGroup = value;
    }
  }

  /// <summary> после динамической группы </summary>
  public int AfterDynamicGroup
  {
    get
    {
      if (this._afterDynamicGroup != -1)
        return this._afterDynamicGroup;
      return this._parent == null ? 0 : this._parent.AfterDynamicGroup;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterDynamicGroup)
        this._afterDynamicGroup = -1;
      else
        this._afterDynamicGroup = value;
    }
  }

  /// <summary> Перед примечанием </summary>
  public int BeforeNote
  {
    get
    {
      if (this._beforeNote != -1)
        return this._beforeNote;
      return this._parent == null ? 1 : this._parent.BeforeNote;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeNote)
        this._beforeNote = -1;
      else
        this._beforeNote = value;
    }
  }

  /// <summary> после примечания </summary>
  public int AfterNote
  {
    get
    {
      if (this._afterNote != -1)
        return this._afterNote;
      return this._parent == null ? 0 : this._parent.AfterNote;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterNote)
        this._afterNote = -1;
      else
        this._afterNote = value;
    }
  }

  /// <summary> Перед заголовком части </summary>
  public int BeforeAdditional
  {
    get
    {
      if (this._beforeAdditional != -1)
        return this._beforeAdditional;
      return this._parent == null ? 1 : this._parent.BeforeAdditional;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeAdditional)
        this._beforeAdditional = -1;
      else
        this._beforeAdditional = value;
    }
  }

  /// <summary> Установка пропусков строк по позициям </summary>
  public NumberingPositionsEnum NumberingPositions
  {
    get
    {
      if (this._numberingPositions.HasValue)
        return this._numberingPositions.Value;
      return this._parent == null ? NumberingPositionsEnum.NotUse : this._parent.NumberingPositions;
    }
    set
    {
      if (this._parent != null && value == this._parent.NumberingPositions)
        this._numberingPositions = new NumberingPositionsEnum?();
      else
        this._numberingPositions = new NumberingPositionsEnum?(value);
    }
  }

  /// <summary> После заголовка части </summary>
  public int AfterAdditional
  {
    get
    {
      if (this._afterAdditional != -1)
        return this._afterAdditional;
      return this._parent == null ? 1 : this._parent.AfterAdditional;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterAdditional)
        this._afterAdditional = -1;
      else
        this._afterAdditional = value;
    }
  }

  /// <summary> Перед Дополнительной 1 </summary>
  public int BeforeAdd1
  {
    get
    {
      if (this._beforeAdd1 != -1)
        return this._beforeAdd1;
      return this._parent == null ? 1 : this._parent.BeforeAdd1;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeAdd1)
        this._beforeAdd1 = -1;
      else
        this._beforeAdd1 = value;
    }
  }

  /// <summary> После Дополнительной 1 </summary>
  public int AfterAdd1
  {
    get
    {
      if (this._afterAdd1 != -1)
        return this._afterAdd1;
      return this._parent == null ? 1 : this._parent.AfterAdd1;
    }
    set
    {
      if (this._parent != null && value == this._parent.AfterAdd1)
        this._afterAdd1 = -1;
      else
        this._afterAdd1 = value;
    }
  }

  /// <summary> Перед Дополнительной 2 </summary>
  public int BeforeAdd2
  {
    get
    {
      if (this._beforeAdd2 != -1)
        return this._beforeAdd2;
      return this._parent == null ? 1 : this._parent.BeforeAdd2;
    }
    set
    {
      if (this._parent != null && value == this._parent.BeforeAdd2)
        this._beforeAdd2 = -1;
      else
        this._beforeAdd2 = value;
    }
  }

  /// <summary> Игнорировать пропуски в начале страницы </summary>
  public bool NonSkipBeforeAtStartPage
  {
    get
    {
      if (this._nonSkipBeforeAtStartPage.HasValue)
        return this._nonSkipBeforeAtStartPage.Value;
      return this._parent == null || this._parent.NonSkipBeforeAtStartPage;
    }
    set
    {
      if (this._parent != null && value == this._parent.NonSkipBeforeAtStartPage)
        this._nonSkipBeforeAtStartPage = new bool?();
      else
        this._nonSkipBeforeAtStartPage = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BetweenDifferentDesignationsChanged
  {
    get => this._parent != null && this._betweenDifferentDesignations != -1;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BetweenSameDesignationsChanged
  {
    get => this._parent != null && this._betweenSameDesignations != -1;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BetweenArtVariantsChanged => this._parent != null && this._betweenArtVariants != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BetweenDifferentObjTypesChanged
  {
    get => this._parent != null && this._betweenDifferentObjTypes != -1;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BetweenSameObjTypesChanged => this._parent != null && this._betweenSameObjTypes != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeSectionNameChanged => this._parent != null && this._beforeSectionName != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterSectionNameChanged => this._parent != null && this._afterSectionName != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeVariableDataChanged => this._parent != null && this._beforeVariableData != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterVariableDataChanged => this._parent != null && this._afterVariableData != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeVariantNumberChanged => this._parent != null && this._beforeVariantNumber != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterVariantNumberChanged => this._parent != null && this._afterVariantNumber != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeNoteChanged => this._parent != null && this._beforeNote != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterDynamicGroupChanged => this._parent != null && this._afterDynamicGroup != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeDynamicGroupChanged => this._parent != null && this._beforeDynamicGroup != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterNoteChanged => this._parent != null && this._afterNote != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeAdditionalChanged => this._parent != null && this._beforeAdditional != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool NumberingPositionsChanged
  {
    get => this._parent != null && this._numberingPositions.HasValue;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterAdditionalChanged => this._parent != null && this._afterAdditional != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeAdd1Changed => this._parent != null && this._beforeAdd1 != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool AfterAdd1Changed => this._parent != null && this._afterAdd1 != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool BeforeAdd2Changed => this._parent != null && this._beforeAdd2 != -1;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool NonSkipBeforeAtStartPageChanged
  {
    get => this._parent != null && this._nonSkipBeforeAtStartPage.HasValue;
  }

  /// <summary> Схема сравнения обозначений </summary>
  public CompareDesignationSchema CompareDesignationSchema => this._compareDesignationSchema;

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultParams()
  {
    if (this._parent == null)
    {
      this._betweenDifferentDesignations = 0;
      this._betweenSameDesignations = 0;
      this._betweenArtVariants = 0;
      this._betweenDifferentObjTypes = 0;
      this._betweenSameObjTypes = 0;
      this._beforeSectionName = 2;
      this._afterSectionName = 1;
      this._beforeVariableData = 2;
      this._afterVariableData = 1;
      this._beforeVariantNumber = 1;
      this._afterVariantNumber = 1;
      this._beforeNote = 1;
      this._afterNote = 0;
      this._beforeDynamicGroup = 0;
      this._afterDynamicGroup = 0;
      this._beforeAdditional = 1;
      this._afterAdditional = 1;
      this._beforeAdd1 = 1;
      this._afterAdd1 = 1;
      this._beforeAdd2 = 1;
      this._nonSkipBeforeAtStartPage = new bool?(true);
      this._numberingPositions = new NumberingPositionsEnum?();
      this._compareDesignationSchema.LoadDefaultSchema();
    }
    else
    {
      if (this._compareDesignationSchema.Changed)
      {
        SkipLinesSchema parent = this._parent;
        while (!parent.CompareDesignationSchema.Changed || parent.Parent != null)
          parent = parent.Parent;
        this._compareDesignationSchema.CopyParamsFrom(parent.CompareDesignationSchema);
        this._compareDesignationSchema.Changed = false;
      }
      this._betweenDifferentDesignations = -1;
      this._betweenSameDesignations = -1;
      this._betweenArtVariants = -1;
      this._betweenDifferentObjTypes = -1;
      this._betweenSameObjTypes = -1;
      this._beforeSectionName = -1;
      this._afterSectionName = -1;
      this._beforeVariableData = -1;
      this._afterVariableData = -1;
      this._beforeVariantNumber = -1;
      this._afterVariantNumber = -1;
      this._beforeNote = -1;
      this._afterNote = -1;
      this._beforeAdd1 = -1;
      this._afterAdd1 = -1;
      this._beforeAdd2 = -1;
      this._beforeDynamicGroup = -1;
      this._afterDynamicGroup = -1;
      this._nonSkipBeforeAtStartPage = new bool?();
    }
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public SkipLinesSchema Clone()
  {
    SkipLinesSchema skipLinesSchema = new SkipLinesSchema(this._parent, this._ownerObjectID, this._level);
    skipLinesSchema.CopyParamsFrom(this);
    return skipLinesSchema;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(SkipLinesSchema copy)
  {
    this._afterAdditional = copy._afterAdditional;
    this._beforeAdditional = copy._beforeAdditional;
    this._afterAdd1 = copy._afterAdd1;
    this._afterNote = copy._afterNote;
    this._beforeNote = copy._beforeNote;
    this._afterSectionName = copy._afterSectionName;
    this._afterVariableData = copy._afterVariableData;
    this._afterVariantNumber = copy._afterVariantNumber;
    this._beforeAdd1 = copy._beforeAdd1;
    this._beforeAdd2 = copy._beforeAdd2;
    this._afterDynamicGroup = copy._afterDynamicGroup;
    this._beforeDynamicGroup = copy._beforeDynamicGroup;
    this._beforeSectionName = copy._beforeSectionName;
    this._beforeVariableData = copy._beforeVariableData;
    this._beforeVariantNumber = copy._beforeVariantNumber;
    this._betweenArtVariants = copy._betweenArtVariants;
    this._betweenDifferentDesignations = copy._betweenDifferentDesignations;
    this._betweenDifferentObjTypes = copy._betweenDifferentObjTypes;
    this._betweenSameDesignations = copy._betweenSameDesignations;
    this._betweenSameObjTypes = copy._betweenSameObjTypes;
    this._nonSkipBeforeAtStartPage = copy._nonSkipBeforeAtStartPage;
    this._numberingPositions = copy._numberingPositions;
    this.CompareDesignationSchema.CopyParamsFrom(copy.CompareDesignationSchema);
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "AfterAdd1":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterAdd1 = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterAdditional":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterAdditional = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterDynamicGroup":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterDynamicGroup = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterNote":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterNote = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterSectionName":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterSectionName = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterVariableData":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterVariableData = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "AfterVariantNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.AfterVariantNumber = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeAdd1":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeAdd1 = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeAdd2":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeAdd2 = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeAdditional":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeAdditional = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeDynamicGroup":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeDynamicGroup = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeNote":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeNote = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeSectionName":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeSectionName = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeVariableData":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeVariableData = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BeforeVariantNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BeforeVariantNumber = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BetweenArtVariants":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BetweenArtVariants = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BetweenDifferentDesignations":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BetweenDifferentDesignations = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BetweenDifferentObjTypes":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BetweenDifferentObjTypes = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BetweenSameDesignations":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BetweenSameDesignations = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "BetweenSameObjTypes":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.BetweenSameObjTypes = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "NonSkipBeforeAtStartPage":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.NonSkipBeforeAtStartPage = readArgs.Reader.Value == "1";
        return true;
      case "NumberingPositions":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.NumberingPositions = (NumberingPositionsEnum) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      default:
        if (this._compareDesignationSchema != null && readArgs.Reader.LocalName == "CompareDesignationSchema")
          ((IWriteReadXml) this._compareDesignationSchema).ReadFromXml(readArgs);
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
      int num;
      if (this._parent == null || this._betweenDifferentDesignations != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenDifferentDesignations;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenDifferentDesignations", str);
      }
      if (this._parent == null || this._betweenSameDesignations != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenSameDesignations;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenSameDesignations", str);
      }
      if (this._parent == null || this._betweenArtVariants != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenArtVariants;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenArtVariants", str);
      }
      if (this._parent == null || this._betweenDifferentObjTypes != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenDifferentObjTypes;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenDifferentObjTypes", str);
      }
      if (this._parent == null || this._betweenSameObjTypes != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BetweenSameObjTypes;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BetweenSameObjTypes", str);
      }
      if (this._parent == null || this._beforeSectionName != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeSectionName;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeSectionName", str);
      }
      if (this._parent == null || this._afterSectionName != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterSectionName;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterSectionName", str);
      }
      if (this._parent == null || this._beforeVariableData != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeVariableData;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeVariableData", str);
      }
      if (this._parent == null || this._afterVariableData != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterVariableData;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterVariableData", str);
      }
      if (this._parent == null || this._beforeVariantNumber != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeVariantNumber;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeVariantNumber", str);
      }
      if (this._parent == null || this._afterVariantNumber != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterVariantNumber;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterVariantNumber", str);
      }
      if (this._parent == null || this._beforeNote != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeNote;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeNote", str);
      }
      if (this._parent == null || this._afterNote != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterNote;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterNote", str);
      }
      if (this._parent == null || this._beforeDynamicGroup != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeDynamicGroup;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeDynamicGroup", str);
      }
      if (this._parent == null || this._afterDynamicGroup != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterDynamicGroup;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterDynamicGroup", str);
      }
      if (this._parent == null || this._beforeAdditional != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeAdditional;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeAdditional", str);
      }
      if (this._parent == null || this._afterAdditional != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterAdditional;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterAdditional", str);
      }
      if (this._parent == null || this._beforeAdd1 != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeAdd1;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeAdd1", str);
      }
      if (this._parent == null || this._afterAdd1 != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.AfterAdd1;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("AfterAdd1", str);
      }
      if (this._parent == null || this._beforeAdd2 != -1)
      {
        XmlWriter xmlWriter = xw;
        num = this.BeforeAdd2;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("BeforeAdd2", str);
      }
      if (this._parent == null || this._numberingPositions.HasValue)
      {
        XmlWriter xmlWriter = xw;
        num = (int) this.NumberingPositions;
        string str = num.ToString();
        xmlWriter.WriteAttributeString("NumberingPositions", str);
      }
      if (this._nonSkipBeforeAtStartPage.HasValue)
        xw.WriteAttributeString("NonSkipBeforeAtStartPage", this._nonSkipBeforeAtStartPage.Value ? "1" : "0");
      else if (this._parent == null)
        xw.WriteAttributeString("NonSkipBeforeAtStartPage", this.NonSkipBeforeAtStartPage ? "1" : "0");
      if (this._compareDesignationSchema == null || !this._compareDesignationSchema.Changed && this._parent != null)
        return;
      ((IWriteReadXml) this._compareDesignationSchema).WriteToXml("CompareDesignationSchema", xw, objectRefId);
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
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, nameof (SkipLinesSchema));
  }

  /// <summary> Загрузка параметров из объекта с guid-ом = OwnerGuid </summary>
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
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_SkipLines);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, nameof (SkipLinesSchema));
          this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
        }
        else
        {
          this._readOnly = AvsIDCache.Attr_SkipLines == -1;
          this._compareDesignationSchema.LoadDefaultSchema();
        }
        if (this._compareDesignationSchema.SubStrs.Length == 0)
          this._compareDesignationSchema.LoadDefaultSchema();
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
    if (this.ReadOnly)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_SkipLines);
  }

  /// <summary> Получить схему сортировки по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема сортировки </returns>
  public SkipLinesSchema GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent != null ? this._parent.GetSchemaByLevel(level) : (SkipLinesSchema) null;
  }
}
