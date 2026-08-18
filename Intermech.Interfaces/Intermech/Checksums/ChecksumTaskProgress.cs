
// Type: Intermech.Checksums.ChecksumTaskProgress
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Checksums
{
    /// <summary>
    /// Класс для хранения текущей информации по процессу вычисления контрольной суммы
    /// </summary>
    [Serializable]
    public class ChecksumTaskProgress
    {
      private ChecksumOperationType _operation;
      public int Percent;
      public Exception ErrorException;
      /// <summary>операция, на которой произошла ошибка</summary>
      public ChecksumOperationType OnErrorOperation;

      public ChecksumOperationType Operation
      {
        get => this._operation;
        set
        {
          if (value == ChecksumOperationType.Error)
            this.OnErrorOperation = this._operation;
          this._operation = value;
        }
      }

      public ChecksumTaskProgress(ChecksumOperationType operation)
      {
        this.Operation = operation;
        this.Percent = 0;
      }
    }
}
