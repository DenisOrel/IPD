
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.LayoutNode




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class LayoutNode : PrintQueueNode
    {
      public LayoutNode(PrinterNode parent, IPdfPageProducer layout, List<PrintCenterNode> children = null)
        : base((PrintCenterNode) parent, children: children)
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
