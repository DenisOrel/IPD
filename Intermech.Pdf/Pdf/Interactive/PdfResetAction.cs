// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfResetAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfResetAction : PdfFormAction
    {
      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("ResetForm"));
      }

      public override bool Include
      {
        get => base.Include;
        set
        {
          if (base.Include == value)
            return;
          base.Include = value;
          this.Dictionary.SetNumber("Flags", base.Include ? 0 : 1);
        }
      }
    }
}
