
// Type: Intermech.ErrorRecoveryInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    /// <summary>
    /// Контейнер для действий восстановления после ошибки (необработанного исключения и др.)
    /// Экземпляры этого типа часто цепляются к объектам исключений через свойство <see cref="P:System.Exception.Data" /> с помощью методов-расширений
    /// <see cref="M:Intermech.ExceptionDataExtensions.WithRecoveryActions(System.Exception,Intermech.ErrorRecoveryAction[])" /> и
    /// <see cref="M:Intermech.ExceptionDataExtensions.WithRecoveryInfo(System.Exception,Intermech.ErrorRecoveryInfo)" />.
    /// Реализация является immutable.
    /// </summary>
    [Serializable]
    public sealed class ErrorRecoveryInfo
    {
      /// <summary>Создает объект.</summary>
      /// <param name="actions">Массив действий</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="actions" /> содержит null</exception>
      public ErrorRecoveryInfo(params ErrorRecoveryAction[] actions)
      {
        this.Actions = actions != null ? (IReadOnlyCollection<ErrorRecoveryAction>) actions : throw new ArgumentNullException(nameof (actions));
      }

      /// <summary>
      /// Возвращает коллекцию действий по восстановлению после ошибки  (необработанного исключения и др.)
      /// </summary>
      public IReadOnlyCollection<ErrorRecoveryAction> Actions { get; }
    }
}
