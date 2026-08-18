
// Type: Intermech.OpenFileRecoveryAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Действие восстановления после ошибки (необработанного исключения и др.),
    /// предлагающее пользователю открыть проблемный файл и исправить его.
    /// Для добавления действия к исключению следует использовать метод-расширения
    /// <see cref="M:Intermech.ExceptionDataExtensions.WithRecoveryActions(System.Exception,Intermech.ErrorRecoveryAction[])" />.
    /// Реализация является immutable.
    /// </summary>
    [Serializable]
    public sealed class OpenFileRecoveryAction : ErrorRecoveryAction
    {
      /// <summary>Создает объект.</summary>
      /// <param name="filePath">Путь к файлу</param>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="filePath" /> содержит null или пустую строку</exception>
      public OpenFileRecoveryAction(string filePath)
      {
        this.FilePath = !string.IsNullOrEmpty(filePath) ? filePath : throw new ArgumentException("The parameter filePath is null or empty.", nameof (filePath));
      }

      /// <summary>Возвращает путь к файлу.</summary>
      public string FilePath { get; }
    }
}
