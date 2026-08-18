
// Type: Intermech.ErrorRecoveryAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Базовый класс для всех действий восстановления после ошибки (необработанного исключения и др.)
    /// Наследники должны поддерживать сериализацию, так как экземпляры часто цепляются к
    /// объектам исключений через свойство <see cref="P:System.Exception.Data" /> с помощью метода-расширения
    /// <see cref="M:Intermech.ExceptionDataExtensions.WithRecoveryActions(System.Exception,Intermech.ErrorRecoveryAction[])" />.
    /// Реализация должна быть immutable.
    /// </summary>
    [Serializable]
    public abstract class ErrorRecoveryAction
    {
    }
}
