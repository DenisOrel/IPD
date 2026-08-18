
// Type: Intermech.Interfaces.Projects.ProjectFiltrationModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Projects
{
    /// <summary>
    /// Способ фильтрации списков объектов в зависимости от их принадлежности к проектам
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_50")]
    [Category("Projects")]
    public enum ProjectFiltrationModes
    {
      /// <summary>Не фильтровать объекты</summary>
      [CustomDescription("Attribute.Interfaces_51")] None,
      /// <summary>
      /// Показывать объекты текущего проекта	и объекты, не включенные в проекты
      /// </summary>
      [CustomDescription("Attribute.Interfaces_52")] CurrentProject,
      /// <summary>
      /// Показывать объекты проектов, в которых участвует текущий пользователь
      /// </summary>
      [CustomDescription("Attribute.Interfaces_53")] UserProjects,
      /// <summary>Показывать только объекты текущего проекта</summary>
      [CustomDescription("OnlyCurrentProject")] OnlyCurrentProject,
    }
}
