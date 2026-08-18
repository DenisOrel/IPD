// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrinterNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class PrinterNode : PrintQueueNode
    {
        public PrinterNode(string printerName, List<PrintCenterNode> children = null)
          : base(children: children)
        {
            this.PrinterName = printerName;
            if (children == null)
                this.Children = new List<PrintCenterNode>();
            this.SetMainColumnCaption();
        }

        public string PrinterName { get; private set; }

        public Dictionary<string, List<PrintQueuePagesNode>> GroupNodesByFileName()
        {
            Dictionary<string, List<PrintQueuePagesNode>> dictionary = new Dictionary<string, List<PrintQueuePagesNode>>();
            foreach (PrintCenterNode printCenterNode in this.Children.OfType<LayoutNode>())
            {
                foreach (PrintQueuePagesNode printQueuePagesNode in printCenterNode.Children.OfType<PrintQueuePagesNode>())
                {
                    if (!dictionary.ContainsKey(printQueuePagesNode.FileName))
                        dictionary.Add(printQueuePagesNode.FileName, new List<PrintQueuePagesNode>());
                    dictionary[printQueuePagesNode.FileName].Add(printQueuePagesNode);
                }
            }
            return dictionary;
        }

        protected override void SetMainColumnCaption() => this.MainColumnCaption = this.PrinterName;
    }
}
