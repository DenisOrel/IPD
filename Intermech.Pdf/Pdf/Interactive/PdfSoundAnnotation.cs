// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSoundAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfSoundAnnotation : PdfFileAnnotation
    {
      private PdfSoundIcon m_icon;
      private PdfSound m_sound;

      public PdfSoundAnnotation(RectangleF rectangle, string fileName)
        : base(rectangle)
      {
        this.m_sound = fileName != null ? new PdfSound(fileName) : throw new ArgumentNullException(nameof (fileName));
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Sound"));
      }

      protected override void Save()
      {
        base.Save();
        this.Dictionary.SetProperty("Sound", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_sound));
      }

      public override string FileName
      {
        get => this.m_sound.FileName;
        set
        {
          switch (value)
          {
            case null:
              throw new ArgumentNullException(nameof (FileName));
            case "":
              throw new ArgumentException("FileName can't be empty");
            default:
              if (!(this.m_sound.FileName != value))
                break;
              this.m_sound.FileName = value;
              break;
          }
        }
      }

      public PdfSoundIcon Icon
      {
        get => this.m_icon;
        set
        {
          if (this.m_icon == value)
            return;
          this.m_icon = value;
          this.Dictionary.SetName("Name", this.m_icon.ToString());
        }
      }

      public PdfSound Sound
      {
        get => this.m_sound;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Sound));
          if (value == this.m_sound)
            return;
          this.m_sound = value;
        }
      }
    }
}
