
// Type: Intermech.Interfaces.Briefcase.BriefcaseImportProgress
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Класс для хранения текущей информации по процессу импорта
    /// </summary>
    [Serializable]
    public class BriefcaseImportProgress
    {
      private OperationType _operation;
      public int Percent;
      public Exception ErrorException;
      public List<CheckMetadataLogItem> CheckErrors;
      public OperationType OnErrorOperation;

      public OperationType Operation
      {
        get => this._operation;
        set
        {
          if (value == OperationType.Error)
            this.OnErrorOperation = this._operation;
          this._operation = value;
        }
      }

      public BriefcaseImportProgress(OperationType operation)
      {
        this.Operation = operation;
        this.Percent = 0;
        this.CheckErrors = new List<CheckMetadataLogItem>();
      }
    }
}
