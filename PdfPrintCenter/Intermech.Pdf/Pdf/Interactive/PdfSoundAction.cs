// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSoundAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfSoundAction : PdfAction
{
  private bool m_mix;
  private bool m_repeat;
  private PdfSound m_sound;
  private bool m_synchronous;
  private float m_volume = 1f;

  public PdfSoundAction(string fileName)
  {
    this.m_sound = fileName != null ? new PdfSound(fileName) : throw new ArgumentNullException(nameof (fileName));
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    this.Dictionary.SetProperty("Sound", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_sound));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("Sound"));
  }

  public string FileName
  {
    get => this.m_sound.FileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArgumentException("FileName can't be an empty string.");
        default:
          if (!(value != this.m_sound.FileName))
            break;
          this.m_sound.FileName = value;
          break;
      }
    }
  }

  public bool Mix
  {
    get => this.m_mix;
    set
    {
      if (value == this.m_mix)
        return;
      this.m_mix = value;
      this.Dictionary.SetBoolean(nameof (Mix), this.m_mix);
    }
  }

  public bool Repeat
  {
    get => this.m_repeat;
    set
    {
      if (this.m_repeat == value)
        return;
      this.m_repeat = value;
      this.Dictionary.SetBoolean(nameof (Repeat), this.m_repeat);
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

  public bool Synchronous
  {
    get => this.m_synchronous;
    set
    {
      if (this.m_synchronous == value)
        return;
      this.m_synchronous = value;
      this.Dictionary.SetBoolean(nameof (Synchronous), this.m_synchronous);
    }
  }

  public float Volume
  {
    get => this.m_volume;
    set
    {
      if ((double) value > 1.0 || (double) value < -1.0)
        throw new ArgumentOutOfRangeException(nameof (Volume));
      if ((double) this.m_volume == (double) value)
        return;
      this.m_volume = value;
      this.Dictionary.SetNumber(nameof (Volume), this.m_volume);
    }
  }
}
