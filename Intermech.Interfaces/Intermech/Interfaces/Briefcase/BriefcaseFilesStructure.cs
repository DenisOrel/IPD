
// Type: Intermech.Interfaces.Briefcase.BriefcaseFilesStructure
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Класс для хранения файловой архитектуры портфеля</summary>
    [Serializable]
    public class BriefcaseFilesStructure
    {
      /// <summary>Файлы</summary>
      public ArrayList Files;
      /// <summary>Папки</summary>
      public ArrayList Folders;

      public BriefcaseFilesStructure(ArrayList files, ArrayList folders)
      {
        this.Files = files;
        this.Folders = folders;
      }
    }
}
