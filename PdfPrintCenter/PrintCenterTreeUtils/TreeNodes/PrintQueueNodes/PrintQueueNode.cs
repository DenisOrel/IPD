// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrintQueueNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal abstract class PrintQueueNode : PrintCenterNode
    {
        public PrintQueueNode(
          PrintCenterNode parent = null,
          string pages = "",
          short copies = 0,
          string objectName = "",
          string filePath = "",
          bool addFilenameToCaption = false,
          List<PrintCenterNode> children = null)
          : base(parent, objectName, filePath, addFilenameToCaption, children)
        {
            this.Pages = pages;
            this.Copies = copies == (short)0 ? "" : copies.ToString();
        }

        public string Copies { get; set; }
    }
}
