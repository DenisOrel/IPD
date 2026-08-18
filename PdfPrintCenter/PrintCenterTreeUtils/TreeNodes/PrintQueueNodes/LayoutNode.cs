// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.LayoutNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class LayoutNode : PrintQueueNode
    {
        public LayoutNode(PrinterNode parent, IPdfPageProducer layout, List<PrintCenterNode> children = null)
          : base((PrintCenterNode)parent, children: children)
        {
            this.Layout = layout;
            if (children == null)
                this.Children = new List<PrintCenterNode>();
            this.SetMainColumnCaption();
        }

        public IPdfPageProducer Layout { get; set; }

        public string LayoutName => this.Layout.ToString();

        public void ModifyLayoutName(string layoutName)
        {
            this.Layout.Caption = layoutName;
            this.MainColumnCaption = this.LayoutName;
        }

        protected override void SetMainColumnCaption() => this.MainColumnCaption = this.LayoutName;
    }
}
