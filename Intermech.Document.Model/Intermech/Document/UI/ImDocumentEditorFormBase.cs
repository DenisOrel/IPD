// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ImDocumentEditorFormBase
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Controls.SpellCheck;
using Intermech.Docking;
using Intermech.Document.Model;
using Intermech.Document.Model.FindReplace;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.StandaloneView;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Окно документа в редакторе</summary>
public class ImDocumentEditorFormBase : DockControl, ICommandTarget, ICommandTarget2, IUndo
{
  private IUndoManager undoManager;
  private FindReplaceManager findReplaceManager;
  private IExternalEditor externalEditor;
  protected bool suspendSaveDocControlsSettings;
  public DocumentMenuHelper menuHelper;
  private bool isTemplate;
  private ErrorsUserControl errorsUserControl;
  private bool disposeDocumentOnClose = true;
  private int _lockDocumentUpdateCounter;
  private bool _needUpdateTree;
  private int _lockPageKeysCounter;
  /// <summary>Режим просмотра документа</summary>
  private Intermech.Interfaces.Document.DocumentViewMode? DocumentViewMode;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private const int MM_ANISOTROPIC = 8;
  protected bool _IsQueryChacheIsInit;
  protected List<DocumentTreeNode> _queryDocumentNodeList;
  protected List<DocumentTreeNode> _queryStatusContext;
  private List<DocumentTreeNode> _oldQueryStatusContext;
  private bool _queryContextWasChanged;
  private bool _queryStatusFormatText;
  private bool _queryStatusFormatCharFormat;
  private ImRtfEditor _queryTern;
  private string _queryFontFamily;
  private float? _queryFontSize;
  private bool _queryIsFontAutoSize;
  /// <summary>Курсор в активном редакторе находится в защищённой зоне</summary>
  protected bool _queryIsProtectedZone;
  /// <summary>Курсор в активном редакторе находится на формуле</summary>
  private bool _queryIsFormula;
  private bool _queryIsAllTextSelected = true;
  private int _queryFirstLineSelection = -1;
  private int _queryFirstColSelection = -1;
  private int _queryEndLineSelection = -1;
  private int _queryEndColSelection = -1;
  private int? _queryLeftIndent = new int?(-1);
  private int? _queryRigthIndent = new int?(-1);
  private int? _queryFirstIndent = new int?(-1);
  private int _queryFlags = -1;
  private int? _queryLineSpacing = new int?(-1);
  private int? _querySpaceBefore = new int?(-1);
  private int? _querySpaceAfter = new int?(-1);
  private int? _querySpaceBetween = new int?(-9999);
  private bool? _queryDisableFloatLines;
  private bool? _queryDisableWordWrap;
  private bool? _queryKeepTogether;
  private bool? _queryKeepWithNext;
  private bool? _queryFromNewPage;
  private bool _queryBordersLeft;
  private bool _queryBordersTop;
  private bool _queryBordersRight;
  private bool _queryBordersBottom;
  private bool? _queryBordersHorisontal = new bool?(false);
  private bool? _queryBordersVertical = new bool?(false);
  private Color? _queryTextColor = new Color?(Color.Black);
  private Color? _queryTextBkColor = new Color?(Color.White);
  private Color? _queryULColor = new Color?(Color.Black);
  private HorzAlignment? _queryHorzAlignment;
  private string _queryTypeface = "Arial";
  private int? _queryPointSize = new int?(12);
  private int? _queryStyles = new int?(0);
  private CharFormat _queryCharFormatforStyles;
  private ParagraphFormat _queryParagraphFormatforStyles;
  private int? _lastSetLineSpacing = new int?(50);
  private int _fontChangeLockCounter;
  private static VertAlignment _lastVertAlignment;
  private static HorzAlignment _lastHorzAlignment;
  private string defaultFileExtension = ".imdx";
  protected string defaultFileName;
  private string documentCaption;
  private IImDocumentManager documentManager;
  private DocumentControl documentControl;
  private string recentlySaveAsFileName;
  private bool recentlyPackedFile;
  private SaveFileDialog saveToFileDialog;
  private ImDocumentDockManagerStorage dockManagerStorage;
  private bool needSaveControlsConfig;
  protected static StatusBarPanel sbMessagePanel;
  protected static StatusBarPanel sbCursorCoorPanel;
  protected static StatusBarPanel sbPagePanel;

  /// <summary>Заголовок документа</summary>
  public virtual string DocumentCaption
  {
    [DebuggerStepThrough] get
    {
      if (this.documentCaption != null)
        return this.documentCaption;
      return this.Document != null ? this.Document.GetDefautCaption() : LocalizationHolder.rm.GetString("Document.Model_66");
    }
    set
    {
      if (!(this.documentCaption != value))
        return;
      this.documentCaption = value;
      this.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Обновить заголовок окна документа</summary>
  public virtual void UpdateDocumentWindowCaption()
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new MethodInvoker(this.UpdateDocumentWindowCaption));
    else
      this.Text = this.FormatDocWindowCaption();
  }

  /// <summary>Сформировать текст заголовка окна документа</summary>
  /// <returns></returns>
  public virtual string FormatDocWindowCaption()
  {
    bool flag1 = false;
    bool flag2 = false;
    if (this.Document != null)
    {
      flag1 = this.Document.IsTemplate;
      flag2 = !flag1 || this.Document.TemplateOwner == null ? this.Document.Modified && !this.ReadOnly : this.Document.TemplateOwner.Modified && !this.ReadOnly;
    }
    string str = this.DocumentCaption;
    if (str.Length > 70)
      str = str.Substring(0, 67) + "...";
    return str + (flag1 ? LocalizationHolder.rm.GetString("Document.Model_67") : "") + (this.ReadOnly ? LocalizationHolder.rm.GetString("Document.Model_68") : "") + (flag2 ? "*" : "");
  }

  /// <summary>Выбрать окно документа</summary>
  /// <param name="directed"></param>
  /// <param name="forward"></param>
  protected override void Select(bool directed, bool forward)
  {
    try
    {
      base.Select(directed, forward);
      if (this.DocumentControl == null || this.DocumentControl.PageControl == null)
        return;
      this.DocumentControl.PageControl.Select();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnEnter(EventArgs e) => base.OnEnter(e);

  /// <summary>Режим только для чтения</summary>
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.documentControl == null || this.documentControl.ReadOnly;
    set
    {
      if (this.documentControl == null || this.ReadOnly == value)
        return;
      this.documentControl.ReadOnly = value;
      if (this.CommandManager == null)
        return;
      this.CommandManager.QueryStatus();
    }
  }

  /// <summary>Менеджер команд</summary>
  public ICommandManager CommandManager
  {
    [DebuggerStepThrough] get
    {
      return this.documentManager != null ? this.documentManager.CommandManager : (ICommandManager) null;
    }
  }

  public FindReplaceManager FindReplaceManager => this.findReplaceManager;

  [Browsable(false)]
  public virtual IUndoManager UndoManager
  {
    get => this.undoManager;
    set => this.undoManager = value;
  }

  [Browsable(false)]
  public virtual IExternalEditor ExternalEditor
  {
    get => this.externalEditor;
    set => this.externalEditor = value;
  }

  /// <summary>
  /// DockManager используемый для отображения встроенных форм редактора документов
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual DockManager DockManager => this.Manager;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ImDocumentDockManagerStorage DockManagerStorage
  {
    get => this.dockManagerStorage;
    set => this.dockManagerStorage = value;
  }

  /// <summary>
  /// Было изменено положение контролов, необходимо сохранить
  /// </summary>
  [Browsable(false)]
  public bool NeedSaveControlsConfig
  {
    get => this.needSaveControlsConfig;
    set => this.needSaveControlsConfig = value;
  }

  protected internal virtual void SaveControlsConfig()
  {
  }

  /// <summary>Объект управлющий окнами документов</summary>
  public IImDocumentManager DocumentManager
  {
    [DebuggerStepThrough] get => this.documentManager;
    set
    {
      this.documentManager = value;
      if (this.documentControl == null)
        return;
      this.documentControl.DocumentManager = this.documentManager;
    }
  }

  public virtual DocumentMenuHelper CreateDocumentMenuHelper() => (DocumentMenuHelper) null;

  public void AddToolbar(BarManager barManager, Intermech.Bars.ToolBar toolbar, DockStyle style)
  {
    int dockLine = toolbar.DockLine;
    int dockOffset = toolbar.DockOffset;
    barManager.AddToolbar(toolbar, style);
    toolbar.DockLine = dockLine;
    toolbar.DockOffset = dockOffset;
  }

  public virtual DocumentMenuHelper MenuHelper
  {
    get => this.menuHelper == null ? DocumentMenuHelper.Instance : this.menuHelper;
    set => this.menuHelper = value;
  }

  /// <summary>Основной документ.
  /// Если это окно открыто для внутреннего шаблона, то возвращается документ, владеющий шаблоном</summary>
  public ImDocument MainImDocument
  {
    get
    {
      if (this.Document == null)
        return (ImDocument) null;
      return this.IsInternalDocumentTemplate ? (ImDocument) this.Document.TemplateOwner : this.Document;
    }
  }

  public bool IsDocumentTemplate
  {
    get => this.Document != null ? this.Document.IsTemplate : this.isTemplate;
    set => this.isTemplate = value;
  }

  public bool IsInternalDocumentTemplate => this.Document?.TemplateOwner != null;

  /// <summary>
  /// Ссылка на зависимое окно в котором открыт внутренний шаблон документа
  /// </summary>
  public ImDocumentEditorFormBase InternalDocumentTemplateWindow
  {
    get
    {
      return this.Document?.DocumentTemplate is ImDocument documentTemplate ? (ImDocumentEditorFormBase) documentTemplate.DocumentControl?.Parent : (ImDocumentEditorFormBase) null;
    }
  }

  /// <summary>Собственно документ</summary>
  public ImDocument Document
  {
    [DebuggerStepThrough] get
    {
      return this.documentControl != null ? this.documentControl.Document : (ImDocument) null;
    }
  }

  /// <summary>Комплект документов</summary>
  public DocumentsComplect DocumentsComplect
  {
    [DebuggerStepThrough] get
    {
      return this.documentControl != null ? this.documentControl.DocumentsComplect : (DocumentsComplect) null;
    }
  }

  /// <summary>Контрол для отображения ошибок</summary>
  public ErrorsUserControl ErrorsUserControl
  {
    get
    {
      if (this.errorsUserControl == null)
      {
        this.errorsUserControl = new ErrorsUserControl(this.DockManager, this);
        this.DockManagerStorage.SetControl((DockControl) this.errorsUserControl, DockLocation.Bottom);
      }
      return this.errorsUserControl;
    }
  }

  /// <summary>Элемент управления документа</summary>
  public virtual DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.documentControl;
  }

  public void SuspendDocumentUpdates()
  {
    if (this.Document == null)
      return;
    this.Document.SuspendUpdateGeometryRefreshUI();
    this.Document.SuspendUpdateLayout();
  }

