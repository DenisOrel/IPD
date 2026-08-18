
// Type: Intermech.Interfaces.WebPortal.ImportPacketOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class ImportPacketOptions
    {
      /// <summary>Запуск задачи импорта незамедлительно</summary>
      public bool StartImmediately { get; set; }

      /// <summary>Режим импорта версий</summary>
      public ImportVersionsModes ImportVersionsMode { get; set; }

      public ImportPacketOptions()
      {
      }

      public ImportPacketOptions(bool startImmediately, ImportVersionsModes importVersionsMode)
      {
        this.StartImmediately = startImmediately;
        this.ImportVersionsMode = importVersionsMode;
      }
    }
}
