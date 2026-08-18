// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedSoundAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.IO;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedSoundAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfAppearance m_appearance;
      private PdfCrossTable m_crossTable;
      private PdfDictionary m_dictionary;
      private PdfSoundIcon m_icon;
      private PdfSound m_sound;

      internal PdfLoadedSoundAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle)
        : base(dictionary, crossTable)
      {
        if (PdfCrossTable.Dereference((PdfCrossTable.Dereference(dictionary[nameof (Sound)]) as PdfDictionary)["T"]) is PdfString pdfString)
        {
          string str = pdfString.Value;
          PdfReferenceHolder pdfReferenceHolder = dictionary[nameof (Sound)] as PdfReferenceHolder;
          if (pdfReferenceHolder == (PdfReferenceHolder) null)
            throw new ArgumentNullException();
          byte[] data = (pdfReferenceHolder.Object as PdfStream).Data;
          FileStream fileStream = File.Create(Path.GetFileName(str));
          fileStream.Write(data, 0, data.Length);
          fileStream.Close();
          this.m_dictionary = dictionary;
          this.m_crossTable = crossTable;
          this.m_sound = new PdfSound(str, true);
        }
        else
        {
          this.m_dictionary = !(dictionary[nameof (Sound)] as PdfReferenceHolder == (PdfReferenceHolder) null) ? dictionary : throw new ArgumentNullException();
          this.m_crossTable = crossTable;
          this.m_sound = new PdfSound();
        }
      }

      private PdfSoundEncoding GetEncodigType(string eType)
      {
        PdfSoundEncoding encodigType = PdfSoundEncoding.Raw;
        string str = eType;
        switch (str)
        {
          case null:
            return encodigType;
          case "Raw":
            return PdfSoundEncoding.Raw;
          default:
            if (!(str != "Signed"))
              return PdfSoundEncoding.Signed;
            if (str == "MuLaw")
              return PdfSoundEncoding.MuLaw;
            return str != "ALaw" ? encodigType : PdfSoundEncoding.ALaw;
        }
      }

      private string GetFileName()
      {
        string empty = string.Empty;
        if (this.Dictionary.ContainsKey("Sound"))
          empty = ((this.m_crossTable.GetObject(this.Dictionary["Sound"]) as PdfDictionary)["T"] as PdfString).Value.ToString();
        return empty;
      }

      private PdfSoundIcon GetIcon()
      {
        PdfSoundIcon icon = PdfSoundIcon.Mic;
        if (this.Dictionary.ContainsKey("Name"))
          icon = this.GetIconName((this.Dictionary["Name"] as PdfName).Value.ToString());
        return icon;
      }

      private PdfSoundIcon GetIconName(string iType)
      {
        PdfSoundIcon iconName = PdfSoundIcon.Mic;
        string str = iType;
        switch (str)
        {
          case null:
            return iconName;
          case "Mic":
            return PdfSoundIcon.Mic;
          default:
            return str != "Speaker" ? iconName : PdfSoundIcon.Speaker;
        }
      }

      private PdfSound GetSound()
      {
        PdfSound sound = new PdfSound(this.GetFileName());
        if (this.Dictionary.ContainsKey("Sound"))
        {
          PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["Sound"]) as PdfDictionary;
          if (pdfDictionary.ContainsKey("B"))
            sound.Bits = (pdfDictionary["B"] as PdfNumber).IntValue;
          if (pdfDictionary.ContainsKey("R"))
            sound.Rate = (pdfDictionary["R"] as PdfNumber).IntValue;
          if (pdfDictionary.ContainsKey("C"))
            sound.Channels = (pdfDictionary["C"] as PdfNumber).IntValue != 1 ? PdfSoundChannels.Stereo : PdfSoundChannels.Mono;
          if (pdfDictionary.ContainsKey("E"))
          {
            PdfName pdfName = pdfDictionary["E"] as PdfName;
            sound.Encoding = this.GetEncodigType(pdfName.Value.ToString());
          }
        }
        return sound;
      }

      public string FileName => this.GetFileName();

      public PdfSoundIcon Icon
      {
        get => this.GetIcon();
        set
        {
          this.m_icon = value;
          this.Dictionary.SetName("Name", this.m_icon.ToString());
        }
      }

      public PdfSound Sound
      {
        get => this.GetSound();
        set
        {
          this.m_sound = value;
          this.Dictionary.Remove(nameof (Sound));
          this.Dictionary.SetProperty(nameof (Sound), (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_sound));
          this.Dictionary.Modify();
        }
      }
    }
}
