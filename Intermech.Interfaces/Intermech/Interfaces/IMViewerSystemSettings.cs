
// Type: Intermech.Interfaces.IMViewerSystemSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Глобальные настройки интеграции с IMViewer.</summary>
    [Serializable]
    public class IMViewerSystemSettings : FreezableObject, ICloneable
    {
      private bool enableIntegration;

      /// <summary>Включает и выключает интеграцию с IMViewer.</summary>
      public bool EnableIntegration
      {
        [DebuggerStepThrough] get => this.enableIntegration;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (EnableIntegration));
          this.enableIntegration = value;
        }
      }

      public IMViewerSystemSettings Clone()
      {
        IMViewerSystemSettings viewerSystemSettings = new IMViewerSystemSettings();
        viewerSystemSettings.Assign(this);
        return viewerSystemSettings;
      }

      object ICloneable.Clone() => (object) this.Clone();

      public void Assign(IMViewerSystemSettings other)
      {
        if (other == null)
          throw new ArgumentNullException(nameof (other));
        this.DoAssign(other);
      }

      protected virtual void DoAssign(IMViewerSystemSettings other)
      {
        this.EnableIntegration = other.EnableIntegration;
      }
    }
}
