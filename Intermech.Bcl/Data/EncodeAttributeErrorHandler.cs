
// Type: Intermech.Data.EncodeAttributeErrorHandler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Text;


namespace Intermech.Data
{
    internal sealed class EncodeAttributeErrorHandler : IAction
    {
      private readonly IAction encodeAction;
      private readonly string containerName;
      private readonly bool reportErrorsOnly;

      public EncodeAttributeErrorHandler(
        IAction encodeAction,
        string containerName,
        bool reportErrorsOnly)
      {
        if (encodeAction == null)
          throw new ArgumentNullException(nameof (encodeAction));
        if (string.IsNullOrEmpty(containerName))
          throw new ArgumentException();
        this.encodeAction = encodeAction;
        this.containerName = containerName;
        this.reportErrorsOnly = reportErrorsOnly;
      }

      public void Perform()
      {
        try
        {
          this.encodeAction.Perform();
        }
        catch (CantUpdateAttributeValueException ex)
        {
          if (ex.Attribute.Flags[NamedFlags.ThrowSetException] || !this.reportErrorsOnly)
            throw new FaultException(this.GetEncodeErrorMessage(ex, true), (Exception) ex);
          if (!UIReport.Enabled)
            return;
          UIReport.ReportEvent(this.GetEncodeErrorMessage(ex, false), TraceLevel.Warning);
        }
      }

      private string GetEncodeErrorMessage(CantUpdateAttributeValueException x, bool useContainerName)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (useContainerName)
          {
            stringBuilder.Append(this.containerName);
            stringBuilder.Append(':');
            stringBuilder.Append(' ');
          }
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("SR_808"), (object) x.Attribute.Key));
          stringBuilder.Append(' ');
          stringBuilder.Append(x.InnerException != null ? x.InnerException.Message : LocalizationHolder.rm.GetString("SR_809"));
          return stringBuilder.ToString();
        }
      }
    }
}
