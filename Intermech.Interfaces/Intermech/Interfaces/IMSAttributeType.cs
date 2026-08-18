
// Type: Intermech.Interfaces.IMSAttributeType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о типе атрибута
    /// </summary>
    [Serializable]
    public sealed class IMSAttributeType : 
      MetaDataCacheItem,
      IComparable,
      IComparable<IMSAttributeType>,
      IDisplayable
    {
      /// <summary>Идентификатор атрибута</summary>
      private int attributeID;
      /// <summary>Имя атрибута</summary>
      private string name;
      /// <summary>Короткое имя атрибута</summary>
      private string shortName;
      /// <summary>
      /// Альтернативное имя атрибута (для хранения понятий Техкарда)
      /// </summary>
      private string alias;
      /// <summary>Комментарии</summary>
      private string note;
      /// <summary>Тип данных атрибута (строковый, числовой, т.д.)</summary>
      private FieldTypes fieldType;
      /// <summary>
      /// Реальный тип данных атрибута (если FieldType == FieldTypes.ftSystem)
      /// </summary>
      private FieldTypes realFieldType;
      /// <summary>Значение по умолчанию</summary>
      private object defaultValue;
      /// <summary>
      /// Может ли принимать множественные значения и каким образом.
      /// </summary>
      private MultiValueModes multiValueMode;
      /// <summary>Способ вычисления параметра.</summary>
      private ComputeValueModes computed;
      /// <summary>
      /// 1. для строковых параметров - максимальная длина строки,
      /// 2. для ссылки на объект - идентификатор типа объекта,
      /// 3. для таблицы из внешней БД - ссылка на объект, описывающий эту БД, таблицу и
      /// поля с ключом и значением.
      /// </summary>
      private long sizeType;
      /// <summary>
      /// Формула вычисления значения поля. Для ссылок на объекты содержит номер атрибута,
      /// значение которого будет показываться методом AsString атрибута.
      /// </summary>
      private string formula;
      /// <summary>Метод контроля уникальности значений атрибута</summary>
      private UniqueValueModes unique;
      /// <summary>Идентификатор уровня продвижения.</summary>
      private int levelID;
      /// <summary>Идентификатор языка</summary>
      private string languageID;
      /// <summary>Идентификатор предметной области</summary>
      private string areaID;
      /// <summary>Глобальный идентификатор атрибута</summary>
      private Guid attributeGuid;
      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      private OptimizationModes optimizationMode;
      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      private bool isContent;
      /// <summary>Опции атрибута</summary>
      private AttributeOptions options;
      /// <summary>Маска ввода значения атрибута</summary>
      private string mask;
      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      private int masterAttributeID;
      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      private int sourceAttributeID;
      /// <summary>
      /// Имя поля в таблице IMS_POSSIBLE_VALUES, в котором хранятся допустимые значения атрибута
      /// </summary>
      private string valueFieldName;
      /// <summary>
      /// 
      /// </summary>
      private string textFieldName;
      /// <summary>
      /// Имя поля в таблице IMS_POSSIBLE_VALUES, которое используется для хранения значения допустимых значений атрибута
      /// </summary>
      private string possibleValueFieldName;
      /// <summary>Список допустимых значений</summary>
      private List<object> possibleValues;
      /// <summary>Список описаний допустимых значений</summary>
      private List<object> possibleValuesDescriptions;
      private string[] fieldNames;

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSAttributeType imsAttributeType) ? base.Equals(obj) : this.AttributeID == imsAttributeType.AttributeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
      /// <returns>32-битный хэш-код экземпляра объекта</returns>
      public override int GetHashCode() => this.AttributeID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => $"Attribute: [{this.AttributeID}] {this.Name}";

      /// <summary>Идентификатор атрибута</summary>
      public int AttributeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.attributeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AttributeID));
          this.attributeID = value;
        }
      }

      /// <summary>Имя атрибута</summary>
      public string Name
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.name;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Name));
          this.name = value;
        }
      }

      /// <summary>Короткое имя атрибута</summary>
      public string ShortName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.shortName;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ShortName));
          this.shortName = value;
        }
      }

      /// <summary>
      /// Альтернативное имя атрибута (для хранения понятий Техкарда)
      /// </summary>
      public string Alias
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.alias;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Alias));
          this.alias = value;
        }
      }

      /// <summary>Комментарии</summary>
      public string Note
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.note;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Note));
          this.note = value;
        }
      }

      /// <summary>Тип данных атрибута (строковый, числовой, т.д.)</summary>
      public FieldTypes FieldType
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.fieldType;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (FieldType));
          this.fieldType = value;
        }
      }

      /// <summary>
      /// Реальный тип данных атрибута (если FieldType == FieldTypes.ftSystem)
      /// </summary>
      public FieldTypes RealFieldType
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.realFieldType;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RealFieldType));
          this.realFieldType = value;
        }
      }

      /// <summary>Значение по умолчанию</summary>
      public object DefaultValue
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.defaultValue;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (DefaultValue));
          this.defaultValue = value;
        }
      }

      /// <summary>
      /// Может ли принимать множественные значения и каким образом.
      /// </summary>
      public MultiValueModes MultiValueMode
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.multiValueMode;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (MultiValueMode));
          this.multiValueMode = value;
        }
      }

      /// <summary>Способ вычисления параметра.</summary>
      public ComputeValueModes Computed
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.computed;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Computed));
          this.computed = value;
        }
      }

      /// <summary>
      /// 1. для строковых параметров - максимальная длина строки,
      /// 2. для ссылки на объект - идентификатор типа объекта,
      /// 3. для таблицы из внешней БД - ссылка на объект, описывающий эту БД, таблицу и
      /// поля с ключом и значением.
      /// 4. ид. физ. величины для единиц измерения
      /// </summary>
      public long SizeType
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.sizeType;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SizeType));
          this.sizeType = value;
        }
      }

      /// <summary>
      /// Формула вычисления значения поля. Для ссылок на объекты содержит номер атрибута,
      /// значение которого будет показываться методом AsString атрибута.
      /// </summary>
      public string Formula
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.formula;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Formula));
          this.formula = value;
        }
      }

      /// <summary>Метод контроля уникальности значений атрибута</summary>
      public UniqueValueModes Unique
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.unique;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Unique));
          this.unique = value;
        }
      }

      /// <summary>Идентификатор уровня продвижения.</summary>
      public int LevelID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.levelID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LevelID));
          this.levelID = value;
        }
      }

      /// <summary>Идентификатор языка</summary>
      public string LanguageID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.languageID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (LanguageID));
          this.languageID = value;
        }
      }

      /// <summary>Идентификатор предметной области</summary>
      public string AreaID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.areaID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AreaID));
          this.areaID = value;
        }
      }

      /// <summary>Глобальный идентификатор атрибута</summary>
      public Guid AttributeGuid
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.attributeGuid;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AttributeGuid));
          this.attributeGuid = value;
        }
      }

      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      public OptimizationModes OptimizationMode
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.optimizationMode;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (OptimizationMode));
          this.optimizationMode = value;
        }
      }

      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      public bool IsContent
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.isContent;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (IsContent));
          this.isContent = value;
        }
      }

      /// <summary>Опции атрибута</summary>
      public AttributeOptions Options
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>Маска ввода значения атрибута</summary>
      public string Mask
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.mask;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Mask));
          this.mask = value;
        }
      }

      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      public int MasterAttributeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.masterAttributeID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (MasterAttributeID));
          this.masterAttributeID = value;
        }
      }

      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      public int SourceAttributeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.sourceAttributeID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SourceAttributeID));
          this.sourceAttributeID = value;
        }
      }

      /// <summary>
      /// Имя поля в таблице IMS_POSSIBLE_VALUES, в котором хранятся допустимые значения атрибута
      /// </summary>
      public string ValueFieldName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.valueFieldName;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ValueFieldName));
          this.valueFieldName = value;
        }
      }

      /// <summary>
      /// 
      /// </summary>
      public string TextFieldName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.textFieldName;
      }

      /// <summary>
      /// Имя поля в таблице IMS_POSSIBLE_VALUES, которое используется для хранения значения допустимых значений атрибута
      /// </summary>
      public string PossibleValueFieldName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.possibleValueFieldName;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (PossibleValueFieldName));
          this.possibleValueFieldName = value;
        }
      }

      /// <summary>Список допустимых значений</summary>
      public List<object> PossibleValues
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.possibleValues;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (PossibleValues));
          this.possibleValues = value;
        }
      }

      /// <summary>Список описаний допустимых значений</summary>
      public List<object> PossibleValuesDescriptions
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.possibleValuesDescriptions;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (PossibleValuesDescriptions));
          this.possibleValuesDescriptions = value;
        }
      }

      /// <summary>Можно ли отображать атрибут</summary>
      public bool IsGridable
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this.FieldType != FieldTypes.ftPassword && this.Computed != ComputeValueModes.IndexValue;
        }
      }

      public string[] FieldNames
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.fieldNames;
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.AttributeID = 0;
        this.Name = (string) null;
        this.ShortName = (string) null;
        this.Alias = (string) null;
        this.Note = (string) null;
        this.FieldType = FieldTypes.ftUnknown;
        this.RealFieldType = FieldTypes.ftUnknown;
        this.DefaultValue = (object) null;
        this.MultiValueMode = MultiValueModes.SingleValue;
        this.Computed = ComputeValueModes.NotComputableValue;
        this.SizeType = 0L;
        this.Formula = (string) null;
        this.Unique = UniqueValueModes.NotUnique;
        this.LevelID = 0;
        this.LanguageID = (string) null;
        this.AreaID = (string) null;
        this.AttributeGuid = Guid.Empty;
        this.OptimizationMode = OptimizationModes.Write;
        this.IsContent = false;
        this.Options = AttributeOptions.None;
        this.Mask = (string) null;
        this.MasterAttributeID = 0;
        this.SourceAttributeID = 0;
        this.ValueFieldName = (string) null;
        this.PossibleValueFieldName = (string) null;
        this.PossibleValues = (List<object>) null;
        this.PossibleValuesDescriptions = (List<object>) null;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSAttributeType imsAttributeType))
          return;
        this.AttributeID = imsAttributeType.AttributeID;
        this.Name = imsAttributeType.Name;
        this.ShortName = imsAttributeType.ShortName;
        this.Alias = imsAttributeType.Alias;
        this.Note = imsAttributeType.Note;
        this.FieldType = imsAttributeType.FieldType;
        this.RealFieldType = imsAttributeType.RealFieldType;
        this.DefaultValue = imsAttributeType.DefaultValue;
        this.MultiValueMode = imsAttributeType.MultiValueMode;
        this.Computed = imsAttributeType.Computed;
        this.SizeType = imsAttributeType.SizeType;
        this.Formula = imsAttributeType.Formula;
        this.Unique = imsAttributeType.Unique;
        this.LevelID = imsAttributeType.LevelID;
        this.LanguageID = imsAttributeType.LanguageID;
        this.AreaID = imsAttributeType.AreaID;
        this.AttributeGuid = imsAttributeType.AttributeGuid;
        this.OptimizationMode = imsAttributeType.OptimizationMode;
        this.IsContent = imsAttributeType.IsContent;
        this.Options = imsAttributeType.Options;
        this.Mask = imsAttributeType.Mask;
        this.MasterAttributeID = imsAttributeType.MasterAttributeID;
        this.SourceAttributeID = imsAttributeType.SourceAttributeID;
        this.ValueFieldName = imsAttributeType.ValueFieldName;
        this.PossibleValueFieldName = imsAttributeType.PossibleValueFieldName;
        if (imsAttributeType.PossibleValues != null)
          this.PossibleValues = new List<object>((IEnumerable<object>) imsAttributeType.PossibleValues);
        if (imsAttributeType.PossibleValuesDescriptions == null)
          return;
        this.PossibleValuesDescriptions = new List<object>((IEnumerable<object>) imsAttributeType.PossibleValuesDescriptions);
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSAttributeType Clone() => (IMSAttributeType) base.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as IMSAttributeType);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttributeType other)
      {
        return other == null ? 1 : this.AttributeID.CompareTo(other.AttributeID);
      }

      /// <summary>Отображаемый на экране текст</summary>
      public string Text
      {
        [DebuggerStepThrough] get => this.Name;
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.AttributeID = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        this.Name = Convert.ToString(row["F_NAME"]);
        this.ShortName = Convert.ToString(row["F_SHORT_NAME"]);
        this.Alias = Convert.ToString(row["F_ALIAS"]);
        this.Note = Convert.ToString(row["F_NOTE"]);
        this.FieldType = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        this.RealFieldType = this.FieldType == FieldTypes.ftSystem ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) this.AttributeID) : this.FieldType;
        this.DefaultValue = (object) Convert.ToString(row["F_DEFAULT_VALUE"]);
        this.MultiValueMode = (MultiValueModes) Convert.ToInt32(row["F_MULTIPLE_VALUED"]);
        this.Computed = (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]);
        this.SizeType = Convert.ToInt64(row["F_SIZE_TYPE"]);
        this.Formula = Convert.ToString(row["F_FORMULA"]);
        this.Unique = (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]);
        this.LevelID = Convert.ToInt32(row["F_LEVEL_ID"]);
        this.LanguageID = Convert.ToString(row["F_LANGUAGE_ID"]);
        this.AreaID = Convert.ToString(row["F_AREA_ID"]);
        this.AttributeGuid = new Guid(Convert.ToString(row["F_GUID"]));
        this.OptimizationMode = (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]);
        this.IsContent = Convert.ToInt32(row["F_CONTENT"]) == 1;
        this.Options = (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]);
        this.Mask = Convert.ToString(row["F_MASK"]);
        this.MasterAttributeID = Convert.ToInt32(row["F_MASTER_ID"]);
        this.SourceAttributeID = Convert.ToInt32(row["F_SOURCE_ID"]);
        this.ValueFieldName = "F_STRING_VALUE";
        this.PossibleValueFieldName = "F_STRING_VALUE";
        this.textFieldName = "F_STRING_VALUE";
        List<FieldTypes> convertList = new List<FieldTypes>();
        RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
        bool computableAttribute = false;
        AttributeCacheHelper.GetAttributeTypeValues(this.FieldType, this.AttributeID, ref this.valueFieldName, ref this.textFieldName, ref convertList, ref enabledOperators, ref computableAttribute, ref this.possibleValueFieldName);
        this.fieldNames = AttributeCacheHelper.GetAtributeFieldNames(this.FieldType, this.AttributeID);
        this.PossibleValues = (List<object>) null;
        this.PossibleValuesDescriptions = (List<object>) null;
      }
    }
}
