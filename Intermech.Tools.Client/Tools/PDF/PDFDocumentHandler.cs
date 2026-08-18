// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFDocumentHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using iTextSharp.text.pdf;
using System.Text;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : SingleFileDocumentHandler(driver, ctx, docItem)
{
  protected override void CorrectAttributes()
  {
    base.CorrectAttributes();
    if (!this.DocumentObject.NewObject)
      return;
    this.ConvertHexEncodedStringToNormalStrings();
  }

  private void ConvertHexEncodedStringToNormalStrings()
  {
    HexEncodedValuesConverter encodedValuesConverter = new HexEncodedValuesConverter();
    Encoding encoding = Encoding.Default;
    foreach (ValueRecord working in this.DocumentAttributes.WorkingSet)
    {
      if (!working.IsNull && working.DataType == typeof (string))
      {
        string str = (string) working.Value;
        string result;
        if (encodedValuesConverter.IsHexEncodedValue(str) && encodedValuesConverter.TryConvertToString(str, encoding, out result))
          working.Value = (object) result;
      }
    }
  }

  protected override bool WriteFileProperties(ContainerValues fileProperties)
  {
    try
    {
      return base.WriteFileProperties(fileProperties);
    }
    catch (FaultException ex)
    {
      if (ex.InnerException != null && ex.InnerException is BadPasswordException)
        return false;
      throw;
    }
  }
}
