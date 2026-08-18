// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes.WorkspacePagesTreeNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes
{
    internal class WorkspacePagesTreeNode : WorkspaceTreeNode
    {
        public WorkspacePagesTreeNode(
          WorkspaceObjectTreeNode parent,
          string filePath,
          IntermechPageSize pageSize)
          : base((PrintCenterNode)parent, filePath: filePath, format: pageSize.Name)
        {
            this.AddFilenameToCaption = this.Parent.AddFilenameToCaption;
            this.ObjectName = this.Parent.ObjectName;
            this.PageSize = pageSize;
            this.Pages = this.PageSize.Range;
            this.SetMainColumnCaption();
        }

        public IntermechPageSize PageSize { get; private set; }

        protected override void SetMainColumnCaption() => this.MainColumnCaption = this.Pages;
    }
}
