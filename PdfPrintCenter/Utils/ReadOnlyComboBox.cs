// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.ReadOnlyComboBox
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class ReadOnlyComboBox : ComboBox
    {
        public ReadOnlyComboBox() => this.KeyPress += new KeyPressEventHandler(this.OnKeyPress);

        private void OnKeyPress(object sender, KeyPressEventArgs e) => e.KeyChar = char.MinValue;
    }
}
