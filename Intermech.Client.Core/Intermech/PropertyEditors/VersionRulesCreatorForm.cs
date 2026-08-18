
// Type: Intermech.PropertyEditors.VersionRulesCreatorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма по созданию новых правил подбора версий</summary>
public class VersionRulesCreatorForm : Form
{
  /// <summary>Текущая страница (0 или 1)</summary>
  private int FPageIndex;
  /// <summary>Максимально допустимый индекс странички</summary>
  private const int FMaxPageIndex = 1;
  /// <summary>ID вновь созданного объекта</summary>
  private long FNewObjectID;
  /// <summary>ID типа создаваемого объекта</summary>
  private int FNewObjectTypeID;
  /// <summary>ID объекта-шаблона</summary>
  private long FTemplateObjectID;
  /// <summary>Название объекта-шаблона</summary>
  private string FTemplateObjectName = "";
  /// <summary>Название типа создаваемого объекта</summary>
  private string FNewObjTypeName = "";
  /// <summary>
  /// Встроенная форма для редактирования правила отбора версий
  /// </summary>
  private VersionRulesEditorForm RuleEditor;
  private IContainer components;
  private Panel panelPage1;
  private Panel panelPage2;
  private Panel panelBottom;
  private Label lbPromt;
  private TextBox edCaption;
  private Label lbTemplate;
  private TextBox edTemplate;
  private Button btnBrowseTemplate;
  private Button btnNext;
  private Button btnBack;
  private Button btnCancel;
  private Button btnClearTemplate;
  private ErrorProvider errorProvider;
  private PictureBox pictureBox1;
  private ToolTip toolTip;

