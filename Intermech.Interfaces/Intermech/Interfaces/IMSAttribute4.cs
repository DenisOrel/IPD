
// Type: Intermech.Interfaces.IMSAttribute4
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о типе атрибута для типа объекта/связи
    /// </summary>
    [Serializable]
    public abstract class IMSAttribute4 : MetaDataCacheItem, IComparable, IComparable<IMSAttribute4>
    {
      /// <summary>Идентификатор типа атрибута</summary>
      private int attributeID;
      /// <summary>Вычисляемый параметр или нет</summary>
      private ComputeValueModes computed;
      /// <summary>Способ добавления/удаления атрибута</summary>
      private RequiredModes required;
      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      private OptimizationModes optimizationMode;
      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      private bool isContent;
      /// <summary>Опции атрибута</summary>
      private AttributeOptions options;
      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      private int masterAttributeID;
      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      private int sourceAttributeID;
      /// <summary>Тип данных атрибута (строковый, числовой, т.д.)</summary>
      private FieldTypes fieldType;
      /// <summary>
      /// Реальный тип данных атрибута (если FieldType == FieldTypes.ftSystem)
      /// </summary>
      private FieldTypes realFieldType;
      /// <summary>
      /// Правило проверки (Intermech.Consts.ObjectLinkConstraint или String.Empty)
      /// </summary>
      private string validationRule;
      /// <summary>Маска атрибута.</summary>
      private string mask;
      /// <summary>Значение по умолчанию</summary>
      private string defaultValue;

      /// <summary>Идентификатор типа атрибута</summary>
      public int AttributeID
      {
        get => this.attributeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (AttributeID));
          this.attributeID = value;
        }
      }

      /// <summary>Вычисляемый параметр или нет</summary>
      public ComputeValueModes Computed
      {
        get => this.computed;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Computed));
          this.computed = value;
        }
      }

      /// <summary>Способ добавления/удаления атрибута</summary>
      public RequiredModes Required
      {
        get => this.required;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Required));
          this.required = value;
        }
      }

      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      public OptimizationModes OptimizationMode
      {
        get => this.optimizationMode;
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
        get => this.isContent;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (IsContent));
          this.isContent = value;
        }
      }

      /// <summary>Опции атрибута</summary>
      public AttributeOptions Options
      {
        get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      public int MasterAttributeID
      {
        get => this.masterAttributeID;
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
        get => this.sourceAttributeID;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (SourceAttributeID));
          this.sourceAttributeID = value;
        }
      }

      /// <summary>Тип данных атрибута (строковый, числовой, т.д.)</summary>
      public FieldTypes FieldType
      {
        get => this.fieldType;
        internal set
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
        get => this.realFieldType;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RealFieldType));
          this.realFieldType = value;
        }
      }

      /// <summary>
      /// Правило проверки (Intermech.Consts.ObjectLinkConstraint или String.Empty)
      /// </summary>
      public string ValidationRule
      {
        get => this.validationRule;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ValidationRule));
          this.validationRule = value;
        }
      }

      /// <summary>Маска атрибута.</summary>
      public string Mask
      {
        get => this.mask;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Mask));
          this.mask = value;
        }
      }

      /// <summary>Строковое представление значения по умолчанию</summary>
      public string DefaultValue
      {
        get => this.defaultValue;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (DefaultValue));
          this.defaultValue = value;
        }
      }

      /// <summary>Можно ли отображать атрибут</summary>
      public bool IsGridable
      {
        get => this.FieldType != FieldTypes.ftPassword && this.Computed != ComputeValueModes.IndexValue;
      }

      /// <summary>
      /// Ссылочный атрибут не разрешает удалять объекты, на которые ссылаются значения этого атрибута
      /// </summary>
      public bool DisableDeleteLinkedObjects => this.ValidationRule == "Value";

      /// <summary>
      /// Список допустимых значений для атрибута, назначенного типу объекта/связи
      /// (когда в "ядре" появится поддержка допустимых значений для атрибутов для типов объектов/связей, будет возвращаться эта информация)
      /// </summary>
      public List<object> PossibleValues
      {
        get
        {
          if (this.AttributeID == 0)
            return new List<object>();
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeID);
          return attributeType == null ? new List<object>() : attributeType.PossibleValues;
        }
      }

      /// <summary>
      /// Список описаний допустимых значений для атрибута, назначенного типу объекта/связи
      /// (когда в "ядре" появится поддержка описаний допустимых значений для атрибутов для типов объектов/связей, будет возвращаться эта информация)
      /// </summary>
      public List<object> PossibleValuesDescriptions
      {
        get
        {
          if (this.AttributeID == 0)
            return new List<object>();
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeID);
          return attributeType == null ? new List<object>() : attributeType.PossibleValuesDescriptions;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSAttribute4 imsAttribute4) ? base.Equals(obj) : this.AttributeID == imsAttribute4.AttributeID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
      /// <returns>32-битный хэш-код экземпляра объекта</returns>
      public override int GetHashCode() => this.AttributeID.GetHashCode();

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return $"[{this.AttributeID}] {MetaDataHelper.GetAttributeTypeName(this.AttributeID)}";
      }

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.AttributeID = 0;
        this.Computed = ComputeValueModes.NotComputableValue;
        this.Required = RequiredModes.Auto;
        this.OptimizationMode = OptimizationModes.NotFound;
        this.IsContent = false;
        this.Options = AttributeOptions.None;
        this.MasterAttributeID = 0;
        this.SourceAttributeID = 0;
        this.FieldType = FieldTypes.ftUnknown;
        this.RealFieldType = FieldTypes.ftUnknown;
        this.ValidationRule = string.Empty;
        this.Mask = string.Empty;
        this.DefaultValue = string.Empty;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSAttribute4 imsAttribute4))
          return;
        this.AttributeID = imsAttribute4.AttributeID;
        this.Computed = imsAttribute4.Computed;
        this.Required = imsAttribute4.Required;
        this.OptimizationMode = imsAttribute4.OptimizationMode;
        this.IsContent = imsAttribute4.IsContent;
        this.Options = imsAttribute4.Options;
        this.MasterAttributeID = imsAttribute4.MasterAttributeID;
        this.SourceAttributeID = imsAttribute4.SourceAttributeID;
        this.FieldType = imsAttribute4.FieldType;
        this.RealFieldType = imsAttribute4.RealFieldType;
        this.ValidationRule = imsAttribute4.ValidationRule;
        this.Mask = imsAttribute4.Mask;
        this.DefaultValue = imsAttribute4.DefaultValue;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public virtual int CompareTo(object obj) => this.CompareTo(obj as IMSAttribute4);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttribute4 other)
      {
        return other == null ? 1 : this.AttributeID.CompareTo(other.AttributeID);
      }

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.AttributeID = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        this.Computed = (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]);
        this.OptimizationMode = (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]);
        this.Required = (RequiredModes) Convert.ToInt32(row["F_REQUIRED"]);
        this.IsContent = Convert.ToInt32(row["F_CONTENT"]) == 1;
        this.Options = (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]);
        this.MasterAttributeID = Convert.ToInt32(row["F_MASTER_ID"]);
        this.SourceAttributeID = Convert.ToInt32(row["F_SOURCE_ID"]);
        this.ValidationRule = Convert.ToString(row["F_VALIDATION_RULE"]);
        this.Mask = Convert.ToString(row["F_MASK"]);
        this.DefaultValue = Convert.ToString(row["F_DEFAULT_VALUE"]);
        IMSAttributeType attrType = MetaDataHelper.AttrTypes.ContainsKey(this.AttributeID) ? MetaDataHelper.AttrTypes[this.AttributeID] : (IMSAttributeType) null;
        this.FieldType = attrType != null ? attrType.FieldType : FieldTypes.ftUnknown;
        this.RealFieldType = attrType != null ? attrType.RealFieldType : FieldTypes.ftUnknown;
      }
    }
}
