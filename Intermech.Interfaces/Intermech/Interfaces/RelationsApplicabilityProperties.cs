
// Type: Intermech.Interfaces.RelationsApplicabilityProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.CompositionTracking;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, описывающая свойства контекста связи типа RelationType между объектами
    /// типа ObjectType (дочерний) и InObjectType (родительский)
    /// </summary>
    [Serializable]
    public struct RelationsApplicabilityProperties(
      int _applicabilityID,
      int _objectType,
      int _inObjectType,
      int _relationType,
      bool _cloneChildRelations,
      int _maximumLinks,
      ApplicabilityModes _applicabilityMode,
      RelationConstraintModes _relationConstraintMode,
      bool _checkoutFiles,
      bool isContent,
      ApplicabilityOptions _options) : IObjectTypeApplicabilityContext
    {
      /// <summary>Ид. контекста связи</summary>
      public int ApplicabilityID = _applicabilityID;
      /// <summary>Тип дочернего объекта</summary>
      public int ObjectType = _objectType;
      /// <summary>Тип родительского объекта</summary>
      public int InObjectType = _inObjectType;
      /// <summary>Тип связи</summary>
      public int RelationType = _relationType;
      /// <summary>
      /// Если true, то при создании версии родительского объекта нужно копировать данные связи у исходной версии объекта или у объекта-прототипа
      /// </summary>
      public bool CloneChildRelations = _cloneChildRelations;
      /// <summary>Нужно ли извлекать файлы объектов по данной связи</summary>
      public bool CheckoutFiles = _checkoutFiles;
      /// <summary>
      /// Максимально допустимое количество таких связей. Если = Int32.MaximumValue, то бесконечное.
      /// </summary>
      public int MaximumLinks = _maximumLinks;
      /// <summary>Свойство обязательности связи</summary>
      public ApplicabilityModes ApplicabilityMode = _applicabilityMode;
      /// <summary>
      /// Способ обрабатки удаление объектов, связанных этой связью.
      /// </summary>
      public RelationConstraintModes RelationConstraintMode = _relationConstraintMode;
      /// <summary>
      /// Влияет ли данная связь на содержимое родительского объекта. Если да, то при модификации
      /// атрибутов этой связи меняется дата модификации родительского объекта.
      /// </summary>
      public bool IsContent = isContent;
      /// <summary>Опции связи</summary>
      public ApplicabilityOptions Options = _options;

      int IObjectTypeApplicabilityContext.ObjectTypeId
      {
        get => this.ObjectType;
        set => this.ObjectType = value;
      }

      int IObjectTypeApplicabilityContext.InObjectTypeId
      {
        get => this.InObjectType;
        set => this.InObjectType = value;
      }

      int IObjectTypeApplicabilityContext.RelationTypeId
      {
        get => this.RelationType;
        set => this.RelationType = value;
      }
    }
}
