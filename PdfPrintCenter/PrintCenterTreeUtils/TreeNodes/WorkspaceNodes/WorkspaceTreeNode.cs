// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes.WorkspaceTreeNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes
{
    internal abstract class WorkspaceTreeNode : PrintCenterNode
    {
        public WorkspaceTreeNode(
          PrintCenterNode parent = null,
          string objectName = "",
          string filePath = "",
          string format = "",
          bool addFilenameToCaption = false,
          List<PrintCenterNode> children = null)
          : base(parent, objectName, filePath, addFilenameToCaption, children)
        {
            this.Format = format;
        }

        public string Format { get; set; }
    }
}
