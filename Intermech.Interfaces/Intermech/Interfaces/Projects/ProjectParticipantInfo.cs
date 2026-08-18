
// Type: Intermech.Interfaces.Projects.ProjectParticipantInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Projects
{
    /// <summary>Класс для хранения информации об участнике проекта</summary>
    [Serializable]
    public class ProjectParticipantInfo
    {
      /// <summary>Идентификатор участника проекта</summary>
      public long ParticipantID;
      /// <summary>
      /// Если true, то указанный участник является менеджером проекта
      /// </summary>
      public bool ProjectManager;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="participantID">Идентификатор участника проекта</param>
      /// <param name="projectManager">Если true, то указанный участник является менеджером проекта</param>
      public ProjectParticipantInfo(long participantID, bool projectManager)
      {
        this.ParticipantID = participantID;
        this.ProjectManager = projectManager;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        return obj is ProjectParticipantInfo projectParticipantInfo && projectParticipantInfo.ParticipantID == this.ParticipantID;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => this.ParticipantID.GetHashCode();
    }
}
