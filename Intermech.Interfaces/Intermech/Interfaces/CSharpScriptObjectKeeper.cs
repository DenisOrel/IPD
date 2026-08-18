
// Type: Intermech.Interfaces.CSharpScriptObjectKeeper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Scripting.CSharp;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Объект-хранитель, содержащий проинициализированный и готовый к использованию объект сценария C#.
    /// Такие объекты-хранители применяются в тех случаях, когда обращение к сценарию C# не может быть
    /// сведено к единственному вызову метода Execute.
    /// </summary>
    /// <remarks>
    /// Реализация не является thread safe. Объекты-хранители и содержащиеся в них объекты сценариев
    /// привязаны к потоку выполнения (thread), в котором они были созданы, и могут использоваться
    /// только из этого потока.
    /// </remarks>
    public sealed class CSharpScriptObjectKeeper : IDisposable
    {
      private IScriptObjectKeeper internalKeeper;

      /// <summary>Создает объект.</summary>
      /// <param name="internalKeeper">Объект-хранитель, созданный текущим исполнителем сценариев</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="internalKeeper" /> содержит null</exception>
      public CSharpScriptObjectKeeper(IScriptObjectKeeper internalKeeper)
      {
        this.internalKeeper = internalKeeper != null ? internalKeeper : throw new ArgumentNullException(nameof (internalKeeper));
      }

      /// <summary>
      /// Освобождает ресурсы сценария C# и очищает объект сценария.
      /// </summary>
      public void Dispose() => this.internalKeeper.Dispose();

      /// <summary>Возвращает объект сценария C#.</summary>
      /// <exception cref="T:System.InvalidOperationException">Обращения из других потоков управления запрещены</exception>
      /// <exception cref="T:System.ObjectDisposedException">Ресурсы объекта-хранителя и сценария уже были освобождены</exception>
      public object ScriptObject
      {
        [DebuggerStepThrough] get => this.internalKeeper.ScriptObject;
      }
    }
}