  public VersionRulesCreatorForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1235);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.edCaption.MaxLength = Intermech.Consts.MaxStringSize;
    this.RuleEditor = new VersionRulesEditorForm();
    this.RuleEditor.HideApplyCancel = true;
    this.RuleEditor.SetParent((Control) this.panelPage2);
    this.RuleEditor.ParentMode = 1;
    this.UpdateTemplate();
    this.UpdateControls();
  }

  /// <summary>Почистить за собой мусор</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls()
  {
    if (this.errorProvider.GetError((Control) this.edCaption).Length > 0)
    {
      this.lbPromt.Text = VersionRulesCreatorForm.RulesCreatorConsts.Label2;
      this.lbPromt.ForeColor = Color.Red;
    }
    else
    {
      this.lbPromt.Text = VersionRulesCreatorForm.RulesCreatorConsts.Label1;
      this.lbPromt.ForeColor = SystemColors.ControlText;
    }
    this.Text = string.Format(VersionRulesCreatorForm.RulesCreatorConsts.Caption, (object) this.FNewObjTypeName);
    if (this.FTemplateObjectID == 0L && this.FTemplateObjectName.Length > 0)
    {
      this.FTemplateObjectName = "";
      if (!this.RuleEditor.IsChanged)
      {
        this.RuleEditor.RuleClass.Clear();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.RuleEditor.RuleClass.Valid(sessionKeeper.Session);
        this.RuleEditor.BuildRuleNodes();
      }
    }
    this.edTemplate.Text = this.FTemplateObjectName;
    Size clientSize;
    if (this.FPageIndex == 0 && !this.panelPage1.Visible)
    {
      this.panelPage1.Left = 0;
      this.panelPage1.Top = 0;
      Panel panelPage1_1 = this.panelPage1;
      clientSize = this.panelPage1.Parent.ClientSize;
      int width = clientSize.Width;
      panelPage1_1.Width = width;
      Panel panelPage1_2 = this.panelPage1;
      clientSize = this.panelPage1.Parent.ClientSize;
      int num = clientSize.Height - this.panelBottom.Height;
      panelPage1_2.Height = num;
    }
    if (this.FPageIndex == 1 && !this.panelPage2.Visible)
    {
      this.panelPage2.Left = 0;
      this.panelPage2.Top = 0;
      Panel panelPage2_1 = this.panelPage2;
      clientSize = this.panelPage2.Parent.ClientSize;
      int width = clientSize.Width;
      panelPage2_1.Width = width;
      Panel panelPage2_2 = this.panelPage2;
      clientSize = this.panelPage2.Parent.ClientSize;
      int num = clientSize.Height - this.panelBottom.Height;
      panelPage2_2.Height = num;
    }
    this.panelPage1.Visible = this.FPageIndex == 0;
    this.panelPage2.Visible = this.FPageIndex == 1;
    if (this.FPageIndex == 1)
      this.RuleEditor.UpdateControls();
    this.btnNext.Enabled = this.edCaption.Text.Length > 0;
    this.btnBack.Enabled = this.FPageIndex > 0;
    switch (this.FPageIndex)
    {
      case 0:
        this.btnNext.Text = VersionRulesCreatorForm.RulesCreatorConsts.OK1;
        break;
      case 1:
        this.btnNext.Text = VersionRulesCreatorForm.RulesCreatorConsts.OK2;
        break;
    }
  }

  /// <summary>Обновить контролы</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoUpdateControls(object sender, EventArgs e)
  {
    this.errorProvider.SetError((Control) this.edCaption, string.Empty);
    this.UpdateControls();
  }

  public void UpdateTemplate()
  {
    if (this.FTemplateObjectID == 0L)
      return;
    try
    {
      this.Cursor = Cursors.WaitCursor;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService rulesCacheService;
        try
        {
          rulesCacheService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        }
        catch
        {
          rulesCacheService = (IVersionRulesCacheService) null;
        }
        VersionsRule versionsRule = rulesCacheService[(object) sessionKeeper.Session.SessionGUID, this.FTemplateObjectID];
        if (versionsRule == null)
          return;
        this.FTemplateObjectName = versionsRule.RuleObjectCaption;
        versionsRule.CurrentRuleType = VersionsRuleType.vrtStandardRule;
        this.edCaption.Text = this.FTemplateObjectName;
        if (this.FTemplateObjectID == 0L)
          return;
        this.RuleEditor.LoadTemplateData(this.RuleEditor.EditorMode, this.FTemplateObjectID);
      }
    }
    finally
    {
      this.Cursor = Cursors.Default;
    }
  }

  /// <summary>
  /// Вызвать форму как модальный диалог. При успехе создать новый объект и вернуть его ID
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <returns>0 при ошибке или отмене</returns>
  public static long Execute(int ObjectTypeID, long TemplateObjectID)
  {
    if (ObjectTypeID == 0)
      return 0;
    using (VersionRulesCreatorForm rulesCreatorForm = new VersionRulesCreatorForm())
    {
      rulesCreatorForm.FNewObjectID = 0L;
      rulesCreatorForm.FTemplateObjectID = TemplateObjectID;
      rulesCreatorForm.FNewObjectTypeID = ObjectTypeID;
      return rulesCreatorForm.ExecuteForm();
    }
  }

  /// <summary>
  /// Вызвать форму как модальный диалог. При успехе создать новый объект и вернуть его ID
  /// </summary>
  /// <returns>0 при ошибке или отмене</returns>
  private long ExecuteForm()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      try
      {
        this.FNewObjTypeName = session.GetObjectType(this.FNewObjectTypeID).ObjectTypeName;
      }
      catch
      {
        return 0;
      }
    }
    this.UpdateTemplate();
    this.UpdateControls();
    this.DialogResult = DialogResult.None;
    int num = (int) this.ShowDialog();
    return this.DialogResult != DialogResult.OK ? 0L : this.FNewObjectID;
  }

  /// <summary>
  /// Пробуем создать новый объект в базе согласно введённой пользователем информации
  /// </summary>
  public void TryToCreateObject()
  {
    if (this.edCaption.Text.Length <= 0)
    {
      this.FPageIndex = 0;
      this.UpdateControls();
      this.edCaption.Focus();
    }
    else
    {
      bool flag = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBObjectCollection objectCollection = session.GetObjectCollection(this.FNewObjectTypeID);
        if (objectCollection == null)
          return;
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) this.edCaption.Text, LogicalOperators.NONE, 0)
        };
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) session.IdentHelper.GetAttributeID("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, recordCount: 1);
        DataTable dataTable;
        try
        {
          dataTable = objectCollection.Select(paramSet);
        }
        catch
        {
          dataTable = (DataTable) null;
        }
        if (dataTable != null && dataTable.Rows.Count > 0)
          flag = dataTable.Rows[0][0].ToString() == this.edCaption.Text;
        if (!flag)
        {
          IDBObject RuleObject = objectCollection.Create(this.FNewObjectTypeID);
          IDBAttribute byGuid = RuleObject.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
          session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
          string text = this.edCaption.Text;
          byGuid.AsString = text;
          try
          {
            this.RuleEditor.RuleClass.SaveToObject(sessionKeeper.Session, RuleObject);
            RuleObject.CommitCreation(true);
            this.FNewObjectID = RuleObject.ObjectID;
            if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
              customService.LoadRule((object) sessionKeeper.Session.SessionGUID, this.FNewObjectID, this.RuleEditor.RuleClass.ActualDate);
          }
          catch
          {
            this.FNewObjectID = 0L;
          }
        }
      }
      if (flag)
      {
        this.FPageIndex = 0;
        this.UpdateControls();
        this.edCaption.Focus();
        this.errorProvider.SetError((Control) this.edCaption, VersionRulesCreatorForm.RulesCreatorConsts.Error1);
        this.UpdateControls();
      }
      else
        this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesCreatorForm));
    this.panelPage1 = new Panel();
    this.btnClearTemplate = new Button();
    this.btnBrowseTemplate = new Button();
    this.edTemplate = new TextBox();
    this.lbTemplate = new Label();
    this.pictureBox1 = new PictureBox();
    this.edCaption = new TextBox();
    this.lbPromt = new Label();
    this.panelPage2 = new Panel();
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnBack = new Button();
    this.btnNext = new Button();
    this.toolTip = new ToolTip(this.components);
    this.errorProvider = new ErrorProvider(this.components);
    this.panelPage1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelPage1, "panelPage1");
    this.panelPage1.Controls.Add((Control) this.btnClearTemplate);
    this.panelPage1.Controls.Add((Control) this.btnBrowseTemplate);
    this.panelPage1.Controls.Add((Control) this.edTemplate);
    this.panelPage1.Controls.Add((Control) this.lbTemplate);
    this.panelPage1.Controls.Add((Control) this.pictureBox1);
    this.panelPage1.Controls.Add((Control) this.edCaption);
    this.panelPage1.Controls.Add((Control) this.lbPromt);
    this.panelPage1.Name = "panelPage1";
    componentResourceManager.ApplyResources((object) this.btnClearTemplate, "btnClearTemplate");
    this.btnClearTemplate.Cursor = Cursors.Hand;
    this.btnClearTemplate.Name = "btnClearTemplate";
    this.toolTip.SetToolTip((Control) this.btnClearTemplate, componentResourceManager.GetString("btnClearTemplate.ToolTip"));
    this.btnClearTemplate.Click += new EventHandler(this.btnClearTemplate_Click);
    componentResourceManager.ApplyResources((object) this.btnBrowseTemplate, "btnBrowseTemplate");
    this.btnBrowseTemplate.Cursor = Cursors.Hand;
    this.btnBrowseTemplate.Name = "btnBrowseTemplate";
    this.toolTip.SetToolTip((Control) this.btnBrowseTemplate, componentResourceManager.GetString("btnBrowseTemplate.ToolTip"));
    this.btnBrowseTemplate.Click += new EventHandler(this.btnBrowseTemplate_Click);
    componentResourceManager.ApplyResources((object) this.edTemplate, "edTemplate");
    this.edTemplate.Name = "edTemplate";
    this.edTemplate.ReadOnly = true;
    this.toolTip.SetToolTip((Control) this.edTemplate, componentResourceManager.GetString("edTemplate.ToolTip"));
    componentResourceManager.ApplyResources((object) this.lbTemplate, "lbTemplate");
    this.lbTemplate.BackColor = Color.Transparent;
    this.lbTemplate.Name = "lbTemplate";
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.edCaption, "edCaption");
    this.edCaption.Name = "edCaption";
    this.toolTip.SetToolTip((Control) this.edCaption, componentResourceManager.GetString("edCaption.ToolTip"));
    this.edCaption.TextChanged += new EventHandler(this.DoUpdateControls);
    componentResourceManager.ApplyResources((object) this.lbPromt, "lbPromt");
    this.lbPromt.BackColor = Color.Transparent;
    this.lbPromt.ForeColor = SystemColors.ControlText;
    this.lbPromt.Name = "lbPromt";
    componentResourceManager.ApplyResources((object) this.panelPage2, "panelPage2");
    this.panelPage2.Name = "panelPage2";
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnBack);
    this.panelBottom.Controls.Add((Control) this.btnNext);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnBack, "btnBack");
    this.btnBack.Name = "btnBack";
    this.btnBack.Click += new EventHandler(this.Goto_PrevPage);
    componentResourceManager.ApplyResources((object) this.btnNext, "btnNext");
    this.btnNext.Name = "btnNext";
    this.btnNext.Click += new EventHandler(this.Goto_NextPage);
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.errorProvider, "errorProvider");
    this.AcceptButton = (IButtonControl) this.btnNext;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.panelPage1);
    this.Controls.Add((Control) this.panelPage2);
    this.Name = nameof (VersionRulesCreatorForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.VersionRulesCreatorForm_Load);
    this.Closed += new EventHandler(this.VersionRulesCreatorForm_Closed);
    this.panelPage1.ResumeLayout(false);
    this.panelPage1.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Сменить шаблон для создаваемого объекта</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnBrowseTemplate_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(VersionRulesCreatorForm.RulesCreatorConsts.Dialog1, VersionRulesCreatorForm.RulesCreatorConsts.Dialog2, ObjectTypesHelper.GetObjTypeID("cad001b3-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
    if (numArray == null)
      return;
    long num = numArray[0];
    if (num == this.FTemplateObjectID)
      return;
    this.FTemplateObjectID = num;
    this.Update();
    this.UpdateTemplate();
    this.UpdateControls();
  }

  /// <summary>Очистить шаблон для нового объекта</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btnClearTemplate_Click(object sender, EventArgs e)
  {
    this.FTemplateObjectID = 0L;
    this.Update();
    this.UpdateTemplate();
    this.UpdateControls();
  }

  /// <summary>Перейти к предыдущей страничке мастера</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void Goto_PrevPage(object sender, EventArgs e)
  {
    if (this.FPageIndex <= 0)
      return;
    --this.FPageIndex;
    this.UpdateControls();
  }

  /// <summary>Перейти к следующей страничке мастера</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void Goto_NextPage(object sender, EventArgs e)
  {
    if (this.FPageIndex >= 1)
    {
      this.TryToCreateObject();
    }
    else
    {
      ++this.FPageIndex;
      this.UpdateControls();
    }
  }

  private void VersionRulesCreatorForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void VersionRulesCreatorForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// Свалка констант для формы по созданию новых правил отбора версий
  /// </summary>
  public abstract class RulesCreatorConsts
  {
    public static readonly string OK1 = LocalizationHolder.rm.GetString("Client.Core_797");
    public static readonly string OK2 = LocalizationHolder.rm.GetString("Client.Core_218");
    public static readonly string Caption = LocalizationHolder.rm.GetString("Client.Core_798");
    public static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_799");
    public static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_800");
    public static readonly string Error1 = LocalizationHolder.rm.GetString("Client.Core_801");
    public static readonly string Label1 = LocalizationHolder.rm.GetString("Client.Core_802");
    public static readonly string Label2 = LocalizationHolder.rm.GetString("Client.Core_803");
  }
}
