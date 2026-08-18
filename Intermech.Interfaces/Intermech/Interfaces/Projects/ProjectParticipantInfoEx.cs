
// Type: Intermech.Interfaces.Projects.ProjectParticipantInfoEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Projects
{
    /// <summary>
    /// Класс для хранения информации об участнике проекта (включая некоторые расширенные свойства пользователя)
    /// </summary>
    [Serializable]
    public class ProjectParticipantInfoEx : ProjectParticipantInfo
    {
      /// <summary>Заголовок</summary>
      public string Caption;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="participantID">Идентификатор участника проекта</param>
      /// <param name="projectManager">Если true, то указанный участник является менеджером проекта</param>
      /// <param name="caption">Заголовок</param>
      public ProjectParticipantInfoEx(long participantID, bool projectManager, string caption)
        : base(participantID, projectManager)
      {
        this.Caption = caption;
      }
    }
}
