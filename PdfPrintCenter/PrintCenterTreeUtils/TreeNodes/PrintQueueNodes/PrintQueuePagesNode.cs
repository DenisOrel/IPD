// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrintQueuePagesNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class PrintQueuePagesNode : PrintQueueNode
    {
        public PrintQueuePagesNode(PrintQueuePagesNode node)
          : this(node.Parent as LayoutNode, node.PageSize, short.Parse(node.Copies), node.ObjectName, node.FilePath, node.AddFilenameToCaption, node.IgnoreDifferentCopies)
        {
        }

        public PrintQueuePagesNode(
          LayoutNode parent,
          IntermechPageSize pageSize,
          short copies,
          string objectName,
          string filePath,
          bool fitToPage,
          bool addFilenameToCaption,
          bool ignore = false)
          : base((PrintCenterNode)parent, pageSize.Range, copies, objectName, filePath, addFilenameToCaption)
        {
            this.PageSize = pageSize;
            this.FitToPage = fitToPage;
            this.IgnoreDifferentCopies = ignore;
            this.SetMainColumnCaption();
        }

        public IntermechPageSize PageSize { get; private set; }

        public bool IgnoreDifferentCopies { get; set; }

        public bool FitToPage { get; set; }

        protected override void SetMainColumnCaption()
        {
            this.MainColumnCaption = this.ObjectName;
            if (!this.AddFilenameToCaption)
                return;
            this.MainColumnCaption = $"{this.MainColumnCaption} ({this.FileName})";
        }
    }
}
