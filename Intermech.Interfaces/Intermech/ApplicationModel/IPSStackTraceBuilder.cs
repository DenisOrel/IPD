
// Type: Intermech.ApplicationModel.IPSStackTraceBuilder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.ApplicationModel
{
    public sealed class IPSStackTraceBuilder : StackTraceBuilder
    {
      protected override void DoAppendException(Exception exception)
      {
        if (exception is CompositeException)
          this.AppendCompositeException((CompositeException) exception);
        else
          base.DoAppendException(exception);
      }

      private void AppendCompositeException(CompositeException ce)
      {
        for (int index = 0; index < ce.List.Count; ++index)
        {
          if (this.TextBuilder.Length > 0)
          {
            this.TextBuilder.AppendLine();
            this.AppendDelimiter();
          }
          if (ce.List.Count > 1)
            this.TextBuilder.AppendFormat("[{0}]", (object) (index + 1));
          this.AppendException(ce.List[index]);
          if (ce.List[index].InnerException != null)
            this.AppendAllInnerExceptions(ce.List[index]);
        }
      }
    }
}
