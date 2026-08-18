using System;
using System.ComponentModel;
using System.Reflection;


namespace Intermech.Printing
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [Browsable(true)]
    [RefreshProperties(RefreshProperties.None)]
    public sealed class CPrinterStatus
    {
      private readonly PrinterStatus _status;

      public CPrinterStatus(int status) => this._status = (PrinterStatus) status;

      public override string ToString()
      {
        Type nullableType = this._status.GetType();
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
        FieldInfo field = nullableType.GetField(this._status.ToString());
        if (field != (FieldInfo) null)
        {
          DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
          if (customAttributes.Length != 0)
            return customAttributes[0].Description;
        }
        return Enum.Format(typeof (PrinterStatus), (object) this._status, "g");
      }

      public bool Busy
      {
        get => (this._status & CPrinterStatus.PrinterStatus.Busy) == CPrinterStatus.PrinterStatus.Busy;
      }

      public bool DoorOpen
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.DoorOpen) == CPrinterStatus.PrinterStatus.DoorOpen;
        }
      }

      public bool Error
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Error) == CPrinterStatus.PrinterStatus.Error;
        }
      }

      public bool Initializing
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Initializing) == CPrinterStatus.PrinterStatus.Initializing;
        }
      }

      public bool InputOutputActive
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.IOActive) == CPrinterStatus.PrinterStatus.IOActive;
        }
      }

      public bool ManualFeed
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.ManualFeed) == CPrinterStatus.PrinterStatus.ManualFeed;
        }
      }

      public bool NoToner
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.NoToner) == CPrinterStatus.PrinterStatus.NoToner;
        }
      }

      public bool NotAvailable
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.NotAvailable) == CPrinterStatus.PrinterStatus.NotAvailable;
        }
      }

      public bool Offline
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Offline) == CPrinterStatus.PrinterStatus.Offline;
        }
      }

      public bool OutOfMemory
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.OutOfMemory) == CPrinterStatus.PrinterStatus.OutOfMemory;
        }
      }

      public bool OutPutBinFull
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.OutputBinFull) == CPrinterStatus.PrinterStatus.OutputBinFull;
        }
      }

      public bool PagePunt
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PagePunt) == CPrinterStatus.PrinterStatus.PagePunt;
        }
      }

      public bool PaperJam
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PaperJam) == CPrinterStatus.PrinterStatus.PaperJam;
        }
      }

      public bool PaperOut
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PaperOut) == CPrinterStatus.PrinterStatus.PaperOut;
        }
      }

      public bool PaperProblem
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PaperProblem) == CPrinterStatus.PrinterStatus.PaperProblem;
        }
      }

      public bool Paused
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Paused) == CPrinterStatus.PrinterStatus.Paused;
        }
      }

      public bool PendingDeletion
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PendingDeletion) == CPrinterStatus.PrinterStatus.PendingDeletion;
        }
      }

      public bool PowerSave
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.PowerSave) == CPrinterStatus.PrinterStatus.PowerSave;
        }
      }

      public bool Printing
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Printing) == CPrinterStatus.PrinterStatus.Printing;
        }
      }

      public bool Processing
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Processing) == CPrinterStatus.PrinterStatus.Processing;
        }
      }

      public bool Ready
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Ready) == CPrinterStatus.PrinterStatus.Ready;
        }
      }

      public bool ServerUnknown
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.ServerUnknown) == CPrinterStatus.PrinterStatus.ServerUnknown;
        }
      }

      public bool TonerLow
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.TonerLow) == CPrinterStatus.PrinterStatus.TonerLow;
        }
      }

      public bool UserInterventionRequired
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.UserIntervention) == CPrinterStatus.PrinterStatus.UserIntervention;
        }
      }

      public bool Waiting
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.Waiting) == CPrinterStatus.PrinterStatus.Waiting;
        }
      }

      public bool WarmingUp
      {
        get
        {
          return (this._status & CPrinterStatus.PrinterStatus.WarmingUp) == CPrinterStatus.PrinterStatus.WarmingUp;
        }
      }

      [Flags]
      private enum PrinterStatus
      {
        [Description("Готов")] Ready = 0,
        [Description("Занят")] Busy = 512, // 0x00000200
        [Description("Крышка открыта")] DoorOpen = 4194304, // 0x00400000
        [Description("Ошибка")] Error = 2,
        [Description("Инициализация")] Initializing = 32768, // 0x00008000
        [Description("Передача данных")] IOActive = 256, // 0x00000100
        [Description("Ручная подача")] ManualFeed = 32, // 0x00000020
        [Description("Недоступен")] NotAvailable = 4096, // 0x00001000
        [Description("Нет тонера")] NoToner = 262144, // 0x00040000
        [Description("Не готов")] Offline = 128, // 0x00000080
        [Description("Недостаточно памяти")] OutOfMemory = 2097152, // 0x00200000
        [Description("Лоток переполнен")] OutputBinFull = 2048, // 0x00000800
        [Description("Готов")] PagePunt = 524288, // 0x00080000
        [Description("Застряла бумага")] PaperJam = 8,
        [Description("Нет бумаги")] PaperOut = 16, // 0x00000010
        [Description("Проблема с бумагой")] PaperProblem = 64, // 0x00000040
        [Description("Пауза")] Paused = 1,
        [Description("Удаление задания")] PendingDeletion = 4,
        [Description("Энергосберегающий режим")] PowerSave = 16777216, // 0x01000000
        [Description("Печатает")] Printing = 1024, // 0x00000400
        [Description("Обработка")] Processing = 16384, // 0x00004000
        [Description("Состояние неизвестно")] ServerUnknown = 8388608, // 0x00800000
        [Description("Тонер заканчивается")] TonerLow = 131072, // 0x00020000
        [Description("Вмешательство пользователя")] UserIntervention = 1048576, // 0x00100000
        [Description("Ожидание")] Waiting = 8192, // 0x00002000
        [Description("Разогревается")] WarmingUp = 65536, // 0x00010000
      }
    }
}
