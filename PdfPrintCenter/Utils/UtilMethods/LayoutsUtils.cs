
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.LayoutsUtils




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class LayoutsUtils
    {
      public static KnownPaperFormat FindAptPageFormat(SizeF mmPageSize)
      {
        List<KnownPaperFormat> list = KnownPaperFormats.Formats.ToList<KnownPaperFormat>();
        list.Sort((Comparison<KnownPaperFormat>) ((lhs, rhs) => lhs.Width == rhs.Width ? lhs.Height - rhs.Height : lhs.Width - rhs.Width));
        foreach (KnownPaperFormat aptPageFormat in list.Where<KnownPaperFormat>((Func<KnownPaperFormat, bool>) (format => format.IsPortait)))
        {
          if ((double) mmPageSize.Width <= (double) aptPageFormat.Width && (double) mmPageSize.Height <= (double) aptPageFormat.Height || (double) mmPageSize.Width <= (double) aptPageFormat.Height && (double) mmPageSize.Height <= (double) aptPageFormat.Width)
            return aptPageFormat;
        }
        return (KnownPaperFormat) null;
      }

      public static Dictionary<string, HashSet<WorkspacePagesTreeNode>> GetNodesCannotBeDistributed(
        IPdfPageProducer layout,
        Dictionary<string, HashSet<WorkspacePagesTreeNode>> nodesToFilename)
      {
        Dictionary<string, HashSet<WorkspacePagesTreeNode>> cannotBeDistributed = new Dictionary<string, HashSet<WorkspacePagesTreeNode>>();
        if (layout is LayoutAsItIs)
          return cannotBeDistributed;
        LayoutDescriptor layoutDescriptor = layout as LayoutDescriptor;
        foreach (string key in nodesToFilename.Keys)
        {
          foreach (WorkspacePagesTreeNode workspacePagesTreeNode in nodesToFilename[key])
          {
            SizeF pageSize = new SizeF((float) workspacePagesTreeNode.PageSize.MmWidth, (float) workspacePagesTreeNode.PageSize.MmHeight);
            if (!layoutDescriptor.CanDistributePage(pageSize))
            {
              if (!cannotBeDistributed.ContainsKey(key))
                cannotBeDistributed.Add(key, new HashSet<WorkspacePagesTreeNode>());
              cannotBeDistributed[key].Add(workspacePagesTreeNode);
            }
          }
        }
        return cannotBeDistributed;
      }

      public static Dictionary<string, HashSet<WorkspacePagesTreeNode>> GroupNodesByFilename(
        List<WorkspaceTreeNode> nodes)
      {
        Dictionary<string, HashSet<WorkspacePagesTreeNode>> dictionary = new Dictionary<string, HashSet<WorkspacePagesTreeNode>>();
        foreach (WorkspaceTreeNode node in nodes)
        {
          if (!dictionary.ContainsKey(node.FileName))
            dictionary.Add(node.FileName, new HashSet<WorkspacePagesTreeNode>());
          if (node is WorkspaceObjectTreeNode)
          {
            foreach (WorkspacePagesTreeNode workspacePagesTreeNode in node.Children.OfType<WorkspacePagesTreeNode>())
              dictionary[node.FileName].Add(workspacePagesTreeNode);
          }
          else if (node is WorkspacePagesTreeNode workspacePagesTreeNode1)
            dictionary[node.FileName].Add(workspacePagesTreeNode1);
        }
        return dictionary;
      }

      public static Dictionary<string, List<PrintQueuePagesNode>> GroupNodesByFilename(
        List<PrintQueuePagesNode> nodes)
      {
        return nodes.GroupBy<PrintQueuePagesNode, string>((Func<PrintQueuePagesNode, string>) (node => node.FileName)).ToDictionary<IGrouping<string, PrintQueuePagesNode>, string, List<PrintQueuePagesNode>>((Func<IGrouping<string, PrintQueuePagesNode>, string>) (x => x.Key), (Func<IGrouping<string, PrintQueuePagesNode>, List<PrintQueuePagesNode>>) (x => x.ToList<PrintQueuePagesNode>()));
      }

      public static Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>> GroupPagesByAptFormats(
        List<WorkspacePagesTreeNode> nodes)
      {
        Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>> dictionary = new Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>>();
        foreach (WorkspacePagesTreeNode node in nodes)
        {
          KnownPaperFormat aptPageFormat = LayoutsUtils.FindAptPageFormat(new SizeF((float) node.PageSize.MmWidth, (float) node.PageSize.MmHeight));
          if (aptPageFormat == null)
            return (Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>>) null;
          if (!dictionary.ContainsKey(aptPageFormat))
            dictionary.Add(aptPageFormat, new List<WorkspacePagesTreeNode>());
          dictionary[aptPageFormat].Add(node);
        }
        return dictionary;
      }
    }
}
