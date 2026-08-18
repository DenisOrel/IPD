
// Type: Intermech.Interfaces.WebPortal.CompositionApplicabilities
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Хранилище доступных типов связей для формирования состава публикуемых объектов
    /// </summary>
    [Serializable]
    public class CompositionApplicabilities
    {
      /// <summary>
      /// Список наименований или гуидов типов связей по которым ищется состав
      /// </summary>
      public string[] RelationTypes;
      /// <summary>
      /// Список наименований или гуидов типов связей которые разрешены всегда, независимо от флага recursive
      /// </summary>
      public string[] RecursivityRelationTypes;
      /// <summary>
      /// Список глобальных типов (наименования или гуиды), объекты которых должны отфильтровыватся
      /// </summary>
      public string[] ObjectTypesFilter;
      /// <summary>Флаг того, что в массивах лежат гуиды</summary>
      public bool IsGuids;

      public CompositionApplicabilities()
        : this((string[]) null, (string[]) null, (string[]) null)
      {
      }

      public CompositionApplicabilities(string[] relationTypes, string[] recursivityRelationTypes)
        : this(relationTypes, recursivityRelationTypes, (string[]) null)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="relationTypes">Типы связей для состава и способ запроса по ним</param>
      /// <param name="recursivityRelationTypes">Список наименований или гуидов типов связей по которым ищется полный состав</param>
      /// <param name="objectTypesFilter">Список глобальных типов (наименования или гуиды), объекты которых должны отфильтровыватся</param>
      public CompositionApplicabilities(
        string[] relationTypes,
        string[] recursivityRelationTypes,
        string[] objectTypesFilter)
      {
        this.RelationTypes = relationTypes;
        this.RecursivityRelationTypes = recursivityRelationTypes;
        this.ObjectTypesFilter = objectTypesFilter;
        if (this.RelationTypes != null && this.RelationTypes.Length != 0)
          this.IsGuids = GuidHelper.IsGuid(this.RelationTypes[0]);
        else if (this.RecursivityRelationTypes != null && this.RecursivityRelationTypes.Length != 0)
          this.IsGuids = GuidHelper.IsGuid(this.RecursivityRelationTypes[0]);
        else if (this.ObjectTypesFilter != null && this.ObjectTypesFilter.Length != 0)
          this.IsGuids = GuidHelper.IsGuid(this.ObjectTypesFilter[0]);
        else
          this.IsGuids = false;
      }
    }
}
