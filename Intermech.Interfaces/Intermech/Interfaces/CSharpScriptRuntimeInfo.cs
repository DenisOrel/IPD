
// Type: Intermech.Interfaces.CSharpScriptRuntimeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Информация о среде выполнения сценариев.</summary>
    [Serializable]
    public class CSharpScriptRuntimeInfo
    {
      private ICollection<string> autoReferencesAssemblies;
      private ICollection<string> searchPathList;

      /// <summary>Создает объект</summary>
      public CSharpScriptRuntimeInfo()
      {
        this.autoReferencesAssemblies = (ICollection<string>) new string[0];
        this.searchPathList = (ICollection<string>) new string[0];
      }

      /// <summary>
      /// Список имен файлов для автоматически подключаемых сборок.
      /// </summary>
      public ICollection<string> AutoReferencesAssemblies
      {
        [DebuggerStepThrough] get => this.autoReferencesAssemblies;
        set
        {
          this.autoReferencesAssemblies = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }

      /// <summary>Список путей поиска используемых сборок.</summary>
      public ICollection<string> SearchPathList
      {
        [DebuggerStepThrough] get => this.searchPathList;
        set
        {
          this.searchPathList = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }
    }
}
