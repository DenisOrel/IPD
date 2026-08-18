
// Type: Intermech.Navigator.CommonCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Провайдер команд контекстного меню, общих для всех категорий элементов
/// навигации.
/// </summary>
internal class CommonCommandsProvider : ICommandsProvider
{
  private NavigatorTreeViewSelectedItems _nav;
  private Dictionary<int, Image> _images = new Dictionary<int, Image>();
  private Dictionary<string, Image> _stateImages = new Dictionary<string, Image>();

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("CopyHyperlink", new CommandInfo(0));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    if (viewServices.GetService(typeof (IViewState)) is IViewState service)
    {
      long viewState = (long) service.ViewState;
    }
    if (items.Count == 1 && items.GetItemData(0, typeof (ICanOpenInNewWindow)) != null)
      groupCommands.Add("OpenInNewWindow", new CommandInfo(4, new ClickEventHandler(CommonCommandsProvider.OpenInNewWindow)));
    if (this.CheckSelectedItemsForOpenInParentComposition(items))
      groupCommands.Add("OpenInParentComposition", new CommandInfo(0, new ClickEventHandler(this.OpenInParentComposition)));
    if (items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID)
    {
      groupCommands.Add("CopyHyperlinkForObjects", new CommandInfo(0, new ClickEventHandler(this.CopyHyperlinkForObjects)));
      groupCommands.Add("CopyHyperlinkForObjectCart", new CommandInfo(0, new ClickEventHandler(this.CopyHyperlinkForObjectCart)));
      groupCommands.Add("CopyHyperlinkForObjectView", new CommandInfo(0, new ClickEventHandler(this.CopyHyperlinkForObjectView)));
      if (!(items is NavigatorTreeViewSelectedItems viewSelectedItems) || viewSelectedItems.Nodes.Length != 0 && viewSelectedItems.Nodes[0].Tree.RootNode != null && viewSelectedItems.Nodes[0].Tree.RootNode.NodeID.TypeID == 0)
        return groupCommands;
      groupCommands.Add("GetHTML", new CommandInfo(0, new ClickEventHandler(this.GetHTML)));
    }
    return groupCommands;
  }

  private void GetHTML(ISelectedItems items, System.IServiceProvider viewservices, object additionalinfo)
  {
    this._nav = items as NavigatorTreeViewSelectedItems;
    if (this._nav == null)
      throw new KernelException("Произошла внутренняя ошибка. Создание отчёта невозможно. Элемент не поддерживает выделение.");
    NavigatorTreeNode rootNode = this._nav.Nodes[0].Tree.RootNode;
    rootNode.Expanded = true;
    if (rootNode.Children.Count <= 0)
      throw new KernelException("У главного элемента отсутствует состав. Создание отчёта невозможно.");
    ExpandNodesWithBeginUpdate.Execute(this._nav.Nodes[0].Tree.RootNode, this._nav.Nodes[0].Tree, viewservices);
    ExpandNodesWithBeginUpdate.currForm.FormClosed += new FormClosedEventHandler(this.currForm_FormClosed);
  }

  private void currForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    try
    {
      if (!(sender is ExpandNodesWithBeginUpdate nodesWithBeginUpdate) || nodesWithBeginUpdate.DialogResult == DialogResult.Cancel)
        return;
      this.GenerateAndSaveHTML(this._nav.Nodes[0].Tree.RootNode, this._nav.Nodes[0].Tree.TreeColumns);
    }
    catch (Exception ex)
    {
      throw new KernelException($"Произошла ошибка при формировании отчёта HTML. {ex.Message}", ex.InnerException);
    }
    finally
    {
      this._images.Clear();
      this._stateImages.Clear();
    }
  }

  public void GenerateAndSaveHTML(NavigatorTreeNode nodes, NodeColumnCollection columns)
  {
    int indexOfCaption = -1;
    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
    saveFileDialog1.Filter = "html файл|*.html";
    saveFileDialog1.Title = "Сохранить отчёт";
    saveFileDialog1.DefaultExt = "html";
    saveFileDialog1.AddExtension = true;
    saveFileDialog1.RestoreDirectory = true;
    SaveFileDialog saveFileDialog2 = saveFileDialog1;
    StringBuilder sb = new StringBuilder();
    sb.Append("<html>\n<head>\n<meta http-equiv=Content-Type content=\"text/html;charset=utf-8\">\n");
    sb.Append("<style>\nbody{\nbackground-color:white;\nfont-family:\"Verdana\",sans-serif;\nmargin-left:10px;\nmargin-top:10px;\n} \ntable.sostavtable{\nbackground-color:#f0f0e0;\nborder-collapse:collapse;\nborder:#000000 1px solid;\nfont-size:100%;\n} \ntable.sostavtable caption{\nbackground-color:#cecf9c;\nborder-style:solid;\nborder-width:1px 1px 0px 1px;\n} \ntable.sostavtable th{\nbackground-color:#cecf9c;\ncolor:#000000;\nborder-style:solid;\nborder-width:1px;\nborder-color:#000000;\n} \ntable.sostavtable td{\nborder-style:solid;\nborder-width:1px 0px 1px 0px;\npadding:0px 5px 0px 5px;\nwhite-space:nowrap;\n}\n</style>\n");
    sb.Append("</head>\n<body>\n<table class=\"sostavtable\">\n<caption>Дерево состава объекта</caption>\n<tr>\n");
    int num1 = 0;
    bool flag = false;
    foreach (NodeColumn column in (List<NodeColumn>) columns)
    {
      if (!flag && (column.Attribute.AttributeID == -50 || column.Attribute.AttributeGuid == new Guid("cad0001f-306c-11d8-b4e9-00304f19f545") || column.Attribute.AttributeGuid == new Guid("cad00020-306c-11d8-b4e9-00304f19f545")))
      {
        indexOfCaption = num1;
        flag = true;
        saveFileDialog2.FileName = this._nav.Nodes[0].Tree.RootNode.Values[indexOfCaption].ToString();
      }
      sb.Append($"<th>{column.Caption}</th>\n");
      ++num1;
    }
    sb.Append("</tr>\n<tr>\n");
    for (int index = 0; index < nodes.Values.Length; ++index)
    {
      object obj = nodes.Values[index];
      int num2 = 0;
      if (Statics.IconSrv != null)
      {
        if (!this._images.ContainsKey(nodes.NodeID.TypeID))
        {
          Image image32x16 = Images32x16_Cache.GetImage32x16(4, nodes.NodeID.TypeID, (NavigatorTreeNode) null);
          this._images.Add(nodes.NodeID.TypeID, image32x16);
        }
        if (this._images[nodes.NodeID.TypeID] != null)
          num2 = this._images[nodes.NodeID.TypeID].Width == this._images[nodes.NodeID.TypeID].Height ? 16 /*0x10*/ : this._images[nodes.NodeID.TypeID].Width;
      }
      if (index == indexOfCaption)
        sb.Append($"<td><img src=\"img/{nodes.NodeID.TypeID}.png\" style=\"width: {num2}px;\">{obj}</td>\n");
      else if (!(obj is byte[]))
      {
        sb.Append($"<td>{obj}</td>\n");
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(((NodeID) nodes.NodeID).LCStepID);
          if (Statics.IconSrv != null)
          {
            Image image32x16 = Images32x16_Cache.GetImage32x16(8, lifecycleStep.LevelID, (NavigatorTreeNode) null);
            if (!this._stateImages.ContainsKey($"{nodes.NodeID.TypeID}_s"))
              this._stateImages.Add($"{nodes.NodeID.TypeID}_s", image32x16);
            int num3 = 0;
            if (image32x16 != null)
              num3 = image32x16.Width == image32x16.Height ? 16 /*0x10*/ : image32x16.Width;
            sb.Append($"<td><img src=\"img/{nodes.NodeID.TypeID}_s.png\" title=\"{lifecycleStep.LCName}\" style=\"width: {num3}px;\"></td>\n");
          }
        }
      }
    }
    sb.Append("</tr>\n");
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) nodes.Children)
    {
      int num4 = 0;
      if (Statics.IconSrv != null)
      {
        if (!this._images.ContainsKey(child.NodeID.TypeID))
        {
          Image image32x16 = Images32x16_Cache.GetImage32x16(4, child.NodeID.TypeID, (NavigatorTreeNode) null);
          this._images.Add(child.NodeID.TypeID, image32x16);
        }
        if (this._images[child.NodeID.TypeID] != null)
          num4 = this._images[child.NodeID.TypeID].Width == this._images[child.NodeID.TypeID].Height ? 16 /*0x10*/ : this._images[child.NodeID.TypeID].Width;
      }
      sb.Append("<tr>\n");
      for (int index = 0; index < child.Values.Length; ++index)
      {
        object obj = child.Values[index];
        if (index == indexOfCaption)
          sb.Append($"<td style=\"padding-left:{child.Level * 10}px\"><img src=\"img/{child.NodeID.TypeID}.png\" style=\"width: {num4}px;\">{obj}</td>\n");
        else if (!(obj is byte[]))
        {
          sb.Append($"<td>{obj}</td>\n");
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(((NodeID) child.NodeID).LCStepID);
            if (Statics.IconSrv != null)
            {
              Image image32x16 = Images32x16_Cache.GetImage32x16(8, lifecycleStep.LevelID, (NavigatorTreeNode) null);
              if (!this._stateImages.ContainsKey($"{child.NodeID.TypeID}_s"))
                this._stateImages.Add($"{child.NodeID.TypeID}_s", image32x16);
              int num5 = 0;
              if (image32x16 != null)
                num5 = image32x16.Width == image32x16.Height ? 16 /*0x10*/ : image32x16.Width;
              sb.Append($"<td><img src=\"img/{child.NodeID.TypeID}_s.png\" title=\"{lifecycleStep.LCName}\" style=\"width: {num5}px;\"></td>\n");
            }
          }
        }
      }
      sb.Append("</tr>\n");
      if (child.Children.Count > 0)
        this.GenerateForChild(child.Children, sb, indexOfCaption);
    }
    sb.Append("</body>\n");
    sb.Append("</html>\n");
    if (saveFileDialog2.ShowDialog() != DialogResult.OK)
      return;
    StreamWriter streamWriter = new StreamWriter((Stream) File.Open(saveFileDialog2.FileName, FileMode.Create, FileAccess.Write));
    streamWriter.Write(sb.ToString());
    streamWriter.Close();
    string path = new FileInfo(saveFileDialog2.FileName).DirectoryName + "\\img";
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    foreach (KeyValuePair<int, Image> image in this._images)
    {
      using (FileStream fileStream = new FileStream($"{path}\\{image.Key}.png", FileMode.Create))
        image.Value.Save((Stream) fileStream, ImageFormat.Png);
    }
    foreach (KeyValuePair<string, Image> stateImage in this._stateImages)
    {
      using (FileStream fileStream = new FileStream($"{path}\\{stateImage.Key}.png", FileMode.Create))
        stateImage.Value.Save((Stream) fileStream, ImageFormat.Png);
    }
    Process.Start(saveFileDialog2.FileName);
  }

  private void GenerateForChild(
    NavigatorTreeNodes navigatorTreeNodes,
    StringBuilder sb,
    int indexOfCaption)
  {
    foreach (NavigatorTreeNode navigatorTreeNode in (List<NavigatorTreeNode>) navigatorTreeNodes)
    {
      int num1 = 0;
      if (Statics.IconSrv != null)
      {
        if (!this._images.ContainsKey(navigatorTreeNode.NodeID.TypeID))
        {
          Image image32x16 = Images32x16_Cache.GetImage32x16(4, navigatorTreeNode.NodeID.TypeID, (NavigatorTreeNode) null);
          this._images.Add(navigatorTreeNode.NodeID.TypeID, image32x16);
        }
        num1 = this._images[navigatorTreeNode.NodeID.TypeID].Width == this._images[navigatorTreeNode.NodeID.TypeID].Height ? 16 /*0x10*/ : this._images[navigatorTreeNode.NodeID.TypeID].Width;
      }
      sb.Append("<tr>\n");
      for (int index = 0; index < navigatorTreeNode.Values.Length; ++index)
      {
        object obj = navigatorTreeNode.Values[index];
        if (index == indexOfCaption)
          sb.Append($"<td style=\"padding-left:{navigatorTreeNode.Level * 10}px\"><img src=\"img/{navigatorTreeNode.NodeID.TypeID}.png\" style=\"width: {num1}px;\">{obj}</td>\n");
        else if (!(obj is byte[]))
        {
          sb.Append($"<td>{obj}</td>\n");
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(((NodeID) navigatorTreeNode.NodeID).LCStepID);
            if (Statics.IconSrv != null)
            {
              Image image32x16 = Images32x16_Cache.GetImage32x16(8, lifecycleStep.LevelID, (NavigatorTreeNode) null);
              if (!this._stateImages.ContainsKey($"{navigatorTreeNode.NodeID.TypeID}_s"))
                this._stateImages.Add($"{navigatorTreeNode.NodeID.TypeID}_s", image32x16);
              int num2 = 0;
              if (image32x16 != null)
                num2 = image32x16.Width == image32x16.Height ? 16 /*0x10*/ : image32x16.Width;
              sb.Append($"<td><img src=\"img/{navigatorTreeNode.NodeID.TypeID}_s.png\" title=\"{lifecycleStep.LCName}\" style=\"width: {num2}px;\"></td>\n");
            }
          }
        }
      }
      sb.Append("</tr>\n");
      if (navigatorTreeNode.Children.Count > 0)
        this.GenerateForChild(navigatorTreeNode.Children, sb, indexOfCaption);
    }
  }

  /// <summary>Формируем HTML для копирования в буфер</summary>
  /// <param name="htmlFragment">ссылки на объекты</param>
  /// <returns>итоговый хтмл документ который будет помещён в буфер</returns>
  public string CopyToClipboard(string htmlFragment)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string str1 = "Format:HTML Format\r\nVersion:0.9\r\nStartHTML:<<<<<<<1\r\nEndHTML:<<<<<<<2\r\nStartFragment:<<<<<<<3\r\nEndFragment:<<<<<<<4\r\n";
    string str2 = "<HTML><BODY>";
    string str3 = "</BODY></HTML>";
    stringBuilder.Append(str1);
    int length1 = stringBuilder.Length;
    stringBuilder.Append(str2);
    int length2 = stringBuilder.Length;
    stringBuilder.Append(htmlFragment);
    int length3 = stringBuilder.Length;
    stringBuilder.Append(str3);
    int length4 = stringBuilder.Length;
    stringBuilder.Replace("<<<<<<<1", $"{length1,8}");
    stringBuilder.Replace("<<<<<<<2", $"{length4,8}");
    stringBuilder.Replace("<<<<<<<3", $"{length2,8}");
    stringBuilder.Replace("<<<<<<<4", $"{length3,8}");
    return stringBuilder.ToString();
  }

  public string CopyHtmlToClipBoard(string html)
  {
    Encoding utF8 = Encoding.UTF8;
    string format = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";
    string s1 = $"<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset={utF8.WebName}\">\r\n<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n<!--StartFragment-->";
    string s2 = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";
    string s3 = string.Format(format, (object) 0, (object) 0, (object) 0, (object) 0);
    int byteCount1 = utF8.GetByteCount(s3);
    int byteCount2 = utF8.GetByteCount(s1);
    int byteCount3 = utF8.GetByteCount(html);
    int byteCount4 = utF8.GetByteCount(s2);
    return string.Format(format, (object) byteCount1, (object) (byteCount1 + byteCount2 + byteCount3 + byteCount4), (object) (byteCount1 + byteCount2), (object) (byteCount1 + byteCount2 + byteCount3)) + s1 + html + s2;
  }

  private void CopyHyperlinkForObjects(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Clipboard.Clear();
    this.SetClipboardForItems(items, "<a href='ips://object/{0}'>{1}</a><br>", "ips://object/{0}\n");
  }

  private void CopyHyperlinkForObjectCart(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Clipboard.Clear();
    this.SetClipboardForItems(items, "<a href='ips://object/{0}/card'>{1}_Карточка</a><br>", "ips://object/{0}/card\n");
  }

  private void CopyHyperlinkForObjectView(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Clipboard.Clear();
    this.SetClipboardForItems(items, "<a href='ips://object/{0}/view'>{1}_Просмотр</a><br>", "ips://object/{0}/view\n");
  }

  private void SetClipboardForItems(ISelectedItems items, string links, string text)
  {
    DataObject data = new DataObject();
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      stringBuilder1.Append(string.Format(text, (object) itemData.ObjectID));
      stringBuilder2.Append(string.Format(links, (object) itemData.ObjectID, (object) itemData.Caption));
    }
    string clipBoard = this.CopyHtmlToClipBoard(stringBuilder2.ToString());
    data.SetData(System.Windows.Forms.DataFormats.Text, true, (object) stringBuilder1.ToString());
    data.SetData(System.Windows.Forms.DataFormats.Html, (object) new MemoryStream(Encoding.UTF8.GetBytes(clipBoard)));
    data.SetData(System.Windows.Forms.DataFormats.OemText, true, (object) stringBuilder1.ToString());
    data.SetData(System.Windows.Forms.DataFormats.UnicodeText, true, (object) stringBuilder1.ToString());
    data.SetData(System.Windows.Forms.DataFormats.Locale, true, (object) stringBuilder1.ToString());
    Clipboard.SetDataObject((object) data, true);
  }

  private static void OpenInNewWindow(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.GetItemData(0, typeof (IDescriptor)) is IDescriptor itemData)
    {
      Utils.OpenNewWindow(itemData, viewServices);
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_841"), LocalizationHolder.rm.GetString("Client.Core_453"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void OpenInParentComposition(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBRelationID dbRelationId = this.CheckSelectedItemsForOpenInParentComposition(items) ? items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID : throw new Exception();
    items.GetItemData(0, typeof (IDBTypedObjectID));
    NavWindow navWindow = Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(dbRelationId.ProjID), viewServices);
    if (navWindow == null)
    {
      int num = (int) MessageBox.Show("Данный объкт невозможно открыть в новом окне", "Intermech Profesional Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      navWindow.TreeView.PopulateNode(navWindow.TreeView.RootNode);
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) navWindow.TreeView.RootNode.Children)
      {
        if (child.NodeID is NodeID nodeId && nodeId.PrjLinkID == dbRelationId.Value)
        {
          navWindow.TreeView.FocusedNode = child;
          break;
        }
      }
    }
  }

  private bool CheckSelectedItemsForOpenInParentComposition(ISelectedItems selectedItems)
  {
    IDBTypedObjectID parentData = selectedItems.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBRelationID itemData1 = selectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID itemData2 = selectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    return selectedItems.Count == 1 && selectedItems.GetParentData(0, typeof (ICanOpenInNewWindow)) != null && parentData != null && parentData.ObjectType != -1 && Utils.EnableOpenInNewWindow(parentData.ObjectType) && itemData1 != null && !RelationHelper.IsUnknownRelationID(itemData1.Value) && !ObjectHelper.IsUnknownObjectVersionID(itemData1.ProjID) && itemData2 != null && !ObjectHelper.IsUnknownObjectVersionID(itemData2.ObjectID);
  }
}
