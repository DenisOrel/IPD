
// Type: Intermech.Interfaces.Attribute4RelationTypeProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, описывающая свойства атрибута в контексте определенного типа
    /// связи
    /// </summary>
    [Serializable]
    public struct Attribute4RelationTypeProperties(
      int _AttributeID,
      int _RelationType,
      RequiredModes _RequiredMode,
      string _ValidationRule,
      ComputeValueModes _ComputeValueMode,
      string _Formula,
      object _DefaultValue,
      OptimizationModes optimizationMode,
      bool isContent,
      AttributeOptions options,
      string mask,
      int _MasterAttributeID,
      int _SourceAttributeID)
    {
      /// <summary>Идентификатор атрибута (только для чтения)</summary>
      public int AttributeID = _AttributeID;
      /// <summary>Тип связи атрибутов</summary>
      public int RelationType = _RelationType;
      /// <summary>
      /// Свойство описывает допустимость и обязательность атрибута для данного
      /// типа объектов и связей (см. описание RequiredModes)
      /// </summary>
      public RequiredModes RequiredMode = _RequiredMode;
      /// <summary>
      /// Правило валидации правильности вводимых в атрибут значений
      /// </summary>
      public string ValidationRule = _ValidationRule;
      /// <summary>Способ вычисления параметра.</summary>
      public ComputeValueModes ComputeValueMode = _ComputeValueMode;
      /// <summary>
      /// Формула вычисления значения поля. Для ссылок на объекты содержит номер атрибута,
      /// значение которого будет показываться методом AsString атрибута.
      /// </summary>
      public string Formula = _Formula;
      /// <summary>Значение по умолчанию</summary>
      public object DefaultValue = _DefaultValue;
      /// <summary>Тип атрибута (строковый, числовой, т.д.)</summary>
      public FieldTypes FieldType = FieldTypes.ftUnknown;
      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      public OptimizationModes OptimizationMode = optimizationMode;
      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      public bool IsContent = isContent;
      /// <summary>Опции атрибута (см. описание AttributeOptions)</summary>
      public AttributeOptions Options = options;
      /// <summary>Маска ввода значения атрибута</summary>
      public string Mask = mask;
      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      public int MasterAttributeID = _MasterAttributeID;
      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      public int SourceAttributeID = _SourceAttributeID;

      public Attribute4RelationTypeProperties(DataRow row)
        : this(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_RELATION_TYPE"]), (RequiredModes) Convert.ToInt32(row["F_REQUIRED"]), row["F_VALIDATION_RULE"].ToString(), (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]), row["F_FORMULA"].ToString(), row["F_DEFAULT_VALUE"], (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]), Convert.ToInt32(row["F_CONTENT"]) == 1, (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]), row["F_MASK"].ToString(), Convert.ToInt32(row["F_MASTER_ID"]), Convert.ToInt32(row["F_SOURCE_ID"]))
      {
      }

      public override bool Equals(object obj)
      {
        if (obj == null || obj.GetType() != typeof (Attribute4RelationTypeProperties))
          return base.Equals(obj);
        Attribute4RelationTypeProperties relationTypeProperties = (Attribute4RelationTypeProperties) obj;
        bool flag = this.AttributeID == relationTypeProperties.AttributeID && this.RelationType == relationTypeProperties.RelationType && this.RequiredMode == relationTypeProperties.RequiredMode && this.ValidationRule == relationTypeProperties.ValidationRule && this.ComputeValueMode == relationTypeProperties.ComputeValueMode && this.Formula == relationTypeProperties.Formula && this.FieldType == relationTypeProperties.FieldType && this.OptimizationMode == relationTypeProperties.OptimizationMode && this.IsContent == relationTypeProperties.IsContent && this.Options == relationTypeProperties.Options && this.Mask == relationTypeProperties.Mask && this.MasterAttributeID == relationTypeProperties.MasterAttributeID && this.SourceAttributeID == relationTypeProperties.SourceAttributeID;
        if (!flag)
          return flag;
        if (this.DefaultValue == null && relationTypeProperties.DefaultValue != null)
          return relationTypeProperties.DefaultValue.ToString() == string.Empty;
        if (this.DefaultValue != null && relationTypeProperties.DefaultValue == null)
          return this.DefaultValue.ToString() == string.Empty;
        if (this.DefaultValue == null && relationTypeProperties.DefaultValue == null)
          return true;
        return !(this.DefaultValue.GetType() != relationTypeProperties.DefaultValue.GetType()) && this.DefaultValue.Equals(relationTypeProperties.DefaultValue);
      }

      public override int GetHashCode() => base.GetHashCode();
    }
}
