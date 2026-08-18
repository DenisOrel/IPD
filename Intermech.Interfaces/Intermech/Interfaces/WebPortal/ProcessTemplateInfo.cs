
// Type: Intermech.Interfaces.WebPortal.ProcessTemplateInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Описывает удаленный шаблон процесса</summary>
    [Serializable]
    public class ProcessTemplateInfo
    {
      /// <summary>Глобальный идентификатор версии</summary>
      public Guid Guid;
      /// <summary>Наименование</summary>
      public string Name;

      public ProcessTemplateInfo()
      {
      }

      public ProcessTemplateInfo(Guid guid, string name)
      {
        this.Guid = guid;
        this.Name = name;
      }

      public override string ToString() => this.Name;
    }
}
