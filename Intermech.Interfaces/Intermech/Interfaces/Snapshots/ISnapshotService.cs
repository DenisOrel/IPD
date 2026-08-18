
// Type: Intermech.Interfaces.Snapshots.ISnapshotService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Snapshots
{
    /// <summary>Служба для работы с итерациями</summary>
    public interface ISnapshotService
    {
      /// <summary>Получить текущие настройки службы итераций</summary>
      /// <returns>Настройки службы итераций</returns>
      SnapshotSettings GetSnapshotSettings();

      /// <summary>Записать новые настройки службы итераций</summary>
      /// <param name="userSession">Гуид юзерской сессии</param>
      /// <param name="settings">Настройки</param>
      void SetSnapshotSettings(Guid userSession, SnapshotSettings settings);
    }
}
