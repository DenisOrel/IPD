
// Type: Intermech.Interfaces.IDBRelationsApplicability
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс объекта, описывающего контекст связи типа RelationType между объектами
    /// типа ObjectType (дочерний) и InObjectType (родительский)
    /// </summary>
    public interface IDBRelationsApplicability
    {
      /// <summary>Ид. контекста связи</summary>
      int ApplicabilityID { get; }

      /// <summary>Тип дочернего объекта</summary>
      int ObjectType { get; }

      /// <summary>Тип родительского объекта</summary>
      int InObjectType { get; }

      /// <summary>Тип связи</summary>
      int RelationType { get; }

      /// <summary>
      /// Если true, то при создании версии родительского объекта нужно создавать версии
      /// дочерних объектов
      /// </summary>
      bool CloneChildRelations { get; set; }

      /// <summary>
      /// Максимально допустимое количество таких связей. Если = Int32.MaximumValue, то бесконечное.
      /// </summary>
      int MaximumLinks { get; set; }

      /// <summary>Свойство обязательности связи</summary>
      ApplicabilityModes ApplicabilityMode { get; set; }

      /// <summary>
      /// Способ обрабатки удаление объектов, связанных этой связью.
      /// </summary>
      RelationConstraintModes RelationConstraintMode { get; set; }

      /// <summary>Удалить контекст связи</summary>
      /// <returns></returns>
      int Delete();

      RelationsApplicabilityProperties PropertiesStructure { get; set; }

      /// <summary>
      /// Влияет ли данная связь на содержимое родительского объекта. Если да, то при модификации
      /// атрибутов этой связи меняется дата модификации родительского объекта. Также при создании
      /// объекта по прототипу родительского объекта копируются все связи, у которых IsContent == true
      /// </summary>
      bool IsContent { get; set; }

      /// <summary>Опции связи</summary>
      ApplicabilityOptions Options { get; set; }

      /// <summary>Извлекать ли на диск файлы по таким связям</summary>
      bool CheckoutFiles { get; set; }

      /// <summary>Возвращает количество таких связей в базе данных</summary>
      int RelationsCount { get; }
    }
}
