
// Type: Intermech.Interfaces.DBPatches.AbstractPatchRunner
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.DBPatches
{
    public abstract class AbstractPatchRunner
    {
      public void Run(params AbstractPatch[] patches)
      {
        if (patches == null)
          throw new ArgumentNullException(nameof (patches));
        this.RunInternal((IEnumerable<AbstractPatch>) patches);
      }

      public void Run(IEnumerable<AbstractPatch> patches)
      {
        if (patches == null)
          throw new ArgumentNullException(nameof (patches));
        this.RunInternal(patches);
      }

      private void RunInternal(IEnumerable<AbstractPatch> patches)
      {
        foreach (AbstractPatch patch in patches)
        {
          if (patch != null)
          {
            try
            {
              patch.Perform();
            }
            catch (Exception ex)
            {
              this.LogPatchException(patch, ex);
            }
          }
        }
      }

      private void LogPatchException(AbstractPatch patch, Exception exception)
      {
        string errorMessage = $"Ошибка применения патча {patch.GetType().Name}: {exception.Message}";
        string errorType = $"Type: {exception.GetType()}";
        string errorStackTrace = $"Stack trace:{Environment.NewLine}{ExceptionServices.GetExtendedStackTrace(exception)}";
        this.LogPatchException(patch, exception, errorMessage, errorType, errorStackTrace);
      }

      protected abstract void LogPatchException(
        AbstractPatch patch,
        Exception exception,
        string errorMessage,
        string errorType,
        string errorStackTrace);
    }
}