  public void ResumeDocumentUpdates()
  {
    if (this.Document == null)
      return;
    this.Document.ResumeUpdateLayout(0, false, true);
    this.Document.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary> Вызывается если требуется заблокировать визуальное представление документа </summary>
  protected virtual bool OnLockUpdate()
  {
    try
    {
      int num = this.Document == null ? 1 : (!this.Document.SuspendedUpdateUIGeometryFlag ? 0 : (this.Document.SuspendedUpdateLayoutFlag ? 1 : 0));
      if (num == 0)
      {
        this.Document.SuspendUpdateGeometryRefreshUI();
        this.Document.SuspendUpdateLayout();
      }
      return num == 0;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  /// <summary> Блокировать все автоматические обновления в визуальном представлении документа </summary>
  /// <returns>true - если блокировка выполнена, false - если документ уже был заблокирован</returns>
  public virtual bool LockVisualUpdates()
  {
    bool flag = false;
    Thread.BeginCriticalRegion();
    if (this._lockDocumentUpdateCounter == 0)
      flag = this.OnLockUpdate();
    Interlocked.Increment(ref this._lockDocumentUpdateCounter);
    return flag;
  }

  /// <summary> Убрать все вызовы, заменить на вызовы LockVisualUpdates () </summary>
  /// <returns></returns>
  public bool LockDocumentUpdates() => this.LockVisualUpdates();

  /// <summary> Вызывается если требуется разблокировать визуальное представление документа </summary>
  /// <param name="update"> true если требуется обновить представление </param>
  protected virtual void OnUnlockUpdate(bool update)
  {
    try
    {
      if (this.Document == null)
        return;
      this.Document.ResumeUpdateLayout(false, update);
      this.Document.ResumeUpdateRefreshUI(update, update);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary> Разблокировать все автоматические обновления в визуальном представлении документа </summary>
  /// <param name="update">Вызвать обновление</param>
  public virtual void UnlockVisualUpdates(bool update)
  {
    this._needUpdateTree |= update;
    if (this._lockDocumentUpdateCounter > 0)
      Interlocked.Decrement(ref this._lockDocumentUpdateCounter);
    if (this._lockDocumentUpdateCounter == 0)
    {
      this.OnUnlockUpdate(this._needUpdateTree);
      this._needUpdateTree = false;
    }
    Thread.EndCriticalRegion();
  }

  /// <summary>Убрать все вызовы, заменить на вызовы UnlockVisualUpdates()</summary>
  public void UnlockDocumentUpdates(bool update) => this.UnlockVisualUpdates(update);

  /// <summary>LockPageKeys</summary>
  protected void LockPageKeys() => ++this._lockPageKeysCounter;

  /// <summary>UnlockPageKeys</summary>
  protected void UnlockPageKeys()
  {
    if (this._lockPageKeysCounter <= 0)
      return;
    --this._lockPageKeysCounter;
  }

  /// <summary>Перехватить обработку клавиш</summary>
  /// <param name="msg">Сообщение</param>
  /// <param name="keyData">Клавиши</param>
  /// <returns>true, если клавиша не нуждается в дальнейшей обработке</returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    try
    {
      if (this.DocumentControl != null && this._lockPageKeysCounter == 0)
      {
        switch (keyData)
        {
          case Keys.Prior:
            this.DocumentControl.ProcessPageUp();
            return true;
          case Keys.Next:
            this.DocumentControl.ProcessPageDown();
            return true;
          case Keys.Prior | Keys.Control:
            this.DocumentControl.ProcessCtrlPageUp();
            return true;
          case Keys.Next | Keys.Control:
            this.DocumentControl.ProcessCtrlPageDown();
            return true;
          case Keys.B | Keys.Control:
            ICommandState command1 = this.CommandManager.FindCommand("Format.Font.TextBold");
            if (command1 != null && command1.Enabled)
              this.Execute(command1);
            this.UpdateFormatCommands();
            return true;
          case Keys.I | Keys.Control:
            ICommandState command2 = this.CommandManager.FindCommand("Format.Font.TextCursive");
            if (command2 != null && command2.Enabled)
              this.Execute(command2);
            this.UpdateFormatCommands();
            return true;
          case Keys.U | Keys.Control:
            ICommandState command3 = this.CommandManager.FindCommand("Format.Font.TextUnderline");
            if (command3 != null && command3.Enabled)
              this.Execute(command3);
            this.UpdateFormatCommands();
            return true;
        }
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  /// <summary>Вызывается при назначении нового документа</summary>
  public virtual void OnDocumentChanged()
  {
  }

  /// <summary>Назначить DocumentControl</summary>
  /// <param name="value">Значение DocumentControl</param>
  public virtual void AssignDocumentControl(DocumentControl value)
  {
    if (this.documentControl != null)
    {
      this.documentControl.Parent = (Control) null;
      this.documentControl.ActivePageChanged -= new ActivePageChanged_EventHandler(this.documentControl_ActivePageChanged);
      this.documentControl.PageCursorPositionChanged -= new PageCursorPositionChanged_EventHandler(this.documentControl_PageCursorPositionChanged);
      this.documentControl.GetCustomElementContextMenu -= new GetCustomElementContextMenu_EventHandler(this.GetDocumentElementContextMenu);
    }
    this.documentControl = value;
    if (this.documentControl != null)
    {
      if (StandaloneViewVars.AdjustSettingsInDialogMode.IsDeclared && StandaloneViewVars.AdjustSettingsInDialogMode.Value)
      {
        if (!this.DocumentViewMode.HasValue)
        {
          this.DocumentViewMode = new Intermech.Interfaces.Document.DocumentViewMode?(Intermech.Interfaces.Document.DocumentViewMode.Normal);
          this.DocumentViewMode = new Intermech.Interfaces.Document.DocumentViewMode?(ViewWithForm.Execute(this.DocumentViewMode.Value));
        }
        this.documentControl.DocumentViewMode = this.DocumentViewMode.Value;
      }
      this.Controls.Add((Control) this.documentControl);
      this.documentControl.DocumentManager = this.documentManager;
      this.documentControl.Dock = DockStyle.Fill;
      this.documentControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.documentControl_ActivePageChanged);
      this.documentControl.PageCursorPositionChanged += new PageCursorPositionChanged_EventHandler(this.documentControl_PageCursorPositionChanged);
      this.documentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.GetDocumentElementContextMenu);
      if (this.Document != null)
        this.Document.SetNeedUIRecursive(true, true);
      this.findReplaceManager = new FindReplaceManager(this.documentControl);
    }
    else
      this.findReplaceManager = (FindReplaceManager) null;
  }

  /// <summary>Получить пункты контекстного меню для элемента документа</summary>
  /// <param name="sender">Вызвавший объект</param>
  /// <param name="e">Аргументы</param>
  protected virtual void GetDocumentElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
  }

  /// <summary>Получить фокус для документа</summary>
  public void FocusDocument()
  {
    if (this.DocumentControl == null || this.DocumentControl.ContainsFocus)
      return;
    this.Focus();
    if (this.DocumentControl.ActivePage == null)
      this.DocumentControl.GotoNextPage();
    if (this.DocumentControl.ActivePage != null && this.DocumentControl.PageControl != null)
      this.DocumentControl.PageControl.Focus();
    this.DocumentControl.SetSelection((DocumentTreeNode) this.DocumentControl.Document, false, Point.Empty, false, true);
    if (this.DocumentControl.ContainsFocus)
      return;
    this.DocumentControl.Focus();
  }

  private void documentControl_PageCursorPositionChanged(
    object sender,
    PageCursorPositionChanged_EventArgs e)
  {
    try
    {
      this.UpdateCursorCoorPanel(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void documentControl_ActivePageChanged(object sender, EventArgs e)
  {
    try
    {
      this.UpdateSBPagePanel();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Расширения по умолчанию</summary>
  public virtual string DefaultFileName
  {
    [DebuggerStepThrough] get
    {
      string defaultFileName = "";
      if (this.defaultFileName != null)
        defaultFileName = this.defaultFileName;
      else if (this.Document != null)
        defaultFileName = ImDocumentData.ReplaceForbiddenSymbols(this.Document?.GetDefautCaption() + this.DefaultFileExtension);
      return defaultFileName;
    }
    set => this.defaultFileName = value;
  }

  /// <summary>Расширение по умолчанию</summary>
  public virtual string DefaultFileExtension
  {
    [DebuggerStepThrough] get => this.defaultFileExtension;
    set => this.defaultFileExtension = value;
  }

  /// <summary>Последнее имя файла при сохранении в файл</summary>
  protected virtual string RecentlySaveAsFileName
  {
    [DebuggerStepThrough] get
    {
      if (this.recentlySaveAsFileName != null && this.recentlySaveAsFileName != "")
        return this.recentlySaveAsFileName;
      string str = (string) null;
      if (this.documentManager != null)
        str = this.documentManager.RecentlySaveAsPath;
      if (str == null)
        str = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\";
      return str + this.DefaultFileName;
    }
    set => this.recentlySaveAsFileName = value;
  }

  /// <summary>Сжимался ли файл при сохранении</summary>
  protected virtual bool RecentlyPackedFile
  {
    [DebuggerStepThrough] get => this.recentlyPackedFile;
    set => this.recentlyPackedFile = value;
  }

  /// <summary>Извлечь выбранный фильтр из строки с фильтрами диалога открытия или сохранения</summary>
  /// <param name="filter">Строка с фильтрами</param>
  /// <param name="filterIndex">Индекс фильтра</param>
  /// <returns>Строка выделенного фильтра</returns>
  public static string GetSelectedFileFilter(string filter, int filterIndex)
  {
    string selectedFileFilter = "";
    string[] strArray = filter.Split('|');
    if (filterIndex * 2 - 1 < strArray.Length)
      selectedFileFilter = strArray[filterIndex * 2 - 1];
    return selectedFileFilter;
  }

  /// <summary>Диалог сохранения документа в файл</summary>
  protected virtual SaveFileDialog SaveToFileDialog
  {
    [DebuggerStepThrough] get
    {
      if (this.documentManager != null)
        return this.documentManager.SaveToFileDialog;
      if (this.saveToFileDialog == null)
        this.saveToFileDialog = ImDocumentEditorFormBase.CreateSaveFileDialog();
      return this.saveToFileDialog;
    }
  }

  /// <summary>Фильтр типов файлов документов интермех для диалога сохранения</summary>
  public static string ImDocumentFilter
  {
    [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("Document.Model_69");
  }

  /// <summary>Фильтр типов файлов комплектов документов интермех для диалога сохранения</summary>
  public static string ImDocumentsComplectFilter
  {
    [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("Document.Model_556");
  }

  /// <summary>Создать диалог сохранения файла</summary>
  /// <returns>Диалог сохранения файла</returns>
  public static SaveFileDialog CreateSaveFileDialog()
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.FileName = "";
    saveFileDialog.Filter = ImDocumentEditorFormBase.ImDocumentFilter;
    saveFileDialog.InitialDirectory = "\".\"";
    saveFileDialog.RestoreDirectory = true;
    return saveFileDialog;
  }

  /// <summary>Обработчик события Closing</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnClosing(CancelEventArgs e)
  {
    try
    {
      if (this.DocumentControl != null)
        this.DocumentControl.EditorValidating(e);
      if (e.Cancel)
        return;
      base.OnClosing(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public override void OnClosed(EventArgs e)
  {
    if (this.DocumentControl != null && this.DocumentControl.Document != null)
    {
      if (this.DocumentControl.Document.PageControl != null)
        this.DocumentControl.Document.PageControl.LockUpdateSettings();
      this.DocumentControl.LockForClosing = true;
      this.DocumentControl.Document.AbortBackgroundThreads();
      for (int index = 0; this.DocumentControl.LockedForHandler > 0 && index < 10; ++index)
        Application.DoEvents();
    }
    base.OnClosed(e);
  }

  /// <summary>Обновить панель страниц в строке статуса</summary>
  public virtual void UpdateSBPagePanel()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new MethodInvoker(this.UpdateSBPagePanel));
    }
    else
    {
      if (this.StatusBarPagePanel == null)
        return;
      if (this.DocumentControl != null && this.Document != null)
      {
        ImDocument document = this.Document;
        int num = document.Nodes.IndexOf((DocumentTreeNode) this.DocumentControl.ActivePage);
        int count = document.Nodes.Count;
        if (num < 0)
          this.StatusBarPagePanel.Text = string.Format(LocalizationHolder.rm.GetString("Document.Model_70"), (object) "...", (object) count);
        else
          this.StatusBarPagePanel.Text = string.Format(LocalizationHolder.rm.GetString("Document.Model_71"), (object) (num + 1), (object) count);
      }
      else
        this.StatusBarPagePanel.Text = LocalizationHolder.rm.GetString("Document.Model_72");
    }
  }

  /// <summary>Обновить панель координат курсора в строке статуса</summary>
  /// <param name="e">Аргументы</param>
  public virtual void UpdateCursorCoorPanel(PageCursorPositionChanged_EventArgs e)
  {
    if (this.StatusBarCursorCoorPanel == null)
      return;
    this.StatusBarCursorCoorPanel.Text = $"{e.Position.X.ToString("G")}; {e.Position.Y.ToString("G")}";
  }

  /// <summary>Настроить строку статуса под окно</summary>
  /// <param name="statusBar">Строка статуса</param>
  public virtual void SetStatusBar(StatusBar statusBar)
  {
    if (statusBar == null)
      return;
    if (statusBar.Panels.Count == 0)
      statusBar.Panels.Add(ImDocumentEditorFormBase.sbMessagePanel = new StatusBarPanel());
    else
      ImDocumentEditorFormBase.sbMessagePanel = statusBar.Panels[0];
    if (ImDocumentEditorFormBase.sbCursorCoorPanel == null)
    {
      ImDocumentEditorFormBase.sbCursorCoorPanel = new StatusBarPanel();
      ImDocumentEditorFormBase.sbCursorCoorPanel.Name = "CursorCoor";
      ImDocumentEditorFormBase.sbCursorCoorPanel.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_590");
      ImDocumentEditorFormBase.sbCursorCoorPanel.Width = 80 /*0x50*/;
    }
    if (!statusBar.Panels.Contains(ImDocumentEditorFormBase.sbCursorCoorPanel))
      statusBar.Panels.Insert(1, ImDocumentEditorFormBase.sbCursorCoorPanel);
    if (ImDocumentEditorFormBase.sbPagePanel == null)
    {
      ImDocumentEditorFormBase.sbPagePanel = new StatusBarPanel();
      ImDocumentEditorFormBase.sbPagePanel.Width = 80 /*0x50*/;
      ImDocumentEditorFormBase.sbPagePanel.Name = "Page";
      ImDocumentEditorFormBase.sbPagePanel.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_590");
    }
    if (!statusBar.Panels.Contains(ImDocumentEditorFormBase.sbPagePanel))
      statusBar.Panels.Insert(2, ImDocumentEditorFormBase.sbPagePanel);
    this.UpdateSBPagePanel();
  }

  /// <summary>Восстановить строку статуса</summary>
  /// <param name="statusBar">Строка статуса</param>
  public virtual void RestoreStatusBar(StatusBar statusBar)
  {
    if (statusBar == null)
      return;
    if (ImDocumentEditorFormBase.sbCursorCoorPanel != null)
      statusBar.Panels.Remove(ImDocumentEditorFormBase.sbCursorCoorPanel);
    if (ImDocumentEditorFormBase.sbPagePanel != null)
      statusBar.Panels.Remove(ImDocumentEditorFormBase.sbPagePanel);
    if (ImDocumentEditorFormBase.sbMessagePanel == null)
      return;
    ImDocumentEditorFormBase.sbMessagePanel.Text = "";
  }

  /// <summary>Панель общих сообщений в строке статуса</summary>
  protected virtual StatusBarPanel StatusBarMessagePanel
  {
    [DebuggerStepThrough] get => ImDocumentEditorFormBase.sbMessagePanel;
    set => ImDocumentEditorFormBase.sbMessagePanel = value;
  }

  /// <summary>Панель координат курсора в строке статуса</summary>
  protected virtual StatusBarPanel StatusBarCursorCoorPanel
  {
    [DebuggerStepThrough] get => ImDocumentEditorFormBase.sbCursorCoorPanel;
    set => ImDocumentEditorFormBase.sbCursorCoorPanel = value;
  }

  /// <summary>Панель страниц в строке статуса</summary>
  protected virtual StatusBarPanel StatusBarPagePanel
  {
    [DebuggerStepThrough] get => ImDocumentEditorFormBase.sbPagePanel;
    set => ImDocumentEditorFormBase.sbPagePanel = value;
  }

  /// <summary>Пустой конструктор. Должен вызываться во всех конструкторах</summary>
  protected ImDocumentEditorFormBase()
  {
    this.AllowedStates = DockLocation.Document;
    this.InitializeComponent();
    Application.ApplicationExit += new EventHandler(this.Application_ApplicationExit);
  }

  private void Application_ApplicationExit(object sender, EventArgs e)
  {
    if (this.DocumentControl == null || this.DocumentControl.Document == null)
      return;
    this.DocumentControl.Document.AbortBackgroundThreads();
  }

  private ImDocumentEditorFormBase(IImDocumentManager documentManager)
    : this()
  {
    this.documentManager = documentManager;
  }

  /// <summary>Конструктор. Создает окно с пустым документом</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="createDocument">Создать документ</param>
  /// <param name="createFirstPage">Создать первую страницу в документе</param>
  public ImDocumentEditorFormBase(
    IImDocumentManager documentManager,
    bool createDocument,
    bool createFirstPage)
    : this(documentManager)
  {
    if (!createDocument)
      return;
    this.AssignDocumentControl(new DocumentControl());
    if (createFirstPage)
      this.documentControl.Document.NewPage();
    this.documentControl.Document.Modified = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="document">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  public ImDocumentEditorFormBase(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
    : this(documentManager)
  {
    this.AssignDocumentControl(new DocumentControl(document, documentManager));
    this.documentControl.ReadOnly = readOnly;
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="documentationComplect">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  public ImDocumentEditorFormBase(
    IImDocumentManager documentManager,
    DocumentsComplect documentsComplect,
    bool readOnly)
    : this(documentManager)
  {
    if (documentsComplect == null)
      throw new ArgumentNullException(nameof (documentsComplect));
    ImDocument document = (ImDocument) null;
    if (documentsComplect.Nodes.Count == 0)
      this.AssignDocumentControl(new DocumentControl(document, documentManager));
    if (DocumentsComplect.GetFirstDocument((DocumentTreeNode) documentsComplect) is ImDocument firstDocument)
      this.AssignDocumentControl(new DocumentControl(firstDocument, documentManager));
    this.DocumentCaption = documentsComplect.Name;
    this.documentControl.DocumentsComplect = documentsComplect;
    this.documentControl.ReadOnly = readOnly;
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="documentControl">Элемент управления документа</param>
  /// <param name="readOnly">Только для чтения</param>
  public ImDocumentEditorFormBase(
    IImDocumentManager documentManager,
    DocumentControl documentControl,
    bool readOnly)
    : this(documentManager)
  {
    this.AssignDocumentControl(documentControl);
    documentControl.ReadOnly = readOnly;
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.FloatingSize = new Size(250, 300);
    this.Name = nameof (ImDocumentEditorFormBase);
    this.Size = new Size(292, 273);
    this.ResumeLayout(false);
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      Application.ApplicationExit -= new EventHandler(this.Application_ApplicationExit);
      if (this.DisposeDocumentOnClose)
      {
        if (this.DocumentsComplect != null)
          this.DocumentsComplect.Dispose();
        if (this.Document != null)
        {
          ImDocument document = this.Document;
          if (document.TemplateOwner == null)
            document.Dispose();
        }
      }
      if (this.documentControl != null && !this.documentControl.IsDisposed && this.documentControl.Document != null)
      {
        this.documentControl.Document.DocumentControl = (DocumentControl) null;
        this.documentControl.Parent = (Control) null;
        this.documentControl.Dispose();
        this.documentControl = (DocumentControl) null;
      }
      if (this.components != null)
        this.components.Dispose();
      if (this.menuHelper != null)
      {
        this.menuHelper.Dispose();
        this.menuHelper = (DocumentMenuHelper) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Данные команды</param>
  /// <returns>true, если команда найдена</returns>
  public virtual bool Execute(ICommandState commandState)
  {
    if (this.MenuHelper == null)
      return false;
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "AddToUserDictionary":
          int pWordIdx = 0;
          int pWordLen = 0;
          ImRtfEditor activeEditorControl = this.DocumentControl.GetActiveEditorControl();
          string word;
          if (activeEditorControl != null && activeEditorControl.spl.GetMisspelledWord(activeEditorControl.MouseLine, activeEditorControl.MouseCol, out word, ref pWordIdx, ref pWordLen))
          {
            if (SpellChecker.Instance.Dict.UserWords.ContainsKey((object) word))
            {
              int num1 = (int) MessageBox.Show("Слово добавлено ранее");
            }
            else
            {
              SpellChecker.Instance.Dict.UserFileAdd(word);
              activeEditorControl.spl.SpellCheckCurWordPart3();
            }
          }
          return true;
        case "ExportToWMF":
          if (this.Document != null)
          {
            this.Document.FirstOrDefault<PageData>()?.FindFirstMainTable();
            string fileName = Path.ChangeExtension(this.RecentlySaveAsFileName, ".wmf");
            int[] pages;
            if (ExportToImagesDlg.Execute(this.Document.Nodes.Count, out pages, ref fileName) == DialogResult.OK)
            {
              this.RecentlySaveAsFileName = Path.ChangeExtension(fileName, (string) null);
              this.Document.GeneratePageMetafiles(pages, this.RecentlySaveAsFileName);
            }
          }
          return true;
        case "LineStyleSetup":
          return true;
        case "Navigation.FirstPage":
          this.DocumentControl.GotoFirstPage();
          return true;
        case "Navigation.LastPage":
          this.DocumentControl.GotoLastPage();
          return true;
        case "Navigation.NextDocument":
          this.DocumentControl.GoToNextDocument();
          return true;
        case "Navigation.NextPage":
          this.DocumentControl.GotoNextPage();
          return true;
        case "Navigation.PrevDocument":
          this.DocumentControl.GoToPrevDocument(false);
          return true;
        case "Navigation.PrevPage":
          this.DocumentControl.GotoPrevPage();
          return true;
        case "Print":
        case "PrintDocument":
          VisualStyleState visualStyleState = Application.VisualStyleState;
          try
          {
            if (this.DocumentControl != null && this.DocumentControl.DocumentsComplect != null)
            {
              DocumentsComplect documentsComplect = this.DocumentControl.DocumentsComplect;
              documentsComplect.BeforeShowPrintDialog();
              int num2 = 1;
              if (documentsComplect.PageCount == 0)
                num2 = 0;
              PrinterSettings printerSettings = documentsComplect.PrintDocument.PrinterSettings;
              documentsComplect.PrintDocument.PrinterSettings = new PrinterSettings();
              documentsComplect.PrintDocument.PrinterSettings.PrinterName = printerSettings.PrinterName;
              documentsComplect.PrintDocument.PrinterSettings.MinimumPage = num2;
              documentsComplect.PrintDocument.PrinterSettings.FromPage = num2;
              documentsComplect.PrintDocument.PrinterSettings.MaximumPage = documentsComplect.PageCount;
              documentsComplect.PrintDocument.PrinterSettings.ToPage = documentsComplect.PageCount;
              documentsComplect.PrintDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
              documentsComplect.PrintDocument.PrinterSettings.Collate = true;
              PrintComplectDialog printComplectDialog = new PrintComplectDialog(documentsComplect.PrintDocument, documentsComplect);
              if (printComplectDialog.ShowDialog() == DialogResult.OK)
                documentsComplect.PrintDocument.Print();
              printComplectDialog.Dispose();
              GC.Collect();
              int num3 = (int) GC.WaitForFullGCComplete();
              return true;
            }
            if (this.Document == null)
              throw new Exception(LocalizationHolder.rm.GetString("Document.Model_596"));
            int num4 = 1;
            if (this.Document.NodesCount == 0)
              num4 = 0;
            PrinterSettings printerSettings1 = this.Document.PrintDocument.PrinterSettings;
            this.Document.PrintDocument.PrinterSettings = new PrinterSettings();
            this.Document.PrintDocument.PrinterSettings.PrinterName = printerSettings1.PrinterName;
            this.Document.PrintDocument.PrinterSettings.FromPage = num4;
            this.Document.PrintDocument.PrinterSettings.MaximumPage = this.Document.NodesCount;
            this.Document.PrintDocument.PrinterSettings.ToPage = this.Document.NodesCount;
            this.Document.PrintDocument.PrinterSettings.Collate = true;
            DialogResult dialogResult = new PrintDocumentDialog(this.Document.PrintDocument, (ImDocumentData) this.Document).ShowDialog();
            try
            {
              this.Document.PrintDocument.DefaultPageSettings = this.Document.PrintDocument.PrinterSettings.DefaultPageSettings;
              if (dialogResult == DialogResult.OK)
                this.Document.PrintDocument.Print();
            }
            catch
            {
            }
            GC.Collect();
            int num5 = (int) GC.WaitForFullGCComplete();
          }
          finally
          {
            DocumentMenuHelper.SilentRecoverVisualStyle(visualStyleState);
          }
          return true;
        case "PrintPreview":
          if (this.Document != null)
          {
            PrintPreviewDlg printPreviewDlg = new PrintPreviewDlg();
            PrintDocument printDocument;
            if (this.DocumentControl != null && this.DocumentControl.DocumentsComplect != null)
            {
              DocumentsComplect documentsComplect = this.DocumentControl.DocumentsComplect;
              printDocument = documentsComplect.PrintDocument;
              PrinterSettings printerSettings = this.Document.PrintDocument.PrinterSettings;
              printDocument.PrinterSettings = new PrinterSettings();
              printDocument.PrinterSettings.PrinterName = printerSettings.PrinterName;
              printPreviewDlg.Tag = (object) documentsComplect;
              int num6 = 1;
              if (documentsComplect.PageCount == 0)
                num6 = 0;
              printDocument.PrinterSettings.MinimumPage = num6;
              printDocument.PrinterSettings.FromPage = num6;
              printDocument.PrinterSettings.MaximumPage = documentsComplect.PageCount;
              printDocument.PrinterSettings.ToPage = documentsComplect.PageCount;
              printDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
              printDocument.PrinterSettings.Collate = true;
            }
            else
            {
              printDocument = this.Document.PrintDocument;
              PrinterSettings printerSettings = this.Document.PrintDocument.PrinterSettings;
              printDocument.PrinterSettings = new PrinterSettings();
              printDocument.PrinterSettings.PrinterName = printerSettings.PrinterName;
              printPreviewDlg.Tag = (object) this.Document;
              int num7 = 1;
              if (this.Document.NodesCount == 0)
                num7 = 0;
              printDocument.PrinterSettings.MinimumPage = num7;
              printDocument.PrinterSettings.FromPage = num7;
              printDocument.PrinterSettings.MaximumPage = this.Document.NodesCount;
              printDocument.PrinterSettings.ToPage = this.Document.NodesCount;
              printDocument.PrinterSettings.Collate = true;
            }
            printPreviewDlg.Document = printDocument;
            int num8 = (int) printPreviewDlg.ShowDialog();
          }
          return true;
        case "SaveAs":
          string fileName1 = (string) null;
          this.SaveAsExecute(ref fileName1);
          return true;
        default:
          this._IsQueryChacheIsInit = false;
          this.InitQueryCache();
          List<DocumentTreeNode> queryStatusContext = this._queryStatusContext;
          bool flag1 = this.Document == null || this.ReadOnly;
          switch (commandState.CommandName)
          {
            case "Format.BgColor":
              if (flag1)
                return true;
              if (this.Document != null && this.Document.UndoManager != null)
                this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_580"));
              try
              {
                this.BgColorChanged();
                if (this.documentManager != null)
                  this.documentManager.UpdateSelectedElementInfo();
              }
              finally
              {
                if (this.Document != null && this.Document.UndoManager != null)
                  this.Document.UndoManager.EndCreateMultyUndo();
              }
              return true;
            case "Format.Borders":
            case "Format.Borders.Color":
              if (flag1)
                return true;
              if (DocumentMenuHelper.ActiveBordersCommand != null)
                this.ProcessBordersCommand((IList<DocumentTreeNode>) queryStatusContext, DocumentMenuHelper.ActiveBordersCommand);
              this.Document.RefreshUI();
              return true;
            case "Format.BulletsList":
              if (flag1)
                return true;
              this.SetListedParagraph(!commandState.Checked, true);
              return true;
            case "Format.DecreaseIdent":
              if (flag1)
                return true;
              this.IncreaseIndent(false);
              return true;
            case "Format.Font.SetupFont":
              if (flag1)
                return true;
              CharFormat queryCharFormat = (CharFormat) null;
              this.QueryCharFormat((IList<DocumentTreeNode>) queryStatusContext, ref queryCharFormat);
              if (queryCharFormat.FontFamily == null && queryStatusContext[0] is TextData textData)
                queryCharFormat = textData.CharFormat;
              FontSetupDlg fontSetupDlg = new FontSetupDlg(queryCharFormat, string.Empty);
              if (fontSetupDlg.ShowDialog() == DialogResult.OK)
              {
                fontSetupDlg.Save();
                this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, queryCharFormat, true);
              }
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.Strikeout":
              if (flag1)
                return true;
              bool flag2 = !commandState.Checked;
              CharFormat charFormat1 = new CharFormat(true);
              charFormat1.Strike = flag2 ? new StrikeoutLineStyle?(StrikeoutLineStyle.SingleLine) : (charFormat1.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.None));
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, charFormat1, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.StrikeoutDouble":
              if (flag1)
                return true;
              bool flag3 = !commandState.Checked;
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, new CharFormat(true)
              {
                Strike = new StrikeoutLineStyle?(flag3 ? StrikeoutLineStyle.DoubleLine : StrikeoutLineStyle.None)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.Subscript":
              if (flag1)
                return true;
              bool flag4 = !commandState.Checked;
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, new CharFormat(true)
              {
                Subscript = this._queryCharFormatforStyles == null || this._queryCharFormatforStyles.Subscript.HasValue ? new bool?(flag4) : new bool?(true)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.Superscript":
              if (flag1)
                return true;
              bool flag5 = !commandState.Checked;
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, new CharFormat(true)
              {
                Superscript = this._queryCharFormatforStyles == null || this._queryCharFormatforStyles.Superscript.HasValue ? new bool?(flag5) : new bool?(true)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.TextBold":
              if (flag1)
                return true;
              bool flag6 = !commandState.Checked;
              CharFormat charFormat2 = new CharFormat(true);
              int num9;
              if (this._queryCharFormatforStyles != null)
              {
                BoldItalicStyle? boldItalic = this._queryCharFormatforStyles.BoldItalic;
                if (boldItalic.HasValue)
                {
                  boldItalic = this._queryCharFormatforStyles.BoldItalic;
                  num9 = (int) boldItalic.Value;
                  goto label_59;
                }
              }
              num9 = 0;
label_59:
              charFormat2.BoldItalic = !flag6 ? new BoldItalicStyle?((BoldItalicStyle) (num9 & -3)) : new BoldItalicStyle?((BoldItalicStyle) (num9 | 2));
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, charFormat2, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.TextCursive":
              if (flag1)
                return true;
              bool flag7 = !commandState.Checked;
              CharFormat charFormat3 = new CharFormat(true);
              int num10;
              if (this._queryCharFormatforStyles != null)
              {
                BoldItalicStyle? boldItalic = this._queryCharFormatforStyles.BoldItalic;
                if (boldItalic.HasValue)
                {
                  boldItalic = this._queryCharFormatforStyles.BoldItalic;
                  num10 = (int) boldItalic.Value;
                  goto label_68;
                }
              }
              num10 = 0;
label_68:
              charFormat3.BoldItalic = !flag7 ? new BoldItalicStyle?((BoldItalicStyle) (num10 & -5)) : new BoldItalicStyle?((BoldItalicStyle) (num10 | 4));
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, charFormat3, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.Font.TextUnderline":
              if (flag1)
                return true;
              bool flag8 = !commandState.Checked;
              this.SaveCharFormat((IList<DocumentTreeNode>) queryStatusContext, new CharFormat(true)
              {
                Underline = !flag8 ? new UnderlineStyle?(UnderlineStyle.None) : new UnderlineStyle?(UnderlineStyle.Underline)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.IncreaseIdent":
              if (flag1)
                return true;
              this.IncreaseIndent(true);
              return true;
            case "Format.NumberingList":
              if (flag1)
                return true;
              this.SetListedParagraph(!commandState.Checked, false);
              return true;
            case "Format.SetupBordersAndBackground":
              if (flag1)
                return true;
              SetupBorders setupBorders = new SetupBorders();
              if (this.Document != null && this.Document.UndoManager != null)
                this.Document.UndoManager.BeginCreateMultyUndo("");
              try
              {
                int num11 = (int) setupBorders.ShowDialog();
              }
              finally
              {
                if (this.Document != null && this.Document.UndoManager != null)
                  this.Document.UndoManager.EndCreateMultyUndo();
              }
              return true;
            case "Format.SetupParagraph":
              if (flag1)
                return true;
              ParagraphFormat queryParagraphFormat = (ParagraphFormat) null;
              this.QueryParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, ref queryParagraphFormat);
              SetupParagraphDlg setupParagraphDlg = this._queryIsAllTextSelected || this._queryTern == null || this._queryTern.AllParagraphsSelected() ? new SetupParagraphDlg(queryParagraphFormat, new float?(12f), true) : new SetupParagraphDlg(queryParagraphFormat, new float?(12f), false);
              if (setupParagraphDlg.ShowDialog() == DialogResult.OK && setupParagraphDlg.ParagraphFormat != null)
                this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, setupParagraphDlg.ParagraphFormat, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.SetupTextDirrection":
              if (flag1)
                return true;
              TextOrientation? textOrientation = new TextOrientation?();
              if (this.GetTextOrientation((IList<DocumentTreeNode>) queryStatusContext, ref textOrientation) && !textOrientation.HasValue)
                textOrientation = new TextOrientation?(TextOrientation.Normal);
              SetupTextDirrectionDlg textDirrectionDlg = new SetupTextDirrectionDlg(textOrientation);
              if (textDirrectionDlg.ShowDialog() == DialogResult.OK && textDirrectionDlg.SelectedTextOrientation.HasValue)
                this.SetTextOrientation((IList<DocumentTreeNode>) queryStatusContext, textDirrectionDlg.SelectedTextOrientation.Value);
              return true;
            case "Format.TextAlignCenter":
              if (flag1)
                return true;
              bool flag9 = !commandState.Checked;
              this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, new ParagraphFormat(true)
              {
                HorzAlignment = !flag9 ? new HorzAlignment?(HorzAlignment.Left) : new HorzAlignment?(HorzAlignment.Center)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.TextAlignJustify":
              if (flag1)
                return true;
              bool flag10 = !commandState.Checked;
              this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, new ParagraphFormat(true)
              {
                HorzAlignment = !flag10 ? new HorzAlignment?(HorzAlignment.Left) : new HorzAlignment?(HorzAlignment.Justify)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.TextAlignLeft":
              if (flag1)
                return true;
              bool flag11 = !commandState.Checked;
              this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, new ParagraphFormat(true)
              {
                HorzAlignment = !flag11 ? new HorzAlignment?(HorzAlignment.Justify) : new HorzAlignment?(HorzAlignment.Left)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.TextAlignRight":
              if (flag1)
                return true;
              bool flag12 = !commandState.Checked;
              this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, new ParagraphFormat(true)
              {
                HorzAlignment = !flag12 ? new HorzAlignment?(HorzAlignment.Left) : new HorzAlignment?(HorzAlignment.Right)
              }, true);
              if (this.documentManager != null)
                this.documentManager.UpdateSelectedElementInfo();
              return true;
            case "Format.TextBkColor":
              if (flag1)
                return true;
              if (this.Document != null && this.Document.UndoManager != null)
                this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_581"));
              try
              {
                this.TextBkColorChanged();
                if (this.documentManager != null)
                  this.documentManager.UpdateSelectedElementInfo();
              }
              finally
              {
                if (this.Document != null && this.Document.UndoManager != null)
                  this.Document.UndoManager.EndCreateMultyUndo();
              }
              return true;
            case "Format.TextColor":
              if (flag1)
                return true;
              if (this.Document != null && this.Document.UndoManager != null)
                this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_582"));
              try
              {
                this.TextColorChanged();
                if (this.documentManager != null)
                  this.documentManager.UpdateSelectedElementInfo();
              }
              finally
              {
                if (this.Document != null && this.Document.UndoManager != null)
                  this.Document.UndoManager.EndCreateMultyUndo();
              }
              return true;
            default:
              if (commandState.CommandName.StartsWith("Format.Font.Registr"))
              {
                if (commandState.CommandName != "Format.Font.Registr")
                  this.ApplyRegistr((IList<DocumentTreeNode>) queryStatusContext, commandState.CommandName, true);
                return true;
              }
              if (commandState.CommandName.StartsWith("Format.Borders."))
              {
                if (!flag1)
                {
                  this.ProcessBordersCommand((IList<DocumentTreeNode>) queryStatusContext, commandState.CommandName);
                  if (this.Document != null && this.Document.DocumentControl != null)
                    this.Document.DocumentControl.ActivePage.RefreshUI();
                }
                this.UpdateBorberCommands();
                return true;
              }
              if (commandState.CommandName.StartsWith("Format.TextSpaceBetweenLines"))
              {
                if (flag1)
                  return true;
                MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandState.CommandName);
                if (menuItem == null || menuItem.Tag == null || !(menuItem.Tag is int))
                  return true;
                ParagraphFormat paragraphFormat1 = new ParagraphFormat(true);
                int tag = (int) menuItem.Tag;
                if (tag == -1)
                {
                  if (!commandState.Checked)
                  {
                    if (!this._lastSetLineSpacing.HasValue)
                      this._lastSetLineSpacing = new int?(50);
                    int? lastSetLineSpacing = this._lastSetLineSpacing;
                    int num12 = 0;
                    if (lastSetLineSpacing.GetValueOrDefault() == num12 & lastSetLineSpacing.HasValue)
                      paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1);
                    lastSetLineSpacing = this._lastSetLineSpacing;
                    int num13 = 50;
                    if (lastSetLineSpacing.GetValueOrDefault() == num13 & lastSetLineSpacing.HasValue)
                      paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1_5);
                    lastSetLineSpacing = this._lastSetLineSpacing;
                    int num14 = 100;
                    if (lastSetLineSpacing.GetValueOrDefault() == num14 & lastSetLineSpacing.HasValue)
                      paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_2);
                    lastSetLineSpacing = this._lastSetLineSpacing;
                    int num15 = 100;
                    if (lastSetLineSpacing.GetValueOrDefault() > num15 & lastSetLineSpacing.HasValue)
                    {
                      paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio);
                      ParagraphFormat paragraphFormat2 = paragraphFormat1;
                      lastSetLineSpacing = this._lastSetLineSpacing;
                      float? nullable = lastSetLineSpacing.HasValue ? new float?((float) (lastSetLineSpacing.GetValueOrDefault() + 100) / 100f) : new float?();
                      paragraphFormat2.SpaceBetweenLines = nullable;
                    }
                    ParagraphFormat paragraphFormat3 = paragraphFormat1;
                    lastSetLineSpacing = this._lastSetLineSpacing;
                    float? nullable1 = lastSetLineSpacing.HasValue ? new float?((float) (lastSetLineSpacing.GetValueOrDefault() + 100) / 100f) : new float?();
                    paragraphFormat3.SpaceBetweenLines = nullable1;
                  }
                  else
                    paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1);
                }
                else
                {
                  if (tag == 0)
                    paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1);
                  if (tag == 50)
                    paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1_5);
                  if (tag == 100)
                    paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_2);
                  if (tag > 100)
                  {
                    paragraphFormat1.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio);
                    paragraphFormat1.SpaceBetweenLines = new float?((float) (tag + 100) / 100f);
                  }
                  paragraphFormat1.SpaceBetweenLines = new float?((float) (tag + 100) / 100f);
                  this._lastSetLineSpacing = new int?(tag);
                }
                this.SaveParagraphFormat((IList<DocumentTreeNode>) queryStatusContext, paragraphFormat1, true);
                if (this.documentManager != null)
                  this.documentManager.UpdateSelectedElementInfo();
                return true;
              }
              this.InitQueryCache();
              break;
          }
          break;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  private void ProcessBordersCommand(IList<DocumentTreeNode> context, string commandName)
  {
    MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandName);
    if (menuItem == null)
      return;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
    {
      case 44481532:
        if (!(commandName == "Format.Borders.Delete"))
          return;
        this.ProcessBordersCommand(context, false, ImDocumentEditorFormBase.borderType.OuterLeft | ImDocumentEditorFormBase.borderType.OuterTop | ImDocumentEditorFormBase.borderType.OuterRight | ImDocumentEditorFormBase.borderType.OuterBottom | ImDocumentEditorFormBase.borderType.InnerHorizontal | ImDocumentEditorFormBase.borderType.InnerVertical);
        break;
      case 1236359072:
        if (!(commandName == "Format.Borders.Bottom"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterBottom);
        break;
      case 1488192673:
        if (!(commandName == "Format.Borders.Vertical"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.InnerVertical);
        break;
      case 1939602238:
        if (!(commandName == "Format.Borders.Horisontal"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.InnerHorizontal);
        break;
      case 2461918249:
        if (!(commandName == "Format.Borders.Inner"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.InnerHorizontal | ImDocumentEditorFormBase.borderType.InnerVertical);
        break;
      case 2977871658:
        if (!(commandName == "Format.Borders.Outer"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterLeft | ImDocumentEditorFormBase.borderType.OuterTop | ImDocumentEditorFormBase.borderType.OuterRight | ImDocumentEditorFormBase.borderType.OuterBottom);
        break;
      case 3977994238:
        if (!(commandName == "Format.Borders.Top"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterTop);
        break;
      case 4004493482:
        if (!(commandName == "Format.Borders.All"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterLeft | ImDocumentEditorFormBase.borderType.OuterTop | ImDocumentEditorFormBase.borderType.OuterRight | ImDocumentEditorFormBase.borderType.OuterBottom | ImDocumentEditorFormBase.borderType.InnerHorizontal | ImDocumentEditorFormBase.borderType.InnerVertical);
        break;
      case 4198414646:
        if (!(commandName == "Format.Borders.Left"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterLeft);
        break;
      case 4225635947:
        if (!(commandName == "Format.Borders.Right"))
          return;
        this.ProcessBordersCommand(context, !menuItem.Checked, ImDocumentEditorFormBase.borderType.OuterRight);
        break;
      default:
        return;
    }
    if (this.MenuHelper.BordersToolButton == null)
      return;
    if (DocumentMenuHelper.ActiveBordersCommand != commandName)
      DocumentMenuHelper.ActiveBordersCommand = commandName;
    if (!this.MenuHelper.BordersToolButton.Image.Equals((object) menuItem.Image))
      this.MenuHelper.BordersToolButton.Image = menuItem.Image;
    if (this.MenuHelper.BordersToolButton.ToolTipText != menuItem.ToolTipText)
      this.MenuHelper.BordersToolButton.ToolTipText = menuItem.ToolTipText;
    if (this.MenuHelper.BordersToolButton.Checked == menuItem.Checked)
      return;
    this.MenuHelper.BordersToolButton.Checked = menuItem.Checked;
  }

  private void QueryCharFormat(IList<DocumentTreeNode> context, ref CharFormat queryCharFormat)
  {
    queryCharFormat = new CharFormat();
    if (this._queryTern == null)
    {
      bool firstLoad = true;
      this.QueryCharFormat(context, ref queryCharFormat, ref firstLoad);
    }
    else
    {
      queryCharFormat.TextColorForUser = this._queryTextColor;
      queryCharFormat.TextBkColorForUser = this._queryTextBkColor;
      queryCharFormat.CharStyle = (CharStyle) this._queryStyles.Value;
      queryCharFormat.BoldItalic = this._queryCharFormatforStyles.BoldItalic;
      queryCharFormat.Underline = this._queryCharFormatforStyles.Underline;
      int num1 = queryCharFormat.Underline.HasValue ? 1 : 0;
      UnderlineStyle? underline = queryCharFormat.Underline;
      UnderlineStyle underlineStyle = UnderlineStyle.None;
      int num2 = !(underline.GetValueOrDefault() == underlineStyle & underline.HasValue) ? 1 : 0;
      queryCharFormat.UnderlineColor = (num1 & num2) == 0 ? new Color?() : this._queryULColor;
      queryCharFormat.Strike = this._queryCharFormatforStyles.Strike;
      queryCharFormat.FontFamily = this._queryTypeface;
      queryCharFormat.AllCaps = this._queryCharFormatforStyles.AllCaps;
      queryCharFormat.AllSmallCaps = this._queryCharFormatforStyles.AllSmallCaps;
      queryCharFormat.Subscript = this._queryCharFormatforStyles.Subscript;
      queryCharFormat.Superscript = this._queryCharFormatforStyles.Superscript;
      queryCharFormat.HiddenText = this._queryCharFormatforStyles.HiddenText;
      if (this._queryPointSize.HasValue)
      {
        int num3 = (int) Math.Round((double) this._queryPointSize.Value / 5.0);
        queryCharFormat.FontSize = new float?(0.25f * (float) num3);
      }
      else
        queryCharFormat.FontSize = new float?();
    }
  }

  private void QueryCharFormat(
    IList<DocumentTreeNode> context,
    ref CharFormat queryCharFormat,
    ref bool firstLoad)
  {
    if (context == null || context.Count <= 0)
      return;
    for (int index = 0; index < context.Count; ++index)
      this.QueryCharFormat(context[index], ref queryCharFormat, ref firstLoad);
  }

  private void QueryCharFormat(
    DocumentTreeNode context,
    ref CharFormat queryCharFormat,
    ref bool firstLoad)
  {
    if (context == null)
      return;
    if (!(context is ImDocument imDocument) && context is Intermech.Document.Model.Page)
      imDocument = (context as Intermech.Document.Model.Page).Parent as ImDocument;
    if (imDocument != null)
    {
      queryCharFormat = imDocument.DefaultCharFormat;
      firstLoad = true;
    }
    else if (context.NodesCount > 0)
    {
      this.QueryCharFormat((IList<DocumentTreeNode>) context.Nodes, ref queryCharFormat, ref firstLoad);
    }
    else
    {
      if (!(context is TextData))
        return;
      CharFormat charFormat1 = ((TextData) context).CharFormat;
      if (firstLoad)
      {
        queryCharFormat = charFormat1.Clone();
        firstLoad = false;
      }
      else
      {
        if (queryCharFormat.FontFamily != null && charFormat1.FontFamily != queryCharFormat.FontFamily)
          queryCharFormat.FontFamily = (string) null;
        if (queryCharFormat.BoldItalic.HasValue)
        {
          BoldItalicStyle? boldItalic1 = charFormat1.BoldItalic;
          BoldItalicStyle? boldItalic2 = queryCharFormat.BoldItalic;
          if (!(boldItalic1.GetValueOrDefault() == boldItalic2.GetValueOrDefault() & boldItalic1.HasValue == boldItalic2.HasValue))
            queryCharFormat.BoldItalic = new BoldItalicStyle?();
        }
        if (queryCharFormat.Strike.HasValue)
        {
          StrikeoutLineStyle? strike1 = charFormat1.Strike;
          StrikeoutLineStyle? strike2 = queryCharFormat.Strike;
          if (!(strike1.GetValueOrDefault() == strike2.GetValueOrDefault() & strike1.HasValue == strike2.HasValue))
            queryCharFormat.Strike = new StrikeoutLineStyle?();
        }
        float? nullable1;
        if (queryCharFormat.FontSize.HasValue)
        {
          nullable1 = charFormat1.FontSize;
          float? fontSize = queryCharFormat.FontSize;
          if (!((double) nullable1.GetValueOrDefault() == (double) fontSize.GetValueOrDefault() & nullable1.HasValue == fontSize.HasValue))
            queryCharFormat.FontSize = new float?();
        }
        Color? nullable2;
        Color? nullable3;
        if (queryCharFormat.TextColor.HasValue)
        {
          nullable2 = charFormat1.TextColor;
          nullable3 = queryCharFormat.TextColor;
          if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            CharFormat charFormat2 = queryCharFormat;
            nullable3 = new Color?();
            Color? nullable4 = nullable3;
            charFormat2.TextColor = nullable4;
          }
        }
        nullable3 = queryCharFormat.TextBkColor;
        if (nullable3.HasValue)
        {
          nullable3 = charFormat1.TextBkColor;
          nullable2 = queryCharFormat.TextBkColor;
          if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            CharFormat charFormat3 = queryCharFormat;
            nullable2 = new Color?();
            Color? nullable5 = nullable2;
            charFormat3.TextBkColor = nullable5;
          }
        }
        if (queryCharFormat.Underline.HasValue)
        {
          UnderlineStyle? underline1 = charFormat1.Underline;
          UnderlineStyle? underline2 = queryCharFormat.Underline;
          if (!(underline1.GetValueOrDefault() == underline2.GetValueOrDefault() & underline1.HasValue == underline2.HasValue))
            queryCharFormat.Underline = new UnderlineStyle?();
        }
        nullable2 = queryCharFormat.UnderlineColor;
        if (nullable2.HasValue)
        {
          nullable2 = charFormat1.UnderlineColor;
          nullable3 = queryCharFormat.UnderlineColor;
          if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            CharFormat charFormat4 = queryCharFormat;
            nullable3 = new Color?();
            Color? nullable6 = nullable3;
            charFormat4.UnderlineColor = nullable6;
          }
        }
        if (queryCharFormat.Zoom.HasValue)
        {
          int? zoom1 = charFormat1.Zoom;
          int? zoom2 = queryCharFormat.Zoom;
          if (!(zoom1.GetValueOrDefault() == zoom2.GetValueOrDefault() & zoom1.HasValue == zoom2.HasValue))
            queryCharFormat.Zoom = new int?();
        }
        float? nullable7;
        if (queryCharFormat.Interval.HasValue)
        {
          nullable7 = charFormat1.Interval;
          nullable1 = queryCharFormat.Interval;
          if (!((double) nullable7.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable7.HasValue == nullable1.HasValue))
          {
            CharFormat charFormat5 = queryCharFormat;
            nullable1 = new float?();
            float? nullable8 = nullable1;
            charFormat5.Interval = nullable8;
          }
        }
        nullable1 = queryCharFormat.Displacement;
        if (nullable1.HasValue)
        {
          nullable1 = charFormat1.Displacement;
          nullable7 = queryCharFormat.Displacement;
          if (!((double) nullable1.GetValueOrDefault() == (double) nullable7.GetValueOrDefault() & nullable1.HasValue == nullable7.HasValue))
          {
            CharFormat charFormat6 = queryCharFormat;
            nullable7 = new float?();
            float? nullable9 = nullable7;
            charFormat6.Displacement = nullable9;
          }
        }
        if ((queryCharFormat.UndefinedCharStyles & CharStyle.Superscript) == CharStyle.Regular && (charFormat1.CharStyle & CharStyle.Superscript) != (queryCharFormat.CharStyle & CharStyle.Superscript))
        {
          queryCharFormat.CharStyle &= ~CharStyle.Superscript;
          queryCharFormat.UndefinedCharStyles |= CharStyle.Superscript;
        }
        if ((queryCharFormat.UndefinedCharStyles & CharStyle.Subscript) == CharStyle.Regular && (charFormat1.CharStyle & CharStyle.Subscript) != (queryCharFormat.CharStyle & CharStyle.Subscript))
        {
          queryCharFormat.CharStyle &= ~CharStyle.Subscript;
          queryCharFormat.UndefinedCharStyles |= CharStyle.Subscript;
        }
        if ((queryCharFormat.UndefinedCharStyles & CharStyle.HiddenText) == CharStyle.Regular && (charFormat1.CharStyle & CharStyle.HiddenText) != (queryCharFormat.CharStyle & CharStyle.HiddenText))
        {
          queryCharFormat.CharStyle &= ~CharStyle.HiddenText;
          queryCharFormat.UndefinedCharStyles |= CharStyle.HiddenText;
        }
        if ((queryCharFormat.UndefinedCharStyles & CharStyle.AllSmallCaps) == CharStyle.Regular && (charFormat1.CharStyle & CharStyle.AllSmallCaps) != (queryCharFormat.CharStyle & CharStyle.AllSmallCaps))
        {
          queryCharFormat.CharStyle &= ~CharStyle.AllSmallCaps;
          queryCharFormat.UndefinedCharStyles |= CharStyle.AllSmallCaps;
        }
        if ((queryCharFormat.UndefinedCharStyles & CharStyle.AllCaps) != CharStyle.Regular || (charFormat1.CharStyle & CharStyle.AllCaps) == (queryCharFormat.CharStyle & CharStyle.AllCaps))
          return;
        queryCharFormat.CharStyle &= ~CharStyle.AllCaps;
        queryCharFormat.UndefinedCharStyles |= CharStyle.AllCaps;
      }
    }
  }

  private TextData SaveCharFormat(
    IList<DocumentTreeNode> context,
    CharFormat charFormat,
    bool firstLoad)
  {
    if (charFormat == null)
      return (TextData) null;
    TextData textData1 = (TextData) null;
    if (context == null || context.Count <= 0)
      return (TextData) null;
    for (int index = 0; index < context.Count; ++index)
    {
      if (context[index] is RectangleElement rectangleElement)
        rectangleElement.NeedUpdateFormulas = true;
    }
    if (this._queryTern == null)
      this.InitQueryCache();
    bool flag1 = this._queryTern != null && this._queryTern.text != null && this._queryFirstLineSelection >= 0 && this._queryTern.text[this._queryFirstLineSelection].txt != null && this._queryFirstLineSelection == this._queryEndLineSelection && this._queryFirstColSelection == this._queryEndColSelection && this._queryFirstColSelection < this._queryTern.text[this._queryFirstLineSelection].len - 1 && this._queryFirstColSelection > 0 && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection - 1]) && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection + 1]);
    if (this.Document != null && this.Document.UndoManager != null)
      this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_583"));
    try
    {
      SelectionBlock selectionBlock = this._queryTern?.GetSelectionBlock();
      bool flag2 = selectionBlock == null || selectionBlock.EndPos == selectionBlock.StartPos;
      Color? nullable1;
      int num1;
      if (this._queryIsAllTextSelected || flag2 && this._queryIsProtectedZone)
      {
        nullable1 = charFormat.TextBkColorForUser;
        if (nullable1.HasValue)
        {
          nullable1 = charFormat.TextBkColorForUser;
          Color white = Color.White;
          if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == white ? 1 : 0) : 1) : 0) == 0)
          {
            nullable1 = charFormat.TextBkColorForUser;
            Color transparent = Color.Transparent;
            num1 = nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == transparent ? 1 : 0) : 1) : 0;
            goto label_20;
          }
        }
        num1 = 1;
      }
      else
        num1 = 0;
label_20:
      bool flag3 = num1 != 0;
      if (flag1 | flag3 && this._queryTern != null)
      {
        if (flag3)
        {
          this._queryTern.SelectAll(false);
        }
        else
        {
          if (flag1)
            this._queryTern.ctl.OnDoubleClick(new EventArgs());
          flag3 = this._queryTern.IsAllTextSelected();
        }
      }
      ImRtfEditor queryTern1 = this._queryTern;
      if (this._queryTern != null)
      {
        CharFormat charFormat1 = ((TextData) context[0] ?? new TextData()).CharFormat;
        UnderlineStyle? underline1 = charFormat.Underline;
        UnderlineStyle underlineStyle = UnderlineStyle.None;
        if (!(underline1.GetValueOrDefault() == underlineStyle & underline1.HasValue))
        {
          nullable1 = charFormat.UnderlineColor;
          if (nullable1.HasValue)
          {
            ImRtfEditor queryTern2 = this._queryTern;
            nullable1 = charFormat.UnderlineColor;
            Color color = nullable1.Value;
            queryTern2.TerSetUlineColor(false, color, false);
          }
        }
        this._queryTern = queryTern1;
        if (charFormat.FontFamily != null)
          this._queryTern?.SetTerFont(charFormat.FontFamily, false);
        this._queryTern = queryTern1;
        if (charFormat.FontSize.HasValue)
        {
          ImRtfEditor queryTern3 = this._queryTern;
          if (queryTern3 != null)
          {
            float? nullable2 = charFormat.FontSize;
            float? nullable3 = nullable2.HasValue ? new float?(-nullable2.GetValueOrDefault()) : new float?();
            float num2 = 20f;
            float? nullable4;
            if (!nullable3.HasValue)
            {
              nullable2 = new float?();
              nullable4 = nullable2;
            }
            else
              nullable4 = new float?(nullable3.GetValueOrDefault() * num2);
            nullable2 = nullable4;
            queryTern3.SetTerPointSize((int) nullable2.Value, false);
          }
        }
        this._queryTern = queryTern1;
        nullable1 = charFormat.TextColorForUser;
        if (nullable1.HasValue)
        {
          ImRtfEditor queryTern4 = this._queryTern;
          if (queryTern4 != null)
          {
            nullable1 = charFormat.TextColorForUser;
            queryTern4.SetTerColor(nullable1.Value, false);
          }
        }
        nullable1 = charFormat.TextBkColorForUser;
        if (nullable1.HasValue)
        {
          ImRtfEditor queryTern5 = this._queryTern;
          if (queryTern5 != null)
          {
            nullable1 = charFormat.TextBkColorForUser;
            queryTern5.SetTerBkColor(nullable1.Value, false);
          }
        }
        this._queryTern = queryTern1;
        this._queryTern?.SetTerCharStyle((int) charFormat.CharStyle, true, false);
        this._queryTern = queryTern1;
        int FmtType = 0;
        if (charFormat.BoldItalic.HasValue)
        {
          if ((charFormat.CharStyle & CharStyle.Italic) == CharStyle.Regular)
            FmtType |= 4;
          if ((charFormat.CharStyle & CharStyle.Bold) == CharStyle.Regular)
            FmtType |= 2;
        }
        bool? nullable5 = charFormat.AllCaps;
        if (nullable5.HasValue && (charFormat.CharStyle & CharStyle.AllCaps) == CharStyle.Regular)
          FmtType |= 65536 /*0x010000*/;
        nullable5 = charFormat.AllSmallCaps;
        if (nullable5.HasValue && (charFormat.CharStyle & CharStyle.AllSmallCaps) == CharStyle.Regular)
          FmtType |= 131072 /*0x020000*/;
        StrikeoutLineStyle? strike = charFormat.Strike;
        if (strike.HasValue)
        {
          strike = charFormat.Strike;
          if ((strike.Value & StrikeoutLineStyle.SingleLine) == StrikeoutLineStyle.None)
            FmtType |= 8;
        }
        strike = charFormat.Strike;
        if (strike.HasValue)
        {
          strike = charFormat.Strike;
          if ((strike.Value & StrikeoutLineStyle.DoubleLine) == StrikeoutLineStyle.None)
            FmtType |= 524288 /*0x080000*/;
        }
        nullable5 = charFormat.Subscript;
        if (nullable5.HasValue && (charFormat.CharStyle & CharStyle.Subscript) == CharStyle.Regular)
          FmtType |= 32 /*0x20*/;
        nullable5 = charFormat.Superscript;
        if (nullable5.HasValue && (charFormat.CharStyle & CharStyle.Superscript) == CharStyle.Regular)
          FmtType |= 16 /*0x10*/;
        UnderlineStyle? underline2 = charFormat.Underline;
        if (underline2.HasValue)
        {
          underline2 = charFormat.Underline;
          if ((underline2.Value & UnderlineStyle.Underline) == UnderlineStyle.None)
            FmtType |= 1;
        }
        underline2 = charFormat.Underline;
        if (underline2.HasValue)
        {
          underline2 = charFormat.Underline;
          if ((underline2.Value & UnderlineStyle.DoubleUnderline) == UnderlineStyle.None)
            FmtType |= 256 /*0x0100*/;
        }
        nullable5 = charFormat.HiddenText;
        if (nullable5.HasValue && (charFormat.CharStyle & CharStyle.HiddenText) == CharStyle.Regular)
          FmtType |= 64 /*0x40*/;
        this._queryTern?.SetTerCharStyle(FmtType, false, false);
        this._queryTern = queryTern1;
        if (flag1 | flag3)
        {
          this._queryTern?.DeselectTerText(selectionBlock == null);
          if (selectionBlock != null)
            this._queryTern?.RestoreSelection(selectionBlock, true);
        }
        this._queryTern = queryTern1;
        this._queryTern?.TerRepaint(false);
      }
      if (flag3 || this._queryTern == null)
      {
        for (int index = 0; index < context.Count; ++index)
        {
          TextData textData2 = this.SaveCharFormat(context[index], charFormat, firstLoad);
          if (textData2 != null)
            textData1 = textData2;
        }
      }
      if (firstLoad && this.DocumentControl != null && this.DocumentControl.ActivePage != null)
      {
        if (this._queryTern != null)
          this.DocumentControl.EditorValidating(new CancelEventArgs());
        this.DocumentControl.ActivePage.UpdateLayout(true);
      }
      if (context[0] is TextBoxElement textBoxElement)
      {
        if (textBoxElement.InPlaceEditorActive)
        {
          ImRtfEditor placeEditorControl = textBoxElement.InPlaceEditorControl as ImRtfEditor;
          textBoxElement.NeedUpdateFormulas = true;
          textBoxElement.TextBox.ReplaceSpecSymbolAndFormulas(placeEditorControl, textBoxElement.ReplaceOldAVSSpecChars, true, true, textBoxElement.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false), out List<int> _);
          placeEditorControl.page.Repaginate(false, false, 0, false);
          textBoxElement.RefreshUI();
        }
      }
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
      this.UpdateFormatCommands();
      for (int index = 0; index < context.Count; ++index)
      {
        if (context[index] is RectangleElement rectangleElement)
          rectangleElement.NeedUpdateFormulas = false;
      }
    }
    return textData1;
  }

  private TextData SaveCharFormat(DocumentTreeNode context, CharFormat charFormat, bool firstLoad)
  {
    if (charFormat == null)
      return (TextData) null;
    if (!(context is ImDocument imDocument) && context is Intermech.Document.Model.Page)
      imDocument = (context as Intermech.Document.Model.Page).Parent as ImDocument;
    if (context == null)
      return (TextData) null;
    if (imDocument == null && context.NodesCount > 0)
      this.SaveCharFormat((IList<DocumentTreeNode>) context.Nodes, charFormat, false);
    else if (context is TextData || imDocument != null)
    {
      TextData textData = context as TextData;
      CharFormat charFormat1 = textData == null ? imDocument?.DefaultCharFormat : textData.CharFormat;
      CharFormat charFormat2 = charFormat1.Clone();
      bool flag = false;
      if (charFormat.FontFamily != null && charFormat1.FontFamily != charFormat.FontFamily)
      {
        charFormat2.FontFamily = charFormat.FontFamily;
        flag = true;
      }
      BoldItalicStyle? boldItalic1 = charFormat.BoldItalic;
      BoldItalicStyle? boldItalic2;
      if (boldItalic1.HasValue)
      {
        boldItalic1 = charFormat1.BoldItalic;
        boldItalic2 = charFormat.BoldItalic;
        if (!(boldItalic1.GetValueOrDefault() == boldItalic2.GetValueOrDefault() & boldItalic1.HasValue == boldItalic2.HasValue))
        {
          charFormat2.BoldItalic = charFormat.BoldItalic;
          flag = true;
        }
      }
      float? nullable1 = charFormat.FontSize;
      float? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = charFormat1.FontSize;
        nullable2 = charFormat.FontSize;
        if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          charFormat2.FontSize = charFormat.FontSize;
          flag = true;
        }
      }
      Color? nullable3 = charFormat.TextColor;
      Color? nullable4;
      if (nullable3.HasValue)
      {
        nullable3 = charFormat1.TextColor;
        nullable4 = charFormat.TextColor;
        if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          charFormat2.TextColor = charFormat.TextColor;
          flag = true;
        }
      }
      nullable4 = charFormat.TextBkColor;
      if (nullable4.HasValue)
      {
        nullable4 = charFormat1.TextBkColor;
        nullable3 = charFormat.TextBkColor;
        if ((nullable4.HasValue == nullable3.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          charFormat2.TextBkColor = charFormat.TextBkColor;
          flag = true;
        }
      }
      UnderlineStyle? underline1 = charFormat.Underline;
      UnderlineStyle? underline2;
      if (underline1.HasValue)
      {
        underline1 = charFormat1.Underline;
        underline2 = charFormat.Underline;
        if (!(underline1.GetValueOrDefault() == underline2.GetValueOrDefault() & underline1.HasValue == underline2.HasValue))
        {
          charFormat2.Underline = charFormat.Underline;
          flag = true;
        }
      }
      nullable3 = charFormat.UnderlineColor;
      if (nullable3.HasValue)
      {
        nullable3 = charFormat1.UnderlineColor;
        nullable4 = charFormat.UnderlineColor;
        if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          charFormat2.UnderlineColor = charFormat.UnderlineColor;
          flag = true;
        }
      }
      int? zoom1 = charFormat.Zoom;
      if (zoom1.HasValue)
      {
        zoom1 = charFormat1.Zoom;
        int? zoom2 = charFormat.Zoom;
        if (!(zoom1.GetValueOrDefault() == zoom2.GetValueOrDefault() & zoom1.HasValue == zoom2.HasValue))
        {
          charFormat2.Zoom = charFormat.Zoom;
          flag = true;
        }
      }
      nullable2 = charFormat.Interval;
      if (nullable2.HasValue)
      {
        nullable2 = charFormat1.Interval;
        nullable1 = charFormat.Interval;
        if (!((double) nullable2.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          charFormat2.Interval = charFormat.Interval;
          flag = true;
        }
      }
      nullable1 = charFormat.Displacement;
      if (nullable1.HasValue)
      {
        nullable1 = charFormat1.Displacement;
        nullable2 = charFormat.Displacement;
        if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          charFormat2.Displacement = charFormat.Displacement;
          flag = true;
        }
      }
      bool? nullable5 = charFormat.Superscript;
      bool? nullable6;
      if (nullable5.HasValue)
      {
        nullable5 = charFormat1.Superscript;
        nullable6 = charFormat.Superscript;
        if (!(nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue))
        {
          charFormat2.Superscript = charFormat.Superscript;
          flag = true;
        }
      }
      nullable6 = charFormat.Subscript;
      if (nullable6.HasValue)
      {
        nullable6 = charFormat1.Subscript;
        nullable5 = charFormat.Subscript;
        if (!(nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue))
        {
          charFormat2.Subscript = charFormat.Subscript;
          flag = true;
        }
      }
      StrikeoutLineStyle? strike1 = charFormat.Strike;
      StrikeoutLineStyle? strike2;
      if (strike1.HasValue)
      {
        strike1 = charFormat1.Strike;
        strike2 = charFormat.Strike;
        if (!(strike1.GetValueOrDefault() == strike2.GetValueOrDefault() & strike1.HasValue == strike2.HasValue))
        {
          charFormat2.Strike = charFormat.Strike;
          flag = true;
        }
      }
      nullable5 = charFormat.HiddenText;
      if (nullable5.HasValue)
      {
        nullable5 = charFormat1.HiddenText;
        nullable6 = charFormat.HiddenText;
        if (!(nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue))
        {
          charFormat2.HiddenText = charFormat.HiddenText;
          flag = true;
        }
      }
      nullable6 = charFormat.AllSmallCaps;
      if (nullable6.HasValue)
      {
        nullable6 = charFormat1.AllSmallCaps;
        nullable5 = charFormat.AllSmallCaps;
        if (!(nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue))
        {
          charFormat2.AllSmallCaps = charFormat.AllSmallCaps;
          flag = true;
        }
      }
      nullable5 = charFormat.AllCaps;
      if (nullable5.HasValue)
      {
        nullable5 = charFormat1.AllCaps;
        nullable6 = charFormat.AllCaps;
        if (!(nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue))
        {
          charFormat2.AllCaps = charFormat.AllCaps;
          flag = true;
        }
      }
      if (flag)
      {
        string rtfText = "";
        if (textData is TextBoxElement)
        {
          TextBoxElement textBoxElement = textData as TextBoxElement;
          ImRtfEditor imRtfEditor = (ImRtfEditor) null;
          if (textBoxElement != null && textBoxElement.OwnerDocument != null)
            imRtfEditor = (textBoxElement.OwnerDocument as ImDocument).TernPaintBuffer;
          if (imRtfEditor != null && textBoxElement != null && !textBoxElement.IsEmptyText && textBoxElement.Rtf != null)
          {
            rtfText = textBoxElement.Rtf;
            if (textBoxElement.TextBox.EditorActive)
              textBoxElement.TextBox.GetActualText(out string _, out rtfText, true);
          }
        }
        if (textData != null)
          textData.SetCharFormat(charFormat2, false, false);
        else if (imDocument != null)
          imDocument.DefaultCharFormat = charFormat2;
        if (textData is TextBoxElement)
        {
          TextBoxElement textBoxElement = textData as TextBoxElement;
          ImRtfEditor editor = (ImRtfEditor) null;
          if (textBoxElement != null && textBoxElement.OwnerDocument != null)
            editor = (textBoxElement.OwnerDocument as ImDocument).TernPaintBuffer;
          if (editor != null && textBoxElement != null && !string.IsNullOrEmpty(rtfText))
          {
            Rectangle editorBounds;
            ref Rectangle local = ref editorBounds;
            int left = (int) textBoxElement.Bounds.Left;
            int top = (int) textBoxElement.Bounds.Top;
            RectangleF bounds = textBoxElement.Bounds;
            int width = (int) bounds.Width;
            bounds = textBoxElement.Bounds;
            int height = (int) bounds.Height;
            local = new Rectangle(left, top, width, height);
            textBoxElement.TextBox.SetupEditor(editor, rtfText, true, textBoxElement.StartCharIndex, textBoxElement.ParagraphFormat, textBoxElement.Orientation, textBoxElement.CharFormat, textBoxElement.BackColor, textBoxElement.Bounds, editorBounds, new MarginsF(textBoxElement.LeftMargin, textBoxElement.RightMargin, textBoxElement.TopMargin, textBoxElement.BottomMargin), 1f, textBoxElement.DefaultRowSize);
            editor.SelectAll(false);
            underline2 = charFormat.Underline;
            UnderlineStyle underlineStyle = UnderlineStyle.None;
            if (!(underline2.GetValueOrDefault() == underlineStyle & underline2.HasValue))
            {
              nullable4 = charFormat.UnderlineColor;
              if (nullable4.HasValue)
              {
                ImRtfEditor imRtfEditor = editor;
                nullable4 = charFormat.UnderlineColor;
                Color color = nullable4.Value;
                imRtfEditor.TerSetUlineColor(false, color, false);
              }
            }
            if (charFormat.FontFamily != null)
              editor.SetTerFont(charFormat.FontFamily, false);
            nullable2 = charFormat.FontSize;
            if (nullable2.HasValue)
            {
              ImRtfEditor imRtfEditor = editor;
              nullable1 = charFormat.FontSize;
              nullable2 = nullable1.HasValue ? new float?(-nullable1.GetValueOrDefault()) : new float?();
              float num = 20f;
              float? nullable7;
              if (!nullable2.HasValue)
              {
                nullable1 = new float?();
                nullable7 = nullable1;
              }
              else
                nullable7 = new float?(nullable2.GetValueOrDefault() * num);
              int int32 = Convert.ToInt32((object) nullable7);
              imRtfEditor.SetTerPointSize(int32, false);
            }
            nullable4 = charFormat.TextColorForUser;
            if (nullable4.HasValue)
            {
              ImRtfEditor imRtfEditor = editor;
              nullable4 = charFormat.TextColorForUser;
              Color color = nullable4.Value;
              imRtfEditor.SetTerColor(color, false);
            }
            nullable4 = charFormat.TextBkColorForUser;
            if (nullable4.HasValue)
            {
              ImRtfEditor imRtfEditor = editor;
              nullable4 = charFormat.TextBkColorForUser;
              Color color = nullable4.Value;
              imRtfEditor.SetTerBkColor(color, false);
            }
            boldItalic2 = charFormat.BoldItalic;
            if (!boldItalic2.HasValue)
            {
              strike2 = charFormat.Strike;
              if (!strike2.HasValue)
              {
                underline2 = charFormat.Underline;
                if (!underline2.HasValue)
                  goto label_89;
              }
            }
            editor.SetTerCharStyle((int) charFormat.CharStyle, true, false);
label_89:
            int FmtType = 0;
            boldItalic2 = charFormat.BoldItalic;
            if (boldItalic2.HasValue)
            {
              if ((charFormat.CharStyle & CharStyle.Italic) == CharStyle.Regular)
                FmtType |= 4;
              if ((charFormat.CharStyle & CharStyle.Bold) == CharStyle.Regular)
                FmtType |= 2;
            }
            nullable6 = charFormat.AllCaps;
            if (nullable6.HasValue && (charFormat.CharStyle & CharStyle.AllCaps) == CharStyle.Regular)
              FmtType |= 65536 /*0x010000*/;
            nullable6 = charFormat.AllSmallCaps;
            if (nullable6.HasValue && (charFormat.CharStyle & CharStyle.AllSmallCaps) == CharStyle.Regular)
              FmtType |= 131072 /*0x020000*/;
            strike2 = charFormat.Strike;
            if (strike2.HasValue && (charFormat.CharStyle & CharStyle.Strikethrough) == CharStyle.Regular)
              FmtType |= 8;
            nullable6 = charFormat.Subscript;
            if (nullable6.HasValue && (charFormat.CharStyle & CharStyle.Subscript) == CharStyle.Regular)
              FmtType |= 32 /*0x20*/;
            nullable6 = charFormat.Superscript;
            if (nullable6.HasValue && (charFormat.CharStyle & CharStyle.Superscript) == CharStyle.Regular)
              FmtType |= 16 /*0x10*/;
            underline2 = charFormat.Underline;
            if (underline2.HasValue)
            {
              underline2 = charFormat.Underline;
              if (underline2.HasValue)
              {
                underline2 = charFormat.Underline;
                if ((underline2.Value & UnderlineStyle.Underline) == UnderlineStyle.None)
                  FmtType |= 1;
              }
            }
            underline2 = charFormat.Underline;
            if (underline2.HasValue)
            {
              underline2 = charFormat.Underline;
              if (underline2.HasValue)
              {
                underline2 = charFormat.Underline;
                if ((underline2.Value & UnderlineStyle.DoubleUnderline) == UnderlineStyle.None)
                  FmtType |= 256 /*0x0100*/;
              }
            }
            nullable6 = charFormat.HiddenText;
            if (nullable6.HasValue && (charFormat.CharStyle & CharStyle.HiddenText) == CharStyle.Regular)
              FmtType |= 64 /*0x40*/;
            editor.SetTerCharStyle(FmtType, false, false);
            textBoxElement.AssignText(textBoxElement.Text, editor.RtfText, true, false, false);
          }
        }
      }
      return textData;
    }
    return (TextData) null;
  }

  /// <summary>Применение регистра</summary>
  /// <param name="context"></param>
  /// <param name="command"></param>
  /// <param name="firstLoad"></param>
  /// <returns></returns>
  private TextData ApplyRegistr(IList<DocumentTreeNode> context, string command, bool firstLoad)
  {
    TextData textData = (TextData) null;
    if (context == null || context.Count <= 0)
      return (TextData) null;
    bool flag = this._queryTern != null && this._queryFirstLineSelection >= 0 && this._queryFirstLineSelection == this._queryEndLineSelection && this._queryFirstColSelection == this._queryEndColSelection && this._queryFirstColSelection < this._queryTern.text[this._queryFirstLineSelection].len - 1 && this._queryFirstColSelection > 0 && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection - 1]) && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection + 1]);
    SelectionBlock block = (SelectionBlock) null;
    if (this.Document != null && this.Document.UndoManager != null)
      this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_602"));
    try
    {
      if (flag)
      {
        block = this._queryTern?.GetSelectionBlock();
        this._queryTern?.ctl.OnDoubleClick(new EventArgs());
      }
      ImRtfEditor queryTern = this._queryTern;
      if (this._queryIsAllTextSelected || this._queryTern == null)
      {
        for (int index = 0; index < context.Count; ++index)
          this.ApplyRegistr(context[index], command, firstLoad);
      }
      else if (queryTern != null)
      {
        this.SetRegistr(command, this._queryTern);
        this._queryTern = queryTern;
        if (flag)
        {
          this._queryTern.DeselectTerText(block == null);
          if (block != null)
            this._queryTern.RestoreSelection(block, true);
        }
        this._queryTern = queryTern;
        this._queryTern.TerRepaint(false);
      }
      if (firstLoad)
      {
        if (this.DocumentControl != null)
        {
          if (this.DocumentControl.ActivePage != null)
            this.DocumentControl.ActivePage.UpdateLayout(true);
        }
      }
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
    }
    return textData;
  }

  /// <summary>Применение регистра</summary>
  /// <param name="context"></param>
  /// <param name="command"></param>
  /// <param name="firstLoad"></param>
  /// <returns></returns>
  private void ApplyRegistr(DocumentTreeNode context, string command, bool firstLoad)
  {
    if (context == null)
      return;
    if (context.NodesCount > 0)
    {
      this.ApplyRegistr((IList<DocumentTreeNode>) context.Nodes, command, false);
    }
    else
    {
      if (!(context is TextData) || !(context is TextData textData))
        return;
      TextBoxElement owner = textData as TextBoxElement;
      ImRtfEditor editor = (ImRtfEditor) null;
      if (owner != null)
        editor = !owner.InPlaceEditorActive || owner.TextBox == null ? (owner.OwnerDocument as ImDocument).TernPaintBuffer : owner.TextBox.Editor;
      if (editor != null)
      {
        if (owner != null && !owner.InPlaceEditorActive)
        {
          Rectangle editorBounds = new Rectangle((int) owner.Bounds.Left, (int) owner.Bounds.Top, (int) owner.Bounds.Width, (int) owner.Bounds.Height);
          if (owner.TextBox == null)
            owner.TextBox = new RtfInSiteEditorWrapper((TextData) owner);
          if (!owner.IsEmptyText && owner.Rtf != null)
            owner.TextBox.SetupEditor(editor, owner.Rtf, true, owner.StartCharIndex, owner.ParagraphFormat, owner.Orientation, owner.CharFormat, owner.BackColor, owner.Bounds, editorBounds, new MarginsF(owner.LeftMargin, owner.RightMargin, owner.TopMargin, owner.BottomMargin), 1f, owner.DefaultRowSize);
          else
            owner.TextBox.SetupEditor(editor, owner.Text, false, owner.StartCharIndex, owner.ParagraphFormat, owner.Orientation, owner.CharFormat, owner.BackColor, owner.Bounds, editorBounds, new MarginsF(owner.LeftMargin, owner.RightMargin, owner.TopMargin, owner.BottomMargin), 1f, owner.DefaultRowSize);
          editor.SelectAll(false);
        }
        this.SetRegistr(command, editor);
        if (owner == null || owner.InPlaceEditorActive)
          return;
        owner.AssignText(owner.Text, editor.RtfText, true, false, false);
      }
      else
      {
        string text = textData.Text;
        string str = this.SetRegistr(command, text);
        textData.AssignText(str, false, true, true, false, false);
      }
    }
  }

  /// <summary>Установка регистра в строке</summary>
  /// <param name="command"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  private string SetRegistr(string command, string text)
  {
    bool flag1 = command == "Format.Font.Registr.LowerCase";
    bool flag2 = command == "Format.Font.Registr.UpperCase";
    bool flag3 = command == "Format.Font.Registr.BeginFromUpperCase";
    bool flag4 = command == "Format.Font.Registr.Invert";
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < text.Length; ++index)
    {
      char c = text[index];
      char ch = c;
      bool flag5 = true;
      if (index != 0)
        flag5 = !char.IsLetterOrDigit(text[index - 1]);
      if (flag1)
        ch = char.ToLower(c);
      if (flag2)
        ch = char.ToUpper(c);
      if (flag3)
        ch = !flag5 ? char.ToLower(c) : char.ToUpper(c);
      if (flag4)
        ch = char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c);
      stringBuilder.Append(ch);
    }
    return stringBuilder.ToString();
  }

  /// <summary>Установка регистра с редакторе</summary>
  /// <param name="command"></param>
  /// <param name="editor"></param>
  private void SetRegistr(string command, ImRtfEditor editor)
  {
    SelectionBlock selectionBlock = editor.GetSelectionBlock();
    bool flag1 = command == "Format.Font.Registr.LowerCase";
    bool flag2 = command == "Format.Font.Registr.UpperCase";
    bool flag3 = command == "Format.Font.Registr.BeginFromUpperCase";
    bool flag4 = command == "Format.Font.Registr.Invert";
    char c1 = char.MinValue;
    int LastLine = selectionBlock.StartPos < selectionBlock.EndPos ? selectionBlock.StartPos : selectionBlock.EndPos;
    int num = selectionBlock.StartPos < selectionBlock.EndPos ? selectionBlock.EndPos : selectionBlock.StartPos;
    if (LastLine != 0)
    {
      editor.SelectTerText(LastLine - 1, -1, LastLine, -1, false);
      string textSel = editor.TerGetTextSel();
      if (textSel != null && textSel.Length > 0)
        c1 = textSel[0];
    }
    for (int FirstLine = LastLine; FirstLine < num; ++FirstLine)
    {
      editor.SelectTerText(FirstLine, -1, FirstLine + 1, -1, false);
      string textSel = editor.TerGetTextSel();
      if (textSel != null && textSel.Length > 0)
      {
        char c2 = textSel[0];
        char c3 = c2;
        bool flag5 = true;
        if (c1 != char.MinValue)
          flag5 = !char.IsLetterOrDigit(c1);
        c1 = c2;
        if (flag1)
          c3 = char.ToLower(c2);
        if (flag2)
          c3 = char.ToUpper(c2);
        if (flag3)
          c3 = !flag5 ? char.ToLower(c2) : char.ToUpper(c2);
        if (flag4)
          c3 = char.IsUpper(c2) ? char.ToLower(c2) : char.ToUpper(c2);
        editor.TerSetTextCase(char.IsUpper(c3), false);
      }
    }
    editor.RestoreSelection(selectionBlock, false);
  }

  private void QueryParagraphFormat(
    IList<DocumentTreeNode> context,
    ref ParagraphFormat queryParagraphFormat)
  {
    if (this._queryTern == null)
    {
      bool firstLoad = true;
      this.QueryParagraphFormat(context, ref queryParagraphFormat, ref firstLoad);
    }
    else
    {
      if (queryParagraphFormat == null)
        queryParagraphFormat = new ParagraphFormat(true);
      queryParagraphFormat.DisableFloatLines = new bool?((this._queryFlags & 32 /*0x20*/) != 0);
      queryParagraphFormat.DisableWordWrap = new bool?((this._queryFlags & 16 /*0x10*/) != 0);
      queryParagraphFormat.KeepTogether = new bool?((this._queryFlags & 16384 /*0x4000*/) != 0);
      queryParagraphFormat.KeepWithNext = new bool?((this._queryFlags & 32768 /*0x8000*/) != 0);
      queryParagraphFormat.FromNewPage = new bool?((this._queryFlags & 64 /*0x40*/) != 0);
      if (this._queryDisableFloatLines.HasValue)
        queryParagraphFormat.DisableFloatLines = this._queryDisableFloatLines;
      if (this._queryDisableWordWrap.HasValue)
        queryParagraphFormat.DisableWordWrap = this._queryDisableWordWrap;
      if (this._queryKeepTogether.HasValue)
        queryParagraphFormat.KeepTogether = this._queryKeepTogether;
      if (this._queryKeepWithNext.HasValue)
        queryParagraphFormat.KeepWithNext = this._queryKeepWithNext;
      if (this._queryFromNewPage.HasValue)
        queryParagraphFormat.FromNewPage = this._queryFromNewPage;
      if (this._queryHorzAlignment.HasValue)
        queryParagraphFormat.HorzAlignment = this._queryHorzAlignment;
      if (this._queryFirstIndent.HasValue)
        queryParagraphFormat.IdentFirstLine = new float?(UnitsConverter.TwipsToMm((float) this._queryFirstIndent.Value) / 10f);
      if (this._queryLeftIndent.HasValue)
        queryParagraphFormat.IdentLeft = new float?(UnitsConverter.TwipsToMm((float) this._queryLeftIndent.Value) / 10f);
      if (this._queryRigthIndent.HasValue)
        queryParagraphFormat.IdentRight = new float?(UnitsConverter.TwipsToMm((float) this._queryRigthIndent.Value) / 10f);
      int? nullable1;
      if (this._querySpaceAfter.HasValue)
      {
        ParagraphFormat paragraphFormat = queryParagraphFormat;
        nullable1 = this._querySpaceAfter;
        float? nullable2 = nullable1.HasValue ? new float?((float) (nullable1.GetValueOrDefault() / 20)) : new float?();
        paragraphFormat.IntervalAfter = nullable2;
      }
      if (this._querySpaceBefore.HasValue)
      {
        ParagraphFormat paragraphFormat = queryParagraphFormat;
        nullable1 = this._querySpaceBefore;
        float? nullable3 = nullable1.HasValue ? new float?((float) (nullable1.GetValueOrDefault() / 20)) : new float?();
        paragraphFormat.IntervalBefore = nullable3;
      }
      ParagraphFormat paragraphFormat1 = (ParagraphFormat) null;
      if (context.Count <= 0)
        return;
      if (context[0] is TextData)
        paragraphFormat1 = (context[0] as TextData).ParagraphFormat.Clone();
      nullable1 = this._queryLineSpacing;
      if (nullable1.HasValue)
      {
        switch (nullable1.GetValueOrDefault())
        {
          case 0:
            int? querySpaceBetween = this._querySpaceBetween;
            int num1 = 0;
            if (querySpaceBetween.GetValueOrDefault() == num1 & querySpaceBetween.HasValue)
            {
              queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1);
              return;
            }
            querySpaceBetween = this._querySpaceBetween;
            int num2 = 0;
            LineSpacingMethod? lineSpacingMethod1;
            if (querySpaceBetween.GetValueOrDefault() > num2 & querySpaceBetween.HasValue && paragraphFormat1 != null)
            {
              lineSpacingMethod1 = paragraphFormat1.LineSpacingMethod;
              LineSpacingMethod lineSpacingMethod2 = LineSpacingMethod.AtLeastMM;
              if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod2 & lineSpacingMethod1.HasValue))
              {
                lineSpacingMethod1 = paragraphFormat1.LineSpacingMethod;
                LineSpacingMethod lineSpacingMethod3 = LineSpacingMethod.ExactMM;
                if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod3 & lineSpacingMethod1.HasValue))
                {
                  queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeast);
                  ParagraphFormat paragraphFormat2 = queryParagraphFormat;
                  querySpaceBetween = this._querySpaceBetween;
                  float? nullable4 = querySpaceBetween.HasValue ? new float?((float) (querySpaceBetween.GetValueOrDefault() / 20)) : new float?();
                  paragraphFormat2.SpaceBetweenLines = nullable4;
                  goto label_39;
                }
              }
              queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              queryParagraphFormat.SpaceBetweenLines = new float?(UnitsConverter.TwipsToMm((float) this._querySpaceBetween.Value));
            }
