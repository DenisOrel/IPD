
// Type: Intermech.UI.UICommandInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.UI
{
    public sealed class UICommandInfo : IDisposable
    {
      private string displayName;
      private bool isDisposed;
      private List<Action> disposeActions;
      private Dictionary<object, object> tags;

      /// <summary>Создает объект.</summary>
      /// <param name="displayName">Имя UI-команды</param>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="displayName" /> не должен быть пуст или равен null</exception>
      public UICommandInfo(string displayName)
      {
        this.displayName = !string.IsNullOrEmpty(displayName) ? displayName : throw new ArgumentException("Не задано имя UI-команды.", nameof (displayName));
      }

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        if (this.disposeActions != null && this.disposeActions.Count != 0)
          this.ExecuteDisposeActions();
        if (this.tags != null && this.tags.Count != 0)
          this.DisposeTags();
        this.isDisposed = true;
      }

      private void ExecuteDisposeActions()
      {
        foreach (Action disposeAction in this.disposeActions)
          SilentActionInvoker.Default.Invoke(disposeAction, "UICommandInfo.ExecuteDisposeActions()");
      }

      private void DisposeTags()
      {
        foreach (KeyValuePair<object, object> tag in this.tags)
          DisposeUtils.SafelyDispose(tag.Value as IDisposable);
      }

      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.isDisposed;
      }

      private void RequireNotDisposed()
      {
        if (this.IsDisposed)
          throw new ObjectDisposedException(this.GetType().FullName);
      }

      public void RegisterDisposeAction(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.RequireNotDisposed();
        if (this.disposeActions == null)
          this.disposeActions = new List<Action>();
        this.disposeActions.Add(action);
      }

      public void UnregisterDisposeAction(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.RequireNotDisposed();
        if (this.disposeActions == null)
          return;
        this.disposeActions.Remove(action);
      }

      /// <summary>Возвращае имя UI-команды.</summary>
      public string DisplayName
      {
        [DebuggerStepThrough] get => this.displayName;
      }

      /// <summary>Возвращает коллекцию тегов UI-команды.</summary>
      public Dictionary<object, object> Tags
      {
        [DebuggerStepThrough] get
        {
          if (this.tags == null)
            this.tags = new Dictionary<object, object>();
          return this.tags;
        }
      }
    }
}
