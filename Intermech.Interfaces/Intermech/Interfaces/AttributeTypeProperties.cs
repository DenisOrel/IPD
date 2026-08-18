
// Type: Intermech.Interfaces.AttributeTypeProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Структура, содержащая свойства атрибута.</summary>
    [DebuggerDisplay("{Name}[{ShortName}] attId={AttributeID}, type={FieldType}")]
    [Serializable]
    public struct AttributeTypeProperties
    {
      /// <summary>Идентификатор атрибута (только для чтения)</summary>
      public int AttributeID;
      /// <summary>Имя атрибута</summary>
      public string Name;
      /// <summary>Короткое имя атрибута</summary>
      public string ShortName;
      /// <summary>
      /// Альтернативное имя атрибута (для хранения понятий Техкарда)
      /// </summary>
      public string Alias;
      /// <summary>Комментарии</summary>
      public string Note;
      /// <summary>Тип атрибута (строковый, числовой, т.д.)</summary>
      public FieldTypes FieldType;
      /// <summary>Значение по умолчанию</summary>
      public object DefaultValue;
      /// <summary>
      /// Может ли принимать множественные значения и каким образом.
      /// </summary>
      public MultiValueModes MultiValueMode;
      /// <summary>Способ вычисления параметра.</summary>
      public ComputeValueModes Computed;
      /// <summary>
      /// 1. для строковых параметров - максимальная длина строки,
      /// 2. для ссылки на объект - идентификатор типа объекта,
      /// 3. для таблицы из внешней БД - ссылка на объект, описывающий эту БД, таблицу и
      /// поля с ключом и значением.
      /// </summary>
      public long SizeType;
      /// <summary>
      /// Формула вычисления значения поля. Для ссылок на объекты содержит номер атрибута,
      /// значение которого будет показываться методом AsString атрибута.
      /// </summary>
      public string Formula;
      /// <summary>Метод контроля уникальности значений атрибута</summary>
      public UniqueValueModes Unique;
      /// <summary>Идентификатор уровня продвижения.</summary>
      public int LevelID;
      /// <summary>Идентификатор языка</summary>
      public string LanguageID;
      /// <summary>Идентификатор предметной области</summary>
      public string AreaID;
      /// <summary>Глобальный идентификатор атрибута</summary>
      public Guid AttributeGuid;
      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      public OptimizationModes OptimizationMode;
      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      public bool IsContent;
      /// <summary>Опции атрибута (см. описание AttributeOptions)</summary>
      public AttributeOptions Options;
      /// <summary>Маска ввода значения атрибута</summary>
      public string Mask;
      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      public int MasterAttributeID;
      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      public int SourceAttributeID;
      /// <summary>
      /// Список допустимых значений (записывается только если != null)
      /// </summary>
      public DataTable PossibleValues;
      /// <summary>
      /// Список расширений метаданных в формате Имя параметра=Массив значений (либо значение).
      /// Содержит null если нет расширений.
      /// </summary>
      private ListDictionary _MetadataExtensions;

      public AttributeTypeProperties(string _name, FieldTypes _fieldType)
      {
        this.AttributeID = 0;
        this.Name = _name;
        this.ShortName = string.Empty;
        this.Alias = string.Empty;
        this.Note = string.Empty;
        this.FieldType = _fieldType;
        this.DefaultValue = (object) null;
        this.MultiValueMode = MultiValueModes.SingleValue;
        this.Computed = ComputeValueModes.NotComputableValue;
        this.SizeType = _fieldType != FieldTypes.ftString ? 0L : 10L;
        this.Formula = string.Empty;
        this.Unique = UniqueValueModes.NotUnique;
        this.LevelID = 0;
        this.LanguageID = string.Empty;
        this.AreaID = string.Empty;
        this.AttributeGuid = Guid.NewGuid();
        this.OptimizationMode = OptimizationModes.Write;
        this.IsContent = false;
        this.Options = AttributeOptions.None;
        this.Mask = string.Empty;
        this.MasterAttributeID = 0;
        this.SourceAttributeID = 0;
        this.PossibleValues = (DataTable) null;
        this._MetadataExtensions = (ListDictionary) null;
      }

      public AttributeTypeProperties(
        int _attributeID,
        string _name,
        string _shortName,
        string _alias,
        string _note,
        FieldTypes _fieldType,
        object _defaultValue,
        MultiValueModes _multiValueMode,
        ComputeValueModes _computed,
        long _sizeType,
        string _formula,
        UniqueValueModes _unique,
        int _levelID,
        string _languageID,
        string _areaID,
        Guid _attributeGuid,
        OptimizationModes optimizationMode,
        bool isContent,
        AttributeOptions _Options,
        string _Mask,
        int _MasterAttributeID,
        int _SourceAttributeID)
      {
        this.AttributeID = _attributeID;
        this.Name = _name;
        this.ShortName = _shortName;
        this.Alias = _alias;
        this.Note = _note;
        this.FieldType = _fieldType;
        this.DefaultValue = _defaultValue;
        this.MultiValueMode = _multiValueMode;
        this.Computed = _computed;
        this.SizeType = _sizeType;
        this.Formula = _formula;
        this.Unique = _unique;
        this.LevelID = _levelID;
        this.LanguageID = _languageID;
        this.AreaID = _areaID;
        this.AttributeGuid = _attributeGuid;
        this.OptimizationMode = optimizationMode;
        this.IsContent = isContent;
        this.Options = _Options;
        this.Mask = _Mask;
        this.MasterAttributeID = _MasterAttributeID;
        this.SourceAttributeID = _SourceAttributeID;
        this.PossibleValues = (DataTable) null;
        this._MetadataExtensions = (ListDictionary) null;
      }

      public AttributeTypeProperties(DataRow row)
      {
        this.AttributeID = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        this.Name = row["F_NAME"].ToString();
        this.ShortName = row["F_SHORT_NAME"].ToString();
        this.Alias = row["F_ALIAS"].ToString();
        this.Note = row["F_NOTE"].ToString();
        this.FieldType = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        this.DefaultValue = (object) row["F_DEFAULT_VALUE"].ToString();
        this.MultiValueMode = (MultiValueModes) Convert.ToInt32(row["F_MULTIPLE_VALUED"]);
        this.Computed = (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]);
        this.SizeType = Convert.ToInt64(row["F_SIZE_TYPE"]);
        this.Formula = row["F_FORMULA"].ToString();
        this.Unique = (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]);
        this.LevelID = Convert.ToInt32(row["F_LEVEL_ID"]);
        this.LanguageID = row["F_LANGUAGE_ID"].ToString();
        this.AreaID = row["F_AREA_ID"].ToString();
        this.AttributeGuid = new Guid(row["F_GUID"].ToString());
        this.OptimizationMode = (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]);
        this.IsContent = Convert.ToInt32(row["F_CONTENT"]) == 1;
        this.Options = (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]);
        this.Mask = row["F_MASK"].ToString();
        this.MasterAttributeID = Convert.ToInt32(row["F_MASTER_ID"]);
        this.SourceAttributeID = Convert.ToInt32(row["F_SOURCE_ID"]);
        this.PossibleValues = (DataTable) null;
        this._MetadataExtensions = (ListDictionary) null;
      }

      public AttributeTypeProperties(AttributeTypeProperties atp)
        : this(atp.AttributeID, atp.Name, atp.ShortName, atp.Alias, atp.Note, atp.FieldType, atp.DefaultValue, atp.MultiValueMode, atp.Computed, atp.SizeType, atp.Formula, atp.Unique, atp.LevelID, atp.LanguageID, atp.AreaID, atp.AttributeGuid, atp.OptimizationMode, atp.IsContent, atp.Options, atp.Mask, atp.MasterAttributeID, atp.SourceAttributeID)
      {
      }

      public override string ToString()
      {
        return !string.IsNullOrEmpty(this.ShortName) ? $"{this.Name} [{this.ShortName}]" : this.Name;
      }

      /// <summary>
      /// Список расширений метаданных в формате Имя параметра=Массив значений (либо значение).
      /// </summary>
      public ListDictionary MetadataExtensions
      {
        get
        {
          if (this._MetadataExtensions == null)
            this._MetadataExtensions = new ListDictionary();
          return this._MetadataExtensions;
        }
      }
    }
}
