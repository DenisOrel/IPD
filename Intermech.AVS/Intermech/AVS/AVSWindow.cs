// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSWindow
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Infralution.Controls.VirtualTree;
using Intermech.AVS.AVSProperties;
using Intermech.AVS.AVSViews;
using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.Common_Dialogs.ArticleWithDocForm;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.AVS.NumberingPositions;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Окно AVS</summary>
public class AVSWindow : 
  ImDocumentEditorForm,
  IAvsWindow,
  IWindowWithFindAndReplace,
  IWindowWithFind,
  IMessageFilter,
  IOpenAsObjectSupport
{
  private EventUserControl eventUserControl;
  private IHotKeysManager _hotKeysManager;
  private bool lockOnActivated;
  private bool _objectIsSelected;
  private int _lockTreeColumnsSaveCounter;
  private static string _treeListColumnsLayout = string.Empty;
  /// <summary>Необходимо загрузить настройки столбцов табличного вида</summary>
  public bool NeedToLoadColumnParams = true;
  private int _loadColumnStateCounter;
  private FormFindOrReplaceTextInSpecification findController;
  private ServiceContainer _navigatorViewServices;
  private ISelectedItems navigatorMenuItems;
  private Dictionary<MenuButtonItem, object> navigatorMenuItemsHelpers;
  /// <summary>Выделенный раздел в спецификации</summary>
  private SpecificationSection selectedSection;
  private IContainer components;
  public Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList virtualTree;
  private Splitter _splitterBottom;
  protected static StatusBarPanel sbChapterPanel = (StatusBarPanel) null;
  private RowPropsDockControl rowPropertyGrid;
  private DockControlLayoutSettings rowpropertyGridSettings = new DockControlLayoutSettings();
  private bool workCompleteWaitMode;
  private bool restoreWorkCompleteMode;
  private AutoResetEvent workCompleteEvent;
  private long templateProductIDForCreation = -1;
  /// <summary>Заблокировать обработку сообщения DBRelationsEventArgsFromForm</summary>
  internal bool Suspended_DBRelationsEventArgsFromForm;
  /// <summary>Приостановлена обработка события ObjectsWasChangedHandler</summary>
  internal bool IsSuspended_ObjectsWasChangedHandler;
  private AVSDocument avsDocument;
  internal List<long> _relationIDsWithNeedToBeUpdated_FromNotificationService = new List<long>();
  internal Dictionary<int, List<long>> _relationIDsCreated_FromNotificationService = new Dictionary<int, List<long>>();
  internal List<long> _relationIDsRemoved_FromNotificationService = new List<long>();
  internal List<long> _objectIDsRemoved_FromNotificationService = new List<long>();
  internal Dictionary<string, ExternalAVSCommand> ExternalAVSCommands = new Dictionary<string, ExternalAVSCommand>();
  internal int _lockViewModeCounter;
  internal int _lockUpdateSelection;
  internal MenuBar menuBar;
  internal ContextMenuBarItem contextMenuBarItem;
  internal AVSViewMode viewMode;
  internal bool _activated;
  internal INavGraphicsCache _navGraphicsCache;
  internal ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Панель "Спецификации"</summary>
  protected Intermech.Bars.ToolBar specificationToolBar;
  /// <summary>Загружен ли плагин "Intermech.PDM.Server"</summary>
  internal bool _pdmServerLoaded;
  /// <summary>Ссылка на клиентский плагин "Intermech.Pdm"</summary>
  internal IPDMSubstitutesService _pdmClient;
  private ImageList _viewsImages;
  internal ViewSwitch viewSwitch1;
  private Panel _panelMain;
  private AVSWindow.enumBottomPanelType _bottomPanelType;
  private ProductPropertiesUserControl _productPropertiesUserControl;
  public Panel _panelBottom;

  [Browsable(false)]
  public AVSDocument AVSDocument
  {
    [DebuggerStepThrough] get => this.avsDocument;
    set
    {
      if (this.avsDocument == value)
        return;
      this.avsDocument = value;
      if (this.avsDocument == null)
        return;
      this.DocumentControl.SetDocument(this.avsDocument.Document, false, false);
      this.SetDocumentParams(this.avsDocument);
      this.avsDocument.AVSWindow = this;
    }
  }

  /// <summary>Установить параметры документа</summary>
  /// <param name="avsDocument">Документ AVS из которого нужно брать все параметры</param>
  internal void SetDocumentParams(AVSDocument avsDocument)
  {
    if (avsDocument != null)
      this.SetDocumentParams(avsDocument.DocumentID, avsDocument.DocumentGuid, avsDocument.DocumentDBObjectType, avsDocument.DocumentName, avsDocument.DocumentDesignation, avsDocument.DocumentCaption);
    else
      this.SetDocumentParams(-1L, Guid.Empty, -1, "", "", "");
  }

  /// <summary>Назначить DocumentControl</summary>
  /// <param name="value">Значение DocumentControl</param>
  public override void AssignDocumentControl(Intermech.Document.UI.DocumentControl value)
  {
    if (this.DocumentControl != null)
    {
      if (this.ContextMenuBarItem != null)
      {
        this.ContextMenuBarItem.BeforePopup -= new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
        this.ContextMenuBarItem.AfterPopup -= new EventHandler(this.contextMenuBarItem_AfterPopup);
      }
      this.DocumentControl.PreProcessCmdKey -= new PreProcessCmdKey_EventHandler(this.DocumentControl_PreProcessCmdKey);
    }
    base.AssignDocumentControl(value);
    if (this.DocumentControl == null)
      return;
    this.DocumentControl.PreProcessCmdKey += new PreProcessCmdKey_EventHandler(this.DocumentControl_PreProcessCmdKey);
    if (this.ContextMenuBarItem == null || this.virtualTree == null)
      return;
    (this.ContextMenuBarItem.ToolBar as MenuBar).SetPopupMenu((Control) this.virtualTree, (MenuBarItem) this.ContextMenuBarItem);
    this.ContextMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
    this.ContextMenuBarItem.AfterPopup += new EventHandler(this.contextMenuBarItem_AfterPopup);
  }

  internal ContextMenuBarItem ContextMenuBarItem
  {
    get
    {
      return this.DocumentControl != null && this.DocumentControl.PageControl != null ? this.DocumentControl.PageControl.ContextMenuBarItem : (ContextMenuBarItem) null;
    }
  }

  /// <summary>Обработчик события PreProcessCmdKey для DocumentControl</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DocumentControl_PreProcessCmdKey(object sender, PreProcessCmdKey_EventArgs e)
  {
    try
    {
      if (e.KeyData != Keys.Return)
        return;
      DocumentTreeNode selectedNode = this.DocumentControl.SelectedNode;
      if (!(selectedNode is TextBoxElement cell))
        return;
      AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(selectedNode);
      if (avsDocRow != null)
      {
        AvsRowAttributeInfo attributeInfoForCell = avsDocRow.GetAttributeInfoForCell((TextData) cell);
        if (attributeInfoForCell == null)
          return;
        if ((attributeInfoForCell.AttributeId == AvsIDCache.Attr_Format || attributeInfoForCell.AttributeId == AvsIDCache.Attr_Zone || attributeInfoForCell.AttributeId == AvsIDCache.Attr_Position) && !cell.AutoSizeHeight)
        {
          if (cell.PageUI is TableCellUI pageUi)
            pageUi.GotoNextSingleCell();
          e.Cancel = true;
        }
        if (AVSRow.IsCountField(attributeInfoForCell))
        {
          if (cell.PageUI is TableCellUI pageUi)
            pageUi.GotoNextSingleCell();
          e.Cancel = true;
        }
        if (e.Cancel || !attributeInfoForCell.EqualAttrs(this.avsDocument.Field_Name, false) || !cell.InPlaceEditorActive || !cell.ReadOnlyFormating)
          return;
        e.Cancel = true;
      }
      else
      {
        if (e.Cancel || !this.CanPasteSymbol())
          return;
        this.PasteSymbol('\u0015'.ToString());
        e.Cancel = true;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Получить текущий раздел спецификации</summary>
  /// <returns>Раздел спецификации</returns>
  public SpecificationSection GetSelectedSection()
  {
    SpecificationSection selectedSection = (SpecificationSection) null;
    if (this.viewMode == AVSViewMode.Page)
    {
      if (this.DocumentControl != null)
      {
        List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
        for (int index = 0; selectedSection == null && index < selectedNodes.Count; ++index)
          selectedSection = this.avsDocument.GetSection(selectedNodes[index]);
      }
    }
    else if (this.virtualTree != null && this.virtualTree.SelectedRows.Count > 0)
    {
      for (int index = 0; selectedSection == null && index < this.virtualTree.SelectedRows.Count; ++index)
        selectedSection = this.avsDocument.GetSection(this.virtualTree.SelectedRows[index]);
    }
    return selectedSection;
  }

  /// <summary>Получить текущий раздел спецификации</summary>
  /// <returns>Раздел спецификации</returns>
  public Chapter GetSelectedChapter(bool ignoreSections)
  {
    Chapter selectedChapter = (Chapter) null;
    if (this.viewMode == AVSViewMode.Page)
    {
      if (this.DocumentControl != null)
      {
        List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
        for (int index = 0; selectedChapter == null && index < selectedNodes.Count; ++index)
          selectedChapter = this.avsDocument.GetChapter(selectedNodes[index], ignoreSections);
      }
    }
    else if (this.virtualTree != null && this.virtualTree.SelectedRows.Count > 0)
    {
      for (int index = 0; selectedChapter == null && index < this.virtualTree.SelectedRows.Count; ++index)
        selectedChapter = this.avsDocument.GetChapter(this.virtualTree.SelectedRows[index]);
    }
    return selectedChapter;
  }

  /// <summary>Получить текущую строку спецификации</summary>
  /// <returns>Текущая строка спецификации</returns>
  public AVSRow GetSelectedSpecRow()
  {
    AVSRow selectedSpecRow = (AVSRow) null;
    long num = long.MinValue;
    if (this.viewMode == AVSViewMode.Page)
    {
      if (this.DocumentControl != null)
      {
        List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
        for (int index = 0; index < selectedNodes.Count; ++index)
        {
          if (selectedNodes[index] != null && selectedNodes[index].OwnerDocument != null)
          {
            AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(selectedNodes[index]);
            if (avsDocRow != null && avsDocRow.Section != null && (selectedSpecRow == null || avsDocRow.SortIndex <= num))
            {
              num = avsDocRow.SortIndex;
              selectedSpecRow = avsDocRow;
            }
          }
        }
      }
    }
    else if (this.virtualTree != null && this.virtualTree.SelectedRows.Count > 0)
    {
      for (int index = 0; index < this.virtualTree.SelectedRows.Count; ++index)
      {
        AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(this.virtualTree.SelectedRows[index]);
        if (avsDocRow != null && (selectedSpecRow == null || avsDocRow.SortIndex <= num))
        {
          num = avsDocRow.SortIndex;
          selectedSpecRow = avsDocRow;
        }
      }
    }
    return selectedSpecRow;
  }

  public bool CheckErrors(bool runOnClose = false)
  {
    bool flag = false;
    AVSCheckType checkType = !runOnClose ? AVSCheckType.EmptyCountOrWithoutRelation | AVSCheckType.EmptyPosition | AVSCheckType.ImBase | AVSCheckType.DuplicatePosition | AVSCheckType.NotNumberPosition | AVSCheckType.CheckDuplicatePositionDesignation | AVSCheckType.EmptyPositionDesignation | AVSCheckType.EmptyCountAllProdFormB | AVSCheckType.PartWithoutDraft | AVSCheckType.DraftCountDoesntMatch | AVSCheckType.MissingOutputMappingForNote : (this.AVSDocument.IsSpecification ? AvsConfig.CheckSP.EnabledChecks : (this.AVSDocument.IsElementList ? AvsConfig.CheckEL.EnabledChecks : AVSCheckType.None));
    Dictionary<AVSRow, List<SpecRowCheckMessage>> dictionary = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
    this.AVSDocument.CheckErrorsInRows(checkType, AVSCheckMode.CheckErrors, (ICollection<AVSRow>) null, dictionary);
    if (dictionary.Count > 0)
    {
      this.DocumentControl.SetSelection((DocumentTreeNode) dictionary.Keys.First<AVSRow>().DocNode, true, false);
      this.Show(this.DockManager, DockState.Document);
      this.Select();
      this.ErrorsUserControl.Show(AVSRowErrorMessage.CreateMessages(dictionary));
      if (runOnClose)
      {
        if (this.restoreWorkCompleteMode)
        {
          this.restoreWorkCompleteMode = false;
          this.EnableWorkCompleteMode();
        }
        if (IMMessageBox.ShowEx("Внимание!", "В текущем документе обнаружены ошибки.", new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Все равно закрыть", DialogResultAdv.Ignore),
          new IMMessageBoxButton("Не закрывать", DialogResult.Cancel)
        }) is IMMessageBoxButton messageBoxButton && messageBoxButton.MessageResultAdv == DialogResultAdv.Ignore)
        {
          flag = true;
          this.DisableCompleteWaitMode();
        }
      }
    }
    else
    {
      this.ErrorsUserControl.Close();
      if (!runOnClose)
      {
        int num = (int) MessageBox.Show("Ошибок не обнаружено", "Проверка спецификации");
      }
    }
    return dictionary.Count == 0 | flag;
  }

  /// <summary>Добавить дополнительную часть</summary>
  /// <param name="context">Контекст добавления</param>
  public void AddAdditionalChapter(DocumentTreeNode[] context)
  {
    try
    {
      List<Chapter> chapterList1 = new List<Chapter>();
      if (this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single && !this.avsDocument.IsFormB)
      {
        if (this.avsDocument.AdditionalChaptersInDataChapter)
        {
          if (context == null)
            return;
          context = DocumentTreeNode.GetNodesWithoutChilds(context);
          for (int index = 0; index < context.Length; ++index)
          {
            Chapter chapterForSection = this.avsDocument.GetChapterForSection(context[index]);
            if (chapterForSection != null)
            {
              if (chapterForSection.IsCommonDataChapter || chapterForSection.IsVariableDataChapter && this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
                chapterList1.Add(chapterForSection);
              else if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A && chapterForSection.IsVariableDataChapter)
              {
                if (chapterForSection.Chapters.Count > 0)
                  chapterList1.Add(chapterForSection.Chapters[0]);
              }
              else if (chapterForSection is ProductVariableDataChapter)
                chapterList1.Add(chapterForSection);
              else if (chapterForSection.Parent != null && chapterForSection.Parent is ProductVariableDataChapter)
                chapterList1.Add(chapterForSection.Parent);
            }
          }
        }
      }
      else
        chapterList1.Add(this.avsDocument.CommonDataChapter);
      SelectChapterDlg selectChapterDlg = new SelectChapterDlg(this.avsDocument.AVSCommonPropertiesSchema.AdditionalChapters, this.avsDocument.DocumentDesignation);
      selectChapterDlg.Multiselect = false;
      AdditionalChapterSettings chapterSettings = (AdditionalChapterSettings) null;
      if (selectChapterDlg.ShowDialog() == DialogResult.OK)
        chapterSettings = selectChapterDlg.GetSelectedChapter();
      if (chapterSettings == null)
        return;
      this.avsDocument.SuspendDocumentAndGridUpdates();
      int fromPage = -1;
      List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
      List<Chapter> chapterList2 = new List<Chapter>();
      Chapter chapter1 = (Chapter) null;
      bool flag = false;
      try
      {
        if (chapterList1.Count > 0)
        {
          for (int index = 0; index < chapterList1.Count; ++index)
          {
            Chapter chapter2 = chapterList1[index].GetChapter(chapterSettings.ChapterGuid);
            if (chapter2 == null)
            {
              flag = true;
              chapterList1[index].AddChapter(chapter2 = (Chapter) new AdditionalChapter(this.avsDocument, chapterSettings, this.avsDocument.AdditionalChaptersInDataChapter), true, true, this.viewMode == AVSViewMode.Grid, (TableData) null);
            }
            chapterList2.Add(chapter2);
          }
        }
        else
        {
          if (this.avsDocument.AdditionalChaptersInDataChapter)
          {
            chapter1 = this.avsDocument.commonDataChapter.GetChapter(chapterSettings.ChapterGuid);
            if (chapter1 == null)
            {
              flag = true;
              this.avsDocument.commonDataChapter.AddChapter(chapter1 = (Chapter) new AdditionalChapter(this.avsDocument, chapterSettings, this.avsDocument.AdditionalChaptersInDataChapter), true, true, this.viewMode == AVSViewMode.Grid, (TableData) null);
            }
          }
          else
          {
            for (int index = 0; index < this.avsDocument.rootChapters.Count; ++index)
            {
              if (this.avsDocument.rootChapters[index].ChapterGuid == chapterSettings.ChapterGuid)
              {
                chapter1 = this.avsDocument.rootChapters[index];
                break;
              }
            }
            if (chapter1 == null)
            {
              this.avsDocument.AddRootChapter(chapter1 = (Chapter) new AdditionalChapter(this.avsDocument, chapterSettings, this.avsDocument.AdditionalChaptersInDataChapter), true);
              flag = true;
            }
          }
          chapterList2.Add(chapter1);
        }
        if (!flag)
          return;
        this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
      }
      finally
      {
        this.avsDocument.ResumeDocumentAndGridUpdates(fromPage, flag, flag, true, true);
        for (int index = 0; index < chapterList2.Count; ++index)
        {
          if (chapterList2[index].DocNode != null)
            selection.Add((DocumentTreeNode) chapterList2[index].DocNode);
        }
        if (selection.Count > 0)
          this.DocumentControl.SetSelection(selection, true, false);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Добавить разделы спецификации</summary>
  public void AddSpecSections(DocumentTreeNode[] context, List<long> selectedSectionIDs)
  {
    try
    {
      if (context == null)
        return;
      context = DocumentTreeNode.GetNodesWithoutChilds(context);
      List<Chapter> chapterList1 = new List<Chapter>();
      bool flag = false;
      for (int index = 0; index < context.Length; ++index)
      {
        Chapter chapterForSection = this.avsDocument.GetChapterForSection(context[index]);
        if (chapterForSection != null)
        {
          if (chapterForSection.IsCommonDataChapter || chapterForSection.IsProductVariableDataChapter)
            chapterList1.Add(chapterForSection);
          else if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A && chapterForSection.IsVariableDataChapter)
          {
            if (chapterForSection.Chapters.Count > 0)
              chapterList1.Add(chapterForSection.Chapters[0]);
            flag = true;
          }
          else
          {
            switch (chapterForSection)
            {
              case ProductVariableDataChapter _:
                chapterList1.Add(chapterForSection);
                flag = true;
                continue;
              case VariableDataChapterFormV _:
                chapterList1.Add(chapterForSection);
                flag = true;
                continue;
              case AdditionalChapter _:
                if (!chapterForSection.IsSectionOwner || chapterForSection.Parent == null)
                {
                  if (chapterForSection.Chapters.Count > 0 && !(chapterForSection.Chapters[0] is SpecificationSection))
                  {
                    chapterList1.Add(chapterForSection.Chapters[0]);
                    continue;
                  }
                  chapterList1.Add(chapterForSection);
                  continue;
                }
                chapterList1.Add(chapterForSection);
                continue;
              default:
                continue;
            }
          }
        }
      }
      if (selectedSectionIDs == null)
      {
        SelectSectionForm selectSectionForm = new SelectSectionForm(this.avsDocument.GetAllowableDocumentSections());
        if (selectSectionForm.ShowDialog() == DialogResult.OK)
          selectedSectionIDs = selectSectionForm.GetSelectedSectionIDs();
      }
      if (selectedSectionIDs == null || selectedSectionIDs.Count <= 0)
        return;
      if (chapterList1.Count == 0)
      {
        if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
        {
          TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
          if (selectCharterForm.ShowDialog() != DialogResult.OK)
            return;
          List<ProductInfo> selectedProducts = selectCharterForm.GetSelectedProducts();
          if (selectedProducts.Count == 0 || selectedProducts.Count == 1 && selectedProducts[0].IsCommonData)
            chapterList1.Add(this.avsDocument.commonDataChapter);
          else if (this.avsDocument.variableDataChapter_FormA != null)
          {
            for (int index = 0; index < selectedProducts.Count; ++index)
            {
              if (selectedProducts[index].IsCommonData)
              {
                chapterList1.Add(this.avsDocument.commonDataChapter);
              }
              else
              {
                Chapter productChapter = this.avsDocument.variableDataChapter_FormA.GetProductChapter(selectedProducts[index]);
                if (productChapter != null)
                {
                  chapterList1.Add(productChapter);
                  flag = true;
                }
              }
            }
          }
          else if (this.avsDocument.variableDataChapter_FormV != null)
            chapterList1.Add((Chapter) this.avsDocument.variableDataChapter_FormV);
          else
            chapterList1.Add(this.avsDocument.commonDataChapter);
        }
        else
          chapterList1.Add(this.avsDocument.commonDataChapter);
      }
      this.avsDocument.SuspendDocumentAndGridUpdates();
      int fromPage = -1;
      List<Chapter> chapterList2 = new List<Chapter>();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          SpecificationSectionInfo.UpdateCacheSpecSections(sessionKeeper.Session, (IList<long>) null);
          for (int index1 = 0; index1 < selectedSectionIDs.Count; ++index1)
          {
            try
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(selectedSectionIDs[index1]);
              for (int index2 = 0; index2 < chapterList1.Count; ++index2)
              {
                if (!(chapterList1[index2].GetChapter(dbObject.ObjectID) is SpecificationSection chapter))
                {
                  SpecificationSection section = this.avsDocument.CreateSection(dbObject.ObjectID);
                  chapterList1[index2].AddChapter((Chapter) section, true, true, this.viewMode == AVSViewMode.Grid, chapterList1[index2].GetSectionTemplate());
                  if (flag && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
                  {
                    if (chapterList1[index2].Parent != null)
                      chapterList1[index2].Parent.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
                    else if (this.avsDocument.variableDataChapter_FormA != null)
                      this.avsDocument.variableDataChapter_FormA.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
                    flag = false;
                  }
                  if (section.HasDocNodes)
                    chapterList2.Add((Chapter) section);
                }
                else if (flag && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
                {
                  if (chapterList1[index2].Parent != null)
                    chapterList1[index2].Parent.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
                  else if (this.avsDocument.variableDataChapter_FormA != null)
                    this.avsDocument.variableDataChapter_FormA.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
                  flag = false;
                }
                else
                  chapter.Parent.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, true, false, EmptyRowUpdateMode.DontChange);
              }
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
        }
      }
      finally
      {
        this.avsDocument.UpdateSkipLines(false, false);
        this.avsDocument.UpdateVariableDataCaptions();
        this.avsDocument.ResumeDocumentAndGridUpdates(fromPage, true, true, true, true);
        if (chapterList2.Count > 0)
        {
          this.DocumentControl.SetSelection((DocumentTreeNode) chapterList2[0].DocNode, true, false);
          if (this.ViewMode == AVSViewMode.Grid)
            this.virtualTree.SelectedItem = (object) chapterList2[0];
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Вставить копию записи примечания</summary>
  /// <param name="context">Контекст вставки</param>
  /// <param name="srcNoteDocRow">Оригинал записи примечания</param>
  /// <param name="updateDoc">Обновить разбивку документа</param>
  /// <param name="selectNewRow">Выбрать вставленную запись</param>
  public AVSRow InsertCopyNoteDocRow(
    DocumentTreeNode context,
    TableData srcNoteDocRow,
    bool updateDoc,
    bool selectNewRow)
  {
    if (srcNoteDocRow == null)
      throw new ArgumentNullException(nameof (srcNoteDocRow));
    AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(context);
    if (contextChapters.Section == null)
      return (AVSRow) null;
    TableData tableData = (TableData) null;
    if (contextChapters.RowIndex == -1)
      contextChapters.RowIndex = contextChapters.Section.Rows.Count;
    string templateId = srcNoteDocRow.TemplateId;
    TableData rowTemplate = this.avsDocument.Document.Template.FindNode(srcNoteDocRow.TemplateId) as TableData;
    bool flag = AVSDocument.IsNoteRowDocNode((DocumentTreeNode) srcNoteDocRow);
    if (rowTemplate == null)
      rowTemplate = !flag ? this.avsDocument.avsRowTemplate : this.avsDocument.note1Template;
    if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.V && contextChapters.Section.IsFormB)
    {
      TableData nameVarDataFormV = this.avsDocument.FindNoteTemplateByName_VarDataFormV(rowTemplate.Name);
      if (nameVarDataFormV != null)
        rowTemplate = nameVarDataFormV;
    }
    if (rowTemplate != null)
      tableData = this.avsDocument.CreateNoteDocRow(rowTemplate, (string) null);
    if (tableData == null)
      tableData = (TableData) srcNoteDocRow.Clone();
    AVSDocument.SetupDocNodeAsNoteRow((DocumentTreeNode) tableData);
    AVSRow.CopyDataFromToDocRow(srcNoteDocRow, tableData);
    return this.avsDocument.InsertNoteDocRow(contextChapters, tableData, updateDoc, selectNewRow);
  }

  /// <summary>Расширение по умолчанию</summary>
  public override string DefaultFileExtension
  {
    [DebuggerStepThrough] get
    {
      return this.avsDocument != null ? this.avsDocument.DefaultFileExtension : base.DefaultFileExtension;
    }
    set => base.DefaultFileExtension = value;
  }

  /// <summary>Спросить у пользователя сохранять ли изменения в документе перед закрытием окна</summary>
  public override bool AskForSaveBeforeClose
  {
    get
    {
      return this.avsDocument != null && !this.avsDocument.IsSpecification || base.AskForSaveBeforeClose;
    }
    set => base.AskForSaveBeforeClose = value;
  }

  /// <summary>Открытый документ в окне является спецификацией</summary>
  public bool IsSpecification => this.avsDocument != null && this.avsDocument.IsSpecification;

  public bool IsElementList => this.avsDocument != null && this.avsDocument.IsElementList;

  internal bool DocumentDBObjectWasRemoved { get; set; }

  private void virtualTree_SelectionChanged(object sender, EventArgs e)
  {
    try
    {
      this.LockUpdateSelection();
      if (this.viewMode != AVSViewMode.Grid || this.IsViewModeLocked())
        return;
      if (!this.IsViewModeLocked())
        this.DocumentControl.SetSelection(this.GetDocumentNodeList(), true, false);
      this.UpdateNavigatorMenu(false);
      this.UpdateISimpleSelectedItemsService();
      AVSPlugin.Instance.SelectionChanged();
      if (this.BottomPanelType == AVSWindow.enumBottomPanelType.None)
        return;
      this.UpdateProductPropertiesPanel();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this.UnlockUpdateSelection();
    }
  }

  private void virtualTree_FocusedCellChanged(object sender, EventArgs e)
  {
    try
    {
      this.LockUpdateSelection();
      if (this.viewMode != AVSViewMode.Grid || this.IsViewModeLocked())
        return;
      if (!this.IsViewModeLocked())
        this.DocumentControl.SetSelection(this.GetDocumentNodeList(), true, false);
      this.UpdateNavigatorMenu(false);
      AVSPlugin.Instance.SelectionChanged();
      if (this.BottomPanelType == AVSWindow.enumBottomPanelType.None)
        return;
      this.UpdateProductPropertiesPanel();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this.UnlockUpdateSelection();
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">DocumentManager</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="readOnly">Режим только для чтения</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  /// <param name="restoreParams">Список ранее сохранёных параметров для восстановления окна</param>
  /// <param name="externalCommands">Массив внешних команд, которые можно вызвать из окна</param>
  public AVSWindow(
    IImDocumentManager documentManager,
    int objectType,
    long objectId,
    bool readOnly,
    HybridDictionary restoreParams = null,
    bool? createUndo = null,
    ExternalAVSCommand[] externalCommands = null)
    : this(documentManager, readOnly, restoreParams, externalCommands)
  {
    this.AVSDocument = new AVSDocument(this, objectType, objectId, readOnly, createUndo);
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">DocumentManager</param>
  /// <param name="avsDocument">Конструкторский документ</param>
  /// <param name="readOnly">Режим только для чтения</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  /// <param name="restoreParams">Список ранее сохранёных параметров для восстановления окна</param>
  /// <param name="externalCommands">Массив внешних команд, которые можно вызвать из окна</param>
  public AVSWindow(
    IImDocumentManager documentManager,
    AVSDocument avsDocument,
    bool readOnly,
    HybridDictionary restoreParams = null,
    ExternalAVSCommand[] externalCommands = null)
    : this(documentManager, readOnly, restoreParams, externalCommands)
  {
    this.AVSDocument = avsDocument;
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">DocumentManager</param>
  /// <param name="readOnly">Режим только для чтения</param>
  /// <param name="createUndo">Создавать данные для восстановления</param>
  /// <param name="restoreParams">Список ранее сохранёных параметров для восстановления окна</param>
  /// <param name="externalCommands">Массив внешних команд, которые можно вызвать из окна</param>
  private AVSWindow(
    IImDocumentManager documentManager,
    bool readOnly,
    HybridDictionary restoreParams,
    ExternalAVSCommand[] externalCommands)
    : this(documentManager, readOnly)
  {
    this.RestoreWindowProperties(restoreParams);
    if (this.DocTreeViewDlg != null && this.DocTreeViewDlg.Visible)
      this.DocTreeViewDlg.TreeRoot = (DocumentTreeNode) this.Document;
    this.DocumentControl.DocumentModifiedChanged += new ModifiedChanged_EventHandler(DocumentEditorPlugin.Instance.DocumentModifiedChanged);
    this.DocumentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
    if (this.ReadOnly)
    {
      this.DocumentControl.ViewsSwitch.ViewsCaptions = new string[0];
      this.DocumentControl.ViewsSwitch.ViewsHints = new string[0];
    }
    else
    {
      this.DocumentControl.ViewsSwitch.ViewsCaptions = new string[2]
      {
        "Страничный",
        "Табличный"
      };
      this.DocumentControl.ViewsSwitch.ViewsHints = new string[2]
      {
        "Перейти к страничному виду спецификации",
        "Перейти к табличному виду спецификации"
      };
    }
    this.DocumentControl.ViewsSwitch.ImageList = this._viewsImages;
    this.DocumentControl.ViewsSwitch.OnActivePageChanged += new EventHandler(this.ViewsSwitch_OnActivePageChanged);
    this.DocumentControl.HSPanel.Anchor = AnchorStyles.None;
    this.DocumentControl.HSPanel.SetBounds(0, this.DocumentControl.Height - this.DocumentControl.hScrollBar.Height, this.DocumentControl.Width - this.DocumentControl.hScrollBar.Height, this.DocumentControl.hScrollBar.Height);
    this.DocumentControl.HSPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.InitializePDMPlugin();
    AVSPlugin.Instance.CheckPDMPlugins();
    if (externalCommands != null)
    {
      for (int index = 0; index < externalCommands.Length; ++index)
      {
        if (this.CommandManager.FindCommand(externalCommands[index].CommandName) == null)
        {
          if (externalCommands[index].MenuItem == null)
          {
            externalCommands[index].MenuItem = (MenuItemBase) DocumentMenuHelper.CreateMenuItem(externalCommands[index].CommandName, externalCommands[index].Caption, externalCommands[index].Hint, false, false, this.CommandManager);
            if (externalCommands[index].MenuItem != null)
              ((BarManager) ServicesManager.GetService(typeof (BarManager)))?.MenuBar.FindMenuBar("AVS").Items.Add((ToolbarItemBase) externalCommands[index].MenuItem);
          }
          else
            this.CommandManager.Add((ButtonItemBase) externalCommands[index].MenuItem);
        }
        if (!this.ExternalAVSCommands.ContainsKey(externalCommands[index].CommandName))
          this.ExternalAVSCommands.Add(externalCommands[index].CommandName, externalCommands[index]);
      }
    }
    this.DocumentControl.ReadOnly = this.ReadOnly;
    this.DocumentControl.RowSelection = true;
  }

  protected AVSWindow(IImDocumentManager documentManager, bool readOnly)
    : base(documentManager, true, readOnly)
  {
    this.Guid = DocumentEditorPlugin.AVSWindowGuid;
    this.InitializePDMPlugin();
    AVSPlugin.Instance.CheckPDMPlugins();
    this.virtualTree = new Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList();
    this.virtualTree.AVSWindow = this;
    this.virtualTree.SelectionChanged += new EventHandler(this.virtualTree_SelectionChanged);
    this.virtualTree.FocusedCellChanged += new EventHandler(this.virtualTree_FocusedCellChanged);
    this.DefaultFileExtension = ".spx";
    this.SetBaseEditCommandsEnabled(false, false);
    this.AskForSaveBeforeClose = false;
    this.updateReferenceByNotificationService = false;
    this.InitializeComponent();
    if (this.ContextMenuBarItem != null)
    {
      (this.ContextMenuBarItem.ToolBar as MenuBar).SetPopupMenu((Control) this.virtualTree, (MenuBarItem) this.ContextMenuBarItem);
      this.ContextMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
      this.ContextMenuBarItem.AfterPopup += new EventHandler(this.contextMenuBarItem_AfterPopup);
    }
    this.DocumentControl.RowSelection = true;
    this.DocumentControl.ReadOnlyGeometryForDocument = true;
    this.DocumentControl.SelectionChanged += new SelectionChanged_EventHandler(this.DocControl_SelectionChanged);
    this.DocumentControl.BeforeSelectionChanged += new BeforeSelectionChanged_EventHandler(this.DocumentControl_BeforeSelectionChanged);
    this.DocumentControl.CanShiftSelect += new CanShiftSelect_EventHandler(this.DocumentControl_CanShiftSelect);
    this.DocumentControl.SelectionChanged += new SelectionChanged_EventHandler(this.DocumentControl_SelectionChanged);
    this.DocumentControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.DocumentControl_ActivePageChanged);
    this.DocumentControl.RowSelectionEvent += new RowSelection_EventHandler(this.DocumentControl_RowSelectionEvent);
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.workCompleteWaitMode = false;
    this.workCompleteEvent = new AutoResetEvent(false);
    AvsConfig.General.Changed += new EventHandler(this.AvsConfig_Changed);
  }

  private void DocumentControl_RowSelectionEvent(object sender, RowSelection_EventArgs e)
  {
    bool flag = true;
    foreach (DocumentTreeNode node in e.Nodes)
    {
      if (node is TextData)
      {
        if (!AVSRow.IsCountFormBCell(true, node as TextData))
          flag = false;
      }
      else
        flag = false;
    }
    if (!flag)
      return;
    e.RowSelection = new bool?(false);
  }

  internal AvsRowEventMessageViewer AvsRowEventMessageViewer
  {
    get => this.avsDocument.AvsRowEventMessageViewer;
  }

  protected override DockControl GetDockControl(Guid guid, string persistString, string text)
  {
    if (guid == RowPropsDockControl.DockGuid)
    {
      this.ShowRowPropertyGrid(false);
      return (DockControl) this.rowPropertyGrid;
    }
    return guid == EventUserControl.DockGuid ? (DockControl) null : base.GetDockControl(guid, persistString, text);
  }

  public EventUserControl EventUserControl
  {
    get
    {
      if (this.eventUserControl == null)
        this.CreateEventUserControl(false);
      return this.eventUserControl;
    }
  }

  public void CreateEventUserControl(bool show)
  {
    if (this.eventUserControl == null)
    {
      this.eventUserControl = new EventUserControl(this, (AvsRowEventMessageViewer) null);
      this.DockManagerStorage.SetControl((DockControl) this.eventUserControl);
    }
    if (!show || this.eventUserControl.DockContainer != null)
      return;
    this.DockManagerStorage.GetSettings((DockControl) this.EventUserControl).Open((DockControl) this.EventUserControl, this.DockManager);
  }

  protected override void LoadControlsConfigurationOld()
  {
    if (AVSPlugin.Instance != null)
    {
      IConfigurationManager configurationManager = AVSPlugin.Instance.ConfigurationManager;
      if (configurationManager != null)
      {
        IConfiguration config = configurationManager.Open("AVS");
        if (config != null)
          this.rowpropertyGridSettings = DockControlLayoutSettings.GetSettings(config, "RowPropertyGrid");
      }
    }
    if (this.rowpropertyGridSettings.Opened && (this.rowpropertyGridSettings.Visible || this.rowpropertyGridSettings.DockLocation != DockLocation.Float))
      this.ShowRowPropertyGrid(true);
    base.LoadControlsConfigurationOld();
  }

  private void AvsConfig_Changed(object sender, EventArgs e)
  {
    if (!AvsConfig.General.NoteFieldSettingsIsChanged)
      return;
    this.avsDocument.UpdateNoteDocCells(false, true);
    AvsConfig.General.NoteFieldSettingsIsChanged = false;
  }

  /// <summary> Восстановление настроек окна во время открытия IPS </summary>
  internal void RestoreWindowProperties(HybridDictionary props)
  {
  }

  /// <summary>
  /// Выполнить проверку наличия модуля "Intermech.Pdm.Server" на сервере приложений
  /// </summary>
  protected void InitializePDMPlugin()
  {
    this._pdmClient = ServicesManager.GetService(typeof (IPDMSubstitutesService)) as IPDMSubstitutesService;
    if (this._pdmClient == null)
      throw new Exception("Не загружен модуль PDM, необходимый для полноценной работы AVS");
  }

  /// <summary>Получение списка выбранных записей </summary>
  /// <returns>Список выбранных записей </returns>
  public List<AVSRow> GetSelectedSpecRows(bool updateQueryCache)
  {
    List<AVSRow> selectedSpecRows = new List<AVSRow>(0);
    if (this.viewMode == AVSViewMode.Page)
    {
      if (this.DocumentControl != null)
      {
        List<DocumentTreeNode> nodes;
        if (updateQueryCache)
        {
          this._IsQueryChacheIsInit = false;
          this.InitQueryCache();
          if (this._queryDocumentNodeList == null || this._queryDocumentNodeList.Count == 0)
            this._queryDocumentNodeList = this.DocumentControl.SelectedNodes;
          nodes = this._queryDocumentNodeList;
        }
        else
          nodes = this.DocumentControl.SelectedNodes;
        selectedSpecRows = nodes == null ? new List<AVSRow>(0) : this.GetSpecRowsFromNodes(nodes);
      }
    }
    else if (this.virtualTree != null && this.virtualTree.SelectedRows.Count > 0)
    {
      for (int index = 0; index < this.virtualTree.SelectedRows.Count; ++index)
      {
        AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(this.virtualTree.SelectedRows[index]);
        if (avsDocRow != null)
          selectedSpecRows.Add(avsDocRow);
      }
    }
    return selectedSpecRows;
  }

  /// <summary>Получить список выделенных примечаний</summary>
  /// <returns></returns>
  public List<DocumentTreeNode> GetSelectedNoteRows()
  {
    List<DocumentTreeNode> selectedNoteRows = new List<DocumentTreeNode>();
    if (this.DocumentControl.SelectedNodes != null)
    {
      for (int index = 0; index < this.DocumentControl.SelectedNodes.Count; ++index)
      {
        DocumentTreeNode parentNoteRowDocNode = AVSDocument.FindParentNoteRowDocNode(this.DocumentControl.SelectedNodes[index]);
        if (parentNoteRowDocNode != null)
          selectedNoteRows.Add(parentNoteRowDocNode);
      }
    }
    return selectedNoteRows;
  }

  /// <summary>Получить записи спецификации для узлов документа</summary>
  /// <param name="nodes">Список узлов документа</param>
  /// <returns>Список записей спецификации</returns>
  public List<AVSRow> GetSpecRowsFromNodes(List<DocumentTreeNode> nodes)
  {
    List<AVSRow> specRows = nodes != null ? new List<AVSRow>(nodes.Count) : throw new ArgumentNullException(nameof (nodes));
    if (this.avsDocument != null)
    {
      for (int index = 0; index < nodes.Count; ++index)
      {
        if (nodes[index] != null)
          this.avsDocument.GetSpecRows(nodes[index], specRows);
      }
    }
    return specRows;
  }

  /// <summary>Получить записи спецификации для узлов документа</summary>
  /// <param name="nodes">Список узлов документа</param>
  /// <returns>Список записей спецификации</returns>
  public List<AVSRow> GetSpecRowsFromNodes(DocumentTreeNode[] nodes)
  {
    List<AVSRow> specRows = nodes != null ? new List<AVSRow>(nodes.Length) : throw new ArgumentNullException(nameof (nodes));
    if (this.avsDocument != null)
    {
      for (int index = 0; index < nodes.Length; ++index)
      {
        if (nodes[index] != null)
          this.avsDocument.GetSpecRows(nodes[index], specRows);
      }
    }
    return specRows;
  }

  /// <summary>Проверить есть ли связь относящаяся к выделенным элементам документа</summary>
  /// <returns></returns>
  public bool CheckSelectedRelations()
  {
    List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
    if (selectedNodes != null)
    {
      for (int index = 0; index < selectedNodes.Count; ++index)
      {
        if (this.avsDocument.CheckRelationsInDocNode(selectedNodes[index]))
          return true;
      }
    }
    return false;
  }

  /// <summary>Получить список исполнений к которым относятся выделенные ячейки документа</summary>
  /// <returns></returns>
  public List<ProductInfo> GetSelectedProducts()
  {
    List<ProductInfo> products = new List<ProductInfo>();
    if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
    {
      List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
      if (selectedNodes != null)
      {
        for (int index = 0; index < selectedNodes.Count; ++index)
          this.avsDocument.GetProductsForDocNode(selectedNodes[index], products);
      }
    }
    if (!this.avsDocument.IsSpecification)
    {
      List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
      if (selectedNodes != null)
      {
        for (int index = 0; index < selectedNodes.Count; ++index)
        {
          DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(selectedNodes[index]);
          if (productVariableDocNode != null)
          {
            Chapter tag = (productVariableDocNode as TableData).Tag as Chapter;
            ProductVariableDataChapter variableDataChapter = (ProductVariableDataChapter) null;
            if (tag is ProductVariableDataChapter)
              variableDataChapter = tag as ProductVariableDataChapter;
            if (tag != null && tag.Parent is ProductVariableDataChapter)
              variableDataChapter = tag.Parent as ProductVariableDataChapter;
            if (variableDataChapter != null)
              products.Add(variableDataChapter.Product);
          }
        }
      }
    }
    return products;
  }

  /// <summary>Получить список исполнений к которым относятся выделенные ячейки документа</summary>
  /// <param name="onlyHeaders">true рассматривать только ячейки которые сами входят в заголовок исполнения</param>
  /// <returns></returns>
  public List<long> GetSelectedProducts(bool onlyHeaders)
  {
    List<long> selectedProducts = new List<long>();
    if (this.DocumentControl != null)
    {
      foreach (DocumentTreeNode selectedNode in this.DocumentControl.SelectedNodes)
      {
        DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(selectedNode);
        if (productVariableDocNode != null && (productVariableDocNode as TableData).Tag is ProductVariableDataChapter tag && !tag.ChapterID.IsUndefinedId())
          selectedProducts.Add(tag.ChapterID);
      }
    }
    return selectedProducts;
  }

  /// <summary>Сформировать текст заголовка окна документа</summary>
  public override string FormatDocWindowCaption()
  {
    if (this.avsDocument != null && !this.avsDocument.IsSpecification)
      return base.FormatDocWindowCaption();
    bool flag = false;
    if (this.Document != null)
      flag = this.Document.IsTemplate;
    string str = this.DocumentCaption;
    if (str.Length > 70)
      str = str.Substring(0, 67) + "...";
    return str + (flag ? " [Шаблон]" : "") + (this.ReadOnly ? " (только чтение)" : "");
  }

  public override string DocumentCaption
  {
    get => this.avsDocument != null ? this.avsDocument.DocumentCaption : base.DocumentCaption;
    set => base.DocumentCaption = value;
  }

  public override string DefaultFileName
  {
    get => this.avsDocument != null ? this.avsDocument.DefaultFileName : base.DefaultFileName;
    set => base.DefaultFileName = value;
  }

  /// <summary>Обработчик события загрузки окна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void AVSWindow_Load(object sender, EventArgs e)
  {
    try
    {
      this.AssignTreeListEvents();
      this.DocumentControl.ViewsSwitch.Visible = true;
      this.viewSwitch1.Visible = false;
      this.DocumentControl.ResumeLayout(true);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Текущий вид - табличный или страничный</summary>
  public AVSViewMode ViewMode
  {
    [DebuggerStepThrough] get => this.viewMode;
  }

  public event EventHandler ViewModeSwitched;

  public void UpdateGridMode()
  {
    if (this.IsViewModeLocked() || this.ViewMode != AVSViewMode.Grid)
      return;
    this.LoadColumnsStateIfNeeded();
    this.avsDocument.RecreateTreeListNodes();
  }

  private void PasteSymbol(string symbol)
  {
    if (!(this.DocumentControl.SelectedNode is TextBoxElement selectedNode) || !selectedNode.InPlaceEditorActive || !(selectedNode.InPlaceEditorControl is ImRtfEditor placeEditorControl))
      return;
    int col = 0;
    int num;
    placeEditorControl.GetTerCursorPos(out num, ref col);
    this.avsDocument.Lock_DocCell_TextChanged();
    try
    {
      int abs = placeEditorControl.TerRowColToAbs(num, col);
      bool flag = selectedNode.ReadOnlyNow || placeEditorControl.CurPositionInProtectedZone;
      placeEditorControl.InsertTerText(symbol, true, true);
      placeEditorControl.TerAbsToRowCol(abs, out num, out col);
      if (flag)
        placeEditorControl.SetTextTags(num, col, num, col, 80 /*0x50*/, (string) null, (string) null, 0);
      placeEditorControl.TerRepaginate(true);
      placeEditorControl.TerSetModify(true);
      placeEditorControl.FireModified((object) placeEditorControl);
    }
    finally
    {
      this.avsDocument.Unlock_DocCell_TextChanged();
    }
  }

  /// <summary>Переключить вид</summary>
  /// <param name="viewMode">Вид</param>
  public void SetViewMode(AVSViewMode newViewMode)
  {
    if (this.IsViewModeLocked())
      return;
    this.LockViewMode();
    try
    {
      if (this.ViewMode != newViewMode)
      {
        this.viewMode = newViewMode;
        this.viewSwitch1.Visible = true;
        if (this.DocumentControl != null)
          this.DocumentControl.Parent = (Control) null;
        List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
        DocumentTreeNode focusedDocNode = (DocumentTreeNode) null;
        if (newViewMode == AVSViewMode.Grid)
        {
          this.LoadColumnsStateIfNeeded();
          if (this.ReadOnly && !this.avsDocument.DataLoaded)
            this.avsDocument.LoadAVSDocumentData((AVSDocumentContext) null);
          else
            this.avsDocument.LoadNewAttributes(this.GetGridViewColumns(), true);
          documentTreeNodeList = this.DocumentControl?.SelectedNodes;
          this.avsDocument.RecreateTreeListNodes();
        }
        List<AVSRow> avsRowList = new List<AVSRow>();
        if (newViewMode == AVSViewMode.Page && this.virtualTree != null)
        {
          documentTreeNodeList = new List<DocumentTreeNode>();
          foreach (IVirtualTreeItem listNode in (IEnumerable) this.virtualTree.Selection)
          {
            focusedDocNode = AVSWindow.GetDocNodeForListNode(listNode);
            if (focusedDocNode != null)
              documentTreeNodeList.Add(focusedDocNode);
          }
          if (this.DocumentControl != null)
          {
            this.virtualTree.Parent = (Control) null;
            this.DocumentControl.Parent = (Control) this._panelMain;
            this.viewSwitch1.Visible = false;
          }
        }
        if (this.ViewModeSwitched != null)
          this.ViewModeSwitched((object) this, new EventArgs());
        else if (newViewMode == AVSViewMode.Page)
        {
          this.DocumentControl?.SetSelection(documentTreeNodeList, true, false);
          this.DocumentControl?.BringToFront();
        }
        if (newViewMode == AVSViewMode.Grid)
        {
          this.ExpandTreeListNodes();
          if (documentTreeNodeList != null && documentTreeNodeList.Count > 0)
            focusedDocNode = documentTreeNodeList[0];
          this.RestoreListSelection(documentTreeNodeList, focusedDocNode);
          this._panelMain.BringToFront();
          this.virtualTree.Height = 400;
          this.virtualTree.Dock = DockStyle.Fill;
          this.virtualTree.Parent = (Control) this._panelMain;
        }
        else
          this.virtualTree.Parent = (Control) null;
        if (newViewMode == AVSViewMode.Grid)
          this.virtualTree.AllowDrop = !this.ReadOnly;
      }
      if (AVSPlugin.Instance.CommandManager != null)
        AVSPlugin.Instance.CommandManager.QueryStatus();
      if ((AVSViewMode) this.DocumentControl.ViewsSwitch.ActivepageIndex != this.viewMode)
        this.DocumentControl.ViewsSwitch.ActivepageIndex = (int) this.viewMode;
      if ((AVSViewMode) this.viewSwitch1.ActivepageIndex == this.viewMode)
        return;
      this.viewSwitch1.ActivepageIndex = (int) this.viewMode;
    }
    finally
    {
      this.UnlockViewMode();
    }
  }

  /// <summary>Раскрыть узлы дерева</summary>
  /// <param name="listNodes">Список узлов</param>
  internal void ExpandTreeListNodes() => this.virtualTree.RestoreExpanded();

  private void RestoreSelection(List<long> relationIDs, long focusedRelationID)
  {
    switch (this.viewMode)
    {
      case AVSViewMode.Page:
        List<DocumentTreeNode> selection = new List<DocumentTreeNode>(relationIDs.Count);
        foreach (long relationId in relationIDs)
        {
          AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(relationId);
          if (avsDocRow.DocNode != null)
            selection.Add((DocumentTreeNode) avsDocRow.DocNode);
        }
        this.DocumentControl.SetSelection(selection, true, false);
        break;
      case AVSViewMode.Grid:
        this.virtualTree.BeginUpdate();
        try
        {
          List<IVirtualTreeItem> virtualTreeItemList = new List<IVirtualTreeItem>();
          if (focusedRelationID != -1L)
          {
            IVirtualTreeItem nodeByRelationId = this.GetTreeNodeByRelationID(focusedRelationID);
            if (nodeByRelationId != null)
              this.virtualTree.FocusedItem = (object) nodeByRelationId;
          }
          if (relationIDs.Count <= 0)
            break;
          foreach (long relationId in relationIDs)
          {
            IVirtualTreeItem nodeByRelationId = this.GetTreeNodeByRelationID(relationId);
            if (nodeByRelationId != null)
              virtualTreeItemList.Add(nodeByRelationId);
          }
          this.virtualTree.Selection = (IList) virtualTreeItemList;
          break;
        }
        finally
        {
          this.virtualTree.EndUpdate();
        }
    }
  }

  private IVirtualTreeItem GetTreeNodeByRelationID(long relationID)
  {
    return (IVirtualTreeItem) this.avsDocument.GetAvsDocRow(relationID);
  }

  internal void RestoreSelection(List<AVSRow> selectedSpecRows, AVSRow focusedSpecRow)
  {
    switch (this.viewMode)
    {
      case AVSViewMode.Page:
        List<DocumentTreeNode> selection = new List<DocumentTreeNode>(selectedSpecRows.Count);
        foreach (AVSRow selectedSpecRow in selectedSpecRows)
        {
          if (selectedSpecRow.DocNode != null)
            selection.Add((DocumentTreeNode) selectedSpecRow.DocNode);
        }
        this.DocumentControl.SetSelection(selection, true, false);
        break;
      case AVSViewMode.Grid:
        this.virtualTree.Selection = (IList) selectedSpecRows;
        break;
    }
  }

  internal static IVirtualTreeItem GetListNodeForDocNode(DocumentTreeNode docNode)
  {
    if (docNode == null || docNode.IsVirtualNode)
      return (IVirtualTreeItem) null;
    switch (docNode)
    {
      case RectangleElement rectangleElement:
        pattern_0 = rectangleElement.ParentCell;
        break;
    }
    object listNodeForDocNode1 = (object) null;
    for (; pattern_0 != null; pattern_0 = pattern_0.ParentCell)
    {
      listNodeForDocNode1 = pattern_0.Tag;
      if (listNodeForDocNode1 != null)
        break;
    }
    if (listNodeForDocNode1 != null)
    {
      if (listNodeForDocNode1 is IVirtualTreeItem)
        return (IVirtualTreeItem) listNodeForDocNode1;
      if (listNodeForDocNode1 is AVSRow listNodeForDocNode2)
        return (IVirtualTreeItem) listNodeForDocNode2;
      if (listNodeForDocNode1 is Chapter listNodeForDocNode3)
        return (IVirtualTreeItem) listNodeForDocNode3;
    }
    return (IVirtualTreeItem) null;
  }

  internal static List<IVirtualTreeItem> GetListNodesForDocNodes(List<DocumentTreeNode> docNodes)
  {
    List<IVirtualTreeItem> nodesForDocNodes = new List<IVirtualTreeItem>();
    if (docNodes == null)
      return (List<IVirtualTreeItem>) null;
    foreach (DocumentTreeNode docNode in docNodes)
    {
      if (docNode != null)
      {
        if (docNode.IsVirtualNode)
        {
          foreach (DocumentTreeNode realCell in (docNode as RectangleElement).GetRealCells())
          {
            IVirtualTreeItem listNodeForDocNode = AVSWindow.GetListNodeForDocNode(realCell);
            if (listNodeForDocNode != null && !nodesForDocNodes.Contains(listNodeForDocNode))
              nodesForDocNodes.Add(listNodeForDocNode);
          }
        }
        else
        {
          IVirtualTreeItem listNodeForDocNode = AVSWindow.GetListNodeForDocNode(docNode);
          if (listNodeForDocNode != null && !nodesForDocNodes.Contains(listNodeForDocNode))
            nodesForDocNodes.Add(listNodeForDocNode);
        }
      }
    }
    return nodesForDocNodes;
  }

  internal static DocumentTreeNode GetDocNodeForListNode(IVirtualTreeItem listNode)
  {
    if (listNode == null)
      return (DocumentTreeNode) null;
    IVirtualTreeItem docNodeForListNode = listNode;
    switch (docNodeForListNode)
    {
      case DocumentTreeNode _:
        return (DocumentTreeNode) docNodeForListNode;
      case AVSRow avsRow:
        return (DocumentTreeNode) avsRow.DocNode;
      case Chapter chapter:
        return (DocumentTreeNode) chapter.DocNode;
      default:
        return (DocumentTreeNode) null;
    }
  }

  public void RestoreListSelection(
    List<DocumentTreeNode> selectedDocNodes,
    DocumentTreeNode focusedDocNode)
  {
    List<IVirtualTreeItem> nodesForDocNodes = AVSWindow.GetListNodesForDocNodes(selectedDocNodes);
    this.virtualTree.Selection = (IList) nodesForDocNodes;
    if (nodesForDocNodes != null)
    {
      foreach (IVirtualTreeItem virtualTreeItem in nodesForDocNodes)
        this.virtualTree.ExpandTo(virtualTreeItem);
    }
    List<IVirtualTreeItem> virtualTreeItemList = nodesForDocNodes;
    if (focusedDocNode != null)
      virtualTreeItemList = AVSWindow.GetListNodesForDocNodes(new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) new DocumentTreeNode[1]
      {
        focusedDocNode
      }));
    if (virtualTreeItemList.Count <= 0)
      return;
    this.virtualTree.FocusedItem = (object) virtualTreeItemList[0];
  }

  private void RestoreDocSelection(List<IVirtualTreeItem> selectedListNodes)
  {
    List<DocumentTreeNode> selection = new List<DocumentTreeNode>(selectedListNodes.Count);
    for (int index = 0; index < selectedListNodes.Count; ++index)
    {
      DocumentTreeNode docNodeForListNode = AVSWindow.GetDocNodeForListNode(selectedListNodes[index]);
      if (docNodeForListNode != null)
        selection.Add(docNodeForListNode);
    }
    this.DocumentControl.SetSelection(selection, true, false);
  }

  /// <summary>Переключение вида заблокировано</summary>
  /// <returns></returns>
  private bool IsViewModeLocked() => this._lockViewModeCounter > 0;

  /// <summary>Заблокировать переключение вида</summary>
  private void LockViewMode() => ++this._lockViewModeCounter;

  /// <summary>Разблокировать переключение вида</summary>
  private void UnlockViewMode()
  {
    if (this._lockViewModeCounter <= 0)
      return;
    --this._lockViewModeCounter;
  }

  /// <summary>Переключение вида заблокировано</summary>
  /// <returns></returns>
  private bool IsUpdateSelectionLocked() => this._lockUpdateSelection > 0;

  /// <summary>Заблокировать переключение вида</summary>
  private void LockUpdateSelection() => ++this._lockUpdateSelection;

  /// <summary>Разблокировать переключение вида</summary>
  private void UnlockUpdateSelection()
  {
    if (this._lockUpdateSelection <= 0)
      return;
    --this._lockUpdateSelection;
  }

  /// <summary>Получить строку содержащую данные для восстановления окна</summary>
  protected override string GetPersistString()
  {
    if (this.avsDocument == null)
      return (string) null;
    try
    {
      HybridDictionary graph = new HybridDictionary();
      graph[(object) "AssemblyGuid"] = (object) this.avsDocument.DocumentGuid;
      graph[(object) "BottomPanelType"] = (object) this.BottomPanelType;
      graph[(object) "BottomPanelHeight"] = (object) this._panelBottom.Height;
      if (this.ReadOnly)
        graph[(object) "ReadOnly"] = (object) this.ReadOnly;
      string empty = string.Empty;
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
        return Convert.ToBase64String(serializationStream.ToArray());
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (string) null;
  }

  private void ViewsSwitch_OnActivePageChanged(object sender, EventArgs e)
  {
    try
    {
      if (sender == null || !(sender is ViewSwitch) || this.viewMode == (AVSViewMode) ((ViewSwitch) sender).ActivepageIndex)
        return;
      this.SetViewMode((AVSViewMode) ((ViewSwitch) sender).ActivepageIndex);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public override void OnClosed(EventArgs e)
  {
    try
    {
      if (this.DocumentControl != null)
        this.DocumentControl.DocumentModifiedChanged -= new ModifiedChanged_EventHandler(DocumentEditorPlugin.Instance.DocumentModifiedChanged);
      AvsConfig.General.Changed -= new EventHandler(this.AvsConfig_Changed);
      base.OnClosed(e);
      if (this.workCompleteWaitMode)
      {
        this.workCompleteEvent.Set();
        this.workCompleteWaitMode = false;
      }
      Application.RemoveMessageFilter((IMessageFilter) this);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Обработка события Closing</summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    try
    {
      if (this.Document != null && this.Document.Modified)
      {
        int num = this.Document.Modified ? 1 : 0;
      }
      if (!this.DocumentDBObjectWasRemoved)
      {
        this.DocumentControl.EditorValidating(e);
        this.DocumentControl.DeactivateInPlaceEditor();
        if (e.Cancel)
          return;
      }
      if (!this.DocumentDBObjectWasRemoved && !this.ReadOnly && (this.IsSpecification && AvsConfig.General.CheckSpecificationBeforeClose || this.IsElementList && AvsConfig.General.CheckElementListBeforeClose) && !this.CheckErrors(true))
      {
        e.Cancel = true;
      }
      else
      {
        base.OnClosing(e);
        if (this.workCompleteWaitMode)
        {
          if (IMMessageBox.Show("Завершение редактирования спецификации", "Завершить редактирование спецификации и вернуться в CAD-систему?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) == DialogResultAdv.OK)
          {
            this.workCompleteEvent.Set();
            this.workCompleteWaitMode = false;
            if (AVSPlugin.Instance.CommandManager != null)
              AVSPlugin.Instance.CommandManager.QueryStatus();
          }
          else
            e.Cancel = true;
        }
        this.barManagerInitializing = true;
        if (!this.DocumentDBObjectWasRemoved && !this.AskForSaveBeforeClose && !e.Cancel && this.ObjectAssigned && this.Document != null && !this.ReadOnly)
          this.avsDocument.SaveAVSDocumentToDbIfNeed();
        foreach (KeyValuePair<string, ExternalAVSCommand> externalAvsCommand in this.ExternalAVSCommands)
        {
          ICommandState command = this.CommandManager.FindCommand(externalAvsCommand.Key);
          if (command != null)
          {
            command.Visible = false;
            command.Enabled = false;
          }
        }
        this.ExternalAVSCommands.Clear();
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      e.Cancel = true;
    }
    finally
    {
      if (e.Cancel)
        this.barManagerInitializing = false;
    }
  }

  /// <summary>Получить контекст команды. Выделенные элементы для меню
  /// и элементы под курсором мыши для контекстного меню</summary>
  /// <returns>Контекст команды</returns>
  public DocumentTreeNode[] GetCommandContext()
  {
    if (this.DocumentControl == null)
      return new DocumentTreeNode[0];
    DocumentTreeNode[] commandContext = this.viewMode == AVSViewMode.Page ? NodeContextMenu.ContextForContextMenu : (DocumentTreeNode[]) null;
    if (commandContext == null || !NodeContextMenu.ContextMenuCommand)
    {
      if (this.viewMode == AVSViewMode.Page)
        commandContext = this.DocumentControl.GetSelectedNodes();
      if (this.viewMode == AVSViewMode.Grid && this.virtualTree != null && this.virtualTree.Selection != null)
      {
        commandContext = new DocumentTreeNode[0];
        List<DocumentTreeNode> documentTreeNodeList1 = new List<DocumentTreeNode>();
        for (int index = 0; index < this.virtualTree.Selection.Count; ++index)
        {
          if (this.virtualTree.Selection[index] != null)
          {
            if (this.virtualTree.Selection[index] is RectangleElement)
              documentTreeNodeList1.Add((DocumentTreeNode) (this.virtualTree.Selection[index] as RectangleElement));
            if (this.virtualTree.Selection[index] is AVSRow)
              documentTreeNodeList1.Add((DocumentTreeNode) (this.virtualTree.Selection[index] as AVSRow).DocNode);
            if (this.virtualTree.Selection[index] is Chapter)
              documentTreeNodeList1.Add((DocumentTreeNode) (this.virtualTree.Selection[index] as Chapter).DocNode);
            if (this.virtualTree.Selection[index] is AVSDocument)
            {
              if (this.DocumentControl.ActivePage != null)
                documentTreeNodeList1.Add((DocumentTreeNode) this.DocumentControl.ActivePage);
              else if (this.DocumentControl.Document.NodesCount > 0)
                documentTreeNodeList1.Add(this.DocumentControl.Document.Nodes[0]);
            }
          }
        }
        if (documentTreeNodeList1.Count > 0)
        {
          List<DocumentTreeNode> documentTreeNodeList2 = new List<DocumentTreeNode>();
          foreach (DocumentTreeNode documentTreeNode in documentTreeNodeList1)
          {
            if (documentTreeNode != null)
              documentTreeNodeList2.Add(documentTreeNode);
          }
          commandContext = documentTreeNodeList2.ToArray();
        }
        else if ((this.virtualTree.RootRow == null || this.virtualTree.RootRow.ChildItems.Count == 0) && this.avsDocument.commonDataChapter.DocNode != null)
          commandContext = new DocumentTreeNode[1]
          {
            (DocumentTreeNode) this.avsDocument.commonDataChapter.DocNode
          };
      }
    }
    return commandContext;
  }

  /// <summary>Получить контекст команды, только если выбран один узел. Выделенные элементы для меню
  /// и элементы под курсором мыши для контекстного меню</summary>
  public AVSDocumentContext GetAVSDocumentContext_OnlyOneNode()
  {
    return this.avsDocument.GetContextChapters(this.GetCommandContext_OnlyOneNode(), true);
  }

  /// <summary>Получить контекст команды, только если выбран один узел. Выделенные элементы для меню
  /// и элементы под курсором мыши для контекстного меню</summary>
  public DocumentTreeNode GetCommandContext_OnlyOneNode()
  {
    DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(this.GetCommandContext(), true);
    return nodesWithoutChilds.Length == 1 ? nodesWithoutChilds[0] : (DocumentTreeNode) null;
  }

  /// <summary>Получить контекст команды. Возвращает первый выделенный узел</summary>
  public DocumentTreeNode GetCommandContext_OnlyFirstNode()
  {
    DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(this.GetCommandContext(), true);
    return nodesWithoutChilds.Length != 0 ? nodesWithoutChilds[0] : (DocumentTreeNode) null;
  }

  protected bool IsFocusedDocument()
  {
    bool flag = this.DocumentControl.GetFocusedControl() != null || this.ActiveControl == this.DocumentControl;
    if (!flag)
      flag = this.virtualTree.GetFocusedControl() != null;
    return flag;
  }

  /// <summary>Перехватить обработку клавиш</summary>
  /// <param name="msg">Сообщение</param>
  /// <param name="keyData">Клавиши</param>
  /// <returns>true, если клавиша не нуждается в дальнейшей обработке</returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    try
    {
      if (keyData == (Keys.ControlKey | Keys.Control) && this.DocumentControl != null && this.DocumentControl.DocumentManager != null)
      {
        ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
        if (commandManager != null)
        {
          ICommandState command1 = commandManager.FindCommand("Copy");
          commandManager.QueryStatus(command1);
          ICommandState command2 = commandManager.FindCommand("Cut");
          commandManager.QueryStatus(command2);
          ICommandState command3 = commandManager.FindCommand("Paste");
          commandManager.QueryStatus(command3);
        }
      }
      if (keyData == Keys.Next || keyData == Keys.Prior || keyData == (Keys.Next | Keys.Control) || keyData == (Keys.Prior | Keys.Control))
      {
        this.LockPageKeys();
        try
        {
          return base.ProcessCmdKey(ref msg, keyData);
        }
        finally
        {
          this.UnlockPageKeys();
        }
      }
      else
      {
        if (this.ViewMode == AVSViewMode.Grid)
        {
          switch (keyData)
          {
            case Keys.C | Keys.Control:
              if (this.DocumentControl != null && this.DocumentControl.DocumentManager != null)
              {
                ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
                if (commandManager != null)
                {
                  ICommandState command = commandManager.FindCommand("Copy");
                  if (command != null)
                  {
                    if (this.IsFocusedDocument())
                    {
                      if (command.Enabled)
                        commandManager.Execute(command);
                      return true;
                    }
                    command.Enabled = false;
                    return true;
                  }
                }
              }
              return true;
            case Keys.V | Keys.Control:
              if (this.DocumentControl != null && this.DocumentControl.DocumentManager != null)
              {
                ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
                if (commandManager != null)
                {
                  ICommandState command = commandManager.FindCommand("Paste");
                  if (command != null && this.IsFocusedDocument() && command.Enabled)
                  {
                    commandManager.Execute(command);
                    return true;
                  }
                }
              }
              return true;
            case Keys.X | Keys.Control:
              if (this.DocumentControl != null && this.DocumentControl.DocumentManager != null)
              {
                ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
                if (commandManager != null)
                {
                  ICommandState command = commandManager.FindCommand("Cut");
                  if (command != null && this.IsFocusedDocument() && command.Enabled)
                  {
                    commandManager.Execute(command);
                    return true;
                  }
                }
              }
              return true;
          }
        }
        return base.ProcessCmdKey(ref msg, keyData);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return false;
    }
  }

  private void AVSWindow_Leave(object sender, EventArgs e)
  {
    try
    {
      this.SaveColumnsState(true);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected override void Init()
  {
    this.ShowImageInDocumentTab = true;
    this.TabImage = DocumentMenuHelper.LoadImageFromResurces(this.GetType().Assembly, "Intermech.AVS.Resources.AVSDocument.png");
    if (this.ExternalEditor == null)
      this.ExternalEditor = (IExternalEditor) new AVSExternalEditor(this);
    base.Init();
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
  }

  /// <summary>Выполнить инициализацию менеджера панелей</summary>
  protected override void InitBarManager()
  {
    try
    {
      if (AVSPlugin.Instance.CommandManager == null)
        return;
      this.barManagerInitializing = true;
      if (this.DocumentControl != null)
        this.DocumentControl.BarManager = this.barManager;
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        this.barManager.OwnerForm = service.OwnerForm;
        service.RendererChanged += new EventHandler(((ImDocumentEditorForm) this).ToolbarRendererChanged);
        this.ToolbarRendererChanged((object) service, EventArgs.Empty);
        service.CollectToolbars += new CollectToolbarsHandler(this.barMgr_CollectToolbars);
      }
      this.MenuHelper = this.CreateDocumentMenuHelper();
      (this.MenuHelper as AvsMenuHelper).AvsWindow = this;
      this.formatToolBar = this.MenuHelper.CreateFormatToolBar(DocumentEditorPlugin.imageList, AVSPlugin.Instance.CommandManager);
      this.AddToolbar(this.barManager, this.formatToolBar, DockStyle.Top);
      this.formatToolBar.DockLine = 0;
      this.formatToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.formatToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.formatToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
      this.navigateToolbar = this.MenuHelper.CreateNavigatorToolBar(DocumentEditorPlugin.imageList, AVSPlugin.Instance.CommandManager);
      this.navigateToolbar.Visible = false;
      this.AddToolbar(this.barManager, this.navigateToolbar, DockStyle.Top);
      this.navigateToolbar.DockLine = 0;
      this.navigateToolbar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.navigateToolbar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.navigateToolbar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
      this.specificationToolBar = (this.MenuHelper as AvsMenuHelper).CreateSpecificationToolBar(DocumentEditorPlugin.imageList, AVSPlugin.Instance.CommandManager);
      this.AddToolbar(this.barManager, this.specificationToolBar, DockStyle.Top);
      this.specificationToolBar.DockLine = 0;
      this.specificationToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.specificationToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
      this.specificationToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
      this.InitRedlineOnOffToolbar(AVSPlugin.Instance.CommandManager, new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged));
      this.InitRedlineNotesEditingToolbar(AVSPlugin.Instance.CommandManager, new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged));
      MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("AVS.AddSpecSection");
      if (contextMenuItem != null && contextMenuItem.Items.Count == 0)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem("[Нет записей]");
        menuButtonItem.CommandName = "AVS.AddSpecSection.None";
        contextMenuItem.Items.Add((ToolbarItemBase) menuButtonItem);
      }
      NodeContextMenu.GetContextMenuItem("AVS.AddOtherRecordTypes")?.Items.Add("[Нет записей]");
      IConfigurationManager configurationManager = AVSPlugin.Instance.ConfigurationManager;
      if (configurationManager == null)
        return;
      IConfiguration configuration = configurationManager.Open(this.GetConfigName());
      if (configuration == null)
        return;
      string property = configuration.GetProperty(this.GetToolbarConfigName());
      switch (property)
      {
        case null:
          break;
        case "":
          break;
        default:
          this.barManager.SetLayout(property);
          break;
      }
    }
    finally
    {
      this.barManagerInitializing = false;
    }
  }

  public override DocumentMenuHelper CreateDocumentMenuHelper()
  {
    return (DocumentMenuHelper) new AvsMenuHelper(AVSPlugin.Instance.CommandManager)
    {
      AvsWindow = this
    };
  }

  protected override string GetConfigName() => "AVS";

  private void toolBar_HiddenChanged(object sender, EventArgs e)
  {
    if (this.barManagerInitializing)
      return;
    try
    {
      IConfigurationManager configurationManager = AVSPlugin.Instance.ConfigurationManager;
      if (configurationManager == null)
        return;
      (configurationManager.Open(this.GetConfigName()) ?? configurationManager.Create(this.GetConfigName()))?.SetProperty(this.GetToolbarConfigName(), this.barManager.GetLayout(true));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void barMgr_CollectToolbars(object sender, CollectToolbarsEventArgs e)
  {
    try
    {
      if (AVSPlugin.Instance.ActiveAVSWindow != this)
        return;
      e.Toolbars.AddRange((ICollection) this.barManager.GetToolbarsList());
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Вызывается если требуется заблокировать визуальное представление документа </summary>
  protected override bool OnLockUpdate()
  {
    try
    {
      switch (this.ViewMode)
      {
        case AVSViewMode.Page:
          base.OnLockUpdate();
          return true;
        case AVSViewMode.Grid:
          this.LockTreeList();
          return true;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return true;
  }

  /// <summary>Вызывается если требуется разблокировать визуальное представление документа </summary>
  /// <param name="update"> true если требуется обновить представление </param>
  protected override void OnUnlockUpdate(bool update)
  {
    switch (this.ViewMode)
    {
      case AVSViewMode.Page:
        base.OnUnlockUpdate(update);
        break;
      case AVSViewMode.Grid:
        if (update)
          this.avsDocument.RecreateTreeListNodes();
        this.UnlockTreeList();
        break;
    }
  }

  /// <summary>Обработчик изменений выделения в редакторе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DocumentControl_SelectionChanged(object sender, SelectionChanged_EventArgs e)
  {
    try
    {
      if (this.viewMode == AVSViewMode.Grid && !this.IsUpdateSelectionLocked())
        this.RestoreListSelection(this.DocumentControl.SelectedNodes, (DocumentTreeNode) null);
      if (this.viewMode == AVSViewMode.Page && this.BottomPanelType != AVSWindow.enumBottomPanelType.None)
      {
        this._bottomPanelType = AVSWindow.enumBottomPanelType.SelectedRowProperties;
        this.UpdateProductPropertiesPanel();
      }
      else
      {
        if (this.rowPropertyGrid == null || !this.rowPropertyGrid.Visible)
          return;
        this.rowPropertyGrid.UpdateRows();
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Получить список выделенных узлов с отфильтрованными дочерними узлами</summary>
  protected override List<DocumentTreeNode> GetDocumentNodeList()
  {
    if (this.ViewMode != AVSViewMode.Grid)
      return base.GetDocumentNodeList();
    List<DocumentTreeNode> documentNodeList = new List<DocumentTreeNode>(this.virtualTree?.Selection?.Count ?? 0);
    if (this.virtualTree.Selection != null && this.virtualTree.Selection.Count > 0)
    {
      foreach (IVirtualTreeItem listNode in (IEnumerable) this.virtualTree.Selection)
      {
        DocumentTreeNode docNodeForListNode = AVSWindow.GetDocNodeForListNode(listNode);
        if (docNodeForListNode != null)
          documentNodeList.Add(docNodeForListNode);
      }
    }
    return documentNodeList;
  }

  protected override void SaveDocumentToDBObjectFile()
  {
    if (this.AVSDocument == null)
      return;
    this.AVSDocument.SaveAVSDocumentToDB_Internal();
  }

  protected override ImDocument LoadTemplateFromDB(long newTemplateId)
  {
    ImDocument template = base.LoadTemplateFromDB(newTemplateId);
    if (template != null && this.avsDocument != null)
      this.avsDocument.FindAllTemplates((ImDocumentData) template, false);
    return template;
  }

  protected override bool CompareTemplates(
    ImDocument oldTemplate,
    ImDocument newTemplate,
    out string resultDescription)
  {
    resultDescription = string.Empty;
    List<string> exclusionList = new List<string>()
    {
      AVSDocument.AdditionalComplectRowGroupTemplateId,
      AVSDocument.ChapterWithoutHeaderFormBTemplateId
    };
    return ImDocument.AreCompatibleTemplates(oldTemplate, newTemplate, out resultDescription, exclusionList: exclusionList);
  }

  /// <summary>Форма активирована</summary>
  public override void Activated()
  {
    try
    {
      base.Activated();
      if (this._activated)
        return;
      try
      {
        IAVSViewsService service = (IAVSViewsService) ServicesManager.GetService(typeof (IAVSViewsService));
        if (service != null)
          service.AVSWindow = this;
        else
          ServicesManager.AddService(typeof (IAVSViewsService), (object) new AVSViewsService(this));
        Application.AddMessageFilter((IMessageFilter) this);
        if (this.rowPropertyGrid == null || !this.rowPropertyGrid.Visible)
          return;
        this.rowPropertyGrid.Reset();
        this.rowPropertyGrid.UpdateRows();
      }
      finally
      {
        this._activated = true;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Деактивация формы</summary>
  public override void Deactivated()
  {
    try
    {
      base.Deactivated();
      Application.RemoveMessageFilter((IMessageFilter) this);
      if (!this._activated)
        return;
      try
      {
        foreach (KeyValuePair<string, ExternalAVSCommand> externalAvsCommand in this.ExternalAVSCommands)
        {
          ICommandState command = this.CommandManager.FindCommand(externalAvsCommand.Key);
          if (command != null)
            command.Visible = false;
        }
        if ((IAVSViewsService) ServicesManager.GetService(typeof (IAVSViewsService)) == null)
          return;
        ServicesManager.RemoveService(typeof (IAVSViewsService));
      }
      finally
      {
        this._activated = false;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>
  /// Получить список неучтенных связей объекта во всех исполнениях
  /// </summary>
  /// <param name="list">список связей</param>
  /// <returns>список связей</returns>
  [Obsolete("Переместить код в AVSDocument")]
  internal List<long> GetProductsRelations(IList<long> list, int relType)
  {
    List<long> productsRelations1 = new List<long>();
    try
    {
      if (this.avsDocument == null || this.avsDocument.productsInfo == null || this.avsDocument.productsInfo.Count < 2)
        return productsRelations1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ColumnDescriptor[] columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
        };
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relType);
        foreach (long aRelationID in (IEnumerable<long>) list)
        {
          if (!productsRelations1.Contains(aRelationID))
          {
            productsRelations1.Add(aRelationID);
            IDBRelation relation = sessionKeeper.Session.GetRelation(aRelationID);
            if (relation != null)
            {
              DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
              long partId = relation.PartID;
              long objectId = sessionKeeper.Session.GetObjectByID(partId, false).ObjectID;
              relationCollection.ChildObjectTypes = (IList<int>) ((IEnumerable<int>) AvsIDCache.BaseProductForSpecificationTypes).ToList<int>();
              DataTable dataTable = relationCollection.EntersInVersion(paramSet, objectId);
              List<long> longList = new List<long>();
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                long int64 = Convert.ToInt64(row[1]);
                if (int64 != aRelationID && this.avsDocument.ContainsProduct(Convert.ToInt64(row[0])) && !productsRelations1.Contains(int64))
                  productsRelations1.Add(int64);
              }
            }
          }
        }
      }
    }
    catch
    {
    }
    List<long> productsRelations2 = new List<long>();
    foreach (long num in productsRelations1)
    {
      if (!list.Contains(num))
        productsRelations2.Add(num);
    }
    return productsRelations2;
  }

  /// <summary>Вызывается при активации окна</summary>
  public void OnActivated()
  {
    if (this.lockOnActivated)
      return;
    try
    {
      this.lockOnActivated = true;
      if (this._relationIDsWithNeedToBeUpdated_FromNotificationService.Count > 0)
        this.RelationsWasChangedHandler((IList<long>) this._relationIDsWithNeedToBeUpdated_FromNotificationService);
      if (this._relationIDsCreated_FromNotificationService.Count > 0)
        this.RelationsWasCreatedHandler(this._relationIDsCreated_FromNotificationService);
      if (this._relationIDsRemoved_FromNotificationService.Count > 0)
        this.avsDocument.RemoveRelation_NotificationHandler((IList<long>) this._relationIDsRemoved_FromNotificationService);
      if (this._objectIDsRemoved_FromNotificationService.Count > 0)
        this.avsDocument.RemoveObject_NotificationHandler((IList<long>) this._objectIDsRemoved_FromNotificationService);
      this.UpdateNavigatorMenu(true);
      this.UpdateISimpleSelectedItemsService();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this.lockOnActivated = false;
    }
  }

  /// <summary>Получить идентификатор версии первого выделенного исполнения</summary>
  /// <returns>Идентификатор версии первого выделенного исполнения</returns>
  protected virtual long GetDefaultArticleID()
  {
    long defaultArticleId = 0;
    List<ProductInfo> selectedProducts = this.GetSelectedProducts();
    if (selectedProducts.Count > 0)
      defaultArticleId = selectedProducts[0].Id;
    return defaultArticleId;
  }

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Данные команды</param>
  /// <returns>true, если команда найдена</returns>
  public override bool Execute(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "AVS":
          return true;
        case "AVS.AddAdditionalChapter":
          this.AddAdditionalChapter(this.GetCommandContext());
          return true;
        case "AVS.AddDopComplect":
          DocumentTreeNode[] commandContext1 = this.GetCommandContext();
          if (commandContext1 != null && commandContext1.Length == 1)
            this.ContextAddSpecRow(commandContext1[0], AvsIDCache.Relation_AddComplect, (object[]) null);
          else if (commandContext1 == null || commandContext1.Length == 0)
            this.ContextAddSpecRow((DocumentTreeNode) null, AvsIDCache.Relation_AddComplect, (object[]) null);
          return true;
        case "AVS.AddGroupSpecRowFromImbase":
          DocumentTreeNode[] commandContext2 = this.GetCommandContext();
          if (commandContext2.Length == 1)
            this.ContextAddGroupSpecRowFromImbase(commandContext2[0]);
          else if (commandContext2.Length == 0)
            this.ContextAddGroupSpecRowFromImbase((DocumentTreeNode) null);
          return true;
        case "AVS.AddIspoln":
          if (this.avsDocument.IsSpecification && this.avsDocument.productsInfo.Count > 0)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              if (PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, this.avsDocument.productsInfo[0].Id))
              {
                int num = (int) MessageBox.Show("Невозможно добавить исполнение, так как это изделие создано на основе электронной модели и его изменение должно проводиться через эту модель.", "Добавление исполнения");
                return true;
              }
            }
          }
          SelectProductForm selectProductForm1 = new SelectProductForm(this.avsDocument, this.avsDocument.productsInfo, "Выберите исполнение, которое выступит прототипом для создаваемого", true, false);
          if (selectProductForm1.ShowDialog() == DialogResult.OK)
            this.AddProductVersion(selectProductForm1.SelectedProductIndex);
          return true;
        case "AVS.AddLRIRecord":
        case "AVS.AddLRIRecord_After":
          this.ContextAddRLIRecord(true);
          return true;
        case "AVS.AddLRIRecord_Before":
          this.ContextAddRLIRecord(false);
          return true;
        case "AVS.AddNewSpecRow":
          this.ContextAddNewSpecRow(this.GetCommandContext_OnlyOneNode());
          return true;
        case "AVS.AddOtherRecordTypes":
          return true;
        case "AVS.AddSkipLineAfter":
          List<AVSRow> selectedSpecRows1 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows1.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index1 = 0; index1 < selectedSpecRows1.Count; ++index1)
              {
                if (selectedSpecRows1[index1].SkipLinesAfter.HasValue)
                {
                  AVSRow avsRow = selectedSpecRows1[index1];
                  int? skipLinesAfter = avsRow.SkipLinesAfter;
                  avsRow.SkipLinesAfter = skipLinesAfter.HasValue ? new int?(skipLinesAfter.GetValueOrDefault() + 1) : new int?();
                }
                else if (selectedSpecRows1[index1].DocNode != null)
                {
                  int num = Convert.ToInt32(selectedSpecRows1[index1].DocNode.SkipCellsAfter);
                  List<SkipLinesStruct> skipLines = this.avsDocument.GetSkipLines();
                  for (int index2 = 0; index2 < skipLines.Count - 1; ++index2)
                  {
                    if (skipLines[index2].SpecRow == selectedSpecRows1[index1])
                    {
                      SkipLinesStruct skipLinesStruct = skipLines[index2 + 1];
                      if (!skipLinesStruct.BeforeSetted && (int) skipLinesStruct.SkipBefore > num)
                      {
                        num = (int) skipLinesStruct.SkipBefore;
                        break;
                      }
                      break;
                    }
                  }
                  selectedSpecRows1[index1].SkipLinesAfter = new int?(num + 1);
                }
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
            }
          }
          else
          {
            SpecificationSection selectedSection = this.GetSelectedSection();
            if (selectedSection != null)
            {
              this.avsDocument.SuspendDocumentAndGridUpdates();
              try
              {
                int? skipLinesAfter = selectedSection.SkipLinesAfter;
                if (skipLinesAfter.HasValue)
                {
                  SpecificationSection specificationSection = selectedSection;
                  skipLinesAfter = specificationSection.SkipLinesAfter;
                  int? nullable = skipLinesAfter;
                  specificationSection.SkipLinesAfter = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
                }
                else if (selectedSection.DocNode != null)
                {
                  int num = Convert.ToInt32(selectedSection.DocNode.SkipCellsAfter);
                  List<SkipLinesStruct> skipLines = this.avsDocument.GetSkipLines();
                  for (int index = 0; index < skipLines.Count - 1; ++index)
                  {
                    if (skipLines[index].Chapter == selectedSection)
                    {
                      SkipLinesStruct skipLinesStruct = skipLines[index + 1];
                      if (!skipLinesStruct.BeforeSetted && (int) skipLinesStruct.SkipBefore > num)
                      {
                        num = (int) skipLinesStruct.SkipBefore;
                        break;
                      }
                      break;
                    }
                  }
                  selectedSection.SkipLinesAfter = new int?(num + 1);
                }
              }
              finally
              {
                this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              }
            }
          }
          return true;
        case "AVS.AddSkipLineBefore":
          List<AVSRow> selectedSpecRows2 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows2.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index3 = 0; index3 < selectedSpecRows2.Count; ++index3)
              {
                if (selectedSpecRows2[index3].SkipLinesBefore.HasValue)
                {
                  AVSRow avsRow = selectedSpecRows2[index3];
                  int? skipLinesBefore = avsRow.SkipLinesBefore;
                  avsRow.SkipLinesBefore = skipLinesBefore.HasValue ? new int?(skipLinesBefore.GetValueOrDefault() + 1) : new int?();
                }
                else if (selectedSpecRows2[index3].DocNode != null)
                {
                  int num = Convert.ToInt32(selectedSpecRows2[index3].DocNode.SkipCellsBefore);
                  List<SkipLinesStruct> skipLines = this.avsDocument.GetSkipLines();
                  for (int index4 = 1; index4 < skipLines.Count; ++index4)
                  {
                    if (skipLines[index4].SpecRow == selectedSpecRows2[index3])
                    {
                      SkipLinesStruct skipLinesStruct = skipLines[index4 - 1];
                      if (!skipLinesStruct.AfterSetted && (int) skipLinesStruct.SkipAfter > num)
                      {
                        num = (int) skipLinesStruct.SkipAfter;
                        break;
                      }
                      break;
                    }
                  }
                  selectedSpecRows2[index3].SkipLinesBefore = new int?(num + 1);
                }
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
            }
          }
          else
          {
            SpecificationSection selectedSection = this.GetSelectedSection();
            if (selectedSection != null)
            {
              this.avsDocument.SuspendDocumentAndGridUpdates();
              try
              {
                int? skipLinesBefore = selectedSection.SkipLinesBefore;
                if (skipLinesBefore.HasValue)
                {
                  SpecificationSection specificationSection = selectedSection;
                  skipLinesBefore = specificationSection.SkipLinesBefore;
                  int? nullable = skipLinesBefore;
                  specificationSection.SkipLinesBefore = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
                }
                else if (selectedSection.DocNode != null)
                {
                  int num = Convert.ToInt32(selectedSection.DocNode.SkipCellsBefore);
                  List<SkipLinesStruct> skipLines = this.avsDocument.GetSkipLines();
                  for (int index = 1; index < skipLines.Count; ++index)
                  {
                    if (skipLines[index].Chapter == selectedSection)
                    {
                      SkipLinesStruct skipLinesStruct = skipLines[index - 1];
                      if (!skipLinesStruct.AfterSetted && (int) skipLinesStruct.SkipAfter > num)
                      {
                        num = (int) skipLinesStruct.SkipAfter;
                        break;
                      }
                      break;
                    }
                  }
                  selectedSection.SkipLinesBefore = new int?(num + 1);
                }
              }
              finally
              {
                this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              }
            }
          }
          return true;
        case "AVS.AddSpecRow":
          DocumentTreeNode[] commandContext3 = this.GetCommandContext();
          if (commandContext3 != null && commandContext3.Length == 1)
            this.ContextAddSpecRow(commandContext3[0], AvsIDCache.Relation_Project, (object[]) null);
          else if (commandContext3 == null || commandContext3.Length == 0)
            this.ContextAddSpecRow((DocumentTreeNode) null, AvsIDCache.Relation_Project, (object[]) null);
          return true;
        case "AVS.AddSpecRowFromImbase":
          DocumentTreeNode[] commandContext4 = this.GetCommandContext();
          if (commandContext4.Length == 1)
            this.ContextAddSpecRowFromImbase(commandContext4[0]);
          else if (commandContext4.Length == 0)
            this.ContextAddSpecRowFromImbase((DocumentTreeNode) null);
          return true;
        case "AVS.AddSpecSection":
          this.AddSpecSections(this.GetCommandContext(), (List<long>) null);
          return true;
        case "AVS.AddZagotovkaForPart":
          DocumentTreeNode[] commandContext5 = this.GetCommandContext();
          if (commandContext5 != null && commandContext5.Length == 1 && this.avsDocument is AVSSpecification)
            this.AddSpecRow_Zagotovka(commandContext5[0]);
          return true;
        case "AVS.AddZagotovkaForPart_FromImBase":
          DocumentTreeNode[] commandContext6 = this.GetCommandContext();
          if (commandContext6 != null && commandContext6.Length == 1 && this.avsDocument is AVSSpecification)
            this.AddSpecRow_ZagotovkaFromImBase(commandContext6[0]);
          return true;
        case "AVS.AdditionalChaptersSetup":
          using (SetupAdditionalChaptersDlg additionalChaptersDlg = new SetupAdditionalChaptersDlg(this.ReadOnly))
          {
            int num = (int) additionalChaptersDlg.ShowDialog();
          }
          return true;
        case "AVS.AssemblyProperty":
          this.BottomPanelType = AVSWindow.enumBottomPanelType.ProductsProperties;
          return true;
        case "AVS.ChangeRecordIspolnenie":
          this.ChangeRowProduct(this.GetCommandContext());
          return true;
        case "AVS.CheckErrors":
          this.CheckErrors();
          return true;
        case "AVS.CheckIn":
        case "CheckIn":
          List<AVSRow> selectedSpecRows3 = this.GetSelectedSpecRows(true);
          if (selectedSpecRows3.Count > 0 && selectedSpecRows3.All<AVSRow>((System.Func<AVSRow, bool>) (i => i.ObjectId < 0L)))
            this.DoCheckInSelectedItems();
          else
            this.DoCheckInDocument();
          this.UpdateNavigatorMenu(false);
          return true;
        case "AVS.CheckOut":
        case "CheckOut":
          this.DoCheckOutSelectedItems();
          this.UpdateNavigatorMenu(false);
          return true;
        case "AVS.ClearNumberPositions":
          if (!AvsConfig.General.AskRenumber || MessageBox.Show("Очистить графу \"Позиции\" в спецификации?", "Нумерация позиций", MessageBoxButtons.YesNo) == DialogResult.Yes)
            this.avsDocument.ClearNumberPositions();
          return true;
        case "AVS.ClearSmotri":
          this.avsDocument.ClearSmotriAttributeInEntireSpecification();
          return true;
        case "AVS.CommonPositions":
          AVSRow selectedSpecRow = this.GetSelectedSpecRows(false)[0];
          AVSRow parentRow = (AVSRow) null;
          List<AVSRow> commonPositionRows = selectedSpecRow.GetCommonPositionRows();
          string commonPosition1 = selectedSpecRow.CommonPosition;
          if (string.IsNullOrEmpty(commonPosition1))
            commonPosition1 = (this.AVSDocument.GetLastCommonPosition(this.AVSDocument.GetAllRows(false, false)) + 1).ToString();
          CommonPositionsForm commonPositionsForm = new CommonPositionsForm(this.avsDocument, commonPosition1, selectedSpecRow, parentRow);
          if (commonPositionsForm.ShowDialog() == DialogResult.OK)
          {
            if (commonPositionsForm.SelectedRows.Count > 1)
            {
              if (selectedSpecRow.CommonPositionDocument == null)
                selectedSpecRow.CommonPositionDocument = !string.IsNullOrEmpty(commonPositionsForm.CommonPosition) ? (string) null : Guid.NewGuid().ToString();
              selectedSpecRow.CommonPosition = commonPositionsForm.CommonPosition;
            }
            else
            {
              selectedSpecRow.CommonPositionDocument = (string) null;
              selectedSpecRow.CommonPosition = (string) null;
            }
            string commonPosition2 = selectedSpecRow.CommonPosition;
            string positionDocument = selectedSpecRow.CommonPositionDocument;
            if (commonPositionRows != null)
            {
              foreach (AVSRow avsRow in commonPositionRows)
              {
                avsRow.CommonPosition = (string) null;
                avsRow.CommonPositionDocument = (string) null;
              }
            }
            foreach (AVSRow selectedRow in commonPositionsForm.SelectedRows)
            {
              selectedRow.CommonPosition = commonPosition2;
              selectedRow.CommonPositionDocument = positionDocument;
            }
          }
          return true;
        case "AVS.ConvertFromZagotovka":
          this.ConvertZagotovkaToPart(this.GetCommandContext());
          return true;
        case "AVS.CopyRecord":
          this.CopyToClipboardCommand(this.GetCommandContext());
          return true;
        case "AVS.CreateDocumentFromFile_VB":
          Vedomost_VB_Static.Create_Document_From_Avs6File();
          return true;
        case "AVS.CreateElementList":
          AVSPlugin.CreateElementListById(this.avsDocument.ProductId, AvsIDCache.ObjType_AssemblyUnit);
          return true;
        case "AVS.CreateVedomost_VB":
          Vedomost_VB vedomostVb = new Vedomost_VB();
          vedomostVb.DesignationArticle = this.AVSDocument.documentDesignation;
          vedomostVb.NameArticle = this.AVSDocument.documentName;
          vedomostVb._metodCreate = "Auto";
          vedomostVb._metodFrom = "AVSDocument";
          vedomostVb._iDSP = this.AVSDocument.DocumentID;
          if (vedomostVb.CreateVedomost(this.AVSDocument))
            return true;
          if (vedomostVb.sborVedTask != null)
            vedomostVb.sborVedTask.Dispose();
          return false;
        case "AVS.DeleteEmptySections":
          this.avsDocument.RemoveEmptySections(false);
          this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          return true;
        case "AVS.DeleteObjects":
          DocumentTreeNode[] commandContext7 = this.GetCommandContext();
          if (commandContext7 != null)
          {
            List<AVSRow> specRowsFromNodes = this.GetSpecRowsFromNodes(commandContext7);
            if (specRowsFromNodes.Count > 0)
              this.DeleteObjects(specRowsFromNodes);
          }
          return true;
        case "AVS.DeleteRecords":
        case "Delete":
          this.DeleteCommand(this.GetCommandContext(), true, false, true);
          return true;
        case "AVS.DeleteTitlePage":
          this.avsDocument.DeleteTitlePage((PageData) this.avsDocument.Document.DocumentControl.ActivePage);
          return true;
        case "AVS.DesignationTrimSetup":
          this.ShowSetupDesignationTrimDlg();
          return true;
        case "AVS.DisconnectSort":
          List<AVSRow> selectedSpecRows4 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows4.Count > 0)
          {
            for (int index = 0; index < selectedSpecRows4.Count; ++index)
            {
              selectedSpecRows4[index].SortAfterRow = (AVSRow) null;
              selectedSpecRows4[index].SortBeforeRow = (AVSRow) null;
            }
          }
          return true;
        case "AVS.DocumentProperty":
          this.BottomPanelType = AVSWindow.enumBottomPanelType.SpecificationProperties;
          return true;
        case "AVS.DocumentTypesWeights":
          if (DocumentTypesWeightsEditorForm.EditSystemCollection() == DialogResult.OK)
            this.avsDocument.ResortSpecification(false, true);
          return true;
        case "AVS.DontIncludeClassNameInGroupRow":
          List<AVSRow> selectedSpecRows5 = this.AVSDocument.AVSWindow.GetSelectedSpecRows(false);
          if (selectedSpecRows5.Count == 0)
            return true;
          this.ForceExcludeClassNameFromGroupCaption(selectedSpecRows5, true);
          return true;
        case "AVS.DynamicGroupHeaderSetup":
          this.ShowDynamicGroupHeaderSettingsDlg();
          return true;
        case "AVS.FinishWork":
          if (IMMessageBox.Show("Завершение редактирования спецификации", "Завершить редактирование спецификации и вернуться в CAD-систему?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) == DialogResultAdv.OK)
          {
            this.restoreWorkCompleteMode = true;
            this.workCompleteEvent.Set();
            this.DisableCompleteWaitMode();
            this.Close();
          }
          return true;
        case "AVS.FromNewPage":
          this.SetFromNewPageFlag(true);
          return true;
        case "AVS.GridViewMode":
          this.SetViewMode(AVSViewMode.Grid);
          return true;
        case "AVS.Group":
          this.Context_GroupRows();
          return true;
        case "AVS.GroupRowsByHeader":
          this.avsDocument.Document.SetDynamicGroupHeaderIsEnabled(true, true, true);
          this.avsDocument.UpdateRowsGroupHeaders();
          this.avsDocument.Document.UpdateLayout(true);
          if (this.AVSDocument.IsGridViewMode)
            this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) null);
          return true;
        case "AVS.Hide":
          List<AVSRow> selectedSpecRows6 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows6.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index = 0; index < selectedSpecRows6.Count; ++index)
              {
                if (this.avsDocument.CheckRowForExistingDopZamen(selectedSpecRows6[index]))
                {
                  int num = (int) IMMessageBox.Show("Скрыть строку", "Невозможно скрыть строку, т.к. она входит в группу доп.замен.", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
                }
                else if (!selectedSpecRows6[index].IsHiddenRow)
                  selectedSpecRows6[index].Hide();
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              this.avsDocument.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
            }
          }
          return true;
        case "AVS.HideDocRowsWithoutCount":
          if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
            this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.Delete);
          return true;
        case "AVS.HideSameChapters":
          this.AVSDocument.VariableDataChapter_FormA.HideSameProductChapters = true;
          this.AVSDocument.UpdateVariableDataCaptions(true);
          this.AVSDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          return true;
        case "AVS.ImbaseCatalogsSetup":
          this.ShowImbaseCatalogsDlg(this.avsDocument.AVSDocumentTemplateID);
          return true;
        case "AVS.IncludeClassNameInGroupRow":
          List<AVSRow> selectedSpecRows7 = this.AVSDocument.AVSWindow.GetSelectedSpecRows(false);
          if (selectedSpecRows7.Count == 0)
            return true;
          this.ForceExcludeClassNameFromGroupCaption(selectedSpecRows7, false);
          return true;
        case "AVS.InsertAdditionalPages":
          DocumentMenuHelper.InsertAdditionalPage(this.Document.DocumentControl);
          return true;
        case "AVS.InsertTitlePage":
          this.avsDocument.InsertTitlePage();
          return true;
        case "AVS.KeyWordsSetup":
          this.ShowSetupKeyWordsDlg();
          return true;
        case "AVS.MoveSpecRow":
          List<AVSRow> selectedSpecRows8 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows8.Count > 0)
            this.MoveSpecRow(selectedSpecRows8);
          return true;
        case "AVS.MoveSpecRowToChapter":
          List<AVSRow> selectedSpecRows9 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows9.Count > 0)
            this.MoveSpecRowToChapter(selectedSpecRows9, (AdditionalChapterSettings) null);
          return true;
        case "AVS.NumberPositions":
          if (!AvsConfig.General.AskRenumber || MessageBox.Show("Нумеровать непронумерованные позиции в спецификации?", "Нумерация позиций", MessageBoxButtons.YesNo) == DialogResult.Yes)
            this.avsDocument.RenumberPositions();
          return true;
        case "AVS.PageViewMode":
          this.SetViewMode(AVSViewMode.Page);
          return true;
        case "AVS.ParentProductsList":
          int num1 = (int) new ParentProductListDialog(this.AVSDocument).ShowDialog();
          return true;
        case "AVS.PasteBreak":
          this.PasteSymbol('\u0015'.ToString());
          return true;
        case "AVS.PasteNonBreakSpace":
          this.PasteSymbol('\u000E'.ToString());
          return true;
        case "AVS.Podbor.AddExisting":
          AVSDocumentContext contextOnlyOneNode1 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode1.Row != null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              ((IComponentSelectionCommandService) ServicesManager.GetService(typeof (IComponentSelectionCommandService))).AddExisting(sessionKeeper.Session, contextOnlyOneNode1.GetCurrentProductsInRow(), contextOnlyOneNode1.GetCurrentRelationGuidsInRow());
          }
          return true;
        case "AVS.Podbor.AddFromImbase":
          AVSDocumentContext contextOnlyOneNode2 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode2.Row != null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              ((IComponentSelectionCommandService) ServicesManager.GetService(typeof (IComponentSelectionCommandService))).AddFromImbase(sessionKeeper.Session, contextOnlyOneNode2.GetCurrentProductsInRow(), contextOnlyOneNode2.GetCurrentRelationGuidsInRow());
          }
          return true;
        case "AVS.Podbor.CreateNew":
          AVSDocumentContext contextOnlyOneNode3 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode3.Row != null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              ((IComponentSelectionCommandService) ServicesManager.GetService(typeof (IComponentSelectionCommandService))).CreateNew(sessionKeeper.Session, contextOnlyOneNode3.GetCurrentProductsInRow(), contextOnlyOneNode3.GetCurrentRelationGuidsInRow());
          }
          return true;
        case "AVS.Podbor.ListModeForRow":
          AVSDocumentContext contextOnlyOneNode4 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode4.Row != null)
            contextOnlyOneNode4.Row.LimitAndNominalValueMode = LimitAndNominalValueMode.List;
          return true;
        case "AVS.Podbor.RangeModeForRow":
          AVSDocumentContext contextOnlyOneNode5 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode5.Row != null)
            contextOnlyOneNode5.Row.LimitAndNominalValueMode = LimitAndNominalValueMode.Range;
          return true;
        case "AVS.Podbor.Reset":
          AVSDocumentContext contextOnlyOneNode6 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode6.Row != null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              ((IComponentSelectionCommandService) ServicesManager.GetService(typeof (IComponentSelectionCommandService))).Reset(sessionKeeper.Session, contextOnlyOneNode6.GetCurrentProductsInRow(), contextOnlyOneNode6.GetCurrentRelationGuidsInRow());
          }
          return true;
        case "AVS.Podbor.UseLimitValueModeForRow":
          AVSDocumentContext contextOnlyOneNode7 = this.GetAVSDocumentContext_OnlyOneNode();
          if (contextOnlyOneNode7.Row != null)
            contextOnlyOneNode7.Row.LimitAndNominalValueMode = LimitAndNominalValueMode.UseLimitValuesOnly;
          return true;
        case "AVS.ProductsList":
          int num2 = (int) new ProductsListDialog(this.AVSDocument).ShowDialog();
          return true;
        case "AVS.Properties":
          this.ShowAVSSetupDlg();
          this.avsDocument.UpdateVariableDataCaptions();
          this.avsDocument.UpdateNameDocCells(new List<AvsRowAttributeInfo>(), false);
          this.Document.UpdateLayout(true, true);
          this.avsDocument.Check_ChangesPage(true);
          return true;
        case "AVS.Property":
          this.BottomPanelType = this.BottomPanelType != AVSWindow.enumBottomPanelType.SelectedRowProperties ? AVSWindow.enumBottomPanelType.SelectedRowProperties : AVSWindow.enumBottomPanelType.None;
          commandState.Checked = this.BottomPanelType == AVSWindow.enumBottomPanelType.SelectedRowProperties;
          return true;
        case "AVS.RefreshFormatAndSmotri":
          this.avsDocument.ReloadFormatAttributeInEntireSpecificationFromDB();
          return true;
        case "AVS.RefreshMass":
          Dictionary<AVSRow, List<SpecRowCheckMessage>> dictionary = new Dictionary<AVSRow, List<SpecRowCheckMessage>>();
          this.avsDocument.UpdateMass(dictionary);
          ErrorsUserControl errorsUserControl = this.ErrorsUserControl;
          List<ImErrorMessage> errorRows1 = errorsUserControl.ErrorRows;
          List<ImErrorMessage> errorRows2 = (errorRows1 != null ? errorRows1.Except<ImErrorMessage>(errorsUserControl.ErrorRows.Where<ImErrorMessage>((System.Func<ImErrorMessage, bool>) (er =>
          {
            if (!(er is AVSRowErrorMessage avsRowErrorMessage2))
              return false;
            return avsRowErrorMessage2.ErrorType == AVSCheckType.MassCalc || avsRowErrorMessage2.ErrorType == AVSCheckType.EmptyPosition || avsRowErrorMessage2.ErrorType == AVSCheckType.EmptyCount;
          }))).ToList<ImErrorMessage>() : (List<ImErrorMessage>) null) ?? new List<ImErrorMessage>();
          if (dictionary.Count > 0)
          {
            errorRows2.AddRange((IEnumerable<ImErrorMessage>) AVSRowErrorMessage.CreateMessages(dictionary));
            errorsUserControl.Show(errorRows2);
          }
          else
            errorsUserControl.ErrorRows = errorRows2;
          return true;
        case "AVS.RemarkAttributes":
          int num3 = (int) RemarkAttributesForm.Execute(this.avsDocument.NoteFieldSettingsObjectID, AvsIDCache.Attr_NoteFieldSettings, this.ReadOnly);
          return true;
        case "AVS.RemoveAdditionalPages":
          DocumentMenuHelper.RemoveAdditionalPage(this.Document.DocumentControl);
          return true;
        case "AVS.ReplaceDocInSpecRow":
        case "AVS.ReplaceSpecRow":
          DocumentTreeNode[] commandContext8 = this.GetCommandContext();
          if (commandContext8 != null && commandContext8.Length == 1)
            this.ContextReplaceRow(commandContext8[0], AVSWindow.ReplaceRowMode.ReplaceObject);
          else if (commandContext8 == null || commandContext8.Length == 0)
            this.ContextReplaceRow((DocumentTreeNode) null, AVSWindow.ReplaceRowMode.ReplaceObject);
          return true;
        case "AVS.ReplaceSpecRowFromImbase":
          DocumentTreeNode[] commandContext9 = this.GetCommandContext();
          if (commandContext9 != null && commandContext9.Length == 1)
            this.ContextReplaceRow(commandContext9[0], AVSWindow.ReplaceRowMode.ReplaceObjectFromImbase);
          else if (commandContext9 == null || commandContext9.Length == 0)
            this.ContextReplaceRow((DocumentTreeNode) null, AVSWindow.ReplaceRowMode.ReplaceObjectFromImbase);
          return true;
        case "AVS.ReplaceSpecRowVersion":
          DocumentTreeNode[] commandContext10 = this.GetCommandContext();
          if (commandContext10 != null && commandContext10.Length == 1)
            this.ContextReplaceRow(commandContext10[0], AVSWindow.ReplaceRowMode.ReplaceVersion);
          else if (commandContext10 == null || commandContext10.Length == 0)
            this.ContextReplaceRow((DocumentTreeNode) null, AVSWindow.ReplaceRowMode.ReplaceVersion);
          return true;
        case "AVS.ReplaceTemplate":
          IDBTypedObjectID template = this.ReplaceTemplate(DocIDCache.ObjType_ConstructorDocumentsTemplate);
          if (template != null)
            this.avsDocument.ReplaceTemplate(template);
          return true;
        case "AVS.RowDown":
          this.Context_MoveSpecRow(false);
          return true;
        case "AVS.RowProperties":
          this.ShowRowPropertyGrid(true);
          return true;
        case "AVS.RowUp":
          this.Context_MoveSpecRow(true);
          return true;
        case "AVS.SetOccurenceKey":
          List<AVSRow> allRows = this.avsDocument.GetAllRows(true, true);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            foreach (AVSRow avsRow in allRows)
            {
              Guid guid = Guid.NewGuid();
              foreach (RelationAttributeValuesCache relation in avsRow.Relations)
              {
                if (relation.RelationType == AvsIDCache.Relation_Project)
                {
                  object[] relationAttributeValue = DBObjectHelper.GetRelationAttributeValue(sessionKeeper.Session, relation.RelationId, AvsIDCache.Attr_OccurenceKey);
                  if (relationAttributeValue == null || relationAttributeValue.Length == 0 || relationAttributeValue[0] is DBNull)
                    DBObjectHelper.SetRelationAttributeValue(sessionKeeper.Session, relation.RelationId, AvsIDCache.Attr_OccurenceKey, new object[1]
                    {
                      (object) guid
                    });
                }
              }
            }
          }
          return true;
        case "AVS.SetupAVSTemplates":
          if (this.avsDocument != null)
          {
            AVSDocumentForm? form = new AVSDocumentForm?(this.avsDocument.AvsDocumentForm);
            if (this.avsDocument.Document.IsTemplate)
              form = new AVSDocumentForm?();
            AVSWindow.ShowSetupAVSTemplates(this.avsDocument.Document.IsTemplate ? this.avsDocument.DocumentID : this.avsDocument.AVSDocumentTemplateID, form);
          }
          else
            AVSWindow.ShowSetupAVSTemplates();
          return true;
        case "AVS.SetupNumberingSchema":
          this.ShowSetupPositionsNumberingDlg();
          return true;
        case "AVS.ShowAllDocRows":
          if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
            this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.Create);
          return true;
        case "AVS.ShowEmptySections":
          this.avsDocument.UpdateViewNodes(false, false, false, true, false, EmptyRowUpdateMode.DontChange);
          this.avsDocument.UpdateVariableDataCaptions();
          if (this.avsDocument.VariableDataChapter != null && this.avsDocument.VariableDataChapter.DocNode != null)
            this.avsDocument.VariableDataChapter.DocNode.UpdateLayout(true);
          return true;
        case "AVS.ShowSameChapters":
          this.AVSDocument.VariableDataChapter_FormA.HideSameProductChapters = false;
          this.AVSDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          return true;
        case "AVS.SkipLinesSetup":
          this.ShowSetupSkipLinesDlg();
          return true;
        case "AVS.Sort":
          this.avsDocument.ResortSpecification(true, true);
          return true;
        case "AVS.SortAfter":
          List<AVSRow> selectedSpecRows10 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows10.Count == 1 && selectedSpecRows10[0].Section != null)
          {
            int index = selectedSpecRows10[0].Index;
            if (index != -1 && index > 0)
              selectedSpecRows10[0].SortAfterRow = selectedSpecRows10[0].Section.Rows[index - 1];
          }
          return true;
        case "AVS.SortBefore":
          List<AVSRow> selectedSpecRows11 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows11.Count == 1 && selectedSpecRows11[0].Section != null)
          {
            int index = selectedSpecRows11[0].Index;
            if (index != -1 && index < selectedSpecRows11[0].Section.Rows.Count - 1)
              selectedSpecRows11[0].SortBeforeRow = selectedSpecRows11[0].Section.Rows[index + 1];
          }
          return true;
        case "AVS.SortRazdel":
          DocumentTreeNode[] commandContext11 = this.GetCommandContext();
          if (commandContext11 != null && commandContext11.Length == 1)
            this.avsDocument.ResortSpecificationSection(this.avsDocument.GetSection(commandContext11[0]));
          return true;
        case "AVS.SortingSchema":
          this.ShowSetupSortingDlg();
          return true;
        case "AVS.SpecSectionsSetup":
          this.ShowSetupSectionsDlg(this.avsDocument.AVSDocumentTemplateID);
          return true;
        case "AVS.SpecificationForm":
          SelectAVSDocumentForm selectAvsDocumentForm = new SelectAVSDocumentForm(this.avsDocument.AvsDocumentForm, this.avsDocument.AVSDocType);
          if (selectAvsDocumentForm.ShowDialog() == DialogResult.OK && selectAvsDocumentForm.SelectedSpecificationForm != this.avsDocument.AvsDocumentForm)
          {
            this.avsDocument.ChangeGroupDocumentForm(selectAvsDocumentForm.SelectedSpecificationForm);
            this.avsDocument.IndexAVSDocument(true);
          }
          return true;
        case "AVS.SumPositionDesignation":
          this.avsDocument.SumPositionalDesignation();
          return true;
        case "AVS.UnGroupRowsByHeader":
          this.avsDocument.Document.SetDynamicGroupHeaderIsEnabled(false, true, true);
          this.avsDocument.UpdateRowsGroupHeaders();
          this.avsDocument.Document.UpdateLayout(true);
          if (this.AVSDocument.IsGridViewMode)
            this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) null);
          return true;
        case "AVS.UnHide":
          List<AVSRow> selectedSpecRows12 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows12.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index = 0; index < selectedSpecRows12.Count; ++index)
              {
                if (selectedSpecRows12[index].IsHiddenRow)
                  selectedSpecRows12[index].UnHide();
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              this.avsDocument.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
            }
          }
          return true;
        case "AVS.UndoFromNewPage":
          this.SetFromNewPageFlag(false);
          return true;
        case "AVS.UndoSkipLineAfter":
          List<AVSRow> selectedSpecRows13 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows13.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index = 0; index < selectedSpecRows13.Count; ++index)
              {
                if (selectedSpecRows13[index].SkipLinesAfter.HasValue)
                  selectedSpecRows13[index].SkipLinesAfter = new int?();
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
            }
          }
          else
          {
            SpecificationSection selectedSection = this.GetSelectedSection();
            if (selectedSection != null)
            {
              this.avsDocument.SuspendDocumentAndGridUpdates();
              try
              {
                int? nullable1 = selectedSection.SkipLinesAfter;
                if (nullable1.HasValue)
                {
                  SpecificationSection specificationSection = selectedSection;
                  nullable1 = new int?();
                  int? nullable2 = nullable1;
                  specificationSection.SkipLinesAfter = nullable2;
                }
              }
              finally
              {
                this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              }
            }
          }
          return true;
        case "AVS.UndoSkipLineBefore":
          List<AVSRow> selectedSpecRows14 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows14.Count > 0)
          {
            this.avsDocument.SuspendDocumentAndGridUpdates();
            try
            {
              for (int index = 0; index < selectedSpecRows14.Count; ++index)
              {
                if (selectedSpecRows14[index].SkipLinesBefore.HasValue)
                  selectedSpecRows14[index].SkipLinesBefore = new int?();
              }
            }
            finally
            {
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
            }
          }
          else
          {
            SpecificationSection selectedSection = this.GetSelectedSection();
            if (selectedSection != null)
            {
              this.avsDocument.SuspendDocumentAndGridUpdates();
              try
              {
                int? nullable3 = selectedSection.SkipLinesBefore;
                if (nullable3.HasValue)
                {
                  SpecificationSection specificationSection = selectedSection;
                  nullable3 = new int?();
                  int? nullable4 = nullable3;
                  specificationSection.SkipLinesBefore = nullable4;
                }
              }
              finally
              {
                this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
              }
            }
          }
          return true;
        case "AVS.UpdateDocumentStructure":
          this.avsDocument.ForceUpdateDocumentStructureWithFindEqualRows();
          return true;
        case "AVS.VersionAttributes":
          bool wasChangedHandler = this.IsSuspended_ObjectsWasChangedHandler;
          bool flag = false;
          try
          {
            this.IsSuspended_ObjectsWasChangedHandler = true;
            if (VersionAttributesForm.Execute(this.avsDocument.NoteFieldSettingsObjectID) == DialogResult.OK)
            {
              this.AVSDocument.LoadVersionAttributesHelper();
              VersionAttributesHelper attributesHelper = this.AVSDocument.versionAttributesHelper;
              flag = true;
              this.avsDocument.SuspendDocumentAndGridUpdates();
              this.AVSDocument.UpdateProductsByGroupID();
              this.avsDocument.UpdateVariableDataCaptions();
            }
          }
          finally
          {
            this.IsSuspended_ObjectsWasChangedHandler = wasChangedHandler;
            if (flag)
              this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
          }
          return true;
        case "AVSParametersCard":
          List<long> selectedProductsB = this.GetSelectedProductsB();
          if (selectedProductsB.Count > 0)
          {
            int num4 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, selectedProductsB[0]);
          }
          return true;
        case "AdminCancelChanges":
          this.DoAdminCancelChangesSelectedItems();
          this.UpdateNavigatorMenu(false);
          return true;
        case "CancelChanges":
          this.DoCancelChangesSelectedItems();
          this.UpdateNavigatorMenu(false);
          return true;
        case "Copy":
          this.CopyToClipboardCommand(this.GetCommandContext(), true);
          return true;
        case "CopySelectedToExcel":
          if (this.ViewMode == AVSViewMode.Grid)
          {
            this.virtualTree.CopySelectedToExcel();
            return true;
          }
          break;
        case "Cut":
          DocumentTreeNode[] commandContext12 = this.GetCommandContext();
          this.CopyToClipboardCommand(commandContext12, true);
          this.DeleteCommand(commandContext12, true, true);
          return true;
        case "Find":
          this.ShowFindOrReplaceTextDialog(false);
          return true;
        case "FindNext":
          this.FindNext();
          return true;
        case "PDM.CreateSubstitutesGroup":
          if (this._pdmClient == null)
            return true;
          long firstArticle1 = 0;
          List<ProductInfo> selectedProducts1 = this.GetSelectedProducts();
          if (selectedProducts1.Count > 0)
          {
            firstArticle1 = selectedProducts1[0].Id;
            if (!this.CheckSelectedRelations())
              return true;
          }
          ISelectedItems items1;
          System.IServiceProvider viewServices1;
          this.PrepareItemsServices(firstArticle1, out items1, out viewServices1);
          long desiredGroupNumber = -1;
          List<AVSRow> selectedSpecRows15 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows15 != null && selectedSpecRows15.Count > 0 && selectedSpecRows15[0].InCommonData_AV)
          {
            AVSDocument avsDocument = this.avsDocument;
            desiredGroupNumber = avsDocument != null ? avsDocument.GetDesiredSubstituteGroupNumber(selectedSpecRows15) : -1L;
          }
          this.SetupSubstituteEdior(viewServices1, selectedProducts1);
          this._pdmClient.CreateSubstitutesGroup((object) items1, viewServices1, (object) null, desiredGroupNumber);
          return true;
        case "PDM.DeleteSubstitutesGroup":
          if (this._pdmClient == null)
            return true;
          ISelectedItems selectedItems = this.GetSelectedItems(this.GetDefaultArticleID());
          System.IServiceProvider allowableSubstitutions = this.CreateServiceProviderForRemoveAllAllowableSubstitutions();
          this.avsDocument.needReloadDopZamenText = true;
          this._pdmClient.DeleteSubstitutesGroup((object) selectedItems, allowableSubstitutions, (object) null);
          this.avsDocument.UpdateDopzamenText();
          return true;
        case "PDM.EditSubstitutesGroup":
          if (this._pdmClient == null)
            return true;
          long firstArticle2 = 0;
          List<ProductInfo> selectedProducts2 = this.GetSelectedProducts();
          if (selectedProducts2.Count > 0 && !this.CheckSelectedRelations())
            return true;
          if (selectedProducts2.Count > 0)
            firstArticle2 = selectedProducts2[0].Id;
          if ((this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && selectedProducts2.Count != 1)
          {
            using (SelectProductForm selectProductForm2 = new SelectProductForm(this.avsDocument, selectedProducts2, false, true))
            {
              if (selectProductForm2.ShowDialog() != DialogResult.OK)
                return true;
              firstArticle2 = selectProductForm2.SelectedProductId;
            }
          }
          ISelectedItems items2;
          System.IServiceProvider viewServices2;
          this.PrepareItemsServices(firstArticle2, out items2, out viewServices2);
          this.SetupSubstituteEdior(viewServices2, selectedProducts2);
          this._pdmClient.EditSubstitutesGroup((object) items2, viewServices2, (object) null);
          return true;
        case "PDM.MakeActualSubstitute":
          if (this._pdmClient == null || !this.CheckSelectedRelations())
            return true;
          ISelectedItems items3;
          System.IServiceProvider viewServices3;
          this.PrepareItemsServices(this.GetDefaultArticleID(), out items3, out viewServices3, false);
          this._pdmClient.MakeActualSubstitute((object) items3, viewServices3, (object) null);
          return true;
        case "ParametersCard":
          List<AVSRow> selectedSpecRows16 = this.GetSelectedSpecRows(false);
          if (selectedSpecRows16.Count > 0)
          {
            int num5 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, selectedSpecRows16[0].ObjectId);
          }
          return true;
        case "Paste":
          DocumentTreeNode[] commandContext13 = this.GetCommandContext();
          if (commandContext13.Length >= 1)
          {
            int num6 = this.PasteFromClipboardCommand(commandContext13[0]) ? 1 : 0;
            if (num6 != 0)
              this.Document.UpdateLayout(false, true);
            if (num6 != 0 && this.avsDocument != null)
              this.avsDocument.UpdateViewNodes(false, false, false, false, true, EmptyRowUpdateMode.DontChange);
          }
          return true;
        case "Replace":
          this.ShowFindOrReplaceTextDialog(true);
          return true;
        case "SaveChanges":
          this.DoSaveChangesSelectedItems();
          this.UpdateNavigatorMenu(false);
          return true;
        case "SelectAll":
          if (this.DocumentControl != null && this.DocumentControl.Document != null)
          {
            List<DocumentTreeNode> documentTreeNodeList = this.SelectAll((DocumentTreeNode) this.DocumentControl.Document);
            if (documentTreeNodeList.Count > 0)
            {
              if (this.ViewMode == AVSViewMode.Page)
                this.DocumentControl.SetSelection(documentTreeNodeList, true, false);
              else
                this.virtualTree.Selection = (IList) AVSWindow.GetListNodesForDocNodes(documentTreeNodeList);
            }
          }
          return true;
      }
      ExternalAVSCommand externalAvsCommand;
      if (this.ExternalAVSCommands.TryGetValue(commandState.CommandName, out externalAvsCommand) && externalAvsCommand != null && externalAvsCommand.CommandHandler != null)
      {
        externalAvsCommand.CommandHandler((object) this, EventArgs.Empty);
        return true;
      }
      if (AVSPlugin.Instance.ExternalAVSCommands.TryGetValue(commandState.CommandName, out externalAvsCommand) && externalAvsCommand != null && externalAvsCommand.CommandHandler != null)
      {
        externalAvsCommand.CommandHandler((object) this, EventArgs.Empty);
        return true;
      }
      if (base.Execute(commandState))
        return true;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return true;
    }
    return false;
  }

  private void SetupSubstituteEdior(System.IServiceProvider _context, List<ProductInfo> articles)
  {
    if (!this.avsDocument.IsFormA && !this.avsDocument.IsFormB && this.avsDocument.AvsDocumentForm != AVSDocumentForm.V)
      return;
    PDMSubstitutesEditorOptionsHolder service = _context.GetService(typeof (PDMSubstitutesEditorOptionsHolder)) as PDMSubstitutesEditorOptionsHolder;
    if (this.avsDocument.IsFormA)
    {
      if (this.avsDocument.ProductsAreDifferent())
      {
        service.Mode = PDMSubstitutesEditorMode.DialogMultiInstances;
        service.Articles = (List<long>) null;
      }
      else
        service.Mode = PDMSubstitutesEditorMode.Default;
    }
    else if (articles.Count > 1)
      service.Articles = AVSDocument.GetProductIds(articles);
    else
      service.Articles = (List<long>) null;
  }

  private void ForceExcludeClassNameFromGroupCaption(List<AVSRow> rows, bool value)
  {
    AttributeValues attributeValues = new AttributeValues(AvsIDCache.Attr_GroupWithoutClass, (object) value);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (AVSRow row in rows)
      {
        if (!row.ObjectId.IsUndefinedId())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(row.ObjectId, true);
          DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, dbObject.ObjectID, out string _);
          bool flag = false;
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == 0L)
          {
            dbObject = dbObject.CheckOut();
            flag = true;
          }
          List<AVSRow> avsRowsByObjectId = this.avsDocument.GetAvsRowsByObjectId(row.ObjectId);
          try
          {
            row.SetFieldValue(this.AVSDocument.Attr_GroupWithoutClass, -1, -1, (List<RelationAttributeValuesCache>) null, (object) value, true, false, false, false, true, true, originalAttribute: true);
            foreach (AVSRow avsRow in avsRowsByObjectId)
              avsRow.SetFieldValue(this.AVSDocument.Attr_GroupWithoutClass, -1, -1, (List<RelationAttributeValuesCache>) null, (object) value, false, false, false, false, true, true, originalAttribute: true);
          }
          finally
          {
            if (flag)
              dbObject.CheckIn();
          }
          foreach (AVSRow avsRow in avsRowsByObjectId)
            avsRow.UpdateNameDocCellText(true, true);
        }
      }
    }
  }

  private void DisableCompleteWaitMode()
  {
    this.workCompleteWaitMode = false;
    if (AVSPlugin.Instance.CommandManager == null)
      return;
    AVSPlugin.Instance.CommandManager.QueryStatus();
  }

  private void DoCancelChangesSelectedItems()
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> newObjectIDs = new List<long>(selectedSpecRows.Count);
      List<long> objectIDs = new List<long>(selectedSpecRows.Count);
      if (selectedSpecRows.Count > 1)
      {
        try
        {
          if (IMMessageBox.Show("Отмена изменений", $"Отменить изменения {selectedSpecRows.Count.ToString()} объект(ов)?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          bool flag = false;
          long num = 0;
          IDBObject dbObject1 = (IDBObject) null;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(avsRow.ObjectId);
            if (dbObject2 != null)
            {
              long checkoutBy = dbObject2.CheckoutBy;
              if (checkoutBy == sessionKeeper.Session.UserID)
              {
                long objectId = dbObject2.ObjectID;
                dbObject2.CancelChanges();
                if (dbObject2.ObjectID != objectId)
                {
                  newObjectIDs.Add(dbObject2.ObjectID);
                  objectIDs.Add(objectId);
                }
              }
              else if (checkoutBy != 0L && !flag)
              {
                if (num != checkoutBy)
                {
                  dbObject1 = sessionKeeper.Session.GetObject(checkoutBy);
                  num = checkoutBy;
                }
                switch (IMMessageBox.Show("Отмена изменений", $"Объект \"{dbObject2.Caption}\" в данный момент редактируется пользователем \"{dbObject1.Caption}\".", MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort, IMMessageBoxImage.Question))
                {
                  case DialogResultAdv.Ignore:
                    continue;
                  case DialogResultAdv.IgnoreAll:
                    flag = true;
                    continue;
                  default:
                    return;
                }
              }
            }
          }
        }
        finally
        {
          if (newObjectIDs.Count > 0 && AVSPlugin.NotificationService != null)
            AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
      }
      else
      {
        IDBObject dbObject3 = sessionKeeper.Session.GetObject(selectedSpecRows[0].ObjectId);
        if (dbObject3 == null)
          return;
        if (dbObject3.CheckoutBy == sessionKeeper.Session.UserID)
        {
          if (IMMessageBox.Show("Отмена изменений", $"Отменить изменения объекта \"{dbObject3.Caption}\"?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          long objectId = dbObject3.ObjectID;
          dbObject3.CancelChanges();
          if (dbObject3.ObjectID == objectId)
            return;
          newObjectIDs.Add(dbObject3.ObjectID);
          objectIDs.Add(objectId);
          if (AVSPlugin.NotificationService == null)
            return;
          AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
        else if (dbObject3.CheckoutBy == 0L)
        {
          int num1 = (int) IMMessageBox.Show("Отмена изменений", $"Невозможно отменить изменения т.к. объект \"{dbObject3.Caption}\" не был взят вами на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
        else
        {
          IDBObject dbObject4 = sessionKeeper.Session.GetObject(dbObject3.CheckoutBy);
          int num2 = (int) IMMessageBox.Show("Отмена изменений", $"Объект \"{dbObject3.Caption}\" в данный момент редактируется пользователем \"{dbObject4.Caption}\".", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
      }
    }
  }

  private void DoAdminCancelChangesSelectedItems()
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> newObjectIDs = new List<long>(selectedSpecRows.Count);
      List<long> objectIDs = new List<long>(selectedSpecRows.Count);
      if (selectedSpecRows.Count > 1)
      {
        try
        {
          if (IMMessageBox.Show("Отмена изменений", $"Отменить чужие изменения {selectedSpecRows.Count.ToString()} объект(ов)?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(avsRow.ObjectId);
            if (dbObject != null && dbObject.CheckoutBy != 0L)
            {
              long objectId = dbObject.ObjectID;
              dbObject.CancelChanges(true);
              if (dbObject.ObjectID != objectId)
              {
                newObjectIDs.Add(dbObject.ObjectID);
                objectIDs.Add(objectId);
              }
            }
          }
        }
        finally
        {
          if (newObjectIDs.Count > 0 && AVSPlugin.NotificationService != null)
            AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
      }
      else
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(selectedSpecRows[0].ObjectId);
        if (dbObject == null)
          return;
        if (dbObject.CheckoutBy != 0L)
        {
          if (IMMessageBox.Show("Отмена изменений", $"Отменить чужие изменения объекта \"{dbObject.Caption}\"?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          long objectId = dbObject.ObjectID;
          dbObject.CancelChanges();
          if (dbObject.ObjectID == objectId)
            return;
          newObjectIDs.Add(dbObject.ObjectID);
          objectIDs.Add(objectId);
          if (AVSPlugin.NotificationService == null)
            return;
          AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
        else
        {
          int num = (int) IMMessageBox.Show("Отмена изменений", $"Невозможно отменить изменения т.к. объект \"{dbObject.Caption}\" не был взят на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
      }
    }
  }

  private void DoSaveChangesSelectedItems()
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>(selectedSpecRows.Count);
      if (selectedSpecRows.Count > 1)
      {
        try
        {
          if (IMMessageBox.Show("Сохранение изменений", $"Сохранить изменения {selectedSpecRows.Count.ToString()} объект(ов)?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          bool flag = false;
          long objectID = 0;
          IDBObject dbObject1 = (IDBObject) null;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(avsRow.ObjectId);
            if (dbObject2 != null)
            {
              long checkoutBy = dbObject2.CheckoutBy;
              if (checkoutBy == sessionKeeper.Session.UserID)
              {
                dbObject2.SaveChanges();
                longList.Add(dbObject2.ObjectID);
              }
              else if (checkoutBy != 0L && !flag)
              {
                if (objectID != checkoutBy)
                {
                  objectID = checkoutBy;
                  dbObject1 = sessionKeeper.Session.GetObject(objectID);
                }
                switch (IMMessageBox.Show("Сохранение изменений", $"Объект \"{dbObject2.Caption}\" в данный момент редактируется пользователем \"{dbObject1.Caption}\".", MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort, IMMessageBoxImage.Question))
                {
                  case DialogResultAdv.Ignore:
                    continue;
                  case DialogResultAdv.IgnoreAll:
                    flag = true;
                    continue;
                  default:
                    return;
                }
              }
            }
          }
        }
        finally
        {
          for (int index = 0; index < longList.Count; ++index)
            RecentObjectsNode.MRUObjects.Add(longList[index], ObjectAction.SaveChanges, DateTime.UtcNow);
        }
      }
      else
      {
        IDBObject dbObject3 = sessionKeeper.Session.GetObject(selectedSpecRows[0].ObjectId);
        if (dbObject3 == null)
          return;
        if (dbObject3.CheckoutBy == sessionKeeper.Session.UserID)
        {
          if (IMMessageBox.Show("Сохранение изменений", $"Сохранить изменения объекта \"{dbObject3.Caption}\"?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          dbObject3.SaveChanges();
          longList.Add(dbObject3.ObjectID);
          for (int index = 0; index < longList.Count; ++index)
            RecentObjectsNode.MRUObjects.Add(longList[index], ObjectAction.SaveChanges, DateTime.UtcNow);
        }
        else if (dbObject3.CheckoutBy == 0L)
        {
          int num1 = (int) IMMessageBox.Show("Сохранение изменений", $"Невозможно сохранить изменения т.к. объект \"{dbObject3.Caption}\" не был взят вами на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
        else
        {
          IDBObject dbObject4 = sessionKeeper.Session.GetObject(dbObject3.CheckoutBy);
          int num2 = (int) IMMessageBox.Show("Сохранение изменений", $"Объект \"{dbObject3.Caption}\" в данный момент редактируется пользователем \"{dbObject4.Caption}\".", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
      }
    }
  }

  private void DoCheckOutSelectedItems()
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
    if (selectedSpecRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> newObjectIDs = new List<long>(selectedSpecRows.Count);
      List<long> objectIDs = new List<long>(selectedSpecRows.Count);
      if (selectedSpecRows.Count > 1)
      {
        try
        {
          if (IMMessageBox.Show("Взятие на редактирование", $"Взять на редактирование {selectedSpecRows.Count.ToString()} объект(ов)?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          bool flag = false;
          long num = 0;
          IDBObject dbObject1 = (IDBObject) null;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(avsRow.ObjectId);
            if (dbObject2 != null && dbObject2.ObjectModifyMode != ObjectModifyModes.InBase)
            {
              long checkoutBy = dbObject2.CheckoutBy;
              if (checkoutBy == 0L)
              {
                long objectId = dbObject2.ObjectID;
                IDBObject dbObject3 = dbObject2.CheckOut();
                avsRow.SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) sessionKeeper.Session.UserID, false, false, false, true, false, false);
                if (dbObject3 != null)
                {
                  avsRow.ObjectId = dbObject3.ObjectID;
                  newObjectIDs.Add(dbObject3.ObjectID);
                  objectIDs.Add(objectId);
                }
              }
              else if (checkoutBy != sessionKeeper.Session.UserID && !flag)
              {
                if (num != checkoutBy)
                {
                  num = checkoutBy;
                  dbObject1 = sessionKeeper.Session.GetObject(checkoutBy);
                }
                switch (IMMessageBox.Show("Взятие на редактирование", $"Объект \"{dbObject2.Caption}\" в данный момент редактируется пользователем \"{dbObject1.Caption}\".", MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort, IMMessageBoxImage.Question))
                {
                  case DialogResultAdv.Ignore:
                    continue;
                  case DialogResultAdv.IgnoreAll:
                    flag = true;
                    continue;
                  default:
                    return;
                }
              }
            }
          }
        }
        finally
        {
          if (newObjectIDs.Count > 0 && AVSPlugin.NotificationService != null)
            AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
      }
      else
      {
        IDBObject dbObject4 = sessionKeeper.Session.GetObject(selectedSpecRows[0].ObjectId);
        if (dbObject4 == null)
          return;
        if (dbObject4.CheckoutBy == 0L)
        {
          if (IMMessageBox.Show("Взятие на редактирование", $"Взять на редактирование объект \"{dbObject4.Caption}\"?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          long objectId = dbObject4.ObjectID;
          if (dbObject4.ObjectModifyMode == ObjectModifyModes.InBase)
            return;
          IDBObject dbObject5 = dbObject4.CheckOut();
          selectedSpecRows[0].SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) sessionKeeper.Session.UserID, false, false, false, true, false, false);
          if (dbObject5 == null)
            return;
          selectedSpecRows[0].ObjectId = dbObject5.ObjectID;
          if (AVSPlugin.NotificationService == null)
            return;
          AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
          {
            objectId
          }, (IList<long>) new long[1]{ dbObject5.ObjectID }));
        }
        else if (dbObject4.CheckoutBy == sessionKeeper.Session.UserID)
        {
          int num1 = (int) IMMessageBox.Show("Взятие на редактирование", $"Объект \"{dbObject4.Caption}\" уже взят вами на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
        else
        {
          IDBObject dbObject6 = sessionKeeper.Session.GetObject(dbObject4.CheckoutBy);
          int num2 = (int) IMMessageBox.Show("Взятие на редактирование", $"Объект \"{dbObject4.Caption}\" в данный момент редактируется пользователем \"{dbObject6.Caption}\".", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
      }
    }
  }

  private void DoCheckInDocument()
  {
    if (IMMessageBox.Show("Завершение редактирования", $"Завершить редактирование текущего документа '{this.DocumentCaption}'?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
      return;
    this.avsDocument.SaveAVSDocumentToDB();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.DocumentID);
      if (dbObject1 == null)
        return;
      if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
      {
        long objectId1 = dbObject1.ObjectID;
        List<long> longList = new List<long>();
        List<long> objectIDs = new List<long>();
        dbObject1.CheckIn();
        long objectId2 = dbObject1.ObjectID;
        longList.Add(objectId2);
        if (this.AVSDocument.ProductIds != null)
        {
          foreach (long productId in this.AVSDocument.ProductIds)
            objectIDs.Add(productId);
        }
        objectIDs.Add(objectId1);
        if (AVSPlugin.NotificationService == null)
          return;
        AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs));
      }
      else if (dbObject1.CheckoutBy == 0L)
      {
        int num1 = (int) IMMessageBox.Show("Завершение редактирования", $"Невозможно завершить редактирование т.к. объект \"{dbObject1.Caption}\" не был взят вами на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
      }
      else
      {
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(dbObject1.CheckoutBy);
        int num2 = (int) IMMessageBox.Show("Завершение редактирования", $"Объект \"{dbObject1.Caption}\" в данный момент редактируется пользователем \"{dbObject2.Caption}\".", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
      }
    }
  }

  private void DoCheckInSelectedItems()
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
    if (selectedSpecRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> newObjectIDs = new List<long>(selectedSpecRows.Count);
      List<long> objectIDs = new List<long>(selectedSpecRows.Count);
      if (selectedSpecRows.Count > 1)
      {
        try
        {
          if (IMMessageBox.Show("Завершение редактирования", $"Завершить редактирование {selectedSpecRows.Count.ToString()} объект(ов)?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          bool flag = false;
          long num = 0;
          IDBObject dbObject1 = (IDBObject) null;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            IDBObject dbObject2 = sessionKeeper.Session.GetObject(avsRow.ObjectId);
            if (dbObject2 != null)
            {
              long checkoutBy = dbObject2.CheckoutBy;
              if (checkoutBy == sessionKeeper.Session.UserID)
              {
                long objectId = dbObject2.ObjectID;
                dbObject2.CheckIn();
                avsRow.SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) 0, false, false, false, true, false, false);
                if (dbObject2.ObjectID != dbObject2.ObjectID)
                {
                  avsRow.ObjectId = dbObject2.ObjectID;
                  objectIDs.Add(objectId);
                  newObjectIDs.Add(dbObject2.ObjectID);
                }
              }
              else if (checkoutBy != 0L && !flag)
              {
                if (num != checkoutBy)
                {
                  num = checkoutBy;
                  dbObject1 = sessionKeeper.Session.GetObject(checkoutBy);
                }
                switch (IMMessageBox.Show("Завершение редактирования", $"Объект \"{dbObject2.Caption}\" в данный момент редактируется пользователем \"{dbObject1.Caption}\".", MessageBoxButtonsAdv.Ignore_IgnoreAll_Abort, IMMessageBoxImage.Question))
                {
                  case DialogResultAdv.Ignore:
                    continue;
                  case DialogResultAdv.IgnoreAll:
                    flag = true;
                    continue;
                  default:
                    return;
                }
              }
            }
          }
        }
        finally
        {
          if (newObjectIDs.Count > 0 && AVSPlugin.NotificationService != null)
            AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
      }
      else
      {
        IDBObject dbObject3 = sessionKeeper.Session.GetObject(selectedSpecRows[0].ObjectId);
        if (dbObject3 == null)
          return;
        if (dbObject3.CheckoutBy == sessionKeeper.Session.UserID)
        {
          if (IMMessageBox.Show("Завершение редактирования", $"Завершить редактирование объекта \"{dbObject3.Caption}\"?", MessageBoxButtonsAdv.OKCancel, IMMessageBoxImage.Question) != DialogResultAdv.OK)
            return;
          long objectId = dbObject3.ObjectID;
          dbObject3.CheckIn();
          selectedSpecRows[0].SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) 0, false, false, false, true, false, false);
          if (dbObject3.ObjectID == objectId)
            return;
          selectedSpecRows[0].ObjectId = dbObject3.ObjectID;
          newObjectIDs.Add(dbObject3.ObjectID);
          objectIDs.Add(objectId);
          if (AVSPlugin.NotificationService == null)
            return;
          AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
        }
        else if (dbObject3.CheckoutBy == 0L)
        {
          int num1 = (int) IMMessageBox.Show("Завершение редактирования", $"Невозможно завершить редактирование т.к. объект \"{dbObject3.Caption}\" не был взят вами на редактирование", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
        else
        {
          IDBObject dbObject4 = sessionKeeper.Session.GetObject(dbObject3.CheckoutBy);
          int num2 = (int) IMMessageBox.Show("Завершение редактирования", $"Объект \"{dbObject3.Caption}\" в данный момент редактируется пользователем \"{dbObject4.Caption}\".", MessageBoxButtonsAdv.OK, IMMessageBoxImage.Information);
        }
      }
    }
  }

  private void SetFromNewPageFlag(bool onoffFlag)
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count > 0)
    {
      this.avsDocument.SuspendDocumentAndGridUpdates();
      try
      {
        for (int index = 0; index < selectedSpecRows.Count; ++index)
          selectedSpecRows[index].FromNewPage = new bool?(onoffFlag);
      }
      finally
      {
        this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
    }
    else
    {
      SpecificationSection selectedSection = this.GetSelectedSection();
      if (selectedSection != null)
      {
        this.avsDocument.SuspendDocumentAndGridUpdates();
        try
        {
          selectedSection.FromNewPage = new bool?(onoffFlag);
        }
        finally
        {
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        }
      }
      else
      {
        Chapter selectedChapter = this.GetSelectedChapter(true);
        if (selectedChapter == null)
          return;
        this.avsDocument.SuspendDocumentAndGridUpdates();
        try
        {
          selectedChapter.FromNewPage = new bool?(onoffFlag);
        }
        finally
        {
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        }
      }
    }
  }

  private void NotifyRelationsRemoved(IDictionary<long, RelInfo> removedItemIds)
  {
    if (removedItemIds.Count <= 0 || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsRemoved", (IList<long>) removedItemIds.Keys.ToList<long>(), (IList<long>) removedItemIds.Values.Select<RelInfo, long>((System.Func<RelInfo, long>) (v => v.ProjID)).ToList<long>(), (IList<int>) removedItemIds.Values.Select<RelInfo, int>((System.Func<RelInfo, int>) (v => v.ProjTypeID)).ToList<int>(), (IList<int>) removedItemIds.Values.Select<RelInfo, int>((System.Func<RelInfo, int>) (v => v.RelType)).ToList<int>(), NavigatorRelationCommand.Unknown);
    service.FireEvent((object) this, (NotificationEventArgs) e);
  }

  public void Context_GroupRows()
  {
    AVSRow row = (AVSRow) null;
    bool flag = true;
    long num = -1;
    this.avsDocument.SuspendDocumentAndGridUpdates();
    try
    {
      foreach (DocumentTreeNode selectedNode in this.DocumentControl.SelectedNodes)
      {
        AVSRow avsDocRow = this.AVSDocument.GetAvsDocRow(selectedNode);
        if (avsDocRow != null)
        {
          if (row == null)
          {
            row = avsDocRow;
            num = avsDocRow.ObjectId;
          }
          if (avsDocRow != row)
          {
            flag = false;
            if (num != avsDocRow.ObjectId)
              num = -1L;
          }
        }
      }
      if (row == null)
        return;
      SpecificationSection section = row.Section;
      int index = row.Index;
      if (num == -1L)
        throw new NotificationException("Выбраны записи с разными изделиями");
      if (flag)
      {
        row = new AVSRow(this.avsDocument, (RelationAttributeValuesCache) null, row.ObjectAttributesCache);
        section.InsertRow(index + 1, row);
      }
      List<AVSRow> avsRowList = new List<AVSRow>();
      foreach (DocumentTreeNode rowDocNode in new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) this.DocumentControl.SelectedNodes))
      {
        AVSRow avsDocRow = this.AVSDocument.GetAvsDocRow(rowDocNode);
        if (!avsRowList.Contains(avsDocRow))
          avsRowList.Add(avsDocRow);
        TextData cell = rowDocNode as TextData;
        int indexForCountCell = avsDocRow.GetProductIndexForCountCell(cell);
        if (indexForCountCell != -1 && avsDocRow != row)
        {
          int relationIndexForProduct1 = avsDocRow.GetRelationIndexForProduct(this.AVSDocument.GetProductInfoByIndex(indexForCountCell).Id);
          int relationIndexForProduct2 = row.GetRelationIndexForProduct(this.AVSDocument.GetProductInfoByIndex(indexForCountCell).Id);
          if (relationIndexForProduct1 != -1 && relationIndexForProduct2 == -1)
          {
            RelationAttributeValuesCache relation = avsDocRow.Relations[relationIndexForProduct1];
            if (relation != null)
            {
              avsDocRow.RemoveRelationData(avsDocRow.Relations, relationIndexForProduct1);
              if (avsDocRow.Relations.Count == 0)
                section.RemoveRow(avsDocRow, true, false, true, false, false);
              row.AddRowData(relation);
            }
          }
        }
      }
    }
    finally
    {
      this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      this.avsDocument.UpdateViewNodes(false, false, false, !this.IsSpecification, true, EmptyRowUpdateMode.DontChange);
      this.DocumentControl.SetSelection((DocumentTreeNode) row.DocNode, false, true);
    }
  }

  /// <summary>Добавить исполнение</summary>
  /// <param name="srcProductIndex">Индекс исполнения прототипа. -1 - без прототипа</param>
  public void AddProductVersion(int srcProductIndex)
  {
    if (this.avsDocument.IsSpecification)
    {
      long num;
      using (new SessionKeeper())
      {
        if (srcProductIndex >= 0)
        {
          IObjectCreatorService objectCreatorService = AVSPlugin.ServiceProvider == null ? ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService : AVSPlugin.ServiceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
          objectCreatorService.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.CreateObjectDlg_ProductCreate);
          this.templateProductIDForCreation = this.avsDocument.productsInfo[srcProductIndex].Id;
          try
          {
            num = objectCreatorService.CreateObjectByTemplateDialog(this.templateProductIDForCreation);
          }
          finally
          {
            this.templateProductIDForCreation = -1L;
            objectCreatorService.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.CreateObjectDlg_ProductCreate);
          }
        }
        else
        {
          this.templateProductIDForCreation = -1L;
          IObjectCreatorService objectCreatorService = AVSPlugin.ServiceProvider == null ? ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService : AVSPlugin.ServiceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
          objectCreatorService.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.CreateObjectDlg_ProductCreate);
          try
          {
            num = objectCreatorService.CreateObjectByTypeDialog(this.avsDocument.ProductType);
          }
          finally
          {
            objectCreatorService.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.CreateObjectDlg_ProductCreate);
          }
        }
      }
      if (num.IsUndefinedId())
        return;
      this.avsDocument.InsertProducts((IList<NewProductParams>) new NewProductParams[1]
      {
        new NewProductParams(num, -1, (string) null, (string) null, -1)
      });
    }
    else
    {
      int num = this.avsDocument.UseSameDesignationForProducts ? 0 : 1;
      for (int index = 0; index < this.avsDocument.productsInfo.Count; ++index)
      {
        string number = this.avsDocument.productsInfo[index].GetNumber(this.avsDocument.DocumentDesignation, this.avsDocument.UseSameDesignationForProducts);
        int intValue;
        if (number != null && number != "" && NumberParserAdvanced.TryParseInt32(number, out intValue))
        {
          if (intValue >= num)
            num = intValue + 1;
          else
            ++num;
        }
        else
          ++num;
      }
      EditProductCaptionForm productCaptionForm = new EditProductCaptionForm();
      productCaptionForm.ProductDesignationBase = this.avsDocument.BaseProductDesignation;
      productCaptionForm.ProductNumber = num.ToString("d2");
      productCaptionForm.Text = "Создание нового исполнения";
      if (productCaptionForm.ShowDialog() != DialogResult.OK)
        return;
      this.avsDocument.InsertProducts((IList<NewProductParams>) new NewProductParams[1]
      {
        new NewProductParams(-1L, srcProductIndex, productCaptionForm.ProductCaption, (string) null, this.avsDocument.productsInfo.Count)
      });
    }
  }

  /// <summary>Обработчик для диалога создания объекта при создании нового исполнения</summary>
  /// <param name="productID">Идентификатор версии объекта исполнения</param>
  private void CreateObjectDlg_ProductCreate(object sender, AfterDraftCreatedEventArgs e)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObj = sessionKeeper.Session.GetObject(e.ObjectID, false);
        if (dbObj == null)
          return;
        int num = this.avsDocument.UseSameDesignationForProducts ? 0 : 1;
        for (int index = 0; index < this.avsDocument.productsInfo.Count; ++index)
        {
          string number = this.avsDocument.productsInfo[index].GetNumber(this.avsDocument.DocumentDesignation, this.avsDocument.UseSameDesignationForProducts);
          int intValue;
          if (number != null && number != "" && NumberParserAdvanced.TryParseInt32(number, out intValue))
          {
            if (intValue >= num)
              num = intValue + 1;
            else
              ++num;
          }
          else
            ++num;
        }
        AttributeValues[] values;
        if (this.avsDocument.UseSameDesignationForProducts)
          values = new AttributeValues[4]
          {
            new AttributeValues(AvsIDCache.Attr_Designation, (object) $"{this.DocumentDesignation}-{num.ToString("d2")}"),
            new AttributeValues(AvsIDCache.Attr_Name, (object) this.DocumentName),
            new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.avsDocument.articleGroupID),
            new AttributeValues(AvsIDCache.Attr_ProductCode, (object) "")
          };
        else
          values = new AttributeValues[4]
          {
            new AttributeValues(AvsIDCache.Attr_Designation, (object) this.DocumentDesignation),
            new AttributeValues(AvsIDCache.Attr_Name, (object) this.DocumentName),
            new AttributeValues(AvsIDCache.Attr_ArticleGroupID, (object) this.avsDocument.articleGroupID),
            new AttributeValues(AvsIDCache.Attr_ProductCode, (object) num.ToString())
          };
        DBObjectHelper.SetDBAttributeValues(dbObj, values);
        IDBRelationCollection relationCollection1 = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
        IDBRelationCollection relationCollection2 = (IDBRelationCollection) null;
        relationCollection1.Create(e.ObjectID, this.DocumentID).SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(this.DocumentID))
        });
        List<AVSRow> rowList = new List<AVSRow>();
        if (this.templateProductIDForCreation == -1L)
        {
          if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
          {
            this.avsDocument.commonDataChapter.GetAllRowsList(false, false, rowList);
            for (int index1 = 0; index1 < this.avsDocument.rootChapters.Count; ++index1)
            {
              if (this.avsDocument.rootChapters[index1].IsAdditionalChapter)
              {
                for (int index2 = 0; index2 < this.avsDocument.rootChapters[index1].Chapters.Count; ++index2)
                {
                  if (this.avsDocument.rootChapters[index1].Chapters[index2].IsCommonDataChapter)
                    this.avsDocument.rootChapters[index1].Chapters[index2].GetAllRowsList(false, false, rowList);
                }
              }
            }
          }
        }
        else
        {
          Chapter chapter1 = this.avsDocument.commonDataChapter.GetChapter(AVSDocument.ObjID_SectionDocumentation);
          chapter1?.GetAllRowsList(false, false, rowList);
          for (int index3 = 0; index3 < this.avsDocument.rootChapters.Count; ++index3)
          {
            if (this.avsDocument.rootChapters[index3].IsAdditionalChapter)
            {
              for (int index4 = 0; index4 < this.avsDocument.rootChapters[index3].Chapters.Count; ++index4)
              {
                if (this.avsDocument.rootChapters[index3].Chapters[index4].IsCommonDataChapter)
                {
                  chapter1 = this.avsDocument.rootChapters[index3].Chapters[index4].GetChapter(AVSDocument.ObjID_SectionDocumentation);
                  chapter1?.GetAllRowsList(false, false, rowList);
                }
              }
            }
          }
          if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
          {
            if (this.avsDocument.variableDataChapter_FormV != null)
              chapter1 = this.avsDocument.variableDataChapter_FormV.GetChapter(AVSDocument.ObjID_SectionDocumentation);
            chapter1?.GetAllRowsList(false, false, rowList);
            for (int index5 = 0; index5 < this.avsDocument.rootChapters.Count; ++index5)
            {
              if (this.avsDocument.rootChapters[index5].IsAdditionalChapter)
              {
                for (int index6 = 0; index6 < this.avsDocument.rootChapters[index5].Chapters.Count; ++index6)
                {
                  if (this.avsDocument.rootChapters[index5].Chapters[index6].IsVariableDataChapter)
                  {
                    chapter1 = this.avsDocument.rootChapters[index5].Chapters[index6].GetChapter(AVSDocument.ObjID_SectionDocumentation);
                    chapter1?.GetAllRowsList(false, false, rowList);
                  }
                }
              }
            }
          }
          if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
          {
            if (this.avsDocument.variableDataChapter_FormA != null)
            {
              if (this.avsDocument.variableDataChapter_FormA.GetChapter(this.templateProductIDForCreation) is ProductVariableDataChapter chapter2)
                chapter1 = chapter2.GetChapter(AVSDocument.ObjID_SectionDocumentation);
              chapter1?.GetAllRowsList(false, false, rowList);
            }
            for (int index7 = 0; index7 < this.avsDocument.rootChapters.Count; ++index7)
            {
              if (this.avsDocument.rootChapters[index7].IsAdditionalChapter)
              {
                for (int index8 = 0; index8 < this.avsDocument.rootChapters[index7].Chapters.Count; ++index8)
                {
                  if (this.avsDocument.rootChapters[index7].Chapters[index8] is VariableDataChapterFormA chapter4)
                  {
                    if (chapter4.GetChapter(this.templateProductIDForCreation) is ProductVariableDataChapter chapter3)
                      chapter1 = chapter3.GetChapter(AVSDocument.ObjID_SectionDocumentation);
                    chapter1?.GetAllRowsList(false, false, rowList);
                  }
                }
              }
            }
          }
        }
        long productId = this.templateProductIDForCreation;
        if (productId == -1L)
          productId = this.avsDocument.productsInfo[0].Id;
        for (int index = 0; index < rowList.Count; ++index)
        {
          int relationIndexForProduct = rowList[index].GetRelationIndexForProduct(productId);
          if (relationIndexForProduct != -1)
          {
            NewRelationProperties relationProperties = new NewRelationProperties(rowList[index].Relations[relationIndexForProduct].RelationId, e.ObjectID, rowList[index].Object_F_ID, DateTime.MinValue, DateTime.MaxValue, rowList[index].ObjectId);
            if (rowList[index].RelType == AvsIDCache.Relation_Document)
            {
              if (sessionKeeper.Session.GetRelation(e.ObjectID, rowList[index].ObjectId, true) == null)
                AVSDocument.CreateDocRelationWithLockPDMHandler(relationCollection1, relationProperties);
            }
            else
            {
              if (relationCollection2 == null)
                relationCollection2 = sessionKeeper.Session.GetRelationCollection(rowList[index].RelType, this.FiltrationOwnerID);
              object obj = relationCollection2.Create(relationProperties).GetAttributeByID(AvsIDCache.Attr_SortIndex).Values[0];
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary> Признак того, что выбран хотя бы один объект (изделие, документ и т.п.) </summary>
  public bool ObjectIsSelected
  {
    [DebuggerStepThrough] get
    {
      this.InitQueryCache();
      return this._objectIsSelected;
    }
  }

  private void UpdateObjectIsSelected()
  {
    switch (this.viewMode)
    {
      case AVSViewMode.Page:
        if (this._queryDocumentNodeList == null)
          break;
        using (List<DocumentTreeNode>.Enumerator enumerator = this._queryDocumentNodeList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            DocumentTreeNode current = enumerator.Current;
            if (this.avsDocument != null)
            {
              AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(current);
              if (avsDocRow != null && !avsDocRow.ObjectId.IsUndefinedId())
              {
                this._objectIsSelected = true;
                break;
              }
            }
            this._objectIsSelected = false;
          }
          break;
        }
      case AVSViewMode.Grid:
        IEnumerator enumerator1 = this.virtualTree.Selection.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            IVirtualTreeItem current = (IVirtualTreeItem) enumerator1.Current;
            if (current != null && current is AVSRow && ((AVSRow) current).RelId != -1L)
            {
              this._objectIsSelected = true;
              break;
            }
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      default:
        this._objectIsSelected = false;
        break;
    }
  }

  /// <summary>Выбрано исполнение</summary>
  /// <returns></returns>
  public bool IsVersionSelected()
  {
    if (this.ViewMode == AVSViewMode.Page)
    {
      DocumentTreeNode[] selectedNodes = this.DocumentControl.GetSelectedNodes();
      if (selectedNodes != null && selectedNodes.Length != 0)
      {
        foreach (DocumentTreeNode docNode in selectedNodes)
        {
          if (AVSDocument.FindParentProductVariableDocNode(docNode) != null)
            return true;
        }
      }
    }
    else
    {
      foreach (IVirtualTreeItem virtualTreeItem in (IEnumerable) this.virtualTree.Selection)
      {
        if (virtualTreeItem is ProductVariableDataChapter)
          return true;
      }
    }
    return false;
  }

  public override void InitQueryCache()
  {
    if (this._IsQueryChacheIsInit)
      return;
    base.InitQueryCache();
    this.UpdateObjectIsSelected();
  }

  public override void EndQuery()
  {
    base.EndQuery();
    this._objectIsSelected = false;
  }

  private bool CanPasteSymbol()
  {
    if (this.ReadOnly)
      return false;
    DocumentTreeNode selectedNode = this.DocumentControl.SelectedNode;
    if (!(selectedNode is TextBoxElement textBoxElement) || !(textBoxElement.InPlaceEditorControl is ImRtfEditor))
      return false;
    bool flag = false;
    if (AVSDocument.IsSpecRowDocNodeChild(selectedNode))
      flag = selectedNode.Name != "Обозначение" && this.ViewMode == AVSViewMode.Page && textBoxElement.InPlaceEditorActive && !textBoxElement.ReadOnlyFormating;
    else if (selectedNode.Name == "Наименование" && this.AVSDocument.AvsDocumentForm == AVSDocumentForm.A && AVSDocument.FindParentChapterDocNode(selectedNode, false) is TableData parentChapterDocNode)
      flag = this.AVSDocument.GetChapterForDocNode(parentChapterDocNode, AVSDocumentForm.A, false, false) is VariableDataChapterFormA;
    return flag;
  }

  /// <summary>Проверить статус команды</summary>
  /// <param name="commandState">Состояние команды</param>
  /// <returns>true, если команда найдена</returns>
  public override bool QueryStatus(ICommandState commandState)
  {
    if (commandState != null && this.DocumentControl != null && this.avsDocument != null)
    {
      if (this.Document != null)
      {
        try
        {
          bool backThreadIsActive = this.Document.BackThreadIsActive;
          bool cacheHasLockedNodes = this.DocumentControl.QueryCache_HasLockedNodes;
          switch (commandState.CommandName)
          {
            case "AVS.AddAdditionalChapter":
            case "AVS.AddSpecSection":
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.AddDopComplect":
              commandState.Visible = true;
              commandState.Enabled = !this.ReadOnly && this.CanAddAvsRowInSelectedContext() && !backThreadIsActive && this.IsSpecification;
              return true;
            case "AVS.AddGroupSpecRowFromImbase":
            case "AVS.AddSpecRowFromImbase":
              commandState.Visible = true;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && AVSPlugin.ImbaseSelector != null && this.DocumentControl.SelectedNodes != null && this.DocumentControl.SelectedNodes.Count <= 1;
              if (!commandState.Enabled)
                return true;
              if (!this.avsDocument.IsSpecification)
              {
                commandState.Enabled = true;
                return true;
              }
              if (commandState.Enabled)
              {
                SpecificationSection section = this.DocumentControl.SelectedNodes == null || this.DocumentControl.SelectedNodes.Count <= 0 ? (SpecificationSection) null : this.avsDocument.GetSection(this.DocumentControl.SelectedNodes[0]);
                if (section == null)
                  return true;
                if (!SpecificationSectionInfo.Cached)
                {
                  using (SessionKeeper sessionKeeper = new SessionKeeper())
                    SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
                }
                List<SpecificationSectionInfo> documentSections = this.AVSDocument.GetAllowableDocumentSections();
                for (int index = 0; index < documentSections.Count; ++index)
                {
                  if (documentSections[index].SectionID == section.SectionID)
                  {
                    commandState.Enabled = documentSections[index].ImBaseCatalogs != null && documentSections[index].ImBaseCatalogs.Length != 0 && !documentSections[index].ImBaseCatalogs[0].IsUndefinedId();
                    return true;
                  }
                }
                commandState.Enabled = false;
              }
              return true;
            case "AVS.AddIspoln":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Enabled = !this.ReadOnly && commandState.Visible && !backThreadIsActive && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Mirror && this.avsDocument.AllowableGroupDocument;
              return true;
            case "AVS.AddLRIRecord":
              TableData parentLriRowDocNode = this.avsDocument.FindParentLRIRowDocNode(this.GetCommandContext_OnlyOneNode()) as TableData;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && parentLriRowDocNode == null && this.avsDocument.HasLRITemplates;
              commandState.Visible = parentLriRowDocNode == null;
              return true;
            case "AVS.AddLRIRecord_After":
              TableData tableData1 = (TableData) null;
              List<DocumentTreeNode> docRows1 = this.avsDocument.GetAVSRowsAndDocRows((IEnumerable<DocumentTreeNode>) this.GetCommandContext()).docRows;
              if (docRows1.Count == 1)
                tableData1 = this.avsDocument.FindParentLRIRowDocNode(docRows1[0]) as TableData;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && tableData1 != null && this.avsDocument.HasLRITemplates;
              commandState.Visible = tableData1 != null;
              return true;
            case "AVS.AddLRIRecord_Before":
              TableData tableData2 = (TableData) null;
              List<DocumentTreeNode> docRows2 = this.avsDocument.GetAVSRowsAndDocRows((IEnumerable<DocumentTreeNode>) this.GetCommandContext()).docRows;
              if (docRows2.Count == 1)
                tableData2 = this.avsDocument.FindParentLRIRowDocNode(docRows2[0]) as TableData;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && tableData2 != null && this.avsDocument.HasLRITemplates;
              commandState.Visible = tableData2 != null;
              return true;
            case "AVS.AddNewSpecRow":
            case "AVS.AddSpecRow":
              commandState.Visible = true;
              commandState.Enabled = !this.ReadOnly && this.CanAddAvsRowInSelectedContext() && !backThreadIsActive;
              return true;
            case "AVS.AddOtherRecordTypes":
              SpecificationSection selectedSection1 = this.GetSelectedSection();
              commandState.Visible = true;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && selectedSection1 != null;
              return true;
            case "AVS.AddSkipLineAfter":
            case "AVS.AddSkipLineBefore":
              DocumentTreeNode selectedNode1 = this.DocumentControl.SelectedNode;
              commandState.Enabled = !this.ReadOnly && selectedNode1 != null && this.ViewMode == AVSViewMode.Page && (AVSDocument.IsSpecRowDocNodeChild(selectedNode1) || AVSDocument.IsSpecSectionDocNodeChild(selectedNode1));
              if (commandState.Enabled)
              {
                AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(selectedNode1);
                if (avsDocRow != null)
                  commandState.Enabled = commandState.Enabled && !avsDocRow.IsDynamicGroupHeaderRow;
              }
              commandState.Visible = commandState.Enabled;
              return true;
            case "AVS.AddZagotovkaForPart":
            case "AVS.AddZagotovkaForPart_FromImBase":
              bool flag1 = false;
              if (this.avsDocument.IsSpecification && !this.ReadOnly)
              {
                List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
                if (selectedSpecRows.Count == 1)
                  flag1 = selectedSpecRows[0].RelType == AvsIDCache.Relation_Project && selectedSpecRows[0].HasObject;
              }
              commandState.Enabled = flag1;
              commandState.Visible = this.avsDocument.IsSpecification;
              return true;
            case "AVS.AdditionalChaptersSetup":
            case "AVS.DocumentTypesWeights":
            case "AVS.SetupNumberingSchema":
            case "AVS.SpecSectionsSetup":
              commandState.Enabled = this.avsDocument.IsSpecification;
              commandState.Visible = this.avsDocument.IsSpecification;
              return true;
            case "AVS.AssemblyProperty":
              commandState.Enabled = this.avsDocument.IsSpecification;
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Text = this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single ? "Свойства исполнения..." : "Свойства изделия...";
              return true;
            case "AVS.ChangeRecordIspolnenie":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              bool flag2 = false;
              if (!this.ReadOnly)
                flag2 = this.GetSelectedSpecRows(false).Count > 0 && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A && this.avsDocument.AllowableGroupDocument;
              commandState.Enabled = flag2;
              return true;
            case "AVS.CheckErrors":
              commandState.Enabled = (this.avsDocument.IsSpecification || this.avsDocument.IsElementList) && !this.ReadOnly && !backThreadIsActive;
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              return true;
            case "AVS.CheckIn":
            case "CheckIn":
              List<AVSRow> selectedSpecRows1 = this.GetSelectedSpecRows(true);
              MenuButtonItem navigatorMenuItem1 = this.FindNavigatorMenuItem("CheckIn");
              commandState.Enabled = commandState.Visible && (selectedSpecRows1.Count > 0 ? navigatorMenuItem1 != null && navigatorMenuItem1.Enabled && navigatorMenuItem1.Visible : !this.ReadOnly && this.DocumentID < 0L);
              commandState.Visible = true;
              return true;
            case "AVS.CheckOut":
            case "CheckOut":
              MenuButtonItem navigatorMenuItem2 = this.FindNavigatorMenuItem("CheckOut");
              commandState.Enabled = commandState.Visible && this._objectIsSelected && navigatorMenuItem2 != null && navigatorMenuItem2.Enabled && navigatorMenuItem2.Visible;
              commandState.Visible = true;
              return true;
            case "AVS.ClearNumberPositions":
            case "AVS.ClearSmotri":
            case "AVS.NumberPositions":
            case "AVS.RefreshFormatAndSmotri":
            case "AVS.RefreshMass":
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive;
              commandState.Visible = this.avsDocument.IsSpecification;
              return true;
            case "AVS.CommonPositions":
              bool flag3 = false;
              if (!this.ReadOnly && this.avsDocument.IsSpecification && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single)
              {
                List<AVSRow> selectedSpecRows2 = this.GetSelectedSpecRows(true);
                flag3 = selectedSpecRows2.Count == 1 && selectedSpecRows2[0].DocNode != null && !selectedSpecRows2[0].IsDynamicGroupHeaderRow;
              }
              commandState.Enabled = flag3;
              commandState.Visible = flag3;
              return true;
            case "AVS.ConvertFromZagotovka":
              bool flag4 = false;
              if (this.avsDocument.IsSpecification && !this.ReadOnly)
              {
                List<AVSRow> selectedSpecRows3 = this.GetSelectedSpecRows(true);
                if (selectedSpecRows3.Count > 0)
                {
                  for (int index = 0; !flag4 && index < selectedSpecRows3.Count; ++index)
                    flag4 |= selectedSpecRows3[index].RelType == AvsIDCache.Relation_Zagotovka;
                }
              }
              commandState.Enabled = flag4;
              commandState.Visible = this.avsDocument.IsSpecification;
              return true;
            case "AVS.CreateDocumentFromFile_VB":
              if (AvsConfig.General.AskAVS6 && Vedomost_VB_Static.IsAvs6ToIps)
              {
                commandState.Visible = true;
                commandState.Enabled = true;
              }
              else
              {
                commandState.Visible = false;
                commandState.Enabled = false;
              }
              return true;
            case "AVS.CreateElementList":
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.CreateVedomost_VB":
              commandState.Visible = true;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.DeleteEmptySections":
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.DeleteObjects":
              commandState.Visible = true;
              DocumentTreeNode[] commandContext1 = this.GetCommandContext();
              bool flag5 = false;
              if (!this.ReadOnly && commandContext1 != null && commandContext1.Length != 0)
              {
                List<AVSRow> specRowsFromNodes = this.GetSpecRowsFromNodes(commandContext1);
                flag5 = this.ObjectIsSelected && specRowsFromNodes.Count > 0 && specRowsFromNodes.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              commandState.Enabled = flag5;
              return true;
            case "AVS.DeleteRecords":
              commandState.Visible = true;
              DocumentTreeNode[] commandContext2 = this.GetCommandContext();
              bool flag6 = false;
              if (!this.ReadOnly && commandContext2 != null && commandContext2.Length != 0)
              {
                List<AVSRow> specRowsFromNodes = this.GetSpecRowsFromNodes(commandContext2);
                flag6 = specRowsFromNodes.Count > 0 && specRowsFromNodes.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              commandState.Enabled = flag6;
              return true;
            case "AVS.DeleteTitlePage":
              commandState.Visible = commandState.Enabled = !this.avsDocument.ReadOnly && ((int) this.Document?.DocumentControl?.ActivePage?.IsTitlePage ?? 0) != 0;
              return true;
            case "AVS.DesignationTrimSetup":
            case "AVS.DocumentProperty":
            case "AVS.DynamicGroupHeaderSetup":
            case "AVS.KeyWordsSetup":
            case "AVS.SetupAVSTemplates":
            case "AVS.SkipLinesSetup":
            case "AVS.SortingSchema":
            case "Find":
            case "FindNext":
              commandState.Enabled = true;
              commandState.Visible = true;
              return true;
            case "AVS.DisconnectSort":
              bool flag7 = false;
              if (!this.ReadOnly)
              {
                List<AVSRow> selectedSpecRows4 = this.GetSelectedSpecRows(false);
                for (int index = 0; !flag7 && index < selectedSpecRows4.Count; ++index)
                {
                  if (selectedSpecRows4[index].SortBeforeRow != null || selectedSpecRows4[index].SortAfterRow != null)
                    flag7 = true;
                }
              }
              commandState.Enabled = flag7;
              commandState.Visible = true;
              return true;
            case "AVS.DontIncludeClassNameInGroupRow":
            case "AVS.IncludeClassNameInGroupRow":
              commandState.Visible = (this.avsDocument.IsSpecification || this.avsDocument.IsElementList) && this.avsDocument.Document.DynamicGroupHeaderIsEnabled;
              commandState.Enabled = commandState.Visible && !this.ReadOnly && this.GetSelectedSpecRows(false).Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.ObjectId.IsDefinedId()));
              return true;
            case "AVS.FinishWork":
              commandState.Visible = this.workCompleteWaitMode;
              commandState.Enabled = !this.ReadOnly;
              return true;
            case "AVS.FromNewPage":
              DocumentTreeNode selectedNode2 = this.DocumentControl.SelectedNode;
              bool flag8 = AVSDocument.IsSpecRowDocNodeChild(selectedNode2);
              bool flag9 = AVSDocument.IsSpecSectionDocNodeChild(selectedNode2);
              bool flag10 = !this.ReadOnly && selectedNode2 != null && this.ViewMode == AVSViewMode.Page && flag8 | flag9;
              if (flag10)
                flag10 = flag10 && this.GetSelectedSpecRows(false).All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              commandState.Enabled = flag10;
              commandState.Visible = flag10;
              return true;
            case "AVS.GridViewMode":
              commandState.Enabled = !this.ReadOnly;
              commandState.Checked = this.ViewMode == AVSViewMode.Grid;
              commandState.Visible = commandState.Enabled;
              return true;
            case "AVS.Group":
              bool flag11 = false;
              if (!this.ReadOnly && this.ViewMode == AVSViewMode.Page && this.DocumentControl.SelectedNodes != null && this.DocumentControl.SelectedNodes.Count != 0 && ImDocumentData.ShowDebugInfo)
              {
                flag11 = true;
                foreach (DocumentTreeNode selectedNode3 in this.DocumentControl.SelectedNodes)
                {
                  if (!(selectedNode3 is TextData) || !AVSRow.IsCountFormBCell(true, selectedNode3 as TextData))
                    flag11 = false;
                }
              }
              commandState.Enabled = flag11;
              commandState.Visible = flag11;
              return true;
            case "AVS.GroupRows.Submenu":
              commandState.Enabled = commandState.Visible = (this.avsDocument.IsSpecification || this.avsDocument.IsElementList) && !this.ReadOnly;
              return true;
            case "AVS.GroupRowsByHeader":
              commandState.Visible = (this.avsDocument.IsSpecification || this.avsDocument.IsElementList) && !this.avsDocument.Document.DynamicGroupHeaderIsEnabled;
              commandState.Enabled = commandState.Visible && !this.ReadOnly;
              return true;
            case "AVS.Hide":
              AVSRow selectedSpecRow1 = this.GetSelectedSpecRow();
              bool flag12 = false;
              if (!this.ReadOnly && selectedSpecRow1 != null && (!this.avsDocument.IsElementList || selectedSpecRow1.HasRelation))
                flag12 = !selectedSpecRow1.IsHiddenRow && !selectedSpecRow1.IsDynamicGroupHeaderRow;
              commandState.Enabled = flag12;
              commandState.Visible = flag12;
              return true;
            case "AVS.HideDocRowsWithoutCount":
            case "AVS.ShowAllDocRows":
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive && (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V);
              return true;
            case "AVS.HideSameChapters":
              commandState.Visible = this.avsDocument.IsSpecification && this.avsDocument.IsFormA;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.ImbaseCatalogsSetup":
              commandState.Enabled = !this.avsDocument.IsSpecification;
              commandState.Visible = !this.avsDocument.IsSpecification;
              return true;
            case "AVS.InsertAdditionalPages":
              bool flag13 = this.ViewMode == AVSViewMode.Page && !this.ReadOnly;
              Page activePage1 = this.Document.DocumentControl.ActivePage;
              bool flag14 = activePage1 != null && activePage1.IsAdditionalPage;
              Page activePage2 = this.Document.DocumentControl.ActivePage;
              bool flag15 = activePage2 != null && activePage2.IsTitlePage;
              bool flag16 = ((int) this.Document.DocumentControl.ActivePage?.NextPage?.IsAdditionalPage ?? 0) != 0;
              Page activePage3 = this.Document.DocumentControl.ActivePage;
              bool flag17 = (activePage3 != null ? activePage3.Index : -1) == (this.Document?.Nodes?.Count ?? -2) - 1;
              commandState.Visible = commandState.Enabled = this.Document.DocumentControl.ActivePage != null & flag13 && !flag17 && !flag15 && !flag14 && !flag16;
              return true;
            case "AVS.InsertTitlePage":
              commandState.Visible = commandState.Enabled = !this.avsDocument.ReadOnly && !this.avsDocument.HasTitlePage;
              return true;
            case "AVS.MoveSpecRow":
              commandState.Visible = this.avsDocument.IsSpecification;
              if (!this.ReadOnly && (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A || this.avsDocument.IsSpecification) && !backThreadIsActive)
              {
                List<AVSRow> selectedSpecRows5 = this.GetSelectedSpecRows(false);
                commandState.Enabled = selectedSpecRows5.Count > 0 && selectedSpecRows5.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              else
                commandState.Enabled = false;
              return true;
            case "AVS.MoveSpecRowToChapter":
              commandState.Visible = this.avsDocument.IsSpecification;
              if (!this.ReadOnly && this.avsDocument.IsSpecification && !backThreadIsActive)
              {
                List<AVSRow> selectedSpecRows6 = this.GetSelectedSpecRows(false);
                commandState.Enabled = selectedSpecRows6.Count > 0 && selectedSpecRows6.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              else
                commandState.Enabled = false;
              return true;
            case "AVS.PageViewMode":
              commandState.Enabled = true;
              commandState.Checked = this.ViewMode == AVSViewMode.Page;
              commandState.Visible = !this.ReadOnly;
              return true;
            case "AVS.ParentProductsList":
              commandState.Visible = this.avsDocument.IsElementList;
              commandState.Enabled = !this.ReadOnly && this.avsDocument.AvsDocumentForm == AVSDocumentForm.Single;
              return true;
            case "AVS.PasteBreak":
              commandState.Enabled = !this.ReadOnly && this.CanPasteSymbol();
              commandState.Visible = commandState.Enabled;
              return true;
            case "AVS.PasteNonBreakSpace":
              commandState.Enabled = !this.ReadOnly && this.CanPasteSymbol();
              commandState.Visible = commandState.Enabled;
              return true;
            case "AVS.Podbor.AddExisting":
            case "AVS.Podbor.AddFromImbase":
            case "AVS.Podbor.CreateNew":
            case "AVS.Podbor.Reset":
              commandState.Visible = true;
              bool flag18 = false;
              if (!this.ReadOnly && !backThreadIsActive)
              {
                AVSDocumentContext contextOnlyOneNode = this.GetAVSDocumentContext_OnlyOneNode();
                if (contextOnlyOneNode.Row != null)
                  flag18 = ((IEnumerable<int>) new int[4]
                  {
                    AvsIDCache.ObjType_AssemblyUnit,
                    AvsIDCache.ObjType_Detail,
                    AvsIDCache.ObjType_OtherProduct,
                    AvsIDCache.ObjType_StandartProduct
                  }).Contains<int>(contextOnlyOneNode.Row.ObjType) && contextOnlyOneNode.Row.IsBaseComponentForPodbor(0, (List<RelationAttributeValuesCache>) null);
              }
              commandState.Enabled = flag18;
              return true;
            case "AVS.Podbor.LimitAndValueModeSubmenu":
            case "AVS.Podbor.Submenu":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Enabled = true;
              return true;
            case "AVS.Podbor.ListModeForRow":
            case "AVS.Podbor.RangeModeForRow":
            case "AVS.Podbor.UseLimitValueModeForRow":
              commandState.Visible = true;
              bool flag19 = false;
              if (!this.ReadOnly && !backThreadIsActive)
              {
                AVSDocumentContext contextOnlyOneNode = this.GetAVSDocumentContext_OnlyOneNode();
                if (contextOnlyOneNode.Row != null)
                {
                  flag19 = ((IEnumerable<int>) new int[4]
                  {
                    AvsIDCache.ObjType_AssemblyUnit,
                    AvsIDCache.ObjType_Detail,
                    AvsIDCache.ObjType_OtherProduct,
                    AvsIDCache.ObjType_StandartProduct
                  }).Contains<int>(contextOnlyOneNode.Row.ObjType) && contextOnlyOneNode.Row.IsBaseComponentForPodbor(0, (List<RelationAttributeValuesCache>) null);
                  commandState.Checked = commandState.CommandName == "AVS.Podbor.RangeModeForRow" && contextOnlyOneNode.Row.LimitAndNominalValueMode == LimitAndNominalValueMode.Range || commandState.CommandName == "AVS.Podbor.ListModeForRow" && contextOnlyOneNode.Row.LimitAndNominalValueMode == LimitAndNominalValueMode.List || commandState.CommandName == "AVS.Podbor.UseLimitValueModeForRow" && contextOnlyOneNode.Row.LimitAndNominalValueMode == LimitAndNominalValueMode.UseLimitValuesOnly;
                }
              }
              commandState.Enabled = flag19;
              return true;
            case "AVS.ProductsList":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Enabled = !this.ReadOnly && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Single && this.avsDocument.AllowableGroupDocument;
              return true;
            case "AVS.Properties":
              commandState.Enabled = true;
              commandState.Visible = true;
              return true;
            case "AVS.Property":
              commandState.Enabled = true;
              commandState.Visible = true;
              commandState.Checked = this.BottomPanelType == AVSWindow.enumBottomPanelType.SelectedRowProperties;
              return true;
            case "AVS.RemarkAttributes":
              commandState.Enabled = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Visible = ImDocumentData.ShowDebugInfo;
              return true;
            case "AVS.RemoveAdditionalPages":
              bool flag20 = this.ViewMode == AVSViewMode.Page && !this.ReadOnly;
              Page activePage4 = this.Document.DocumentControl.ActivePage;
              bool flag21 = activePage4 != null && activePage4.IsAdditionalPage;
              bool flag22 = ((int) this.Document.DocumentControl.ActivePage?.NextPage?.IsAdditionalPage ?? 0) != 0;
              commandState.Visible = commandState.Enabled = flag20 && flag22 | flag21;
              return true;
            case "AVS.ReplaceDocInSpecRow":
              AVSRow selectedSpecRow2 = this.GetSelectedSpecRow();
              commandState.Visible = selectedSpecRow2 != null && selectedSpecRow2.RelType == AvsIDCache.Relation_Document;
              commandState.Enabled = !this.ReadOnly && selectedSpecRow2 != null && !selectedSpecRow2.IsNoteRow && selectedSpecRow2.RelType == AvsIDCache.Relation_Document && !backThreadIsActive;
              return true;
            case "AVS.ReplaceSpecRow":
              AVSRow selectedSpecRow3 = this.GetSelectedSpecRow();
              commandState.Visible = selectedSpecRow3 == null || selectedSpecRow3.RelType != AvsIDCache.Relation_Document;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && selectedSpecRow3 != null && !selectedSpecRow3.IsNoteRow && selectedSpecRow3.RelType != AvsIDCache.Relation_Document && !selectedSpecRow3.IsZagotovka();
              return true;
            case "AVS.ReplaceSpecRowFromImbase":
              AVSRow selectedSpecRow4 = this.GetSelectedSpecRow();
              commandState.Visible = selectedSpecRow4 == null || selectedSpecRow4.RelType != AvsIDCache.Relation_Document;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && selectedSpecRow4 != null && !selectedSpecRow4.IsNoteRow && selectedSpecRow4.RelType != AvsIDCache.Relation_Document && !selectedSpecRow4.IsZagotovka();
              if (commandState.Enabled)
              {
                SpecificationSection section = this.DocumentControl.SelectedNodes == null || this.DocumentControl.SelectedNodes.Count <= 0 ? (SpecificationSection) null : this.avsDocument.GetSection(this.DocumentControl.SelectedNodes[0]);
                if (section == null)
                  return true;
                if (!SpecificationSectionInfo.Cached)
                {
                  using (SessionKeeper sessionKeeper = new SessionKeeper())
                    SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
                }
                List<SpecificationSectionInfo> documentSections = this.AVSDocument.GetAllowableDocumentSections();
                for (int index = 0; index < documentSections.Count; ++index)
                {
                  if (documentSections[index].SectionID == section.SectionID)
                  {
                    commandState.Enabled = documentSections[index].ImBaseCatalogs != null && documentSections[index].ImBaseCatalogs.Length != 0 && !documentSections[index].ImBaseCatalogs[0].IsUndefinedId();
                    return true;
                  }
                }
                commandState.Enabled = false;
              }
              return true;
            case "AVS.ReplaceSpecRowVersion":
              AVSRow selectedSpecRow5 = this.GetSelectedSpecRow();
              commandState.Visible = selectedSpecRow5 == null || selectedSpecRow5.RelType != AvsIDCache.Relation_Document;
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && selectedSpecRow5 != null && !selectedSpecRow5.IsNoteRow && selectedSpecRow5.RelType != AvsIDCache.Relation_Document && !selectedSpecRow5.IsZagotovka();
              if (commandState.Enabled)
                commandState.Enabled = !Intermech.Consts.IsUndefinedObjectId((long) selectedSpecRow5.ObjType) && MetaDataHelper.GetObjectType(selectedSpecRow5.ObjType).VersionsMode == ObjectVersionModes.MultiVersion;
              return true;
            case "AVS.ReplaceTemplate":
              commandState.Visible = true;
              ImDocument imDocument = (ImDocument) null;
              if (!this.ReadOnly)
                imDocument = !this.Document.IsTemplate || this.Document.TemplateOwner == null ? this.Document : this.Document.TemplateOwner as ImDocument;
              commandState.Enabled = imDocument != null;
              return true;
            case "AVS.RowDown":
              AVSRow selectedSpecRow6 = this.GetSelectedSpecRow();
              commandState.Visible = !this.ReadOnly && !backThreadIsActive;
              commandState.Enabled = commandState.Visible && selectedSpecRow6 != null && selectedSpecRow6.Section != null && selectedSpecRow6.Index < selectedSpecRow6.Section.Rows.Count - 1 && !AvsConfig.General.AutoSort && !selectedSpecRow6.IsDynamicGroupHeaderRow;
              return true;
            case "AVS.RowProperties":
              commandState.Visible = !this.ReadOnly && !backThreadIsActive;
              commandState.Enabled = commandState.Visible;
              commandState.Checked = false;
              return true;
            case "AVS.RowUp":
              AVSRow selectedSpecRow7 = this.GetSelectedSpecRow();
              commandState.Visible = !this.ReadOnly && !backThreadIsActive;
              commandState.Enabled = commandState.Visible && selectedSpecRow7 != null && selectedSpecRow7.Index > 0 && !AvsConfig.General.AutoSort && !selectedSpecRow7.IsDynamicGroupHeaderRow;
              return true;
            case "AVS.SetOccurenceKey":
              commandState.Visible = this.avsDocument.IsSpecification;
              List<AVSRow> allRows = this.avsDocument.GetAllRows(true, true);
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && allRows.Count > 0;
              return true;
            case "AVS.ShowEmptySections":
              commandState.Visible = this.avsDocument.IsSpecification;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A;
              return true;
            case "AVS.ShowSameChapters":
              commandState.Visible = this.avsDocument.IsSpecification && this.avsDocument.IsFormA;
              commandState.Enabled = this.avsDocument.IsSpecification && !this.ReadOnly && !backThreadIsActive;
              return true;
            case "AVS.Sort":
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive;
              commandState.Visible = true;
              return true;
            case "AVS.SortAfter":
              bool flag23 = false;
              if (!this.ReadOnly)
              {
                List<AVSRow> selectedSpecRows7 = this.GetSelectedSpecRows(false);
                if (selectedSpecRows7.Count == 1 && selectedSpecRows7[0].Section != null)
                {
                  int index = selectedSpecRows7[0].Index;
                  if (index != -1 && index > 0)
                    flag23 = true;
                }
              }
              commandState.Enabled = flag23;
              commandState.Visible = true;
              return true;
            case "AVS.SortBefore":
              bool flag24 = false;
              List<AVSRow> selectedSpecRows8 = this.GetSelectedSpecRows(false);
              if (!this.ReadOnly && selectedSpecRows8.Count == 1 && selectedSpecRows8[0].Section != null)
              {
                int index = selectedSpecRows8[0].Index;
                if (index != -1 && index < selectedSpecRows8[0].Section.Rows.Count - 1)
                  flag24 = true;
              }
              commandState.Enabled = flag24;
              commandState.Visible = true;
              return true;
            case "AVS.SortRazdel":
              commandState.Visible = this.avsDocument.IsSpecification;
              DocumentTreeNode[] commandContext3 = this.GetCommandContext();
              commandState.Enabled = commandState.Visible && !this.ReadOnly && !backThreadIsActive && commandContext3 != null && commandContext3.Length == 1 && this.avsDocument.GetSection(commandContext3[0]) != null;
              return true;
            case "AVS.SpecificationForm":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              AVSDocumentForm[] allowableDocumentForm = AVSDocumentsSettings.GetAllowableDocumentForm(this.avsDocument.AVSDocType);
              commandState.Enabled = !this.ReadOnly && !backThreadIsActive && this.avsDocument.AllowableGroupDocument && allowableDocumentForm.Length > 1;
              return true;
            case "AVS.SumPositionDesignation":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Enabled = commandState.Visible && !this.ReadOnly && !this.avsDocument.IsFormB && this.avsDocument.AvsDocumentForm != AVSDocumentForm.V;
              return true;
            case "AVS.UnGroupRowsByHeader":
              commandState.Visible = (this.avsDocument.IsSpecification || this.avsDocument.IsElementList) && this.avsDocument.Document.DynamicGroupHeaderIsEnabled;
              commandState.Enabled = commandState.Visible && !this.ReadOnly;
              return true;
            case "AVS.UnHide":
              AVSRow selectedSpecRow8 = this.GetSelectedSpecRow();
              bool flag25 = false;
              if (!this.ReadOnly && selectedSpecRow8 != null)
                flag25 = selectedSpecRow8.IsHiddenRow;
              commandState.Enabled = flag25;
              commandState.Visible = flag25;
              return true;
            case "AVS.UndoFromNewPage":
              DocumentTreeNode selectedNode4 = this.DocumentControl.SelectedNode;
              bool flag26 = !this.ReadOnly && selectedNode4 != null && this.ViewMode == AVSViewMode.Page;
              bool flag27 = AVSDocument.IsSpecRowDocNodeChild(selectedNode4);
              bool flag28 = AVSDocument.IsSpecSectionDocNodeChild(selectedNode4);
              bool flag29 = flag26 && flag27 | flag28;
              if (flag29)
              {
                List<AVSRow> selectedSpecRows9 = this.GetSelectedSpecRows(false);
                flag29 = (!flag27 ? flag29 && ((int) this.GetSelectedSection()?.FromNewPage ?? 0) != 0 : flag29 && selectedSpecRows9.Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.FromNewPage ?? false))) && selectedSpecRows9.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              commandState.Enabled = flag29;
              commandState.Visible = flag29;
              return true;
            case "AVS.UndoSkipLineAfter":
              DocumentTreeNode selectedNode5 = this.DocumentControl.SelectedNode;
              bool flag30 = !this.ReadOnly && selectedNode5 != null && this.ViewMode == AVSViewMode.Page;
              bool flag31 = AVSDocument.IsSpecRowDocNodeChild(selectedNode5);
              bool flag32 = AVSDocument.IsSpecSectionDocNodeChild(selectedNode5);
              bool flag33 = flag30 && flag31 | flag32;
              if (flag33)
              {
                List<AVSRow> selectedSpecRows10 = this.GetSelectedSpecRows(false);
                bool flag34;
                if (flag31)
                {
                  flag34 = flag33 && selectedSpecRows10.Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.SkipLinesAfter.HasValue));
                }
                else
                {
                  int num;
                  if (flag33)
                  {
                    SpecificationSection selectedSection2 = this.GetSelectedSection();
                    num = selectedSection2 != null ? (selectedSection2.SkipLinesAfter.HasValue ? 1 : 0) : 0;
                  }
                  else
                    num = 0;
                  flag34 = num != 0;
                }
                flag33 = flag34 && selectedSpecRows10.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              commandState.Enabled = flag33;
              commandState.Visible = flag33;
              return true;
            case "AVS.UndoSkipLineBefore":
              DocumentTreeNode selectedNode6 = this.DocumentControl.SelectedNode;
              bool flag35 = !this.ReadOnly && selectedNode6 != null && this.ViewMode == AVSViewMode.Page;
              bool flag36 = AVSDocument.IsSpecRowDocNodeChild(selectedNode6);
              bool flag37 = AVSDocument.IsSpecSectionDocNodeChild(selectedNode6);
              bool flag38 = flag35 && flag36 | flag37;
              if (flag38)
              {
                List<AVSRow> selectedSpecRows11 = this.GetSelectedSpecRows(false);
                bool flag39;
                if (flag36)
                {
                  flag39 = flag38 && selectedSpecRows11.Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.SkipLinesBefore.HasValue));
                }
                else
                {
                  int num;
                  if (flag38)
                  {
                    SpecificationSection selectedSection3 = this.GetSelectedSection();
                    num = selectedSection3 != null ? (selectedSection3.SkipLinesBefore.HasValue ? 1 : 0) : 0;
                  }
                  else
                    num = 0;
                  flag39 = num != 0;
                }
                flag38 = flag39 && selectedSpecRows11.All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
              }
              commandState.Enabled = flag38;
              commandState.Visible = flag38;
              return true;
            case "AVS.UpdateDocumentStructure":
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Enabled = commandState.Visible && !this.ReadOnly && this.avsDocument.ProductsInfo != null && this.avsDocument.ProductsInfo.Count > 1;
              return true;
            case "AVS.VersionAttributes":
              commandState.Enabled = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              commandState.Visible = this.avsDocument.IsSpecification || this.avsDocument.IsElementList;
              return true;
            case "AVSParametersCard":
              commandState.Enabled = true;
              commandState.Visible = this.avsDocument.IsSpecification && this.GetSelectedProductsB().Count > 0;
              return true;
            case "AdminCancelChanges":
              MenuButtonItem navigatorMenuItem3 = this.FindNavigatorMenuItem("AdminCancelChanges");
              commandState.Enabled = commandState.Visible && this._objectIsSelected && navigatorMenuItem3 != null && navigatorMenuItem3.Enabled && navigatorMenuItem3.Visible;
              commandState.Visible = true;
              return true;
            case "CallEditor":
            case "DocEditor.EditFormula":
              bool flag40 = false;
              Intermech.Document.UI.DocumentControl documentControl1 = this.DocumentControl;
              if (!documentControl1.ReadOnly)
              {
                DocumentTreeNode[] documentTreeNodeArray = NodeContextMenu.ContextForContextMenu;
                if (documentTreeNodeArray == null || !NodeContextMenu.ContextMenuCommand)
                  documentTreeNodeArray = documentControl1.GetSelectedNodes();
                flag40 = documentTreeNodeArray != null && documentTreeNodeArray.Length == 1;
                if (flag40)
                {
                  if (documentTreeNodeArray[0] is RectangleElement rectangleElement)
                  {
                    TableData topLevelTable = rectangleElement.TopLevelTable;
                    if (topLevelTable != null)
                      flag40 = topLevelTable.TemplateId != "2" && topLevelTable.TemplateId != "Основная надпись" && topLevelTable.TemplateId != "Основная надпись. Продолжение";
                  }
                  flag40 = flag40 && documentTreeNodeArray[0] is TextBoxElement && !documentControl1.QueryCache_HasLockedNodes && documentTreeNodeArray[0].CanCallEditor;
                }
                if (commandState.CommandName == "CallEditor" && documentTreeNodeArray != null && documentTreeNodeArray.Length == 1)
                {
                  List<AVSRow> selectedSpecRows12 = this.GetSelectedSpecRows(false);
                  if (selectedSpecRows12.Count == 1)
                  {
                    TextData cellForAttribute = selectedSpecRows12[0].GetDocumentCellForAttribute(selectedSpecRows12[0].Field_Name, -1);
                    if (cellForAttribute != null && documentTreeNodeArray[0] is TextData textData && textData.Id == cellForAttribute.Id)
                    {
                      using (SessionKeeper sessionKeeper = new SessionKeeper())
                      {
                        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(selectedSpecRows12[0].ObjectId, false);
                        if (objectActualCopy != null)
                        {
                          switch (objectActualCopy.ObjectModifyMode)
                          {
                            case ObjectModifyModes.Checkout:
                              if (objectActualCopy.ObjectID > 0L)
                              {
                                if (objectActualCopy.CheckoutBy == 0L)
                                {
                                  flag40 = false;
                                  break;
                                }
                                if (objectActualCopy.CheckoutBy != sessionKeeper.Session.UserID)
                                {
                                  flag40 = false;
                                  break;
                                }
                                break;
                              }
                              break;
                            case ObjectModifyModes.CreateVersion:
                              flag40 = false;
                              break;
                            case ObjectModifyModes.CantModify:
                              flag40 = false;
                              break;
                          }
                        }
                        else
                          flag40 = false;
                      }
                      commandState.Enabled = flag40;
                      commandState.Visible = flag40;
                      return true;
                    }
                  }
                }
              }
              if (!(commandState.CommandName == "CallEditor") || flag40)
              {
                commandState.Enabled = flag40;
                commandState.Visible = flag40;
                return true;
              }
              break;
            case "CancelChanges":
              MenuButtonItem navigatorMenuItem4 = this.FindNavigatorMenuItem("CancelChanges");
              commandState.Enabled = commandState.Visible && this._objectIsSelected && navigatorMenuItem4 != null && navigatorMenuItem4.Enabled && navigatorMenuItem4.Visible;
              commandState.Visible = true;
              return true;
            case "Copy":
              bool flag41 = false;
              DocumentTreeNode[] commandContext4 = this.GetCommandContext();
              if (commandContext4 != null && commandContext4.Length != 0 && !backThreadIsActive && this.IsFocusedDocument())
              {
                if (this.GetSpecRowsFromNodes(commandContext4).Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.IsDynamicGroupHeaderRow)))
                {
                  commandState.Enabled = false;
                  return true;
                }
                if (commandContext4.Length == 1 && !commandContext4[0].ReadOnlyStructure && !cacheHasLockedNodes)
                {
                  if (this.ViewMode == AVSViewMode.Page)
                  {
                    if (commandContext4[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
                    {
                      commandState.Enabled = true;
                      return true;
                    }
                  }
                  else if (this.virtualTree != null && this.virtualTree.TextEditor != null && this.virtualTree.TextEditor.SelectionLength != 0)
                  {
                    commandState.Enabled = true;
                    return true;
                  }
                }
                DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(commandContext4);
                if (nodesWithoutChilds != null)
                {
                  if (nodesWithoutChilds.Length == 1 && nodesWithoutChilds[0].IsVirtualNode && nodesWithoutChilds[0] is RectangleElement)
                    nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(((RectangleElement) nodesWithoutChilds[0]).GetRealCells().ToArray());
                  if (nodesWithoutChilds.Length == 1)
                  {
                    commandState.Enabled = AVSDocument.IsSpecRowDocNodeChild(commandContext4[0]);
                    if (!commandState.Enabled)
                      commandState.Enabled = AVSDocument.IsNoteRowDocNodeChild(commandContext4[0]);
                  }
                  else
                  {
                    for (int index = 0; index < nodesWithoutChilds.Length; ++index)
                    {
                      DocumentTreeNode ownerRow = nodesWithoutChilds[index];
                      if (nodesWithoutChilds[index] is RectangleElement)
                      {
                        RectangleElement rectangleElement = nodesWithoutChilds[index] as RectangleElement;
                        if (rectangleElement.OwnerRow != null)
                          ownerRow = (DocumentTreeNode) rectangleElement.OwnerRow;
                      }
                      bool flag42 = AVSDocument.IsSpecRowDocNode(ownerRow) | AVSDocument.IsNoteRowDocNodeChild(ownerRow);
                      flag41 |= flag42;
                    }
                    commandState.Enabled = flag41;
                  }
                }
              }
              else
                commandState.Enabled = flag41;
              return true;
            case "Cut":
              if (!this.ReadOnly && !backThreadIsActive && this.IsFocusedDocument())
              {
                DocumentTreeNode[] commandContext5 = this.GetCommandContext();
                if (commandContext5 != null && commandContext5.Length != 0)
                {
                  if (this.GetSpecRowsFromNodes(commandContext5).Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.IsDynamicGroupHeaderRow)))
                  {
                    commandState.Enabled = false;
                    return true;
                  }
                  if (commandContext5.Length == 1 && !commandContext5[0].ReadOnlyStructure && !cacheHasLockedNodes)
                  {
                    if (this.ViewMode == AVSViewMode.Page)
                    {
                      if (commandContext5[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
                      {
                        commandState.Enabled = !placeEditorControl.ReadOnlyMode;
                        return true;
                      }
                    }
                    else if (this.virtualTree != null && this.virtualTree.TextEditor != null)
                    {
                      TextBox textEditor = this.virtualTree.TextEditor;
                      if (textEditor.SelectionLength != 0)
                      {
                        if (!textEditor.ReadOnly)
                        {
                          commandState.Enabled = true;
                          return true;
                        }
                        commandState.Enabled = true;
                        return false;
                      }
                    }
                  }
                  DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(commandContext5);
                  if (nodesWithoutChilds != null)
                  {
                    if (nodesWithoutChilds.Length == 1 && nodesWithoutChilds[0].IsVirtualNode && nodesWithoutChilds[0] is RectangleElement)
                      nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(((RectangleElement) nodesWithoutChilds[0]).GetRealCells().ToArray());
                    if (nodesWithoutChilds.Length == 1)
                    {
                      commandState.Enabled = AVSDocument.IsSpecRowDocNodeChild(commandContext5[0]);
                      if (!commandState.Enabled)
                        commandState.Enabled = AVSDocument.IsNoteRowDocNodeChild(commandContext5[0]) && !AVSDocument.IsProductPageLinksDocNodeChild(commandContext5[0]);
                    }
                    else
                    {
                      bool flag43 = false;
                      for (int index = 0; index < nodesWithoutChilds.Length; ++index)
                      {
                        DocumentTreeNode ownerRow = nodesWithoutChilds[index];
                        if (nodesWithoutChilds[index] is RectangleElement rectangleElement && rectangleElement.OwnerRow != null)
                          ownerRow = (DocumentTreeNode) rectangleElement.OwnerRow;
                        bool flag44 = AVSDocument.IsSpecRowDocNode(ownerRow) | AVSDocument.IsNoteRowDocNodeChild(ownerRow);
                        flag43 |= flag44;
                      }
                      commandState.Enabled = flag43;
                    }
                  }
                }
              }
              else
                commandState.Enabled = false;
              return true;
            case "Delete":
              if (!this.ReadOnly && !backThreadIsActive && this.IsFocusedDocument())
              {
                DocumentTreeNode[] commandContext6 = this.GetCommandContext();
                if (commandContext6 != null && commandContext6.Length != 0)
                {
                  if (this.GetSpecRowsFromNodes(commandContext6).Any<AVSRow>((System.Func<AVSRow, bool>) (r => r.IsDynamicGroupHeaderRow)))
                  {
                    commandState.Enabled = false;
                    return true;
                  }
                  if (commandContext6.Length == 1 && !commandContext6[0].ReadOnlyStructure && !cacheHasLockedNodes)
                  {
                    if (this.ViewMode == AVSViewMode.Page && commandContext6[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
                    {
                      commandState.Enabled = !placeEditorControl.ReadOnlyMode && !this._queryIsProtectedZone;
                      return true;
                    }
                    if (this.ViewMode == AVSViewMode.Grid && this.virtualTree != null && this.virtualTree.TextEditor != null)
                    {
                      TextBox textEditor = this.virtualTree.TextEditor;
                      if (textEditor.SelectionLength != 0)
                      {
                        if (!textEditor.ReadOnly)
                        {
                          commandState.Enabled = true;
                          return true;
                        }
                        commandState.Enabled = false;
                        return true;
                      }
                    }
                  }
                  DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(commandContext6);
                  bool flag45 = true;
                  for (int index = 0; flag45 && index < nodesWithoutChilds.Length; ++index)
                  {
                    Chapter chapter = this.avsDocument.GetChapter(nodesWithoutChilds[index], true);
                    flag45 = AVSDocument.FindParentNoteRowDocNode(nodesWithoutChilds[index]) != null && !AVSDocument.IsProductPageLinksDocNodeChild(commandContext6[0]) || AVSDocument.FindParentSpecRowDocNode(nodesWithoutChilds[index]) != null || AVSDocument.FindParentSpecSectionDocNode(nodesWithoutChilds[index]) != null || this.avsDocument.FindParentLRIRowDocNode(nodesWithoutChilds[index]) != null || chapter != null && (chapter is ProductVariableDataChapter || chapter is AdditionalChapter);
                  }
                  commandState.Enabled = flag45;
                }
              }
              else
                commandState.Enabled = false;
              return true;
            case "DocEditor.InsertFormula":
              bool flag46 = false;
              if (!this.ReadOnly)
              {
                Intermech.Document.UI.DocumentControl documentControl2 = this.DocumentControl;
                if (!documentControl2.ReadOnly)
                {
                  DocumentTreeNode[] nodes = NodeContextMenu.ContextForContextMenu;
                  if (nodes == null || !NodeContextMenu.ContextMenuCommand)
                    nodes = documentControl2.GetSelectedNodes();
                  flag46 = nodes != null && nodes.Length == 1;
                  if (flag46)
                    flag46 = this.GetSpecRowsFromNodes(nodes).All<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow));
                  if (flag46 && nodes[0] is RectangleElement docNode)
                  {
                    TableData topLevelTable = docNode.TopLevelTable;
                    if (topLevelTable != null && (topLevelTable.TemplateId == "2" || topLevelTable.TemplateId == "Основная надпись" || topLevelTable.TemplateId == "Основная надпись. Продолжение"))
                      flag46 = false;
                    else if (docNode.TemplateId != AVSRow.DocAttr_Note && !AVSDocument.IsNoteRowDocNodeChild((DocumentTreeNode) docNode))
                      flag46 = false;
                    else if (AVSDocument.IsProductPageLinksDocNodeChild((DocumentTreeNode) docNode))
                      flag46 = false;
                    else if (!docNode.InPlaceEditorActive)
                      flag46 = false;
                    else if (docNode is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.CurPositionInProtectedZone)
                      flag46 = false;
                  }
                }
              }
              commandState.Enabled = flag46;
              commandState.Visible = flag46;
              return true;
            case "DocEditor.OpenInNewWindow":
            case "ParametersCard1":
              commandState.Enabled = false;
              commandState.Visible = false;
              return true;
            case "PDM.CreateSubstitutesGroup":
              if (!this.ReadOnly && this.avsDocument.IsSpecification)
              {
                if (this.DocumentControl.SelectedNodes.OfType<DocumentTreeNode>().Any<DocumentTreeNode>((System.Func<DocumentTreeNode, bool>) (n =>
                {
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(n);
                  return avsDocRow != null && avsDocRow.IsZagotovka();
                })))
                {
                  commandState.Enabled = false;
                }
                else
                {
                  PDMSubstitutesCommands substitutesCommands = this.GetSubstitutesCommands();
                  commandState.Enabled = (substitutesCommands & PDMSubstitutesCommands.CreateSubstitutesGroup) > PDMSubstitutesCommands.None && this.CheckSelectedRelations();
                }
                commandState.Visible = true;
              }
              else
              {
                commandState.Enabled = false;
                commandState.Visible = false;
              }
              return true;
            case "PDM.DeleteSubstitutesGroup":
              if (!this.ReadOnly && this.avsDocument.IsSpecification)
              {
                if (this.DocumentControl.SelectedNodes.OfType<DocumentTreeNode>().Any<DocumentTreeNode>((System.Func<DocumentTreeNode, bool>) (n =>
                {
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(n);
                  return avsDocRow != null && avsDocRow.IsZagotovka();
                })))
                {
                  commandState.Enabled = false;
                }
                else
                {
                  PDMSubstitutesCommands substitutesCommands = this.GetSubstitutesCommands();
                  commandState.Enabled = (substitutesCommands & PDMSubstitutesCommands.DeleteSubstitutesGroup) > PDMSubstitutesCommands.None && this.CheckSelectedRelations();
                }
                commandState.Visible = true;
              }
              else
              {
                commandState.Enabled = false;
                commandState.Visible = false;
              }
              return true;
            case "PDM.EditSubstitutesGroup":
              if (!this.ReadOnly && this.avsDocument.IsSpecification)
              {
                if (this.DocumentControl.SelectedNodes.OfType<DocumentTreeNode>().Any<DocumentTreeNode>((System.Func<DocumentTreeNode, bool>) (n =>
                {
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(n);
                  return avsDocRow != null && avsDocRow.IsZagotovka();
                })))
                {
                  commandState.Enabled = false;
                }
                else
                {
                  PDMSubstitutesCommands substitutesCommands = this.GetSubstitutesCommands();
                  bool flag47 = this.GetSelectedProducts().Count <= 0 || this.CheckSelectedRelations();
                  commandState.Enabled = (substitutesCommands & PDMSubstitutesCommands.EditSubstitutesGroup) > PDMSubstitutesCommands.None & flag47;
                }
                commandState.Visible = true;
              }
              else
              {
                commandState.Enabled = false;
                commandState.Visible = false;
              }
              return true;
            case "PDM.MakeActualSubstitute":
              if (!this.ReadOnly && this.avsDocument.IsSpecification)
              {
                if (this.DocumentControl.SelectedNodes.OfType<DocumentTreeNode>().Any<DocumentTreeNode>((System.Func<DocumentTreeNode, bool>) (n =>
                {
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(n);
                  return avsDocRow != null && avsDocRow.IsZagotovka();
                })))
                {
                  commandState.Enabled = false;
                }
                else
                {
                  PDMSubstitutesCommands substitutesCommands = this.GetSubstitutesCommands();
                  commandState.Enabled = (substitutesCommands & PDMSubstitutesCommands.MakeActualSubstitute) > PDMSubstitutesCommands.None && this.CheckSelectedRelations();
                }
                commandState.Visible = true;
              }
              else
              {
                commandState.Enabled = false;
                commandState.Visible = false;
              }
              return true;
            case "ParametersCard":
              MenuButtonItem navigatorMenuItem5 = this.FindNavigatorMenuItem("ParametersCard");
              commandState.Enabled = commandState.Visible && this._objectIsSelected && navigatorMenuItem5 != null && navigatorMenuItem5.Enabled && navigatorMenuItem5.Visible;
              commandState.Visible = true;
              return true;
            case "Paste":
              commandState.Visible = true;
              bool flag48 = false;
              if (!this.ReadOnly && !backThreadIsActive && this.IsFocusedDocument())
              {
                DocumentTreeNode[] commandContext7 = this.GetCommandContext();
                if (commandContext7 == null)
                {
                  commandState.Enabled = false;
                  return true;
                }
                if (commandContext7.Length == 0 || commandContext7.Length != 0 && commandContext7[0].IsVirtualNode)
                {
                  commandState.Enabled = false;
                  return true;
                }
                if (commandContext7.Length > 1)
                {
                  SpecificationSection section1 = this.AVSDocument.GetSection(commandContext7[0]);
                  for (int index = 1; index < commandContext7.Length; ++index)
                  {
                    SpecificationSection section2 = this.AVSDocument.GetSection(commandContext7[index]);
                    if (section2 != section1 || section2 == null)
                    {
                      commandState.Enabled = false;
                      return true;
                    }
                  }
                }
                if (commandContext7[0] is Page)
                {
                  commandState.Enabled = false;
                  return true;
                }
                bool flag49 = NodeClipboardHelper.CanPasteFromClipboard(commandContext7[0], out PasteType _);
                if (commandContext7.Length == 1 && (!commandContext7[0].ReadOnlyStructure || this.ViewMode == AVSViewMode.Grid) && !cacheHasLockedNodes)
                {
                  if (this.ViewMode == AVSViewMode.Grid && this.virtualTree != null && this.virtualTree.TextEditor != null && !this.virtualTree.TextEditor.ReadOnly)
                  {
                    commandState.Enabled = true;
                    return true;
                  }
                  if (this.ViewMode == AVSViewMode.Page & flag49)
                  {
                    commandState.Enabled = true;
                    return true;
                  }
                }
                if (AVSDocRowsClipboardData.CanPasteDocRowsFromClipboard())
                  flag48 = this.ContextIsSpectification(commandContext7);
                if (this.ContextIsSpectification(commandContext7) && !flag48 && ServicesManager.GetService(typeof (IClipboard)) is IClipboard service && this.avsDocument != null)
                {
                  if (service.GetDataObject() is AVSRowClipboardCollection dataObject2)
                    flag48 = dataObject2.RowList.Count > 0;
                  else if (service.GetDataObject() is IDBObjectTypedIDCollection dataObject1)
                  {
                    IDBTypedObjectID[] typedObjects = dataObject1.GetTypedObjects();
                    flag48 = typedObjects != null && typedObjects.Length != 0;
                  }
                }
                commandState.Enabled = flag48;
                return true;
              }
              commandState.Enabled = flag48;
              return true;
            case "Replace":
              commandState.Enabled = !this.ReadOnly;
              commandState.Visible = true;
              return true;
            case "Save":
              commandState.Enabled = !this.ReadOnly && !this.avsDocument.IsSpecification && this.Document.LoadFromStreamThread == null && this.Document.Modified;
              return true;
            case "SaveChanges":
              MenuButtonItem navigatorMenuItem6 = this.FindNavigatorMenuItem("SaveChanges");
              commandState.Enabled = commandState.Visible && this._objectIsSelected && navigatorMenuItem6 != null && navigatorMenuItem6.Enabled && navigatorMenuItem6.Visible;
              commandState.Visible = true;
              return true;
            case "SelectAll":
              commandState.Text = "Выделить все записи";
              commandState.Enabled = true;
              return true;
          }
          if (this.ExternalAVSCommands.ContainsKey(commandState.CommandName))
          {
            commandState.Visible = true;
            commandState.Enabled = !this.ReadOnly;
            return true;
          }
          if (AVSPlugin.Instance.ExternalAVSCommands.ContainsKey(commandState.CommandName))
          {
            commandState.Visible = true;
            commandState.Enabled = !this.ReadOnly;
            return true;
          }
          if (base.QueryStatus(commandState))
            return true;
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
          commandState.Enabled = false;
          return true;
        }
        return false;
      }
    }
    return false;
  }

  private bool CanAddAvsRowInSelectedContext()
  {
    return !this.avsDocument.AutoSort || this.avsDocument.GetAVSRowsAndDocRows((IEnumerable<DocumentTreeNode>) this.GetCommandContext()).avsRows.Count <= 1;
  }

  private bool CanAddLRIRowInSelectedContext()
  {
    return this.avsDocument.GetAVSRowsAndDocRows((IEnumerable<DocumentTreeNode>) this.GetCommandContext()).docRows.Count <= 1;
  }

  private MenuButtonItem FindNavigatorMenuItem(string commandName)
  {
    MenuButtonItem navigatorMenuItem = (MenuButtonItem) null;
    if (this.navigatorMenuItemsHelpers != null)
    {
      foreach (KeyValuePair<MenuButtonItem, object> navigatorMenuItemsHelper in this.navigatorMenuItemsHelpers)
      {
        if (navigatorMenuItemsHelper.Key.CommandName == commandName)
          navigatorMenuItem = navigatorMenuItemsHelper.Key;
      }
    }
    return navigatorMenuItem;
  }

  /// <summary>Получить настройки столбцов для табличного вида</summary>
  /// <returns></returns>
  public virtual List<AvsRowAttributeInfo> GetGridViewColumns()
  {
    List<AvsRowAttributeInfo> gridViewCols = (this.avsDocument == null || this.avsDocument.IsSpecification ? AVSPlugin.specificationGridViewCols : AVSPlugin.elementListGridViewCols) ?? new List<AvsRowAttributeInfo>();
    if (gridViewCols.Count == 0)
    {
      AVSPlugin.CreateDefaultGridViewCols(this, gridViewCols);
      if (this.avsDocument != null && !this.avsDocument.IsSpecification)
        AVSPlugin.elementListGridViewCols = gridViewCols;
      else
        AVSPlugin.specificationGridViewCols = gridViewCols;
    }
    return gridViewCols;
  }

  /// <summary>Получить индекс столбца для заголовка раздела в табличном виде</summary>
  /// <returns></returns>
  public int GetNameColumnIndex()
  {
    return this.avsDocument.FindGridColumn_Name(this.GetGridViewColumns());
  }

  /// <summary>Заблокировать обновления в TreeList</summary>
  public void LockTreeList() => this.virtualTree.BeginUpdate();

  /// <summary>Разаблокировать обновления в TreeList</summary>
  public void UnlockTreeList() => this.virtualTree.EndUpdate();

  /// <summary>Перекрыть стиль ячейки в TreeList</summary>
  private void treeList_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
    try
    {
      TreeList treeList = e.Node.TreeList;
      if (!AVSDocument.IsSectionTreeListNode(e.Node))
        return;
      e.Style = treeList.Styles["SectionHeader"];
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Получить имя колонки в TreeList для заданного атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <returns>Имя колонки в TreeList для заданного атрибута</returns>
  public string GetTreeListNodeFieldName(AvsRowAttributeInfo attrInfo)
  {
    switch (attrInfo.AttrSrc)
    {
      case FieldSource.Relation:
        return "rel." + attrInfo.AttributeId.ToString();
      case FieldSource.Object:
        return "obj." + attrInfo.AttributeId.ToString();
      case FieldSource.DocumentRowField:
        return "doc." + attrInfo.Name;
      default:
        return "doc." + attrInfo.Name;
    }
  }

  /// <summary>Получить информацию об атрибуте по имени колонки в TreeList</summary>
  /// <param name="fieldName">Имя колонки в TreeList</param>
  /// <returns>Информацию об атрибуте</returns>
  private AvsRowAttributeInfo GetAttrInfoFromFieldName(string fieldName)
  {
    string[] strArray = fieldName.Split('.');
    return new AvsRowAttributeInfo(strArray[0] == "rel", Convert.ToInt32(strArray[1]));
  }

  /// <summary>Пересоздать колонки в TreeList</summary>
  private void ReCreateGridColumns()
  {
    this.LockTreeColumnsSave();
    try
    {
      this.ReCreateGridColumns_VirtualTree();
    }
    finally
    {
      this.UnlockTreeColumnsSave();
    }
  }

  /// <summary>Пересоздать колонки в TreeList</summary>
  private void ReCreateGridColumns_VirtualTree()
  {
    this.LockTreeColumnsSave();
    try
    {
      if (this.virtualTree == null)
        return;
      this.virtualTree.Columns.Clear();
      AVSColumn avsColumn1 = new AVSColumn();
      avsColumn1.Pinned = true;
      avsColumn1.Name = "AVS.Status";
      avsColumn1.MinWidth = 100;
      avsColumn1.Resizable = false;
      this.virtualTree.Columns.Add((Column) avsColumn1);
      int num1 = 0;
      bool flag = this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V;
      List<AvsRowAttributeInfo> gridViewColumns = this.GetGridViewColumns();
      if (flag)
      {
        flag = false;
        for (int index = 0; index < gridViewColumns.Count; ++index)
        {
          if (AVSRow.IsCountField(gridViewColumns[index]))
          {
            flag = true;
            break;
          }
        }
      }
      AVSColumn[] avsColumnArray = new AVSColumn[flag ? gridViewColumns.Count + this.avsDocument.productsInfo.Count - 1 : gridViewColumns.Count];
      int num2 = 0;
      int currentNumber = -1;
      bool isFormB = this.avsDocument.IsFormB;
      for (int index1 = 0; index1 < gridViewColumns.Count; ++index1)
      {
        AvsRowAttributeInfo specRowAttributeInfo = gridViewColumns[index1];
        bool pinned = specRowAttributeInfo.Pinned;
        int num3 = !flag || specRowAttributeInfo.AttributeId != AvsIDCache.Attr_Count ? 1 : this.avsDocument.productsInfo.Count;
        for (int index2 = 0; index2 < num3; ++index2)
        {
          AVSColumn avsColumn2 = new AVSColumn();
          avsColumn2.Tag = new ColumnTag(specRowAttributeInfo, index2);
          if (flag && specRowAttributeInfo.AttributeId == AvsIDCache.Attr_Count && specRowAttributeInfo.IsRelationAttribute)
          {
            avsColumn2.Caption = this.avsDocument.productsInfo[index2].GetNumber(currentNumber, out currentNumber, this.DocumentDesignation, this.avsDocument.UseSameDesignationForProducts).Trim();
            ++num1;
          }
          else
            avsColumn2.Caption = specRowAttributeInfo.Name;
          avsColumn2.Name = specRowAttributeInfo.Name;
          avsColumn2.Width = specRowAttributeInfo.TableViewColumnWidth;
          avsColumn2.Pinned = pinned;
          ++num2;
          if (specRowAttributeInfo.FieldType == FieldTypes.ftBoolean)
            avsColumn2.CheckEdit = true;
          this.virtualTree.Columns.Add((Column) avsColumn2);
        }
      }
    }
    finally
    {
      this.UnlockTreeColumnsSave();
    }
  }

  /// <summary>Получить все дочерние TreeListNodes текущего TreeListNode</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  private List<TreeListNode> GetTreeListNodes(TreeListNode item, bool getOnlyExpanded)
  {
    List<TreeListNode> treeListNodes1 = new List<TreeListNode>();
    if (item.Nodes != null && item.Nodes.Count != 0)
    {
      for (int index = 0; index < item.Nodes.Count; ++index)
      {
        List<TreeListNode> treeListNodes2 = this.GetTreeListNodes(item.Nodes[index], getOnlyExpanded);
        if (treeListNodes2.Count != 0)
          treeListNodes1.AddRange((IEnumerable<TreeListNode>) treeListNodes2);
        treeListNodes1.AddRange((IEnumerable<TreeListNode>) this.GetTreeListNodes(item.Nodes[index], getOnlyExpanded));
      }
      if (!getOnlyExpanded || item.Expanded)
        treeListNodes1.Add(item);
    }
    return treeListNodes1;
  }

  /// <summary>Установить свойство Expanded</summary>
  /// <param name="nodes"></param>
  /// <param name="expandedNodes"></param>
  private void SetExpandedNodes(TreeListNodes nodes, List<TreeListNode> expandedNodes)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      nodes[index].Expanded = true;
      if (nodes[index].Nodes.Count != 0)
        this.SetExpandedNodes(nodes[index].Nodes, expandedNodes);
    }
  }

  /// <summary>Обновить колонки в TreeList. После того как изменен состав колонок.</summary>
  public virtual void UpdateGridViewCols()
  {
    this.LockTreeColumnsSave();
    try
    {
      this.AssignTreeListEvents();
      this.LockTreeList();
      try
      {
        this.ReCreateGridColumns();
        this.avsDocument.LoadNewAttributes(this.GetGridViewColumns(), true);
      }
      finally
      {
        this.avsDocument.UpdateViewNodes(false, true, false, false, false, EmptyRowUpdateMode.DontChange);
        this.UnlockTreeList();
      }
    }
    catch (Exception ex)
    {
      throw;
    }
    finally
    {
      this.UnlockTreeColumnsSave();
    }
  }

  private void contextMenuBarItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    try
    {
      if (this.virtualTree == null || this.ViewMode != AVSViewMode.Grid)
        return;
      this.ContextMenuBarItem.Items.Clear();
      if (!this.virtualTree.CanShowContextMenu())
        return;
      DocumentTreeNode[] context = this.GetCommandContext();
      if (this.virtualTree.SelectedItem != null)
      {
        DocumentTreeNode docNodeForListNode = AVSWindow.GetDocNodeForListNode(this.virtualTree.SelectedItem as IVirtualTreeItem);
        if (docNodeForListNode != null)
          context = new DocumentTreeNode[1]
          {
            docNodeForListNode
          };
      }
      NodeContextMenu.AddToContextMenu(this.ContextMenuBarItem, this.DocumentControl.GetContexMenu(context));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void contextMenuBarItem_AfterPopup(object sender, EventArgs e)
  {
    NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
    NodeContextMenu.ContextMenuCommand = false;
  }

  private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
  }

  private void treeList_KeyDown(object sender, KeyEventArgs e)
  {
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
  }

  private void treeList_ShowingEditor_1(object sender, CancelEventArgs e)
  {
  }

  private void treeList_ColumnChanged(object sender, ColumnChangedEventArgs e)
  {
    try
    {
      if (this.IsTreeColumnsSaveLocked() || this.ViewMode != AVSViewMode.Grid)
        return;
      this.SaveColumnsState();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Заблокировать метод сохранения настроек столбцов табличного вида</summary>
  internal void LockTreeColumnsSave() => ++this._lockTreeColumnsSaveCounter;

  /// <summary>Разблокировать метод сохранения настроек столбцов табличного вида</summary>
  internal void UnlockTreeColumnsSave()
  {
    if (this._lockTreeColumnsSaveCounter <= 0)
      return;
    --this._lockTreeColumnsSaveCounter;
  }

  /// <summary>Метод сохранения настроек столбцов табличного вида заблокирован</summary>
  private bool IsTreeColumnsSaveLocked() => this._lockTreeColumnsSaveCounter > 0;

  /// <summary>Назначаю события Grid-у с параметрами изделий в спецификации </summary>
  private void AssignTreeListEvents()
  {
  }

  /// <summary>Столбцы табличного вида были обновлены</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void TreeListLayoutUpdated(object sender, EventArgs e)
  {
    try
    {
      if (this.IsTreeColumnsSaveLocked() || this.ViewMode != AVSViewMode.Grid)
        return;
      this.SaveColumnsState();
      if (AVSPlugin.DockManager == null)
        return;
      foreach (DockControl dockControl in AVSPlugin.DockManager.GetDockControls())
      {
        if (dockControl != null && dockControl is AVSWindow && dockControl != this)
        {
          if (UIHelper.IsVisible((Control) dockControl) && ((AVSWindow) dockControl).ViewMode == AVSViewMode.Grid)
            ((AVSWindow) dockControl).LoadColumnsState();
          else
            ((AVSWindow) dockControl).NeedToLoadColumnParams = true;
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Сохранить порядок и размер колонок в табличном виде спецификации </summary>
  internal void SaveColumnsState(bool updateOnlyInvisible = false)
  {
    if (this.ViewMode != AVSViewMode.Grid)
      return;
    List<AvsRowAttributeInfo> rowAttributeInfoList = new List<AvsRowAttributeInfo>();
    for (int index = 0; index < this.virtualTree.Columns.Count; ++index)
    {
      AVSColumn column = this.virtualTree.Columns[index] as AVSColumn;
      ColumnTag tag = column.Tag;
      if (tag != null && tag.ProductIndex == 0)
      {
        rowAttributeInfoList.Add(tag.SpecRowAttributeInfo);
        tag.SpecRowAttributeInfo.TableViewColumnWidth = column.Width;
      }
    }
    if (!this.avsDocument.IsSpecification)
      AVSPlugin.elementListGridViewCols = rowAttributeInfoList;
    else
      AVSPlugin.specificationGridViewCols = rowAttributeInfoList;
    foreach (AVSWindow avsWindows in AVSPlugin.Instance.GetAVSWindowsList())
    {
      if (avsWindows != this && (!updateOnlyInvisible || !avsWindows.Visible))
        avsWindows.NeedToLoadColumnParams = true;
    }
  }

  /// <summary>Востановить порядок и размер колонок в табличном виде спецификации если в этом есть необходимость </summary>
  internal void LoadColumnsStateIfNeeded()
  {
    if (!this.NeedToLoadColumnParams || this.ViewMode != AVSViewMode.Grid)
      return;
    this.LoadColumnsState();
    this.NeedToLoadColumnParams = false;
  }

  /// <summary>Востановить порядок и размер колонок в табличном виде спецификации </summary>
  private void LoadColumnsState()
  {
    this.LockTreeColumnsSave();
    try
    {
      this.virtualTree.ClearColumns();
      this.ReCreateGridColumns();
    }
    finally
    {
      this.UnlockTreeColumnsSave();
      this.NeedToLoadColumnParams = false;
    }
  }

  /// <summary>Старт загрузки настроек столбцов</summary>
  private void StartLoadColumnState()
  {
    ++this._loadColumnStateCounter;
    if (!this.avsDocument.IsFormB && this.avsDocument.AvsDocumentForm != AVSDocumentForm.V || this.virtualTree.Columns.Count <= 0)
      return;
    for (int index = this.virtualTree.Columns.Count - 1; index >= 0; --index)
    {
      if (this.virtualTree.Columns[index] is AVSColumn column && column.Tag != null && column.Tag.Equals((object) AvsIDCache.Attr_Count))
        this.virtualTree.Columns.RemoveAt(index);
    }
  }

  /// <summary>Завершение загрузки настроек столбцов</summary>
  private void FinishLoadColumnState()
  {
    if (this._loadColumnStateCounter <= 0)
      return;
    --this._loadColumnStateCounter;
  }

  /// <summary>Загрузить настройки столбцов табличного вида по умолчанию</summary>
  private void LoadDefaultColumns()
  {
    this.virtualTree.ClearColumns();
    this.virtualTree.Columns.Add((Column) new AVSColumn());
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Format, 50));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Position, 50));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Zone, 50));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Designation, 350));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Name, 350));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Count, 50));
    this.virtualTree.Columns.Add((Column) new AVSColumn(this.avsDocument.Field_Note, 500));
  }

  /// <summary>Получить колонку табличного вида по идентификатору атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  public AVSColumn GetColumnByID(Guid attributeID)
  {
    foreach (AVSColumn column in this.virtualTree.Columns)
    {
      if (column.Tag != null && column.Tag.AttributeGuid.Equals(attributeID))
        return column;
    }
    return (AVSColumn) null;
  }

  /// <summary>Сгенерировать фразу "Различия исполнений *** по сборочному черчежу"</summary>
  /// <param name="avsDocument"></param>
  /// <returns></returns>
  public string GetIspolnDifference(AVSDocument avsDocument)
  {
    if (avsDocument == null || avsDocument.productsInfo.Count <= 1 || avsDocument.variableDataChapter_FormA == null || avsDocument.variableDataChapter_FormA.Chapters == null || avsDocument.variableDataChapter_FormA.Chapters.Count <= 0)
      return string.Empty;
    string str = "Различия исполнений ";
    int num = 0;
    foreach (Chapter chapter in avsDocument.variableDataChapter_FormA.Chapters)
    {
      if (chapter != null)
        str = str + (num == 0 ? string.Empty : (num == avsDocument.variableDataChapter_FormA.Chapters.Count - 1 ? " и " : ", ")) + chapter.Caption;
      ++num;
    }
    return str + " по сборочному чертежу";
  }

  /// <summary>Обработчик события при необходимости закрыть DropDown редактор</summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  private void iAttributeEditorControl_OnCloseDemand(object sender, CloseControlEventArgs args)
  {
    try
    {
      if (!(sender is Control control) || !(control.Parent is PopupContainerControl parent) || parent.OwnerEdit == null)
        return;
      parent.OwnerEdit.ClosePopup();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Показать диалог настройки нумерации позиций </summary>
  public bool ShowSetupPositionsNumberingDlg()
  {
    AVSDocumentTypeSettings documentTypeSettings = this.avsDocument.GetDocumentTypeSettings();
    SettingsStructure settingsStructure = (SettingsStructure) null;
    if (documentTypeSettings != null)
      settingsStructure = documentTypeSettings.SettingsInheritanceStructure;
    using (SetupNumberingSchemaForm numberingSchemaForm = new SetupNumberingSchemaForm(settingsStructure, this.DocumentID, this.AVSDocument.AVSDocumentTemplateID))
      return numberingSchemaForm.ShowDialog() == DialogResult.OK;
  }

  /// <summary>Показать диалог настройки AVS </summary>
  public void ShowAVSSetupDlg()
  {
    AVSPropertiesForm.Execute(this.DocumentID, this.avsDocument.AVSDocumentTemplateID);
  }

  /// <summary>Показать диалог настройки сортировки записей </summary>
  public bool ShowSetupSortingDlg()
  {
    using (FormSetupSorting formSetupSorting = new FormSetupSorting(this.DocumentID, this.DocumentType, this.avsDocument.AVSDocumentTemplateID, this.avsDocument.DocumentSettingsStructure, this.avsDocument.GetRelationTypesUsedInDocument()))
    {
      DocFieldsColumnsScheme fieldsColumnsScheme = new DocFieldsColumnsScheme((IEnumerable<AttributeInfo>) this.avsDocument.CollectCellOutputMappingAttributes());
      List<AVSColumnScheme> customColumnSchemes = new List<AVSColumnScheme>()
      {
        (AVSColumnScheme) new AvsVirtualAttributeColumnsScheme((IEnumerable<AttributeInfo>) this.avsDocument.GetVirtualAttributeList()),
        (AVSColumnScheme) fieldsColumnsScheme
      };
      formSetupSorting.AddCustomAttributes(customColumnSchemes);
      if (this.avsDocument.productsInfo.Count > 0)
        formSetupSorting.SpecificationObjectId = this.avsDocument.productsInfo[0].Id;
      return formSetupSorting.ShowDialog() == DialogResult.OK;
    }
  }

  /// <summary>Показать диалог настройки пропуска строк </summary>
  public bool ShowSetupSkipLinesDlg()
  {
    using (FormSetupSkipLines formSetupSkipLines = new FormSetupSkipLines(this.avsDocument.DocumentSettingsStructure, this.DocumentID, this.DocumentType, this.AVSDocument.AVSDocumentTemplateID))
    {
      if (formSetupSkipLines.ShowDialog() != DialogResult.OK)
        return false;
      if (!this.ReadOnly)
      {
        this.avsDocument.skipLinesSchema = (SkipLinesSchema) null;
        this.avsDocument.UpdateSkipLines(true, true);
      }
      return true;
    }
  }

  /// <summary>Показать диалог настройки динамических заголовков групп записей</summary>
  public bool ShowDynamicGroupHeaderSettingsDlg()
  {
    using (DynamicGroupHeaderSettingsForm headerSettingsForm = new DynamicGroupHeaderSettingsForm(this.avsDocument.DocumentSettingsStructure, this.DocumentID, this.DocumentType, this.AVSDocument.AVSDocumentTemplateID))
    {
      if (headerSettingsForm.ShowDialog() != DialogResult.OK)
        return false;
      if (!this.ReadOnly)
      {
        this.avsDocument.dynamicGroupHeaderSettings = (DynamicGroupHeaderSettings) null;
        AVSDocument.keywordReplacementSettings = (KeywordReplacementScheme) null;
        this.avsDocument.UpdateDynamicGroupHeaderSettings(true, true);
      }
      return true;
    }
  }

  /// <summary>Показать диалог настройки пропуска строк </summary>
  public bool ShowSetupDesignationTrimDlg()
  {
    AVSDocumentTypeSettings documentTypeSettings = this.avsDocument.GetDocumentTypeSettings();
    SettingsStructure settingsStructure = (SettingsStructure) null;
    if (documentTypeSettings != null)
      settingsStructure = documentTypeSettings.SettingsInheritanceStructure;
    using (FormSetupDesignationTrim setupDesignationTrim = new FormSetupDesignationTrim(settingsStructure, this.DocumentID, this.AVSDocument.AVSDocumentTemplateID))
    {
      if (setupDesignationTrim.ShowDialog() != DialogResult.OK)
        return false;
      this.avsDocument.designationTrimSchema = setupDesignationTrim.DesignationTrimSchema;
      if (!this.ReadOnly)
      {
        this.avsDocument.UpdatePartProductCaptions();
        this.avsDocument.UpdateProductHeadersOnPages(true, true);
      }
      return true;
    }
  }

  /// <summary>Показать диалог настройки пропуска строк </summary>
  public bool ShowSetupKeyWordsDlg()
  {
    using (FormSetupKeyWords formSetupKeyWords = new FormSetupKeyWords(this.avsDocument.DocumentSettingsStructure, this.DocumentID, this.AVSDocument.AVSDocumentTemplateID))
    {
      if (formSetupKeyWords.ShowDialog() != DialogResult.OK)
        return false;
      if (this.Document != null)
      {
        this.Document.SetMaterialKeyWords((List<string>) this.avsDocument.MaterialKeyWordsSchema?.KeyWords);
        this.Document.UpdateFormulasInDocument();
      }
      return true;
    }
  }

  /// <summary>Показать диалог настройки разделов </summary>
  public bool ShowImbaseCatalogsDlg(long templateId)
  {
    using (ImbaseCatalogsEditor imbaseCatalogsEditor = new ImbaseCatalogsEditor(this.avsDocument.DocumentSettingsStructure, templateId))
      return imbaseCatalogsEditor.ShowDialog() == DialogResult.OK;
  }

  /// <summary>Показать диалог настройки разделов </summary>
  public bool ShowSetupSectionsDlg(long templateId)
  {
    using (SpecSectionsEditor specSectionsEditor = new SpecSectionsEditor(this.avsDocument.DocumentSettingsStructure, templateId))
    {
      if (specSectionsEditor.ShowDialog() != DialogResult.OK)
        return false;
      this.AVSDocument.avsCommonPropertiesSchema = (AVSCommonPropertiesSchema) null;
      this.AVSDocument.UpdateSpecificationSectionsCaptions();
      this.Document.UpdateLayout(true);
      return true;
    }
  }

  /// <summary>Показать диалог настройки разделов </summary>
  public static bool ShowSetupAVSTemplates(long templateId = -1, AVSDocumentForm? form = null)
  {
    using (AVSDocumentTypesTemplateForm typesTemplateForm = new AVSDocumentTypesTemplateForm())
    {
      if (!templateId.IsUndefinedId())
      {
        typesTemplateForm.ContextID = templateId;
        typesTemplateForm.ContextDocumentForm = form;
      }
      typesTemplateForm.RestoreSize();
      return typesTemplateForm.ShowDialog() == DialogResult.OK;
    }
  }

  /// <summary>Показать диалог поиска текста </summary>
  public void ShowFindOrReplaceTextDialog(bool findWithReplace)
  {
    if (this.ReadOnly && !this.avsDocument.DataLoaded)
      this.avsDocument.LoadAVSDocumentData((AVSDocumentContext) null);
    object obj = !findWithReplace ? (object) FindOrReplaceService.ShowFindWindow((IWindowWithFind) this) : (object) FindOrReplaceService.ShowReplaceWindow((IWindowWithFindAndReplace) this);
    if (obj is DockControl)
      (obj as DockControl).Closed += new EventHandler(this.FindOrReplace_Closed);
    this.findController = obj as FormFindOrReplaceTextInSpecification;
    this.findController.EnableReplace = !this.ReadOnly;
  }

  private void FindOrReplace_Closed(object sender, EventArgs e)
  {
    try
    {
      (sender as DockControl).Closed -= new EventHandler(this.FindOrReplace_Closed);
      if (AVSPlugin.Instance.CommandManager == null)
        return;
      AVSPlugin.Instance.CommandManager.ActiveTarget = (ICommandTarget) this;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Команда "найти далее..." </summary>
  public void FindNext()
  {
    if (this.findController == null)
      return;
    this.FindNext((IFindController) this.findController);
  }

  /// <summary>Позволяет сервису поиска определить тип класса окна, в котором должна осуществляться настройка поиска </summary>
  /// <returns>Тип класса окна, в котором должна осуществляться настройка поиска </returns>
  public System.Type GetFindSetupFormClass() => typeof (FormFindOrReplaceTextInSpecification);

  /// <summary>Вызывается, когда в диалоге поиска была нажата кнопка "Найти далее" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска </param>
  public void FindNext(IFindController findController)
  {
    this.FindText(AVSWindow.FindOperation.Find, findController);
  }

  /// <summary>Вызывается при нажатии кнопки "Заменить" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска и замены </param>
  public void Replace(IFindController findController)
  {
    this.FindText(AVSWindow.FindOperation.Replace, findController);
  }

  /// <summary>Вызывается при нажатии кнопки "Заменить все" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска и замены </param>
  public void ReplaceAll(IFindController findController)
  {
    this.FindText(AVSWindow.FindOperation.ReplaceAll, findController);
  }

  /// <summary>Найти текст</summary>
  /// <param name="findOperation">Операция</param>
  /// <param name="findController">Служебный класс контролёр поиска</param>
  private void FindText(AVSWindow.FindOperation findOperation, IFindController findController)
  {
    if (this.avsDocument.IsEmpty || findController == null || !(findController.InterfaceObject is IFindOrReplaceTextController interfaceObject1) || !(findController.InterfaceObject is IAttributesSelection interfaceObject2))
      return;
    string str1 = !interfaceObject1.MatchWholeWord || !interfaceObject1.UseRegularExpressions ? interfaceObject1.FindWhat : $"/W{interfaceObject1.FindWhat}/W";
    AttributeDescriptorList checkedAttributesList = interfaceObject2.GetCheckedAttributesList();
    if (checkedAttributesList == null || checkedAttributesList.Count == 0)
      return;
    AVSRow avsRow1 = (AVSRow) null;
    List<AvsRowAttributeInfo> rowAttributeInfoList1;
    switch (this.ViewMode)
    {
      case AVSViewMode.Page:
        rowAttributeInfoList1 = this.avsDocument.AvsDocumentForm == AVSDocumentForm.V ? this.avsDocument.docRowFields_VarFormV : this.avsDocument.docRowFields;
        break;
      case AVSViewMode.Grid:
        rowAttributeInfoList1 = this.GetGridViewColumns();
        break;
      default:
        return;
    }
    List<AvsRowAttributeInfo> rowAttributeInfoList2 = new List<AvsRowAttributeInfo>();
    foreach (AvsRowAttributeInfo rowAttributeInfo in rowAttributeInfoList1)
    {
      if (rowAttributeInfo != null && checkedAttributesList.IndexOfID(rowAttributeInfo.AttributeId) != -1)
        rowAttributeInfoList2.Add(rowAttributeInfo);
    }
    AvsRowAttributeInfo[] array = rowAttributeInfoList2.ToArray();
    Intermech.Client.Core.AttributeDescriptor attributeDescriptor1 = (Intermech.Client.Core.AttributeDescriptor) null;
    int index1 = 0;
    foreach (AvsRowAttributeInfo rowAttributeInfo in array)
    {
      if (rowAttributeInfo != null && rowAttributeInfo.AttributeId != -1)
      {
        int index2 = checkedAttributesList.IndexOfID(rowAttributeInfo.AttributeId);
        if (index2 != -1 && index2 != index1)
        {
          Intermech.Client.Core.AttributeDescriptor attributeDescriptor2 = checkedAttributesList[index2];
          checkedAttributesList.RemoveAt(index2);
          if (index1 < checkedAttributesList.Count)
            checkedAttributesList.Insert(index1, (object) attributeDescriptor2);
          else
            checkedAttributesList.Add((object) attributeDescriptor2);
        }
        ++index1;
      }
    }
    string empty = string.Empty;
    Regex regex1;
    if (!interfaceObject1.UseRegularExpressions)
    {
      regex1 = (Regex) null;
    }
    else
    {
      Regex regex2 = regex1 = new Regex(str1, interfaceObject1.MatchCase ? RegexOptions.Compiled | RegexOptions.CultureInvariant : RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    }
    Regex regex3 = regex1;
    AVSRow selectedSpecRow = this.GetSelectedSpecRow();
    int num1 = -1;
    AVSViewMode viewMode = this.ViewMode;
    switch (this.ViewMode)
    {
      case AVSViewMode.Page:
        DocumentTreeNode rowDocNode = (DocumentTreeNode) null;
        List<DocumentTreeNode> selectedNodes = this.DocumentControl.SelectedNodes;
        if (selectedNodes != null && selectedNodes.Count > 0)
          rowDocNode = selectedNodes[0];
        if (!(rowDocNode is TextBoxElement))
          rowDocNode = (DocumentTreeNode) null;
        if (rowDocNode != null)
        {
          AvsRowAttributeInfo rowAttributeInfo = (AvsRowAttributeInfo) null;
          AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(rowDocNode);
          if (rowDocNode is TextData cell && avsDocRow != null)
            rowAttributeInfo = AVSDocument.GetAttrInfoFromCell(cell, -1, avsDocRow.IsFormB);
          if (rowAttributeInfo != null)
          {
            int attributeId = rowAttributeInfo.AttributeId;
            Intermech.Client.Core.AttributeDescriptor byId = checkedAttributesList.GetByID(attributeId);
            if (byId != null)
            {
              num1 = checkedAttributesList.IndexOf((object) byId);
              break;
            }
            break;
          }
          break;
        }
        break;
      case AVSViewMode.Grid:
        AVSColumn focusedColumn = this.virtualTree.FocusedColumn;
        if (focusedColumn != null)
        {
          AvsRowAttributeInfo rowAttributeInfo = focusedColumn.Tag.SpecRowAttributeInfo;
          if (rowAttributeInfo != null && rowAttributeInfo.AttributeId != -1)
          {
            num1 = checkedAttributesList.IndexOfID(rowAttributeInfo.AttributeId);
            break;
          }
          break;
        }
        break;
      default:
        return;
    }
    TextData textData = (TextData) null;
    int foundLength = -1;
    List<AVSRow> avsRowList = this.avsDocument.GetAllRows(false, false);
    if (interfaceObject1.SelectedSearchPlace == 1)
    {
      SpecificationSection selectedSection = this.GetSelectedSection();
      if (selectedSection != null)
        avsRowList = selectedSection.Rows;
    }
    if (avsRowList == null || avsRowList.Count == 0)
      return;
    int num2 = selectedSpecRow == null ? 0 : avsRowList.IndexOf(selectedSpecRow);
    if (num2 < 0)
      num2 = 0;
    int index3 = num2;
    int index4 = num1;
    int num3 = 0;
    avsRow1 = (AVSRow) null;
    bool flag1 = false;
    bool flag2 = true;
    bool flag3 = false;
    while (true)
    {
      AVSRow avsRow2;
      Intermech.Client.Core.AttributeDescriptor attributeDescriptor3;
      do
      {
        if (!flag1)
        {
          if (interfaceObject1.SearchDirrection == SearchDirrection.ToBegin)
          {
            if (index4 != -1)
            {
              --index4;
              if (index4 < 0)
              {
                --index3;
                index4 = checkedAttributesList.Count - 1;
              }
            }
            else
              index4 = checkedAttributesList.Count - 1;
          }
          else if (index4 != -1)
          {
            ++index4;
            if (index4 >= checkedAttributesList.Count)
            {
              index4 = 0;
              ++index3;
            }
          }
          else
            index4 = 0;
        }
        flag1 = false;
        switch (interfaceObject1.SearchDirrection)
        {
          case SearchDirrection.ToEnd:
            if (index3 >= avsRowList.Count)
            {
              if (num2 > 0 || num1 > 0)
              {
                if (MessageBox.Show("Достигнут конец документа. Начать поиск с начала?", "Intermech AVS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                  return;
                index3 = 0;
                index4 = 0;
                avsRow1 = avsRowList[index3];
                attributeDescriptor1 = checkedAttributesList[index4];
                flag1 = true;
                continue;
              }
              if (num3 > 0)
              {
                int num4 = (int) MessageBox.Show($"Закончен просмотр документа. Произведено замен: {num3.ToString()}.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
              }
              int num5 = (int) MessageBox.Show("Закончен просмотр документа. Искомый элемент не найден.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            break;
          case SearchDirrection.ToBegin:
            if (index3 < 0)
            {
              if (num2 < avsRowList.Count - 1 || num1 < checkedAttributesList.Count - 1)
              {
                if (MessageBox.Show("Достигнуто начало документа. Начать поиск с конца?", "Intermech AVS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                  return;
                index3 = avsRowList.Count - 1;
                index4 = checkedAttributesList.Count - 1;
                avsRow1 = avsRowList[index3];
                attributeDescriptor1 = checkedAttributesList[index4];
                flag1 = true;
                continue;
              }
              if (num3 > 0)
              {
                int num6 = (int) MessageBox.Show($"Закончен просмотр документа. Произведено замен: {num3.ToString()}.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
              }
              int num7 = (int) MessageBox.Show("Закончен просмотр документа. Искомый элемент не найден.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            break;
          case SearchDirrection.EntireDocSearch:
            if (((!flag2 ? 1 : (findOperation == AVSWindow.FindOperation.Replace ? 0 : (findOperation != AVSWindow.FindOperation.ReplaceAll ? 1 : 0))) & (flag3 ? 1 : 0)) != 0)
            {
              if (num3 > 0)
              {
                int num8 = (int) MessageBox.Show($"Закончен просмотр документа. Произведено замен: {num3.ToString()}.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
              }
              int num9 = (int) MessageBox.Show("Закончен просмотр документа. Искомый элемент не найден.", "Intermech AVS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            if (index3 == num2 && index4 == num1)
              flag3 = true;
            if (index3 >= avsRowList.Count)
            {
              index3 = 0;
              index4 = 0;
              flag1 = true;
              break;
            }
            break;
        }
        avsRow2 = avsRowList[index3];
        if (index4 == -1)
          index4 = 0;
        attributeDescriptor3 = checkedAttributesList[index4];
      }
      while (attributeDescriptor3 == null);
      if (num1 == -1)
        num1 = index4;
      bool flag4 = !avsRow2.IsFormB || attributeDescriptor3.AttributeID != AvsIDCache.Attr_Count || !attributeDescriptor3.IsRelationAttribute;
      for (int index5 = 0; (index5 == 0 || avsRow2.Relations != null && index5 < avsRow2.Relations.Count) && !(index5 > 0 & flag4); ++index5)
      {
        if (!attributeDescriptor3.IsRelationAttribute || avsRow2.Relations != null && avsRow2.Relations.Count != 0)
        {
          object fieldValue = avsRow2.GetFieldValue(new AvsRowAttributeInfo(attributeDescriptor3.IsRelationAttribute, attributeDescriptor3.AttributeID), index5, -1, false, false);
          string str2 = "";
          TextData cellForAttribute = avsRow2.GetDocumentCellForAttribute(new AvsRowAttributeInfo(attributeDescriptor3.IsRelationAttribute, attributeDescriptor3.AttributeID), -1);
          if (cellForAttribute != null)
            str2 = cellForAttribute.Text;
          int num10 = -1;
          switch (fieldValue)
          {
            case DBNull _:
            case null:
              if (num10 == -1 && str2 != null)
              {
                empty = str2.ToString();
                num10 = this.FindText0(interfaceObject1, empty, regex3, str1, out foundLength);
                if (findOperation == AVSWindow.FindOperation.Replace || findOperation == AVSWindow.FindOperation.ReplaceAll)
                  num10 = -1;
              }
              if ((findOperation == AVSWindow.FindOperation.Replace || findOperation == AVSWindow.FindOperation.ReplaceAll) && avsRow2.GetAttributeReadOnly(new AvsRowAttributeInfo(attributeDescriptor3.IsRelationAttribute, attributeDescriptor3.AttributeID), index5, avsRow2.Relations))
                num10 = -1;
              if (num10 != -1)
              {
                if (findOperation == AVSWindow.FindOperation.Replace || findOperation == AVSWindow.FindOperation.ReplaceAll)
                {
                  string str3 = num10 <= 0 ? string.Empty : empty.Substring(0, num10);
                  string str4 = num10 + foundLength >= empty.Length ? string.Empty : empty.Substring(num10 + foundLength, empty.Length - (num10 + foundLength));
                  string replaceWith = (findController.InterfaceObject as IFindOrReplaceTextController).ReplaceWith;
                  string str5 = str4;
                  string str6 = str3 + replaceWith + str5;
                  avsRow2.SetFieldValue(new AvsRowAttributeInfo(attributeDescriptor3.IsRelationAttribute, attributeDescriptor3.AttributeID), flag4 ? -1 : index5, -1, (object) str6, true, true, viewMode == AVSViewMode.Page, true, false, true);
                  ++num3;
                }
                Rectangle rectangle1 = Rectangle.Empty;
                switch (this.ViewMode)
                {
                  case AVSViewMode.Page:
                    if (textData == null)
                    {
                      int productIndex = flag4 ? 0 : this.avsDocument.GetProductIndex(avsRow2.Relations[index5].ProjectId);
                      textData = avsRow2.GetDocumentCellForAttribute(new AvsRowAttributeInfo(attributeDescriptor3.IsRelationAttribute, attributeDescriptor3.AttributeID), productIndex);
                    }
                    if (textData != null && textData is TextBoxElement)
                    {
                      TextBoxElement selection = textData as TextBoxElement;
                      this.DocumentControl.SetSelection((DocumentTreeNode) selection, true, Point.Empty, true, false);
                      if (selection.TextBox != null && selection.InPlaceEditorActive)
                        selection.TextBox.SetTextSelection(selection.PageUI, new TextSelection(num10, foundLength));
                      rectangle1 = selection.PageUI.PageControl.RectangleToScreen(selection.PageUI.Bounds);
                      break;
                    }
                    break;
                  case AVSViewMode.Grid:
                    this.virtualTree.FocusedItem = (object) avsRow2;
                    index4 = -1;
                    int num11 = 0;
                    using (List<AvsRowAttributeInfo>.Enumerator enumerator = this.GetGridViewColumns().GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        AvsRowAttributeInfo current = enumerator.Current;
                        if (current != null && current.AttributeId == attributeDescriptor3.AttributeID)
                        {
                          index4 = num11;
                          break;
                        }
                        ++num11;
                      }
                      break;
                    }
                }
                if (findOperation != AVSWindow.FindOperation.ReplaceAll && rectangle1 != Rectangle.Empty && findController is IFindDialog)
                {
                  IFindDialog findDialog = findController as IFindDialog;
                  Rectangle rectangle2 = new Rectangle(findDialog.GetScreenCoords(), findDialog.GetSize());
                  List<AVSWindow.MoveDirrectionSorter> dirrectionSorterList = new List<AVSWindow.MoveDirrectionSorter>(4);
                  if (rectangle1.Top < rectangle2.Bottom && rectangle1.Top > rectangle2.Top || rectangle1.Bottom < rectangle2.Bottom && rectangle1.Bottom > rectangle2.Top || rectangle2.Top < rectangle1.Bottom && rectangle2.Top > rectangle1.Top || rectangle2.Bottom < rectangle1.Bottom && rectangle2.Bottom > rectangle1.Top)
                  {
                    int step1 = rectangle1.Right - rectangle2.Left;
                    if (step1 > 0 && step1 < rectangle2.Width && step1 < Screen.PrimaryScreen.WorkingArea.Width - rectangle2.Right)
                      dirrectionSorterList.Add(new AVSWindow.MoveDirrectionSorter(AVSWindow.MoveDirrection.Right, step1));
                    int step2 = rectangle2.Right - rectangle1.Left;
                    if (step2 > 0 && step2 < rectangle2.Width && step2 < rectangle2.Left)
                      dirrectionSorterList.Add(new AVSWindow.MoveDirrectionSorter(AVSWindow.MoveDirrection.Left, step2));
                  }
                  if (rectangle1.Left < rectangle2.Right && rectangle1.Left > rectangle2.Left || rectangle1.Right < rectangle2.Right && rectangle1.Right > rectangle2.Left || rectangle2.Left < rectangle1.Right && rectangle2.Left > rectangle1.Left || rectangle2.Right < rectangle1.Right && rectangle2.Right > rectangle1.Left)
                  {
                    int step3 = rectangle2.Bottom - rectangle1.Top;
                    if (step3 > 0 && step3 < rectangle2.Height && step3 < rectangle2.Top)
                      dirrectionSorterList.Add(new AVSWindow.MoveDirrectionSorter(AVSWindow.MoveDirrection.Top, step3));
                    int step4 = rectangle1.Bottom - rectangle2.Top;
                    if (step4 > 0 && step4 < rectangle2.Height && step4 < Screen.PrimaryScreen.WorkingArea.Height - rectangle2.Bottom)
                      dirrectionSorterList.Add(new AVSWindow.MoveDirrectionSorter(AVSWindow.MoveDirrection.Bottom, step4));
                  }
                  if (dirrectionSorterList.Count > 0)
                  {
                    if (dirrectionSorterList.Count > 1)
                      dirrectionSorterList.Sort();
                    switch (dirrectionSorterList[0].MoveDirrection)
                    {
                      case AVSWindow.MoveDirrection.Top:
                        findDialog.SetScreenCoords(new Point(rectangle2.Left, rectangle2.Top - dirrectionSorterList[0].Step));
                        break;
                      case AVSWindow.MoveDirrection.Bottom:
                        findDialog.SetScreenCoords(new Point(rectangle2.Left, rectangle2.Top + dirrectionSorterList[0].Step));
                        break;
                      case AVSWindow.MoveDirrection.Left:
                        findDialog.SetScreenCoords(new Point(rectangle2.Left - dirrectionSorterList[0].Step, rectangle2.Top));
                        break;
                      case AVSWindow.MoveDirrection.Right:
                        findDialog.SetScreenCoords(new Point(rectangle2.Left + dirrectionSorterList[0].Step, rectangle2.Top));
                        break;
                    }
                  }
                }
                if (findOperation != AVSWindow.FindOperation.ReplaceAll)
                  return;
                continue;
              }
              continue;
            default:
              empty = fieldValue.ToString();
              num10 = this.FindText0(interfaceObject1, empty, regex3, str1, out foundLength);
              goto case null;
          }
        }
      }
      flag2 = false;
    }
  }

  private int FindText0(
    IFindOrReplaceTextController iFindOrReplaceTextController,
    string text,
    Regex regex,
    string findText,
    out int foundLength)
  {
    int text0 = -1;
    foundLength = -1;
    if (regex != null)
    {
      Match match = regex.Match(text);
      if (match.Success)
      {
        text0 = match.Index;
        foundLength = match.Length;
      }
    }
    else
    {
      text0 = this.FindText(iFindOrReplaceTextController, text, findText);
      if (text0 == -1)
        text0 = this.FindText(iFindOrReplaceTextController, text.Replace('\u000E', ' ').Replace('\u0017', '-').Replace('\u0017', '-'), findText.Replace('\u000E', ' ').Replace('\u0017', '-').Replace('\u0017', '-'));
      if (text0 != -1)
      {
        foundLength = findText.Length;
        if (iFindOrReplaceTextController.MatchWholeWord)
        {
          int index1 = text0 - 1;
          if (index1 >= 0 && !char.IsSeparator(text[index1]))
            return -1;
          int index2 = text0 + foundLength;
          if (index2 < text.Length && !char.IsSeparator(text[index2]))
            return -1;
        }
      }
    }
    return text0;
  }

  private int FindText(
    IFindOrReplaceTextController iFindOrReplaceTextController,
    string text,
    string findText)
  {
    text.IndexOf(findText);
    return !iFindOrReplaceTextController.MatchCase ? text.IndexOf(findText, 0, StringComparison.OrdinalIgnoreCase) : text.IndexOf(findText, 0);
  }

  /// <summary>Данные элементы документа принадлежат таблице спецификации</summary>
  /// <param name="context">Элементы документа</param>
  /// <returns></returns>
  public bool ContextIsSpectification(DocumentTreeNode[] context)
  {
    if (this.avsDocument == null)
      return false;
    if (context != null && context.Length != 0)
    {
      for (int index = 0; index < context.Length; ++index)
      {
        if ((!(context[index] is RectangleElement rectangleElement) || (rectangleElement.IsTableCell || rectangleElement is TableElement) && !this.avsDocument.IsSpecificationTable(rectangleElement.TopLevelTable) && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации" && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации #2" && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации #3" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей #2" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей #3") && !(context[index] is Page))
          return false;
      }
    }
    return true;
  }

  /// <summary>Данные элементы документа принадлежат таблице спецификации</summary>
  /// <param name="context">Элементы документа</param>
  /// <returns></returns>
  public bool ContextIsVedomost(DocumentTreeNode[] context)
  {
    if (this.avsDocument == null)
      return false;
    if (context != null && context.Length != 0)
    {
      for (int index = 0; index < context.Length; ++index)
      {
        if (context[index] is RectangleElement rectangleElement && (rectangleElement.IsTableCell || rectangleElement is TableElement) && this.avsDocument.IsSpecificationTable(rectangleElement.TopLevelTable) || (rectangleElement == null || (rectangleElement.IsTableCell || rectangleElement is TableElement) && !this.avsDocument.IsSpecificationTable(rectangleElement.TopLevelTable) && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации" && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации #2" && rectangleElement.TopLevelTable.TemplateId != "Заголовок спецификации #3" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей #2" && rectangleElement.TopLevelTable.TemplateId != "Пропуски строк после записей #3") && !(context[index] is Page))
          return false;
      }
    }
    return true;
  }

  /// <summary>Изменить контекстное меню элемента</summary>
  private void DocumentControl_GetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    try
    {
      ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
      if (commandManager == null)
        return;
      if (this.CommandManager != null)
      {
        this.CommandManager.ActiveTarget = (ICommandTarget) this;
        this.CommandManager.QueryStatus();
      }
      for (int index = e.ContextMenuItems.Count - 1; index > -1; --index)
      {
        MenuButtonItem contextMenuItem = e.ContextMenuItems[index];
        if (contextMenuItem.CommandName == "BlockGeometryChanging" || contextMenuItem.CommandName == "UnblockGeometryChanging" || contextMenuItem.CommandName == "ConvertToLabel" || contextMenuItem.CommandName == "ConvertToTextBox" || contextMenuItem.CommandName == "ConvertToContainer" || contextMenuItem.CommandName == "ConvertToArea" || contextMenuItem.CommandName == "RemoveRow" || contextMenuItem.CommandName == "RemoveColumn" || contextMenuItem.CommandName == "RemoveCell" || contextMenuItem.CommandName == "AddTableRowAbove" || contextMenuItem.CommandName == "AddTableRowBelow" || contextMenuItem.CommandName == "AddRowFromTemplateAbove" || contextMenuItem.CommandName == "AddRowFromTemplateBelow" || contextMenuItem.CommandName == "AddTableColumnLeft" || contextMenuItem.CommandName == "AddTableColumnRight" || contextMenuItem.CommandName == "AddTableSection" || contextMenuItem.CommandName == "SplitCell" || contextMenuItem.CommandName == "MergeCells" || contextMenuItem.CommandName == "BringToFront" || contextMenuItem.CommandName == "MoveToEnd" || contextMenuItem.CommandName == "MoveUp" || contextMenuItem.CommandName == "MoveDown" || contextMenuItem.CommandName == "SendToEnd" || contextMenuItem.CommandName == "MoveToBegin" || contextMenuItem.CommandName == "ConvertToHeader" || contextMenuItem.CommandName == "DiconnectDataTable" || contextMenuItem.CommandName == "ChangeVisibility" || contextMenuItem.CommandName == "CreateNextPageTemplate" || contextMenuItem.CommandName == "DocEditor.InsertAdditionalPages" || contextMenuItem.CommandName == "DocEditor.RemoveAdditionalPages")
          e.ContextMenuItems.Remove(contextMenuItem);
      }
      int num = this.ContextIsSpectification(e.Context) ? 1 : 0;
      if (num != 0)
      {
        int contextMenuItemIndex = NodeContextMenu.GetContextMenuItemIndex("Delete", e.ContextMenuItems);
        this.InsertEnabledContextMenu("AVS.DeleteObjects", e.ContextMenuItems, contextMenuItemIndex + 1, commandManager);
        this.AddEnabledContextMenu("AVS.PasteBreak", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.PasteNonBreakSpace", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.CommonPositions", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddSkipLineBefore", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddSkipLineAfter", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.FromNewPage", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.UndoFromNewPage", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.UndoSkipLineBefore", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.UndoSkipLineAfter", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.InsertAdditionalPages", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.RemoveAdditionalPages", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.Hide", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.UnHide", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.Group", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.RowProperties", e.ContextMenuItems, commandManager);
        ICommandState command = commandManager.FindCommand("AVS.Podbor.RangeModeForRow");
        if (command != null)
        {
          this.QueryStatus(command);
          if (command.Enabled)
            this.AddEnabledContextMenu("AVS.Podbor.LimitAndValueModeSubmenu", e.ContextMenuItems, commandManager);
        }
        MenuButtonItem menuButtonItem = this.AddEnabledContextMenu("AVS.AddNewSpecRow", e.ContextMenuItems, commandManager);
        LogManager.AddLine($"AVS.AddNewSpecRow - еxist = {(menuButtonItem != null).ToString()}");
        if (menuButtonItem != null)
        {
          bool flag = menuButtonItem.Enabled;
          string str1 = flag.ToString();
          flag = menuButtonItem.Visible;
          string str2 = flag.ToString();
          LogManager.AddLine($"AVS.AddNewSpecRow - enabled = {str1} - visible = {str2}");
        }
        this.AddEnabledContextMenu("AVS.AddSpecRow", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddSpecRowFromImbase", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddGroupSpecRowFromImbase", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddOtherRecordTypes", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.MoveSpecRow", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.ChangeRecordIspolnenie", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddSpecSection", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.DeleteEmptySections", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.ShowEmptySections", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.ShowAllDocRows", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.HideDocRowsWithoutCount", e.ContextMenuItems, commandManager);
        if (this._pdmClient != null && !this.ReadOnly)
        {
          this.AddEnabledContextMenu("PDM.CreateSubstitutesGroup", e.ContextMenuItems, commandManager);
          this.AddEnabledContextMenu("PDM.MakeActualSubstitute", e.ContextMenuItems, commandManager);
          this.AddEnabledContextMenu("PDM.EditSubstitutesGroup", e.ContextMenuItems, commandManager);
          this.AddEnabledContextMenu("PDM.DeleteSubstitutesGroup", e.ContextMenuItems, commandManager);
        }
        if (this.ViewMode == AVSViewMode.Grid)
          this.AddEnabledContextMenu("AVS.SelectGridColumns", e.ContextMenuItems, commandManager);
      }
      DocumentTreeNode contextOnlyOneNode = this.GetCommandContext_OnlyOneNode();
      if (this.avsDocument.FindParentLRIRowDocNode(contextOnlyOneNode) is TableData)
      {
        this.AddEnabledContextMenu("AVS.AddLRIRecord_Before", e.ContextMenuItems, commandManager);
        this.AddEnabledContextMenu("AVS.AddLRIRecord_After", e.ContextMenuItems, commandManager);
      }
      else if (contextOnlyOneNode != null && this.avsDocument.IsDocumentNodeOnLRIPage(contextOnlyOneNode))
        this.AddEnabledContextMenu("AVS.AddLRIRecord", e.ContextMenuItems, commandManager);
      this.AddEnabledContextMenu("AVSParametersCard", e.ContextMenuItems, commandManager);
      if (num != 0 || this.ReadOnly)
      {
        this.AddEnabledContextMenu("AVS.Property", e.ContextMenuItems, commandManager);
        MenuButtonItem menuButtonItem = this.AddEnabledContextMenu("AVS.NavigatorCommands", e.ContextMenuItems, commandManager);
        if (menuButtonItem != null)
        {
          if (menuButtonItem.Items.Count == 0)
            menuButtonItem.Items.Add("");
          menuButtonItem.BeforePopup -= new MenuItemBase.BeforePopupEventHandler(this.mi_BeforePopup);
          menuButtonItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.mi_BeforePopup);
          menuButtonItem.AfterPopup -= new EventHandler(this.mi_AfterPopup);
          menuButtonItem.AfterPopup += new EventHandler(this.mi_AfterPopup);
        }
      }
      this.AddEnabledContextMenu("DocElementProperty", e.ContextMenuItems, commandManager);
      if (this.ReadOnly || e.Context == null || e.Context.Length == 0 || !(e.Context[0] is RectangleElement rectangleElement) || rectangleElement.Id == null || !rectangleElement.Id.StartsWith("Лист"))
        return;
      ICommandState command1 = commandManager.FindCommand("AVS.ChangePageNumberingStyle");
      if (command1 == null)
        return;
      this.QueryStatus(command1);
      if (!command1.Enabled)
        return;
      this.AddEnabledContextMenu("AVS.ChangePageNumberingStyle", e.ContextMenuItems, commandManager);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Получить список разрешённых команд, работающих с допустимыми заменами, для текущих выделенных связей</summary>
  /// <returns>Список разрешённых команд, работающих с допустимыми заменами, для текущих выделенных связей</returns>
  protected PDMSubstitutesCommands GetSubstitutesCommands()
  {
    int parObjType;
    List<int> items;
    this.PrepareItemsServices(out parObjType, out items);
    return this._pdmClient == null ? PDMSubstitutesCommands.None : this._pdmClient.GetEnabledSubstitutesCommands(parObjType, items);
  }

  /// <summary>Метод позволяет подготовить коллекцию выделенных типов связей</summary>
  /// <param name="parObjType">Идентификатор родительского типа объектов</param>
  /// <param name="items">Коллекция выделенных в спецификации типов связей</param>
  /// <returns>true, если все данные корректно собраны</returns>
  protected bool PrepareItemsServices(out int parObjType, out List<int> items)
  {
    parObjType = -1;
    items = (List<int>) null;
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count > 0)
    {
      List<int> intList = new List<int>(selectedSpecRows.Count);
      int num = this.avsDocument != null ? this.avsDocument.productType : -1;
      for (int index = 0; index < selectedSpecRows.Count; ++index)
      {
        AVSRow avsRow = selectedSpecRows[index];
        if (avsRow != null && avsRow.HasRelation && avsRow.RelType != -1 && avsRow.RelId != -1L)
          intList.Add(avsRow.RelType);
      }
      if (intList.Count > 0)
      {
        parObjType = num;
        items = intList;
      }
      if (items != null)
        return true;
    }
    return false;
  }

  /// <summary>Метод позволяет подготовить коллекцию выделенных элементов и контекст для них - для
  /// дальнейшего использования в командах "Навигатора" (и совместимых с ними)</summary>
  /// <param name="firstArticle">Идентификатор версии исполнения, информация о котором
  /// должна попасть в список-результат в первую очередь, или Intermech.Consts.UnknownObjectID</param>
  /// <param name="items">Коллекция выделенных в спецификации связей</param>
  /// <param name="viewServices">Контейнер сервисов (контекст для коллекции выделенных в спецификации связей)</param>
  /// <param name="createPDMOptions">Создавать PDM опции для работы с допзаменами</param>
  /// <returns>true, если все данные корректно собраны</returns>
  protected bool PrepareItemsServices(
    long firstArticle,
    out ISelectedItems items,
    out System.IServiceProvider viewServices,
    bool createPDMOptions = true)
  {
    items = (ISelectedItems) null;
    viewServices = (System.IServiceProvider) null;
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count > 0)
    {
      AdvancedServiceContainer services = new AdvancedServiceContainer();
      services.AddService(typeof (IViewState), (object) new ViewStateService());
      viewServices = (System.IServiceProvider) services;
      items = AVSSelectedItemsHelper.GetRelationsSelectedItems(firstArticle, selectedSpecRows, (System.IServiceProvider) services);
      List<long> articles = new List<long>();
      if (createPDMOptions)
      {
        PDMSubstitutesEditorOptionsHolder serviceInstance = new PDMSubstitutesEditorOptionsHolder(PDMSubstitutesEditorMode.Default, (AVSSpecificationForm) this.AVSDocument.AvsDocumentForm, articles);
        if (this.AVSDocument.IsFormB || this.AVSDocument.AvsDocumentForm == AVSDocumentForm.V)
        {
          serviceInstance.Mode = PDMSubstitutesEditorMode.DialogMultiInstances;
          serviceInstance.Articles.AddRange((IEnumerable<long>) this.avsDocument.ProductsID);
        }
        else if (!firstArticle.IsUndefinedId())
          serviceInstance.Articles.Add(firstArticle);
        else
          serviceInstance.Articles.Add(this.avsDocument.productId);
        services.AddService(typeof (PDMSubstitutesEditorOptionsHolder), (object) serviceInstance);
      }
    }
    if (items != null)
      return true;
    viewServices = (System.IServiceProvider) null;
    return false;
  }

  protected ISelectedItems GetSelectedItems(long firstArticle)
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count == 0)
      return (ISelectedItems) null;
    AdvancedServiceContainer services = new AdvancedServiceContainer();
    services.AddService(typeof (IViewState), (object) new ViewStateService());
    return AVSSelectedItemsHelper.GetRelationsSelectedItems(firstArticle, selectedSpecRows, (System.IServiceProvider) services);
  }

  protected System.IServiceProvider CreateServiceProviderForRemoveAllAllowableSubstitutions()
  {
    AdvancedServiceContainer allowableSubstitutions = new AdvancedServiceContainer();
    allowableSubstitutions.AddService(typeof (IViewState), (object) new ViewStateService());
    allowableSubstitutions.AddService(typeof (PDMSubstitutesEditorOptionsHolder), (object) new PDMSubstitutesEditorOptionsHolder(PDMSubstitutesEditorMode.Default, (AVSSpecificationForm) this.AVSDocument.AvsDocumentForm, this.avsDocument.ProductsID));
    return (System.IServiceProvider) allowableSubstitutions;
  }

  private void DocumentControl_ActivePageChanged(object sender, EventArgs e)
  {
    try
    {
      if (this.DocumentControl.ActivePage == null)
        return;
      this.DocumentControl.ActivePage.PageControl.BeforeDoDragDrop -= new BeforeDoDragDrop_EventHandler(this.PageControl_BeforeDoDragDrop);
      this.DocumentControl.ActivePage.PageControl.DragOver -= new DragEventHandler(this.PageControl_DragOver);
      this.DocumentControl.ActivePage.PageControl.DragDrop -= new DragEventHandler(this.PageControl_DragDrop);
      this.DocumentControl.ActivePage.PageControl.BeforeDoDragDrop += new BeforeDoDragDrop_EventHandler(this.PageControl_BeforeDoDragDrop);
      this.DocumentControl.ActivePage.PageControl.DragOver += new DragEventHandler(this.PageControl_DragOver);
      this.DocumentControl.ActivePage.PageControl.DragDrop += new DragEventHandler(this.PageControl_DragDrop);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void PageControl_DragDrop(object sender, DragEventArgs e)
  {
    try
    {
      if (e.Data == null)
      {
        LogManager.AddLine("AVS. PageControl_DragDrop# Data == null", true);
      }
      else
      {
        List<AVSRow> collection = e.Data.GetData(typeof (List<AVSRow>)) as List<AVSRow>;
        IOSource data1 = e.Data.GetData(typeof (IOSource)) as IOSource;
        DragNotesWrapper data2 = e.Data.GetData(typeof (DragNotesWrapper)) as DragNotesWrapper;
        ArrayList arrayList = new ArrayList();
        if (!(sender is Intermech.Document.UI.PageControl pageControl))
        {
          LogManager.AddLine("AVS. PageControl_DragDrop# pageControl == null", true);
        }
        else
        {
          Point client = pageControl.PointToClient(new Point(e.X, e.Y));
          if (data2 != null)
            collection = data2.Rows;
          if ((collection == null || collection.Count <= 0 ? 0 : (collection[0].avsDocument == this.AVSDocument ? 1 : 0)) != 0 || data2 != null)
          {
            PageElementUI elementUiAtPoint = pageControl.GetPageElementUIAtPoint(client, true);
            if (elementUiAtPoint != null)
            {
              rowDocNode = (RectangleElement) (elementUiAtPoint.Element as TableData);
              if (rowDocNode == null && elementUiAtPoint.Element is RectangleElement rowDocNode)
                rowDocNode = (RectangleElement) rowDocNode.ParentCell;
              if (rowDocNode != null && collection != null)
              {
                AVSRow avsDocRow = this.AVSDocument.GetAvsDocRow((DocumentTreeNode) rowDocNode);
                if (avsDocRow == null)
                {
                  for (RectangleElement nextNode = rowDocNode.NextNode; avsDocRow == null && nextNode != null; nextNode = nextNode.NextNode)
                    avsDocRow = this.AVSDocument.GetAvsDocRow((DocumentTreeNode) nextNode);
                  if (avsDocRow == null)
                  {
                    for (RectangleElement prevNode = rowDocNode.PrevNode; avsDocRow == null && prevNode != null; prevNode = prevNode.PrevNode)
                      avsDocRow = this.AVSDocument.GetAvsDocRow((DocumentTreeNode) prevNode);
                  }
                }
                if (avsDocRow != null && avsDocRow.Section != null)
                {
                  SpecificationSection section = avsDocRow.Section;
                  RectangleElement docNode = (RectangleElement) avsDocRow.DocNode;
                  bool flag = false;
                  if (docNode is IPageElementWithInterface elementWithInterface)
                  {
                    Rectangle bounds = elementWithInterface.PageUI.Bounds;
                    int num = bounds.Y + bounds.Height / 2;
                    Point point1 = new Point(bounds.Left, bounds.Bottom);
                    Point point2 = new Point(bounds.Right, bounds.Bottom);
                    if (client.Y < num)
                      flag = true;
                  }
                  for (int index = collection.Count - 1; index >= 0; --index)
                    avsDocRow.Section.Rows.Remove(collection[index]);
                  int index1 = avsDocRow.Section.Rows.IndexOf(avsDocRow) + 1;
                  if (flag)
                    --index1;
                  if (index1 != -1)
                  {
                    avsDocRow.Section.Rows.InsertRange(index1, (IEnumerable<AVSRow>) collection);
                    foreach (AVSRow avsRow in collection)
                    {
                      avsRow.Index = -1;
                      arrayList.Add((object) avsRow);
                    }
                    for (int index2 = 0; index2 < avsDocRow.Section.Rows.Count; ++index2)
                      avsDocRow.Section.Rows[index2].Index = index2;
                  }
                }
                e.Effect = DragDropEffects.None;
              }
            }
            if (this.AVSDocument.Document != null)
              this.AVSDocument.Document.UpdateLayout(false, true);
            else
              LogManager.AddLine("AVS. PageControl_DragDrop# Document == null", true);
          }
          else if (data1 != null && data1.SelectedItems != null && data1.SelectedItems.Count > 0 || collection != null)
          {
            PageElementUI elementUiAtPoint = pageControl.GetPageElementUIAtPoint(client, true);
            DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
            if (elementUiAtPoint != null)
            {
              documentTreeNode = (DocumentTreeNode) (elementUiAtPoint.Element as TableData);
              if (documentTreeNode == null)
              {
                documentTreeNode = (DocumentTreeNode) (elementUiAtPoint.Element as RectangleElement);
                if (documentTreeNode != null)
                  documentTreeNode = (DocumentTreeNode) ((RectangleElement) documentTreeNode).ParentCell;
              }
            }
            if (documentTreeNode != null && AVSDocument.IsSpecSectionDocNodeChild(documentTreeNode))
            {
              bool flag = false;
              if (documentTreeNode is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
              {
                Rectangle bounds = elementWithInterface.PageUI.Bounds;
                int num = bounds.Y + bounds.Height / 2;
                if (client.Y < num && (AVSDocument.IsSpecRowDocNodeChild(documentTreeNode) || AVSDocument.IsNoteRowDocNodeChild(documentTreeNode)))
                  flag = true;
              }
              if (flag && documentTreeNode.Index > 0)
                documentTreeNode = documentTreeNode.Parent.Nodes[documentTreeNode.Index - 1];
              if (pageControl.DocumentControl != null)
                pageControl.DocumentControl.SetSelection(documentTreeNode, false, false);
            }
            List<object> objectList = new List<object>();
            int num1 = data1 == null || data1.SelectedItems == null ? 0 : (data1.SelectedItems.Count > 0 ? 1 : 0);
            if (data1 != null && data1.SelectedItems != null && data1.SelectedItems.Count > 0)
            {
              for (int index = 0; index < data1.SelectedItems.Count; ++index)
                objectList.Add((object) data1.SelectedItems.GetItemID(index));
              this.ContextAddSpecRow(documentTreeNode, -1, objectList.ToArray());
            }
            else
            {
              // ISSUE: explicit non-virtual call
              ArrayList rowsList = new ArrayList(collection != null ? __nonvirtual (collection.Count) : 0);
              long objectID = -1;
              TableData tableData = (TableData) null;
              Guid empty = Guid.Empty;
              if (collection != null && collection.Count > 0)
              {
                for (int index = 0; index < collection.Count; ++index)
                {
                  AVSRow avsRow = collection[index];
                  long id = 0;
                  long result = long.MinValue;
                  long owner = 0;
                  if (avsRow != null)
                  {
                    tableData = avsRow.DocNode;
                    if (!avsRow.ObjectId.IsUndefinedId())
                    {
                      using (SessionKeeper sessionKeeper = new SessionKeeper())
                      {
                        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
                        if (dbObject != null)
                        {
                          id = dbObject.ID;
                          owner = dbObject.OwnerID;
                        }
                      }
                    }
                    DBTypedObjectID dbTypedObjectId = new DBTypedObjectID(avsRow.ObjType, avsRow.ObjectId, id, avsRow.ObjCaption, owner, 0L, 0L, string.Empty, 0L);
                    DBRelationID dbRelationId = new DBRelationID(avsRow.RelId, avsRow.ObjectId, avsRow.RelType, avsRow.SortIndex, avsRow.RelGuid, avsRow.ProductID);
                    if (tableData != null)
                      rowsList.Add((object) new AvsRowClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId, (TableData) tableData.Clone(), avsRow.IsFormB));
                    else
                      rowsList.Add((object) new ClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId));
                  }
                  else if (tableData != null)
                  {
                    if (tableData.Reference is ReferenceToDBObject reference)
                    {
                      using (SessionKeeper sessionKeeper = new SessionKeeper())
                      {
                        if (!reference.IsConnectedObjectRef)
                          reference.UpdateDBObjectInfo(sessionKeeper.Session, this.FiltrationOwnerID);
                        if (!long.TryParse(tableData.GetAttributeValue(AVSRow.RowAttr_SortIndex, true), out result))
                          result = long.MinValue;
                        IDBObject dbObject = reference.GetDBObject(sessionKeeper.Session, this.FiltrationOwnerID);
                        if (dbObject != null)
                        {
                          owner = dbObject.OwnerID;
                          id = dbObject.ID;
                        }
                        DBTypedObjectID dbTypedObjectId = new DBTypedObjectID(reference.DBObjectType, reference.DBObjectID, id, reference.DBObjectCaption, owner, 0L, 0L, string.Empty, 0L);
                        DBRelationID dbRelationId = new DBRelationID(reference.DBRelationID, reference.DBObjectID, reference.DBRelationType, result, reference.DBRelationGuid, reference.DBProjectID);
                        PageData page = tableData.Page;
                        rowsList.Add((object) new AvsRowClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId, (TableData) tableData.Clone(), page != null && this.avsDocument.IsFormBPage(page)));
                      }
                    }
                    else
                      rowsList.Add((object) tableData.Clone());
                  }
                }
              }
              if (rowsList.Count > 0 && ServicesManager.GetService(typeof (IClipboard)) is IClipboard)
                this.PasteFromClipboardCommand(documentTreeNode, new AVSRowClipboardCollection(rowsList, this.avsDocument.AVSDocType, this.avsDocument.AvsDocumentForm, this.avsDocument.DocumentID));
            }
          }
          if (this.Document == null || this.DocumentControl == null)
            return;
          this.avsDocument.IndexAVSDocument(false);
          this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          this.Document.SuspendRefreshUI();
          this.DocumentControl.SetSelection(new List<DocumentTreeNode>(), false, false);
          List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
          foreach (object obj in arrayList)
          {
            if (obj is DocumentTreeNode)
              selection.Add(obj as DocumentTreeNode);
            if (obj is AVSRow)
              selection.Add((DocumentTreeNode) (obj as AVSRow).DocNode);
          }
          this.DocumentControl.SetSelection(selection, true, false);
          this.Document.ResumeRefreshUI(true);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void PageControl_DragOver(object sender, DragEventArgs e)
  {
    try
    {
      IOSource data1 = e.Data.GetData(typeof (IOSource)) as IOSource;
      List<AVSRow> data2 = e.Data.GetData(typeof (List<AVSRow>)) as List<AVSRow>;
      DragNotesWrapper data3 = e.Data.GetData(typeof (DragNotesWrapper)) as DragNotesWrapper;
      e.Effect = DragDropEffects.None;
      Intermech.Document.UI.PageControl pageControl = sender as Intermech.Document.UI.PageControl;
      Point client = pageControl.PointToClient(new Point(e.X, e.Y));
      if (data2 != null || data3 != null)
      {
        PageElementUI elementUiAtPoint = pageControl.GetPageElementUIAtPoint(client, true);
        if (elementUiAtPoint == null)
          return;
        bool flag1 = false;
        docNode = (RectangleElement) (elementUiAtPoint.Element as TableData);
        if (docNode == null && elementUiAtPoint.Element is RectangleElement docNode)
          docNode = (RectangleElement) docNode.ParentCell;
        bool flag2 = true;
        if (docNode != null && data2 != null)
        {
          bool flag3 = data2.Count > 0 && data2[0].avsDocument == this.AVSDocument;
          flag2 = !this.avsDocument.AutoSort;
          if (!flag3 || !this.avsDocument.AutoSort)
          {
            TableData ownerSubTable = docNode.OwnerSubTable;
            if (ownerSubTable != null)
            {
              if (ownerSubTable.FindFirstTable().Tag is SpecificationSection tag && data2[0].Section != null && tag.SectionID == data2[0].Section.SectionID && (!flag3 || !this.avsDocument.AutoSort))
                e.Effect = DragDropEffects.Move;
              if (!flag3)
                e.Effect = DragDropEffects.Move;
            }
            if (!AVSDocument.IsSpecRowDocNode((DocumentTreeNode) docNode) && (!AVSDocument.IsNoteRowDocNode((DocumentTreeNode) docNode) || docNode.TableCellType != CellType.DataCell))
              e.Effect = DragDropEffects.None;
            if (e.Effect == DragDropEffects.Move && ((docNode.TableCellType != CellType.Header ? 0 : (ownerSubTable != null ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
            {
              if (docNode.IsLastHeader())
              {
                e.Effect = DragDropEffects.Move;
                flag1 = true;
              }
              else
                e.Effect = DragDropEffects.None;
            }
          }
        }
        if (docNode != null && data3 != null)
        {
          TableData ownerSubTable = docNode.OwnerSubTable;
          e.Effect = DragDropEffects.Move;
          if (ownerSubTable == null || !(data3.Notes[0] is RectangleElement))
          {
            e.Effect = DragDropEffects.None;
          }
          else
          {
            if (!AVSDocument.IsSpecRowDocNode((DocumentTreeNode) docNode) && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) docNode))
              e.Effect = DragDropEffects.None;
            if (data3.IsHeaders != (docNode.TableCellType == CellType.Header))
            {
              if (data3.IsHeaders || !docNode.IsLastHeader())
                e.Effect = DragDropEffects.None;
              else
                flag1 = true;
            }
            if (AVSDocument.IsSpecRowDocNode((DocumentTreeNode) docNode) && this.AVSDocument.AutoSort)
              e.Effect = DragDropEffects.None;
          }
        }
        if (((e.Effect != DragDropEffects.Move || docNode == null ? 0 : ((docNode as IPageElementWithInterface).PageUI != null ? 1 : 0)) & (flag2 ? 1 : 0)) == 0)
          return;
        Rectangle rectangle = (docNode as IPageElementWithInterface).PageUI.Bounds;
        int num = rectangle.Y + rectangle.Height / 2;
        Point point1 = new Point(rectangle.Left, rectangle.Bottom);
        Point point2 = new Point(rectangle.Right, rectangle.Bottom);
        if (client.Y < num && !flag1)
        {
          point1 = new Point(rectangle.Left, rectangle.Top);
          point2 = new Point(rectangle.Right, rectangle.Top);
        }
        rectangle = Rectangle.FromLTRB(point1.X, point1.Y, point2.X, point2.Y);
        pageControl.DragLinePosition = rectangle;
      }
      else
      {
        if (data1 == null || data1.SelectedItems == null || data1.SelectedItems.Count <= 0)
          return;
        DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
        PageElementUI elementUiAtPoint = pageControl.GetPageElementUIAtPoint(client, true);
        if (elementUiAtPoint != null)
          documentTreeNode = (DocumentTreeNode) elementUiAtPoint.Element;
        int num1 = !this.CanAddNodes(documentTreeNode, data1.SelectedItems) ? 0 : (elementUiAtPoint != null ? 1 : 0);
        e.Effect = num1 == 0 ? DragDropEffects.None : DragDropEffects.Copy;
        if (num1 == 0)
          return;
        docNode = (RectangleElement) (elementUiAtPoint.Element as TableData);
        if (docNode == null && elementUiAtPoint.Element is RectangleElement docNode)
          docNode = (RectangleElement) docNode.ParentCell;
        if (e.Effect != DragDropEffects.Copy || docNode == null || !AVSDocument.IsSpecSectionDocNodeChild(documentTreeNode) || (docNode as IPageElementWithInterface).PageUI == null)
          return;
        Rectangle rectangle = (docNode as IPageElementWithInterface).PageUI.Bounds;
        int num2 = rectangle.Y + rectangle.Height / 2;
        Point point3 = new Point(rectangle.Left, rectangle.Bottom);
        Point point4 = new Point(rectangle.Right, rectangle.Bottom);
        if (client.Y < num2 && (AVSDocument.IsSpecRowDocNodeChild((DocumentTreeNode) docNode) || AVSDocument.IsNoteRowDocNodeChild((DocumentTreeNode) docNode)))
        {
          point3 = new Point(rectangle.Left, rectangle.Top);
          point4 = new Point(rectangle.Right, rectangle.Top);
        }
        rectangle = Rectangle.FromLTRB(point3.X, point3.Y, point4.X, point4.Y);
        pageControl.DragLinePosition = rectangle;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void PageControl_BeforeDoDragDrop(object sender, BeforeDoDragDrop_EventArgs e)
  {
    try
    {
      if (AvsConfig.General.DisableSortPageView)
        e.DoDragDrop = false;
      else if (this.ReadOnly)
      {
        e.DoDragDrop = false;
      }
      else
      {
        if (this.DocumentControl.SelectedNodes == null || this.DocumentControl.SelectedNodes.Count == 0)
          return;
        List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
        if (selectedSpecRows.Count > 0)
        {
          bool flag = true;
          SpecificationSection section = selectedSpecRows[0].Section;
          foreach (AVSRow avsRow in selectedSpecRows)
          {
            if (avsRow.Section != section)
            {
              flag = false;
              break;
            }
          }
          if (!flag)
            return;
          e.ObjectToDrag = (object) selectedSpecRows;
          e.DoDragDrop = true;
          e.Effect = DragDropEffects.Move;
        }
        else
        {
          List<DocumentTreeNode> Notes;
          if (this.DocumentControl.SelectedNodes.Count > 1)
            Notes = this.DocumentControl.SelectedNodes;
          else if (this.DocumentControl.SelectedNodes[0].IsVirtualNode)
          {
            Notes = (this.DocumentControl.SelectedNodes[0] as RectangleElement).GetRealCells();
          }
          else
          {
            Notes = new List<DocumentTreeNode>();
            Notes.Add(this.DocumentControl.SelectedNodes[0]);
          }
          bool? nullable1 = new bool?();
          TableData tableData = (TableData) null;
          bool flag1 = true;
          foreach (DocumentTreeNode documentTreeNode in Notes)
          {
            if (!(documentTreeNode is TableData docNode) || !docNode.IsRow || !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) docNode))
            {
              flag1 = false;
              break;
            }
            if (nullable1.HasValue)
            {
              bool? nullable2 = nullable1;
              bool flag2 = docNode.TableCellType == CellType.Header;
              if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
              {
                flag1 = false;
                break;
              }
            }
            nullable1 = new bool?(docNode.TableCellType == CellType.Header);
            if (tableData == null)
              tableData = docNode.OwnerSubTable;
            else if (tableData != docNode.OwnerSubTable)
            {
              flag1 = false;
              break;
            }
          }
          if (!flag1)
            return;
          DragNotesWrapper dragNotesWrapper = new DragNotesWrapper(Notes, nullable1.Value);
          List<AVSRow> avsRowList = new List<AVSRow>();
          foreach (DocumentTreeNode rowDocNode in Notes)
          {
            AVSRow avsDocRow = this.AVSDocument.GetAvsDocRow(rowDocNode);
            if (avsDocRow != null)
              avsRowList.Add(avsDocRow);
          }
          dragNotesWrapper.Rows = avsRowList;
          e.ObjectToDrag = (object) dragNotesWrapper;
          e.DoDragDrop = true;
          e.Effect = DragDropEffects.Move;
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void DocumentControl_CanShiftSelect(object sender, CanShiftSelect_EventArgs e)
  {
    try
    {
      if (this.ViewMode == AVSViewMode.Grid || e.Node == null || this.DocumentControl.SelectedNodes == null || this.DocumentControl.SelectedNodes.Count != 1 || !this.DocumentControl.SelectedNodes[0].IsVirtualNode || !(this.DocumentControl.SelectedNodes[0] is RectangleElement selectedNode))
        return;
      bool flag = false;
      foreach (DocumentTreeNode realCell in selectedNode.GetRealCells())
      {
        if (AVSDocument.IsSpecRowDocNode(realCell) || AVSDocument.IsNoteRowDocNode(realCell))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      if (!e.Node.IsVirtualNode)
      {
        if (AVSDocument.IsSpecRowDocNodeChild(e.Node) || AVSDocument.IsNoteRowDocNodeChild(e.Node))
          e.CanSelect = true;
        else
          e.CanSelect = false;
      }
      else
        e.CanSelect = false;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void DocumentControl_BeforeSelectionChanged(
    object sender,
    BeforeSelectionChanged_EventArgs e)
  {
    try
    {
      if (this.ViewMode == AVSViewMode.Grid || e.Selection == null || e.Selection.Count != 1 || !e.Selection[0].IsVirtualNode || !(e.Selection[0] is RectangleElement selNode))
        return;
      bool flag = false;
      foreach (DocumentTreeNode realCell in selNode.GetRealCells())
      {
        if (AVSDocument.IsSpecRowDocNode(realCell) || AVSDocument.IsNoteRowDocNode(realCell))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      this.RemoveNotRowsFromSelection((DocumentTreeNode) selNode);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Удаляем из выделения все элементы не являющиеся строками спецификации</summary>
  /// <param name="selNode"></param>
  private bool RemoveNotRowsFromSelection(DocumentTreeNode selNode)
  {
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    foreach (DocumentTreeNode node in selNode.Nodes)
    {
      if (!node.IsVirtualNode)
      {
        if (!AVSDocument.IsSpecRowDocNode(node) && !AVSDocument.IsNoteRowDocNode(node))
          documentTreeNodeList.Add(node);
      }
      else if (this.RemoveNotRowsFromSelection(node))
        documentTreeNodeList.Add(node);
    }
    if (selNode.IsVirtualNode)
    {
      foreach (DocumentTreeNode node in documentTreeNodeList)
      {
        if (selNode.Nodes.Contains(node) && selNode.NodesCount == 1)
          return true;
        selNode.RemoveChildNode(node, false, false);
      }
    }
    return false;
  }

  private void UpdateISimpleSelectedItemsService()
  {
    if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this.SelectedMenuItems);
  }

  private void DocControl_SelectionChanged(object sender, SelectionChanged_EventArgs e)
  {
    try
    {
      if (this.viewMode != AVSViewMode.Page)
        return;
      this.UpdateNavigatorMenu(false);
      this.UpdateISimpleSelectedItemsService();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Выделить все записи</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  public List<DocumentTreeNode> SelectAll(DocumentTreeNode node)
  {
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    if (AVSDocument.IsSpecRowDocNode(node) || AVSDocument.IsNoteRowDocNode(node))
      documentTreeNodeList.Add(node);
    if (node.NodesCount > 0)
    {
      foreach (DocumentTreeNode node1 in node.Nodes)
        documentTreeNodeList.AddRange((IEnumerable<DocumentTreeNode>) this.SelectAll(node1));
    }
    return documentTreeNodeList;
  }

  public List<long> GetSelectedIds()
  {
    List<long> selectedIds = new List<long>();
    if (this.ReadOnly && !this.AVSDocument.DataLoaded)
    {
      List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
      if (this.DocumentControl != null)
        documentTreeNodeList = this.DocumentControl.SelectedNodes;
      if (documentTreeNodeList != null && documentTreeNodeList.Count > 0)
      {
        foreach (DocumentTreeNode docNode in documentTreeNodeList)
        {
          long id = -1;
          INodeWithReference parentSpecRowDocNode = AVSDocument.FindParentSpecRowDocNode(docNode) as INodeWithReference;
          if (parentSpecRowDocNode != null && parentSpecRowDocNode.Reference is ReferenceToDBObject reference1)
          {
            if (reference1.DBObjectID == -1L)
              reference1.UpdateDBObjectInfo();
            id = reference1.DBObjectID;
          }
          if (!id.IsUndefinedId())
          {
            selectedIds.Add(id);
          }
          else
          {
            DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(docNode);
            if (productVariableDocNode != null && productVariableDocNode is INodeWithReference && (productVariableDocNode as INodeWithReference).Reference is ReferenceToDBObject reference2)
            {
              long dbObjectId = reference2.DBObjectID;
              if (!dbObjectId.IsUndefinedId())
                selectedIds.Add(dbObjectId);
            }
          }
        }
      }
    }
    else
    {
      List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
      this.GetSelectedProducts();
      if (selectedSpecRows.Count > 0)
      {
        foreach (AVSRow avsRow in selectedSpecRows)
        {
          if (!selectedIds.Contains(avsRow.ObjectId))
            selectedIds.Add(avsRow.ObjectId);
        }
      }
      else
        selectedIds.AddRange((IEnumerable<long>) this.GetSelectedProducts(true));
    }
    return selectedIds;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ISelectedItems SelectedMenuItems
  {
    get
    {
      ISelectedItems selectedMenuItems = this.navigatorMenuItems;
      if ((selectedMenuItems == null || selectedMenuItems.Count == 0) && this.AVSDocument != null)
        selectedMenuItems = ObjectExtensions.GetItems(this.AVSDocument.ProductId);
      return selectedMenuItems;
    }
  }

  /// <summary>Обновляем меню команд навигатора </summary>
  /// <param name="ignoreContext">Игнорируем совпадение контекстов</param>
  private void UpdateNavigatorMenu(bool ignoreContext)
  {
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("AVS.NavigatorCommands");
    contextMenuItem.Items.Clear();
    if (contextMenuItem.Parent == null)
      this.DocumentControl.PageControl.ContextMenuBarItem.Items.Add((ToolbarItemBase) contextMenuItem);
    List<long> longList = new List<long>();
    List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();
    if (this.ReadOnly && !this.AVSDocument.DataLoaded)
    {
      nodes = AVSSelectedItemsHelper.GetSelectedNodes(this, false, false);
    }
    else
    {
      longList = AVSSelectedItemsHelper.GetSelectedIds(this);
      if (longList.Count == 0)
        nodes = AVSSelectedItemsHelper.GetSelectedNodes(this, false, false);
    }
    if (longList.Count > 0 || nodes.Count > 0)
    {
      this.navigatorMenuItems = (ISelectedItems) null;
      if (this._navigatorViewServices == null)
      {
        this._navigatorViewServices = new ServiceContainer();
        this._navigatorViewServices.AddService(typeof (IViewState), (object) new ViewStateService());
        this._navigatorViewServices.AddService(typeof (IAVSViewsService), (object) new AVSViewsService(this));
      }
      List<AVSRow> list = this.GetSelectedSpecRows(false).Where<AVSRow>((System.Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow)).ToList<AVSRow>();
      List<RelationAttributeValuesCache> relationIds = new List<RelationAttributeValuesCache>();
      if (list.Count > 0)
      {
        if (list.Count == 1)
        {
          List<long> selectedProductsB = this.GetSelectedProductsB();
          if (selectedProductsB.Count > 0)
          {
            int relationIndexForProduct = list[0].GetRelationIndexForProduct(selectedProductsB[0]);
            if (relationIndexForProduct >= 0)
            {
              RelationAttributeValuesCache relation = list[0].Relations[relationIndexForProduct];
              if (relation != null)
                relationIds.Add(relation);
            }
          }
        }
        this.navigatorMenuItems = AVSSelectedItemsHelper.GetSelectedItems(list, (System.IServiceProvider) this._navigatorViewServices, relationIds);
      }
      if (this.navigatorMenuItems == null)
        this.navigatorMenuItems = longList.Count <= 0 ? AVSSelectedItemsHelper.GetSelectedItems(this, nodes, (System.IServiceProvider) this._navigatorViewServices, true) : ObjectExtensions.GetItems(longList.ToArray());
      if (this.navigatorMenuItems == null || this.navigatorMenuItems.Count <= 0)
        return;
      MenuBarItem menu = Intermech.Navigator.ContextMenu.Services.GetMenu(this.navigatorMenuItems, (System.IServiceProvider) this._navigatorViewServices);
      if (menu != null)
      {
        if (menu.Items.Count > 0)
        {
          for (int index = menu.Items.Count - 1; index >= 0; --index)
          {
            if (menu.Items[index].CommandName == "Delete" || menu.Items[index].CommandName == "Copy" || menu.Items[index].CommandName == "Cut")
              menu.Items.RemoveAt(index);
          }
        }
        this.LoadNavigatorMenuItemImages(menu);
      }
      if (this.navigatorMenuItemsHelpers == null)
        this.navigatorMenuItemsHelpers = new Dictionary<MenuButtonItem, object>();
      this.navigatorMenuItemsHelpers.Clear();
      if (menu == null || !menu.HasChildren)
        return;
      ArrayList arrayList = new ArrayList(menu.Items.Count);
      foreach (MenuButtonItem key in (CollectionBase) menu.Items)
      {
        key.ShortcutActive = true;
        arrayList.Add((object) key);
        if (!this.navigatorMenuItemsHelpers.ContainsKey(key))
          this.navigatorMenuItemsHelpers.Add(key, key.Tag);
      }
      foreach (MenuButtonItem menuButtonItem in arrayList)
      {
        menuButtonItem.ShortcutActive = true;
        menuButtonItem.Click += new EventHandler(this.menuButtonItem_Click);
        contextMenuItem.Items.Add((ToolbarItemBase) menuButtonItem);
      }
    }
    else
      this.navigatorMenuItems = (ISelectedItems) null;
  }

  private void menuButtonItem_Click(object sender, EventArgs e)
  {
    try
    {
      MenuButtonItem key = sender as MenuButtonItem;
      if (!this.navigatorMenuItemsHelpers.ContainsKey(key))
        return;
      key.Tag = this.navigatorMenuItemsHelpers[key];
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void mi_AfterPopup(object sender, EventArgs e)
  {
    try
    {
      if (sender == null || !(sender is MenuButtonItem))
        return;
      MenuButtonItem menuButtonItem = (MenuButtonItem) sender;
      menuButtonItem.BeforePopup -= new MenuItemBase.BeforePopupEventHandler(this.mi_BeforePopup);
      menuButtonItem.AfterPopup -= new EventHandler(this.mi_AfterPopup);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void mi_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    try
    {
      if (sender == null || !(sender is MenuButtonItem) || this.DocumentControl == null || AVSPlugin.Instance.ActiveAVSWindow != this)
        return;
      this.UpdateNavigatorMenu(true);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void LoadNavigatorMenuItemImages(MenuBarItem contextMenu)
  {
    INamedImageList iNamedImageList = AVSPlugin.ServiceProvider == null ? (INamedImageList) ServicesManager.GetService(typeof (INamedImageList)) : (INamedImageList) AVSPlugin.ServiceProvider.GetService(typeof (INamedImageList));
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) contextMenu.Items)
      this.LoadNavigatorMenuItemImages(menuButtonItem, iNamedImageList);
  }

  private void LoadNavigatorMenuItemImages(
    MenuButtonItem menuButtonItem,
    INamedImageList iNamedImageList)
  {
    if (menuButtonItem.ImageIndex != -1 && menuButtonItem.Image == null && menuButtonItem.Icon == null)
      menuButtonItem.Image = iNamedImageList.ImageList.Images[menuButtonItem.ImageIndex];
    if (!menuButtonItem.HasChildren)
      return;
    foreach (MenuButtonItem menuButtonItem1 in (CollectionBase) menuButtonItem.Items)
      this.LoadNavigatorMenuItemImages(menuButtonItem1, iNamedImageList);
  }

  /// <summary>Обработчик пункта меню "Удалить"</summary>
  /// <param name="context">Контекст удаления</param>
  /// <param name="canDeleteFromEditor">Надо ли проверять возможность удаления из активного редактора</param>
  /// <param name="cut">Удаление для команды Cut</param>
  /// <returns>список id версий удаленных объектов</returns>
  protected void DeleteCommand(
    DocumentTreeNode[] context,
    bool canDeleteFromEditor,
    bool cut,
    bool notifyRelationsRemoved = false)
  {
    IDictionary<long, RelInfo> removedItemIds = (IDictionary<long, RelInfo>) new Dictionary<long, RelInfo>();
    if (context != null && context.Length != 0)
    {
      bool flag1 = false;
      try
      {
        if (context.Length == 1)
        {
          if (this.ViewMode == AVSViewMode.Page & canDeleteFromEditor && context[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
          {
            placeEditorControl.TerCommand(606);
            return;
          }
          if (this.ViewMode == AVSViewMode.Grid && this.virtualTree.TextEditor != null)
          {
            TextBox textEditor = this.virtualTree.TextEditor;
            if (textEditor.SelectionLength != 0)
            {
              textEditor.SelectedText = "";
              return;
            }
          }
        }
        context = DocumentTreeNode.GetNodesWithoutChilds(context);
        if (context.Length == 1 && context[0].IsVirtualNode)
        {
          DocumentTreeNode[] documentTreeNodeArray = new DocumentTreeNode[context[0].NodesCount];
          for (int index = 0; index < context[0].Nodes.Count; ++index)
            documentTreeNodeArray[index] = context[0].Nodes[index];
          context = documentTreeNodeArray;
        }
        bool flag2 = true;
        List<AVSRow> avsRowList1 = new List<AVSRow>();
        List<DocumentTreeNode> docRows = new List<DocumentTreeNode>();
        List<SpecificationSection> sections = new List<SpecificationSection>();
        List<Chapter> сhapters = new List<Chapter>();
        DialogResult dialogResult1 = DialogResult.Yes;
        DialogResult dialogResult2 = DialogResult.Yes;
        DialogResult dialogResult3 = DialogResult.Yes;
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < context.Length; ++index)
        {
          if ((AVSDocument.IsSpecRowDocNodeChild(context[index]) ? 0 : (!AVSDocument.IsNoteRowDocNodeChild(context[index]) ? 1 : 0)) != 0)
          {
            if (AVSDocument.IsSpecSectionDocNodeChild(context[index]))
              ++num1;
            else if (this.AVSDocument.GetChapter(context[index], true) != null)
              ++num2;
          }
        }
        using (new SessionKeeper())
        {
          for (int index1 = 0; index1 < context.Length; ++index1)
          {
            bool flag3 = AVSDocument.IsSpecSectionDocNode(context[index1]);
            bool flag4 = AVSDocument.IsChapterDocNode(context[index1], true);
            avsRowList1.Clear();
            docRows.Clear();
            sections.Clear();
            сhapters.Clear();
            if (!flag3 && !flag4)
            {
              this.avsDocument.GetAVSRowsAndDocRows(context[index1], avsRowList1, docRows);
              if (avsRowList1.Count == 0 && docRows.Count == 0)
              {
                this.avsDocument.GetChapters(context[index1], сhapters, true);
                if (сhapters.Count > 0 && !(сhapters[0] is AVSRowGroup))
                  сhapters.Clear();
                if (сhapters.Count == 0)
                {
                  this.avsDocument.GetSections(context[index1], sections);
                  for (int index2 = sections.Count - 1; index2 >= 0; --index2)
                  {
                    if (sections[index2].UseParentDocNode)
                      sections.RemoveAt(index2);
                  }
                }
                if (sections.Count == 0 && сhapters.Count == 0)
                  this.avsDocument.GetChapters(context[index1], сhapters, true);
              }
            }
            for (int index3 = 0; dialogResult1 == DialogResult.Yes && index3 < docRows.Count; ++index3)
            {
              if (docRows[index3] is TableData tableData)
              {
                if (flag2)
                {
                  flag2 = false;
                  dialogResult1 = MessageBox.Show("Вы хотите удалить записи из документа?", "Удаление", MessageBoxButtons.YesNo);
                  if (dialogResult1 != DialogResult.Yes)
                    break;
                }
                if (!flag1)
                {
                  this.avsDocument.SuspendDocumentAndGridUpdates();
                  flag1 = true;
                }
                tableData.UniteTable();
                tableData.Remove(false, false);
              }
            }
            for (int index4 = 0; dialogResult1 == DialogResult.Yes && index4 < avsRowList1.Count; ++index4)
            {
              if (avsRowList1[index4].Section != null)
              {
                if (flag2)
                {
                  flag2 = false;
                  dialogResult1 = MessageBox.Show("Вы хотите удалить записи из документа?", "Удаление", MessageBoxButtons.YesNo);
                  if (dialogResult1 != DialogResult.Yes)
                    break;
                }
                if (!flag1)
                {
                  this.avsDocument.SuspendDocumentAndGridUpdates();
                  flag1 = true;
                }
                foreach (KeyValuePair<long, RelInfo> keyValuePair in avsRowList1[index4].Section.RemoveRow(avsRowList1[index4], true, this.IsSpecification, true, this.viewMode == AVSViewMode.Grid, !cut))
                {
                  if (!removedItemIds.ContainsKey(keyValuePair.Key))
                    removedItemIds.Add(keyValuePair);
                }
              }
            }
            if (flag3)
              sections.Add(this.avsDocument.GetSection(context[index1]));
            for (int index5 = 0; dialogResult2 != DialogResult.Cancel && index5 < sections.Count; ++index5)
            {
              if (sections[index5] != null && sections[index5].Parent != null && !sections[index5].UseParentDocNode)
              {
                if (!sections[index5].IsEmpty)
                {
                  dialogResult2 = MessageBox.Show($"Вы действительно хотите удалить раздел \"{sections[index5].Caption}\" вместе со всеми записями?", "Удаление", num1 > 1 ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo);
                  switch (dialogResult2)
                  {
                    case DialogResult.Cancel:
                      goto label_74;
                    case DialogResult.No:
                      continue;
                  }
                }
                if (!flag1)
                {
                  this.avsDocument.SuspendDocumentAndGridUpdates();
                  flag1 = true;
                }
                foreach (KeyValuePair<long, RelInfo> keyValuePair in sections[index5].Parent.RemoveChapter((Chapter) sections[index5], true, !cut, true, this.viewMode == AVSViewMode.Grid))
                {
                  if (!removedItemIds.ContainsKey(keyValuePair.Key))
                    removedItemIds.Add(keyValuePair);
                }
              }
            }
label_74:
            if (flag4)
              сhapters.Add(this.avsDocument.GetChapter(context[index1], true));
            for (int index6 = 0; dialogResult3 != DialogResult.Cancel && index6 < сhapters.Count; ++index6)
            {
              if (сhapters[index6].IsAdditionalChapter)
              {
                dialogResult2 = MessageBox.Show($"Вы действительно хотите удалить часть \"{сhapters[index6].Caption}\" вместе со всеми записями?", "Удаление", num2 > 1 ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo);
                switch (dialogResult2)
                {
                  case DialogResult.Cancel:
                    goto label_144;
                  case DialogResult.No:
                    continue;
                  default:
                    if (!flag1)
                    {
                      this.avsDocument.SuspendDocumentAndGridUpdates();
                      flag1 = true;
                    }
                    avsRowList1 = new List<AVSRow>();
                    сhapters[index6].GetAllRowsList(false, false, avsRowList1);
                    if (avsRowList1.Count > 0)
                    {
                      for (int index7 = 0; index7 < avsRowList1.Count; ++index7)
                      {
                        foreach (KeyValuePair<long, RelInfo> keyValuePair in avsRowList1[index7].Section.RemoveRow(avsRowList1[index7], true, true, false, false, false))
                        {
                          if (!removedItemIds.ContainsKey(keyValuePair.Key))
                            removedItemIds.Add(keyValuePair);
                        }
                      }
                    }
                    if (сhapters[index6].Parent != null)
                    {
                      using (List<KeyValuePair<long, RelInfo>>.Enumerator enumerator = сhapters[index6].Parent.RemoveChapter(сhapters[index6], false, false, true, this.viewMode == AVSViewMode.Grid).GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          KeyValuePair<long, RelInfo> current = enumerator.Current;
                          if (current.Key != 1L && !removedItemIds.ContainsKey(current.Key))
                            removedItemIds.Add(current);
                        }
                        continue;
                      }
                    }
                    for (int index8 = 0; index8 < this.avsDocument.rootChapters.Count; ++index8)
                    {
                      if (this.avsDocument.rootChapters[index8] == сhapters[index6])
                      {
                        for (int index9 = 0; index9 < сhapters[index6].DocNodes.Count; ++index9)
                        {
                          сhapters[index6].DocNodes[index9].UniteTable();
                          сhapters[index6].DocNodes[index9].Remove(true, true);
                        }
                        if (this.viewMode == AVSViewMode.Grid && сhapters[index6].ListNode != null && сhapters[index6].ListNode.TreeList != null)
                          сhapters[index6].ListNode.TreeList.DeleteNode(сhapters[index6].ListNode);
                        this.avsDocument.rootChapters.RemoveAt(index8);
                        break;
                      }
                    }
                    continue;
                }
              }
              else if (!сhapters[index6].IsVariableDataChapter)
              {
                if (сhapters[index6] is ProductVariableDataChapter)
                {
                  if (this.avsDocument.IsSpecification && this.avsDocument.productsInfo.Count > 0)
                  {
                    using (SessionKeeper sessionKeeper = new SessionKeeper())
                    {
                      if (PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, this.avsDocument.productsInfo[0].Id))
                      {
                        int num3 = (int) MessageBox.Show("Невозможно добавить исполнение, так как это изделие создано на основе электронной модели и его изменение должно проводиться через эту модель.", "Удаление исполнения");
                        dialogResult3 = DialogResult.Cancel;
                        continue;
                      }
                    }
                  }
                  if (this.avsDocument.productsInfo.Count < 2)
                  {
                    int num4 = (int) MessageBox.Show("Нельзя удалить единственное исполнение!", "Удаление");
                  }
                  else
                  {
                    dialogResult3 = MessageBox.Show($"Вы действительно хотите удалить исполнение \"{((сhapters[index6] as ProductVariableDataChapter).Product != null ? (object) (сhapters[index6] as ProductVariableDataChapter).Product.Designation : (object) сhapters[index6].Caption)}\"?", "Удаление", MessageBoxButtons.YesNoCancel);
                    switch (dialogResult3)
                    {
                      case DialogResult.Cancel:
                        goto label_144;
                      case DialogResult.No:
                        continue;
                      default:
                        if (!flag1)
                        {
                          this.avsDocument.SuspendDocumentAndGridUpdates();
                          flag1 = true;
                        }
                        using (List<KeyValuePair<long, RelInfo>>.Enumerator enumerator = this.avsDocument.RemoveProductVersion(сhapters[index6].Product, true, this.viewMode == AVSViewMode.Grid).GetEnumerator())
                        {
                          while (enumerator.MoveNext())
                          {
                            KeyValuePair<long, RelInfo> current = enumerator.Current;
                            if (!current.Key.IsUndefinedId() && !removedItemIds.ContainsKey(current.Key))
                              removedItemIds.Add(current);
                          }
                          continue;
                        }
                    }
                  }
                }
                else if (сhapters[index6] is AVSRowGroup avsRowGroup && avsRowGroup is AVSAdditionalComplectRowGroup)
                {
                  dialogResult3 = MessageBox.Show(string.Format("Вы действительно хотите удалить все комплекты поставляемые отдельно?"), "Удаление", MessageBoxButtons.YesNoCancel);
                  if (dialogResult3 == DialogResult.Yes)
                  {
                    List<AVSRow> avsRowList2 = new List<AVSRow>();
                    avsRowList1 = new List<AVSRow>(avsRowGroup.GetRows(false, false));
                    if (avsRowList1.Count > 0)
                    {
                      for (int index10 = 0; index10 < avsRowList1.Count; ++index10)
                      {
                        foreach (KeyValuePair<long, RelInfo> keyValuePair in avsRowList1[index10].Section.RemoveRow(avsRowList1[index10], true, true, true, true, false))
                        {
                          if (!removedItemIds.ContainsKey(keyValuePair.Key))
                            removedItemIds.Add(keyValuePair);
                        }
                      }
                    }
                  }
                }
              }
            }
            continue;
label_144:;
          }
        }
      }
      finally
      {
        this.avsDocument.UpdateVariableDataCaptions();
        if (flag1)
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        if (this.DocumentControl != null)
          this.DocumentControl.UnselectRemovedNodes();
      }
    }
    if (!notifyRelationsRemoved)
      return;
    this.NotifyRelationsRemoved(removedItemIds);
  }

  /// <summary>Обработчик пункта меню "Удалить"</summary>
  /// <param name="specRows">Записи, объекты которых нужно удалить</param>
  protected void DeleteObjects(List<AVSRow> specRows)
  {
    if (specRows == null || specRows.Count <= 0)
      return;
    List<long> longList = new List<long>(specRows.Count);
    for (int index = 0; index < specRows.Count; ++index)
    {
      if (!longList.Contains(specRows[index].ObjectId))
        longList.Add(specRows[index].ObjectId);
    }
    DeletingObjects deletingObjects = new DeletingObjects();
    for (int index1 = 0; index1 < longList.Count; ++index1)
    {
      DeletingObject deletingObject = deletingObjects.Add(0L, 0L, longList[index1], true);
      List<AVSRow> avsRowsByObjectId = this.avsDocument.GetAvsRowsByObjectId(longList[index1]);
      for (int index2 = 0; index2 < avsRowsByObjectId.Count; ++index2)
      {
        if (avsRowsByObjectId[index2].Relations != null)
        {
          for (int index3 = 0; index3 < avsRowsByObjectId[index2].Relations.Count; ++index3)
          {
            if (!deletingObject.PrjLinkIDs.Contains(avsRowsByObjectId[index2].Relations[index3].RelationId))
              deletingObject.PrjLinkIDs.Add(avsRowsByObjectId[index2].Relations[index3].RelationId);
          }
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService customService)
        deletingObjects = customService.LoadDescriptions(sessionKeeper.Session.SessionGUID, deletingObjects);
    }
    AdvancedServiceContainer services = new AdvancedServiceContainer();
    services.AddService(typeof (IViewState), (object) new ViewStateService());
    DeleteAnalyzerJobStatus analyzerJobStatus;
    while (true)
    {
      DialogResult dialogResult = DeleteObjectsForm.Execute((System.IServiceProvider) services, deletingObjects, ref ObjectCommands.DeleteOptions);
      switch (dialogResult)
      {
        case DialogResult.Yes:
        case DialogResult.No:
          if (dialogResult == DialogResult.No)
          {
            for (int index = 0; index < deletingObjects.Count; ++index)
              deletingObjects[index].Items.Clear();
            analyzerJobStatus = DeleteAnalyzerForm.Execute(deletingObjects, ObjectCommands.DeleteOptions);
            if (analyzerJobStatus != null && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Cancelled && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Working)
            {
              if (analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Error)
              {
                deletingObjects = analyzerJobStatus.Items;
                continue;
              }
              goto label_34;
            }
            goto label_26;
          }
          goto label_38;
        default:
          goto label_53;
      }
    }
label_53:
    return;
label_26:
    return;
label_34:
    if (analyzerJobStatus.Exception == null)
      return;
    ExceptionHelper.ExceptionService.ShowException(analyzerJobStatus.Exception);
    return;
label_38:
    ObjectCommands.DeleteOptions &= ~DeleteAnalyzerOptions.FindAllVersions;
    List<long> objectIDs = (List<long>) null;
    List<long> relationIDs = (List<long>) null;
    List<long> projIDs = (List<long>) null;
    List<int> relTypeIDs = (List<int>) null;
    try
    {
      DeleteObjectsJobStatus objectsJobStatus = DeleteProgressForm.Execute(deletingObjects);
      objectIDs = objectsJobStatus?.Items;
      relationIDs = objectsJobStatus?.Relations;
      projIDs = objectsJobStatus?.RelationsProjIDs;
      relTypeIDs = objectsJobStatus?.RelationsTypeIDs;
      if (objectsJobStatus == null || objectsJobStatus.Progress == DeleteObjectsJobProgress.Cancelled || objectsJobStatus.Progress == DeleteObjectsJobProgress.Working)
        return;
      if (objectsJobStatus.Progress == DeleteObjectsJobProgress.Error)
      {
        if (objectsJobStatus.Exception == null)
          return;
        ExceptionHelper.ExceptionService.ShowException(objectsJobStatus.Exception);
      }
      else
      {
        this.avsDocument.SuspendDocumentAndGridUpdates();
        try
        {
          for (int index4 = 0; index4 < objectIDs.Count; ++index4)
          {
            List<AVSRow> avsRowsByObjectId = this.avsDocument.GetAvsRowsByObjectId(objectIDs[index4]);
            if (avsRowsByObjectId.Count > 0)
            {
              for (int index5 = 0; index5 < avsRowsByObjectId.Count; ++index5)
              {
                if (avsRowsByObjectId[index5].Section != null)
                  avsRowsByObjectId[index5].Section.RemoveRow(avsRowsByObjectId[index5], true, false, true, this.ViewMode == AVSViewMode.Grid, false);
              }
            }
          }
        }
        finally
        {
          this.avsDocument.UpdateVariableDataCaptions();
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
          if (this.DocumentControl != null)
            this.DocumentControl.UnselectRemovedNodes();
        }
      }
    }
    finally
    {
      INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      if (service != null)
      {
        if (relationIDs != null && relationIDs.Count > 0)
        {
          DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
          service.FireEvent((object) this.avsDocument, (NotificationEventArgs) e);
        }
        if (objectIDs != null && objectIDs.Count > 0)
        {
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs);
          service.FireEvent((object) this.avsDocument, (NotificationEventArgs) e);
        }
      }
    }
  }

  private bool CheckEmptyObjectNameAndDesignation(
    IDBObject dbObject,
    IUserSession session,
    bool showMessage)
  {
    string str1 = (string) null;
    string str2 = (string) null;
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(AvsIDCache.Attr_Name);
    if (attributeById1 != null)
      str1 = Convert.ToString(attributeById1.Values[0]);
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(AvsIDCache.Attr_Designation);
    if (attributeById2 != null)
      str2 = Convert.ToString(attributeById2.Values[0]);
    if (str1 != null && !(str1 == "") || str2 != null && !(str2 == ""))
      return true;
    if (showMessage)
    {
      int num = (int) MessageBox.Show("У добавляемого объекта не заданы значения атрибутов \"Обозначение\" и \"Наименование\"!", "Ошибка!");
    }
    return false;
  }

  /// <summary>Допустима ли вставка Nodoв при Drag Drop</summary>
  /// <param name="contextNode">Текущий элемент документа</param>
  /// <param name="items">Выбранные элементы БД</param>
  /// <returns></returns>
  public bool CanAddNodes(DocumentTreeNode contextNode, ISelectedItems items)
  {
    List<int> availableTypes = this.GetAvailableTypes(contextNode);
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < availableTypes.Count; ++index2)
      {
        if (items.GetItemID(index1).TypeID == availableTypes[index2] || MetaDataHelper.IsObjectTypeChildOf(items.GetItemID(index1).TypeID, availableTypes[index2]))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return false;
    }
    return true;
  }

  /// <summary>Получить допустимые типы из разделов спецификации</summary>
  /// <param name="contextNode">Текущий элемент документа</param>
  /// <returns></returns>
  public List<int> GetAvailableTypes(DocumentTreeNode contextNode)
  {
    List<int> partTypes = new List<int>();
    AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode);
    SpecificationSectionInfo specSection = (SpecificationSectionInfo) null;
    if (this.avsDocument.IsSpecification)
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      if (contextChapters.Section != null)
        specSection = contextChapters.Section.SectionInfo;
      if (specSection != null)
      {
        AVSDocument.GetPartTypes(specSection, partTypes);
      }
      else
      {
        List<SpecificationSectionInfo> documentSections = this.AVSDocument.GetAllowableDocumentSections();
        for (int index = 0; index < documentSections.Count; ++index)
          AVSDocument.GetPartTypes(documentSections[index], partTypes);
      }
    }
    else if (this.AVSDocument.IsElementList)
    {
      partTypes.Add(AvsIDCache.ObjType_Product);
    }
    else
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this.AVSDocument.DocumentDBObjectType);
      if (objectType != null && objectType.ObjectTypeName.Contains("Таблица соединений"))
      {
        partTypes.Add(AvsIDCache.ObjType_OtherProduct);
        partTypes.Add(AvsIDCache.ObjType_AssemblyUnit);
        partTypes.Add(AvsIDCache.ObjType_StandartProduct);
      }
      else
        partTypes.Add(AvsIDCache.ObjType_Product);
    }
    List<int> intList = new List<int>();
    foreach (int num in partTypes)
    {
      if ((num == AvsIDCache.ObjType_Complex || num == AvsIDCache.ObjType_AssemblyUnit) && !intList.Contains(AvsIDCache.ObjType_Specification))
        intList.Add(AvsIDCache.ObjType_Specification);
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      if (!partTypes.Contains(intList[index]))
        partTypes.Add(intList[index]);
    }
    return partTypes;
  }

  /// <summary>Добавить строку спецификации согласно контексту</summary>
  /// <param name="contextNode">Контекст</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="selected">Выбранные объекты</param>
  /// <returns></returns>
  public List<AVSRow> ContextAddSpecRow(
    DocumentTreeNode contextNode,
    int relationType,
    object[] selected)
  {
    AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode, this.avsDocument.AvsDocumentForm != AVSDocumentForm.V);
    List<int> typeList = new List<int>();
    if (relationType == AvsIDCache.Relation_AddComplect)
      typeList = MetaDataHelper.GetApplicabilityChildObjectTypesID(this.AVSDocument.ProductType, relationType);
    ArrayList arrayList = this.SelectObjectFromDB(contextNode, selected, typeList, contextChapters, true);
    if (arrayList == null || arrayList.Count == 0)
      return (List<AVSRow>) null;
    if ((contextChapters.Products.Count == 0 ? 1 : (!this.avsDocument.IsFormA || contextChapters.Product == null ? 0 : (contextChapters.Product.IsVariableData ? 1 : 0))) != 0 && this.avsDocument.IsFormA)
    {
      TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
      ProductInfo product = contextChapters.Product;
      if ((product != null ? (product.IsVariableData ? 1 : 0) : 0) != 0)
        selectCharterForm.UncheckAll();
      if (selectCharterForm.ShowDialog() != DialogResult.OK)
        return new List<AVSRow>();
      contextChapters.Products = selectCharterForm.GetSelectedProducts();
    }
    if (contextChapters.Products.Count == 0)
      contextChapters.Products = new List<ProductInfo>((IEnumerable<ProductInfo>) this.AVSDocument.productsInfo);
    contextChapters.DefaultRelationType = relationType;
    return this.avsDocument.AddAvsRowParts(arrayList.ToArray(), relationType, contextChapters, false, true);
  }

  /// <summary>Добавить строку спецификации с заготовкой из ImBase</summary>
  /// <param name="contextNode">Контекст</param>
  /// <param name="useCurrentMaterial">Использовать материал текущего изделия</param>
  public void AddSpecRow_Zagotovka(DocumentTreeNode contextNode)
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
    if (selectedSpecRows.Count != 1)
      return;
    AVSRow sourceRow = selectedSpecRows[0];
    if (sourceRow == null || !sourceRow.HasObject || !(this.avsDocument is AVSSpecification avsDocument))
      return;
    string errorMessage;
    if (!avsDocument.CheckCanCreateDraftForPart(sourceRow, false, out long _, out QuickObjectInfo? _, out errorMessage).Item1)
    {
      int num = (int) MessageBox.Show(errorMessage, "Ошибка добавления заготовки");
    }
    else
    {
      List<SpecificationSectionInfo> sections = new List<SpecificationSectionInfo>();
      for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
      {
        if (SpecificationSectionInfo.Sections[index].SectionGuid != SpecificationSectionInfo.DocumentSectionGuid)
          sections.Add(SpecificationSectionInfo.Sections[index]);
      }
      ArrayList arrayList = avsDocument.SelectDBObjectsForSections((IList<SpecificationSectionInfo>) sections, false);
      if (arrayList == null || arrayList.Count == 0)
        return;
      avsDocument.GenerateRowForZagotovka((DocumentTreeNode) sourceRow.DocNode, sourceRow, newMaterialData: arrayList[0]);
    }
  }

  /// <summary>Добавить строку спецификации с заготовкой из ImBase</summary>
  /// <param name="contextNode">Контекст</param>
  public void AddSpecRow_ZagotovkaFromImBase(DocumentTreeNode contextNode)
  {
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(true);
    if (selectedSpecRows.Count != 1)
      return;
    AVSRow sourceRow = selectedSpecRows[0];
    if (sourceRow == null || !sourceRow.HasObject || !(this.avsDocument is AVSSpecification avsDocument))
      return;
    string errorMessage;
    if (!avsDocument.CheckCanCreateDraftForPart(sourceRow, false, out long _, out QuickObjectInfo? _, out errorMessage).Item1)
    {
      int num1 = (int) MessageBox.Show(errorMessage, "Ошибка добавления заготовки из ImBase");
    }
    else
    {
      ArrayList arrayList = new ArrayList();
      long newMaterialData = -1;
      if (AVSPlugin.ImbaseSelector != null)
      {
        List<SpecificationSectionInfo> sections = new List<SpecificationSectionInfo>();
        if (SpecificationSectionInfo.SectionDictionaryByGuid.Contains((object) SpecificationSectionInfo.DetailSectionGuid))
          sections.Add((SpecificationSectionInfo) SpecificationSectionInfo.SectionDictionaryByGuid[(object) SpecificationSectionInfo.DetailSectionGuid]);
        if (SpecificationSectionInfo.SectionDictionaryByGuid.Contains((object) SpecificationSectionInfo.OtherDetailSectionGuid))
          sections.Add((SpecificationSectionInfo) SpecificationSectionInfo.SectionDictionaryByGuid[(object) SpecificationSectionInfo.OtherDetailSectionGuid]);
        if (SpecificationSectionInfo.SectionDictionaryByGuid.Contains((object) SpecificationSectionInfo.StandartDetailSectionGuid))
          sections.Add((SpecificationSectionInfo) SpecificationSectionInfo.SectionDictionaryByGuid[(object) SpecificationSectionInfo.StandartDetailSectionGuid]);
        List<object> availableCatalogs = this.GetAvailableCatalogs((IList<SpecificationSectionInfo>) sections);
        if (availableCatalogs.Count == 0)
        {
          int num2 = (int) MessageBox.Show("Ни одному из разделов спецификации не назначены каталоги Imbase", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        newMaterialData = AVSPlugin.ImbaseSelector.SelectFromCatalog("Выберите объект", "Выберите изделие, которое надо добавить в спецификацию", (object) availableCatalogs, false, true, (int[]) null, -1, -1L);
      }
      if (newMaterialData == -1L || newMaterialData == 0L)
        return;
      avsDocument.GenerateRowForZagotovka((DocumentTreeNode) sourceRow.DocNode, sourceRow, newMaterialData: (object) newMaterialData);
    }
  }

  /// <summary>Выбор строк из БД для добавления в спецификацию</summary>
  /// <param name="contextNode">Контекст</param>
  /// <param name="selected">Выбранные объекты</param>
  /// <param name="typeList">Типы объектов из которых можно выбирать. Если null, то используются настройки раздела</param>
  /// <param name="context">Контекст выбора объектов</param>
  /// <param name="multiSelect">Использовать множественный выбор</param>
  /// <returns></returns>
  public ArrayList SelectObjectFromDB(
    DocumentTreeNode contextNode,
    object[] selected,
    List<int> typeList,
    AVSDocumentContext context,
    bool multiSelect)
  {
    if (this.avsDocument.IsSpecification)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    if (typeList == null)
      typeList = new List<int>();
    if (typeList.Count == 0)
      typeList = this.GetAvailableTypes(contextNode);
    List<int> intList1 = typeList;
    DescriptorCollection descriptors1 = new DescriptorCollection();
    List<int> intList2 = (List<int>) null;
    string caption1 = "Документы";
    string caption2 = "Допустимые типы объектов";
    if (typeList.Contains(AvsIDCache.ObjType_Document))
    {
      caption1 = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(AvsIDCache.ObjType_Document).ObjectTypeName;
      intList2 = this.avsDocument.GetApplicabilityTypes(AvsIDCache.ObjType_Document, (SpecificationSection) null);
    }
    DescriptorCollection descriptors2 = new DescriptorCollection();
    if (intList2 != null && intList2.Count > 0)
    {
      ObjectTypesDescriptor objectTypesDescriptor = new ObjectTypesDescriptor(intList2.ToArray(), "Допустимые типы документов");
      descriptors2.Add((IDescriptor) objectTypesDescriptor);
      Intermech.Navigator.CustomNode.Descriptor descriptor = new Intermech.Navigator.CustomNode.Descriptor(caption1, descriptors2);
      descriptors1.Add((IDescriptor) descriptor);
    }
    List<int> intList3 = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < intList1.Count; ++index)
      {
        if (intList2 == null || !intList2.Contains(intList1[index]))
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(intList1[index]);
          IDBSecurity dbSecurity = objectType as IDBSecurity;
          string areaId = objectType.PropertiesStructure.AreaID;
          string sessionSubjArea = sessionKeeper.Session.AreaID;
          if ((string.IsNullOrEmpty(areaId) || string.IsNullOrEmpty(sessionSubjArea) || !((IEnumerable<char>) areaId.ToCharArray()).All<char>((System.Func<char, bool>) (sa => sessionSubjArea.IndexOf(sa) == -1))) && (dbSecurity == null || dbSecurity.CheckAccess(ActionType.View, true, false)) && (intList1[index] != AvsIDCache.ObjType_Document || descriptors2.Count == 0))
            intList3.Add(intList1[index]);
        }
      }
    }
    if (intList3.Count > 0)
    {
      ObjectTypesDescriptor objectTypesDescriptor = new ObjectTypesDescriptor(intList3.ToArray(), caption2);
      descriptors1.Add((IDescriptor) objectTypesDescriptor);
    }
    DesktopObjectNode.GetDesktopID();
    descriptors1.Add((IDescriptor) new DesktopNodeDescriptor(DesktopObjectNode.DesktopObjectID));
    if (ServicesManager.GetService(typeof (IArchivesDescriptorService)) is IArchivesDescriptorService service)
      descriptors1.Add(service.GetDescriptor());
    IDescriptor rootDescriptor1 = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Варианты выбора объектов", descriptors1);
    if (selected == null)
    {
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer((intList3 ?? new List<int>()).Concat<int>((IEnumerable<int>) (intList2 ?? new List<int>())).Distinct<int>().OrderBy<int, int>((System.Func<int, int>) (i => i)).ToList<int>(), true), true);
      selected = Intermech.Navigator.SelectionWindow.Select("Выберите объект", rootDescriptor1, typeof (IDBTypedObjectID), (SelectionOptions) (8589938944L /*0x0200001100*/ | (!multiSelect ? 16777216L /*0x01000000*/ : 0L)));
    }
    for (int index = 0; index < descriptors1.Count; ++index)
    {
      if (descriptors1[index] is IDisposable disposable)
        disposable.Dispose();
    }
    if (selected == null)
      return (ArrayList) null;
    ArrayList arrayList = new ArrayList();
    int objectType1 = -1;
    int productType = this.avsDocument.ProductType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = selected.Length - 1; index1 > -1; --index1)
      {
        long objectIdNavigatorData = AVSDocument.GetObjectIDNavigatorData(sessionKeeper.Session, selected[index1], out long _, out objectType1);
        if (objectIdNavigatorData != -1L)
        {
          if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Document, objectType1))
          {
            bool flag1 = context.SectionID != AVSDocument.ObjID_SectionDocumentation;
            bool flag2 = !AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, objectType1);
            bool flag3 = AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, objectType1) || ((flag1 ? 1 : 0) & (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_AssemblyDrawing, objectType1) || AVSDocument.IsParentObjectType(DocumentTypeWeight.partDrawType, objectType1) || AVSDocument.IsParentObjectType(AvsIDCache.ObjType_DetailModels, objectType1) ? 1 : (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_AssemblyModels, objectType1) ? 1 : 0))) != 0;
            bool flag4 = flag3;
            if (!flag3)
            {
              IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(AvsIDCache.Relation_Document, objectType1, productType);
              if (applicability == null || applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
                flag3 = true;
            }
            if (flag3)
            {
              DataTable dataTable = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[6]
              {
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
              }), objectIdNavigatorData);
              if (flag4 && dataTable.Rows.Count == 1)
              {
                for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                {
                  long int64_1 = Convert.ToInt64(dataTable.Rows[index2][0]);
                  int int32 = Convert.ToInt32(dataTable.Rows[index2][1]);
                  string caption3 = Convert.ToString(dataTable.Rows[index2][2]);
                  long int64_2 = Convert.ToInt64(dataTable.Rows[index2][3]);
                  long int64_3 = Convert.ToInt64(dataTable.Rows[index2][4]);
                  long int64_4 = Convert.ToInt64(dataTable.Rows[index2][5]);
                  arrayList.Add((object) new DBTypedObjectID(int32, int64_1, int64_2, caption3, int64_3, 0L, 0L, string.Empty, int64_4));
                }
              }
              else if (dataTable.Rows.Count > 0)
              {
                string nameInMessages = sessionKeeper.Session.GetObject(objectIdNavigatorData).NameInMessages;
                List<long> objectIDs = new List<long>(dataTable.Rows.Count);
                foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                  objectIDs.Add(Convert.ToInt64(row[0]));
                ListDescriptor rootDescriptor2 = new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, nameInMessages, (IList) objectIDs);
                rootDescriptor2.GetRecordNodeID();
                object[] objArray = Intermech.Navigator.SelectionWindow.Select($"Выберите изделия взамен \"{nameInMessages}\"", (IDescriptor) rootDescriptor2, typeof (IDBTypedObjectID), (SelectionOptions) (8589938944L /*0x0200001100*/ | (!multiSelect ? 16777216L /*0x01000000*/ : 0L)));
                if (objArray != null)
                {
                  for (int index3 = 0; index3 < objArray.Length; ++index3)
                    arrayList.Add(objArray[index3]);
                }
              }
              else if (flag2)
                arrayList.Add(selected[index1]);
            }
            else
              arrayList.Add(selected[index1]);
          }
          else
            arrayList.Add(selected[index1]);
        }
      }
    }
    return arrayList.Count == 0 ? (ArrayList) null : arrayList;
  }

  /// <summary>Заменить строку</summary>
  /// <param name="contextNode">Выбранный элемент документа</param>
  /// <param name="fromImbase">Выбор элемента идет из Imbase или из БД</param>
  private void ContextReplaceRow(
    DocumentTreeNode contextNode,
    AVSWindow.ReplaceRowMode replaceRowMode)
  {
    AVSRow selectedSpecRow = this.GetSelectedSpecRow();
    if (selectedSpecRow == null || selectedSpecRow.IsNoteRow)
      return;
    List<int> typeList = new List<int>();
    typeList.Add(selectedSpecRow.ObjType);
    AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode, true);
    long num1 = -1;
    if (replaceRowMode == AVSWindow.ReplaceRowMode.ReplaceObject)
    {
      ArrayList arrayList = this.SelectObjectFromDB(contextNode, (object[]) null, typeList, contextChapters, false);
      if (arrayList != null && arrayList.Count == 1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          num1 = AVSDocument.GetObjectIDNavigatorData(sessionKeeper.Session, arrayList[0], out long _, out int _);
      }
    }
    if (replaceRowMode == AVSWindow.ReplaceRowMode.ReplaceObjectFromImbase)
      num1 = this.SelectObjectFromIMBase(contextNode);
    if (replaceRowMode == AVSWindow.ReplaceRowMode.ReplaceVersion)
    {
      long num2 = ObjectVersionSelection.SelectVersion(selectedSpecRow.Object_F_ID, true, new List<long>(), new long[1]);
      if (num2 != selectedSpecRow.ObjectId)
        num1 = num2;
    }
    if (num1.IsUndefinedId())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num1);
      if (objectInfo.VersionGuid != Guid.Empty)
      {
        this.AVSDocument.UnregisterSpecRowInDictionaries(selectedSpecRow);
        if (selectedSpecRow.HasAnyRelations)
        {
          foreach (RelationAttributeValuesCache allRelation in selectedSpecRow.AllRelations)
          {
            IDBRelation relationByPartObjectId = sessionKeeper.Session.GetRelationByPartObjectID(allRelation.RelationId, selectedSpecRow.ObjectId, true);
            if (relationByPartObjectId != null)
            {
              if (relationByPartObjectId.Attributes.FindByGUID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545")) == null)
                relationByPartObjectId.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) "cad001c2-306c-11d8-b4e9-00304f19f545"), false);
              relationByPartObjectId.ReplacePartObject(objectInfo.ObjectID);
            }
          }
        }
        if (selectedSpecRow.HasPartAsMaterial(out long _))
          selectedSpecRow.RemoveZagotovka();
        selectedSpecRow.ReplaceObjectID(objectInfo.ID, objectInfo.ObjectID, objectInfo.VersionGuid, objectInfo.ObjectTypeID, objectInfo.Caption);
        this.AVSDocument.RegisterAVSRowInDictionaries(selectedSpecRow);
      }
    }
    try
    {
      this.AVSDocument.SuspendDocumentAndGridUpdates();
      this.AVSDocument.ReloadObjectAttributesFromDB(num1, selectedSpecRow);
      selectedSpecRow.DocNode.Reference = (ReferenceBase) null;
    }
    finally
    {
      this.AVSDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
    this.AVSDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    this.UpdateProductPropertiesPanel();
  }

  /// <summary>Создать новую деталь и добавить строку спецификации согласно контексту</summary>
  /// <param name="contextNode">Контекст</param>
  private List<AVSRow> ContextAddNewSpecRow(DocumentTreeNode contextNode)
  {
    List<AVSRow> avsRowList1 = new List<AVSRow>();
    try
    {
      List<long> objectIDs1 = new List<long>();
      AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode, true);
      if (contextChapters.Products.Count == 0 && this.avsDocument.IsFormA)
      {
        TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
        if (selectCharterForm.ShowDialog() != DialogResult.OK)
          return (List<AVSRow>) null;
        contextChapters.Products = selectCharterForm.GetSelectedProducts();
      }
      if (contextChapters.Products.Count == 0)
        contextChapters.Products = new List<ProductInfo>((IEnumerable<ProductInfo>) this.AVSDocument.productsInfo);
      List<int> intList1 = new List<int>();
      List<int> filteredParentTypes = new List<int>();
      if (this.avsDocument.IsSpecification)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
        SpecificationSectionInfo sectionInfo = contextChapters.Section?.SectionInfo;
        if (sectionInfo != null)
        {
          if (contextChapters.SectionID != AVSDocument.ObjID_SectionDocumentation)
          {
            List<int> idListForSection = AVSDocument.GetTypeIdListForSection(sectionInfo);
            intList1.AddRange(idListForSection.Distinct<int>());
          }
          else
          {
            AVSDocument.GetPartTypes(sectionInfo, intList1);
            filteredParentTypes.Add(AvsIDCache.ObjType_Document);
          }
        }
        else
        {
          List<SpecificationSectionInfo> documentSections = this.AVSDocument.GetAllowableDocumentSections();
          for (int index = 0; index < documentSections.Count; ++index)
          {
            SpecificationSectionInfo specSection = documentSections[index];
            AVSDocument.GetPartTypes(specSection, intList1);
            if (specSection != null && specSection.SectionID == AVSDocument.ObjID_SectionDocumentation)
            {
              intList1.AddRange((IEnumerable<int>) this.avsDocument.GetApplicabilityTypes(AvsIDCache.ObjType_Document, (SpecificationSection) null));
              filteredParentTypes.Add(AvsIDCache.ObjType_Document);
            }
          }
        }
      }
      else if (this.AVSDocument.IsElementList)
      {
        intList1.Add(AvsIDCache.ObjType_Product);
      }
      else
      {
        intList1.Add(AvsIDCache.ObjType_Product);
        IMSObjectType objectType = MetaDataHelper.GetObjectType(this.AVSDocument.DocumentDBObjectType);
        if (objectType != null && objectType.ObjectTypeName.Contains("Таблица соединений"))
        {
          intList1.Add(AvsIDCache.ObjType_OtherProduct);
          intList1.Add(AvsIDCache.ObjType_AssemblyUnit);
          intList1.Add(AvsIDCache.ObjType_StandartProduct);
        }
        else
          intList1.Add(AvsIDCache.ObjType_Product);
      }
      List<int> intList2 = (List<int>) null;
      if (intList1.Count > 0)
        intList2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) intList1);
      if (intList2 != null)
      {
        for (int index = intList2.Count - 1; index >= 0; --index)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(intList2[index]);
          if (objectType.AreaID != "" && !objectType.AreaID.Contains("A") || objectType.VersionsMode == ObjectVersionModes.Abstract)
            intList2.RemoveAt(index);
        }
      }
      int num1 = intList2 == null || intList2.Count != 1 ? -1 : intList2[0];
      if (num1 == -1)
      {
        int num2 = this.avsDocument.IsSpecification ? 1 : 0;
        SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Допустимые типы объектов", typeof (ObjectTypeFolder), false);
        selectorForm.ExpandLevelsOnLoad = 2;
        selectorForm.SelectorFilter = (ISelectorFilter) new AVSSelectorFilter(filteredParentTypes, intList1.ToArray(), true, true);
        selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new AvsNodeSelectorFilter();
        if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
          return (List<AVSRow>) null;
        num1 = (int) selectorForm.IDList[0];
      }
      if (num1 != -1)
      {
        int num3 = -1;
        int num4;
        if (MetaDataHelper.IsObjectTypeChildOf(num1, AvsIDCache.ObjType_Document))
        {
          num4 = num1;
          if (contextChapters.Section == null || contextChapters.Section.ChapterID != AVSDocument.ObjID_SectionDocumentation)
          {
            if (MetaDataHelper.IsObjectTypeChildOf(num1, AvsIDCache.ObjType_DetailDrawing))
              num3 = AvsIDCache.ObjType_Detail;
            else if (!MetaDataHelper.IsObjectTypeChildOf(num1, AvsIDCache.ObjType_AssemblyDrawing) && !MetaDataHelper.IsObjectTypeChildOf(num1, AvsIDCache.ObjType_Specification))
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
                if (customService != null)
                {
                  DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, num4);
                  if (settings.OutputObjectTypes != null)
                  {
                    if (settings.OutputObjectTypes != "")
                    {
                      string[] strArray = settings.OutputObjectTypes.Split(',');
                      List<int> intList3 = new List<int>(strArray.Length);
                      for (int index1 = 0; index1 < strArray.Length; ++index1)
                      {
                        num3 = MetaDataHelper.GetObjectTypeID(new Guid(strArray[index1]));
                        if (intList2.Contains(num3) && !intList3.Contains(num3))
                        {
                          intList3.Add(num3);
                          List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(num3);
                          for (int index2 = 0; objectTypeChildrenId != null && index2 < objectTypeChildrenId.Count; ++index2)
                          {
                            if (MetaDataHelper.GetObjectType(intList2[index1]).VersionsMode != ObjectVersionModes.Abstract && !intList3.Contains(objectTypeChildrenId[index2]))
                              intList3.Add(objectTypeChildrenId[index2]);
                          }
                        }
                      }
                      if (intList3.Count > 1)
                      {
                        SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Допустимые типы изделий", typeof (ObjectTypeFolder), false);
                        selectorForm.Text = "Выберите тип изделия";
                        selectorForm.ExpandLevelsOnLoad = 2;
                        selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(intList3.ToArray(), true, true);
                        selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
                        if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
                          return (List<AVSRow>) null;
                        num3 = (int) selectorForm.IDList[0];
                      }
                    }
                  }
                }
                else if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_DetailDrawing, num1))
                  num3 = AvsIDCache.ObjType_Detail;
                else if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, num1))
                {
                  if (contextChapters.Section != null && contextChapters.Section.SectionID == AVSDocument.ObjID_SectionAssemblyUnits)
                    num3 = AvsIDCache.ObjType_AssemblyUnit;
                  else if (contextChapters.Section != null && contextChapters.Section.SectionID == AVSDocument.ObjID_SectionComplects)
                    num3 = AvsIDCache.ObjType_Complect;
                  else if (contextChapters.Section != null)
                  {
                    if (contextChapters.Section.SectionID == AVSDocument.ObjID_SectionComplex)
                      num3 = AvsIDCache.ObjType_Complex;
                  }
                }
              }
            }
          }
        }
        else
        {
          num3 = num1;
          num4 = -1;
        }
        if (this.avsDocument.IsSpecification && !num3.IsUndefinedTypeId() && !AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Document, num3))
        {
          FormType formType = FormType.Single;
          if (this.avsDocument.AVSDocType != AVSDocumentType.AutoIndustrySpecification)
          {
            if (MetaDataHelper.IsObjectTypeChildOf(num3, AvsIDCache.ObjType_DetailWithoutDrawing))
              formType = FormType.NonDraft;
            if (this.avsDocument.IsFormB || contextChapters.Chapter != null && contextChapters.Chapter.IsFormB)
              formType = formType != FormType.NonDraft ? FormType.GroupB : FormType.NonDraftB;
          }
          else
          {
            formType = FormType.Autoprom_Single;
            if (MetaDataHelper.IsObjectTypeChildOf(num3, AvsIDCache.ObjType_DetailWithoutDrawing))
              formType = FormType.Autoprom_NonDraft;
            if (this.avsDocument.IsFormB || contextChapters.Chapter != null && contextChapters.Chapter.IsFormB)
              formType = formType != FormType.Autoprom_NonDraft ? FormType.Autoprom_GroupB : FormType.Autoprom_NonDraftB;
          }
          this.Suspended_DBRelationsEventArgsFromForm = true;
          CreatedPair createdPair = (CreatedPair) null;
          try
          {
            List<ProductInfo> productInfoList = contextChapters.Products;
            if (contextChapters.Products == null || contextChapters.Products.Count == 0 || contextChapters.Products[0].IsCommonData || contextChapters.Products[0].IsVariableData || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
              productInfoList = this.AVSDocument.productsInfo;
            createdPair = Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm.CreateDialog(this, AVSDocument.GetProductIds(productInfoList), num3, num4, AvsIDCache.Relation_Project, formType);
            if (createdPair != null)
            {
              if (createdPair.DocumentID.IsDefinedId())
                objectIDs1.Add(createdPair.DocumentID);
              if (createdPair.ArticleID.IsDefinedId())
                objectIDs1.Add(createdPair.ArticleID);
            }
          }
          finally
          {
            this.Suspended_DBRelationsEventArgsFromForm = false;
          }
          if (createdPair != null && (createdPair.DocumentID.IsDefinedId() || createdPair.ArticleID.IsDefinedId()))
          {
            Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
            if (!createdPair.RelationIDs.IsEmpty<long>())
              AVSDocument.AddRelationToTypedDictionary(dictionary, createdPair.RelationType, (IEnumerable<long>) createdPair.RelationIDs);
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(createdPair.ArticleID);
              if (dbObject == null)
                return (List<AVSRow>) null;
              if (!this.CheckEmptyObjectNameAndDesignation(dbObject, sessionKeeper.Session, true))
              {
                dbObject.Delete(0L);
                return (List<AVSRow>) null;
              }
              List<AVSRow> avsRowList2 = !dictionary.Any<KeyValuePair<int, List<long>>>() ? new List<AVSRow>(0) : this.avsDocument.AddSpecificationRelations(dictionary, false, contextChapters);
              if (avsRowList2.IsEmpty<AVSRow>() && (this.avsDocument.AvsDocumentForm == AVSDocumentForm.B || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V))
              {
                List<long> objectIDs2 = new List<long>()
                {
                  createdPair.ArticleID
                };
                List<int> objectTypes = new List<int>()
                {
                  num3
                };
                RowDictionariesForLoadDocument rowDicts = new RowDictionariesForLoadDocument();
                avsRowList2 = this.avsDocument.LoadRowsForDBObjects(objectIDs2, objectTypes, (ColumnDescriptor[]) null, (ColumnDescriptor[]) null, true, contextChapters, true, sessionKeeper.Session, rowDicts, AvsConfig.General.AddToCurrentGroup);
              }
              if (avsRowList2.Any<AVSRow>())
              {
                this.avsDocument.SelectNewRows(avsRowList2);
                avsRowList1 = avsRowList2;
              }
              if (avsRowList2.Any<AVSRow>() && avsRowList2[0].RelType != AvsIDCache.Relation_Document)
              {
                this.avsDocument.SuspendDocumentAndGridUpdates(true, this.viewMode == AVSViewMode.Grid);
                try
                {
                  if (createdPair.Format != null && createdPair.Format != "")
                    avsRowList2[0].SetFieldValue(this.AVSDocument.Field_Format, -1, -1, (object) createdPair.Format, false, true, true, false, false, true, false);
                  if (!string.IsNullOrEmpty(createdPair.Smotri))
                    avsRowList2[0].TextLinkToMainDocument = createdPair.Smotri;
                  if (createdPair.Count != null)
                    avsRowList2[0].SetFieldValue(this.AVSDocument.Field_Count, -1, -1, (object) createdPair.Count, false, true, true, false, false, true, false);
                  if (!avsRowList2[0].HasRelation)
                  {
                    if (!string.IsNullOrEmpty(createdPair.Zona))
                      avsRowList2[0].SetFieldValue(this.AVSDocument.Field_Zone, -1, -1, (object) createdPair.Zona, false, false, true, true, false, true, false);
                    if (!string.IsNullOrEmpty(createdPair.Position))
                      avsRowList2[0].SetFieldValue(this.AVSDocument.Field_Position, -1, -1, (object) createdPair.Position, false, true, true, false, false, false);
                    if (!string.IsNullOrEmpty(createdPair.Note))
                      avsRowList2[0].SetFieldValue(this.AVSDocument.Field_Note, -1, -1, (object) createdPair.Note, false, true, true, false, false, true, false);
                    avsRowList2[0].SetFieldValue(this.AVSDocument.Attr_Podbor, -1, -1, (object) createdPair.Podbor, false, true, true, false, false, false, false);
                  }
                }
                finally
                {
                  this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, this.viewMode == AVSViewMode.Grid);
                }
              }
              if (!createdPair.ArticleID.IsUndefinedId())
                RecentObjectsNode.MRUObjects.Add(createdPair.ArticleID, ObjectAction.Create, DateTime.UtcNow);
              if (!createdPair.DocumentID.IsUndefinedId())
                RecentObjectsNode.MRUObjects.Add(createdPair.DocumentID, ObjectAction.Create, DateTime.UtcNow);
              if (objectIDs1.Count > 0)
                ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs1));
            }
          }
        }
        else
        {
          bool flag1 = true;
          long num5 = -1;
          if (MetaDataHelper.GetAttribute4ObjectType(num1, AvsIDCache.Attr_Designation).Unique == UniqueValueModes.TypeOnly && !MetaDataHelper.IsObjectTypeChildOf(num1, AvsIDCache.ObjType_Specification))
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              string documentDesignation = this.GetNewDocumentDesignation(sessionKeeper.Session, num1, contextChapters.Product);
              IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(num1);
              objectCollection.ShowAllModifications = true;
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(AvsIDCache.Attr_Designation, RelationalOperators.Equal, (object) documentDesignation, LogicalOperators.NONE, 0, false)
              }, new ColumnDescriptor[1]
              {
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
              });
              DataTable dataTable = objectCollection.Select(paramSet);
              if (dataTable != null)
              {
                if (dataTable.Rows.Count > 0)
                {
                  if (MessageBox.Show($"В базе данных есть документ с обозначение '{documentDesignation}', добавить его в состав?", "Создание объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                  {
                    flag1 = false;
                    num5 = Convert.ToInt64(dataTable.Rows[0][0]);
                  }
                }
              }
            }
          }
          if (flag1)
          {
            IObjectCreatorService objectCreatorService = AVSPlugin.ServiceProvider == null ? ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService : AVSPlugin.ServiceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
            objectCreatorService.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.cDlg_ObjectCreatorDraftCreatedEvent);
            try
            {
              num5 = objectCreatorService.CreateObjectByTypeDialog(num1);
            }
            finally
            {
              objectCreatorService.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.cDlg_ObjectCreatorDraftCreatedEvent);
            }
          }
          if (!num5.IsUndefinedId())
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(num5);
              if (dbObject == null)
                return (List<AVSRow>) null;
              if (!this.CheckEmptyObjectNameAndDesignation(dbObject, sessionKeeper.Session, true))
              {
                dbObject.Delete(0L);
                return (List<AVSRow>) null;
              }
            }
            if (contextChapters.Products == null && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
            {
              TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
              if (selectCharterForm.ShowDialog() != DialogResult.OK)
                return (List<AVSRow>) null;
              contextChapters.Products = selectCharterForm.GetSelectedProducts();
            }
            long id;
            string caption1;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(num5);
              id = dbObject.ID;
              caption1 = dbObject.Caption;
            }
            ArrayList arrayList = new ArrayList();
            if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Document, num1))
            {
              DataTable dataTable = (DataTable) null;
              bool flag2 = AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, num1) || contextChapters.Section != null && contextChapters.Section.ChapterID != AVSDocument.ObjID_SectionDocumentation && AVSDocument.IsParentObjectType(AvsIDCache.ObjType_DetailDrawing, num1);
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                if (!flag2)
                {
                  IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(AvsIDCache.Relation_Document, num1, this.avsDocument.productType);
                  if (applicability == null || applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
                    flag2 = true;
                }
                if (flag2)
                  dataTable = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[6]
                  {
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                    new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
                  }), num5);
              }
              if (flag2 && dataTable != null)
              {
                if ((AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Specification, num1) || AVSDocument.IsParentObjectType(AvsIDCache.ObjType_DetailDrawing, num1)) && dataTable.Rows.Count == 1)
                {
                  for (int index = 0; index < dataTable.Rows.Count; ++index)
                  {
                    long int64_1 = Convert.ToInt64(dataTable.Rows[index][0]);
                    int int32 = Convert.ToInt32(dataTable.Rows[index][1]);
                    string caption2 = Convert.ToString(dataTable.Rows[index][2]);
                    long int64_2 = Convert.ToInt64(dataTable.Rows[index][3]);
                    long int64_3 = Convert.ToInt64(dataTable.Rows[index][4]);
                    long int64_4 = Convert.ToInt64(dataTable.Rows[index][5]);
                    arrayList.Add((object) new DBTypedObjectID(int32, int64_1, int64_2, caption2, int64_3, 0L, 0L, string.Empty, int64_4));
                  }
                }
                else if (dataTable.Rows.Count > 0)
                {
                  List<long> objectIDs3 = new List<long>(dataTable.Rows.Count);
                  foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                    objectIDs3.Add(Convert.ToInt64(row[0]));
                  ListDescriptor rootDescriptor = new ListDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, caption1, (IList) objectIDs3);
                  object[] objArray = Intermech.Navigator.SelectionWindow.Select($"Выберите изделия взамен \"{caption1}\"", (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule);
                  if (objArray != null)
                  {
                    for (int index = 0; index < objArray.Length; ++index)
                      arrayList.Add(objArray[index]);
                  }
                }
              }
            }
            if (arrayList.Count == 0)
              avsRowList1 = this.avsDocument.AddAvsRowParts(new object[1]
              {
                (object) new DBTypedObjectID(num1, num5, id, caption1, 0L, 0L, 0L, string.Empty, 0L)
              }, -1, contextChapters, true, true);
            else
              avsRowList1 = this.avsDocument.AddAvsRowParts(arrayList.ToArray(), -1, contextChapters, true, true);
            objectIDs1.Add(num5);
            ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs1));
          }
        }
      }
      return avsRowList1;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return (List<AVSRow>) null;
    }
  }

  /// <summary>Обозначение нового, добавляемого в состав документа</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="objectType">Тип документа</param>
  /// <param name="product">Изделие для которого создаётся документ</param>
  /// <returns></returns>
  private string GetNewDocumentDesignation(
    IUserSession session,
    int objectType,
    ProductInfo product)
  {
    string code = (string) null;
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (customService != null)
    {
      DocumentTypeSettings settings = customService.GetSettings(session.SessionGUID, objectType);
      if (settings.DocumentTypeCodeInDesignation)
        code = settings.DocumentTypeCode;
    }
    string designation = this.DocumentDesignation;
    if (product != null && !string.IsNullOrEmpty(product.Designation))
      designation = product.Designation;
    return code != null ? DocumentsHelper.AppendDocCode(session, designation, code) : designation;
  }

  private void cDlg_ObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
    try
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(e.ObjectTypeID, AvsIDCache.ObjType_Document))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObj = sessionKeeper.Session.GetObject(e.ObjectID, false);
        if (dbObj == null)
          return;
        string code = (string) null;
        IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
        if (customService != null)
        {
          DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, dbObj.ObjectType);
          if (settings.DocumentTypeCodeInDesignation)
            code = settings.DocumentTypeCode;
        }
        AttributeValues[] values = new AttributeValues[3]
        {
          new AttributeValues(AvsIDCache.Attr_Designation, code != null ? (object) DocumentsHelper.AppendDocCode(sessionKeeper.Session, this.DocumentDesignation, code) : (object) this.DocumentDesignation),
          new AttributeValues(AvsIDCache.Attr_Name, (object) this.DocumentName),
          new AttributeValues(AvsIDCache.Attr_OwnerLink, (object) this.AVSDocument.ProductId)
        };
        DBObjectHelper.SetDBAttributeValues(dbObj, values);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private bool ImBaseSelectionHandler(long objectId, DynamicSelectionMode mode)
  {
    try
    {
      if (mode == DynamicSelectionMode.Select)
      {
        if (objectId == -1L || objectId == 0L)
          return false;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
          if (dbObject == null)
            return false;
          if (!this.CheckEmptyObjectNameAndDesignation(dbObject, sessionKeeper.Session, true))
            return false;
        }
        DocumentTreeNode[] commandContext = this.GetCommandContext();
        DocumentTreeNode contextNode = (DocumentTreeNode) null;
        if (commandContext.Length == 1)
          contextNode = commandContext[0];
        AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode);
        if (contextChapters.Products == null || contextChapters.Products.Count == 0)
        {
          if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
          {
            TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
            if (selectCharterForm.ShowDialog() != DialogResult.OK)
              return true;
            contextChapters.Products = selectCharterForm.GetSelectedProducts();
          }
          else
          {
            contextChapters.Products = new List<ProductInfo>();
            contextChapters.Products.Add(this.avsDocument.commonDataChapter.Product);
          }
        }
        this.avsDocument.AddAvsRowParts(new object[1]
        {
          (object) new DBTypedObjectID(-1, objectId, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L)
        }, -1, contextChapters, false, true);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return true;
  }

  /// <summary>Получить список идентификаторов выделенных или доступных каталогов Imbase</summary>
  /// <param name="contextNode">Узел документа</param>
  /// <returns>Список идентификаторов выделенных или доступных каталогов Imbase</returns>
  protected List<object> GetAvailableCatalogs(DocumentTreeNode contextNode)
  {
    List<object> availableCatalogs = new List<object>();
    if (!this.avsDocument.IsSpecification)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (Guid imbaseCatalog in this.avsDocument.AVSCommonPropertiesSchema.ImbaseCatalogs)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(imbaseCatalog, false);
          if (dbObject != null)
            availableCatalogs.Add((object) dbObject.ObjectID);
        }
        if (availableCatalogs.Count == 0)
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(new Guid("{cad008d9-306c-11d8-b4e9-00304f19f545}"), false);
          if (dbObject1 != null)
            availableCatalogs.Add((object) dbObject1.ObjectID);
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(new Guid("{cad008e6-306c-11d8-b4e9-00304f19f545}"), false);
          if (dbObject2 != null)
            availableCatalogs.Add((object) dbObject2.ObjectID);
        }
      }
      return availableCatalogs;
    }
    SpecificationSection specificationSection = (SpecificationSection) null;
    if (contextNode != null)
      specificationSection = this.avsDocument.GetSection(contextNode);
    this.selectedSection = specificationSection;
    object[] objArray = (object[]) null;
    if (this.selectedSection != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = (IDBObject) null;
        if (!this.selectedSection.ChapterID.IsUndefinedId())
          dbObject = sessionKeeper.Session.GetObject(this.selectedSection.ChapterID);
        if (dbObject != null)
        {
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(AvsIDCache.AttrRefToImBaseDirectory);
          if (attributeByGuid != null)
            objArray = attributeByGuid.Values;
        }
      }
      if (objArray != null && objArray.Length != 0 && objArray[0] != DBNull.Value)
      {
        for (int index = 0; index < objArray.Length; ++index)
        {
          if (!availableCatalogs.Contains(objArray[index]))
            availableCatalogs.Add(objArray[index]);
        }
      }
    }
    else
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      List<SpecificationSectionInfo> documentSections = this.AVSDocument.GetAllowableDocumentSections();
      for (int index1 = 0; index1 < documentSections.Count; ++index1)
      {
        long[] imBaseCatalogs = documentSections[index1].ImBaseCatalogs;
        if (imBaseCatalogs != null && imBaseCatalogs.Length != 0)
        {
          for (int index2 = 0; index2 < imBaseCatalogs.Length; ++index2)
          {
            if (!availableCatalogs.Contains((object) imBaseCatalogs[index2]))
              availableCatalogs.Add((object) imBaseCatalogs[index2]);
          }
        }
      }
    }
    return availableCatalogs;
  }

  /// <summary>Получить список идентификаторов выделенных или доступных каталогов Imbase</summary>
  /// <param name="sections">Список разделов</param>
  /// <returns>Список идентификаторов выделенных или доступных каталогов Imbase</returns>
  protected List<object> GetAvailableCatalogs(IList<SpecificationSectionInfo> sections)
  {
    List<object> availableCatalogs = new List<object>();
    if (!this.avsDocument.IsSpecification)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (Guid imbaseCatalog in this.avsDocument.AVSCommonPropertiesSchema.ImbaseCatalogs)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(imbaseCatalog, false);
          if (dbObject != null)
            availableCatalogs.Add((object) dbObject.ObjectID);
        }
        if (availableCatalogs.Count == 0)
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(new Guid("{cad008d9-306c-11d8-b4e9-00304f19f545}"), false);
          if (dbObject1 != null)
            availableCatalogs.Add((object) dbObject1.ObjectID);
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(new Guid("{cad008e6-306c-11d8-b4e9-00304f19f545}"), false);
          if (dbObject2 != null)
            availableCatalogs.Add((object) dbObject2.ObjectID);
        }
      }
      return availableCatalogs;
    }
    if (!SpecificationSectionInfo.Cached)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
    }
    if (sections == null || sections.Count == 0)
      sections = (IList<SpecificationSectionInfo>) this.AVSDocument.GetAllowableDocumentSections();
    for (int index1 = 0; index1 < sections.Count; ++index1)
    {
      long[] imBaseCatalogs = sections[index1].ImBaseCatalogs;
      if (imBaseCatalogs != null && imBaseCatalogs.Length != 0)
      {
        for (int index2 = 0; index2 < imBaseCatalogs.Length; ++index2)
        {
          if (!availableCatalogs.Contains((object) imBaseCatalogs[index2]))
            availableCatalogs.Add((object) imBaseCatalogs[index2]);
        }
      }
    }
    return availableCatalogs;
  }

  /// <summary>Групповой ввод из ImBase</summary>
  /// <param name="contextNode"></param>
  private void ContextAddGroupSpecRowFromImbase(DocumentTreeNode contextNode)
  {
    if (AVSPlugin.ImbaseSelector == null)
      return;
    List<object> availableCatalogs = this.GetAvailableCatalogs(contextNode);
    if (availableCatalogs.Count == 0)
    {
      if (this.selectedSection != null)
      {
        int num1 = (int) MessageBox.Show($"Текущему разделу \"{this.selectedSection.Caption}\" не назначен ни один каталог Imbase", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num2 = (int) MessageBox.Show("Ни одному из разделов спецификации не назначены каталоги Imbase", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    else
    {
      AVSRow avsRow = (AVSRow) null;
      if (contextNode != null)
        avsRow = this.avsDocument.GetAvsDocRow(contextNode);
      long contextObjsID = -1;
      if (avsRow != null)
        contextObjsID = avsRow.ObjectId;
      AVSPlugin.ImbaseSelector.DynamicSelection("Выберите объект", "Выберите изделия, которые хотите добавить в спецификацию", (object) availableCatalogs, false, true, -1, new DynamicSelectionEventHandler(this.ImBaseSelectionHandler), contextObjsID);
    }
  }

  /// <summary>Добавить строку в спецификацию из Imbase</summary>
  /// <param name="contextNode">Контекст</param>
  private void ContextAddSpecRowFromImbase(DocumentTreeNode contextNode)
  {
    if (AVSPlugin.ImbaseSelector == null)
      return;
    long objectId = this.SelectObjectFromIMBase(contextNode);
    switch (objectId)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        this.ImBaseSelectionHandler(objectId, DynamicSelectionMode.Select);
        break;
    }
  }

  /// <summary>Выбрать объект из IMBase</summary>
  /// <param name="contextNode">Контекст</param>
  /// <returns>Идентификатор выбранного объекта</returns>
  private long SelectObjectFromIMBase(DocumentTreeNode contextNode)
  {
    if (AVSPlugin.ImbaseSelector == null)
      return 0;
    List<object> availableCatalogs = this.GetAvailableCatalogs(contextNode);
    if (availableCatalogs.Count == 0)
    {
      if (this.selectedSection != null)
      {
        int num1 = (int) MessageBox.Show($"Текущему разделу \"{this.selectedSection.Caption}\" не назначен ни один каталог Imbase", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num2 = (int) MessageBox.Show("Ни одному из разделов спецификации не назначены каталоги Imbase", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      return 0;
    }
    AVSRow avsRow = (AVSRow) null;
    if (contextNode != null)
      avsRow = this.avsDocument.GetAvsDocRow(contextNode);
    long contextObjsID = -1;
    if (avsRow != null)
      contextObjsID = avsRow.ObjectId;
    return AVSPlugin.ImbaseSelector.SelectFromCatalog("Выберите объект", "Выберите изделие, которое надо добавить в спецификацию", (object) availableCatalogs, false, true, (int[]) null, -1, contextObjsID);
  }

  /// <summary>Добавить запись в лист регистрации изменений</summary>
  /// <param name="after">Вставлять после текущей записи, если она задана. Иначе добавляется в конец таблицы</param>
  private void ContextAddRLIRecord(bool after)
  {
    DocumentTreeNode[] commandContext = this.GetCommandContext();
    TableData selection = this.avsDocument.AddLRIRow(commandContext.Length == 1 ? commandContext[0] : (DocumentTreeNode) null, after);
    selection.SetNeedUpdateLayoutFlag(true, false, true, true);
    this.DocumentControl.SetSelection((DocumentTreeNode) selection, true, false);
  }

  public void Context_MoveSpecRow(bool up)
  {
    AVSRow selectedSpecRow = this.GetSelectedSpecRow();
    if (selectedSpecRow == null)
      return;
    this.avsDocument.SuspendDocumentAndGridUpdates();
    try
    {
      if (up)
      {
        if (selectedSpecRow.Index != 0)
          selectedSpecRow.Section.MoveRow(selectedSpecRow.Index - 1, selectedSpecRow);
      }
      else if (selectedSpecRow.Index < selectedSpecRow.Section.Rows.Count - 1)
        selectedSpecRow.Section.MoveRow(selectedSpecRow.Index + 1, selectedSpecRow);
      this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
      this.avsDocument.UpdateVariableDataCaptions();
    }
    finally
    {
      this.avsDocument.IndexAVSDocument(true);
      this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      this.DocumentControl.ScrollSelectionToView(false, false);
    }
  }

  /// <summary>Переместить записи спецификации</summary>
  /// <param name="specRows">Массив узлов документа записей спецификации</param>
  public void MoveSpecRow(List<AVSRow> specRows)
  {
    try
    {
      if (specRows == null || specRows.Count <= 0)
        return;
      List<SpecificationSectionInfo> sections = new List<SpecificationSectionInfo>();
      foreach (SpecificationSectionInfo allowableDocumentSection in this.avsDocument.GetAllowableDocumentSections())
      {
        for (int index1 = 0; index1 < specRows.Count; ++index1)
        {
          for (int index2 = 0; index2 < allowableDocumentSection.PartTypes.Length; ++index2)
          {
            if (specRows[index1].IsNoteRow || specRows[index1].ObjType == allowableDocumentSection.PartTypes[index2] || MetaDataHelper.IsObjectTypeChildOf(specRows[index1].ObjType, allowableDocumentSection.PartTypes[index2]))
            {
              if (!sections.Contains(allowableDocumentSection))
              {
                sections.Add(allowableDocumentSection);
                break;
              }
              break;
            }
          }
        }
      }
      SelectSectionForm selectSectionForm = new SelectSectionForm(sections);
      selectSectionForm.Multiselect = false;
      if (selectSectionForm.ShowDialog() != DialogResult.OK)
        return;
      List<long> selectedSectionIds = selectSectionForm.GetSelectedSectionIDs();
      if (selectedSectionIds.Count <= 0)
        return;
      this.avsDocument.SuspendDocumentAndGridUpdates();
      try
      {
        long num = selectedSectionIds[0];
        if (this.viewMode == AVSViewMode.Page)
          this.DocumentControl.SetSelection(new List<DocumentTreeNode>(), false, false);
        for (int index = 0; index < specRows.Count; ++index)
        {
          if (specRows[index].SectionID != num)
          {
            if (!(specRows[index].Section.Parent.GetChapter(num) is SpecificationSection newSection))
            {
              newSection = this.avsDocument.CreateSection(num);
              specRows[index].Section.Parent.AddChapter((Chapter) newSection, true, true, this.viewMode == AVSViewMode.Grid, specRows[index].Section.Parent.GetSectionTemplate());
            }
            specRows[index].Section.MoveRow(specRows[index], newSection, true, this.viewMode == AVSViewMode.Grid, true);
          }
        }
        this.avsDocument.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
        this.avsDocument.UpdateVariableDataCaptions();
      }
      finally
      {
        this.avsDocument.IndexAVSDocument(true);
        this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
      if (!this.AVSDocument.IsGridViewMode)
        return;
      for (int index = 0; index < specRows.Count; ++index)
        this.AVSDocument.AVSWindow.virtualTree.ExpandTo((IVirtualTreeItem) specRows[index]);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Переместить записи спецификации</summary>
  /// <param name="specRows">Массив узлов документа записей спецификации</param>
  public void MoveSpecRowToChapter(
    List<AVSRow> specRows,
    AdditionalChapterSettings newChapterSettings)
  {
    try
    {
      if (specRows == null || specRows.Count <= 0)
        return;
      if (newChapterSettings == null)
      {
        List<AdditionalChapterSettings> chapters = new List<AdditionalChapterSettings>();
        chapters.AddRange((IEnumerable<AdditionalChapterSettings>) this.avsDocument.AVSCommonPropertiesSchema.AdditionalChapters);
        chapters.Insert(0, new AdditionalChapterSettings(AVSDocument.ChapterCommonDataGuid, -1L, "Общая часть", -10L));
        SelectChapterDlg selectChapterDlg = new SelectChapterDlg(chapters, this.avsDocument.DocumentDesignation);
        selectChapterDlg.Multiselect = false;
        if (selectChapterDlg.ShowDialog() == DialogResult.OK)
          newChapterSettings = selectChapterDlg.GetSelectedChapter();
      }
      if (newChapterSettings == null)
        return;
      this.avsDocument.SuspendDocumentAndGridUpdates();
      try
      {
        this.avsDocument.MoveSpecRowToChapter(specRows, newChapterSettings);
      }
      finally
      {
        this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
      if (!this.AVSDocument.IsGridViewMode)
        return;
      for (int index = 0; index < specRows.Count; ++index)
        this.AVSDocument.AVSWindow.virtualTree.ExpandTo((IVirtualTreeItem) specRows[index]);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Получить список записей для связей</summary>
  /// <param name="relations">Список идентификаторов связей</param>
  private List<AVSRow> GetSpecRowsForRelations(List<long> relations)
  {
    List<AVSRow> rowsForRelations = relations != null ? new List<AVSRow>(relations.Count) : throw new ArgumentNullException(nameof (relations));
    HybridDictionary hybridDictionary = new HybridDictionary(relations.Count);
    for (int index = 0; index < relations.Count; ++index)
    {
      AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(relations[index]);
      if (avsDocRow != null && !hybridDictionary.Contains((object) avsDocRow))
      {
        rowsForRelations.Add(avsDocRow);
        hybridDictionary.Add((object) avsDocRow, (object) null);
      }
    }
    hybridDictionary.Clear();
    return rowsForRelations;
  }

  /// <summary>Изменить исполнение записей</summary>
  /// <param name="nodes">Выделенные узлы</param>
  public void ChangeRowProduct(DocumentTreeNode[] nodes)
  {
    if (this.avsDocument.AvsDocumentForm != AVSDocumentForm.A)
      return;
    List<long> relationIDs1 = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> relationIDs2 = new List<long>();
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum);
    List<AVSRow> avsRowList1 = new List<AVSRow>();
    int count = selectedSpecRows.Count;
    for (int index = 0; index < selectedSpecRows.Count; ++index)
    {
      DialogResult dialogResult = DialogResult.None;
      if (selectedSpecRows[index].Relations != null)
      {
        for (int relationIndex = 0; relationIndex < selectedSpecRows[index].Relations.Count; ++relationIndex)
        {
          object fieldValue = selectedSpecRows[index].GetFieldValue(attrInfo, relationIndex, -1, false, false);
          if (fieldValue != null && !(fieldValue is DBNull) && Convert.ToInt64(fieldValue) != 0L)
          {
            string fieldStringValue = selectedSpecRows[index].GetFieldStringValue(this.avsDocument.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
            string objCaption = selectedSpecRows[index].ObjCaption;
            if (fieldStringValue == null || fieldStringValue == "")
            {
              dialogResult = IMMessageBox.Show("Изменение исполнения записей", $"Изделие \"{objCaption}\" нельзя переместить в другое исполнение, \r\nт.к. оно входит в состав допустимых заменителей. Необходимо исключить его из группы допустимых заменителей.", new IMMessageBoxButton[2]
              {
                new IMMessageBoxButton("Пропустить", DialogResult.Ignore),
                new IMMessageBoxButton("Отменить", DialogResult.Cancel)
              });
              break;
            }
            dialogResult = IMMessageBox.Show("Изменение исполнения записей", $"Изделие \"{objCaption}\" позиция \"{fieldStringValue}\" нельзя переместить в другое исполнение, \r\nт.к. оно входит в состав допустимых заменителей. Необходимо исключить его из группы допустимых заменителей.", new IMMessageBoxButton[2]
            {
              new IMMessageBoxButton("Пропустить", DialogResult.Ignore),
              new IMMessageBoxButton("Отменить", DialogResult.Cancel)
            });
            break;
          }
        }
      }
      switch (dialogResult)
      {
        case DialogResult.Cancel:
          return;
        case DialogResult.Ignore:
          selectedSpecRows[index] = (AVSRow) null;
          --count;
          break;
      }
    }
    if (count == 0)
      return;
    List<AVSRow> avsRowList2 = new List<AVSRow>();
    try
    {
      if (selectedSpecRows.Count > 0)
      {
        SelectProductForm selectProductForm = new SelectProductForm(this.avsDocument, this.avsDocument.productsInfo, "Выберите исполнение", false, true);
        if (selectProductForm.ShowDialog() == DialogResult.OK)
        {
          ProductInfo selectedProduct = selectProductForm.SelectedProduct;
          int selectedProductIndex = selectProductForm.SelectedProductIndex;
          this.avsDocument.SuspendDocumentAndGridUpdates();
          try
          {
            AVSRow specRow = (AVSRow) null;
            long num1 = -1;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              Dictionary<int, IDBRelationCollection> dictionary = new Dictionary<int, IDBRelationCollection>();
              for (int index1 = 0; index1 < selectedSpecRows.Count; ++index1)
              {
                specRow = selectedSpecRows[index1];
                if (specRow != null && specRow.Section != null)
                {
                  AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters((DocumentTreeNode) specRow.DocNode);
                  if (!this.avsDocument.AdditionalChaptersInDataChapter)
                  {
                    contextChapters.Chapter = contextChapters.Chapter.GetRootChapter();
                    contextChapters.Chapter = !(contextChapters.Chapter is AdditionalChapter chapter) ? (!selectedProduct.IsCommonData ? this.avsDocument.VariableDataChapter.GetChapter(selectedProduct.Id) : this.avsDocument.CommonDataChapter) : (!selectedProduct.IsCommonData ? chapter.InnerVariableData_FormA.GetChapter(selectedProduct.Id) : chapter.InnerCommonDataChapter);
                  }
                  else
                  {
                    contextChapters.Chapter = !selectedProduct.IsCommonData ? (!(this.avsDocument.VariableDataChapter is VariableDataChapterFormA) || selectedProduct.Id != -1L ? this.avsDocument.VariableDataChapter.GetChapter(selectedProduct.Id) : (this.avsDocument.VariableDataChapter as VariableDataChapterFormA).GetProductChapter(selectedProduct)) : this.avsDocument.CommonDataChapter;
                    if (contextChapters.Chapter.GetRootChapter() is AdditionalChapter rootChapter)
                    {
                      if (!(contextChapters.Chapter.GetChapter(rootChapter.ChapterGuid) is AdditionalChapter additionalChapter))
                        contextChapters.Chapter.AddChapter((Chapter) (additionalChapter = new AdditionalChapter(this.avsDocument, rootChapter.GetChapterSettings(), this.avsDocument.AdditionalChaptersInDataChapter)), true, true, this.viewMode == AVSViewMode.Grid, (TableData) null);
                      contextChapters.Chapter = (Chapter) additionalChapter;
                    }
                  }
                  contextChapters.Section = (SpecificationSection) null;
                  if (this.avsDocument.IsElementList)
                    contextChapters.Section = contextChapters.Chapter as SpecificationSection;
                  if (contextChapters.Section == null)
                    contextChapters.Section = contextChapters.Chapter.GetChapter(specRow.SectionID) as SpecificationSection;
                  if (contextChapters.Section == null)
                    contextChapters.Chapter.AddChapter((Chapter) (contextChapters.Section = this.avsDocument.CreateSection(specRow.SectionID)), true, true, this.viewMode == AVSViewMode.Grid, contextChapters.Chapter.GetSectionTemplate());
                  contextChapters.Product = selectedProduct;
                  Dictionary<int, List<long>> relations = new Dictionary<int, List<long>>();
                  --num1;
                  if (specRow.Product != selectedProduct)
                  {
                    IDBRelationCollection relCollection = (IDBRelationCollection) null;
                    if (!dictionary.TryGetValue(specRow.RelType, out relCollection))
                    {
                      relCollection = sessionKeeper.Session.GetRelationCollection(specRow.RelType, this.FiltrationOwnerID);
                      dictionary.Add(specRow.RelType, relCollection);
                    }
                    if (selectedProduct.IsCommonData)
                    {
                      if (this.avsDocument.productsInfo.Count == 1 || !specRow.HasAnyRelations)
                      {
                        specRow.SortIndex = this.avsDocument.FindNextFreeSortIndex(num1);
                        specRow.Section.MoveRow(specRow, contextChapters.Section, true, this.viewMode == AVSViewMode.Grid, false);
                        avsRowList2.Add(specRow);
                      }
                      else
                      {
                        long relationId = specRow.Relations[0].RelationId;
                        long objectFId = specRow.Object_F_ID;
                        for (int index2 = 0; index2 < this.avsDocument.productsInfo.Count; ++index2)
                        {
                          int relationIndexForProduct = specRow.GetRelationIndexForProduct(this.avsDocument.productsInfo[index2].Id);
                          if (relationIndexForProduct == -1)
                          {
                            NewRelationProperties relationProperties = new NewRelationProperties(relationId, this.avsDocument.productsInfo[index2].Id, objectFId, DateTime.MinValue, DateTime.MaxValue, specRow.ObjectId, new AttributeValues[1]
                            {
                              new AttributeValues(AvsIDCache.Attr_SortIndex, (object) num1)
                            });
                            IDBRelation dbRelation = specRow.RelType != AvsIDCache.Relation_Document ? relCollection.Create(relationProperties) : AVSDocument.CreateDocRelationWithLockPDMHandler(relCollection, relationProperties);
                            AVSDocument.AddRelationToTypedDictionary(relations, specRow.RelType, dbRelation.RelationID);
                            relationIDs1.Add(dbRelation.RelationID);
                            projIDs.Add(dbRelation.ProjID);
                            relTypeIDs.Add(dbRelation.RelationType);
                          }
                          else
                          {
                            AVSDocument.AddRelationToTypedDictionary(relations, specRow.RelType, specRow.Relations[relationIndexForProduct].RelationId);
                            specRow.SortIndex = this.avsDocument.FindNextFreeSortIndex(num1);
                          }
                        }
                        specRow.Section.RemoveRow(specRow, true, false, true, true, false);
                        contextChapters.Section = (SpecificationSection) null;
                        List<AVSRow> collection = this.avsDocument.AddSpecificationRelations(relations, false, contextChapters);
                        if (collection.Count > 0)
                          avsRowList2.AddRange((IEnumerable<AVSRow>) collection);
                      }
                      if (this.avsDocument.IsSpecification || this.avsDocument.IsElementList)
                      {
                        List<AVSRow> list = this.AVSDocument.GetAvsRowsByObjectId(specRow.ObjectId).Where<AVSRow>((System.Func<AVSRow, bool>) (x => x.Position == specRow.Position && !x.InCommonData_AV)).ToList<AVSRow>();
                        if (list.Count > 0 && IMMessageBox.Show("Изменение исполнения записей", $"Изделие \"{specRow.ObjCaption}\" позиция \"{specRow.Position}\" , найдено {list.Count} шт. в других исполнениях, удалить из других исполнений?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                          foreach (AVSRow row in list)
                            row.Section.RemoveRow(row, true, true, true, true, false);
                        }
                      }
                    }
                    else if (this.avsDocument.productsInfo.Count == 1)
                    {
                      specRow.SortIndex = this.avsDocument.FindNextFreeSortIndex(num1);
                      specRow.Section.MoveRow(specRow, contextChapters.Section, true, this.viewMode == AVSViewMode.Grid, false);
                      avsRowList2.Add(specRow);
                    }
                    else
                    {
                      long objectFId = specRow.Object_F_ID;
                      bool flag = false;
                      long num2 = -1;
                      if (specRow.Relations != null)
                      {
                        for (int index3 = 0; index3 < specRow.Relations.Count; ++index3)
                        {
                          if (specRow.Relations[index3].ProjectId != selectedProduct.Id)
                          {
                            if (!num2.IsUndefinedId())
                            {
                              sessionKeeper.Session.GetRelationByPartObjectID(specRow.Relations[index3].RelationId, specRow.ObjectId, true)?.Delete(0L);
                              relationIDs2.Add(specRow.Relations[index3].RelationId);
                            }
                            else
                              num2 = specRow.Relations[index3].RelationId;
                          }
                          else
                            flag = true;
                        }
                      }
                      if (flag)
                      {
                        for (int index4 = specRow.Relations.Count - 1; index4 >= 0; --index4)
                        {
                          if (specRow.Relations[index4].ProjectId != selectedProduct.Id)
                            specRow.RemoveRelationData((List<RelationAttributeValuesCache>) null, index4);
                        }
                        if (!num2.IsUndefinedId())
                        {
                          sessionKeeper.Session.GetRelation(num2, false)?.Delete(0L);
                          relationIDs2.Add(num2);
                        }
                        specRow.SortIndex = this.avsDocument.FindNextFreeSortIndex(num1);
                        specRow.Section.MoveRow(specRow, contextChapters.Section, true, this.viewMode == AVSViewMode.Grid, false);
                        avsRowList2.Add(specRow);
                      }
                      else if (!selectedProduct.Id.IsUndefinedId())
                      {
                        NewRelationProperties relationProperties = new NewRelationProperties(!num2.IsUndefinedId() ? num2 : 0L, selectedProduct.Id, objectFId, DateTime.MinValue, DateTime.MaxValue, specRow.ObjectId, new AttributeValues[1]
                        {
                          new AttributeValues(AvsIDCache.Attr_SortIndex, (object) num1)
                        });
                        IDBRelation dbRelation = specRow.RelType != AvsIDCache.Relation_Document ? relCollection.Create(relationProperties) : AVSDocument.CreateDocRelationWithLockPDMHandler(relCollection, relationProperties);
                        AVSDocument.AddRelationToTypedDictionary(relations, dbRelation.RelationType, dbRelation.RelationID);
                        relationIDs1.Add(dbRelation.RelationID);
                        projIDs.Add(dbRelation.ProjID);
                        relTypeIDs.Add(dbRelation.RelationType);
                        if (!num2.IsUndefinedId())
                        {
                          sessionKeeper.Session.GetRelation(num2, false)?.Delete(0L);
                          relationIDs2.Add(num2);
                        }
                        specRow.Section.RemoveRow(specRow, true, false, true, true, false);
                        List<AVSRow> collection = this.avsDocument.AddSpecificationRelations(relations, false, contextChapters);
                        if (collection.Count > 0)
                          avsRowList2.AddRange((IEnumerable<AVSRow>) collection);
                      }
                      else
                      {
                        specRow.SortIndex = this.avsDocument.FindNextFreeSortIndex(num1);
                        specRow.Section.MoveRow(specRow, contextChapters.Section, true, this.viewMode == AVSViewMode.Grid, false);
                        avsRowList2.Add(specRow);
                      }
                    }
                  }
                }
              }
            }
            this.avsDocument.UpdateViewNodes(false, false, false, true, true, EmptyRowUpdateMode.DontChange);
            this.avsDocument.UpdateVariableDataCaptions();
          }
          finally
          {
            this.avsDocument.IndexAVSDocument(true);
            this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
            List<DocumentTreeNode> selection = new List<DocumentTreeNode>(avsRowList2.Count > 0 ? avsRowList2.Count : 1);
            for (int index = 0; index < avsRowList2.Count; ++index)
            {
              if (avsRowList2[index].DocNode != null)
                selection.Add((DocumentTreeNode) avsRowList2[index].DocNode);
            }
            if (selection.Count > 0)
              this.DocumentControl.SetSelection(selection, true, false);
          }
        }
      }
      INotificationService notificationService = (INotificationService) null;
      if (relationIDs2.Count > 0)
      {
        notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        notificationService?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs2));
      }
      if (relationIDs1.Count <= 0)
        return;
      if (notificationService == null)
        notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      notificationService?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs1, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Преобразовать заготовку в обычное изделие</summary>
  /// <param name="nodes">Выделенные узлы</param>
  public void ConvertZagotovkaToPart(DocumentTreeNode[] nodes)
  {
    List<long> relationIDs1 = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> relationIDs2 = new List<long>();
    List<AVSRow> selectedSpecRows = this.GetSelectedSpecRows(false);
    if (selectedSpecRows.Count == 0)
      return;
    List<AVSRow> avsRowList = new List<AVSRow>();
    AVSDocumentContext avsDocumentContext = new AVSDocumentContext();
    try
    {
      if (selectedSpecRows.Count > 0)
      {
        this.avsDocument.SuspendDocumentAndGridUpdates();
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBRelationCollection relationCollection1 = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Project, this.FiltrationOwnerID);
            sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Zagotovka, this.FiltrationOwnerID);
            IDBRelationCollection relationCollection2 = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Zagotovka);
            for (int index1 = 0; index1 < selectedSpecRows.Count; ++index1)
            {
              AVSRow draftRow = selectedSpecRows[index1];
              if (draftRow != null && draftRow.RelType == AvsIDCache.Relation_Zagotovka)
              {
                long fieldInt64Value = draftRow.GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false);
                if (fieldInt64Value.IsDefinedId())
                {
                  IDBObject objectActual = sessionKeeper.Session.GetObjectActual(fieldInt64Value, true);
                  AVSRow partForDraft = draftRow.GetPartForDraft();
                  string reason;
                  if (partForDraft != null && this.avsDocument is AVSSpecification avsDocument && !avsDocument.VerifyMaterialEditableForPartRow(partForDraft, out reason))
                    throw new Exception("Ошибка конвертации заготовки: " + reason);
                  ICollection<int> lockedAttributes = ServiceUtils.GetService<IAttributesLockService>((object) ServicesManager.ServiceContainer, true)?.GetLockedAttributes(AttributableElements.Object, objectActual.ObjectID, objectActual.ObjectType);
                  AttributeValues attributeValues = lockedAttributes == null || !lockedAttributes.Contains(AvsIDCache.Attr_Material) ? new AttributeValues(AvsIDCache.Attr_Material, (object) null) : throw new Exception("Ошибка конвертации заготовки: Заготовка изделия с CAD-моделью не может быть преобразована.");
                  objectActual.SetAttributesValues(new AttributeValues[1]
                  {
                    attributeValues
                  });
                }
                long[] array = draftRow.AllRelations.Select<RelationAttributeValuesCache, long>((System.Func<RelationAttributeValuesCache, long>) (r => r.RelationId)).ToArray<long>();
                CommandResult commandResult = relationCollection2.DeleteAttribute(array, (object) AvsIDCache.Attr_ArticleID, true);
                if (commandResult.ProcessedObjects.Length < array.Length)
                  throw new Exception("Ошибка конвертации заготовки: " + commandResult.ErrorMessage);
                if (draftRow.Relations != null && draftRow.Relations.Count > 0)
                {
                  long objectFId = draftRow.Object_F_ID;
                  for (int index2 = 0; index2 < draftRow.Relations.Count; ++index2)
                  {
                    long relationId = draftRow.Relations[index2].RelationId;
                    NewRelationProperties properties = new NewRelationProperties(!relationId.IsUndefinedId() ? relationId : 0L, draftRow.Relations[index2].ProjectId, objectFId, DateTime.MinValue, DateTime.MaxValue, draftRow.ObjectId);
                    IDBRelation dbRelation = relationCollection1.Create(properties);
                    draftRow.Relations[index2].SetRelationID(dbRelation.RelationID, dbRelation.GUID, dbRelation.RelationType, draftRow.Relations[index2].projInfo);
                    draftRow.SetFieldValue(this.avsDocument.Attr_SortIndex, index2, -1, (object) draftRow.SortIndex, true, true, false, false, false, false);
                    relationIDs1.Add(dbRelation.RelationID);
                    projIDs.Add(dbRelation.ProjID);
                    relTypeIDs.Add(dbRelation.RelationType);
                    if (!relationId.IsUndefinedId())
                    {
                      sessionKeeper.Session.GetRelation(relationId, false)?.Delete(0L);
                      relationIDs2.Add(relationId);
                    }
                  }
                }
                draftRow.SaveRelationsReferencesToDocRows();
                draftRow.RelType = AvsIDCache.Relation_Project;
                if (draftRow.DocNode != null)
                {
                  if (draftRow.GetDocumentCellForAttribute(draftRow.Field_Name, -1) != null)
                  {
                    if (draftRow.DocNode != null)
                    {
                      draftRow.DocNode.RemoveAttribute(AVSRow.DocAttr_ZagotovkaDlya, false, false);
                      draftRow.DocNode.RemoveAttribute(AVSRow.DocAttr_PartFromDraftGuid, false, false);
                    }
                    draftRow.UpdateNameDocCellText(false, false);
                  }
                  for (int index3 = 0; index3 < draftRow.DocNodes.Count; ++index3)
                  {
                    ReferenceToDBObject reference = draftRow.DocNodes[index3].Reference as ReferenceToDBObject;
                    if (draftRow.Relations != null && draftRow.Relations.Count > 0)
                    {
                      if (reference == null)
                      {
                        ReferenceToDBObject referenceToDbObject = new ReferenceToDBObject((DocumentTreeNode) draftRow.DocNodes[index3], RefToDBObjectType.rtSelectedRelation, (DBObjectInfoBase) draftRow.RowID, true);
                        referenceToDbObject.PassiveLink = true;
                        draftRow.DocNodes[index3].AssignReference((ReferenceBase) referenceToDbObject, false, false);
                      }
                      else
                        reference.AssignDBObjectInfo((DBObjectInfoBase) draftRow.RowID, true);
                    }
                    else if (draftRow.ObjectAttributesCache != null)
                    {
                      if (reference == null)
                      {
                        ReferenceToDBObject referenceToDbObject = new ReferenceToDBObject((DocumentTreeNode) draftRow.DocNodes[index3], RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(draftRow.ObjGuid, draftRow.ObjectId, draftRow.ObjType, draftRow.ObjCaption), true);
                        referenceToDbObject.PassiveLink = true;
                        draftRow.DocNodes[index3].AssignReference((ReferenceBase) referenceToDbObject, false, false);
                      }
                      else
                        reference.AssignDBObjectInfo((DBObjectInfoBase) draftRow.RowID, true);
                    }
                    else
                      draftRow.DocNodes[index3].AssignReference((ReferenceBase) null, false, false);
                  }
                }
                draftRow.SetFieldValue(this.avsDocument.Field_Position, -1, -1, (object) "", true, true, true, this.viewMode == AVSViewMode.Grid, false, false);
              }
            }
          }
        }
        finally
        {
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        }
      }
      INotificationService notificationService = (INotificationService) null;
      if (relationIDs2.Count > 0)
      {
        notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
        notificationService?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relationIDs2));
      }
      if (relationIDs1.Count <= 0)
        return;
      if (notificationService == null)
        notificationService = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
      notificationService?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs1, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Копировать в буфер обмена</summary>
  /// <param name="context">Контекст</param>
  public void CopyToClipboardCommand(DocumentTreeNode[] context)
  {
    this.CopyToClipboardCommand(context, false);
  }

  /// <summary>Копировать в буфер обмена</summary>
  /// <param name="context">Контекст</param>
  /// <param name="canCopyFromEditor">Надо ли проверять возможность копирования из активного редактора</param>
  public void CopyToClipboardCommand(DocumentTreeNode[] context, bool canCopyFromEditor)
  {
    if (context == null || context.Length == 0)
      return;
    if (context.Length == 1)
    {
      if (this.ViewMode == AVSViewMode.Page & canCopyFromEditor && context[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
      {
        placeEditorControl.TerCommand(629);
        return;
      }
      if (this.ViewMode == AVSViewMode.Grid && this.virtualTree.TextEditor != null)
      {
        TextBox textEditor = this.virtualTree.TextEditor;
        if (textEditor.SelectionLength != 0)
        {
          textEditor.Copy();
          return;
        }
      }
    }
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context);
    if (nodesWithoutChilds.Length == 1 && nodesWithoutChilds[0].IsVirtualNode && nodesWithoutChilds[0] is RectangleElement)
      nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(((RectangleElement) nodesWithoutChilds[0]).GetRealCells().ToArray());
    for (int index = 0; index < nodesWithoutChilds.Length; ++index)
    {
      if (AVSDocument.FindParentSpecRowDocNode(nodesWithoutChilds[index]) is TableData parentSpecRowDocNode)
      {
        AVSRow avsDocRow = this.avsDocument.GetAvsDocRow((DocumentTreeNode) parentSpecRowDocNode);
        if (avsDocRow != null && avsDocRow.ObjectId.IsDefinedId())
        {
          if (!arrayList1.Contains((object) avsDocRow))
            arrayList1.Add((object) avsDocRow);
        }
        else if (parentSpecRowDocNode.Reference != null && !arrayList1.Contains((object) parentSpecRowDocNode))
          arrayList1.Add((object) parentSpecRowDocNode);
      }
      else if (AVSDocument.FindParentNoteRowDocNode(nodesWithoutChilds[index]) is TableData parentNoteRowDocNode && !arrayList1.Contains((object) parentNoteRowDocNode))
        arrayList1.Add((object) parentNoteRowDocNode);
    }
    ArrayList rowsList = new ArrayList(arrayList2.Count);
    long objectID = -1;
    Guid empty = Guid.Empty;
    for (int index = 0; index < arrayList1.Count; ++index)
    {
      AVSRow avsRow = arrayList1[index] as AVSRow;
      TableData tableData = (TableData) (arrayList1[index] as TableElement);
      long id = 0;
      long result = long.MinValue;
      long owner = 0;
      if (avsRow != null)
      {
        TableData docNode = avsRow.DocNode;
        if (!avsRow.ObjectId.IsUndefinedId())
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
            if (dbObject != null)
            {
              id = dbObject.ID;
              owner = dbObject.OwnerID;
            }
          }
        }
        DBTypedObjectID dbTypedObjectId = new DBTypedObjectID(avsRow.ObjType, avsRow.ObjectId, id, avsRow.ObjCaption, owner, 0L, 0L, string.Empty, 0L);
        DBRelationID dbRelationId = new DBRelationID(avsRow.RelId, avsRow.ObjectId, avsRow.RelType, avsRow.SortIndex, avsRow.RelGuid, avsRow.ProductID);
        if (docNode != null)
          rowsList.Add((object) new AvsRowClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId, (TableData) docNode.Clone(), avsRow.IsFormB));
        else
          rowsList.Add((object) new ClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId));
      }
      else if (tableData != null)
      {
        if (tableData.Reference is ReferenceToDBObject)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ReferenceToDBObject reference = tableData.Reference as ReferenceToDBObject;
            if (!reference.IsConnectedObjectRef)
              reference.UpdateDBObjectInfo(sessionKeeper.Session, this.FiltrationOwnerID);
            long.TryParse(tableData.GetAttributeValue(AVSRow.RowAttr_SortIndex, true), out result);
            IDBObject dbObject = reference.GetDBObject(sessionKeeper.Session, this.FiltrationOwnerID);
            if (dbObject != null)
            {
              owner = dbObject.OwnerID;
              id = dbObject.ID;
            }
            DBTypedObjectID dbTypedObjectId = new DBTypedObjectID(reference.DBObjectType, reference.DBObjectID, id, reference.DBObjectCaption, owner, 0L, 0L, string.Empty, 0L);
            DBRelationID dbRelationId = new DBRelationID(reference.DBRelationID, reference.DBObjectID, reference.DBRelationType, result, reference.DBRelationGuid, reference.DBProjectID);
            PageData page = tableData.Page;
            rowsList.Add((object) new AvsRowClipboardObject((IDBTypedObjectID) dbTypedObjectId, (IDBRelationID) dbRelationId, (TableData) tableData.Clone(), page != null && this.avsDocument.IsFormBPage(page)));
          }
        }
        else
          rowsList.Add((object) tableData.Clone());
      }
    }
    if (rowsList.Count <= 0 || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    service.SetDataObject((object) new AVSRowClipboardCollection(rowsList, this.avsDocument.AVSDocType, this.avsDocument.AvsDocumentForm, this.avsDocument.DocumentID));
  }

  private void LoadSpecRowDataForClipboard(
    DBSpecificationObjectID specObject,
    TableData docRow,
    bool isFormB)
  {
    if (specObject == null)
      throw new ArgumentNullException(nameof (specObject));
    if (docRow == null)
      throw new ArgumentNullException(nameof (docRow));
    for (int index = 0; index < docRow.NodesCount; ++index)
    {
      if (docRow.Nodes[index] is TextData node && node.Text != null && node.Text != "")
      {
        AvsRowAttributeInfo attrInfoFromCell = AVSDocument.GetAttrInfoFromCell(node, -1, isFormB);
        if (attrInfoFromCell.Equals((AttributeInfo) this.avsDocument.Field_Format))
        {
          string str = node.GetAttributeValue(AVSRow.CellAttrName_EditText, false) ?? node.Text;
          specObject.Format = str;
        }
        else if (attrInfoFromCell.Equals((AttributeInfo) this.avsDocument.Field_Zone))
        {
          string str = node.GetAttributeValue(AVSRow.CellAttrName_EditText, false) ?? node.Text;
          specObject.Zone = str;
        }
        else if (attrInfoFromCell.Equals((AttributeInfo) this.avsDocument.Field_Note))
        {
          string text;
          if (AVSRow.ExtractTextBetweenProtectedZones(node as TextBoxElement, out text))
            text = (string) null;
          specObject.Remark = text;
        }
        else if (AVSRow.IsCountField(attrInfoFromCell))
        {
          string str = node.GetAttributeValue(AVSRow.CellAttrName_EditText, false) ?? node.Text;
          specObject.Quantity = str;
        }
      }
    }
  }

  /// <summary>Вставить из буфер обмена</summary>
  /// <param name="contextNode">Контекст</param>
  /// <returns>true если необходимо перестроить геометрию</returns>
  public bool PasteFromClipboardCommand(
    DocumentTreeNode contextNode,
    AVSRowClipboardCollection avsRowClipboardCollection = null)
  {
    if (this.ViewMode == AVSViewMode.Page && !contextNode.ReadOnlyStructure && NodeClipboardHelper.CanPasteFromClipboard(contextNode))
    {
      NodeClipboardHelper.PasteFromClipboard(contextNode, IntPtr.Zero);
      return false;
    }
    if (this.ViewMode == AVSViewMode.Grid && this.virtualTree.TextEditor != null && Clipboard.ContainsText())
    {
      this.virtualTree.TextEditor.Paste();
      return false;
    }
    bool flag1 = false;
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return flag1;
    bool flag2 = false;
    if (avsRowClipboardCollection == null)
      avsRowClipboardCollection = service.GetDataObject() as AVSRowClipboardCollection;
    ArrayList arrayList1;
    if (avsRowClipboardCollection != null)
    {
      arrayList1 = avsRowClipboardCollection.RowList;
      flag2 = avsRowClipboardCollection.SpecificationId == this.AVSDocument.DocumentID;
    }
    else
    {
      if (!(service.GetDataObject() is IDBObjectTypedIDCollection dataObject))
        return flag1;
      arrayList1 = new ArrayList((ICollection) dataObject.GetTypedObjects());
    }
    if (arrayList1 == null || arrayList1.Count == 0)
      return flag1;
    string str1 = (string) null;
    NewSpecObjectParams formParams = (NewSpecObjectParams) null;
    AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(contextNode);
    bool alwaysCreateRelations = (contextChapters.Chapter == null || !contextChapters.Chapter.IsFormB) && !this.avsDocument.IsFormB;
    if (contextChapters.Products.Count == 0 && this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
    {
      TemporalySelectCharterForm selectCharterForm = new TemporalySelectCharterForm(this.avsDocument);
      if (selectCharterForm.ShowDialog() != DialogResult.OK)
        return flag1;
      contextChapters.Products = selectCharterForm.GetSelectedProducts();
      if (contextChapters.Products.Count == 0)
        contextChapters.Products = this.avsDocument.productsInfo;
    }
    ArrayList arrayList2 = new ArrayList();
    Dictionary<int, List<long>> relations = new Dictionary<int, List<long>>();
    rowClipboardObject = (AvsRowClipboardObject) null;
    Dictionary<long, AvsRowClipboardObject> dictionary = new Dictionary<long, AvsRowClipboardObject>();
    if (this.avsDocument.IsSpecification && arrayList1.Count == 1 && arrayList1[0] is IDBTypedObjectID)
    {
      IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) arrayList1[0];
      DBSpecificationObjectID specificationObjectId = new DBSpecificationObjectID(-1, 0L, 0L, string.Empty);
      List<AVSRow> avsRowsByObjectId = this.AVSDocument.GetAvsRowsByObjectId(dbTypedObjectId.ObjectID);
      if (avsRowsByObjectId.Count > 0)
      {
        rowClipboardObject = arrayList1[0] as AvsRowClipboardObject;
        bool flag3 = false;
        if (avsRowClipboardCollection != null && rowClipboardObject != null && rowClipboardObject.DocRow != null && avsRowClipboardCollection.DocType == AVSDocumentType.ElementList)
        {
          for (int index = 0; index < rowClipboardObject.DocRow.Nodes.Count; ++index)
          {
            if (rowClipboardObject.DocRow.Nodes[index] is TextData node && node.Name == AVSRow.DocAttr_PosDesignation)
            {
              flag3 = !string.IsNullOrWhiteSpace(node.Text);
              break;
            }
          }
        }
        if (!flag3)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            specificationObjectId.Load(sessionKeeper.Session, avsRowsByObjectId[0].ObjectId, avsRowsByObjectId[0].RelId);
          if (rowClipboardObject != null && rowClipboardObject.DocRow != null)
          {
            this.LoadSpecRowDataForClipboard(specificationObjectId, rowClipboardObject.DocRow, rowClipboardObject.IsFormB);
          }
          else
          {
            specificationObjectId.Format = avsRowsByObjectId[0].GetFieldStringValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format), -1, -1, (List<RelationAttributeValuesCache>) null, false);
            if (avsRowsByObjectId[0].RelId == -1L)
              specificationObjectId.Zone = avsRowsByObjectId[0].GetFieldStringValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_Zone), -1, -1, (List<RelationAttributeValuesCache>) null, false);
          }
          if (!SpecificationSectionInfo.Cached)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
          }
          SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(contextChapters.Section != null ? contextChapters.Section.SectionID : -1L);
          bool flag4 = false;
          if (sectionById != null && sectionById.PartTypes != null)
          {
            for (int index = 0; index < sectionById.PartTypes.Length; ++index)
            {
              if (MetaDataHelper.IsObjectTypeChildOf(specificationObjectId.ObjectType, sectionById.PartTypes[index]))
              {
                flag4 = true;
                break;
              }
            }
          }
          if (sectionById == null)
          {
            int num = (int) MessageBox.Show("Не выбран раздел в спецификации для вставки объекта из буфера обмена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return flag1;
          }
          if (!flag4)
          {
            int num = (int) MessageBox.Show($"Объект типа \"{MetaDataHelper.GetObjectTypeName(specificationObjectId.ObjectType)}\" нельзя добавлять в раздел \"{contextChapters.Section?.Caption}\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return flag1;
          }
          formParams = new NewSpecObjectParams(this.AVSDocument, (IDBSpecificationObjectID) specificationObjectId, AVSDocument.GetProductIds(contextChapters.Products), contextChapters.Section != null ? contextChapters.Section.SectionID : -1L, alwaysCreateRelations);
          formParams.SameSpecification = flag2;
          if (CreateNewSpecObjectForm.Execute(formParams) != DialogResult.OK)
            return flag1;
          AVSDocument.AddRelationToTypedDictionary(relations, formParams.NewPart.RelationTypeID, (IEnumerable<long>) formParams.NewRelations);
          if (formParams.NewPart.RelationTypeID != AvsIDCache.Relation_Document)
            str1 = formParams.NewPart.Format;
        }
      }
    }
    List<long> relationIDs = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<AttributeValues> attributeValuesList1 = new List<AttributeValues>();
    long num1 = -1;
    List<AVSRow> avsRowList1 = new List<AVSRow>();
    using (SessionKeeper sessionKeeper1 = new SessionKeeper())
    {
      if (this.avsDocument.IsSpecification && relations.Count == 0)
      {
        bool flag5 = alwaysCreateRelations || formParams != null && !string.IsNullOrEmpty(formParams.NewPart.Quantity);
        IDBRelationCollection relationCollection1 = sessionKeeper1.Session.GetRelationCollection(AvsIDCache.Relation_Project, this.FiltrationOwnerID);
        IDBRelationCollection relationCollection2 = sessionKeeper1.Session.GetRelationCollection(AvsIDCache.Relation_Document, this.FiltrationOwnerID);
        foreach (ProductInfo product in contextChapters.Products)
        {
          IDBRelation dbRelation = (IDBRelation) null;
          for (int index1 = arrayList1.Count - 1; index1 > -1; --index1)
          {
            IDBTypedObjectID newPart = arrayList1[index1] as IDBTypedObjectID;
            if (formParams != null)
              newPart = (IDBTypedObjectID) formParams.NewPart;
            TableData srcNoteDocRow = !(arrayList1[index1] is AvsRowClipboardObject rowClipboardObject) ? arrayList1[index1] as TableData : rowClipboardObject.DocRow;
            if (newPart != null || rowClipboardObject != null || srcNoteDocRow != null)
            {
              if (newPart != null)
              {
                if (!newPart.ObjectID.IsUndefinedId())
                {
                  try
                  {
                    AVSDocument.GetDefaultSectionIdForObject(newPart.ObjectType, (string) null, contextChapters.Section != null ? contextChapters.Section.SectionID : -1L, this.avsDocument.GetAllowableDocumentSections());
                    --num1;
                    for (int index2 = 0; index2 < this.avsDocument.productsInfo.Count; ++index2)
                    {
                      long num2 = product.IsCommonData || product.IsVariableData && this.avsDocument.AvsDocumentForm == AVSDocumentForm.V ? this.avsDocument.productsInfo[index2].Id : product.Id;
                      string str2 = (string) null;
                      if ((this.avsDocument.IsFormB || contextChapters.Chapter != null && contextChapters.Chapter.IsFormB) && avsRowClipboardCollection != null && avsRowClipboardCollection.DocType == AVSDocumentType.ElementList && this.avsDocument.productsInfo.Count == 1 && rowClipboardObject != null && rowClipboardObject.DocRow != null)
                      {
                        for (int index3 = 0; index3 < rowClipboardObject.DocRow.NodesCount; ++index3)
                        {
                          if (rowClipboardObject.DocRow.Nodes[index3] is TextData node && node.Name == AVSRow.DocAttr_Count && node.Text != null && node.Text != "")
                            str2 = node.Text;
                        }
                      }
                      if (!num2.IsUndefinedId() && (flag5 || !alwaysCreateRelations && str2 != null))
                      {
                        if (AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Document, newPart.ObjectType))
                        {
                          dbRelation = sessionKeeper1.Session.GetRelation(num2, newPart.ObjectID, AvsIDCache.Relation_Document, true);
                          if (dbRelation == null)
                          {
                            dbRelation = this.avsDocument.productsInfo.Count <= 1 ? relationCollection2.Create(num2, newPart.ObjectID) : AVSDocument.CreateDocRelationWithLockPDMHandler(relationCollection2, num2, newPart.ObjectID, newPart.ID);
                            relationIDs.Add(dbRelation.RelationID);
                            projIDs.Add(dbRelation.ProjID);
                            relTypeIDs.Add(dbRelation.RelationType);
                            AVSDocument.AddRelationToTypedDictionary(relations, dbRelation.RelationType, dbRelation.RelationID);
                            dictionary.Add(dbRelation.RelationID, rowClipboardObject);
                          }
                        }
                        else
                        {
                          dbRelation = relationCollection1.Create(num2, newPart.ObjectID);
                          relationIDs.Add(dbRelation.RelationID);
                          projIDs.Add(dbRelation.ProjID);
                          relTypeIDs.Add(dbRelation.RelationType);
                          AVSDocument.AddRelationToTypedDictionary(relations, dbRelation.RelationType, dbRelation.RelationID);
                          dictionary.Add(dbRelation.RelationID, rowClipboardObject);
                        }
                      }
                      else
                        arrayList2.Add((object) newPart);
                      if (avsRowClipboardCollection != null && rowClipboardObject != null && rowClipboardObject.DocRow != null && avsRowClipboardCollection.DocType == AVSDocumentType.ElementList && dbRelation != null)
                      {
                        List<AttributeValues> attributeValuesList2 = new List<AttributeValues>();
                        for (int index4 = 0; index4 < rowClipboardObject.DocRow.NodesCount; ++index4)
                        {
                          if (rowClipboardObject.DocRow.Nodes[index4] is TextData node && !string.IsNullOrEmpty(node.Text))
                          {
                            if (node.Name == AVSRow.DocAttr_PosDesignation)
                              attributeValuesList2.Add(new AttributeValues(AvsIDCache.Attr_PosDesignation, (object) node.Text));
                            else if (node.Name == AVSRow.DocAttr_Count)
                              attributeValuesList2.Add(new AttributeValues(AvsIDCache.Attr_Count, (object) AVSRow.ConvertCountToMeasuredValue((object) node.Text)));
                            else if (node.Name == AVSRow.DocAttr_Note)
                            {
                              string text;
                              if (AVSRow.ExtractTextBetweenProtectedZones(node as TextBoxElement, out text))
                                text = (string) null;
                              attributeValuesList2.Add(new AttributeValues(this.AVSDocument.Field_Note.AttributeId, (object) text));
                            }
                          }
                        }
                        if (attributeValuesList2.Count > 0)
                          dbRelation.SetAttributesValues(attributeValuesList2.ToArray());
                      }
                      if (flag5)
                      {
                        if (!product.IsCommonData)
                        {
                          if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
                            break;
                        }
                      }
                      else
                        break;
                    }
                    continue;
                  }
                  catch (Exception ex)
                  {
                    ExceptionHelper.ExceptionService.ShowException(ex);
                    continue;
                  }
                }
              }
              if (srcNoteDocRow != null)
              {
                AVSRow avsRow = this.InsertCopyNoteDocRow(contextNode, srcNoteDocRow, false, false);
                if (avsRow != null)
                  avsRowList1.Add(avsRow);
                flag1 = true;
              }
            }
          }
        }
      }
      List<AVSRow> avsRowList2 = new List<AVSRow>();
      List<AVSRow> collection = (List<AVSRow>) null;
      if (!this.avsDocument.IsSpecification)
      {
        collection = this.avsDocument.AddAvsRowParts(arrayList1.ToArray(), -1, contextChapters, false, false);
        if (avsRowClipboardCollection != null && avsRowClipboardCollection.RowList != null)
        {
          for (int index5 = 0; index5 < avsRowClipboardCollection.RowList.Count; ++index5)
          {
            if (avsRowClipboardCollection.RowList[index5] is AvsRowClipboardObject row1 && row1.DocRow != null)
            {
              AVSRow row = (AVSRow) null;
              for (int index6 = 0; index6 < collection.Count; ++index6)
              {
                AVSRow avsRow = collection[index6];
                if (avsRow.ObjectId == row1.ObjectID)
                {
                  if (avsRowList2.Contains(avsRow))
                  {
                    if (avsRow.Section != null)
                    {
                      int num3 = avsRow.Index - index6;
                      if (num3 < 0)
                        num3 = 0;
                      int index7 = num3 + index5;
                      if (index7 > avsRow.Section.Rows.Count)
                        index7 = avsRow.Section.Rows.Count;
                      row = new AVSRow(this.AVSDocument, (RelationAttributeValuesCache) null, avsRow.ObjectAttributesCache);
                      row.DocNode = avsRow.DocNode.Clone() as TableData;
                      avsRow.Section.InsertRow(index7, row);
                      break;
                    }
                    break;
                  }
                  row = avsRow;
                  avsRowList2.Add(avsRow);
                  break;
                }
              }
              if (row != null)
              {
                for (int index8 = 0; index8 < row1.DocRow.NodesCount; ++index8)
                {
                  if (row1.DocRow.Nodes[index8] is TextData node && node.Text != null && node.Text != "")
                  {
                    AvsRowAttributeInfo attrInfoFromCell = AVSDocument.GetAttrInfoFromCell(node, -1, AVSDocument.IsDocumentFormB(avsRowClipboardCollection.DocForm));
                    if (attrInfoFromCell.Equals((AttributeInfo) this.AVSDocument.Field_Note))
                    {
                      row.SetFieldValue(this.AVSDocument.Field_Note, -1, -1, (object) node.Text, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                      flag1 = true;
                    }
                    else if (!AVSDocument.IsDocumentFormB(avsRowClipboardCollection.DocForm) && AVSRow.IsCountField(attrInfoFromCell))
                    {
                      row.SetFieldValue(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AVSRow.DocAttr_Count), -1, -1, (object) node.Text, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                      flag1 = true;
                    }
                    else
                    {
                      row.SetFieldValue(attrInfoFromCell, -1, -1, (object) node.Text, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                      flag1 = true;
                    }
                  }
                }
                string str3 = (string) null;
                if (AVSDocumentsSettings.IsSpecificationDocType(avsRowClipboardCollection.DocType) && this.avsDocument.IsElementList)
                {
                  if (row1.DocRow.Reference is ReferenceToDBObject reference)
                  {
                    using (SessionKeeper sessionKeeper2 = new SessionKeeper())
                    {
                      IDBRelation dbRelation = reference.GetDBRelation(sessionKeeper2.Session, out IDBObject _, this.FiltrationOwnerID);
                      if (dbRelation != null)
                      {
                        IDBAttribute attributeById = dbRelation.GetAttributeByID(AvsIDCache.Attr_PosDesignation);
                        if (attributeById != null)
                          str3 = attributeById.AsString;
                      }
                    }
                  }
                  if (str3 == null)
                    str3 = row1.DocRow.GetAttributeValue(AVSRow.DocAttr_PosDesignation, false);
                  if (str3 != null && str3 != "")
                  {
                    row.SetFieldValue(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AVSRow.DocAttr_PosDesignation), -1, -1, (object) str3, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                    flag1 = true;
                  }
                }
              }
            }
          }
        }
      }
      else
      {
        if (relations.Count > 0)
        {
          collection = this.avsDocument.LoadNewRelations(relations, contextChapters, true);
          foreach (AVSRow avsRow in collection)
          {
            dictionary.TryGetValue(avsRow.RelId, out rowClipboardObject);
            if (rowClipboardObject != null)
            {
              for (int index = 0; index < rowClipboardObject.DocRow.NodesCount; ++index)
              {
                if (rowClipboardObject.DocRow.Nodes[index] is TextData node && node.Name == AVSRow.DocAttr_Format)
                {
                  string str4 = !node.ContainsAttribute(AVSRow.CellAttrName_EditText) ? node.Text : node.GetAttributeValue(AVSRow.CellAttrName_EditText, true);
                  if (str4 != null && str4 != "")
                    avsRow.SetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format), -1, -1, (object) str4, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                }
              }
            }
          }
          avsRowList1.AddRange((IEnumerable<AVSRow>) collection);
        }
        if (arrayList2.Count > 0)
        {
          collection = this.avsDocument.AddAvsRowParts(arrayList2.ToArray(), -1, contextChapters, false, false);
          if (avsRowClipboardCollection != null)
          {
            if (avsRowClipboardCollection.DocType == AVSDocumentType.ElementList)
            {
              for (int index9 = avsRowClipboardCollection.RowList.Count - 1; index9 > -1; --index9)
              {
                if (avsRowClipboardCollection.RowList[index9] is AvsRowClipboardObject row && row.DocRow != null)
                {
                  AVSRow avsRow = (AVSRow) null;
                  for (int index10 = 0; index10 < collection.Count; ++index10)
                  {
                    if (collection[index10].ObjectId == row.ObjectID)
                    {
                      avsRow = collection[index10];
                      collection.RemoveAt(index10);
                      break;
                    }
                  }
                  if (avsRow != null)
                  {
                    for (int index11 = 0; index11 < row.DocRow.NodesCount; ++index11)
                    {
                      if (row.DocRow.Nodes[index11] is TextData node)
                      {
                        if (node.Name == AVSRow.DocAttr_PosDesignation)
                        {
                          if (node.Text != null && node.Text != "")
                            avsRow.SetFieldValue(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AVSRow.DocAttr_PosDesignation), -1, -1, (object) node.Text, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                        }
                        else if (node.Name == AVSRow.DocAttr_Note && node.Text != null && node.Text != "")
                          avsRow.SetFieldValue(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AVSRow.DocAttr_Note), -1, -1, (object) node.Text, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                      }
                    }
                  }
                }
              }
            }
            else if (avsRowClipboardCollection.DocType == AVSDocumentType.Specification)
            {
              for (int index12 = avsRowClipboardCollection.RowList.Count - 1; index12 > -1; --index12)
              {
                if (avsRowClipboardCollection.RowList[index12] is AvsRowClipboardObject row && row.DocRow != null)
                {
                  AVSRow avsRow = (AVSRow) null;
                  for (int index13 = 0; index13 < collection.Count; ++index13)
                  {
                    if (collection[index13].ObjectId == row.ObjectID)
                    {
                      avsRow = collection[index13];
                      if (formParams == null || formParams.NewPart == null)
                      {
                        collection.RemoveAt(index13);
                        break;
                      }
                      break;
                    }
                  }
                  if (avsRow != null)
                  {
                    for (int index14 = 0; index14 < row.DocRow.NodesCount; ++index14)
                    {
                      if (row.DocRow.Nodes[index14] is TextData node && node.Name == AVSRow.DocAttr_Format)
                      {
                        string str5 = !node.ContainsAttribute(AVSRow.CellAttrName_EditText) ? node.Text : node.GetAttributeValue(AVSRow.CellAttrName_EditText, true);
                        if (str5 != null && str5 != "")
                          avsRow.SetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format), -1, -1, (object) str5, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
                      }
                    }
                  }
                }
              }
            }
          }
          if (formParams != null && formParams.NewPart != null)
          {
            for (int index = 0; index < collection.Count; ++index)
            {
              if (!string.IsNullOrEmpty(formParams.NewPart.Format))
                collection[index].SetFieldValue(this.AVSDocument.Field_Format, -1, -1, (object) formParams.NewPart.Format, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
              if (!string.IsNullOrEmpty(formParams.NewPart.Zone))
                collection[index].SetFieldValue(this.AVSDocument.Field_Zone, -1, -1, (object) formParams.NewPart.Zone, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
              if (!string.IsNullOrEmpty(formParams.NewPart.Position))
                collection[index].SetFieldValue(this.AVSDocument.Field_Position, -1, -1, (object) formParams.NewPart.Position, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
              if (!string.IsNullOrEmpty(formParams.NewPart.Quantity))
                collection[index].SetFieldValue(this.AVSDocument.Field_Count, -1, -1, (object) formParams.NewPart.Quantity, false, false, true, this.viewMode == AVSViewMode.Grid, false, false);
              if (!string.IsNullOrEmpty(formParams.NewPart.Remark))
                collection[index].SetFieldValue(this.AVSDocument.Field_Note, -1, -1, (object) formParams.NewPart.Remark, true, false, true, this.viewMode == AVSViewMode.Grid, false, false);
            }
          }
        }
      }
      if (collection != null)
      {
        avsRowList1.AddRange((IEnumerable<AVSRow>) collection);
        if (str1 != null)
        {
          if (collection.Count == 1)
          {
            collection[0].SetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format), -1, -1, (object) str1, false, false, true, this.ViewMode == AVSViewMode.Grid, false, false);
            flag1 = true;
          }
        }
      }
    }
    if (relationIDs != null && relationIDs.Count > 0)
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
    return flag1;
  }

  /// <summary>Проверить заполнение некоторых полей в Спецификации</summary>
  public bool CheckSpecificationBaseFields(bool doFullSpecificationCheck)
  {
    if (this.avsDocument == null)
      return true;
    if (this.DocumentControl != null)
    {
      CancelEventArgs cancelArgs = new CancelEventArgs();
      this.DocumentControl.EditorValidating(cancelArgs);
      if (cancelArgs.Cancel)
        return false;
    }
    bool flag1 = true;
    List<AVSRow> allRows = this.avsDocument.GetAllRows(false, false);
    SpecificationSectionInfo specificationSectionInfo = SpecificationSectionInfo.SectionDictionaryByGuid[(object) new Guid("cad0025d-306c-11d8-b4e9-00304f19f545")] as SpecificationSectionInfo;
    bool flag2 = false;
    bool flag3 = false;
    int relationDocument = AvsIDCache.Relation_Document;
    List<string> stringList = new List<string>();
    for (int index = 0; index < allRows.Count; ++index)
    {
      if (allRows[index].RelType != relationDocument && allRows[index].RelType != AvsIDCache.Relation_AddComplect)
      {
        if (doFullSpecificationCheck && !flag2 && (specificationSectionInfo == null || allRows[index].SectionID != specificationSectionInfo.SectionID))
        {
          string fieldStringValue = allRows[index].GetFieldStringValue(this.avsDocument.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
          object obj1 = SpecificationSectionInfo.SectionDictionaryByID[(object) allRows[index].SectionID];
          if (fieldStringValue == "" || fieldStringValue == null)
          {
            this.DocumentControl.SetSelection((DocumentTreeNode) allRows[index].DocNode, true, false);
            this.Show(this.DockManager, DockState.Document);
            this.Select();
            object obj2 = IMMessageBox.ShowEx("Внимание!", $"В записи с изделием \"{allRows[index].ObjCaption}\" отсутствует позиция.", new IMMessageBoxButton[3]
            {
              new IMMessageBoxButton("Пропустить", DialogResultAdv.Ignore),
              new IMMessageBoxButton("Пропустить все позиции", DialogResultAdv.IgnoreAll),
              new IMMessageBoxButton("Отменить", DialogResult.Cancel)
            });
            IMMessageBoxButton messageBoxButton = obj2 as IMMessageBoxButton;
            if (obj2 == null || messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.Cancel)
            {
              flag1 = false;
              break;
            }
            if (messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.IgnoreAll)
              flag2 = true;
          }
        }
        if (doFullSpecificationCheck && !flag3 && allRows[index].GetFieldValue(this.avsDocument.Field_Count, 0, -1, true, false) == null)
        {
          this.DocumentControl.SetSelection((DocumentTreeNode) allRows[index].DocNode, true, false);
          this.Show(this.DockManager, DockState.Document);
          this.Select();
          object obj = IMMessageBox.ShowEx("Внимание!", $"В записи с изделием \"{allRows[index].ObjCaption}\" отсутствует количество.", new IMMessageBoxButton[3]
          {
            new IMMessageBoxButton("Пропустить", DialogResultAdv.Ignore),
            new IMMessageBoxButton("Пропустить всё количество", DialogResultAdv.IgnoreAll),
            new IMMessageBoxButton("Отменить", DialogResult.Cancel)
          });
          IMMessageBoxButton messageBoxButton = obj as IMMessageBoxButton;
          if (obj == null || messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.Cancel)
          {
            flag1 = false;
            break;
          }
          if (messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.IgnoreAll)
            flag3 = true;
        }
        if (!allRows[index].IsNoteRow && this.avsDocument.AvsDocumentForm == AVSDocumentForm.B && !this.avsDocument.HasCountForAnyProduct(allRows[index]))
          stringList.Add(allRows[index].ObjCaption);
      }
    }
    if (flag1 && stringList.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder("В спецификации содержатся записи со следующими объектами, у которых количество отсутствует во всех исполнениях изделия:");
      stringBuilder.AppendLine();
      stringBuilder.AppendLine();
      foreach (string str in stringList)
        stringBuilder.AppendLine("  " + str);
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("После закрытия спецификации данные объекты будут отсутствовать в дереве состава всех исполнений изделия. Для");
      stringBuilder.AppendLine("добавления объектов в дерево состава требуемых исполнений необходимо заполнить соответствующие графы с указанием");
      stringBuilder.AppendLine("количества.");
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("  Закрыть спецификацию?");
      if (IMMessageBox.Show("AVS", stringBuilder.ToString(), MessageBoxButtons.OKCancel) != DialogResult.OK)
        flag1 = false;
    }
    return flag1;
  }

  /// <summary>Проверить заполнение некоторых полей в Перечне Элементов</summary>
  public bool CheckElementListBaseFields()
  {
    if (this.avsDocument == null)
      return true;
    if (this.DocumentControl != null)
    {
      CancelEventArgs cancelArgs = new CancelEventArgs();
      this.DocumentControl.EditorValidating(cancelArgs);
      if (cancelArgs.Cancel)
        return false;
    }
    bool flag1 = true;
    List<AVSRow> allRows = this.avsDocument.GetAllRows(false, false);
    object obj1 = SpecificationSectionInfo.SectionDictionaryByGuid[(object) new Guid("cad0025d-306c-11d8-b4e9-00304f19f545")];
    bool flag2 = false;
    bool flag3 = false;
    int relationDocument = AvsIDCache.Relation_Document;
    for (int index = 0; index < allRows.Count; ++index)
    {
      if (allRows[index].RelType != relationDocument)
      {
        if (!flag2)
        {
          string fieldStringValue = allRows[index].GetFieldStringValue(this.avsDocument.Field_PosDesignation, -1, -1, (List<RelationAttributeValuesCache>) null, false);
          object obj2 = SpecificationSectionInfo.SectionDictionaryByID[(object) allRows[index].SectionID];
          if (string.IsNullOrWhiteSpace(fieldStringValue))
          {
            this.DocumentControl.SetSelection((DocumentTreeNode) allRows[index].DocNode, true, false);
            object obj3 = IMMessageBox.ShowEx("Внимание!", $"В записи с изделием \"{allRows[index].ObjCaption}\" отсутствует Позиционное обозначение.", new IMMessageBoxButton[3]
            {
              new IMMessageBoxButton("Пропустить", DialogResultAdv.Ignore),
              new IMMessageBoxButton("Пропустить всё Поз. Обозначение", DialogResultAdv.IgnoreAll),
              new IMMessageBoxButton("Отменить", DialogResult.Cancel)
            });
            IMMessageBoxButton messageBoxButton = obj3 as IMMessageBoxButton;
            if (obj3 == null || messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.Cancel)
            {
              flag1 = false;
              break;
            }
            if (messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.IgnoreAll)
              flag2 = true;
          }
        }
        if (!flag3 && allRows[index].GetFieldValue(this.avsDocument.Field_Count, 0, -1, true, false) == null)
        {
          this.DocumentControl.SetSelection((DocumentTreeNode) allRows[index].DocNode, true, false);
          object obj4 = IMMessageBox.ShowEx("Внимание!", $"В записи с изделием \"{allRows[index].ObjCaption}\" отсутствует количество.", new IMMessageBoxButton[3]
          {
            new IMMessageBoxButton("Пропустить", DialogResultAdv.Ignore),
            new IMMessageBoxButton("Пропустить всё Количество", DialogResultAdv.IgnoreAll),
            new IMMessageBoxButton("Отменить", DialogResult.Cancel)
          });
          IMMessageBoxButton messageBoxButton = obj4 as IMMessageBoxButton;
          if (obj4 == null || messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.Cancel)
          {
            flag1 = false;
            break;
          }
          if (messageBoxButton != null && messageBoxButton.MessageResultAdv == DialogResultAdv.IgnoreAll)
            flag3 = true;
        }
      }
    }
    return flag1;
  }

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AVSWindow));
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this._viewsImages = new ImageList(this.components);
    this.viewSwitch1 = new ViewSwitch();
    this._panelMain = new Panel();
    this._panelBottom = new Panel();
    this._splitterBottom = new Splitter();
    this._panelMain.SuspendLayout();
    this.SuspendLayout();
    this.menuBar.Guid = new Guid("5a561fc6-ae3a-4e84-8db4-1f56071bfffb");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Location = new Point(0, 0);
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    this.menuBar.Size = new Size(936, 22);
    this.menuBar.TabIndex = 1;
    this.menuBar.Text = "menuBar";
    this.menuBar.Visible = false;
    this.contextMenuBarItem.CommandName = "contextMenuBarItemAVS";
    this.contextMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
    this.contextMenuBarItem.AfterPopup += new EventHandler(this.contextMenuBarItem_AfterPopup);
    this._viewsImages.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_viewsImages.ImageStream");
    this._viewsImages.TransparentColor = Color.Transparent;
    this._viewsImages.Images.SetKeyName(0, "PageView.gif");
    this._viewsImages.Images.SetKeyName(1, "TableView.gif");
    this.viewSwitch1.ActivePageColor = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 111);
    this.viewSwitch1.ActivepageIndex = 0;
    this.viewSwitch1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.viewSwitch1.AutoSize = true;
    this.viewSwitch1.HlightPageColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
    this.viewSwitch1.ImageIndexes = new int[2]{ 0, 1 };
    this.viewSwitch1.ImageList = this._viewsImages;
    this.viewSwitch1.InactivePageColor = SystemColors.Control;
    this.viewSwitch1.Location = new Point(0, 443);
    this.viewSwitch1.Name = "viewSwitch1";
    this.viewSwitch1.Size = new Size(155, 16 /*0x10*/);
    this.viewSwitch1.TabIndex = 2;
    this.viewSwitch1.ViewsCaptions = new string[2]
    {
      "Страничный",
      "Табличный"
    };
    this.viewSwitch1.ViewsHints = new string[2]
    {
      "Перейти к страничному виду спецификации",
      "Перейти к табличному виду спецификации"
    };
    this.viewSwitch1.OnActivePageChanged += new EventHandler(this.ViewsSwitch_OnActivePageChanged);
    this._panelMain.Controls.Add((Control) this.viewSwitch1);
    this._panelMain.Dock = DockStyle.Fill;
    this._panelMain.Location = new Point(0, 22);
    this._panelMain.Name = "_panelMain";
    this._panelMain.Size = new Size(936, 459);
    this._panelMain.TabIndex = 3;
    this._panelBottom.Dock = DockStyle.Bottom;
    this._panelBottom.Location = new Point(0, 484);
    this._panelBottom.Name = "_panelBottom";
    this._panelBottom.Size = new Size(936, 100);
    this._panelBottom.TabIndex = 4;
    this._panelBottom.Visible = false;
    this._panelBottom.Resize += new EventHandler(this._panelBottom_Resize);
    this._splitterBottom.Dock = DockStyle.Bottom;
    this._splitterBottom.Location = new Point(0, 481);
    this._splitterBottom.MinSize = 100;
    this._splitterBottom.Name = "_splitterBottom";
    this._splitterBottom.Size = new Size(936, 3);
    this._splitterBottom.TabIndex = 5;
    this._splitterBottom.TabStop = false;
    this._splitterBottom.Visible = false;
    this.Controls.Add((Control) this._panelMain);
    this.Controls.Add((Control) this._splitterBottom);
    this.Controls.Add((Control) this._panelBottom);
    this.Controls.Add((Control) this.menuBar);
    this.Name = nameof (AVSWindow);
    this.Size = new Size(936, 584);
    this.Leave += new EventHandler(this.AVSWindow_Leave);
    this.Load += new EventHandler(this.AVSWindow_Load);
    this._panelMain.ResumeLayout(false);
    this._panelMain.PerformLayout();
    this.ResumeLayout(false);
  }

  private void _panelBottom_Resize(object sender, EventArgs e)
  {
    IConfigurationManager configurationManager = AVSPlugin.Instance.ConfigurationManager;
    if (configurationManager == null)
      return;
    (configurationManager.Open("AVS") ?? configurationManager.Create("AVS"))?.SetProperty("panelBottomHeight", this._panelBottom.Height.ToString());
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.DocumentControl != null)
        this.DocumentControl.LockForClosing = true;
      if (this.components != null)
        this.components.Dispose();
      if (this.EventUserControl != null)
      {
        this.DockManagerStorage.DisposeControl((DockControl) this.eventUserControl);
        this.EventUserControl.Dispose();
        this.eventUserControl = (EventUserControl) null;
      }
      if (this.rowPropertyGrid != null)
        this.DockManagerStorage.DisposeControl((DockControl) this.rowPropertyGrid);
      if (this.avsDocument != null)
      {
        this.avsDocument.Dispose();
        this.avsDocument = (AVSDocument) null;
      }
      if (this.specificationToolBar != null)
      {
        if (this.barManager != null)
          this.barManager.RemoveToolbar(this.specificationToolBar);
        this.specificationToolBar.VisibleChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.specificationToolBar.LocationChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.specificationToolBar.ExitMenuLoop -= new EventHandler(this.toolBar_HiddenChanged);
        this.specificationToolBar = (Intermech.Bars.ToolBar) null;
      }
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        service.RendererChanged -= new EventHandler(((ImDocumentEditorForm) this).ToolbarRendererChanged);
        service.CollectToolbars -= new CollectToolbarsHandler(this.barMgr_CollectToolbars);
      }
      this.viewSwitch1.OnActivePageChanged -= new EventHandler(this.ViewsSwitch_OnActivePageChanged);
      this.contextMenuBarItem.BeforePopup -= new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem_BeforePopup);
      this.contextMenuBarItem.AfterPopup -= new EventHandler(this.contextMenuBarItem_AfterPopup);
      this.workCompleteEvent.Close();
      this.contextMenuBarItem.Items.Clear();
      this.contextMenuBarItem.Dispose();
      this.menuBar.Items.Clear();
      this.menuBar.SetPopupMenu((Control) this, (MenuBarItem) null);
      this._navigatorViewServices?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Панель активного раздела в AVS</summary>
  protected virtual StatusBarPanel StatusBarChapterPanel
  {
    [DebuggerStepThrough] get => AVSWindow.sbChapterPanel;
    set => AVSWindow.sbChapterPanel = value;
  }

  private RowPropsDockControl CreateRowPropertyGrid()
  {
    if (this.rowPropertyGrid != null)
    {
      this.rowPropertyGrid.Close();
      this.rowPropertyGrid.Dispose();
      this.rowPropertyGrid = (RowPropsDockControl) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.DockManager != null)
      dockControl = this.DockManager.FindDockControl(RowPropsDockControl.DockGuid);
    if (dockControl == null || !(dockControl is RowPropsDockControl))
    {
      this.rowPropertyGrid = new RowPropsDockControl();
      this.DockManagerStorage.SetControl((DockControl) this.rowPropertyGrid);
      this.rowPropertyGrid.UpdateRows();
    }
    return this.rowPropertyGrid;
  }

  private void SaveRowPropertyGridConfig()
  {
    if (this.suspendSaveDocControlsSettings)
      return;
    try
    {
      this.NeedSaveControlsConfig = true;
      this.SaveControlsConfig();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected override IConfigurationManager ConfigurationManager
  {
    get => AVSPlugin.Instance.ConfigurationManager;
  }

  /// <summary>Открыть панель свойств</summary>
  public void ShowRowPropertyGrid(bool show)
  {
    if (this.ReadOnly)
      return;
    if (this.rowPropertyGrid != null && this.rowPropertyGrid.IsOpen)
    {
      this.rowPropertyGrid.Close();
    }
    else
    {
      if (this.DocumentControl == null)
        return;
      if (this.rowPropertyGrid == null)
        this.CreateRowPropertyGrid();
      else
        this.rowPropertyGrid.UpdateRows();
      if (!show)
        return;
      if (this.rowPropertyGrid != null)
      {
        this.rowpropertyGridSettings = this.DockManagerStorage.GetSettings((DockControl) this.rowPropertyGrid);
        this.rowpropertyGridSettings.Open((DockControl) this.rowPropertyGrid, this.DockManager);
      }
      this.SaveRowPropertyGridConfig();
    }
  }

  /// <summary>Настроить строку статуса под окно</summary>
  /// <param name="statusBar">Строка статуса</param>
  public override void SetStatusBar(StatusBar statusBar)
  {
    if (statusBar == null)
      return;
    if (statusBar.Panels.Count == 0)
      statusBar.Panels.Add(this.StatusBarMessagePanel = new StatusBarPanel());
    else
      this.StatusBarMessagePanel = statusBar.Panels[0];
    if (this.StatusBarPagePanel == null)
    {
      this.StatusBarPagePanel = new StatusBarPanel();
      this.StatusBarPagePanel.Width = 80 /*0x50*/;
      this.StatusBarPagePanel.ToolTipText = "Страница/Страниц";
    }
    if (!statusBar.Panels.Contains(this.StatusBarPagePanel))
      statusBar.Panels.Insert(1, this.StatusBarPagePanel);
    if (this.StatusBarChapterPanel == null)
    {
      this.StatusBarChapterPanel = new StatusBarPanel();
      this.StatusBarChapterPanel.Width = 200;
      this.StatusBarChapterPanel.ToolTipText = "Текущий раздел спецификации";
    }
    if (!statusBar.Panels.Contains(this.StatusBarChapterPanel))
      statusBar.Panels.Insert(1, this.StatusBarChapterPanel);
    this.UpdateSBChapterPanel();
    this.UpdateSBPagePanel();
  }

  /// <summary>Восстановить строку статуса</summary>
  /// <param name="statusBar">Строка статуса</param>
  public override void RestoreStatusBar(StatusBar statusBar)
  {
    if (statusBar != null && this.StatusBarChapterPanel != null)
      statusBar.Panels.Remove(this.StatusBarChapterPanel);
    base.RestoreStatusBar(statusBar);
  }

  /// <summary>Обновить панель страниц в строке статуса</summary>
  public virtual void UpdateSBChapterPanel()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new MethodInvoker(this.UpdateSBChapterPanel));
    }
    else
    {
      if (this.StatusBarChapterPanel == null)
        return;
      if (this.AVSDocument != null)
      {
        DocumentTreeNode[] commandContext = this.GetCommandContext();
        if (commandContext != null && commandContext.Length == 1)
        {
          AVSDocumentContext contextChapters = this.avsDocument.GetContextChapters(commandContext[0]);
          if (contextChapters.Section != null)
            contextChapters.Chapter = (Chapter) contextChapters.Section;
          if (contextChapters.Chapter != null)
          {
            string str = contextChapters.Chapter.Caption;
            if (str.Length > 34)
              str = str.Substring(0, 33) + "…";
            this.StatusBarChapterPanel.Text = str;
            return;
          }
          if (commandContext[0] is RectangleElement rectangleElement && rectangleElement.TopLevelTable != null && rectangleElement.TopLevelTable.FindFirstTable() == this.avsDocument.avsDocTable)
          {
            this.StatusBarChapterPanel.Text = "Спецификация";
            return;
          }
        }
      }
      this.StatusBarChapterPanel.Text = "Не выбран раздел";
    }
  }

  /// <summary>
  /// Переводит окно AVS в режим ожидания завершения пользовательских действий.
  /// </summary>
  public void EnableWorkCompleteMode()
  {
    if (this.InvokeRequired)
      throw new InvalidOperationException("Cross-thread operation not valid: AVS window accessed from a thread other than the thread it was created on.");
    this.workCompleteWaitMode = true;
    if (AVSPlugin.Instance.CommandManager == null)
      return;
    AVSPlugin.Instance.CommandManager.QueryStatus();
  }

  /// <summary>
  /// При включенном режиме ожидания устанавливается в true, когда пользователь завершает работу со спецификацией.
  /// </summary>
  public AutoResetEvent WorkCompleteEvent => this.workCompleteEvent;

  /// <summary>Идентификатор версии объекта документа</summary>
  long IAvsWindow.DocumentID => this.DocumentID;

  /// <summary>Обозначение документа</summary>
  string IAvsWindow.Designation => this.DocumentDesignation;

  /// <summary>Наименование документа</summary>
  string IAvsWindow.DocumentName => this.DocumentName;

  /// <summary>Установить окно в режим только для чтения</summary>
  public void SetReadOnly()
  {
    this.SetViewMode(AVSViewMode.Page);
    this.ReadOnly = true;
    this.DocumentControl.ViewsSwitch.ViewsCaptions = new string[0];
    this.DocumentControl.ViewsSwitch.ViewsHints = new string[0];
  }

  /// <summary>Вызывается, если какой-то объект системы (не обязательно входящий в данную спецификацию) был взят на редактирование </summary>
  internal void ObjectWasCheckedOut(long oldObjectID, long newObjectID)
  {
    if (this.avsDocument != null)
    {
      this.avsDocument.ReplaceObjectID(oldObjectID, newObjectID);
      foreach (AVSRow avsRow in this.avsDocument.GetAvsRowsByObjectId(newObjectID))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          avsRow.SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) sessionKeeper.Session.UserID, false, false, false, true, false, false);
      }
    }
    this.UpdateProductPropertiesPanel(newObjectID);
  }

  /// <summary>Вызывается, если какой-то объект системы (не обязательно входящий в данную спецификацию) был возвращён в архив </summary>
  internal void ObjectWasCheckedIn(long oldObjectID, long newObjectID)
  {
    if (this.avsDocument != null)
    {
      this.avsDocument.ReplaceObjectID(oldObjectID, newObjectID);
      foreach (AVSRow avsRow in this.avsDocument.GetAvsRowsByObjectId(newObjectID))
        avsRow.SetFieldValue(new AvsRowAttributeInfo(false, -6), -1, -1, (object) 0, false, false, false, true, false, false);
    }
    this.UpdateProductPropertiesPanel(newObjectID);
  }

  /// <summary>Вызывается, если какой-то объект системы (не обязательно входящий в данную спецификацию) был возвращён в архив </summary>
  internal void ObjectChangesWasCanceled(long oldObjectID, long newObjectID)
  {
    if (this.avsDocument != null)
    {
      this.avsDocument.ReplaceObjectID(oldObjectID, newObjectID);
      this.avsDocument.ReloadObjectsAttributesFromDB((IList<long>) new long[1]
      {
        newObjectID
      });
    }
    this.UpdateProductPropertiesPanel(newObjectID);
  }

  /// <summary>Заменить идентификатор oldObjectID на newObjectID</summary>
  /// <param name="oldObjectID"></param>
  /// <param name="newObjectID"></param>
  private void ReplaceObjectID(long oldObjectID, long newObjectID)
  {
    if (this.avsDocument != null)
      this.avsDocument.ReplaceObjectID(oldObjectID, newObjectID);
    this.UpdateProductPropertiesPanel(newObjectID);
  }

  /// <summary>Обновить атрибуты связей</summary>
  /// <param name="relationIDs">Идентификаторы связей</param>
  public void RelationsWasChangedHandler(IList<long> relationIDs)
  {
    if (this._relationIDsWithNeedToBeUpdated_FromNotificationService != relationIDs)
      this._relationIDsWithNeedToBeUpdated_FromNotificationService.AddRange((IEnumerable<long>) relationIDs);
    if (!this.Visible)
      return;
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    for (int index = 0; index < relationIDs.Count; ++index)
    {
      AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(relationIDs[index]);
      if (avsDocRow != null)
        AVSDocument.AddRelationToTypedDictionary(dictionary, avsDocRow.RelType, relationIDs[index]);
    }
    try
    {
      this.avsDocument.ReloadRelationsAttributesFromDB(dictionary);
    }
    finally
    {
      this._relationIDsWithNeedToBeUpdated_FromNotificationService.Clear();
    }
  }

  /// <summary>Загрузить новые связи по событию от NotificationService и добавить новые записи</summary>
  /// <param name="createdRelations">Типизированный словарь идентификаторов созданных связей</param>
  public void RelationsWasCreatedHandler(Dictionary<int, List<long>> createdRelations)
  {
    if (this._relationIDsCreated_FromNotificationService != createdRelations)
    {
      if (this._relationIDsCreated_FromNotificationService.Count == 0)
      {
        this._relationIDsCreated_FromNotificationService = createdRelations;
      }
      else
      {
        foreach (KeyValuePair<int, List<long>> createdRelation in createdRelations)
          AVSDocument.AddRelationToTypedDictionary(this._relationIDsCreated_FromNotificationService, createdRelation.Key, (IEnumerable<long>) createdRelation.Value);
      }
    }
    if (!this.Visible)
      return;
    if (this._relationIDsCreated_FromNotificationService.Count == 0)
      return;
    try
    {
      this.AVSDocument.LoadNewRelations(this._relationIDsCreated_FromNotificationService, new AVSDocumentContext(), true);
    }
    finally
    {
      this._relationIDsCreated_FromNotificationService.Clear();
    }
  }

  /// <summary>Что находиться в нижней части окна редактирования спецификации </summary>
  public AVSWindow.enumBottomPanelType BottomPanelType
  {
    get => this._bottomPanelType;
    set
    {
      if (this._bottomPanelType == value)
        return;
      this._bottomPanelType = value;
      switch (value)
      {
        case AVSWindow.enumBottomPanelType.None:
          this._panelBottom.Visible = false;
          this._splitterBottom.Visible = false;
          if (this._productPropertiesUserControl == null)
            break;
          this._productPropertiesUserControl.Visible = false;
          this._productPropertiesUserControl.UpdateViews((ISelectedItems) null);
          this._productPropertiesUserControl.OnClose -= new EventHandler(this._productPropertiesUserControl_OnClose);
          this._productPropertiesUserControl.Parent = (Control) null;
          break;
        case AVSWindow.enumBottomPanelType.SelectedRowProperties:
        case AVSWindow.enumBottomPanelType.ProductsProperties:
        case AVSWindow.enumBottomPanelType.SpecificationProperties:
          IConfigurationManager configurationManager = AVSPlugin.Instance.ConfigurationManager;
          if (configurationManager != null)
          {
            IConfiguration configuration = configurationManager.Open("AVS");
            if (configuration != null)
            {
              string property = configuration.GetProperty("panelBottomHeight");
              int num = 0;
              ref int local = ref num;
              if (int.TryParse(property, out local))
                this._panelBottom.Height = num;
              else
                this._panelBottom.Height = 300;
            }
          }
          this._panelBottom.Visible = true;
          this._splitterBottom.Visible = true;
          this._splitterBottom.Top = this._panelBottom.Top - this._splitterBottom.Height;
          if (this._productPropertiesUserControl == null)
            this._productPropertiesUserControl = new ProductPropertiesUserControl(this);
          this._productPropertiesUserControl.Visible = false;
          this._productPropertiesUserControl.Parent = (Control) this._panelBottom;
          this._productPropertiesUserControl.Dock = DockStyle.Fill;
          this._productPropertiesUserControl.OnClose += new EventHandler(this._productPropertiesUserControl_OnClose);
          this._productPropertiesUserControl.Visible = true;
          this.UpdateProductPropertiesPanel();
          break;
      }
    }
  }

  /// <summary>
  /// Получить выделенные исполнения в форме Б для отображения карточки
  /// </summary>
  /// <returns></returns>
  private List<long> GetSelectedProductsB()
  {
    if (!this.avsDocument.IsSpecification)
      return new List<long>();
    List<AVSRow> avsRowList = new List<AVSRow>();
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    List<long> selectedProductsB = new List<long>();
    switch (this.viewMode)
    {
      case AVSViewMode.Page:
        using (List<DocumentTreeNode>.Enumerator enumerator = this.DocumentControl.SelectedNodes.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            DocumentTreeNode current = enumerator.Current;
            if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
            {
              if (current is TextData textData)
              {
                AVSRow avsDocRow = this.avsDocument.GetAvsDocRow((DocumentTreeNode) textData);
                if (avsDocRow != null)
                {
                  if (avsDocRow.IsFormB)
                  {
                    int indexForCountCell = avsDocRow.GetProductIndexForCountCell(textData);
                    if (indexForCountCell != -1 && indexForCountCell < this.avsDocument.productsInfo.Count && !selectedProductsB.Contains(this.avsDocument.productsInfo[indexForCountCell].Id))
                      selectedProductsB.Add(this.avsDocument.productsInfo[indexForCountCell].Id);
                  }
                }
                else
                {
                  ProductInfo productInfo = (ProductInfo) null;
                  if (this.avsDocument.IsProductNumberCell(current))
                    productInfo = this.avsDocument.GetProductForProductNumberCell(current);
                  else if (this.avsDocument.IsProductKodOrLitera(current))
                    productInfo = this.avsDocument.GetProductForProductKodOrLiteraCell(current);
                  if (productInfo != null && !selectedProductsB.Contains(productInfo.Id))
                    selectedProductsB.Add(productInfo.Id);
                }
              }
            }
            else
            {
              DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(current);
              if (productVariableDocNode != null && (productVariableDocNode as TableData).Tag is ProductVariableDataChapter tag)
                selectedProductsB.Add(tag.ChapterID);
            }
          }
          break;
        }
      case AVSViewMode.Grid:
        IEnumerator enumerator1 = this.virtualTree.Selection.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            IVirtualTreeItem current = (IVirtualTreeItem) enumerator1.Current;
            if ((this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && this.avsDocument.productsInfo.Count > 1 && this.virtualTree.Selection.Count == 1)
            {
              if (this.virtualTree.FocusedColumn != null)
              {
                ColumnTag tag = this.virtualTree.FocusedColumn.Tag;
                if (tag != null && AVSRow.IsCountField(tag.SpecRowAttributeInfo) && tag.ProductIndex != -1 && tag.ProductIndex < this.avsDocument.productsInfo.Count && !selectedProductsB.Contains(this.avsDocument.productsInfo[tag.ProductIndex].Id))
                  selectedProductsB.Add(this.avsDocument.productsInfo[tag.ProductIndex].Id);
              }
            }
            else
            {
              ProductInfo product = this.avsDocument.GetProduct(current);
              if (product != null && product.IsCommonData)
                product = this.avsDocument.productsInfo.Count != 1 ? (ProductInfo) null : this.avsDocument.productsInfo[0];
              if (product != null && !selectedProductsB.Contains(product.Id))
                selectedProductsB.Add(product.Id);
            }
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
    }
    return selectedProductsB;
  }

  /// <summary>Обновить панель свойств</summary>
  private void UpdateProductPropertiesPanel()
  {
    if (this.rowPropertyGrid != null && this.rowPropertyGrid.Visible)
      this.rowPropertyGrid.UpdateRows();
    if (this._productPropertiesUserControl == null || !this._productPropertiesUserControl.Visible)
      return;
    List<AVSRow> rows = new List<AVSRow>();
    List<RelationAttributeValuesCache> relationIds = new List<RelationAttributeValuesCache>();
    ISelectedItems items = (ISelectedItems) null;
    List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();
    List<long> longList = new List<long>();
    string str = "";
    switch (this.BottomPanelType)
    {
      case AVSWindow.enumBottomPanelType.SelectedRowProperties:
        if (this.ReadOnly && !this.AVSDocument.DataLoaded)
        {
          nodes = AVSSelectedItemsHelper.GetSelectedNodes(this, false, false);
        }
        else
        {
          switch (this.viewMode)
          {
            case AVSViewMode.Page:
              using (List<DocumentTreeNode>.Enumerator enumerator = this.DocumentControl.SelectedNodes.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  DocumentTreeNode current = enumerator.Current;
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(current);
                  if (avsDocRow != null && !rows.Contains(avsDocRow))
                    rows.Add(avsDocRow);
                  DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(current);
                  if (productVariableDocNode != null)
                  {
                    Chapter tag = (productVariableDocNode as TableData).Tag as Chapter;
                    if (tag is ProductVariableDataChapter)
                    {
                      if (!this.avsDocument.IsSpecification && tag.ChapterID.IsUndefinedId())
                      {
                        if (!longList.Contains(this.AVSDocument.DocumentID))
                          longList.Add(this.AVSDocument.DocumentID);
                      }
                      else
                        longList.Add(tag.ChapterID);
                    }
                  }
                  else if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
                  {
                    ProductInfo productInfo = (ProductInfo) null;
                    if (this.avsDocument.IsProductNumberCell(current))
                      productInfo = this.avsDocument.GetProductForProductNumberCell(current);
                    else if (this.avsDocument.IsProductKodOrLitera(current))
                      productInfo = this.avsDocument.GetProductForProductKodOrLiteraCell(current);
                    if (productInfo != null && !longList.Contains(productInfo.Id))
                    {
                      if (!this.avsDocument.IsSpecification)
                        longList.Add(0L);
                      else
                        longList.Add(productInfo.Id);
                    }
                  }
                }
                break;
              }
            case AVSViewMode.Grid:
              IEnumerator enumerator1 = this.virtualTree.Selection.GetEnumerator();
              try
              {
                while (enumerator1.MoveNext())
                {
                  IVirtualTreeItem current = (IVirtualTreeItem) enumerator1.Current;
                  AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(this.virtualTree.FindRow((object) current));
                  if (avsDocRow != null && !rows.Contains(avsDocRow))
                  {
                    rows.Add(avsDocRow);
                  }
                  else
                  {
                    ProductInfo product = this.avsDocument.GetProduct(current);
                    if (product != null && product.IsCommonData)
                      product = this.avsDocument.productsInfo.Count != 1 ? (ProductInfo) null : this.avsDocument.productsInfo[0];
                    if (product != null)
                    {
                      if (!this.avsDocument.IsSpecification)
                        longList.Add(0L);
                      else
                        longList.Add(product.Id);
                    }
                  }
                }
                break;
              }
              finally
              {
                if (enumerator1 is IDisposable disposable)
                  disposable.Dispose();
              }
            default:
              return;
          }
        }
        if (this._navigatorViewServices == null)
        {
          this._navigatorViewServices = new ServiceContainer();
          this._navigatorViewServices.AddService(typeof (IViewState), (object) new ViewStateService());
          this._navigatorViewServices.AddService(typeof (IAVSViewsService), (object) new AVSViewsService(this));
        }
        str = "Свойства выбранных записей";
        if (rows.Count > 0)
        {
          if (rows.Count == 1)
          {
            List<long> selectedProductsB = this.GetSelectedProductsB();
            if (selectedProductsB.Count > 0)
            {
              int relationIndexForProduct = rows[0].GetRelationIndexForProduct(selectedProductsB[0]);
              if (relationIndexForProduct >= 0)
              {
                RelationAttributeValuesCache relation = rows[0].Relations[relationIndexForProduct];
                if (relation != null)
                  relationIds.Add(relation);
              }
            }
          }
          items = AVSSelectedItemsHelper.GetSelectedItems(rows, (System.IServiceProvider) this._navigatorViewServices, relationIds);
          if (items == null)
          {
            items = ObjectExtensions.GetItems(this.DocumentID);
            break;
          }
          break;
        }
        if (longList.Count == 0)
        {
          if (nodes.Count > 0)
          {
            items = AVSSelectedItemsHelper.GetSelectedItems(this, nodes, (System.IServiceProvider) this._navigatorViewServices, false);
            break;
          }
          items = ObjectExtensions.GetItems(this.DocumentID);
          str = "Свойства спецификации";
          break;
        }
        items = ObjectExtensions.GetItems(longList[0]);
        str = "Свойства выбраных исполнений";
        break;
      case AVSWindow.enumBottomPanelType.ProductsProperties:
        switch (this.viewMode)
        {
          case AVSViewMode.Page:
            using (List<DocumentTreeNode>.Enumerator enumerator2 = this.DocumentControl.SelectedNodes.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                DocumentTreeNode current = enumerator2.Current;
                if (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
                {
                  if (current is TextData textData)
                  {
                    AVSRow avsDocRow = this.avsDocument.GetAvsDocRow((DocumentTreeNode) textData);
                    if (avsDocRow != null)
                    {
                      if (avsDocRow.IsFormB)
                      {
                        int indexForCountCell = avsDocRow.GetProductIndexForCountCell(textData);
                        if (indexForCountCell != -1 && indexForCountCell < this.avsDocument.productsInfo.Count && !longList.Contains(this.avsDocument.productsInfo[indexForCountCell].Id))
                          longList.Add(this.avsDocument.productsInfo[indexForCountCell].Id);
                      }
                    }
                    else
                    {
                      ProductInfo productInfo = (ProductInfo) null;
                      if (this.avsDocument.IsProductNumberCell(current))
                        productInfo = this.avsDocument.GetProductForProductNumberCell(current);
                      else if (this.avsDocument.IsProductKodOrLitera(current))
                        productInfo = this.avsDocument.GetProductForProductKodOrLiteraCell(current);
                      if (productInfo != null && !longList.Contains(productInfo.Id))
                        longList.Add(productInfo.Id);
                    }
                  }
                }
                else
                {
                  DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(current);
                  if (productVariableDocNode != null)
                  {
                    if ((productVariableDocNode as TableData).Tag is ProductVariableDataChapter tag)
                    {
                      longList.Add(tag.ChapterID);
                    }
                    else
                    {
                      INodeWithReference nodeWithReference = productVariableDocNode as INodeWithReference;
                      if (nodeWithReference != null && nodeWithReference.Reference is ReferenceToDBObject reference)
                      {
                        reference.UpdateDBObjectInfo();
                        longList.Add(reference.DBObjectID);
                      }
                    }
                  }
                }
              }
              break;
            }
          case AVSViewMode.Grid:
            IEnumerator enumerator3 = this.virtualTree.Selection.GetEnumerator();
            try
            {
              while (enumerator3.MoveNext())
              {
                IVirtualTreeItem current = (IVirtualTreeItem) enumerator3.Current;
                if ((this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && this.avsDocument.productsInfo.Count > 1 && this.virtualTree.Selection.Count == 1)
                {
                  ColumnTag tag = this.virtualTree.FocusedColumn.Tag;
                  if (tag != null && AVSRow.IsCountField(tag.SpecRowAttributeInfo) && tag.ProductIndex != -1 && tag.ProductIndex < this.avsDocument.productsInfo.Count && !longList.Contains(this.avsDocument.productsInfo[tag.ProductIndex].Id))
                    longList.Add(this.avsDocument.productsInfo[tag.ProductIndex].Id);
                }
                else
                {
                  ProductInfo product = this.avsDocument.GetProduct(current);
                  if (product != null && product.IsCommonData)
                    product = this.avsDocument.productsInfo.Count != 1 ? (ProductInfo) null : this.avsDocument.productsInfo[0];
                  if (product != null && !longList.Contains(product.Id))
                    longList.Add(product.Id);
                }
              }
              break;
            }
            finally
            {
              if (enumerator3 is IDisposable disposable)
                disposable.Dispose();
            }
          default:
            return;
        }
        if (this._navigatorViewServices == null)
        {
          this._navigatorViewServices = new ServiceContainer();
          this._navigatorViewServices.AddService(typeof (IViewState), (object) new ViewStateService());
          this._navigatorViewServices.AddService(typeof (IAVSViewsService), (object) new AVSViewsService(this));
        }
        if (longList.Count == 0)
        {
          if (this.avsDocument.productsInfo.Count == 1)
            items = ObjectExtensions.GetItems(this.avsDocument.productsInfo[0].Id);
        }
        else
          items = ObjectExtensions.GetItems(longList[0]);
        str = "Свойства выбраных исполнений";
        break;
      case AVSWindow.enumBottomPanelType.SpecificationProperties:
        items = ObjectExtensions.GetItems(this.DocumentID);
        str = "Свойства спецификации";
        break;
    }
    this._productPropertiesUserControl.UpdateViews(items);
    this._productPropertiesUserControl.Text = str;
  }

  /// <summary>Обновить панель свойств если там свойства заданного объекта</summary>
  /// <param name="objectID">Ид версии объекта</param>
  private void UpdateProductPropertiesPanel(long objectID)
  {
    if (this._productPropertiesUserControl == null || !this._productPropertiesUserControl.Visible)
      return;
    List<long> longList = new List<long>();
    ISelectedItems items = (ISelectedItems) null;
    bool flag = false;
    switch (this.BottomPanelType)
    {
      case AVSWindow.enumBottomPanelType.SelectedRowProperties:
        this.UpdateProductPropertiesPanel();
        return;
      case AVSWindow.enumBottomPanelType.ProductsProperties:
        for (int index = 0; index < this.avsDocument.productsInfo.Count; ++index)
          flag |= this.avsDocument.productsInfo[index].Id == objectID;
        if (!flag)
          return;
        this.UpdateProductPropertiesPanel();
        return;
      case AVSWindow.enumBottomPanelType.SpecificationProperties:
        if (this.DocumentID != objectID)
          return;
        items = ObjectExtensions.GetItems(this.DocumentID);
        break;
    }
    this._productPropertiesUserControl.UpdateViews(items);
  }

  /// <summary>Обработчик события вызываемый при закрытии панеля свойств</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _productPropertiesUserControl_OnClose(object sender, EventArgs e)
  {
    try
    {
      if (this.virtualTree == null)
        return;
      this.BottomPanelType = AVSWindow.enumBottomPanelType.None;
      if (this.DocumentManager == null || this.DocumentManager.CommandManager == null)
        return;
      this.DocumentManager.CommandManager.QueryStatus();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  bool IMessageFilter.PreFilterMessage(ref Message m)
  {
    try
    {
      if (this.Visible && this.Manager != null && this.Manager.ActiveDocument == this && Form.ActiveForm == this.Manager.OwnerForm && (m.Msg == 256 /*0x0100*/ || m.Msg == 260))
      {
        Keys shortcut = (Keys) (int) m.WParam | Control.ModifierKeys;
        if (this._hotKeysManager != null && this.ContextMenuBarItem != null)
        {
          List<IHotKeysCommand> hotKeysCommandList = this._hotKeysManager[shortcut];
          if (hotKeysCommandList != null && hotKeysCommandList.Count > 0)
          {
            if (!(this.ContextMenuBarItem.FindItem("AVS.NavigatorCommands") is MenuButtonItem contextMenuItem))
              contextMenuItem = NodeContextMenu.GetContextMenuItem("AVS.NavigatorCommands");
            if (contextMenuItem != null)
            {
              foreach (IHotKeysCommand hotKeysCommand in hotKeysCommandList)
              {
                MenuItemBase itemRecursive = this.FindItemRecursive((MenuItemBase) contextMenuItem, hotKeysCommand.Command);
                if (itemRecursive != null && itemRecursive.Enabled && itemRecursive.Visible)
                {
                  itemRecursive.PerformClick();
                  this.UpdateNavigatorMenu(false);
                  return true;
                }
              }
            }
          }
        }
      }
      return false;
    }
    catch
    {
      return false;
    }
  }

  private MenuItemBase FindItemRecursive(MenuItemBase item, string commandName)
  {
    foreach (MenuItemBase itemRecursive1 in (CollectionBase) item.Items)
    {
      if (itemRecursive1.CommandName == commandName)
        return itemRecursive1;
      MenuItemBase itemRecursive2 = this.FindItemRecursive(itemRecursive1, commandName);
      if (itemRecursive2 != null)
        return itemRecursive2;
    }
    return (MenuItemBase) null;
  }

  public override string HelpID => "1492";

  public new bool CanBeOpenedInNewWindowsAsObject
  {
    get
    {
      AVSDocument avsDocument = this.avsDocument;
      return (avsDocument != null ? avsDocument.DocumentID : -1L).IsDefinedId();
    }
  }

  public new void OpenNewInstanceAsObject()
  {
    AVSPlugin.DoOpenInNewWindowCommand(new long[1]
    {
      this.avsDocument.DocumentID
    });
  }

  /// <summary>Режим работы окна поиска и замены</summary>
  private enum FindOperation
  {
    Find,
    Replace,
    ReplaceAll,
  }

  /// <summary>Направление движения</summary>
  private enum MoveDirrection
  {
    Top,
    Bottom,
    Left,
    Right,
  }

  /// <summary>Вспомогательный класс для упорядочивания списка направлений поиска</summary>
  private class MoveDirrectionSorter : IComparable<AVSWindow.MoveDirrectionSorter>
  {
    /// <summary>Направление</summary>
    public AVSWindow.MoveDirrection MoveDirrection;
    /// <summary>Шаг</summary>
    public int Step;

    /// <summary>Конструктор</summary>
    /// <param name="moveDirrection">Направление</param>
    /// <param name="step">Шаг</param>
    public MoveDirrectionSorter(AVSWindow.MoveDirrection moveDirrection, int step)
    {
      this.MoveDirrection = moveDirrection;
      this.Step = step;
    }

    /// <summary>Сравнить с другим значением</summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(AVSWindow.MoveDirrectionSorter other)
    {
      if (other == null)
        return 0;
      if (this.Step > other.Step)
        return 1;
      return this.Step != other.Step ? -1 : 0;
    }
  }

  private enum ReplaceRowMode
  {
    ReplaceObject,
    ReplaceObjectFromImbase,
    ReplaceVersion,
  }

  /// <summary>Тип, описывающий что находиться внизу окна редактирования спецификации </summary>
  public enum enumBottomPanelType
  {
    /// <summary>Ничего не выбрано</summary>
    None,
    /// <summary>Свойства выбраной записи</summary>
    SelectedRowProperties,
    /// <summary>Свойства выбранного исполнения</summary>
    ProductsProperties,
    /// <summary>Свойства конструкторского документа</summary>
    SpecificationProperties,
  }
}
