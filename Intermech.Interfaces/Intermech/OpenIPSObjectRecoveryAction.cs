
// Type: Intermech.OpenIPSObjectRecoveryAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Действие восстановления после ошибки (необработанного исключения и др.),
    /// предлагающее пользователю открыть проблемный объект IPS и исправить его.
    /// Для добавления действия к исключению следует использовать метод-расширения
    /// <see cref="M:Intermech.ExceptionDataExtensions.WithRecoveryActions(System.Exception,Intermech.ErrorRecoveryAction[])" />.
    /// Реализация является immutable.
    /// </summary>
    [Serializable]
    public sealed class OpenIPSObjectRecoveryAction : ErrorRecoveryAction
    {
      /// <summary>Создает объект.</summary>
      /// <param name="objectId">Идентификатор версии объекта IPS</param>
      public OpenIPSObjectRecoveryAction(long objectId) => this.ObjectId = objectId;

      /// <summary>Возвращает идентификатор версии объекта IPS.</summary>
      public long ObjectId { get; }
    }
}
