// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.IntegratorsPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class IntegratorsPage : UserControl, IPageControl, IDisposable
{
  private static readonly string DefaultExtension = "xml";
  private static readonly string DefaultFilterExtension = "XML-конфигурации интеграторов (*.xml)|*.xml";
  private IIntegratorRegistry integrators;
  private IntegratorSettingsCacheManager integratorSettingsCacheManager;
  private IPagerControl pager;
  private ToolSecurityContext securityContext;
  private IntegratorsPageEvents editorEvents;
  private const string DefaultIntegratorImage = "Integrator";
  private IContainer components;
  private PictureBox pbDescription;
  private Label lbDescription;
  private Button btCreate;
  private Button btRemove;
  private ListView lvIntegrators;
  private ColumnHeader chDisplayName;
  private Button btProperties;
  private ImageList ilIntegrators;
  private Label lbIntegrators;
  private Button buttonImport;
  private Button buttonExport;

  public IntegratorsPage()
  {
    this.InitializeComponent();
    if (!this.DesignMode)
    {
      this.integrators = ClientContext.Integrators;
      this.integratorSettingsCacheManager = ServiceUtils.GetService<IntegratorSettingsCacheManager>((object) ApplicationServices.Container, true);
    }
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1634);
  }

  public void Initialize(IPagerControl pagerControl)
  {
    this.pager = pagerControl;
    this.securityContext = new ToolSecurityContext();
    this.ReloadServerCache();
    this.InitIntegratorImages();
    this.ShowIntegrators();
    this.ConfigurePageButtons();
    this.InitPageEvents();
  }

  public bool CanClose => true;

  public void Close()
  {
    this.DeletePageEvents();
    this.lvIntegrators.Items.Clear();
    this.securityContext = (ToolSecurityContext) null;
    this.pager = (IPagerControl) null;
  }

  public event EventHandler DynamicContentChanged;

  private void ReloadServerCache()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).ReloadCache();
  }

  private void InitIntegratorImages()
  {
    foreach (IIntegrator integrator in this.integrators.GetIntegrators())
    {
      Image applicationImage = integrator.GetApplicationImage(AppImageSize.Image32x32);
      if (applicationImage != null)
        this.ilIntegrators.Images.Add(this.MakeImageKey(integrator), applicationImage);
    }
  }

  private void ShowIntegrators()
  {
    this.lvIntegrators.BeginUpdate();
    try
    {
      List<IntegratorObject> integratorObjectList = new List<IntegratorObject>(8);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, false);
        if (service != null)
          integratorObjectList.AddRange((IEnumerable<IntegratorObject>) service.GetIntegrators());
      }
      foreach (IntegratorObject integratorObject in integratorObjectList)
        this.lvIntegrators.Items.Add(this.MakeItem(integratorObject));
      if (this.lvIntegrators.Items.Count > 0)
        this.lvIntegrators.Items[0].Selected = true;
      this.lvIntegrators.Focus();
    }
    finally
    {
      this.lvIntegrators.EndUpdate();
    }
  }

  private ListViewItem MakeItem(IntegratorObject integratorObject)
  {
    ListViewItem listViewItem = new ListViewItem();
    listViewItem.Text = integratorObject.DisplayName;
    listViewItem.Tag = (object) integratorObject;
    IIntegrator integrator = this.integrators.GetIntegrator(integratorObject, false);
    if (integrator != null)
    {
      string key = this.MakeImageKey(integrator);
      listViewItem.ImageKey = this.ilIntegrators.Images.ContainsKey(key) ? key : "Integrator";
    }
    else
      listViewItem.ImageKey = "Integrator";
    return listViewItem;
  }

  private ListViewItem FindItem(Guid integratorId)
  {
    foreach (ListViewItem listViewItem in this.lvIntegrators.Items)
    {
      if (((IntegratorObject) listViewItem.Tag).Id == integratorId)
        return listViewItem;
    }
    return (ListViewItem) null;
  }

  private string MakeImageKey(IIntegrator integrator) => integrator.Id.ToString();

  private void liIntegrators_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ConfigureIntegratorButtons(this.lvIntegrators.SelectedItems.Count != 0);
  }

  private void OnIntegratorDoubleClick(object sender, EventArgs e)
  {
    if (this.lvIntegrators.SelectedItems.Count == 0)
      return;
    this.btProperties.PerformClick();
  }

  private void ButtonExport_Click(object sender, EventArgs e)
  {
    if (this.lvIntegrators.SelectedItems.Count == 0 || !(this.lvIntegrators.SelectedItems[0].Tag is IntegratorObject tag))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string integratorData = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).GetIntegratorData(tag.Id);
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.AddExtension = true;
      saveFileDialog1.DefaultExt = IntegratorsPage.DefaultExtension;
      saveFileDialog1.FileName = tag.DisplayName;
      saveFileDialog1.Filter = IntegratorsPage.DefaultFilterExtension;
      saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      saveFileDialog1.Title = "Сохранение XML-конфигурации интегратора в файл";
      saveFileDialog1.RestoreDirectory = true;
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      File.WriteAllText(saveFileDialog2.FileName, integratorData, Encoding.UTF8);
    }
  }

  private void ButtonImport_Click(object sender, EventArgs e)
  {
    if (this.lvIntegrators.SelectedItems.Count == 0 || !(this.lvIntegrators.SelectedItems[0].Tag is IntegratorObject tag))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Filter = IntegratorsPage.DefaultFilterExtension;
      openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      openFileDialog1.Title = "Выбор файла XML-конфигурации интегратора";
      openFileDialog1.RestoreDirectory = true;
      OpenFileDialog openFileDialog2 = openFileDialog1;
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      string xmlData = File.ReadAllText(openFileDialog2.FileName, Encoding.UTF8);
      service.SetIntegratorData(tag.Id, xmlData);
      this.editorEvents.FireUpdated(tag);
    }
  }

  private void ConfigurePageButtons()
  {
    this.btCreate.Enabled = this.securityContext.CanEditPublicSettings;
  }

  private void ConfigureIntegratorButtons(bool integratorSelected)
  {
    bool flag = this.securityContext.CanEditPublicSettings & integratorSelected;
    this.btRemove.Enabled = flag;
    this.btProperties.Enabled = flag;
    this.buttonImport.Enabled = flag;
    this.buttonExport.Enabled = flag;
  }

  private void InitPageEvents()
  {
    this.editorEvents = new IntegratorsPageEvents();
    this.editorEvents.Updated += new EventHandler<IntegratorArgs>(this.OnIntegratorUpdated);
    this.editorEvents.Removed += new EventHandler<IntegratorArgs>(this.OnIntegratorRemoved);
  }

  private void DeletePageEvents() => this.editorEvents = (IntegratorsPageEvents) null;

  private void OnIntegratorUpdated(object sender, IntegratorArgs e)
  {
    ListViewItem listViewItem = this.FindItem(e.IntegratorObject.Id);
    if (listViewItem == null)
      return;
    this.integratorSettingsCacheManager.ResetCache();
    listViewItem.SubItems[0].Text = e.IntegratorObject.DisplayName;
    listViewItem.Tag = (object) e.IntegratorObject;
    this.lvIntegrators.Sort();
  }

  private void OnIntegratorRemoved(object sender, IntegratorArgs e)
  {
    ListViewItem listViewItem = this.FindItem(e.IntegratorObject.Id);
    if (listViewItem == null)
      return;
    int index = listViewItem.Selected ? listViewItem.Index : -1;
    this.lvIntegrators.Items.RemoveAt(listViewItem.Index);
    if (index < 0 || this.lvIntegrators.Items.Count <= 0)
      return;
    if (index == this.lvIntegrators.Items.Count)
      --index;
    this.lvIntegrators.Items[index].Selected = true;
  }

  private void OnCreateButton(object sender, EventArgs e)
  {
    List<IIntegrator> integrators = this.integrators.GetIntegrators();
    List<IntegratorsPage.IntegratorTemplate> integratorTemplateList = new List<IntegratorsPage.IntegratorTemplate>(integrators.Count);
    foreach (IIntegrator integrator in integrators)
    {
      if (this.FindItem(integrator.Id) == null)
        integratorTemplateList.Add(new IntegratorsPage.IntegratorTemplate(integrator.Id, integrator.DisplayName, integrator.GetServerObjectTemplate()));
    }
    if (integratorTemplateList.Count > 0)
    {
      integratorTemplateList.Sort((Comparison<IntegratorsPage.IntegratorTemplate>) ((x, y) => StringComparer.CurrentCultureIgnoreCase.Compare(x.IntegratorName, y.IntegratorName)));
      SelectItemForm currentControl = new SelectItemForm();
      currentControl.Items = (IEnumerable) integratorTemplateList;
      currentControl.Text = LocalizationHolder.rm.GetString("Tools.Client_159");
      currentControl.Description = LocalizationHolder.rm.GetString("Tools.Client_211");
      HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1635);
      if (currentControl.ShowDialog() != DialogResult.OK)
        return;
      IntegratorsPage.IntegratorTemplate selectedItem = (IntegratorsPage.IntegratorTemplate) currentControl.SelectedItem;
      IntegratorObject integrator;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        integrator = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).CreateIntegrator(selectedItem.IntegratorId, selectedItem.ServerObjectXml);
      this.integratorSettingsCacheManager.ResetCache();
      ListViewItem listViewItem = this.MakeItem(integrator);
      this.lvIntegrators.Items.Add(listViewItem);
      listViewItem.Selected = true;
      this.lvIntegrators.Focus();
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_160"), LocalizationHolder.rm.GetString("Tools.Client_159"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void OnRemoveButton(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_161"), LocalizationHolder.rm.GetString("Tools.Client_162"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    IntegratorObject tag = (IntegratorObject) this.lvIntegrators.SelectedItems[0].Tag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).RemoveIntegrator(tag.Id);
    this.integratorSettingsCacheManager.ResetCache();
    this.editorEvents.FireRemoved(tag);
    this.lvIntegrators.Focus();
  }

  private void OnPropertiesButton(object sender, EventArgs e)
  {
    IntegratorObject tag = (IntegratorObject) this.lvIntegrators.SelectedItems[0].Tag;
    IntegratorObject newObject = (IntegratorObject) null;
    Form form = new Form();
    form.Text = string.Format(LocalizationHolder.rm.GetString("Tools.Client_212"), (object) tag.DisplayName);
    form.StartPosition = FormStartPosition.CenterParent;
    form.Size = new Size(850, 550);
    form.MinimumSize = form.Size;
    form.MinimizeBox = false;
    form.MaximizeBox = false;
    form.Padding = new Padding(4);
    IntegratorDataPage dataPage = new IntegratorDataPage();
    dataPage.Parent = (Control) form;
    dataPage.Dock = DockStyle.Fill;
    dataPage.InfoUpdated += (EventHandler) ((x, y) =>
    {
      newObject = dataPage.SelectedIntegrator;
      form.Text = string.Format(LocalizationHolder.rm.GetString("Tools.Client_212"), (object) newObject.DisplayName);
    });
    dataPage.PageClose += (EventHandler) ((x, y) => form.Close());
    dataPage.InitializePage(tag, !this.securityContext.CanEditPublicSettings);
    form.ActiveControl = (Control) dataPage;
    int num = (int) form.ShowDialog();
    if (newObject != null)
      this.editorEvents.FireUpdated(newObject);
    this.lvIntegrators.Focus();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntegratorsPage));
    this.pbDescription = new PictureBox();
    this.lbDescription = new Label();
    this.btCreate = new Button();
    this.btRemove = new Button();
    this.btProperties = new Button();
    this.lvIntegrators = new ListView();
    this.chDisplayName = new ColumnHeader();
    this.ilIntegrators = new ImageList(this.components);
    this.lbIntegrators = new Label();
    this.buttonImport = new Button();
    this.buttonExport = new Button();
    ((ISupportInitialize) this.pbDescription).BeginInit();
    this.SuspendLayout();
    this.pbDescription.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.pbDescription, "pbDescription");
    this.pbDescription.Name = "pbDescription";
    this.pbDescription.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.btCreate, "btCreate");
    this.btCreate.Name = "btCreate";
    this.btCreate.UseVisualStyleBackColor = true;
    this.btCreate.Click += new EventHandler(this.OnCreateButton);
    componentResourceManager.ApplyResources((object) this.btRemove, "btRemove");
    this.btRemove.Name = "btRemove";
    this.btRemove.UseVisualStyleBackColor = true;
    this.btRemove.Click += new EventHandler(this.OnRemoveButton);
    componentResourceManager.ApplyResources((object) this.btProperties, "btProperties");
    this.btProperties.Name = "btProperties";
    this.btProperties.UseVisualStyleBackColor = true;
    this.btProperties.Click += new EventHandler(this.OnPropertiesButton);
    componentResourceManager.ApplyResources((object) this.lvIntegrators, "lvIntegrators");
    this.lvIntegrators.Columns.AddRange(new ColumnHeader[1]
    {
      this.chDisplayName
    });
    this.lvIntegrators.FullRowSelect = true;
    this.lvIntegrators.GridLines = true;
    this.lvIntegrators.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvIntegrators.HideSelection = false;
    this.lvIntegrators.LargeImageList = this.ilIntegrators;
    this.lvIntegrators.MultiSelect = false;
    this.lvIntegrators.Name = "lvIntegrators";
    this.lvIntegrators.ShowItemToolTips = true;
    this.lvIntegrators.Sorting = SortOrder.Ascending;
    this.lvIntegrators.UseCompatibleStateImageBehavior = false;
    this.lvIntegrators.SelectedIndexChanged += new EventHandler(this.liIntegrators_SelectedIndexChanged);
    this.lvIntegrators.DoubleClick += new EventHandler(this.OnIntegratorDoubleClick);
    componentResourceManager.ApplyResources((object) this.chDisplayName, "chDisplayName");
    this.ilIntegrators.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilIntegrators.ImageStream");
    this.ilIntegrators.TransparentColor = Color.Transparent;
    this.ilIntegrators.Images.SetKeyName(0, "Integrator");
    componentResourceManager.ApplyResources((object) this.lbIntegrators, "lbIntegrators");
    this.lbIntegrators.Name = "lbIntegrators";
    componentResourceManager.ApplyResources((object) this.buttonImport, "buttonImport");
    this.buttonImport.Name = "buttonImport";
    this.buttonImport.UseVisualStyleBackColor = true;
    this.buttonImport.Click += new EventHandler(this.ButtonImport_Click);
    componentResourceManager.ApplyResources((object) this.buttonExport, "buttonExport");
    this.buttonExport.Name = "buttonExport";
    this.buttonExport.UseVisualStyleBackColor = true;
    this.buttonExport.Click += new EventHandler(this.ButtonExport_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.buttonExport);
    this.Controls.Add((Control) this.buttonImport);
    this.Controls.Add((Control) this.lbIntegrators);
    this.Controls.Add((Control) this.btProperties);
    this.Controls.Add((Control) this.lvIntegrators);
    this.Controls.Add((Control) this.btRemove);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.btCreate);
    this.Controls.Add((Control) this.pbDescription);
    this.Name = nameof (IntegratorsPage);
    ((ISupportInitialize) this.pbDescription).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class IntegratorTemplate
  {
    private Guid integratorId;
    private string integratorName;
    private string serverObjectXml;

    public IntegratorTemplate(Guid integratorId, string integratorName, string serverObjectXml)
    {
      this.integratorId = integratorId;
      this.integratorName = integratorName;
      this.serverObjectXml = serverObjectXml;
    }

    public Guid IntegratorId => this.integratorId;

    public string IntegratorName => this.integratorName;

    public string ServerObjectXml => this.serverObjectXml;

    public override string ToString() => this.IntegratorName;
  }
}
