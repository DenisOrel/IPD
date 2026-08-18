// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Base.ClearOldProcessSettings
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using System.ComponentModel;


namespace Intermech.Workflow.Base
{
    public class ClearOldProcessSettings
    {
      private bool _enableClearOldProcess;
      private int _clearOldProcessStartTimeValue;
      private static ClearOldProcessSettings _cfg;
      private short _comletedTypeClear;
      private int _timeTypeComboBoxSelectedIndex;

      public static ClearOldProcessSettings Cfg => ClearOldProcessSettings._cfg;

      public static void Init(IUserSession session = null)
      {
        if (ClearOldProcessSettings._cfg != null)
          return;
        ClearOldProcessSettings._cfg = new ClearOldProcessSettings();
        ClearOldProcessSettings._cfg.Load(session);
      }

      /// <summary>
      /// Указывает включена ли поддержка чистики устаревших процессов Workflow
      /// </summary>
      [DefaultValue(false)]
      [DisplayName("Включить удаление устаревших процессов")]
      public bool EnableClearOldProcess
      {
        get => this._enableClearOldProcess;
        set => this._enableClearOldProcess = value;
      }

      /// <summary>
      /// Период времени от дня запуска очистки устаревших данных после которого нужно удалять процессы. Указывается простое число, а его тип задается в TimeTypeComboBoxSelectedIndex
      /// </summary>
      [DisplayName("Время после которого считать процесс устаревшим")]
      [Description("Указывается временной промежуток после которого процессы становятся устаревшими. Указывается число дней/недель/месяцев/лет.")]
      public int ClearOldProcessStartTimeValue
      {
        get => this._clearOldProcessStartTimeValue;
        set => this._clearOldProcessStartTimeValue = value;
      }

      /// <summary>
      /// Какие типы процессов стоит удалять
      /// 0 - Выполненные и Прерванные
      /// 1 - Выполненные
      /// 2 - Прерванные
      /// 3 - Никакие
      /// </summary>
      public short ComletedTypeClear
      {
        get => this._comletedTypeClear;
        set => this._comletedTypeClear = value;
      }

      /// <summary>
      /// Тип данных периода времени ClearOldProcessStartTime
      /// 0 - Дней
      /// 1 - Недель
      /// 2 - Месяцец
      /// 3 - Лет
      /// </summary>
      public int TimeTypeComboBoxSelectedIndex
      {
        get => this._timeTypeComboBoxSelectedIndex;
        set => this._timeTypeComboBoxSelectedIndex = value;
      }

      public void Load(IUserSession session = null)
      {
        if (!(ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations dbConfigurations))
        {
          dbConfigurations = session?.Configurations;
          if (dbConfigurations == null)
            return;
        }
        this.EnableClearOldProcess = dbConfigurations.ReadBool("Workflow", "Global", "EnableClearOldProcess", false, DBConfigMode.GlobalOnly);
        this.ClearOldProcessStartTimeValue = (int) dbConfigurations.ReadInteger("Workflow", "Global", "ClearOldProcessStartTime", 1L, DBConfigMode.GlobalOnly);
        this.ComletedTypeClear = (short) dbConfigurations.ReadInteger("Workflow", "Global", "ComletedTypeClear", 0L, DBConfigMode.GlobalOnly);
        this.TimeTypeComboBoxSelectedIndex = (int) (short) dbConfigurations.ReadInteger("Workflow", "Global", "TimeTypeComboBoxSelectedIndex", 0L, DBConfigMode.GlobalOnly);
      }

      /// <summary>
      /// Для сохранения настроек возьмём сессию, т.к. клиентская обёртка на каждый вызов будет дёргать по киперу, а нам это не надо
      /// </summary>
      /// <param name="session"></param>
      public void Save(IUserSession session)
      {
        IDBConfigurations configurations = session.Configurations;
        configurations.WriteBool("Workflow", "Global", "EnableClearOldProcess", this.EnableClearOldProcess, 0L);
        configurations.WriteInteger("Workflow", "Global", "ClearOldProcessStartTime", (long) this.ClearOldProcessStartTimeValue, 0L);
        configurations.WriteInteger("Workflow", "Global", "ComletedTypeClear", (long) this.ComletedTypeClear, 0L);
        configurations.WriteInteger("Workflow", "Global", "TimeTypeComboBoxSelectedIndex", (long) this.TimeTypeComboBoxSelectedIndex, 0L);
      }

      public void Assign(ClearOldProcessSettings src)
      {
        this.ClearOldProcessStartTimeValue = src.ClearOldProcessStartTimeValue;
        this.EnableClearOldProcess = src.EnableClearOldProcess;
        this.ComletedTypeClear = src.ComletedTypeClear;
        this.TimeTypeComboBoxSelectedIndex = src.TimeTypeComboBoxSelectedIndex;
      }
    }
}