label_39:
            querySpaceBetween = this._querySpaceBetween;
            int num3 = 0;
            if (!(querySpaceBetween.GetValueOrDefault() < num3 & querySpaceBetween.HasValue) || paragraphFormat1 == null)
              return;
            lineSpacingMethod1 = paragraphFormat1.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod4 = LineSpacingMethod.AtLeastMM;
            if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod4 & lineSpacingMethod1.HasValue))
            {
              lineSpacingMethod1 = paragraphFormat1.LineSpacingMethod;
              LineSpacingMethod lineSpacingMethod5 = LineSpacingMethod.ExactMM;
              if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod5 & lineSpacingMethod1.HasValue))
              {
                queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Exact);
                ParagraphFormat paragraphFormat3 = queryParagraphFormat;
                querySpaceBetween = this._querySpaceBetween;
                float? nullable5 = querySpaceBetween.HasValue ? new float?((float) -querySpaceBetween.GetValueOrDefault() / 20f) : new float?();
                paragraphFormat3.SpaceBetweenLines = nullable5;
                return;
              }
            }
            queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            queryParagraphFormat.SpaceBetweenLines = new float?(UnitsConverter.TwipsToMm((float) this._querySpaceBetween.Value));
            return;
          case 50:
            queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1_5);
            return;
          case 100:
            queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_2);
            return;
        }
      }
      queryParagraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio);
    }
  }

  private void QueryParagraphFormat(
    IList<DocumentTreeNode> context,
    ref ParagraphFormat queryParagraphFormat,
    ref bool firstLoad)
  {
    if (context == null || context.Count <= 0)
      return;
    for (int index = 0; index < context.Count; ++index)
      this.QueryParagraphFormat(context[index], ref queryParagraphFormat, ref firstLoad);
  }

  private void QueryParagraphFormat(
    DocumentTreeNode context,
    ref ParagraphFormat queryParagraphFormat,
    ref bool firstLoad)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return;
    if (context.NodesCount > 0)
    {
      this.QueryParagraphFormat((IList<DocumentTreeNode>) context.Nodes, ref queryParagraphFormat, ref firstLoad);
    }
    else
    {
      if (!(context is TextData))
        return;
      ParagraphFormat paragraphFormat = ((TextData) context).ParagraphFormat;
      if (firstLoad)
      {
        queryParagraphFormat = paragraphFormat.Clone();
        firstLoad = false;
      }
      else
        queryParagraphFormat.GetFields(paragraphFormat);
    }
  }

  [DllImport("gdiplus.dll", SetLastError = true)]
  private static extern int GdipEmfToWmfBits(
    int hEmf,
    int uBufferSize,
    byte[] bBuffer,
    int iMappingMode,
    ImDocumentEditorFormBase.EmfToWmfBitsFlags flags);

  public void SaveAsXLS(string filename)
  {
    List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
    if (this.DocumentsComplect != null)
      imDocumentDataList = this.DocumentsComplect.GetAllDocuments();
    else
      imDocumentDataList.Add((ImDocumentData) this.Document);
    ImDocument.SaveToXLS(imDocumentDataList.ToArray(), filename, true, true);
  }

  public void SaveAsPdf(string filename)
  {
    List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
    List<Metafile> metafileList = new List<Metafile>();
    PrintDocument printDocument;
    if (this.DocumentsComplect != null)
    {
      imDocumentDataList = this.DocumentsComplect.GetAllDocuments();
      printDocument = this.DocumentsComplect.PrintDocument;
    }
    else
    {
      imDocumentDataList.Add((ImDocumentData) this.Document);
      printDocument = this.Document.PrintDocument;
    }
    ImDocument.SaveToPdf(printDocument, imDocumentDataList.ToArray(), filename, showProgress: true);
  }

  /// <summary>Сохранить как файл в файловую систему</summary>
  /// <param name="fileName">Имя файла. Если до сохранения не null, то используется в диалоге как имя файла предлагаемое пользователю.
  /// После сохранения возвращает имя сохранённого файла</param>
  /// <returns>Возвращает true, если файл успешно сохранён</returns>
  public virtual bool SaveAsExecute(ref string fileName)
  {
    bool flag = false;
    if (this.Document != null)
    {
      if (fileName == null || fileName == "")
        fileName = this.RecentlySaveAsFileName;
      if (this.DocumentsComplect != null)
      {
        this.SaveToFileDialog.Filter = ImDocumentEditorFormBase.ImDocumentsComplectFilter;
        this.SaveToFileDialog.FileName = Path.ChangeExtension(fileName, !this.RecentlyPackedFile ? ".idcx" : ".zidc");
      }
      else
      {
        this.SaveToFileDialog.Filter = ImDocumentEditorFormBase.ImDocumentFilter;
        this.SaveToFileDialog.FileName = Path.ChangeExtension(fileName, !this.RecentlyPackedFile ? ".imdx" : ".zimd");
      }
      this.SaveToFileDialog.FileName = Path.GetFileNameWithoutExtension(this.SaveToFileDialog.FileName);
      this.SaveToFileDialog.Filter += LocalizationHolder.rm.GetString("Document.Model_623");
      this.SaveToFileDialog.Filter += LocalizationHolder.rm.GetString("Document.Model_668");
      if (this.SaveToFileDialog.ShowDialog() == DialogResult.OK)
      {
        fileName = this.SaveToFileDialog.FileName;
        if (new FileInfo(fileName).Extension == string.Empty)
        {
          string str = ImDocumentEditorFormBase.GetSelectedFileFilter(this.SaveToFileDialog.Filter, this.SaveToFileDialog.FilterIndex).TrimStart('*');
          fileName += str;
        }
        string selectedFileFilter = ImDocumentEditorFormBase.GetSelectedFileFilter(this.SaveToFileDialog.Filter, this.SaveToFileDialog.FilterIndex);
        if (selectedFileFilter.ToLower().Contains("pdf"))
        {
          this.SaveAsPdf(fileName);
          flag = true;
        }
        else if (selectedFileFilter.ToLower().Contains("xlsx"))
        {
          this.SaveAsXLS(fileName);
          flag = true;
        }
        else
        {
          this.RecentlySaveAsFileName = !(Path.GetExtension(this.SaveToFileDialog.FileName) == "") ? this.SaveToFileDialog.FileName : (this.DocumentsComplect == null ? Path.ChangeExtension(this.SaveToFileDialog.FileName, !this.RecentlyPackedFile ? ".imdx" : ".zimd") : Path.ChangeExtension(this.SaveToFileDialog.FileName, !this.RecentlyPackedFile ? ".idcx" : ".zidc"));
          bool packFile = selectedFileFilter.IndexOf(".zimd") != -1 || selectedFileFilter.IndexOf(".zidc") != -1;
          this.RecentlyPackedFile = packFile;
          if (this.documentManager != null)
            this.documentManager.RecentlySaveAsPath = Path.GetDirectoryName(this.RecentlySaveAsFileName) + "\\";
          fileName = this.RecentlySaveAsFileName;
          if (this.DocumentsComplect != null)
            this.DocumentsComplect.SaveToXml(fileName, packFile);
          else
            this.Document.SaveToXml(fileName, packFile);
          flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>Установить значение свойств интервал до, после и между выделенного текста</summary>
  /// <param name="paragraphFormat">свойства в обяекте класса paragraphFormat</param>
  /// <param name="tern">Терн на котором рисуем</param>
  /// <param name="needUpdate">Обновлять на экране</param>
  private void ApplyTextSpacing(ParagraphFormat paragraphFormat, ImRtfEditor tern, bool needUpdate)
  {
    if (tern == null)
      return;
    int SpaceBefore = paragraphFormat.IntervalBefore.HasValue ? (int) Math.Round((double) paragraphFormat.IntervalBefore.Value * 20.0) : -1;
    float? nullable = paragraphFormat.IntervalAfter;
    int num;
    if (!nullable.HasValue)
    {
      num = -1;
    }
    else
    {
      nullable = paragraphFormat.IntervalAfter;
      num = (int) Math.Round((double) nullable.Value * 20.0);
    }
    int SpaceAfter = num;
    int SpaceBetween = -9999;
    int LineSpacing = 0;
    LineSpacingMethod? lineSpacingMethod = paragraphFormat.LineSpacingMethod;
    if (lineSpacingMethod.HasValue)
    {
      lineSpacingMethod = paragraphFormat.LineSpacingMethod;
      if (lineSpacingMethod.HasValue)
      {
        switch (lineSpacingMethod.GetValueOrDefault())
        {
          case LineSpacingMethod.InPercents:
            nullable = paragraphFormat.SpaceBetweenLines;
            LineSpacing = (int) Math.Round((double) nullable.Value) - 100;
            break;
          case LineSpacingMethod.Ratio_1:
            SpaceBetween = 0;
            LineSpacing = 0;
            break;
          case LineSpacingMethod.Ratio_1_5:
            LineSpacing = 50;
            break;
          case LineSpacingMethod.Ratio_2:
            LineSpacing = 100;
            break;
          case LineSpacingMethod.AtLeast:
            nullable = paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = paragraphFormat.SpaceBetweenLines;
              SpaceBetween = (int) Math.Round((double) nullable.Value * 20.0);
              break;
            }
            break;
          case LineSpacingMethod.Exact:
            nullable = paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = paragraphFormat.SpaceBetweenLines;
              SpaceBetween = -(int) Math.Round((double) nullable.Value * 20.0);
              break;
            }
            break;
          case LineSpacingMethod.ExactMM:
            nullable = paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = paragraphFormat.SpaceBetweenLines;
              SpaceBetween = -(int) Math.Truncate((double) nullable.Value * 56.692913055419922);
              break;
            }
            break;
          case LineSpacingMethod.Ratio:
            nullable = paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = paragraphFormat.SpaceBetweenLines;
              LineSpacing = Convert.ToInt32((float) ((double) nullable.Value * 100.0 - 100.0));
              break;
            }
            break;
        }
      }
    }
    if (SpaceBefore == -1 && SpaceAfter == -1 && SpaceBetween == -9999 && LineSpacing == 0)
      return;
    tern.TerSetParaSpacing2(SpaceBefore, SpaceAfter, SpaceBetween, LineSpacing, needUpdate);
  }

  /// <summary>Установить значение свойств отступ слева, справа и и первой строки выделенного текста</summary>
  private void ApplyTextIdent(ParagraphFormat paragraphFormat, ImRtfEditor tern, bool needUpdate)
  {
    if (tern == null)
      return;
    int left = paragraphFormat.IdentLeft.HasValue ? UnitsConverter.MmToTwips(paragraphFormat.IdentLeft.Value * 10f) : -1;
    float? nullable = paragraphFormat.IdentRight;
    int num1;
    if (!nullable.HasValue)
    {
      num1 = -1;
    }
    else
    {
      nullable = paragraphFormat.IdentRight;
      num1 = UnitsConverter.MmToTwips(nullable.Value * 10f);
    }
    int right = num1;
    nullable = paragraphFormat.IdentFirstLine;
    int num2;
    if (!nullable.HasValue)
    {
      num2 = -1;
    }
    else
    {
      nullable = paragraphFormat.IdentFirstLine;
      num2 = UnitsConverter.MmToTwips(nullable.Value * 10f);
    }
    int first = num2;
    if (left == -1 && right == -1 && first == -1)
      return;
    tern.TerSetParaIndent(left, right, first, needUpdate);
  }

  private TextData SaveParagraphFormat(
    IList<DocumentTreeNode> context,
    ParagraphFormat paragraphFormat,
    bool firstLoad)
  {
    TextData textData1 = (TextData) null;
    if (context == null || context.Count <= 0)
      return (TextData) null;
    ImRtfEditor queryTern = this._queryTern;
    bool flag = true;
    if (this._queryTern != null)
      flag = this._queryTern.AllParagraphsSelected();
    this._queryTern = queryTern;
    if (this.Document != null && this.Document.UndoManager != null)
      this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_584"));
    try
    {
      if (((this._queryIsAllTextSelected ? 1 : (this._queryTern == null ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        for (int index = 0; index < context.Count; ++index)
          this.SaveParagraphFormat(context[index], paragraphFormat, firstLoad);
      }
      else if (queryTern != null)
      {
        TextData textData2 = (TextData) context[0];
        if (paragraphFormat.HorzAlignment.HasValue)
          this.SetTextHAlign(paragraphFormat.HorzAlignment.Value, this._queryTern, false);
        this._queryTern = queryTern;
        this.ApplyTextIdent(paragraphFormat, this._queryTern, false);
        this._queryTern = queryTern;
        this.ApplyTextSpacing(paragraphFormat, this._queryTern, false);
        this._queryTern = queryTern;
        int FmtType1 = 0;
        bool? nullable;
        if (paragraphFormat.DisableFloatLines.HasValue)
        {
          nullable = paragraphFormat.DisableFloatLines;
          if (nullable.Value)
            FmtType1 |= 32 /*0x20*/;
        }
        nullable = paragraphFormat.DisableWordWrap;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.DisableWordWrap;
          if (nullable.Value)
            FmtType1 |= 16 /*0x10*/;
        }
        nullable = paragraphFormat.KeepTogether;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.KeepTogether;
          if (nullable.Value)
            FmtType1 |= 16384 /*0x4000*/;
        }
        nullable = paragraphFormat.KeepWithNext;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.KeepWithNext;
          if (nullable.Value)
            FmtType1 |= 32768 /*0x8000*/;
        }
        nullable = paragraphFormat.FromNewPage;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.FromNewPage;
          if (nullable.Value)
            FmtType1 |= 64 /*0x40*/;
        }
        this._queryTern.TerSetPflags(FmtType1, true, false);
        this._queryTern = queryTern;
        this._queryTern.SetTerParaFmt(FmtType1, true, false);
        this._queryTern = queryTern;
        int FmtType2 = 0;
        nullable = paragraphFormat.DisableFloatLines;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.DisableFloatLines;
          if (!nullable.Value)
            FmtType2 |= 32 /*0x20*/;
        }
        nullable = paragraphFormat.DisableWordWrap;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.DisableWordWrap;
          if (!nullable.Value)
            FmtType2 |= 16 /*0x10*/;
        }
        nullable = paragraphFormat.KeepTogether;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.KeepTogether;
          if (!nullable.Value)
            FmtType2 |= 16384 /*0x4000*/;
        }
        nullable = paragraphFormat.KeepWithNext;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.KeepWithNext;
          if (!nullable.Value)
            FmtType2 |= 32768 /*0x8000*/;
        }
        nullable = paragraphFormat.FromNewPage;
        if (nullable.HasValue)
        {
          nullable = paragraphFormat.FromNewPage;
          if (!nullable.Value)
            FmtType2 |= 64 /*0x40*/;
        }
        this._queryTern.TerSetPflags(FmtType2, false, false);
        this._queryTern = queryTern;
        this._queryTern.SetTerParaFmt(FmtType2, false, false);
        this._queryTern = queryTern;
        this._queryTern.TerRepaint(false);
      }
      if (firstLoad)
      {
        if (this.DocumentControl != null)
        {
          if (this.DocumentControl.ActivePage != null)
            this.DocumentControl.ActivePage.UpdateLayout(true);
        }
      }
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
    }
    return textData1;
  }

  private TextData SaveParagraphFormat(
    DocumentTreeNode context,
    ParagraphFormat paragraphFormat,
    bool firstLoad)
  {
    TextData textData1 = (TextData) null;
    if (context == null || context is Intermech.Document.Model.Page)
      return (TextData) null;
    if (context.NodesCount > 0)
      textData1 = this.SaveParagraphFormat((IList<DocumentTreeNode>) context.Nodes, paragraphFormat, false);
    else if (context is TextData)
    {
      TextData textData2 = (TextData) context;
      ParagraphFormat paragraphFormat1 = textData2.ParagraphFormat;
      ParagraphFormat paragraphFormat2 = paragraphFormat1.Clone();
      bool flag = false;
      HorzAlignment? horzAlignment1 = paragraphFormat.HorzAlignment;
      HorzAlignment? horzAlignment2;
      if (horzAlignment1.HasValue)
      {
        horzAlignment1 = paragraphFormat1.HorzAlignment;
        horzAlignment2 = paragraphFormat.HorzAlignment;
        if (!(horzAlignment1.GetValueOrDefault() == horzAlignment2.GetValueOrDefault() & horzAlignment1.HasValue == horzAlignment2.HasValue))
        {
          paragraphFormat2.HorzAlignment = paragraphFormat.HorzAlignment;
          flag = true;
        }
      }
      VertAlignment? vertAlignment1 = paragraphFormat.VertAlignment;
      if (vertAlignment1.HasValue)
      {
        vertAlignment1 = paragraphFormat1.VertAlignment;
        VertAlignment? vertAlignment2 = paragraphFormat.VertAlignment;
        if (!(vertAlignment1.GetValueOrDefault() == vertAlignment2.GetValueOrDefault() & vertAlignment1.HasValue == vertAlignment2.HasValue))
        {
          paragraphFormat2.VertAlignment = paragraphFormat.VertAlignment;
          flag = true;
        }
      }
      int? textLevel1 = paragraphFormat.TextLevel;
      if (textLevel1.HasValue)
      {
        textLevel1 = paragraphFormat1.TextLevel;
        int? textLevel2 = paragraphFormat.TextLevel;
        if (!(textLevel1.GetValueOrDefault() == textLevel2.GetValueOrDefault() & textLevel1.HasValue == textLevel2.HasValue))
        {
          paragraphFormat2.TextLevel = paragraphFormat.TextLevel;
          flag = true;
        }
      }
      float? nullable1 = paragraphFormat.IdentLeft;
      float? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = paragraphFormat1.IdentLeft;
        nullable2 = paragraphFormat.IdentLeft;
        if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          paragraphFormat2.IdentLeft = paragraphFormat.IdentLeft;
          flag = true;
        }
      }
      nullable2 = paragraphFormat.IdentRight;
      if (nullable2.HasValue)
      {
        nullable2 = paragraphFormat1.IdentRight;
        nullable1 = paragraphFormat.IdentRight;
        if (!((double) nullable2.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          paragraphFormat2.IdentRight = paragraphFormat.IdentRight;
          flag = true;
        }
      }
      nullable1 = paragraphFormat.IdentFirstLine;
      if (nullable1.HasValue)
      {
        nullable1 = paragraphFormat1.IdentFirstLine;
        nullable2 = paragraphFormat.IdentFirstLine;
        if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          paragraphFormat2.IdentFirstLine = paragraphFormat.IdentFirstLine;
          flag = true;
        }
      }
      nullable2 = paragraphFormat.IntervalBefore;
      if (nullable2.HasValue)
      {
        nullable2 = paragraphFormat1.IntervalBefore;
        nullable1 = paragraphFormat.IntervalBefore;
        if (!((double) nullable2.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          paragraphFormat2.IntervalBefore = paragraphFormat.IntervalBefore;
          flag = true;
        }
      }
      nullable1 = paragraphFormat.IntervalAfter;
      if (nullable1.HasValue)
      {
        nullable1 = paragraphFormat1.IntervalAfter;
        nullable2 = paragraphFormat.IntervalAfter;
        if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          paragraphFormat2.IntervalAfter = paragraphFormat.IntervalAfter;
          flag = true;
        }
      }
      LineSpacingMethod? lineSpacingMethod1 = paragraphFormat.LineSpacingMethod;
      if (lineSpacingMethod1.HasValue)
      {
        lineSpacingMethod1 = paragraphFormat1.LineSpacingMethod;
        LineSpacingMethod? lineSpacingMethod2 = paragraphFormat.LineSpacingMethod;
        if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod2.GetValueOrDefault() & lineSpacingMethod1.HasValue == lineSpacingMethod2.HasValue))
        {
          paragraphFormat2.LineSpacingMethod = paragraphFormat.LineSpacingMethod;
          flag = true;
        }
      }
      nullable2 = paragraphFormat.SpaceBetweenLines;
      if (nullable2.HasValue)
      {
        nullable2 = paragraphFormat1.SpaceBetweenLines;
        nullable1 = paragraphFormat.SpaceBetweenLines;
        if (!((double) nullable2.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          paragraphFormat2.SpaceBetweenLines = paragraphFormat.SpaceBetweenLines;
          flag = true;
        }
      }
      bool? nullable3 = paragraphFormat.DisableFloatLines;
      bool? nullable4;
      if (nullable3.HasValue)
      {
        nullable3 = paragraphFormat1.DisableFloatLines;
        nullable4 = paragraphFormat.DisableFloatLines;
        if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
        {
          paragraphFormat2.DisableFloatLines = paragraphFormat.DisableFloatLines;
          flag = true;
        }
      }
      nullable4 = paragraphFormat.KeepTogether;
      if (nullable4.HasValue)
      {
        nullable4 = paragraphFormat1.KeepTogether;
        nullable3 = paragraphFormat.KeepTogether;
        if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
        {
          paragraphFormat2.KeepTogether = paragraphFormat.KeepTogether;
          flag = true;
        }
      }
      nullable3 = paragraphFormat.KeepWithNext;
      if (nullable3.HasValue)
      {
        nullable3 = paragraphFormat1.KeepWithNext;
        nullable4 = paragraphFormat.KeepWithNext;
        if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
        {
          paragraphFormat2.KeepWithNext = paragraphFormat.KeepWithNext;
          flag = true;
        }
      }
      nullable4 = paragraphFormat.FromNewPage;
      if (nullable4.HasValue)
      {
        nullable4 = paragraphFormat1.FromNewPage;
        nullable3 = paragraphFormat.FromNewPage;
        if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
        {
          paragraphFormat2.FromNewPage = paragraphFormat.FromNewPage;
          flag = true;
        }
      }
      nullable3 = paragraphFormat.DisableWordWrap;
      if (nullable3.HasValue)
      {
        nullable3 = paragraphFormat1.DisableWordWrap;
        nullable4 = paragraphFormat.DisableWordWrap;
        if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
        {
          paragraphFormat2.DisableWordWrap = paragraphFormat.DisableWordWrap;
          flag = true;
        }
      }
      if (flag)
      {
        textData2.SetParagraphFormat(paragraphFormat2, false, false);
        if (textData2 is TextBoxElement)
        {
          TextBoxElement textBoxElement = textData2 as TextBoxElement;
          string rtfText = "";
          ImRtfEditor imRtfEditor = (ImRtfEditor) null;
          if (textBoxElement != null)
          {
            rtfText = textBoxElement.Rtf;
            if (textBoxElement.OwnerDocument != null)
              imRtfEditor = (textBoxElement.OwnerDocument as ImDocument).TernPaintBuffer;
          }
          if (imRtfEditor != null && textBoxElement != null && !textBoxElement.IsEmptyText && rtfText != null && textBoxElement.TextBox.EditorActive)
            textBoxElement.TextBox.GetActualText(out string _, out rtfText, true);
          if (imRtfEditor != null && !textBoxElement.IsEmptyText && rtfText != null)
          {
            Rectangle editorBounds = new Rectangle((int) textBoxElement.Bounds.Left, (int) textBoxElement.Bounds.Top, (int) textBoxElement.Bounds.Width, (int) textBoxElement.Bounds.Height);
            textBoxElement.TextBox.SetupEditor(imRtfEditor, rtfText, true, textBoxElement.StartCharIndex, paragraphFormat2, textBoxElement.Orientation, textBoxElement.CharFormat, textBoxElement.BackColor, textBoxElement.Bounds, editorBounds, new MarginsF(textBoxElement.LeftMargin, textBoxElement.RightMargin, textBoxElement.TopMargin, textBoxElement.BottomMargin), 1f, textBoxElement.DefaultRowSize);
            imRtfEditor.SelectAll(false);
            horzAlignment2 = paragraphFormat.HorzAlignment;
            if (horzAlignment2.HasValue)
            {
              horzAlignment2 = paragraphFormat.HorzAlignment;
              this.SetTextHAlign(horzAlignment2.Value, imRtfEditor, false);
            }
            this.ApplyTextIdent(paragraphFormat, imRtfEditor, false);
            this.ApplyTextSpacing(paragraphFormat, imRtfEditor, false);
            int FmtType1 = 0;
            nullable4 = paragraphFormat.DisableFloatLines;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.DisableFloatLines;
              if (nullable4.Value)
                FmtType1 |= 32 /*0x20*/;
            }
            nullable4 = paragraphFormat.DisableWordWrap;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.DisableWordWrap;
              if (nullable4.Value)
                FmtType1 |= 16 /*0x10*/;
            }
            nullable4 = paragraphFormat.KeepTogether;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.KeepTogether;
              if (nullable4.Value)
                FmtType1 |= 16384 /*0x4000*/;
            }
            nullable4 = paragraphFormat.KeepWithNext;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.KeepWithNext;
              if (nullable4.Value)
                FmtType1 |= 32768 /*0x8000*/;
            }
            nullable4 = paragraphFormat.FromNewPage;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.FromNewPage;
              if (nullable4.Value)
                FmtType1 |= 64 /*0x40*/;
            }
            imRtfEditor.TerSetPflags(FmtType1, true, false);
            imRtfEditor.SetTerParaFmt(FmtType1, true, false);
            int FmtType2 = 0;
            nullable4 = paragraphFormat.DisableFloatLines;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.DisableFloatLines;
              if (!nullable4.Value)
                FmtType2 |= 32 /*0x20*/;
            }
            nullable4 = paragraphFormat.DisableWordWrap;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.DisableWordWrap;
              if (!nullable4.Value)
                FmtType2 |= 16 /*0x10*/;
            }
            nullable4 = paragraphFormat.KeepTogether;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.KeepTogether;
              if (!nullable4.Value)
                FmtType2 |= 16384 /*0x4000*/;
            }
            nullable4 = paragraphFormat.KeepWithNext;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.KeepWithNext;
              if (!nullable4.Value)
                FmtType2 |= 32768 /*0x8000*/;
            }
            nullable4 = paragraphFormat.FromNewPage;
            if (nullable4.HasValue)
            {
              nullable4 = paragraphFormat.FromNewPage;
              if (!nullable4.Value)
                FmtType2 |= 64 /*0x40*/;
            }
            imRtfEditor.TerSetPflags(FmtType2, false, false);
            imRtfEditor.SetTerParaFmt(FmtType2, false, false);
            textBoxElement.AssignText(textBoxElement.Text, imRtfEditor.RtfText, true, false, false);
          }
        }
      }
    }
    return textData1;
  }

  private bool GetTextOrientation(
    IList<DocumentTreeNode> context,
    ref TextOrientation? textOrientation)
  {
    if (context == null || context.Count <= 0)
    {
      textOrientation = new TextOrientation?();
      return false;
    }
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.GetTextOrientation(context[index], ref textOrientation))
        return false;
    }
    return true;
  }

  private bool GetTextOrientation(DocumentTreeNode context, ref TextOrientation? textOrientation)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return true;
    if (context.NodesCount > 0)
      return this.GetTextOrientation((IList<DocumentTreeNode>) context.Nodes, ref textOrientation);
    if (context is TextData)
    {
      TextData textData = context as TextData;
      if (!textOrientation.HasValue)
      {
        textOrientation = new TextOrientation?(textData.Orientation);
      }
      else
      {
        TextOrientation? nullable = textOrientation;
        TextOrientation orientation = textData.Orientation;
        if (!(nullable.GetValueOrDefault() == orientation & nullable.HasValue))
        {
          textOrientation = new TextOrientation?();
          return false;
        }
      }
    }
    return true;
  }

  private void SetTextOrientation(IList<DocumentTreeNode> context, TextOrientation textOrientation)
  {
    if (this.Document != null && this.Document.UndoManager != null)
      this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_585"));
    try
    {
      if (context == null || context.Count <= 0)
        return;
      for (int index = 0; index < context.Count; ++index)
        this.SetTextOrientation(context[index], textOrientation);
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
    }
  }

  private void SetTextOrientation(DocumentTreeNode context, TextOrientation textOrientation)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return;
    if (context.NodesCount > 0)
    {
      this.SetTextOrientation((IList<DocumentTreeNode>) context.Nodes, textOrientation);
    }
    else
    {
      if (!(context is TextData))
        return;
      ((TextData) context).Orientation = textOrientation;
    }
  }

  private void ProcessBordersCommand(
    IList<DocumentTreeNode> context,
    bool isChecked,
    ImDocumentEditorFormBase.borderType borderTypes)
  {
    if (this.Document != null && this.Document.UndoManager != null)
      this.Document.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_586"));
    try
    {
      if (context == null || context.Count <= 0)
        return;
      for (int index = 0; index < context.Count; ++index)
        this.ProcessBordersCommand(context[index], isChecked, borderTypes);
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>BorderLine на тулбаре</summary>
  /// <returns></returns>
  private BorderLine GetToolbarBorder()
  {
    if (this.InvokeRequired)
      return this.Invoke((Delegate) new ImDocumentEditorFormBase.GetToolbarBorder_EventHandler(this.GetToolbarBorder)) as BorderLine;
    if (this.MenuHelper == null || this.MenuHelper.CbLineStyle == null || this.MenuHelper.CbLineWidth == null)
      return (BorderLine) null;
    Color linesColor = this.MenuHelper.LinesColor;
    if (this.MenuHelper.CbLineStyle.ComboBox.InvokeRequired)
      return (BorderLine) null;
    BorderStyles selectedItem = (BorderStyles) this.MenuHelper.CbLineStyle.ComboBox.SelectedItem;
    float result;
    if (!float.TryParse(FloatConverter.CorrectDecimal(this.MenuHelper.CbLineWidth.ComboBox.Text), out result))
    {
      result = 0.0f;
      this.MenuHelper.CbLineWidth.ComboBox.SelectedIndex = 0;
    }
    return new BorderLine(this.MenuHelper.LinesColor, selectedItem, result);
  }

  private void ProcessBordersCommand(
    DocumentTreeNode context,
    bool isChecked,
    ImDocumentEditorFormBase.borderType borderTypes)
  {
    BorderLine toolbarBorder = this.GetToolbarBorder();
    if (context == null || context is Intermech.Document.Model.Page)
      return;
    if (context.NodesCount > 0)
    {
      this.ProcessBordersCommand((IList<DocumentTreeNode>) context.Nodes, isChecked, borderTypes);
    }
    else
    {
      if (!(context is RectangleElement))
        return;
      RectangleElement rectangleElement1 = context as RectangleElement;
      TableData topLevelTable = rectangleElement1.TopLevelTable;
      if (topLevelTable != null && topLevelTable != context)
      {
        if ((borderTypes & ImDocumentEditorFormBase.borderType.InnerHorizontal) != (ImDocumentEditorFormBase.borderType) 0 || (borderTypes & ImDocumentEditorFormBase.borderType.OuterTop) != (ImDocumentEditorFormBase.borderType) 0)
        {
          Collection<RectangleElement> topCells = rectangleElement1.GetTopCells(true);
          bool flag = topCells.Count == 0 && (borderTypes & ImDocumentEditorFormBase.borderType.OuterTop) != 0;
          if (!flag)
          {
            foreach (RectangleElement rectangleElement2 in topCells)
            {
              if (rectangleElement2 != null && ((borderTypes & ImDocumentEditorFormBase.borderType.OuterTop) != (ImDocumentEditorFormBase.borderType) 0 && !rectangleElement2.ShowSelected || (borderTypes & ImDocumentEditorFormBase.borderType.InnerHorizontal) != (ImDocumentEditorFormBase.borderType) 0 && rectangleElement2.ShowSelected))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            BorderLine borderLine = rectangleElement1.Borders.Top.Clone();
            if (isChecked)
              borderLine = toolbarBorder.Clone();
            else
              borderLine.Style = BorderStyles.None;
            rectangleElement1.SetTopBorderLine(borderLine, true);
          }
        }
        if ((borderTypes & ImDocumentEditorFormBase.borderType.InnerVertical) != (ImDocumentEditorFormBase.borderType) 0 || (borderTypes & ImDocumentEditorFormBase.borderType.OuterRight) != (ImDocumentEditorFormBase.borderType) 0)
        {
          Collection<RectangleElement> rightCells = rectangleElement1.GetRightCells(true);
          bool flag = rightCells.Count == 0 && (borderTypes & ImDocumentEditorFormBase.borderType.OuterRight) != 0;
          if (!flag)
          {
            foreach (RectangleElement rectangleElement3 in rightCells)
            {
              if (rectangleElement3 != null && ((borderTypes & ImDocumentEditorFormBase.borderType.OuterRight) != (ImDocumentEditorFormBase.borderType) 0 && !rectangleElement3.ShowSelected || (borderTypes & ImDocumentEditorFormBase.borderType.InnerVertical) != (ImDocumentEditorFormBase.borderType) 0 && rectangleElement3.ShowSelected))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            BorderLine borderLine = rectangleElement1.Borders.Right.Clone();
            if (isChecked)
              borderLine = toolbarBorder.Clone();
            else
              borderLine.Style = BorderStyles.None;
            rectangleElement1.SetRightBorderLine(borderLine, true);
          }
        }
        if ((borderTypes & ImDocumentEditorFormBase.borderType.InnerHorizontal) != (ImDocumentEditorFormBase.borderType) 0 || (borderTypes & ImDocumentEditorFormBase.borderType.OuterBottom) != (ImDocumentEditorFormBase.borderType) 0)
        {
          Collection<RectangleElement> bottomCells = rectangleElement1.GetBottomCells(true);
          bool flag = bottomCells.Count == 0 && (borderTypes & ImDocumentEditorFormBase.borderType.OuterBottom) != 0;
          if (!flag)
          {
            foreach (RectangleElement rectangleElement4 in bottomCells)
            {
              if (rectangleElement4 != null && ((borderTypes & ImDocumentEditorFormBase.borderType.OuterBottom) != (ImDocumentEditorFormBase.borderType) 0 && !rectangleElement4.ShowSelected || (borderTypes & ImDocumentEditorFormBase.borderType.InnerHorizontal) != (ImDocumentEditorFormBase.borderType) 0 && rectangleElement4.ShowSelected))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            BorderLine borderLine = rectangleElement1.Borders.Bottom.Clone();
            if (isChecked)
              borderLine = toolbarBorder.Clone();
            else
              borderLine.Style = BorderStyles.None;
            rectangleElement1.SetBottomBorderLine(borderLine, true);
          }
        }
        if ((borderTypes & ImDocumentEditorFormBase.borderType.InnerVertical) == (ImDocumentEditorFormBase.borderType) 0 && (borderTypes & ImDocumentEditorFormBase.borderType.OuterLeft) == (ImDocumentEditorFormBase.borderType) 0)
          return;
        Collection<RectangleElement> leftCells = rectangleElement1.GetLeftCells(true);
        bool flag1 = leftCells.Count == 0 && (borderTypes & ImDocumentEditorFormBase.borderType.OuterLeft) != 0;
        if (!flag1)
        {
          foreach (RectangleElement rectangleElement5 in leftCells)
          {
            if (rectangleElement5 != null && ((borderTypes & ImDocumentEditorFormBase.borderType.OuterLeft) != (ImDocumentEditorFormBase.borderType) 0 && !rectangleElement5.ShowSelected || (borderTypes & ImDocumentEditorFormBase.borderType.InnerVertical) != (ImDocumentEditorFormBase.borderType) 0 && rectangleElement5.ShowSelected))
            {
              flag1 = true;
              break;
            }
          }
        }
        if (!flag1)
          return;
        BorderLine borderLine1 = rectangleElement1.Borders.Left.Clone();
        if (isChecked)
          borderLine1 = toolbarBorder.Clone();
        else
          borderLine1.Style = BorderStyles.None;
        rectangleElement1.SetLeftBorderLine(borderLine1, true);
      }
      else
      {
        if (borderTypes.HasFlag((Enum) ImDocumentEditorFormBase.borderType.OuterLeft))
        {
          BorderLine borderLine = rectangleElement1.Borders.Left.Clone();
          if (isChecked)
            borderLine = toolbarBorder.Clone();
          else
            borderLine.Style = BorderStyles.None;
          rectangleElement1.SetLeftBorderLine(borderLine, true);
        }
        if (borderTypes.HasFlag((Enum) ImDocumentEditorFormBase.borderType.OuterRight))
        {
          BorderLine borderLine = rectangleElement1.Borders.Right.Clone();
          if (isChecked)
            borderLine = toolbarBorder.Clone();
          else
            borderLine.Style = BorderStyles.None;
          rectangleElement1.SetRightBorderLine(borderLine, true);
        }
        if (borderTypes.HasFlag((Enum) ImDocumentEditorFormBase.borderType.OuterTop))
        {
          BorderLine borderLine = rectangleElement1.Borders.Top.Clone();
          if (isChecked)
            borderLine = toolbarBorder.Clone();
          else
            borderLine.Style = BorderStyles.None;
          rectangleElement1.SetTopBorderLine(borderLine, true);
        }
        if (!borderTypes.HasFlag((Enum) ImDocumentEditorFormBase.borderType.OuterBottom))
          return;
        BorderLine borderLine2 = rectangleElement1.Borders.Bottom.Clone();
        if (isChecked)
          borderLine2 = toolbarBorder.Clone();
        else
          borderLine2.Style = BorderStyles.None;
        rectangleElement1.SetBottomBorderLine(borderLine2, true);
      }
    }
  }

  /// <summary> Редактирование стиля текста для всей ячейки </summary>
  /// <param name="context"></param>
  /// <param name="switchOn"></param>
  /// <param name="setFontStyle"></param>
  private void SetCellFontStyle(
    IList<DocumentTreeNode> context,
    bool switchOn,
    CharStyle charStyle)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (this.Document != null)
    {
      flag1 = !this.Document.SuspendedUpdateLayoutFlag;
      if (flag1)
        this.Document.SuspendUpdateLayout();
      flag2 = !this.Document.SuspendedUpdateUIGeometryFlag;
      if (flag2)
        this.Document.SuspendUpdateGeometryRefreshUI();
    }
    if (context == null)
      return;
    if (context.Count == 0)
      return;
    try
    {
      for (int index = 0; index < context.Count; ++index)
      {
        if (context[index] is TextData textData)
        {
          CharFormat charFormat1 = textData.CharFormat;
          CharFormat charFormat2 = charFormat1 != null ? charFormat1.Clone() : new CharFormat();
          if (switchOn)
            charFormat2.CharStyle |= charStyle;
          else
            charFormat2.CharStyle &= ~charStyle;
          textData.SetCharFormat(charFormat2, false, false);
        }
        else if (context[index] != null && context[index].Nodes != null && context[index].Nodes.Count > 0)
          this.SetCellFontStyle((IList<DocumentTreeNode>) context[index].Nodes, switchOn, charStyle);
      }
    }
    finally
    {
      if (this.Document != null)
      {
        if (flag1)
          this.Document.ResumeUpdateLayout(false, true);
        if (flag2)
          this.Document.ResumeUpdateRefreshUI(true, true);
      }
    }
  }

  /// <summary>Удалить символы перевода строки и конца параграфа в конце текста</summary>
  /// <param name="planeText">Текст</param>
  /// <returns>Подчищенный текст</returns>
  protected string DeleteLastEndLine(string planeText)
  {
    if (planeText == null || planeText == string.Empty)
      return string.Empty;
    if (planeText[planeText.Length - 1] == '\r' || planeText[planeText.Length - 1] == '\n')
      planeText = planeText.Remove(planeText.Length - 1, 1);
    if (planeText[planeText.Length - 1] == '\r' || planeText[planeText.Length - 1] == '\n')
      planeText = planeText.Remove(planeText.Length - 1, 1);
    return planeText;
  }

  /// <summary> Редактирование стиля текста для выбраного в активном редакторе текста </summary>
  private void SetFontStyle(bool switchOn, CharStyle charStyle)
  {
    if (this.DocumentControl == null || this._queryTern == null)
      return;
    if (!this._queryTern.TerGetSelection(out this._queryFirstLineSelection, out this._queryFirstColSelection, out this._queryEndLineSelection, out this._queryEndColSelection))
    {
      this._queryTern.TerAbsToRowCol(this._queryTern.TerGetCaretPos(), out this._queryFirstLineSelection, out this._queryFirstColSelection);
      this._queryEndLineSelection = this._queryFirstLineSelection;
      this._queryEndColSelection = this._queryFirstColSelection;
    }
    if (this._queryFirstLineSelection > this._queryEndLineSelection)
    {
      int endLineSelection = this._queryEndLineSelection;
      this._queryEndLineSelection = this._queryFirstLineSelection;
      this._queryFirstLineSelection = endLineSelection;
      int firstColSelection = this._queryFirstColSelection;
      this._queryFirstColSelection = this._queryEndColSelection;
      this._queryEndColSelection = firstColSelection;
    }
    bool flag = this._queryFirstLineSelection == this._queryEndLineSelection && this._queryFirstColSelection == this._queryEndColSelection && this._queryFirstColSelection < this._queryTern.text[this._queryFirstLineSelection].len - 1 && this._queryFirstColSelection > 0 && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection - 1]) && char.IsLetterOrDigit(this._queryTern.text[this._queryFirstLineSelection].txt[this._queryFirstColSelection + 1]);
    SelectionBlock block = (SelectionBlock) null;
    if (flag)
    {
      block = this._queryTern.GetSelectionBlock();
      this._queryTern.ctl.OnDoubleClick(new EventArgs());
    }
    this._queryTern.SetTerCharStyle((int) charStyle, switchOn, !flag);
    if (flag)
    {
      this._queryTern.DeselectTerText(block == null);
      if (block != null)
        this._queryTern.RestoreSelection(block, true);
    }
    if (this.CommandManager == null)
      return;
    this.CommandManager.QueryStatus();
  }

  public void SetCellAlign(string commandName)
  {
    this._IsQueryChacheIsInit = false;
    this.InitQueryCache();
    List<DocumentTreeNode> queryStatusContext = this._queryStatusContext;
    if (this.Document == null || this.ReadOnly)
      return;
    this.Document.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_587"));
    try
    {
      switch (commandName)
      {
        case "Format.CellAlign.CenterBottom":
        case "Format.CellAlign.JustifyBottom":
        case "Format.CellAlign.LeftBottom":
        case "Format.CellAlign.RightBottom":
          this.SetCellTextVAlign((IList<DocumentTreeNode>) queryStatusContext, VertAlignment.Bottom, false);
          break;
        case "Format.CellAlign.CenterMiddle":
        case "Format.CellAlign.JustifyMiddle":
        case "Format.CellAlign.LeftMiddle":
        case "Format.CellAlign.RightMiddle":
          this.SetCellTextVAlign((IList<DocumentTreeNode>) queryStatusContext, VertAlignment.Center, false);
          break;
        case "Format.CellAlign.CenterTop":
        case "Format.CellAlign.JustifyTop":
        case "Format.CellAlign.LeftTop":
        case "Format.CellAlign.RightTop":
          this.SetCellTextVAlign((IList<DocumentTreeNode>) queryStatusContext, VertAlignment.Top, false);
          break;
      }
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
      {
        case 395217590:
          if (!(commandName == "Format.CellAlign.JustifyMiddle"))
            return;
          goto label_38;
        case 616734475:
          if (!(commandName == "Format.CellAlign.CenterTop"))
            return;
          goto label_36;
        case 942372245:
          if (!(commandName == "Format.CellAlign.CenterMiddle"))
            return;
          goto label_36;
        case 2685860866:
          if (!(commandName == "Format.CellAlign.JustifyTop"))
            return;
          goto label_38;
        case 2970257967:
          if (!(commandName == "Format.CellAlign.LeftTop"))
            return;
          break;
        case 2976129875:
          if (!(commandName == "Format.CellAlign.CenterBottom"))
            return;
          goto label_36;
        case 3005020232:
          if (!(commandName == "Format.CellAlign.RightTop"))
            return;
          goto label_37;
        case 3315211207:
          if (!(commandName == "Format.CellAlign.LeftBottom"))
            return;
          break;
        case 3345886436:
          if (!(commandName == "Format.CellAlign.JustifyBottom"))
            return;
          goto label_38;
        case 3580231545:
          if (!(commandName == "Format.CellAlign.LeftMiddle"))
            return;
          break;
        case 3974119700:
          if (!(commandName == "Format.CellAlign.RightMiddle"))
            return;
          goto label_37;
        case 4080472566:
          if (!(commandName == "Format.CellAlign.RightBottom"))
            return;
          goto label_37;
        default:
          return;
      }
      this.SetCellTextHAlign((IList<DocumentTreeNode>) queryStatusContext, HorzAlignment.Left, true);
      return;
label_36:
      this.SetCellTextHAlign((IList<DocumentTreeNode>) queryStatusContext, HorzAlignment.Center, true);
      return;
label_37:
      this.SetCellTextHAlign((IList<DocumentTreeNode>) queryStatusContext, HorzAlignment.Right, true);
      return;
label_38:
      this.SetCellTextHAlign((IList<DocumentTreeNode>) queryStatusContext, HorzAlignment.Justify, true);
    }
    finally
    {
      if (this.Document != null && this.Document.UndoManager != null)
        this.Document.UndoManager.EndCreateMultyUndo();
    }
  }

  private void SetCellTextVAlign(
    IList<DocumentTreeNode> context,
    VertAlignment vertAlignment,
    bool update)
  {
    if (context == null || context.Count == 0)
      return;
    pageElementNode = (PageElementNode) null;
    int fromPage = -1;
    foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) context)
    {
      if (documentTreeNode != null)
      {
        if (update && documentTreeNode is PageElementNode pageElementNode)
        {
          int index = pageElementNode.Index;
          if (fromPage == -1 || index < fromPage)
            fromPage = index;
        }
        if (documentTreeNode.NodesCount > 0)
          this.SetCellTextVAlign((IList<DocumentTreeNode>) documentTreeNode.Nodes, vertAlignment, false);
        if (documentTreeNode is TextData textData)
        {
          ParagraphFormat paragraphFormat1 = textData.ParagraphFormat;
          ParagraphFormat paragraphFormat2 = paragraphFormat1 != null ? paragraphFormat1.Clone() : new ParagraphFormat();
          paragraphFormat2.VertAlignment = new VertAlignment?(vertAlignment);
          textData.SetParagraphFormat(paragraphFormat2, false, false);
        }
      }
    }
    if (!update || fromPage == -1 || pageElementNode == null || pageElementNode.OwnerDocument == null)
      return;
    pageElementNode.OwnerDocument.UpdateLayout(fromPage, false, true);
  }

  private void SetCellTextHAlign(
    IList<DocumentTreeNode> context,
    HorzAlignment horzAlignment,
    bool update)
  {
    if (context == null || context.Count == 0)
      return;
    PageData pageData = (PageData) null;
    int fromPage = -1;
    foreach (DocumentTreeNode documentTreeNode1 in (IEnumerable<DocumentTreeNode>) context)
    {
      if (documentTreeNode1 != null)
      {
        if (update && documentTreeNode1 is PageElementNode pageElementNode1)
        {
          if (!pageElementNode1.IsVirtualNode)
            pageData = pageElementNode1.Page;
          else if (pageElementNode1 is RectangleElement)
          {
            List<DocumentTreeNode> realCells = (pageElementNode1 as RectangleElement).GetRealCells();
            int num = -1;
            foreach (DocumentTreeNode documentTreeNode2 in realCells)
            {
              if (documentTreeNode2 is PageElementNode pageElementNode && pageElementNode.Page != null && (num == -1 || pageElementNode.Page.Index < num))
              {
                num = pageElementNode.Page.Index;
                pageData = pageElementNode.Page;
              }
            }
          }
          if (pageData != null)
          {
            int index = pageData.Index;
            if (fromPage == -1 || index < fromPage)
              fromPage = index;
          }
        }
        if (documentTreeNode1.NodesCount > 0)
          this.SetCellTextHAlign((IList<DocumentTreeNode>) documentTreeNode1.Nodes, horzAlignment, false);
        if (documentTreeNode1 is TextData textData)
        {
          ParagraphFormat paragraphFormat1 = textData.ParagraphFormat;
          ParagraphFormat paragraphFormat2 = paragraphFormat1 != null ? paragraphFormat1.Clone() : new ParagraphFormat();
          paragraphFormat2.HorzAlignment = new HorzAlignment?(horzAlignment);
          textData.SetParagraphFormat(paragraphFormat2, false, false);
        }
      }
    }
    if (!update || fromPage == -1 || pageData == null || pageData.OwnerDocument == null)
      return;
    pageData.OwnerDocument.UpdateLayout(fromPage, false, true);
  }

  private void SetTextHAlign(HorzAlignment horzAlignment, ImRtfEditor tern, bool needUpdate)
  {
    if (this.DocumentControl == null || tern == null)
      return;
    int FmtType;
    switch (horzAlignment)
    {
      case HorzAlignment.Left:
        FmtType = 1024 /*0x0400*/;
        break;
      case HorzAlignment.Center:
        FmtType = 1;
        break;
      case HorzAlignment.Right:
        FmtType = 2;
        break;
      case HorzAlignment.Justify:
        FmtType = 2048 /*0x0800*/;
        break;
      default:
        return;
    }
    tern.SetTerParaFmt(FmtType, true, needUpdate);
  }

  /// <summary>BgColorChanged</summary>
  public void BgColorChanged()
  {
    this.SetBgColor((IList<DocumentTreeNode>) DocumentTreeNode.GetNodesWithoutChilds((IList<DocumentTreeNode>) this.documentControl.SelectedNodes, true));
  }

  /// <summary>TextBkColorChanged</summary>
  public void TextBkColorChanged()
  {
    this._IsQueryChacheIsInit = false;
    this.InitQueryCache();
    this.SaveCharFormat((IList<DocumentTreeNode>) this._queryStatusContext, new CharFormat(true)
    {
      TextBkColorForUser = new Color?(this.MenuHelper.TextBkColor)
    }, true);
    this.InitQueryCache();
  }

  /// <summary>TextColorChanged// </summary>
  public void TextColorChanged()
  {
    this._IsQueryChacheIsInit = false;
    this.InitQueryCache();
    this.SaveCharFormat((IList<DocumentTreeNode>) this._queryStatusContext, new CharFormat(true)
    {
      TextColorForUser = new Color?(this.MenuHelper.TextColor)
    }, true);
    this.InitQueryCache();
  }

  private void SetBgColor(IList<DocumentTreeNode> context)
  {
    if (this.UndoManager != null)
      this.UndoManager.BeginCreateMultyUndo("");
    try
    {
      foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) context)
      {
        if (documentTreeNode != null)
        {
          if (documentTreeNode.NodesCount > 0)
            this.SetBgColor((IList<DocumentTreeNode>) documentTreeNode.Nodes);
          if (documentTreeNode is TextData)
          {
            if (DocumentMenuHelper.BgColor == Color.Transparent)
            {
              ((RectangleElement) documentTreeNode).BackColor = Color.Empty;
              ((PageElementNode) documentTreeNode).Transparent = true;
            }
            else
            {
              ((RectangleElement) documentTreeNode).BackColor = DocumentMenuHelper.BgColor;
              ((PageElementNode) documentTreeNode).Transparent = false;
            }
          }
        }
      }
    }
    finally
    {
      if (this.UndoManager != null)
        this.UndoManager.EndCreateMultyUndo();
    }
  }

  private void SetListedParagraph(bool isSet, bool isBullet)
  {
    if (this.DocumentControl == null)
      return;
    ImRtfEditor activeEditorControl = this.DocumentControl.GetActiveEditorControl();
    if (activeEditorControl == null)
      return;
    if (isBullet)
    {
      if (activeEditorControl.TerMenuSelect2(748))
        activeEditorControl.TerSetListBullet(false, 0, -1, 1, "", ".", false);
      activeEditorControl.TerSetListBullet(activeEditorControl.TerMenuSelect(729) == 0, 23, -1, 1, "", "", true);
    }
    else
    {
      if (activeEditorControl.TerMenuSelect2(729))
        activeEditorControl.TerSetListBullet(false, 23, -1, 1, "", "", false);
      activeEditorControl.TerSetListBullet(activeEditorControl.TerMenuSelect(748) == 0, 0, -1, 1, "", ".", true);
    }
  }

  /// <summary>Получить список выделенных узлов с отфильтрованными дочерними узлами</summary>
  /// <returns></returns>
  protected virtual List<DocumentTreeNode> GetDocumentNodeList()
  {
    return this.documentControl == null ? (List<DocumentTreeNode>) null : DocumentTreeNode.GetNodesWithoutChilds((IList<DocumentTreeNode>) this.documentControl.SelectedNodes, true);
  }

  private bool CanFormatDefaultCharFormat(IList<DocumentTreeNode> context)
  {
    if (context.Count == 0 || this.ReadOnly)
      return false;
    foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) context)
    {
      switch (documentTreeNode)
      {
        case PageData _:
        case ImDocumentData _:
          continue;
        default:
          return false;
      }
    }
    return true;
  }

  public virtual void InitQueryCache()
  {
    if (this.documentControl == null || this._IsQueryChacheIsInit)
      return;
    this._IsQueryChacheIsInit = true;
    if (this.MenuHelper == null)
      return;
    this._queryIsProtectedZone = false;
    this._queryDocumentNodeList = this.GetDocumentNodeList();
    if (this.documentControl != null && this.documentControl.Document != null)
      this.documentControl.QueryCache_HasLockedNodes = this.documentControl.Document.HasLockedNodes((IList<DocumentTreeNode>) this._queryDocumentNodeList);
    this._queryStatusContext = this._queryDocumentNodeList == null ? new List<DocumentTreeNode>() : new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) this._queryDocumentNodeList);
    this._queryStatusFormatText = !this.ReadOnly && this.MenuHelper.QueryStatus_FormatText((IList<DocumentTreeNode>) this._queryStatusContext);
    this._queryStatusFormatCharFormat = this._queryStatusFormatText || this.CanFormatDefaultCharFormat((IList<DocumentTreeNode>) this._queryStatusContext);
    if (this.DocumentControl == null)
      return;
    this._queryTern = this.DocumentControl.GetActiveEditorControl();
    if (this._queryTern != null)
    {
      int caretPos = this._queryTern.TerGetCaretPos();
      this._queryIsProtectedZone = this._queryTern.IsProtectedZone(caretPos, false);
      if (!this._queryTern.TerGetSelection(out this._queryFirstLineSelection, out this._queryFirstColSelection, out this._queryEndLineSelection, out this._queryEndColSelection))
      {
        this._queryTern.TerAbsToRowCol(caretPos, out this._queryFirstLineSelection, out this._queryFirstColSelection);
        this._queryEndLineSelection = this._queryFirstLineSelection;
        this._queryEndColSelection = this._queryFirstColSelection;
      }
      if (this._queryFirstLineSelection > this._queryEndLineSelection)
      {
        int endLineSelection = this._queryEndLineSelection;
        this._queryEndLineSelection = this._queryFirstLineSelection;
        this._queryFirstLineSelection = endLineSelection;
        int firstColSelection = this._queryFirstColSelection;
        this._queryFirstColSelection = this._queryEndColSelection;
        this._queryEndColSelection = firstColSelection;
      }
      this._queryIsFontAutoSize = (this.DocumentControl.ActiveElement as TextBoxElement).FontAutoSize;
      int num1 = this._queryTern.TerGetParam(1);
      int[] font = (int[]) null;
      string text = (string) null;
      int line = this._queryTern.TerGetLine(num1 - 1, out text, out font);
      int num2 = this._queryFirstColSelection;
      int num3 = this._queryEndColSelection;
      if (num2 > num3)
      {
        int num4 = num2;
        num2 = num3;
        num3 = num4;
      }
      this._queryIsAllTextSelected = this._queryFirstLineSelection == 0 && num2 == 0 && this._queryEndLineSelection == num1 - 1 && num3 >= line - 1;
      List<int> intList = new List<int>();
      for (int firstLineSelection = this._queryFirstLineSelection; firstLineSelection <= this._queryEndLineSelection; ++firstLineSelection)
      {
        int LeftIndent;
        int RightIndent;
        int FirstIndent;
        int SpaceBefore;
        int SpaceAfter;
        int SpaceBetween;
        int flags;
        int LineSpacing;
        if (this._queryTern.TerGetParaInfo4(firstLineSelection, false, out LeftIndent, out RightIndent, out FirstIndent, out int _, out int _, out int _, out int _, out int _, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out int _, out Color _, out LineSpacing))
        {
          if (firstLineSelection == this._queryFirstLineSelection)
          {
            this._queryParagraphFormatforStyles = new ParagraphFormat(true);
            this._queryLeftIndent = new int?(LeftIndent);
            this._queryRigthIndent = new int?(RightIndent);
            this._queryFirstIndent = new int?(FirstIndent);
            this._queryFlags = flags;
            this._querySpaceBefore = new int?(SpaceBefore);
            this._querySpaceAfter = new int?(SpaceAfter);
            this._querySpaceBetween = new int?(SpaceBetween);
            this._queryLineSpacing = new int?(LineSpacing);
            this._queryDisableFloatLines = new bool?((this._queryFlags & 32 /*0x20*/) != 0);
            this._queryDisableWordWrap = new bool?((this._queryFlags & 16 /*0x10*/) != 0);
            this._queryKeepTogether = new bool?((this._queryFlags & 16384 /*0x4000*/) != 0);
            this._queryKeepWithNext = new bool?((this._queryFlags & 32768 /*0x8000*/) != 0);
            this._queryFromNewPage = new bool?((this._queryFlags & 64 /*0x40*/) != 0);
          }
          else
          {
            this._queryFlags &= flags;
            int num5 = (this._queryFlags & 32 /*0x20*/) != 0 ? 1 : 0;
            bool? disableFloatLines = this._queryDisableFloatLines;
            int num6 = disableFloatLines.GetValueOrDefault() ? 1 : 0;
            if (!(num5 == num6 & disableFloatLines.HasValue))
              this._queryDisableFloatLines = new bool?();
            int num7 = (this._queryFlags & 32 /*0x20*/) != 0 ? 1 : 0;
            bool? queryDisableWordWrap = this._queryDisableWordWrap;
            int num8 = queryDisableWordWrap.GetValueOrDefault() ? 1 : 0;
            if (!(num7 == num8 & queryDisableWordWrap.HasValue))
              this._queryDisableWordWrap = new bool?();
            int num9 = (this._queryFlags & 32 /*0x20*/) != 0 ? 1 : 0;
            bool? queryKeepTogether = this._queryKeepTogether;
            int num10 = queryKeepTogether.GetValueOrDefault() ? 1 : 0;
            if (!(num9 == num10 & queryKeepTogether.HasValue))
              this._queryKeepTogether = new bool?();
            int num11 = (this._queryFlags & 32 /*0x20*/) != 0 ? 1 : 0;
            bool? queryKeepWithNext = this._queryKeepWithNext;
            int num12 = queryKeepWithNext.GetValueOrDefault() ? 1 : 0;
            if (!(num11 == num12 & queryKeepWithNext.HasValue))
              this._queryKeepWithNext = new bool?();
            int num13 = (this._queryFlags & 32 /*0x20*/) != 0 ? 1 : 0;
            bool? queryFromNewPage = this._queryFromNewPage;
            int num14 = queryFromNewPage.GetValueOrDefault() ? 1 : 0;
            if (!(num13 == num14 & queryFromNewPage.HasValue))
              this._queryFromNewPage = new bool?();
            if (this._queryLineSpacing.HasValue)
            {
              int? queryLineSpacing = this._queryLineSpacing;
              int num15 = LineSpacing;
              if (!(queryLineSpacing.GetValueOrDefault() == num15 & queryLineSpacing.HasValue))
                this._queryLineSpacing = new int?();
            }
            if (this._queryLeftIndent.HasValue)
            {
              int? queryLeftIndent = this._queryLeftIndent;
              int num16 = LeftIndent;
              if (!(queryLeftIndent.GetValueOrDefault() == num16 & queryLeftIndent.HasValue))
                this._queryLeftIndent = new int?();
            }
            if (this._queryRigthIndent.HasValue)
            {
              int? queryRigthIndent = this._queryRigthIndent;
              int num17 = RightIndent;
              if (!(queryRigthIndent.GetValueOrDefault() == num17 & queryRigthIndent.HasValue))
                this._queryRigthIndent = new int?();
            }
            if (this._queryFirstIndent.HasValue)
            {
              int? queryFirstIndent = this._queryFirstIndent;
              int num18 = FirstIndent;
              if (!(queryFirstIndent.GetValueOrDefault() == num18 & queryFirstIndent.HasValue))
                this._queryFirstIndent = new int?();
            }
            if (this._querySpaceBefore.HasValue)
            {
              int? querySpaceBefore = this._querySpaceBefore;
              int num19 = SpaceBefore;
              if (!(querySpaceBefore.GetValueOrDefault() == num19 & querySpaceBefore.HasValue))
                this._querySpaceBefore = new int?();
            }
            if (this._querySpaceAfter.HasValue)
            {
              int? querySpaceAfter = this._querySpaceAfter;
              int num20 = SpaceAfter;
              if (!(querySpaceAfter.GetValueOrDefault() == num20 & querySpaceAfter.HasValue))
                this._querySpaceAfter = new int?();
            }
            if (this._querySpaceBetween.HasValue)
            {
              int? querySpaceBetween = this._querySpaceBetween;
              int num21 = SpaceBetween;
              if (!(querySpaceBetween.GetValueOrDefault() == num21 & querySpaceBetween.HasValue))
                this._querySpaceBetween = new int?();
            }
          }
        }
        int num22 = 0;
        int num23 = this._queryTern.text[firstLineSelection].len;
        if (this._queryFirstLineSelection == this._queryEndLineSelection)
        {
          num22 = this._queryFirstColSelection;
          num23 = this._queryEndColSelection;
        }
        else
        {
          if (firstLineSelection == this._queryFirstLineSelection)
          {
            num22 = this._queryFirstColSelection;
            num23 = this._queryTern.text[firstLineSelection].len;
          }
          if (firstLineSelection == this._queryEndLineSelection)
          {
            num22 = 0;
            num23 = this._queryEndColSelection;
          }
        }
        if (num23 < num22)
        {
          int num24 = num23;
          num23 = num22;
          num22 = num24;
        }
        if (this._queryTern.text[firstLineSelection].fmt == null)
        {
          if (intList.IndexOf((int) this._queryTern.text[firstLineSelection].UniFmt) == -1)
            intList.Add((int) this._queryTern.text[firstLineSelection].UniFmt);
        }
        else
        {
          ushort[] fmt = this._queryTern.text[firstLineSelection].fmt;
          if (num23 > fmt.Length)
            num23 = fmt.Length - 1;
          if (num22 < 0)
            num22 = 0;
          if (num22 == num23)
            intList.Add(this._queryTern.TerGetEffectiveFont());
          for (int index = num22; index < num23; ++index)
          {
            if (intList.IndexOf((int) fmt[index]) == -1)
              intList.Add((int) fmt[index]);
          }
        }
      }
      this._queryCharFormatforStyles = new CharFormat();
      int? nullable1;
      if (intList.Count > 0)
      {
        string stringFromTag = this._queryTern.ExtractStringFromTag((IList<int>) tc.ReplacedCharTags);
        this._queryIsFormula = stringFromTag != null && stringFromTag.IndexOf("<<") != -1;
        Color color1;
        Color TextBackColor;
        this._queryTern.TerGetTextColor(intList[0], out color1, out TextBackColor);
        string TypeFace;
        int TwipsSize;
        int style;
        this._queryTern.GetFontInfo2(intList[0], out TypeFace, out TwipsSize, out style);
        this._queryTextColor = new Color?(color1);
        this._queryTextBkColor = new Color?(TextBackColor);
        this._queryTern.TerGetFontParam(intList[0], 5, out color1);
        this._queryULColor = new Color?(color1);
        this._queryTypeface = TypeFace;
        this._queryPointSize = new int?(TwipsSize);
        this._queryStyles = new int?(style);
        this._queryCharFormatforStyles.BoldItalic = new BoldItalicStyle?((BoldItalicStyle) style);
        this._queryCharFormatforStyles.Underline = new UnderlineStyle?((UnderlineStyle) style);
        this._queryCharFormatforStyles.Strike = new StrikeoutLineStyle?((StrikeoutLineStyle) style);
        for (int index = 1; index < intList.Count; ++index)
        {
          if ((this._queryTern.TerFont[intList[index]].style & 128 /*0x80*/) == 0)
          {
            bool flag = true;
            Color? nullable2;
            if (this._queryTern.TerGetTextColor(intList[index], out color1, out TextBackColor))
            {
              Color color2 = color1;
              nullable2 = this._queryTextColor;
              if ((nullable2.HasValue ? (color2 != nullable2.GetValueOrDefault() ? 1 : 0) : 1) != 0)
                this._queryTextColor = new Color?();
              else
                flag = false;
              Color color3 = TextBackColor;
              nullable2 = this._queryTextBkColor;
              if ((nullable2.HasValue ? (color3 != nullable2.GetValueOrDefault() ? 1 : 0) : 1) != 0)
                this._queryTextBkColor = new Color?();
              else
                flag = false;
            }
            this._queryTern.TerGetFontParam(intList[index], 5, out color1);
            Color color4 = color1;
            nullable2 = this._queryULColor;
            if ((nullable2.HasValue ? (color4 != nullable2.GetValueOrDefault() ? 1 : 0) : 1) != 0)
              this._queryULColor = new Color?();
            else
              flag = false;
            if (flag)
              break;
          }
        }
        for (int index = 1; index < intList.Count; ++index)
        {
          if ((this._queryTern.TerFont[intList[index]].style & 128 /*0x80*/) == 0 && this._queryTern.GetFontInfo2(intList[index], out TypeFace, out TwipsSize, out style))
          {
            if (TypeFace != this._queryTypeface)
              this._queryTypeface = (string) null;
            int num25 = TwipsSize;
            nullable1 = this._queryPointSize;
            int valueOrDefault = nullable1.GetValueOrDefault();
            if (!(num25 == valueOrDefault & nullable1.HasValue))
              this._queryPointSize = new int?();
            BoldItalicStyle boldItalicStyle = (BoldItalicStyle) (style & 6);
            if (this._queryCharFormatforStyles.BoldItalic.HasValue && this._queryCharFormatforStyles.BoldItalic.Value != boldItalicStyle)
              this._queryCharFormatforStyles.BoldItalic = new BoldItalicStyle?();
            UnderlineStyle? nullable3 = this._queryCharFormatforStyles.Underline;
            UnderlineStyle underlineStyle = (UnderlineStyle) style;
            if (!(nullable3.GetValueOrDefault() == underlineStyle & nullable3.HasValue))
            {
              CharFormat charFormatforStyles = this._queryCharFormatforStyles;
              nullable3 = new UnderlineStyle?();
              UnderlineStyle? nullable4 = nullable3;
              charFormatforStyles.Underline = nullable4;
            }
            StrikeoutLineStyle? nullable5 = this._queryCharFormatforStyles.Strike;
            StrikeoutLineStyle strikeoutLineStyle = (StrikeoutLineStyle) style;
            if (!(nullable5.GetValueOrDefault() == strikeoutLineStyle & nullable5.HasValue))
            {
              CharFormat charFormatforStyles = this._queryCharFormatforStyles;
              nullable5 = new StrikeoutLineStyle?();
              StrikeoutLineStyle? nullable6 = nullable5;
              charFormatforStyles.Strike = nullable6;
            }
            int? queryStyles1 = this._queryStyles;
            int num26 = style;
            nullable1 = queryStyles1.HasValue ? new int?((queryStyles1.GetValueOrDefault() ^ num26) & 65536 /*0x010000*/) : new int?();
            int num27 = 0;
            if (!(nullable1.GetValueOrDefault() == num27 & nullable1.HasValue))
              this._queryCharFormatforStyles.AllCaps = new bool?();
            int? queryStyles2 = this._queryStyles;
            int num28 = style;
            nullable1 = queryStyles2.HasValue ? new int?((queryStyles2.GetValueOrDefault() ^ num28) & 131072 /*0x020000*/) : new int?();
            int num29 = 0;
            if (!(nullable1.GetValueOrDefault() == num29 & nullable1.HasValue))
              this._queryCharFormatforStyles.AllSmallCaps = new bool?();
            int? queryStyles3 = this._queryStyles;
            int num30 = style;
            nullable1 = queryStyles3.HasValue ? new int?((queryStyles3.GetValueOrDefault() ^ num30) & 32 /*0x20*/) : new int?();
            int num31 = 0;
            if (!(nullable1.GetValueOrDefault() == num31 & nullable1.HasValue))
              this._queryCharFormatforStyles.Subscript = new bool?();
            int? queryStyles4 = this._queryStyles;
            int num32 = style;
            nullable1 = queryStyles4.HasValue ? new int?((queryStyles4.GetValueOrDefault() ^ num32) & 16 /*0x10*/) : new int?();
            int num33 = 0;
            if (!(nullable1.GetValueOrDefault() == num33 & nullable1.HasValue))
              this._queryCharFormatforStyles.Superscript = new bool?();
            int? queryStyles5 = this._queryStyles;
            int num34 = style;
            nullable1 = queryStyles5.HasValue ? new int?((queryStyles5.GetValueOrDefault() ^ num34) & 64 /*0x40*/) : new int?();
            int num35 = 0;
            if (!(nullable1.GetValueOrDefault() == num35 & nullable1.HasValue))
              this._queryCharFormatforStyles.HiddenText = new bool?();
            nullable1 = this._queryStyles;
            int num36 = style;
            this._queryStyles = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() & num36) : new int?();
          }
        }
      }
      else
      {
        this._queryTypeface = "Arial";
        this._queryPointSize = new int?(240 /*0xF0*/);
        this._queryStyles = new int?(0);
        this._queryTextColor = new Color?(Color.Black);
        this._queryTextBkColor = new Color?(Color.White);
        this._queryULColor = new Color?(Color.Black);
      }
      this._queryFontFamily = this._queryTypeface;
      if (this._queryPointSize.HasValue)
      {
        nullable1 = this._queryPointSize;
        this._queryFontSize = new float?((float) (int) Math.Truncate((double) (nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() / 20) : new int?()).Value));
      }
      else
      {
        nullable1 = this._queryPointSize;
        this._queryFontSize = nullable1.HasValue ? new float?((float) nullable1.GetValueOrDefault()) : new float?();
      }
    }
    this.QueryCharFormat((IList<DocumentTreeNode>) this._queryStatusContext, ref this._queryCharFormatforStyles);
    this.QueryParagraphFormat((IList<DocumentTreeNode>) this._queryStatusContext, ref this._queryParagraphFormatforStyles);
    this._queryContextWasChanged = this._queryStatusContext == null || DocumentTreeNodeCollection.ContentEquals((IList<DocumentTreeNode>) this._queryStatusContext, (IList<DocumentTreeNode>) this._oldQueryStatusContext);
    this._oldQueryStatusContext = this._queryStatusContext;
    if (!this._queryContextWasChanged || this.GetToolbarBorder() == null)
      return;
    this._queryBordersLeft = this.QueryStatus_LeftBorders(this.documentControl.SelectedNodes);
    this._queryBordersTop = this.QueryStatus_TopBorders(this.documentControl.SelectedNodes);
    this._queryBordersRight = this.QueryStatus_RightBorders(this.documentControl.SelectedNodes);
    this._queryBordersBottom = this.QueryStatus_BottomBorders(this.documentControl.SelectedNodes);
    this._queryHorzAlignment = this.QueryHorzAlignment((IList<DocumentTreeNode>) this._queryStatusContext);
    this._queryBordersHorisontal = new bool?(this.QueryStatus_HorizontalBorders(this.documentControl.SelectedNodes));
    this._queryBordersVertical = new bool?(this.QueryStatus_VerticalBorders(this.documentControl.SelectedNodes));
  }

  /// <summary> Начало проверки статуса команд  </summary>
  public virtual void BeginQuery()
  {
    this._IsQueryChacheIsInit = false;
    this.InitQueryCache();
  }

  /// <summary> Завершения проверки статуса команд  </summary>
  public virtual void EndQuery()
  {
    this._oldQueryStatusContext = (List<DocumentTreeNode>) null;
    this._queryStatusContext = (List<DocumentTreeNode>) null;
    this._queryStatusFormatText = false;
    this._queryStatusFormatCharFormat = false;
    this._queryTern = (ImRtfEditor) null;
    if (this.documentControl != null)
      this.documentControl.QueryCache_HasLockedNodes = false;
    this._queryIsAllTextSelected = true;
    this._queryFontFamily = (string) null;
    this._queryFontSize = new float?();
    this._queryFirstLineSelection = -1;
    this._queryFirstColSelection = -1;
    this._queryEndLineSelection = -1;
    this._queryEndColSelection = -1;
    this._queryLeftIndent = new int?(-1);
    this._queryRigthIndent = new int?(-1);
    this._queryFirstIndent = new int?(-1);
    this._queryFlags = -1;
    this._queryLineSpacing = new int?();
    this._queryHorzAlignment = new HorzAlignment?();
    this._queryDocumentNodeList = (List<DocumentTreeNode>) null;
    this._queryCharFormatforStyles = (CharFormat) null;
    this._queryParagraphFormatforStyles = (ParagraphFormat) null;
    this._IsQueryChacheIsInit = false;
  }

  /// <summary>Включить команды редактора таблиц</summary>
  public virtual bool TableEditCommandsEnabled => true;

  /// <summary>Включить базовые команды редактора документов</summary>
  public virtual bool BaseEditCommandsEnabled => true;

  /// <summary>При закрытии окна убивать документ</summary>
  public bool DisposeDocumentOnClose
  {
    get => this.disposeDocumentOnClose;
    set => this.disposeDocumentOnClose = value;
  }

  /// <summary>Проверить статус команды</summary>
  /// <param name="commandState">Состояние команды</param>
  /// <returns>true, если нашел команду</returns>
  public virtual bool QueryStatus(ICommandState commandState)
  {
    try
    {
      if (this.MenuHelper == null || this.DocumentControl == null || this.Document == null || this.Document.LoadFromStreamThread != null)
        return false;
      if (this.InvokeRequired)
        return (bool) this.Invoke((Delegate) new QueryStatusInvoker(this.QueryStatus), (object) commandState);
      if (commandState == null)
        return false;
      bool flag1 = false;
      if (this.documentControl != null)
        flag1 = this.documentControl.QueryCache_HasLockedNodes;
      switch (commandState.CommandName)
      {
        case "AddToUserDictionary":
          int pWordIdx = 0;
          int pWordLen = 0;
          bool flag2 = false;
          if (NodeContextMenu.ContextMenuCommand)
          {
            ImRtfEditor activeEditorControl = this.DocumentControl.GetActiveEditorControl();
            flag2 = activeEditorControl != null && NodeContextMenu.ContextMenuCommand && activeEditorControl.spl.GetMisspelledWord(activeEditorControl.MouseLine, activeEditorControl.MouseCol, out string _, ref pWordIdx, ref pWordLen);
          }
          commandState.Visible = flag2;
          commandState.Enabled = flag2;
          return true;
        case "CallEditor":
        case "DocEditor.EditFormula":
          bool flag3 = false;
          if (this._queryIsFormula && this.documentControl != null && !this.documentControl.ReadOnly)
          {
            DocumentTreeNode[] documentTreeNodeArray = NodeContextMenu.ContextForContextMenu;
            if (documentTreeNodeArray == null || !NodeContextMenu.ContextMenuCommand)
              documentTreeNodeArray = this.documentControl.GetSelectedNodes();
            flag3 = documentTreeNodeArray.Length == 1 && documentTreeNodeArray[0] is TextBoxElement && !this.documentControl.QueryCache_HasLockedNodes;
          }
          bool flag4 = flag3 & !this._queryIsProtectedZone;
          if (commandState.CommandName == "CallEditor" && !flag4)
            return false;
          commandState.Enabled = flag4;
          return true;
        case "DocEditor.InsertFormula":
          bool flag5 = false;
          if (this.documentControl != null && !this.documentControl.ReadOnly)
          {
            DocumentTreeNode[] documentTreeNodeArray = NodeContextMenu.ContextForContextMenu;
            if (documentTreeNodeArray == null || !NodeContextMenu.ContextMenuCommand)
              documentTreeNodeArray = this.documentControl.GetSelectedNodes();
            flag5 = documentTreeNodeArray.Length == 1 && documentTreeNodeArray[0] is TextBoxElement && !((PageElementNode) documentTreeNodeArray[0]).ReadOnly && !this.documentControl.QueryCache_HasLockedNodes;
          }
          bool flag6 = flag5 & !this._queryIsProtectedZone;
          commandState.Enabled = flag6;
          return true;
        case "ExportToWMF":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        case "LineStyleSetup":
        case "PrintPreview":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        case "Navigation.FirstPage":
          commandState.Visible = true;
          bool flag7 = false;
          DocumentTreeNodeCollection nodes1 = this.Document.Nodes;
          // ISSUE: explicit non-virtual call
          if ((nodes1 != null ? __nonvirtual (nodes1.Count) : 0) > 0 && ImDocumentData.GetFirstPage((DocumentTreeNode) this.Document) is Intermech.Document.Model.Page firstPage && firstPage != this.DocumentControl.ActivePage)
            flag7 = true;
          commandState.Enabled = flag7;
          return true;
        case "Navigation.GoToDocument":
          commandState.Visible = this.DocumentsComplect != null;
          bool visible = commandState.Visible;
          if (this.DocumentsComplect != null)
          {
            System.Windows.Forms.ComboBox.ObjectCollection items = this.MenuHelper.CbDocument.ComboBox.Items;
            items.Clear();
            foreach (DocumentTreeNode allDocument in this.DocumentsComplect.GetAllDocuments())
              items.Add((object) allDocument);
          }
          if (this.DocumentControl.Document != null)
            this.MenuHelper.CbDocument.ComboBox.SelectedItem = (object) this.DocumentControl.Document;
          else
            this.MenuHelper.CbDocument.ComboBox.SelectedIndex = -1;
          commandState.Enabled = visible;
          this.MenuHelper.SetVisibleDocumentButtons(commandState.Visible);
          return true;
        case "Navigation.GoToPage":
          commandState.Visible = true;
          bool flag8 = false;
          if (this.Document.Nodes != null)
          {
            if (!this.Document.IsFormulaLib)
              this.MenuHelper.CbPage.MinimumControlWidth = 50;
            else
              this.MenuHelper.CbPage.MinimumControlWidth = 200;
            System.Windows.Forms.ComboBox.ObjectCollection items = this.MenuHelper.CbPage.ComboBox.Items;
            if (this.Document.Nodes.Count != items.Count)
            {
              if (this.Document.Nodes.Count < items.Count)
              {
                for (int index = items.Count - 1; index >= this.Document.Nodes.Count; --index)
                  items.RemoveAt(index);
              }
              else
              {
                for (int count = items.Count; count < this.Document.Nodes.Count; ++count)
                {
                  string str = this.Document.IsFormulaLib ? this.Document.Nodes[count].GetDefautCaption() : (count + 1).ToString();
                  items.Add((object) str);
                }
              }
            }
          }
          if (this.DocumentControl.ActivePage != null)
            this.MenuHelper.CbPage.ComboBox.SelectedIndex = this.DocumentControl.ActivePage.Index;
          else
            this.MenuHelper.CbPage.ComboBox.SelectedIndex = -1;
          commandState.Enabled = flag8;
          return true;
        case "Navigation.LastPage":
          commandState.Visible = true;
          bool flag9 = false;
          DocumentTreeNodeCollection nodes2 = this.Document.Nodes;
          // ISSUE: explicit non-virtual call
          if ((nodes2 != null ? (__nonvirtual (nodes2.Count) > 0 ? 1 : 0) : 0) != 0)
          {
            PageData lastPage = ImDocumentData.GetLastPage((DocumentTreeNode) this.Document);
            if (lastPage != null && lastPage != this.DocumentControl.ActivePage)
              flag9 = true;
          }
          commandState.Enabled = flag9;
          return true;
        case "Navigation.NextDocument":
          commandState.Visible = this.DocumentsComplect != null;
          bool flag10 = false;
          if (this.DocumentsComplect != null)
            flag10 = DocumentsComplect.GetNextDocument(this.Document.Parent, this.Document.Index, false) != null;
          commandState.Enabled = flag10;
          this.MenuHelper.SetVisibleDocumentButtons(commandState.Visible);
          return true;
        case "Navigation.NextPage":
          commandState.Visible = true;
          bool flag11 = false;
          if (this.DocumentControl.ActivePage != null && this.DocumentControl.ActivePage.Parent != null && ImDocumentData.GetNextPage(this.DocumentControl.ActivePage.Parent, this.DocumentControl.ActivePage.Index, false) != null)
            flag11 = true;
          commandState.Enabled = flag11;
          return true;
        case "Navigation.PrevDocument":
          commandState.Visible = this.DocumentsComplect != null;
          bool flag12 = false;
          if (this.DocumentsComplect != null)
            flag12 = DocumentsComplect.GetPrevDocument(this.Document.Parent, this.Document.Index, false) != null;
          commandState.Enabled = flag12;
          this.MenuHelper.SetVisibleDocumentButtons(commandState.Visible);
          return true;
        case "Navigation.PrevPage":
          commandState.Visible = true;
          bool flag13 = false;
          if (this.DocumentControl.ActivePage != null && this.DocumentControl.ActivePage.Parent != null && ImDocumentData.GetPrevPage(this.DocumentControl.ActivePage.Parent, this.DocumentControl.ActivePage.Index, false) != null)
            flag13 = true;
          commandState.Enabled = flag13;
          return true;
        case "Print":
        case "PrintDocument":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        case "SaveAs":
          commandState.Visible = true;
          commandState.Enabled = true;
          return true;
        default:
          if (commandState.CommandName.StartsWith("Format."))
          {
            if (!this._queryStatusFormatText | flag1)
            {
              if (commandState.CommandName.StartsWith("Format.Font.") && this._queryStatusFormatCharFormat)
              {
                commandState.Enabled = true;
              }
              else
              {
                commandState.Enabled = false;
                commandState.Checked = false;
              }
            }
            else
              commandState.Enabled = true;
            if (commandState.CommandName.StartsWith("Format.Font.Registr"))
              return true;
            switch (commandState.CommandName)
            {
              case "Format.BgColor":
                return true;
              case "Format.Borders":
                bool isEnabled1 = false;
                bool isChecked1 = false;
                if (!flag1 && DocumentMenuHelper.ActiveBordersCommand != null && this._queryStatusContext != null && !this.HasTemplate((IList<DocumentTreeNode>) this._queryStatusContext))
                  this.GetBordersCommandEnabled((IList<DocumentTreeNode>) this._queryStatusContext, DocumentMenuHelper.ActiveBordersCommand, ref isEnabled1, ref isChecked1);
                commandState.Enabled = isEnabled1;
                commandState.Checked = isChecked1;
                return true;
              case "Format.BulletsList":
                commandState.Enabled = commandState.Enabled && this._queryTern != null;
                commandState.Checked = commandState.Enabled && this.QueryStatus_ListInParagraph((IList<DocumentTreeNode>) this._queryStatusContext, true);
                return true;
              case "Format.CellAlign":
                ImDocumentEditorFormBase._lastVertAlignment = this.QueryStatus_TextVAlign((IList<DocumentTreeNode>) this._queryStatusContext);
                if (this._queryHorzAlignment.HasValue)
                  ImDocumentEditorFormBase._lastHorzAlignment = this._queryHorzAlignment.Value;
                Image image = (Image) null;
                IconicMenuItem iconicMenuItem = (IconicMenuItem) null;
                switch (ImDocumentEditorFormBase._lastVertAlignment)
                {
                  case VertAlignment.Top:
                    switch (ImDocumentEditorFormBase._lastHorzAlignment)
                    {
                      case HorzAlignment.Left:
                        image = DocumentMenuHelper.CaLeftTopImage;
                        iconicMenuItem = this.MenuHelper.CaLeftTopIconicMenuItem;
                        break;
                      case HorzAlignment.Center:
                        image = DocumentMenuHelper.CaCenterTopImage;
                        iconicMenuItem = this.MenuHelper.CaCenterTopIconicMenuItem;
                        break;
                      case HorzAlignment.Right:
                        image = DocumentMenuHelper.CaRightTopImage;
                        iconicMenuItem = this.MenuHelper.CaRightTopIconicMenuItem;
                        break;
                      case HorzAlignment.Justify:
                        image = DocumentMenuHelper.CaJustifyTopImage;
                        iconicMenuItem = this.MenuHelper.CaJustifyTopIconicMenuItem;
                        break;
                    }
                    break;
                  case VertAlignment.Center:
                    switch (ImDocumentEditorFormBase._lastHorzAlignment)
                    {
                      case HorzAlignment.Left:
                        image = DocumentMenuHelper.CaLeftCenterImage;
                        iconicMenuItem = this.MenuHelper.CaLeftCenterIconicMenuItem;
                        break;
                      case HorzAlignment.Center:
                        image = DocumentMenuHelper.CaCenterCenterImage;
                        iconicMenuItem = this.MenuHelper.CaCenterCenterIconicMenuItem;
                        break;
                      case HorzAlignment.Right:
                        image = DocumentMenuHelper.CaRightCenterImage;
                        iconicMenuItem = this.MenuHelper.CaRightCenterIconicMenuItem;
                        break;
                      case HorzAlignment.Justify:
                        image = DocumentMenuHelper.CaJustifyCenterImage;
                        iconicMenuItem = this.MenuHelper.CaJustifyCenterIconicMenuItem;
                        break;
                    }
                    break;
                  case VertAlignment.Bottom:
                    switch (ImDocumentEditorFormBase._lastHorzAlignment)
                    {
                      case HorzAlignment.Left:
                        image = DocumentMenuHelper.CaLeftBottomImage;
                        iconicMenuItem = this.MenuHelper.CaLeftBottomIconicMenuItem;
                        break;
                      case HorzAlignment.Center:
                        image = DocumentMenuHelper.CaCenterBottomImage;
                        iconicMenuItem = this.MenuHelper.CaCenterBottomIconicMenuItem;
                        break;
                      case HorzAlignment.Right:
                        image = DocumentMenuHelper.CaRightBottomImage;
                        iconicMenuItem = this.MenuHelper.CaRightBottomIconicMenuItem;
                        break;
                      case HorzAlignment.Justify:
                        image = DocumentMenuHelper.CaJustifyBottomImage;
                        iconicMenuItem = this.MenuHelper.CaJustifyBottomIconicMenuItem;
                        break;
                    }
                    break;
                }
                if (this.MenuHelper._iconicMenu.Image != image)
                {
                  this.MenuHelper._iconicMenu.Image = image;
                  this.MenuHelper._iconicMenu.Image = image;
                }
                this.MenuHelper.CaLeftTopIconicMenuItem.Checked = this.MenuHelper.CaLeftTopIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaCenterTopIconicMenuItem.Checked = this.MenuHelper.CaCenterTopIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaRightTopIconicMenuItem.Checked = this.MenuHelper.CaRightTopIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaJustifyTopIconicMenuItem.Checked = this.MenuHelper.CaJustifyTopIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaLeftCenterIconicMenuItem.Checked = this.MenuHelper.CaLeftCenterIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaCenterCenterIconicMenuItem.Checked = this.MenuHelper.CaCenterCenterIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaRightCenterIconicMenuItem.Checked = this.MenuHelper.CaRightCenterIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaJustifyCenterIconicMenuItem.Checked = this.MenuHelper.CaJustifyCenterIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaLeftBottomIconicMenuItem.Checked = this.MenuHelper.CaLeftBottomIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaCenterBottomIconicMenuItem.Checked = this.MenuHelper.CaCenterBottomIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaRightBottomIconicMenuItem.Checked = this.MenuHelper.CaRightBottomIconicMenuItem == iconicMenuItem;
                this.MenuHelper.CaJustifyBottomIconicMenuItem.Checked = this.MenuHelper.CaJustifyBottomIconicMenuItem == iconicMenuItem;
                return true;
              case "Format.DecreaseIdent":
                commandState.Enabled = commandState.Enabled && this._queryTern != null;
                return true;
              case "Format.Font.Registr":
                return true;
              case "Format.Font.SetupFont":
                if (this.MenuHelper.ChooseFontComboBoxToolbarItem != null && this.MenuHelper.CbFontSize != null && !this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.IsDisposed && !this.MenuHelper.CbFontSize.ComboBox.IsDisposed)
                {
                  this.MenuHelper.ChooseFontComboBoxToolbarItem.Enabled = commandState.Enabled;
                  this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.Enabled = this.MenuHelper.ChooseFontComboBoxToolbarItem.Enabled;
                  this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.BackColor = this.MenuHelper.ChooseFontComboBoxToolbarItem.Enabled ? SystemColors.Window : SystemColors.Control;
                  this.MenuHelper.CbFontSize.Enabled = commandState.Enabled && !this._queryIsFontAutoSize;
                  this.MenuHelper.CbFontSize.ComboBox.Enabled = this.MenuHelper.CbFontSize.Enabled;
                  this.MenuHelper.CbFontSize.ComboBox.SelectionLength = 0;
                  this.MenuHelper.CbFontSize.ComboBox.BackColor = this.MenuHelper.CbFontSize.Enabled ? SystemColors.Window : SystemColors.Control;
                  string str = this._queryIsAllTextSelected ? this.GetCurentFontFamily((IList<DocumentTreeNode>) this._queryStatusContext) : (string) null;
                  if (string.IsNullOrEmpty(str))
                    str = this._queryFontFamily;
                  this.LockFontChange();
                  try
                  {
                    if (str == null || str == string.Empty)
                      this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndex = -1;
                    else
                      this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndex = this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.Items.IndexOf((object) str);
                  }
                  finally
                  {
                    this.UnlockFontChange();
                  }
                  float? nullable1 = this._queryIsAllTextSelected ? this.GetCurentFontSize((IList<DocumentTreeNode>) this._queryStatusContext) : new float?();
                  float? nullable2;
                  if (nullable1.HasValue)
                  {
                    nullable2 = nullable1;
                    float num = 0.0f;
                    if (!((double) nullable2.GetValueOrDefault() == (double) num & nullable2.HasValue))
                      goto label_119;
                  }
                  nullable1 = this._queryFontSize;
label_119:
                  DocumentMenuHelper.LockTextSizeChangeEvents();
                  try
                  {
                    if (nullable1.HasValue)
                    {
                      nullable2 = nullable1;
                      float num = 0.0f;
                      if (!((double) nullable2.GetValueOrDefault() == (double) num & nullable2.HasValue))
                      {
                        this.MenuHelper.CbFontSize.ComboBox.Text = nullable1.ToString();
                        goto label_124;
                      }
                    }
                    this.MenuHelper.CbFontSize.ComboBox.SelectedIndex = -1;
                    this.MenuHelper.CbFontSize.ComboBox.SelectedItem = (object) string.Empty;
                    this.MenuHelper.CbFontSize.ComboBox.SelectedValue = (object) string.Empty;
                    this.MenuHelper.CbFontSize.ComboBox.Text = string.Empty;
label_124:
                    this.MenuHelper.OldFontSizeValue = this.MenuHelper.CbFontSize.ComboBox.Text;
                  }
                  finally
                  {
                    DocumentMenuHelper.UnlockTextSizeChangeEvents();
                  }
                }
                return true;
              case "Format.Font.Strikeout":
                commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.Strike.HasValue && (this._queryCharFormatforStyles.Strike.Value & StrikeoutLineStyle.SingleLine) != 0;
                return true;
              case "Format.Font.StrikeoutDouble":
                DocumentTreeNode[] selectedNodes = this.documentControl.GetSelectedNodes();
                commandState.Visible = selectedNodes.Length == 1 && selectedNodes[0] is TextBoxElement;
                commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.Strike.HasValue && (this._queryCharFormatforStyles.Strike.Value & StrikeoutLineStyle.DoubleLine) != 0;
                return true;
              case "Format.Font.Subscript":
                commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.Subscript.HasValue && this._queryCharFormatforStyles.Subscript.Value;
                return true;
              case "Format.Font.Superscript":
                commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.Superscript.HasValue && this._queryCharFormatforStyles.Superscript.Value;
                return true;
              case "Format.Font.TextBold":
                if (this._queryTern == null)
                {
                  ICommandState commandState1 = commandState;
                  int num;
                  if (commandState.Enabled && this._queryCharFormatforStyles != null)
                  {
                    BoldItalicStyle? boldItalic = this._queryCharFormatforStyles.BoldItalic;
                    BoldItalicStyle? nullable = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & BoldItalicStyle.Bold) : new BoldItalicStyle?();
                    BoldItalicStyle boldItalicStyle = BoldItalicStyle.Regular;
                    num = !(nullable.GetValueOrDefault() == boldItalicStyle & nullable.HasValue) ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState1.Checked = num != 0;
                }
                else
                  commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.BoldItalic.HasValue && (this._queryCharFormatforStyles.BoldItalic.Value & BoldItalicStyle.Bold) != 0;
                return true;
              case "Format.Font.TextCursive":
                if (this._queryTern == null)
                {
                  ICommandState commandState2 = commandState;
                  int num;
                  if (commandState.Enabled && this._queryCharFormatforStyles != null)
                  {
                    BoldItalicStyle? boldItalic = this._queryCharFormatforStyles.BoldItalic;
                    BoldItalicStyle? nullable = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & BoldItalicStyle.Italic) : new BoldItalicStyle?();
                    BoldItalicStyle boldItalicStyle = BoldItalicStyle.Regular;
                    num = !(nullable.GetValueOrDefault() == boldItalicStyle & nullable.HasValue) ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState2.Checked = num != 0;
                }
                else
                  commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.BoldItalic.HasValue && (this._queryCharFormatforStyles.BoldItalic.Value & BoldItalicStyle.Italic) != 0;
                return true;
              case "Format.Font.TextUnderline":
                if (this._queryTern == null)
                {
                  ICommandState commandState3 = commandState;
                  int num;
                  if (commandState.Enabled && this._queryCharFormatforStyles != null)
                  {
                    UnderlineStyle? underline = this._queryCharFormatforStyles.Underline;
                    UnderlineStyle? nullable = underline.HasValue ? new UnderlineStyle?(underline.GetValueOrDefault() & UnderlineStyle.Underline) : new UnderlineStyle?();
                    UnderlineStyle underlineStyle = UnderlineStyle.None;
                    num = !(nullable.GetValueOrDefault() == underlineStyle & nullable.HasValue) ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState3.Checked = num != 0;
                }
                else
                  commandState.Checked = commandState.Enabled && this._queryCharFormatforStyles != null && this._queryCharFormatforStyles.Underline.HasValue && (this._queryCharFormatforStyles.Underline.Value & UnderlineStyle.Underline) != 0;
                return true;
              case "Format.IncreaseIdent":
                commandState.Enabled = commandState.Enabled && this._queryTern != null;
                return true;
              case "Format.NumberingList":
                commandState.Enabled = commandState.Enabled && this._queryTern != null;
                commandState.Checked = commandState.Enabled && this.QueryStatus_ListInParagraph((IList<DocumentTreeNode>) this._queryStatusContext, false);
                return true;
              case "Format.SetupBordersAndBackground":
                commandState.Visible = false;
                commandState.Enabled = false;
                return true;
              case "Format.SetupParagraph":
                return true;
              case "Format.SetupTextDirrection":
                bool flag14 = this.QueryStatus_SetupTextDirrection((IList<DocumentTreeNode>) this._queryStatusContext);
                commandState.Enabled = flag14;
                return true;
              case "Format.TextAlignCenter":
                ParagraphFormat paragraphFormatforStyles1 = this._queryParagraphFormatforStyles;
                if (this._queryTern != null)
                {
                  commandState.Checked = commandState.Enabled && (this._queryFlags & 1) != 0;
                }
                else
                {
                  ICommandState commandState4 = commandState;
                  int num;
                  if (commandState.Enabled && paragraphFormatforStyles1 != null)
                  {
                    HorzAlignment? horzAlignment1 = paragraphFormatforStyles1.HorzAlignment;
                    HorzAlignment horzAlignment2 = HorzAlignment.Center;
                    num = horzAlignment1.GetValueOrDefault() == horzAlignment2 & horzAlignment1.HasValue ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState4.Checked = num != 0;
                }
                return true;
              case "Format.TextAlignJustify":
                ParagraphFormat paragraphFormatforStyles2 = this._queryParagraphFormatforStyles;
                if (this._queryTern != null)
                {
                  commandState.Checked = commandState.Enabled && (this._queryFlags & 2048 /*0x0800*/) != 0;
                }
                else
                {
                  ICommandState commandState5 = commandState;
                  int num;
                  if (commandState.Enabled && paragraphFormatforStyles2 != null)
                  {
                    HorzAlignment? horzAlignment3 = paragraphFormatforStyles2.HorzAlignment;
                    HorzAlignment horzAlignment4 = HorzAlignment.Justify;
                    num = horzAlignment3.GetValueOrDefault() == horzAlignment4 & horzAlignment3.HasValue ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState5.Checked = num != 0;
                }
                return true;
              case "Format.TextAlignLeft":
                ParagraphFormat paragraphFormatforStyles3 = this._queryParagraphFormatforStyles;
                if (this._queryTern != null)
                {
                  commandState.Checked = commandState.Enabled && (this._queryFlags & 1024 /*0x0400*/) != 0;
                }
                else
                {
                  ICommandState commandState6 = commandState;
                  int num;
                  if (commandState.Enabled && paragraphFormatforStyles3 != null)
                  {
                    HorzAlignment? horzAlignment5 = paragraphFormatforStyles3.HorzAlignment;
                    HorzAlignment horzAlignment6 = HorzAlignment.Left;
                    num = horzAlignment5.GetValueOrDefault() == horzAlignment6 & horzAlignment5.HasValue ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState6.Checked = num != 0;
                }
                return true;
              case "Format.TextAlignRight":
                ParagraphFormat paragraphFormatforStyles4 = this._queryParagraphFormatforStyles;
                if (this._queryTern != null)
                {
                  commandState.Checked = commandState.Enabled && (this._queryFlags & 2) != 0;
                }
                else
                {
                  ICommandState commandState7 = commandState;
                  int num;
                  if (commandState.Enabled && paragraphFormatforStyles4 != null)
                  {
                    HorzAlignment? horzAlignment7 = paragraphFormatforStyles4.HorzAlignment;
                    HorzAlignment horzAlignment8 = HorzAlignment.Right;
                    num = horzAlignment7.GetValueOrDefault() == horzAlignment8 & horzAlignment7.HasValue ? 1 : 0;
                  }
                  else
                    num = 0;
                  commandState7.Checked = num != 0;
                }
                return true;
              case "Format.TextBkColor":
                commandState.Enabled = commandState.Enabled && this._queryTern != null;
                return true;
              case "Format.TextColor":
                return true;
              case "Format.TextSpaceBetweenLines":
                commandState.Enabled = !flag1 && commandState.Enabled;
                ICommandState commandState8 = commandState;
                int num1;
                if (commandState.Enabled && this._queryLineSpacing.HasValue)
                {
                  int? queryLineSpacing = this._queryLineSpacing;
                  int? lastSetLineSpacing = this._lastSetLineSpacing;
                  num1 = queryLineSpacing.GetValueOrDefault() == lastSetLineSpacing.GetValueOrDefault() & queryLineSpacing.HasValue == lastSetLineSpacing.HasValue ? 1 : 0;
                }
                else
                  num1 = 0;
                commandState8.Checked = num1 != 0;
                return true;
              default:
                if (commandState.CommandName.StartsWith("Format.Borders."))
                {
                  bool isEnabled2 = false;
                  bool isChecked2 = false;
                  if (!flag1)
                    this.GetBordersCommandEnabled((IList<DocumentTreeNode>) this._queryStatusContext, commandState.CommandName, ref isEnabled2, ref isChecked2);
                  commandState.Enabled = isEnabled2;
                  commandState.Checked = isChecked2;
                  if (this.MenuHelper.BordersToolButton != null && commandState.CommandName.Equals(DocumentMenuHelper.ActiveBordersCommand))
                  {
                    if (this.MenuHelper.BordersToolButton.Enabled != commandState.Enabled)
                      this.MenuHelper.BordersToolButton.Enabled = commandState.Enabled;
                    if (this.MenuHelper.BordersToolButton.Checked != commandState.Checked)
                      this.MenuHelper.BordersToolButton.Checked = commandState.Checked;
                    if (this.MenuHelper.CbLineStyle != null)
                      this.MenuHelper.CbLineStyle.Enabled = commandState.Enabled;
                    if (this.MenuHelper.CbLineWidth != null)
                      this.MenuHelper.CbLineWidth.Enabled = commandState.Enabled;
                  }
                  return true;
                }
                if (commandState.CommandName.StartsWith("Format.TextSpaceBetweenLines."))
                {
                  commandState.Checked = false;
                  if (!commandState.Enabled || !this._queryLineSpacing.HasValue)
                    return true;
                  MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandState.CommandName);
                  commandState.Checked = menuItem != null && menuItem.Tag != null && menuItem.Tag is int && (int) menuItem.Tag == this._queryLineSpacing.Value;
                  return true;
                }
                break;
            }
          }
          return false;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  protected virtual void Init()
  {
  }

  private bool HasTemplate(IList<DocumentTreeNode> elems)
  {
    if (elems == null)
      throw new ArgumentNullException(nameof (elems));
    for (int index1 = 0; index1 < elems.Count; ++index1)
    {
      if (elems[index1] is RectangleElement)
      {
        RectangleElement elem = elems[index1] as RectangleElement;
        if (elem.IsVirtualNode)
        {
          List<DocumentTreeNode> realCells = elem.GetRealCells();
          for (int index2 = 0; index2 < realCells.Count; ++index2)
          {
            if (realCells[index2].TemplateId != null)
              return true;
          }
        }
        else if (elem.TemplateId != null)
          return true;
      }
    }
    return false;
  }

  private void GetBordersCommandEnabled(
    IList<DocumentTreeNode> context,
    string commandName,
    ref bool isEnabled,
    ref bool isChecked)
  {
    isEnabled = context != null && !this.ReadOnly;
    if (!isEnabled)
    {
      isChecked = false;
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
      {
        case 44481532:
          if (!(commandName == "Format.Borders.Delete"))
            break;
          isChecked = false;
          break;
        case 803622432:
          if (!(commandName == "Format.Borders.Diagonal.TopLeft"))
            break;
          isEnabled = false;
          isChecked = false;
          break;
        case 1236359072:
          if (!(commandName == "Format.Borders.Bottom"))
            break;
          isChecked = this._queryBordersBottom;
          break;
        case 1488192673:
          if (!(commandName == "Format.Borders.Vertical"))
            break;
          ref bool local1 = ref isChecked;
          bool? queryBordersVertical1 = this._queryBordersVertical;
          bool flag1 = true;
          int num1 = queryBordersVertical1.GetValueOrDefault() == flag1 & queryBordersVertical1.HasValue ? 1 : 0;
          local1 = num1 != 0;
          break;
        case 1939602238:
          if (!(commandName == "Format.Borders.Horisontal"))
            break;
          ref bool local2 = ref isChecked;
          bool? bordersHorisontal1 = this._queryBordersHorisontal;
          bool flag2 = true;
          int num2 = bordersHorisontal1.GetValueOrDefault() == flag2 & bordersHorisontal1.HasValue ? 1 : 0;
          local2 = num2 != 0;
          break;
        case 2461918249:
          if (!(commandName == "Format.Borders.Inner"))
            break;
          ref bool local3 = ref isChecked;
          bool? bordersHorisontal2 = this._queryBordersHorisontal;
          bool flag3 = true;
          int num3;
          if (bordersHorisontal2.GetValueOrDefault() == flag3 & bordersHorisontal2.HasValue)
          {
            bool? queryBordersVertical2 = this._queryBordersVertical;
            bool flag4 = true;
            num3 = queryBordersVertical2.GetValueOrDefault() == flag4 & queryBordersVertical2.HasValue ? 1 : 0;
          }
          else
            num3 = 0;
          local3 = num3 != 0;
          break;
        case 2977871658:
          if (!(commandName == "Format.Borders.Outer"))
            break;
          isChecked = this._queryBordersLeft && this._queryBordersTop && this._queryBordersRight && this._queryBordersBottom;
          break;
        case 3106350037:
          if (!(commandName == "Format.Borders.Diagonal.TopRight"))
            break;
          isEnabled = false;
          isChecked = false;
          break;
        case 3977994238:
          if (!(commandName == "Format.Borders.Top"))
            break;
          isChecked = this._queryBordersTop;
          break;
        case 4004493482:
          if (!(commandName == "Format.Borders.All"))
            break;
          ref bool local4 = ref isChecked;
          int num4;
          if (this._queryBordersLeft && this._queryBordersTop && this._queryBordersRight && this._queryBordersBottom)
          {
            bool? nullable = this._queryBordersHorisontal;
            bool flag5 = true;
            if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
            {
              nullable = this._queryBordersVertical;
              bool flag6 = true;
              num4 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
              goto label_32;
            }
          }
          num4 = 0;
label_32:
          local4 = num4 != 0;
          break;
        case 4198414646:
          if (!(commandName == "Format.Borders.Left"))
            break;
          isChecked = this._queryBordersLeft;
          break;
        case 4225635947:
          if (!(commandName == "Format.Borders.Right"))
            break;
          isChecked = this._queryBordersRight;
          break;
      }
    }
  }

  private bool IsFontChangeLocked() => this._fontChangeLockCounter > 0;

  private void LockFontChange() => ++this._fontChangeLockCounter;

  private void UnlockFontChange()
  {
    if (this._fontChangeLockCounter <= 0)
      return;
    --this._fontChangeLockCounter;
  }

  /// <summary>QueryStatus_FormatTextStyle</summary>
  /// <param name="context"></param>
  /// <param name="checkFontStyle"></param>
  /// <returns></returns>
  public bool QueryStatus_FormatTextStyle(IList<DocumentTreeNode> context, CharStyle checkCharStyle)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_FormatTextStyle(context[index], checkCharStyle))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_FormatTextStyle</summary>
  /// <param name="context"></param>
  /// <param name="checkFontStyle"></param>
  /// <returns></returns>
  public bool QueryStatus_FormatTextStyle(DocumentTreeNode context, CharStyle checkCharStyle)
  {
    if (!(context is TextData textData))
      return false;
    CharFormat charFormat = textData.CharFormat;
    return charFormat != null && (charFormat.CharStyle & checkCharStyle) != 0;
  }

  /// <summary>GetCurentFontFamily</summary>
  /// <param name="documentTreeNodeCollection"></param>
  /// <returns></returns>
  public string GetCurentFontFamily(IList<DocumentTreeNode> documentTreeNodeCollection)
  {
    if (documentTreeNodeCollection == null || documentTreeNodeCollection.Count == 0)
      return string.Empty;
    string curentFontFamily1 = (string) null;
    foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) documentTreeNodeCollection)
    {
      string curentFontFamily2 = this.GetCurentFontFamily(documentTreeNode);
      if (curentFontFamily2 == string.Empty || curentFontFamily2 != null && curentFontFamily1 != null && curentFontFamily1 != curentFontFamily2)
        return string.Empty;
      if (curentFontFamily1 == null)
        curentFontFamily1 = curentFontFamily2;
    }
    return curentFontFamily1;
  }

  /// <summary>GetCurentFontFamily</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public string GetCurentFontFamily(DocumentTreeNode context)
  {
    if (context == null)
      return string.Empty;
    if (context is Intermech.Document.Model.Page)
      return context.Parent is ImDocument ? (context.Parent as ImDocument).DefaultCharFormat.FontFamily : string.Empty;
    if (context is ImDocument)
      return (context as ImDocument).DefaultCharFormat.FontFamily;
    if (context.NodesCount > 0)
      return this.GetCurentFontFamily((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is TextData))
      return string.Empty;
    TextData textData = context as TextData;
    return textData.CharFormat == null ? string.Empty : textData.CharFormat.FontFamily;
  }

  /// <summary>GetCurentFontSize</summary>
  /// <param name="documentTreeNodeCollection"></param>
  /// <returns></returns>
  public float? GetCurentFontSize(IList<DocumentTreeNode> documentTreeNodeCollection)
  {
    if (documentTreeNodeCollection == null || documentTreeNodeCollection.Count == 0)
      return new float?(0.0f);
    float? curentFontSize1 = new float?();
    float? nullable1 = new float?();
    foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) documentTreeNodeCollection)
    {
      float? curentFontSize2 = this.GetCurentFontSize(documentTreeNode);
      float? curentFontSize3 = curentFontSize2;
      float num = 0.0f;
      if ((double) curentFontSize3.GetValueOrDefault() == (double) num & curentFontSize3.HasValue)
      {
        curentFontSize3 = new float?(0.0f);
        return curentFontSize3;
      }
      if (curentFontSize2.HasValue && curentFontSize1.HasValue)
      {
        float? nullable2 = curentFontSize1;
        float? nullable3 = curentFontSize2;
        if (!((double) nullable2.GetValueOrDefault() == (double) nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
        {
          curentFontSize3 = new float?(0.0f);
          return curentFontSize3;
        }
      }
      if (!curentFontSize1.HasValue)
        curentFontSize1 = curentFontSize2;
    }
    return curentFontSize1;
  }

  /// <summary>GetCurentFontSize</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public float? GetCurentFontSize(DocumentTreeNode context)
  {
    if (context == null)
      return new float?(0.0f);
    if (context is Intermech.Document.Model.Page)
      return context.Parent is ImDocument ? (context.Parent as ImDocument).DefaultCharFormat.FontSize : new float?(0.0f);
    if (context is ImDocument)
      return (context as ImDocument).DefaultCharFormat.FontSize;
    if (context.NodesCount > 0)
      return this.GetCurentFontSize((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is TextData))
      return new float?(0.0f);
    TextData textData = context as TextData;
    return textData.CharFormat == null ? new float?(0.0f) : textData.CharFormat.FontSize;
  }

  /// <summary>QueryStatus_TextHAlign</summary>
  /// <param name="context"></param>
  /// <param name="horzAlignment"></param>
  /// <returns></returns>
  public bool QueryStatus_TextHAlign(DocumentTreeNode context, HorzAlignment horzAlignment)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_TextHAlign((IList<DocumentTreeNode>) context.Nodes, horzAlignment);
    if (!(context is TextData))
      return false;
    TextData textData = context as TextData;
    if (textData.ParagraphFormat == null)
      return false;
    HorzAlignment? horzAlignment1 = textData.ParagraphFormat.HorzAlignment;
    HorzAlignment horzAlignment2 = horzAlignment;
    return horzAlignment1.GetValueOrDefault() == horzAlignment2 & horzAlignment1.HasValue;
  }

  /// <summary>QueryStatus_TextHAlign</summary>
  /// <param name="context"></param>
  /// <param name="horzAlignment"></param>
  /// <returns></returns>
  public bool QueryStatus_TextHAlign(IList<DocumentTreeNode> context, HorzAlignment horzAlignment)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_TextHAlign(context[index], horzAlignment))
        return false;
    }
    return true;
  }

  /// <summary>QueryHorzAlignment</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public HorzAlignment? QueryHorzAlignment(DocumentTreeNode context)
  {
    HorzAlignment? nullable = new HorzAlignment?();
    if (context == null || context is Intermech.Document.Model.Page)
      return nullable;
    if (context.NodesCount > 0)
      return this.QueryHorzAlignment((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is TextData))
      return nullable;
    TextData textData = context as TextData;
    return textData.ParagraphFormat == null ? nullable : textData.ParagraphFormat.HorzAlignment;
  }

  /// <summary>QueryHorzAlignment</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public HorzAlignment? QueryHorzAlignment(IList<DocumentTreeNode> context)
  {
    HorzAlignment? nullable1 = new HorzAlignment?();
    HorzAlignment? nullable2 = new HorzAlignment?();
    if (context != null && context.Count > 0)
    {
      for (int index = 0; index < context.Count; ++index)
      {
        HorzAlignment? nullable3 = this.QueryHorzAlignment(context[index]);
        HorzAlignment? nullable4;
        if (!nullable3.HasValue)
        {
          nullable4 = new HorzAlignment?();
          return nullable4;
        }
        if (!nullable1.HasValue)
        {
          nullable1 = nullable3;
        }
        else
        {
          nullable4 = nullable1;
          HorzAlignment? nullable5 = nullable3;
          if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
          {
            nullable5 = new HorzAlignment?();
            return nullable5;
          }
        }
      }
    }
    return nullable1;
  }

  /// <summary>QueryStatus_TextVAlign</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public VertAlignment QueryStatus_TextVAlign(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return ImDocumentEditorFormBase._lastVertAlignment;
    if (context.NodesCount > 0)
      return this.QueryStatus_TextVAlign((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is TextData))
      return ImDocumentEditorFormBase._lastVertAlignment;
    TextData textData = context as TextData;
    if (textData.ParagraphFormat == null)
      return ImDocumentEditorFormBase._lastVertAlignment;
    VertAlignment? vertAlignment = textData.ParagraphFormat.VertAlignment;
    if (!vertAlignment.HasValue)
      return ImDocumentEditorFormBase._lastVertAlignment;
    vertAlignment = textData.ParagraphFormat.VertAlignment;
    return vertAlignment.Value;
  }

  /// <summary>QueryStatus_TextVAlign</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public VertAlignment QueryStatus_TextVAlign(IList<DocumentTreeNode> context)
  {
    VertAlignment? nullable = new VertAlignment?();
    if (context != null && context.Count > 0)
    {
      for (int index = 0; index < context.Count; ++index)
      {
        VertAlignment vertAlignment = this.QueryStatus_TextVAlign(context[index]);
        if (!nullable.HasValue)
          nullable = new VertAlignment?(vertAlignment);
        else if (nullable.Value != vertAlignment)
          return nullable.Value;
      }
    }
    return nullable.HasValue ? nullable.Value : ImDocumentEditorFormBase._lastVertAlignment;
  }

  /// <summary>QueryStatus_OuterBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_OuterBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_OuterBorders(context[index]))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_OuterBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_OuterBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_OuterBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return false;
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable != null && topLevelTable != context)
    {
      Collection<RectangleElement> topCells = rectangleElement1.GetTopCells(true);
      bool flag1 = topCells.Count == 0;
      if (!flag1)
      {
        foreach (RectangleElement rectangleElement2 in topCells)
        {
          if (rectangleElement2 != null && !rectangleElement2.ShowSelected)
          {
            flag1 = true;
            break;
          }
        }
      }
      if (flag1 && rectangleElement1.Borders.Top.Style == BorderStyles.None)
        return false;
      Collection<RectangleElement> rightCells = rectangleElement1.GetRightCells(true);
      bool flag2 = rightCells.Count == 0;
      if (!flag2)
      {
        foreach (RectangleElement rectangleElement3 in rightCells)
        {
          if (rectangleElement3 != null && !rectangleElement3.ShowSelected)
          {
            flag2 = true;
            break;
          }
        }
      }
      if (flag2 && rectangleElement1.Borders.Right.Style == BorderStyles.None)
        return false;
      Collection<RectangleElement> bottomCells = rectangleElement1.GetBottomCells(true);
      bool flag3 = bottomCells.Count == 0;
      if (!flag3)
      {
        foreach (RectangleElement rectangleElement4 in bottomCells)
        {
          if (rectangleElement4 != null && !rectangleElement4.ShowSelected)
          {
            flag3 = true;
            break;
          }
        }
      }
      if (flag3 && rectangleElement1.Borders.Bottom.Style == BorderStyles.None)
        return false;
      Collection<RectangleElement> leftCells = rectangleElement1.GetLeftCells(true);
      bool flag4 = leftCells.Count == 0;
      if (!flag4)
      {
        foreach (RectangleElement rectangleElement5 in leftCells)
        {
          if (rectangleElement5 != null && !rectangleElement5.ShowSelected)
          {
            flag4 = true;
            break;
          }
        }
      }
      return !flag4 || rectangleElement1.Borders.Left.Style != BorderStyles.None;
    }
    return rectangleElement1.Borders.Top.Style != BorderStyles.None && rectangleElement1.Borders.Right.Style != BorderStyles.None && rectangleElement1.Borders.Bottom.Style != BorderStyles.None && rectangleElement1.Borders.Left.Style != 0;
  }

  /// <summary>Проверить статус границ ячеек</summary>
  /// <param name="context">Контекст команды</param>
  /// <param name="borderStatus">Статус границ</param>
  /// <returns>true, если проверка закончена и не требуется проверять остальные ячейки</returns>
  public bool QueryStatus_Borders(IList<DocumentTreeNode> context, BordersStatus borderStatus)
  {
    return false;
  }

  public bool QueryStatus_SetupTextDirrection(IList<DocumentTreeNode> context)
  {
    bool flag = true;
    if (context != null && context.Count > 0)
    {
      foreach (DocumentTreeNode documentTreeNode in (IEnumerable<DocumentTreeNode>) context)
      {
        if (documentTreeNode is TextData textData && textData.Template is TextData)
        {
          DocumentTreeNode template = textData.Template;
          return false;
        }
        flag = this.QueryStatus_SetupTextDirrection((IList<DocumentTreeNode>) documentTreeNode.Nodes);
      }
    }
    return flag;
  }

  /// <summary>QueryStatus_LeftBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_LeftBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_LeftBorders(context[index]))
        return false;
    }
    return true;
  }

  /// <summary>Сравнение с элементом на тулбаре</summary>
  /// <param name="line"></param>
  /// <returns>true, если совпадают</returns>
  private bool CompareWithToolBarBorder(BorderLine line)
  {
    BorderLine toolbarBorder = this.GetToolbarBorder();
    return (double) line.Width == (double) toolbarBorder.Width && line.Style == toolbarBorder.Style && line.Color == toolbarBorder.Color;
  }

  /// <summary>Сравнение с элементом на тулбаре</summary>
  /// <param name="line"></param>
  /// <returns>true, если совпадают</returns>
  private bool CompareWithToolBarBorder(BorderLineTE line)
  {
    BorderLine toolbarBorder = this.GetToolbarBorder();
    return line != null && line.WidthTE.HasValue && line.ColorTE.HasValue && line.StyleTE.HasValue && (double) line.WidthTE.Value == (double) toolbarBorder.Width && line.StyleTE.Value == toolbarBorder.Style && line.ColorTE.Value == toolbarBorder.Color;
  }

  /// <summary>QueryStatus_LeftBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_LeftBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is RectangleElement rectangleElement1))
      return false;
    if (rectangleElement1.IsVirtualNode && rectangleElement1 is TableElement)
    {
      BorderLineTE leftBorderLineTe = (rectangleElement1 as TableElement).LeftBorderLineTE;
      return this.CompareWithToolBarBorder(leftBorderLineTe) && leftBorderLineTe != null && leftBorderLineTe.WidthTE.HasValue && leftBorderLineTe.ColorTE.HasValue && leftBorderLineTe.StyleTE.HasValue && leftBorderLineTe.StyleTE.Value != BorderStyles.None;
    }
    BorderLine leftBorderLine = rectangleElement1.LeftBorderLine;
    if (!this.CompareWithToolBarBorder(leftBorderLine) || leftBorderLine.Style == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is RectangleElement rectangleElement2) || !rectangleElement2.LeftBorderLine.Equals((object) leftBorderLine))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_LeftBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_LeftBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_LeftBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return false;
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return rectangleElement1.Borders.Left.Style != 0;
    Collection<RectangleElement> leftCells = rectangleElement1.GetLeftCells(true);
    bool flag = leftCells.Count == 0;
    if (!flag)
    {
      foreach (RectangleElement rectangleElement2 in leftCells)
      {
        if (rectangleElement2 != null && !rectangleElement2.ShowSelected)
        {
          flag = true;
          break;
        }
      }
    }
    return !flag || rectangleElement1.Borders.Left.Style != BorderStyles.None;
  }

  /// <summary>QueryStatus_TopBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_TopBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_TopBorders(context[index]))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_TopBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_TopBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is RectangleElement rectangleElement1))
      return false;
    if (rectangleElement1.IsVirtualNode && rectangleElement1 is TableElement)
    {
      BorderLineTE topBorderLineTe = (rectangleElement1 as TableElement).TopBorderLineTE;
      return this.CompareWithToolBarBorder(topBorderLineTe) && topBorderLineTe != null && topBorderLineTe.WidthTE.HasValue && topBorderLineTe.ColorTE.HasValue && topBorderLineTe.StyleTE.HasValue && topBorderLineTe.StyleTE.Value != BorderStyles.None;
    }
    BorderLine topBorderLine = rectangleElement1.TopBorderLine;
    if (!this.CompareWithToolBarBorder(topBorderLine) || topBorderLine.Style == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is RectangleElement rectangleElement2) || !rectangleElement2.TopBorderLine.Equals((object) topBorderLine))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_TopBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_TopBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_TopBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return false;
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return rectangleElement1.Borders.Top.Style != 0;
    Collection<RectangleElement> topCells = rectangleElement1.GetTopCells(true);
    bool flag = topCells.Count == 0;
    if (!flag)
    {
      foreach (RectangleElement rectangleElement2 in topCells)
      {
        if (rectangleElement2 != null && !rectangleElement2.ShowSelected)
        {
          flag = true;
          break;
        }
      }
    }
    return !flag || rectangleElement1.Borders.Top.Style != BorderStyles.None;
  }

  /// <summary>QueryStatus_RightBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_RightBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_RightBorders(context[index]))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_RightBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_RightBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is RectangleElement rectangleElement1))
      return false;
    if (rectangleElement1.IsVirtualNode && rectangleElement1 is TableElement)
    {
      BorderLineTE rightBorderLineTe = (rectangleElement1 as TableElement).RightBorderLineTE;
      return this.CompareWithToolBarBorder(rightBorderLineTe) && rightBorderLineTe != null && rightBorderLineTe.WidthTE.HasValue && rightBorderLineTe.ColorTE.HasValue && rightBorderLineTe.StyleTE.HasValue && rightBorderLineTe.StyleTE.Value != BorderStyles.None;
    }
    BorderLine rightBorderLine = rectangleElement1.RightBorderLine;
    if (!this.CompareWithToolBarBorder(rightBorderLine) || rightBorderLine.Style == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is RectangleElement rectangleElement2) || !rectangleElement2.RightBorderLine.Equals((object) rightBorderLine))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_RightBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_RightBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_RightBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return false;
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return rectangleElement1.Borders.Right.Style != 0;
    Collection<RectangleElement> rightCells = rectangleElement1.GetRightCells(true);
    bool flag = rightCells.Count == 0;
    if (!flag)
    {
      foreach (RectangleElement rectangleElement2 in rightCells)
      {
        if (rectangleElement2 != null && !rectangleElement2.ShowSelected)
        {
          flag = true;
          break;
        }
      }
    }
    return !flag || rectangleElement1.Borders.Right.Style != BorderStyles.None;
  }

  /// <summary>QueryStatus_BottomBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_BottomBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_BottomBorders(context[index]))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_BottomBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_BottomBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is RectangleElement rectangleElement1))
      return false;
    if (rectangleElement1.IsVirtualNode && rectangleElement1 is TableElement)
    {
      BorderLineTE bottomBorderLineTe = (rectangleElement1 as TableElement).BottomBorderLineTE;
      return this.CompareWithToolBarBorder(bottomBorderLineTe) && bottomBorderLineTe != null && bottomBorderLineTe.WidthTE.HasValue && bottomBorderLineTe.ColorTE.HasValue && bottomBorderLineTe.StyleTE.HasValue && bottomBorderLineTe.StyleTE.Value != BorderStyles.None;
    }
    BorderLine bottomBorderLine = rectangleElement1.BottomBorderLine;
    if (!this.CompareWithToolBarBorder(bottomBorderLine) || bottomBorderLine.Style == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is RectangleElement rectangleElement2) || !rectangleElement2.BottomBorderLine.Equals((object) bottomBorderLine))
        return false;
    }
    return true;
  }

  /// <summary>QueryStatus_BottomBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_BottomBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return false;
    if (context.NodesCount > 0)
      return this.QueryStatus_BottomBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return false;
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return rectangleElement1.Borders.Bottom.Style != 0;
    Collection<RectangleElement> bottomCells = rectangleElement1.GetBottomCells(true);
    bool flag = bottomCells.Count == 0;
    if (!flag)
    {
      foreach (RectangleElement rectangleElement2 in bottomCells)
      {
        if (rectangleElement2 != null && !rectangleElement2.ShowSelected)
        {
          flag = true;
          break;
        }
      }
    }
    return !flag || rectangleElement1.Borders.Bottom.Style != BorderStyles.None;
  }

  /// <summary>QueryStatus_HorizontalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool? QueryStatus_HorizontalBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return new bool?();
    bool? nullable1 = new bool?();
    bool? nullable2 = new bool?();
    for (int index = 0; index < context.Count; ++index)
    {
      bool? nullable3 = this.QueryStatus_HorizontalBorders(context[index]);
      bool? nullable4 = nullable3;
      bool flag1 = true;
      if (nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue)
      {
        nullable2 = new bool?(true);
      }
      else
      {
        bool? nullable5 = nullable3;
        bool flag2 = false;
        if (nullable5.GetValueOrDefault() == flag2 & nullable5.HasValue)
          return new bool?(false);
      }
    }
    return nullable2;
  }

  /// <summary>QueryStatus_HorizontalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_HorizontalBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is TableElement tableElement1))
      return false;
    BorderLineTE horizontalLineTe1 = tableElement1.InnerHorizontalLineTE;
    if (!this.CompareWithToolBarBorder(horizontalLineTe1) || horizontalLineTe1 == null || !horizontalLineTe1.WidthTE.HasValue || !horizontalLineTe1.ColorTE.HasValue || !horizontalLineTe1.StyleTE.HasValue || horizontalLineTe1.StyleTE.Value == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is TableElement tableElement2))
        return false;
      BorderLineTE horizontalLineTe2 = tableElement2.InnerHorizontalLineTE;
      if (horizontalLineTe2 != null)
      {
        float? widthTe1 = horizontalLineTe1.WidthTE;
        float? widthTe2 = horizontalLineTe2.WidthTE;
        if ((double) widthTe1.GetValueOrDefault() == (double) widthTe2.GetValueOrDefault() & widthTe1.HasValue == widthTe2.HasValue)
        {
          Color? colorTe1 = horizontalLineTe1.ColorTE;
          Color? colorTe2 = horizontalLineTe2.ColorTE;
          if ((colorTe1.HasValue == colorTe2.HasValue ? (colorTe1.HasValue ? (colorTe1.GetValueOrDefault() != colorTe2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          {
            BorderStyles? styleTe1 = horizontalLineTe1.StyleTE;
            BorderStyles? styleTe2 = horizontalLineTe2.StyleTE;
            if (styleTe1.GetValueOrDefault() == styleTe2.GetValueOrDefault() & styleTe1.HasValue == styleTe2.HasValue)
              continue;
          }
        }
      }
      return false;
    }
    return true;
  }

  /// <summary>QueryStatus_HorizontalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool? QueryStatus_HorizontalBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return new bool?();
    if (context.NodesCount > 0)
      return this.QueryStatus_HorizontalBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return new bool?();
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return new bool?();
    bool? nullable = new bool?();
    Collection<RectangleElement> topCells = rectangleElement1.GetTopCells(true);
    bool flag1 = topCells.Count == 0;
    if (!flag1)
    {
      foreach (RectangleElement rectangleElement2 in topCells)
      {
        if (rectangleElement2 != null && rectangleElement2.ShowSelected)
        {
          flag1 = true;
          nullable = new bool?(true);
          break;
        }
      }
    }
    if (flag1 && rectangleElement1.Borders.Top.Style == BorderStyles.None)
      return new bool?(false);
    Collection<RectangleElement> bottomCells = rectangleElement1.GetBottomCells(true);
    bool flag2 = bottomCells.Count == 0;
    if (!flag2)
    {
      foreach (RectangleElement rectangleElement3 in bottomCells)
      {
        if (rectangleElement3 != null && rectangleElement3.ShowSelected)
        {
          flag2 = true;
          nullable = new bool?(true);
          break;
        }
      }
    }
    return flag2 && rectangleElement1.Borders.Bottom.Style == BorderStyles.None ? new bool?(false) : nullable;
  }

  /// <summary>QueryStatus_VerticalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool? QueryStatus_VerticalBorders(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return new bool?();
    bool? nullable1 = new bool?();
    bool? nullable2 = new bool?();
    for (int index = 0; index < context.Count; ++index)
    {
      bool? nullable3 = this.QueryStatus_VerticalBorders(context[index]);
      bool? nullable4 = nullable3;
      bool flag1 = true;
      if (nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue)
      {
        nullable2 = new bool?(true);
      }
      else
      {
        bool? nullable5 = nullable3;
        bool flag2 = false;
        if (nullable5.GetValueOrDefault() == flag2 & nullable5.HasValue)
          return new bool?(false);
      }
    }
    return nullable2;
  }

  /// <summary>QueryStatus_VerticalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_VerticalBorders(List<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0 || !(context[0] is TableElement tableElement1))
      return false;
    BorderLineTE innerVerticalLineTe1 = tableElement1.InnerVerticalLineTE;
    if (!this.CompareWithToolBarBorder(innerVerticalLineTe1) || innerVerticalLineTe1 == null || !innerVerticalLineTe1.WidthTE.HasValue || !innerVerticalLineTe1.ColorTE.HasValue || !innerVerticalLineTe1.StyleTE.HasValue || innerVerticalLineTe1.StyleTE.Value == BorderStyles.None)
      return false;
    for (int index = 1; index < context.Count; ++index)
    {
      if (!(context[index] is TableElement tableElement2))
        return false;
      BorderLineTE innerVerticalLineTe2 = tableElement2.InnerVerticalLineTE;
      if (innerVerticalLineTe2 != null)
      {
        float? widthTe1 = innerVerticalLineTe1.WidthTE;
        float? widthTe2 = innerVerticalLineTe2.WidthTE;
        if ((double) widthTe1.GetValueOrDefault() == (double) widthTe2.GetValueOrDefault() & widthTe1.HasValue == widthTe2.HasValue)
        {
          Color? colorTe1 = innerVerticalLineTe1.ColorTE;
          Color? colorTe2 = innerVerticalLineTe2.ColorTE;
          if ((colorTe1.HasValue == colorTe2.HasValue ? (colorTe1.HasValue ? (colorTe1.GetValueOrDefault() != colorTe2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          {
            BorderStyles? styleTe1 = innerVerticalLineTe1.StyleTE;
            BorderStyles? styleTe2 = innerVerticalLineTe2.StyleTE;
            if (styleTe1.GetValueOrDefault() == styleTe2.GetValueOrDefault() & styleTe1.HasValue == styleTe2.HasValue)
              continue;
          }
        }
      }
      return false;
    }
    return true;
  }

  /// <summary>QueryStatus_VerticalBorders</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool? QueryStatus_VerticalBorders(DocumentTreeNode context)
  {
    if (context == null || context is Intermech.Document.Model.Page)
      return new bool?();
    if (context.NodesCount > 0)
      return this.QueryStatus_VerticalBorders((IList<DocumentTreeNode>) context.Nodes);
    if (!(context is RectangleElement))
      return new bool?();
    RectangleElement rectangleElement1 = context as RectangleElement;
    TableData topLevelTable = rectangleElement1.TopLevelTable;
    if (topLevelTable == null || topLevelTable == context)
      return new bool?();
    bool? nullable = new bool?();
    Collection<RectangleElement> leftCells = rectangleElement1.GetLeftCells(true);
    bool flag1 = leftCells.Count == 0;
    if (!flag1)
    {
      foreach (RectangleElement rectangleElement2 in leftCells)
      {
        if (rectangleElement2 != null && rectangleElement2.ShowSelected)
        {
          flag1 = true;
          nullable = new bool?(true);
          break;
        }
      }
    }
    if (flag1 && rectangleElement1.Borders.Left.Style == BorderStyles.None)
      return new bool?(false);
    Collection<RectangleElement> rightCells = rectangleElement1.GetRightCells(true);
    bool flag2 = rightCells.Count == 0;
    if (!flag2)
    {
      foreach (RectangleElement rectangleElement3 in rightCells)
      {
        if (rectangleElement3 != null && rectangleElement3.ShowSelected)
        {
          flag2 = true;
          nullable = new bool?(true);
          break;
        }
      }
    }
    return flag2 && rectangleElement1.Borders.Right.Style == BorderStyles.None ? new bool?(false) : nullable;
  }

  private bool QueryStatus_ListInParagraph(IList<DocumentTreeNode> context, bool isBullet)
  {
    if (this._queryTern == null || this._queryFirstLineSelection <= -1)
      return false;
    for (int firstLineSelection = this._queryFirstLineSelection; firstLineSelection <= this._queryEndLineSelection; ++firstLineSelection)
    {
      bool IsBullet;
      if (this._queryTern.TerGetBulletInfo(0, firstLineSelection, out IsBullet, out int _, out int _, out int _, out int _) <= 0 || IsBullet != isBullet)
        return false;
    }
    return true;
  }

  /// <summary>Увеличение отступа</summary>
  /// <param name="increase"></param>
  private void IncreaseIndent(bool increase)
  {
    if (this.DocumentControl == null)
      return;
    this.DocumentControl.GetActiveEditorControl()?.ParaLeftIndent(increase, true);
  }

  /// <summary>Обновить меню и инструменты форматирования</summary>
  public virtual void UpdateFormatCommands()
  {
    this.BeginQuery();
    try
    {
      for (int index = 0; index < FormatCommandsList.Commands.Count; ++index)
      {
        ICommandState command = this.CommandManager.FindCommand(FormatCommandsList.Commands[index]);
        if (command != null)
          this.CommandManager.QueryStatus(command);
      }
    }
    finally
    {
      this.EndQuery();
    }
  }

  /// <summary>Обновить меню и инструменты навигации</summary>
  public virtual void UpdateNavigationCommands()
  {
    if (this.DocumentControl.InvokeRequired || this.InvokeRequired)
      return;
    if (this.DocumentControl.LockedForHandler > 0)
      --this.DocumentControl.LockedForHandler;
    if (this.DocumentControl.LockForClosing)
      return;
    this.BeginQuery();
    try
    {
      for (int index = 0; index < NavigationCommandsList.Commands.Count; ++index)
      {
        ICommandState command = this.CommandManager.FindCommand(NavigationCommandsList.Commands[index]);
        if (command != null)
          this.CommandManager.QueryStatus(command);
      }
    }
    finally
    {
      this.EndQuery();
    }
  }

  /// <summary>Обновить меню и инструменты границ</summary>
  public virtual void UpdateBorberCommands()
  {
    this.BeginQuery();
    try
    {
      for (int index = 0; index < BordersCommandsList.Commands.Count; ++index)
      {
        ICommandState command = this.CommandManager.FindCommand(BordersCommandsList.Commands[index]);
        if (command != null)
          this.CommandManager.QueryStatus(command);
      }
    }
    finally
    {
      this.EndQuery();
    }
  }

  /// <summary> Вызывается, когда размер шрифта был изменён посредством выбора нового в выпадающем списке на тулбаре </summary>
  public void TextSizeChanged(float size)
  {
    try
    {
      if (this.IsFontChangeLocked() || this.MenuHelper.CbFontSize == null || this.MenuHelper.CbFontSize.ComboBox.Text == string.Empty)
        return;
      this._IsQueryChacheIsInit = false;
      this.InitQueryCache();
      this.SaveCharFormat((IList<DocumentTreeNode>) this._queryStatusContext, new CharFormat(true)
      {
        FontSize = new float?(size)
      }, true);
      this.InitQueryCache();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void SetFontSize(IList<DocumentTreeNode> context, float size, bool update)
  {
    if (context == null || context.Count == 0)
      return;
    pageElementNode = (PageElementNode) null;
    int fromPage = -1;
    for (int index1 = 0; index1 < context.Count; ++index1)
    {
      if (update && context[index1] is PageElementNode pageElementNode)
      {
        int index2 = pageElementNode.Index;
        if (fromPage == -1 || index2 < fromPage)
          fromPage = index2;
      }
      this.SetFontSize(context[index1], size, false);
    }
    if (!update || fromPage == -1 || pageElementNode == null || pageElementNode.OwnerDocument == null)
      return;
    pageElementNode.OwnerDocument.UpdateLayout(fromPage, false, true);
  }

  private void SetFontSize(DocumentTreeNode context, float size, bool update)
  {
    if (context == null)
      return;
    if (context is TextData textData)
    {
      CharFormat charFormat1 = textData.CharFormat;
      if (charFormat1 == null || !charFormat1.FontSize.HasValue || (double) charFormat1.FontSize.Value != (double) size)
      {
        CharFormat charFormat2 = charFormat1 != null ? charFormat1.Clone() : new CharFormat();
        charFormat2.FontSize = new float?(size);
        textData.SetCharFormat(charFormat2, false, false);
      }
    }
    if (context.Nodes != null)
    {
      this.SetFontSize((IList<DocumentTreeNode>) context.Nodes, size, update);
    }
    else
    {
      if (!(textData != null & update))
        return;
      textData.UpdateLayout(true);
    }
  }

  /// <summary> Вызывается, когда шрифт был изменён посредством выбора нового в выпадающем списке на тулбаре </summary>
  public void FontFamilyCBSelectedIndexChanged()
  {
    try
    {
      if (this.IsFontChangeLocked() || this.MenuHelper.ChooseFontComboBoxToolbarItem == null || this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndex == -1 || this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.Text == string.Empty)
        return;
      this._IsQueryChacheIsInit = false;
      this.InitQueryCache();
      this.SaveCharFormat((IList<DocumentTreeNode>) this._queryStatusContext, new CharFormat(true)
      {
        FontFamily = this.MenuHelper.ChooseFontComboBoxToolbarItem.ComboBox.Text
      }, true);
      this.InitQueryCache();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void SetCellFontFamily(
    IList<DocumentTreeNode> context,
    string newFontFamily,
    bool update)
  {
    if (context == null || context.Count == 0)
      return;
    pageElementNode = (PageElementNode) null;
    int fromPage = -1;
    for (int index1 = 0; index1 < context.Count; ++index1)
    {
      if (update && context[index1] is PageElementNode pageElementNode)
      {
        int index2 = pageElementNode.Index;
        if (fromPage == -1 || index2 < fromPage)
          fromPage = index2;
      }
      this.SetCellFontFamily(context[index1], newFontFamily, false);
    }
    if (!update || fromPage == -1 || pageElementNode == null || pageElementNode.OwnerDocument == null)
      return;
    pageElementNode.OwnerDocument.UpdateLayout(fromPage, false, true);
  }

  private void SetCellFontFamily(DocumentTreeNode context, string newFontFamily, bool update)
  {
    if (context == null)
      return;
    if (context is TextData textData)
    {
      CharFormat charFormat1 = textData.CharFormat;
      if (charFormat1 == null || charFormat1.FontFamily != newFontFamily)
      {
        CharFormat charFormat2 = charFormat1 != null ? charFormat1.Clone() : new CharFormat();
        charFormat2.FontFamily = newFontFamily;
        textData.SetCharFormat(charFormat2, false, false);
      }
    }
    if (context.Nodes != null)
    {
      this.SetCellFontFamily((IList<DocumentTreeNode>) context.Nodes, newFontFamily, update);
    }
    else
    {
      if (!(textData != null & update))
        return;
      textData.UpdateLayout(true);
    }
  }

  public virtual List<UndoItem> GetUndoItems()
  {
    List<UndoItem> undoItems = new List<UndoItem>();
    foreach (IUndoAction action in this.UndoManager.Actions)
    {
      if (action.Caption != "")
        undoItems.Insert(0, new UndoItem(action.Caption, (object) action));
    }
    return undoItems;
  }

  public virtual List<UndoItem> GetRedoItems()
  {
    List<UndoItem> redoItems = new List<UndoItem>();
    foreach (IUndoAction redoAction in this.UndoManager.RedoActions)
    {
      if (redoAction.Caption != "")
        redoItems.Insert(0, new UndoItem(redoAction.Caption, (object) redoAction));
    }
    return redoItems;
  }

  public virtual bool Undo(UndoItem item)
  {
    if (!(item.Tag is IUndoAction tag))
      return false;
    if (this.UndoManager.Actions.Contains(tag))
    {
      while (this.UndoManager.Actions.Contains(tag))
        this.UndoManager.DoUndo();
    }
    return true;
  }

  public virtual bool Redo(UndoItem item)
  {
    if (!(item.Tag is IUndoAction tag))
      return false;
    if (this.UndoManager.RedoActions.Contains(tag))
    {
      while (this.UndoManager.RedoActions.Contains(tag))
        this.UndoManager.DoRedo();
    }
    return true;
  }

  [Flags]
  private enum borderType
  {
    OuterLeft = 1,
    OuterTop = 2,
    OuterRight = 4,
    OuterBottom = 8,
    InnerHorizontal = 16, // 0x00000010
    InnerVertical = 32, // 0x00000020
  }

  [Flags]
  private enum EmfToWmfBitsFlags
  {
    EmfToWmfBitsFlagsDefault = 0,
    EmfToWmfBitsFlagsEmbedEmf = 1,
    EmfToWmfBitsFlagsIncludePlaceable = 2,
    EmfToWmfBitsFlagsNoXORClip = 4,
  }

  public delegate BorderLine GetToolbarBorder_EventHandler();
}
