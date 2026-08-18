
// Type: Intermech.Interfaces.Contexts.CurrentEditingContextScope
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;
using System;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Информация, передаваемая в контексте вызова текущего потока, для фиксации контекста редактирования только в данном потоке.
    /// </summary>
    public sealed class CurrentEditingContextScope : IDisposable
    {
      private bool needScope;
      private string previousHeaderValue;
      private bool isDisposed;
      private static readonly string HeaderName = "X-IPS-CurrentEditingContext";
      private static readonly CurrentEditingContextHeaderSerializer HeaderSerializer = new CurrentEditingContextHeaderSerializer();
      [ThreadStatic]
      private static CurrentEditingContextScope.LastDeserializedContext _currentThreadCachedContext;

      /// <summary>Создает объект.</summary>
      /// <param name="editingContext">Контекст редактирования</param>
      public CurrentEditingContextScope(CurrentEditingContext editingContext)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (editingContext.IsDummy)
          return;
        this.needScope = true;
        this.previousHeaderValue = RemotingCallContext.GetData(CurrentEditingContextScope.HeaderName);
        this.ReplaceHeaderValue(CurrentEditingContextScope.HeaderSerializer.Serialize(editingContext));
      }

      /// <summary>Освободить ресурсы</summary>
      public void Dispose()
      {
        if (this.isDisposed)
          return;
        this.isDisposed = true;
        if (!this.needScope)
          return;
        this.ReplaceHeaderValue(this.previousHeaderValue);
        this.previousHeaderValue = (string) null;
        this.needScope = false;
      }

      /// <summary>
      /// Возвращает текущий <see cref="T:Intermech.Interfaces.Contexts.CurrentEditingContext" />, заданный через <see cref="T:Intermech.Remoting.RemotingCallContext" />.
      /// Метод может вернуть null, если фиксация контекста редактирования не выполнялась.
      /// </summary>
      /// <returns><see cref="T:Intermech.Interfaces.Contexts.CurrentEditingContext" /> или null</returns>
      public static CurrentEditingContext TryGet()
      {
        string data = RemotingCallContext.GetData(CurrentEditingContextScope.HeaderName);
        if (string.IsNullOrEmpty(data))
          return (CurrentEditingContext) null;
        if (CurrentEditingContextScope._currentThreadCachedContext != null && CurrentEditingContextScope._currentThreadCachedContext.HeaderValue == data)
          return CurrentEditingContextScope._currentThreadCachedContext.EditingContext;
        CurrentEditingContext editingContext = CurrentEditingContextScope.HeaderSerializer.Deserialize(data);
        CurrentEditingContextScope._currentThreadCachedContext = new CurrentEditingContextScope.LastDeserializedContext(data, editingContext);
        return editingContext;
      }

      private void ReplaceHeaderValue(string newHeaderValue)
      {
        if (newHeaderValue != null)
          RemotingCallContext.SetData(CurrentEditingContextScope.HeaderName, newHeaderValue);
        else
          RemotingCallContext.FreeNamedDataSlot(CurrentEditingContextScope.HeaderName);
        if (CurrentEditingContextScope._currentThreadCachedContext == null)
          return;
        CurrentEditingContextScope._currentThreadCachedContext = (CurrentEditingContextScope.LastDeserializedContext) null;
      }

      private sealed class LastDeserializedContext
      {
        public LastDeserializedContext(string headerValue, CurrentEditingContext editingContext)
        {
          this.HeaderValue = headerValue;
          this.EditingContext = editingContext;
        }

        public string HeaderValue { get; }

        public CurrentEditingContext EditingContext { get; }
      }
    }
}
