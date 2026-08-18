
// Type: Intermech.Interfaces.Briefcase.BriefcaseExportProgress
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Класс для хранения текущей информации по процессу экспорта
    /// </summary>
    [Serializable]
    public class BriefcaseExportProgress
    {
      private ExportOperationType _operation;
      public int Percent;
      public Exception ErrorException;
      /// <summary>операция, на которой произошла ошибка</summary>
      public ExportOperationType OnErrorOperation;

      public ExportOperationType Operation
      {
        get => this._operation;
        set
        {
          if (value == ExportOperationType.Error)
            this.OnErrorOperation = this._operation;
          this._operation = value;
        }
      }

      public BriefcaseExportProgress(ExportOperationType operation)
      {
        this.Operation = operation;
        this.Percent = 0;
      }
    }
}
