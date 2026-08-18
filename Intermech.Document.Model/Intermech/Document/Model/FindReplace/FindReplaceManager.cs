// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FindReplace.FindReplaceManager
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.FindReplace;

public class FindReplaceManager
{
  private static List<string> historyFind = new List<string>();
  private static List<string> historyReplace = new List<string>();
  private bool initialized;
  private DocumentControl docControl;
  private string findWhat;
  private string replaceWith;
  private string[] possibleSearchPlaces;
  private int selectedSearchPlace;
  private SearchDirrection searchDirrection;
  private bool matchCase;
  private bool matchWholeWord;
  private string curNodeId = "";
  private int position = -1;

  public FindReplaceManager(DocumentControl docControl)
  {
    this.PossibleSearchPlaces = new string[2];
    this.PossibleSearchPlaces[0] = LocalizationHolder.rm.GetString("Document.Model_592");
    this.PossibleSearchPlaces[1] = LocalizationHolder.rm.GetString("Document.Model_593");
    this.SelectedSearchPlace = 0;
    this.SearchDirrection = SearchDirrection.ToEnd;
    this.docControl = docControl;
  }

  public static List<string> HistoryFind
  {
    get => FindReplaceManager.historyFind;
    set => FindReplaceManager.historyFind = value;
  }

  public static List<string> HistoryReplace
  {
    get => FindReplaceManager.historyReplace;
    set => FindReplaceManager.historyReplace = value;
  }

  public bool Initialized
  {
    get => this.initialized;
    set => this.initialized = value;
  }

  public DocumentControl DocumentControl => this.docControl;

  /// <summary> Строка поиска </summary>
  public string FindWhat
  {
    get => this.findWhat;
    set => this.findWhat = value;
  }

  /// <summary> На что требуется заменять найденый текст </summary>
  public string ReplaceWith
  {
    get => this.replaceWith;
    set => this.replaceWith = value;
  }

  /// <summary> Список доступных мест для поиска текста (например, поиск в [текущем документе], [на текущей странице] и т.п.) </summary>
  public string[] PossibleSearchPlaces
  {
    get => this.possibleSearchPlaces;
    set => this.possibleSearchPlaces = value;
  }

  /// <summary> Индекс выбраного места для поиска в PossibleSearchPlaces </summary>
  public int SelectedSearchPlace
  {
    get => this.selectedSearchPlace;
    set => this.selectedSearchPlace = value;
  }

  /// <summary> Направление сортировки </summary>
  public SearchDirrection SearchDirrection
  {
    get => this.searchDirrection;
    set => this.searchDirrection = value;
  }

  /// <summary> Признак того, что поиск должен вестись с учётом регистра </summary>
  public bool MatchCase
  {
    get => this.matchCase;
    set => this.matchCase = value;
  }

  /// <summary> Признак того, что ищется слово "целиком" </summary>
  public bool MatchWholeWord
  {
    get => this.matchWholeWord;
    set => this.matchWholeWord = value;
  }

  public void Find() => this.Find(FindOperation.Find);

  public void Replace() => this.Find(FindOperation.Replace);

  public void ReplaceAll() => this.Find(FindOperation.ReplaceAll);

