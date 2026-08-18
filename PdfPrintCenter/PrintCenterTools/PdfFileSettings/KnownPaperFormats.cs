// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings.KnownPaperFormats
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings
{
    internal static class KnownPaperFormats
    {
        static KnownPaperFormats()
        {
            KnownPaperFormats.Formats = new List<KnownPaperFormat>()
        {
          new KnownPaperFormat("A0", 841, 1189),
          new KnownPaperFormat("A1", 594, 841),
          new KnownPaperFormat("A2", 420, 594),
          new KnownPaperFormat("A3", 297, 420),
          new KnownPaperFormat("A4", 210, 297),
          new KnownPaperFormat("A5", 148, 210),
          new KnownPaperFormat("A6", 105, 148),
          new KnownPaperFormat("A7", 74, 105),
          new KnownPaperFormat("A8", 52, 74),
          new KnownPaperFormat("A9", 37, 52),
          new KnownPaperFormat("A10", 26, 37),
          new KnownPaperFormat("A0", 1189, 841),
          new KnownPaperFormat("A1", 841, 594),
          new KnownPaperFormat("A2", 594, 420),
          new KnownPaperFormat("A3", 420, 297),
          new KnownPaperFormat("A4", 297, 210),
          new KnownPaperFormat("A5", 210, 148),
          new KnownPaperFormat("A6", 148, 105),
          new KnownPaperFormat("A7", 105, 74),
          new KnownPaperFormat("A8", 74, 52),
          new KnownPaperFormat("A9", 52, 37),
          new KnownPaperFormat("A10", 37, 26),
          new KnownPaperFormat("B0", 1000, 1414),
          new KnownPaperFormat("B1", 707, 1000),
          new KnownPaperFormat("B2", 500, 707),
          new KnownPaperFormat("B3", 353, 500),
          new KnownPaperFormat("B4", 250, 353),
          new KnownPaperFormat("B5", 176 /*0xB0*/, 250),
          new KnownPaperFormat("B6", 125, 176 /*0xB0*/),
          new KnownPaperFormat("B7", 88, 125),
          new KnownPaperFormat("B8", 62, 88),
          new KnownPaperFormat("B9", 44, 62),
          new KnownPaperFormat("B10", 31 /*0x1F*/, 44),
          new KnownPaperFormat("B0", 1414, 1000),
          new KnownPaperFormat("B1", 1000, 707),
          new KnownPaperFormat("B2", 707, 500),
          new KnownPaperFormat("B3", 500, 353),
          new KnownPaperFormat("B4", 353, 250),
          new KnownPaperFormat("B5", 250, 176 /*0xB0*/),
          new KnownPaperFormat("B6", 176 /*0xB0*/, 125),
          new KnownPaperFormat("B7", 125, 88),
          new KnownPaperFormat("B8", 88, 62),
          new KnownPaperFormat("B9", 62, 44),
          new KnownPaperFormat("B10", 44, 31 /*0x1F*/),
          new KnownPaperFormat("C0", 917, 1297),
          new KnownPaperFormat("C1", 648, 917),
          new KnownPaperFormat("C2", 458, 648),
          new KnownPaperFormat("C3", 324, 458),
          new KnownPaperFormat("C4", 229, 324),
          new KnownPaperFormat("C5", 162, 229),
          new KnownPaperFormat("C6", 114, 162),
          new KnownPaperFormat("C7", 81, 114),
          new KnownPaperFormat("C8", 57, 81),
          new KnownPaperFormat("C9", 40, 57),
          new KnownPaperFormat("C10", 28, 40),
          new KnownPaperFormat("C0", 1297, 917),
          new KnownPaperFormat("C1", 917, 648),
          new KnownPaperFormat("C2", 648, 458),
          new KnownPaperFormat("C3", 458, 324),
          new KnownPaperFormat("C4", 324, 229),
          new KnownPaperFormat("C5", 229, 162),
          new KnownPaperFormat("C6", 162, 114),
          new KnownPaperFormat("C7", 114, 81),
          new KnownPaperFormat("C8", 81, 57),
          new KnownPaperFormat("C9", 57, 40),
          new KnownPaperFormat("C10", 40, 28),
          new KnownPaperFormat("ANSI A (Letter)", 216, 280),
          new KnownPaperFormat("Legal", 216, 356),
          new KnownPaperFormat("ANSI B (Ledger или Tabloid)", 432, 279),
          new KnownPaperFormat("ANSI C", 432, 559),
          new KnownPaperFormat("ANSI D", 559, 864),
          new KnownPaperFormat("ANSI E", 864, 1121),
          new KnownPaperFormat("ANSI A (Letter)", 280, 216),
          new KnownPaperFormat("Legal", 356, 216),
          new KnownPaperFormat("ANSI B (Ledger или Tabloid)", 279, 432),
          new KnownPaperFormat("ANSI C", 559, 432),
          new KnownPaperFormat("ANSI D", 864, 559),
          new KnownPaperFormat("ANSI E", 1121, 864)
        };
        }

        public static List<KnownPaperFormat> Formats { get; set; }

        public static string AcceptsAllSizesOfFormat(string formatName)
        {
            if (formatName.Contains("size sheet"))
            {
                string str = formatName.Replace(" size sheet", "").Replace(" ", "");
                if (str == "A" || str == "B" || str == "C")
                    return str;
            }
            return (string)null;
        }

        public static KnownPaperFormat GetFormat(string fullFormatName)
        {
            string baseFormatName = fullFormatName;
            bool isPortrait = KnownPaperFormats.IsPortraitFormat(fullFormatName);
            if (!isPortrait)
                baseFormatName = baseFormatName.Replace("альбомный", "").Trim();
            return KnownPaperFormats.GetFormat(baseFormatName, isPortrait);
        }

        public static KnownPaperFormat GetFormat(string baseFormatName, bool isPortrait = true)
        {
            return KnownPaperFormats.Formats.FirstOrDefault<KnownPaperFormat>((Func<KnownPaperFormat, bool>)(format => format.BaseName.Contains(baseFormatName) && format.IsPortait == isPortrait));
        }

        public static List<KnownPaperFormat> GetSmallerFormats(KnownPaperFormat mainFormat)
        {
            return KnownPaperFormats.Formats.Where<KnownPaperFormat>((Func<KnownPaperFormat, bool>)(format => format.Width < mainFormat.Width && format.Height < mainFormat.Height)).ToList<KnownPaperFormat>();
        }

        public static bool IsFormatName(string fullFormatName)
        {
            return KnownPaperFormats.GetFormat(fullFormatName) != null;
        }

        public static bool IsPortraitFormat(string formatName) => !formatName.Contains("альбомный");

        public static void LoadToComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (KnownPaperFormat format in KnownPaperFormats.Formats)
                comboBox.Items.Add((object)format);
        }
    }
}
