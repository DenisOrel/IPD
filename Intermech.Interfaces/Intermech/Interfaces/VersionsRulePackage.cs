
// Type: Intermech.Interfaces.VersionsRulePackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует контейнер, содержащий ссылку на настройки фильтрации составов объектов в базе IPS.
    /// </summary>
    [Serializable]
    public sealed class VersionsRulePackage
    {
      private readonly string ownerId;

      /// <summary>Создает объект.</summary>
      /// <param name="ownerId">Идентификатор владельца настроек фильтрации</param>
      public VersionsRulePackage(string ownerId)
      {
        this.ownerId = !string.IsNullOrEmpty(ownerId) ? ownerId : throw new ArgumentException();
      }

      /// <summary>
      /// Возвращает идентификатор владельца настроек фильтрации.
      /// </summary>
      public string OwnerId => this.ownerId;
    }
}
