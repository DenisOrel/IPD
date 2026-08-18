
// Type: Intermech.Interfaces.WebPortal.TaskType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Тип задачи</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_498")]
    [Category("Misc")]
    public enum TaskType
    {
      /// <summary>Импорт обновлений</summary>
      [CustomDescription("Attribute.Interfaces_499"), PublishTaskType(false)] ImportUpdates,
      /// <summary>Публикация</summary>
      [CustomDescription("Attribute.Interfaces_500"), PublishTaskType(true)] Publish,
      /// <summary>Запрос на импорт опубликованных объектов</summary>
      [CustomDescription("Attribute.Interfaces_501"), PublishTaskType(false)] ImportObjects,
      /// <summary>Публикация информации по удаленному процессу</summary>
      [CustomDescription("Attribute.Interfaces_502"), PublishTaskType(true)] ProcessPublish,
      /// <summary>Публикация информации по удаленному процессу</summary>
      [CustomDescription("Attribute.Interfaces_503"), PublishTaskType(true)] ProjectPublish,
    }
}
