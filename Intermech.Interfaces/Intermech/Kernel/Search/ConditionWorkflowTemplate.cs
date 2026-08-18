
// Type: Intermech.Kernel.Search.ConditionWorkflowTemplate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс для указания условия поиска объектов по их входимости в элементы процесса по указанному в условии шаблону
    /// </summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<ConditionWorkflowTemplate>))]
    [Serializable]
    public class ConditionWorkflowTemplate : ICloneable
    {
      /// <summary>Идентификатор шаблона процесса</summary>
      public long TemplateObjectID { get; set; }

      /// <summary>Ид. типа действий, входимость в которые нужно искать</summary>
      public int ActivityTypeID { get; set; }

      /// <summary>
      /// Идентификаторы действий из указанного шаблона, входимость в которые нужно искать
      /// </summary>
      public long[] ActivitiesID { get; set; }

      /// <summary>Дополнительные условия по атрибутам действий</summary>
      public ConditionStructure[] Conditions { get; set; }

      /// <summary>
      /// Нужно ли производить поиск в процессах по всем версиям шаблона (если не заданы идентификаторы конкретных действий)
      /// </summary>
      public bool AllVersions { get; set; }

      public ConditionWorkflowTemplate()
      {
      }

      public ConditionWorkflowTemplate(
        long templateObjectID,
        int activityTypeID,
        ConditionStructure[] conditions,
        bool allVersions)
      {
        this.TemplateObjectID = templateObjectID;
        this.ActivityTypeID = activityTypeID;
        this.Conditions = conditions;
        this.ActivitiesID = (long[]) null;
        this.AllVersions = allVersions;
      }

      public ConditionWorkflowTemplate(
        int activityTypeID,
        long[] activitiesID,
        ConditionStructure[] conditions)
      {
        this.TemplateObjectID = 0L;
        this.AllVersions = false;
        this.ActivityTypeID = activityTypeID;
        this.Conditions = conditions;
        this.ActivitiesID = activitiesID;
      }

      public object Clone()
      {
        return (object) new ConditionWorkflowTemplate()
        {
          TemplateObjectID = this.TemplateObjectID,
          ActivityTypeID = this.ActivityTypeID,
          Conditions = (this.Conditions != null ? (ConditionStructure[]) this.Conditions.Clone() : this.Conditions),
          ActivitiesID = (this.ActivitiesID != null ? (long[]) this.ActivitiesID.Clone() : this.ActivitiesID),
          AllVersions = this.AllVersions
        };
      }
    }
}