  private void Find(FindOperation oper)
  {
    if (!this.Initialized)
      return;
    if (this.FindWhat != string.Empty && !FindReplaceManager.HistoryFind.Contains(this.FindWhat))
      FindReplaceManager.HistoryFind.Add(this.FindWhat);
    if (oper != FindOperation.Find && this.ReplaceWith != string.Empty && !FindReplaceManager.HistoryReplace.Contains(this.ReplaceWith))
      FindReplaceManager.HistoryReplace.Add(this.ReplaceWith);
    SearchDirrection searchDirrection = this.SearchDirrection;
    if (oper == FindOperation.ReplaceAll)
      this.SearchDirrection = SearchDirrection.EntireDocSearch;
    DocumentTreeNode node1 = this.docControl.SelectedNode;
    DocumentTreeNode parentNode = (DocumentTreeNode) null;
    DocumentTreeNode node2 = (DocumentTreeNode) null;
    int pos = -1;
    if (this.docControl.Document != null)
    {
      if (this.SelectedSearchPlace == 0)
      {
        parentNode = (DocumentTreeNode) this.docControl.Document;
        node2 = this.SearchDirrection == SearchDirrection.ToBegin ? (this.docControl.Document.NodesCount > 0 ? this.docControl.Document.Nodes[this.docControl.Document.NodesCount - 1] : (DocumentTreeNode) null) : (this.docControl.Document.NodesCount > 0 ? this.docControl.Document.Nodes[0] : (DocumentTreeNode) null);
      }
      if (this.SelectedSearchPlace == 1)
      {
        parentNode = (DocumentTreeNode) this.docControl.ActivePage;
        node2 = (DocumentTreeNode) this.docControl.ActivePage;
      }
      if (node1 != null)
      {
        if (node1 is TextData && node1.Id == this.curNodeId)
          pos = this.position;
        if (node1 is TextBoxElement textBoxElement && textBoxElement.TextBox != null)
        {
          TextSelection textSelection = textBoxElement.TextBox.GetTextSelection();
          pos = this.SearchDirrection == SearchDirrection.ToBegin ? textSelection.Position - 1 : textSelection.EndPosition;
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(this.curNodeId))
          node1 = this.docControl.Document.FindNode(this.curNodeId);
        pos = this.position;
        if (node1 == null && this.docControl.ActivePage != null)
          node1 = (DocumentTreeNode) this.docControl.ActivePage;
        if (node1 == null && this.docControl.Document.NodesCount > 0)
          node1 = this.docControl.Document.Nodes[0];
      }
    }
    if (node1 == null || parentNode == null)
      return;
    TextData textData1 = node1 as TextData;
    TextData textData2 = (TextData) null;
    int num1 = 0;
    int position = pos;
    int num2 = 0;
    bool flag1 = false;
    bool flag2 = true;
    int num3 = 0;
    while (!flag1 & flag2)
    {
      pos = textData1 == null ? -2 : this.FindText(textData1, pos);
      if (pos >= 0)
      {
        if (textData2 == null)
        {
          textData2 = textData1;
          num1 = pos;
        }
        else if (textData2 == textData1 && pos >= num1 && num3 > 0)
        {
          flag2 = false;
          continue;
        }
        bool updateUI = true;
        flag1 = true;
        if (oper == FindOperation.ReplaceAll)
        {
          updateUI = false;
          flag1 = false;
        }
        if (oper != FindOperation.Find)
        {
          if (!textData1.ReadOnlyNow)
          {
            this.ReplaceText(textData1, pos, updateUI);
            ++num2;
          }
          else
            flag1 = false;
        }
        position = pos;
        if (this.SearchDirrection != SearchDirrection.ToBegin)
          pos += this.FindWhat.Length;
        else
          --pos;
      }
      else
      {
        textData1 = this.GetNextTextNode(parentNode, node1);
        pos = -2;
        if (textData1 == null)
        {
          bool flag3 = false;
          if (this.SearchDirrection == SearchDirrection.EntireDocSearch || oper == FindOperation.ReplaceAll)
          {
            ++num3;
            if (num3 < 2)
              flag3 = true;
          }
          else
          {
            if (this.SearchDirrection == SearchDirrection.ToBegin && MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_610"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
              flag3 = true;
            if (this.SearchDirrection == SearchDirrection.ToEnd && MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_611"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
              flag3 = true;
          }
          if (flag3)
            textData1 = this.GetNextTextNode(parentNode, node2);
        }
        if (textData1 == null)
          flag2 = false;
        else
          node1 = (DocumentTreeNode) textData1;
      }
    }
    if (flag1 && textData1 != null)
    {
      Rectangle empty = Rectangle.Empty;
      this.curNodeId = textData1.Id;
      this.position = pos;
      this.docControl.SetSelection((DocumentTreeNode) textData1, true, Point.Empty, true, false);
      if (textData1 is TextBoxElement textBoxElement)
      {
        int length = this.FindWhat.Length;
        if (textBoxElement.TextBox != null && textBoxElement.InPlaceEditorActive)
          textBoxElement.TextBox.SetTextSelection(textBoxElement.PageUI, new TextSelection(position, length));
      }
    }
    if (oper == FindOperation.ReplaceAll)
      this.docControl.Document.UpdateLayout(false, true);
    if (!flag1)
    {
      if (oper == FindOperation.ReplaceAll)
      {
        if (num2 > 0)
        {
          int num4 = (int) MessageBox.Show($"{LocalizationHolder.rm.GetString("Document.Model_612")}{num2.ToString()}.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_613"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      else if (this.SearchDirrection == SearchDirrection.EntireDocSearch)
      {
        int num6 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_613"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    this.SearchDirrection = searchDirrection;
  }

  private bool ReplaceText(TextData textNode, int pos, bool updateUI)
  {
    string text = textNode.Text;
    TextBoxElement textBoxElement = textNode as TextBoxElement;
    bool flag = true;
    if (textBoxElement != null && textBoxElement.TextBox != null)
    {
      if (textBoxElement.Rtf != null)
      {
        flag = false;
        textBoxElement.TextBox.ActivateEditor(textBoxElement.PageUI, (MouseEventArgs) null, false);
        ImRtfEditor editor = textBoxElement.TextBox.Editor;
        editor.SelectTerText(-1, pos, pos + this.FindWhat.Length, -1, false);
        editor.TerDeleteBlock(false);
        int curFont = editor.TerGetCurFont(-1, -1);
        editor.TerInsertText(this.ReplaceWith, curFont, -1, false);
        editor.DeselectTerText(false);
        textBoxElement.AssignText(editor.PlaneText, editor.RtfText, updateUI, updateUI, updateUI);
      }
      else
        textBoxElement.TextBox.Invalidate();
    }
    if (flag)
    {
      string str1 = text.Replace("\n", "\r\n").Replace("\r\r", "\r");
      string str2 = str1.Remove(pos) + this.ReplaceWith + str1.Remove(0, pos + this.FindWhat.Length);
      textNode.AssignText(str2, false, true, true, updateUI, updateUI);
    }
    return true;
  }

  /// <summary>Поиск текста в элементе</summary>
  /// <param name="textNode"></param>
  /// <param name="pos"></param>
  /// <returns></returns>
  private int FindText(TextData textNode, int pos)
  {
    int startIndex = -1;
    int length1 = this.FindWhat.Length;
    string str = textNode.Text;
    bool flag = true;
    if (textNode is TextBoxElement textBoxElement && textBoxElement.TextBox != null && textBoxElement.Rtf != null)
      flag = false;
    if (flag)
      str = str.Replace("\n", "\r\n").Replace("\r\r", "\r");
    int length2 = str.Length;
    if (pos > length2)
      pos = length2;
    if (pos < 0)
    {
      if (this.SearchDirrection == SearchDirrection.ToBegin)
      {
        if (pos == -2)
          pos = length2 - 1;
      }
      else
        pos = 0;
    }
    if (this.SearchDirrection != SearchDirrection.ToBegin)
    {
      startIndex = !this.MatchCase ? str.IndexOf(this.FindWhat, pos, StringComparison.OrdinalIgnoreCase) : str.IndexOf(this.FindWhat, pos);
      if (startIndex != -1)
        str.Remove(startIndex);
    }
    else if (pos >= 0)
      startIndex = !this.MatchCase ? str.LastIndexOf(this.FindWhat, pos, StringComparison.OrdinalIgnoreCase) : str.LastIndexOf(this.FindWhat, pos);
    if (startIndex != -1 && this.MatchWholeWord)
    {
      int num = startIndex;
      int index1 = startIndex - 1;
      if (index1 >= 0 && !char.IsSeparator(str[index1]))
        num = -1;
      if (num != -1)
      {
        int index2 = startIndex + length1;
        if (index2 < str.Length && !char.IsSeparator(str[index2]))
          num = -1;
      }
      if (num == -1)
      {
        pos = this.SearchDirrection == SearchDirrection.ToBegin ? startIndex - 1 : startIndex + this.FindWhat.Length;
        startIndex = this.FindText(textNode, pos);
      }
    }
    return startIndex;
  }

  /// <summary>Поиск следуюзей текстовой строки</summary>
  /// <param name="parentNode"></param>
  /// <param name="node"></param>
  /// <returns></returns>
  private TextData GetNextTextNode(DocumentTreeNode parentNode, DocumentTreeNode node)
  {
    TextData nextTextNode = (TextData) null;
    for (DocumentTreeNode node1 = node; nextTextNode == null && node1 != null; nextTextNode = node1 as TextData)
      node1 = this.GetNextNode(parentNode, node1, true);
    return nextTextNode;
  }

  /// <summary>Получение след узла</summary>
  /// <param name="parentNode"></param>
  /// <param name="node"></param>
  /// <param name="getChild"></param>
  /// <returns></returns>
  private DocumentTreeNode GetNextNode(
    DocumentTreeNode parentNode,
    DocumentTreeNode node,
    bool getChild)
  {
    return node.Parent == parentNode.Parent && (!getChild || node.NodesCount == 0) ? (DocumentTreeNode) null : (this.SearchDirrection == SearchDirrection.ToEnd || this.SearchDirrection == SearchDirrection.EntireDocSearch ? (!(node.NodesCount > 0 & getChild) ? (node.Parent.NodesCount <= node.Index + 1 ? this.GetNextNode(parentNode, node.Parent, false) : node.Parent.Nodes[node.Index + 1]) : node.Nodes[0]) : (!(node.NodesCount > 0 & getChild) ? (node.Index <= 0 ? this.GetNextNode(parentNode, node.Parent, false) : node.Parent.Nodes[node.Index - 1]) : node.Nodes[node.NodesCount - 1]));
  }
}
