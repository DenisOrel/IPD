// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings.KnownPaperFormat
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings
{
    internal class KnownPaperFormat
    {
        public KnownPaperFormat(string baseName, int width, int height)
        {
            this.BaseName = baseName;
            this.PageSize = new Size(width, height);
            this.IsPortait = width <= height;
        }

        public string BaseName { get; private set; }

        public string FullName => this.BaseName + (this.IsPortait ? "" : " альбомный");

        public bool IsPortait { get; private set; }

        public Size PageSize { get; private set; }

        public int Width => this.PageSize.Width;

        public int Height => this.PageSize.Height;

        public float WidthF => Convert.ToSingle((double)this.Width * (360.0 / (double)sbyte.MaxValue));

        public float HeightF
        {
            get => Convert.ToSingle((double)this.Height * (360.0 / (double)sbyte.MaxValue));
        }

        public override bool Equals(object obj)
        {
            return obj is KnownPaperFormat knownPaperFormat && this.BaseName == knownPaperFormat.BaseName && this.IsPortait == knownPaperFormat.IsPortait;
        }

        public override int GetHashCode()
        {
            return (-1369970509 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.BaseName)) * -1521134295 + this.IsPortait.GetHashCode();
        }

        public override string ToString() => this.FullName;
    }
}
