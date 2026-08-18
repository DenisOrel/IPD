
// Type: Intermech.Tools.Integrators.IntegratorSettingsCacheManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Memoization;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators
{
    /// <summary>
    /// Сервис для управления кэшированием настроек интеграторов.
    /// </summary>
    public sealed class IntegratorSettingsCacheManager
    {
      private static readonly TimeSpan serverCheckPeriod = new TimeSpan(18000000000L);
      private static readonly SimpleStateMonitor resetMonitor = new SimpleStateMonitor();

      /// <summary>
      /// Интервал для периодического обновления локального кэша настроек интеграторов.
      /// </summary>
      public TimeSpan ServerCheckPeriod
      {
        [DebuggerStepThrough] get => IntegratorSettingsCacheManager.serverCheckPeriod;
      }

      /// <summary>
      /// Возвращает монитор состояния, позволяющий определить необходимость принудительного обновления локального кэша настроек интеграторов.
      /// </summary>
      public IStateMonitor ResetMonitor
      {
        [DebuggerStepThrough] get => (IStateMonitor) IntegratorSettingsCacheManager.resetMonitor;
      }

      /// <summary>Событие сброса локального кэша настроек интеграторов.</summary>
      public event EventHandler AfterResetCache;

      /// <summary>
      /// Сигнализирует о необходимости принудительного обновления локального кэша настроек интеграторов.
      /// </summary>
      public void ResetCache()
      {
        IntegratorSettingsCacheManager.resetMonitor.UpdateState();
        this.RaiseAfterResetCache();
      }

      private void RaiseAfterResetCache()
      {
        EventHandler afterResetCache = this.AfterResetCache;
        if (afterResetCache == null)
          return;
        afterResetCache((object) this, EventArgs.Empty);
      }
    }
}
