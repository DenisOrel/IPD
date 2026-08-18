
// Type: Intermech.Interfaces.Snapshots.SnapshotSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Snapshots
{
    /// <summary>
    /// Класс для хранения и передачи настроек серверной службы итераций
    /// </summary>
    [Serializable]
    public class SnapshotSettings
    {
      /// <summary>Максимальное количество итераций на версию объекта</summary>
      public int MaxIterationsPerObject;
      /// <summary>Максимальное время жизни итерации в днях</summary>
      public int IterationLifetime;
      /// <summary>
      /// Уровень продвижения, на котором у версии объекта удаляются итерации
      /// </summary>
      public int TruncateLevel;

      public SnapshotSettings(int maxIterations, int iterationLifetime, int truncLevel)
      {
        this.MaxIterationsPerObject = maxIterations;
        this.IterationLifetime = iterationLifetime;
        this.TruncateLevel = truncLevel;
      }
    }
}
