
// Type: Intermech.Interfaces.Contexts.CurrentEditingContextExtensions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Threading;


namespace Intermech.Interfaces.Contexts
{
    public static class CurrentEditingContextExtensions
    {
      public static CurrentEditingContext WithContextID(
        this CurrentEditingContext editingContext,
        long newContextID)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        return new CurrentEditingContext(newContextID, editingContext.ModificationID, editingContext.ContextMode);
      }

      public static CurrentEditingContext WithModificationID(
        this CurrentEditingContext editingContext,
        long newModificationID)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        return new CurrentEditingContext(editingContext.ContextID, newModificationID, editingContext.ContextMode);
      }

      public static CurrentEditingContext WithContextMode(
        this CurrentEditingContext editingContext,
        EditingContextMode newContextMode)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        return new CurrentEditingContext(editingContext.ContextID, editingContext.ModificationID, newContextMode);
      }

      public static ThreadStart SendToThread(
        this CurrentEditingContext editingContext,
        ThreadStart threadStartAction)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (threadStartAction == null)
          throw new ArgumentNullException(nameof (threadStartAction));
        return editingContext.IsDummy ? threadStartAction : (ThreadStart) (() =>
        {
          using (new CurrentEditingContextScope(editingContext))
            threadStartAction();
        });
      }

      public static ParameterizedThreadStart SendToThread(
        this CurrentEditingContext editingContext,
        ParameterizedThreadStart threadStartAction)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (threadStartAction == null)
          throw new ArgumentNullException(nameof (threadStartAction));
        return editingContext.IsDummy ? threadStartAction : (ParameterizedThreadStart) (arg =>
        {
          using (new CurrentEditingContextScope(editingContext))
            threadStartAction(arg);
        });
      }

      public static Action SendToTask(this CurrentEditingContext editingContext, Action taskAction)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (taskAction == null)
          throw new ArgumentNullException(nameof (taskAction));
        return editingContext.IsDummy ? taskAction : (Action) (() =>
        {
          using (new CurrentEditingContextScope(editingContext))
            taskAction();
        });
      }
    }
}
