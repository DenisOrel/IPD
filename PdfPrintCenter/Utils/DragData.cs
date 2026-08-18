// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.DragData
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class DragData
    {
        public DragData(Control control, IList selectedNodes)
        {
            this.Control = control;
            this.SelectedNodes = selectedNodes;
        }

        public Control Control { get; private set; }

        public IList SelectedNodes { get; private set; }
    }
}
