// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFileLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfFileLinkAnnotation : PdfActionLinkAnnotation
    {
      private PdfLaunchAction m_action;

      public PdfFileLinkAnnotation(RectangleF rectangle, string fileName)
        : base(rectangle)
      {
        switch (fileName)
        {
          case null:
            throw new ArgumentNullException(nameof (fileName));
          case "":
            throw new ArgumentException("fileName - string can not be empty");
          default:
            this.m_action = new PdfLaunchAction(fileName);
            break;
        }
      }

      protected override void Save()
      {
        base.Save();
        this.Dictionary.SetProperty("A", (IPdfWrapper) this.m_action);
      }

      public override PdfAction Action
      {
        get => base.Action;
        set
        {
          base.Action = value;
          this.m_action.Next = value;
        }
      }

      public string FileName
      {
        get => this.m_action.FileName;
        set
        {
          switch (value)
          {
            case null:
              throw new ArgumentNullException(nameof (FileName));
            case "":
              throw new ArgumentException("FileName - string can not be empty");
            default:
              if (!(this.m_action.FileName != value))
                break;
              this.m_action.FileName = value;
              break;
          }
        }
      }
    }
}
