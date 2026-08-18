// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.FactoryConfigEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

internal static class FactoryConfigEditor
{
  public static IConfigEditor CreateConfigEditor(
    TreeView treeView,
    long idObjConfig,
    int objectType,
    ContextMenuStrip contextMenu)
  {
    IConfigEditor configEditor = (IConfigEditor) null;
    if (objectType == MetaDataHelper.GetObjectTypeID("cadd9444-306c-11d8-b4e9-00304f19f545"))
      configEditor = (IConfigEditor) new ExportConfigEditor(treeView, treeView.Nodes, contextMenu);
    if (objectType == MetaDataHelper.GetObjectTypeID("cadd9458-306c-11d8-b4e9-00304f19f545"))
      configEditor = (IConfigEditor) new ImportConfigEditor(treeView, treeView.Nodes, contextMenu);
    configEditor?.LoadConfigInObject(idObjConfig);
    return configEditor;
  }

  public static IConfigEditor CreateConfigEditor(
    TreeView treeView,
    string pathFile,
    ContextMenuStrip contextMenu)
  {
    IConfigEditor configEditor = (IConfigEditor) null;
    FileInfo fileInfo = new FileInfo(pathFile);
    if (!fileInfo.Exists)
      return (IConfigEditor) null;
    try
    {
      using (FileStream fileStream = fileInfo.OpenRead())
      {
        XDocument xdocument = XDocument.Load((Stream) fileStream, LoadOptions.PreserveWhitespace);
        if (xdocument.Root != null && string.Compare(xdocument.Root.Name.ToString(), "xmlexportsettings", StringComparison.InvariantCultureIgnoreCase) == 0)
          configEditor = (IConfigEditor) new ExportConfigEditor(treeView, treeView.Nodes, contextMenu);
        else if (xdocument.Root != null)
        {
          if (string.Compare(xdocument.Root.Name.ToString(), "XMLImportSettings", StringComparison.InvariantCultureIgnoreCase) == 0)
            configEditor = (IConfigEditor) new ImportConfigEditor(treeView, treeView.Nodes, contextMenu);
        }
      }
    }
    catch (IOException ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
    configEditor?.LoadConfigInFile(pathFile);
    return configEditor;
  }

  public static void SetFontTreeNode(
    Font fontTreeView,
    TreeNode treeNode,
    FontStyle fontStyle,
    string toolTipText = "")
  {
    Font font = new Font(fontTreeView, fontStyle);
    treeNode.NodeFont = font;
    treeNode.ToolTipText = toolTipText;
  }
}
