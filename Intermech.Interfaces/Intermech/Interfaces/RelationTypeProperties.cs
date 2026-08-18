
// Type: Intermech.Interfaces.RelationTypeProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Структура, содержащая свойства типа связей</summary>
    [Serializable]
    public struct RelationTypeProperties
    {
      /// <summary>Ид. типа связи (только для чтения).</summary>
      public int RelationType;
      /// <summary>
      /// Наименование типа связи с точки зрения родительского объекта (например, состоит из...).
      /// </summary>
      public string TypeName;
      /// <summary>
      /// Наименование типа связи с точки зрения дочернего объекта  (например, Входит в...).
      /// В случае горизонтальных связей равно TypeName
      /// </summary>
      public string ReverseName;
      /// <summary>Комментарии</summary>
      public string Note;
      /// <summary>
      /// Нужно ли извлекать на диск файлы объектов, объединённых данной связью.
      /// </summary>
      public bool CheckoutFile;
      /// <summary>
      /// Нужно ли сохранять историю изменения связей в рамках одной версии.
      /// </summary>
      public bool SaveHistory;
      /// <summary>
      /// Уникальное описание типа связи (например, Проектная связь)
      /// </summary>
      public string Description;
      public Guid RelationTypeGuid;
      /// <summary>
      /// Строка символов, определяющих предметные области объектов, объединенных данной связью
      /// </summary>
      public string AreaID;
      /// <summary>
      /// Контроль набора атрибутов
      /// false - допускается добавлять к связям данного типа только разрешенные атрибуты.
      /// true - допускается добавлять любые атрибуты.
      /// </summary>
      public bool AnyAttributes;
      /// <summary>Краткое наименование типа связи (может быть пустым)</summary>
      public string ShortName;
      /// <summary>Опции типа связей</summary>
      public RelationTypeOptions Options;

      public RelationTypeProperties(
        int relationType,
        string typeName,
        string reverseName,
        string note,
        bool checkoutFile,
        bool saveHistory,
        string description,
        Guid relationTypeGuid,
        string areaID,
        bool anyAttributes,
        string shortName,
        RelationTypeOptions options)
      {
        this.RelationType = relationType;
        this.TypeName = typeName;
        this.ReverseName = reverseName;
        this.Note = note;
        this.CheckoutFile = checkoutFile;
        this.SaveHistory = saveHistory;
        this.Description = description;
        this.RelationTypeGuid = relationTypeGuid;
        this.AreaID = areaID;
        this.AnyAttributes = anyAttributes;
        this.ShortName = shortName;
        this.Options = options;
      }

      public RelationTypeProperties(
        int relationType,
        string typeName,
        string reverseName,
        string note,
        bool checkoutFile,
        bool saveHistory,
        string description,
        Guid relationTypeGuid,
        string areaID,
        bool anyAttributes,
        string shortName)
      {
        this.RelationType = relationType;
        this.TypeName = typeName;
        this.ReverseName = reverseName;
        this.Note = note;
        this.CheckoutFile = checkoutFile;
        this.SaveHistory = saveHistory;
        this.Description = description;
        this.RelationTypeGuid = relationTypeGuid;
        this.AreaID = areaID;
        this.AnyAttributes = anyAttributes;
        this.ShortName = shortName;
        this.Options = RelationTypeOptions.None;
      }

      public RelationTypeProperties(DataRow row)
      {
        this.RelationType = Convert.ToInt32(row["F_RELATION_TYPE"]);
        this.TypeName = row["F_TYPE_NAME"].ToString();
        this.ReverseName = row["F_REVERSE_NAME"].ToString();
        this.Note = row["F_NOTE"].ToString();
        this.CheckoutFile = Convert.ToInt32(row["F_CHKOUTFILE"]) == 1;
        this.SaveHistory = Convert.ToInt32(row["F_SAVE_HISTORY"]) == 1;
        this.Description = row["F_DESCRIPTION"].ToString();
        this.RelationTypeGuid = new Guid(row["F_GUID"].ToString());
        this.AreaID = row["F_AREA_ID"].ToString();
        this.AnyAttributes = Convert.ToInt32(row["F_ANY_ATTRIBUTES"]) == 1;
        this.ShortName = row["F_SHORT_NAME"].ToString();
        this.Options = (RelationTypeOptions) Convert.ToInt32(row["F_OPTIONS"]);
      }
    }
}
