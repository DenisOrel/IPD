
// Type: IMClient.UI.Winforms.SelectPluginsForm




using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;


namespace IMClient.UI.Winforms
{
    public class SelectPluginsForm : Form
    {
      private bool _isInited;
      private bool _allDllsPageIsActive;
      private bool _isOkBtn;
      private IContainer components;
      private RadPageView radPageView1;
      private RadPageViewPage allDllFilesPage;
      private RadPageViewPage loadModulesPage;
      private Windows8Theme windows8Theme1;
      private RadGroupBox radGroupBox1;
      private ListView loadModulesList;
      private ListView allDllList;
      private Button cancelBtn;
      private Button okBtn;
      private ColumnHeader columnName;
      private ColumnHeader columnPath;
      private ColumnHeader columnNameDlls;
      private ColumnHeader columnPathDlls;

      public SelectPluginsForm() => this.InitializeComponent();

      private void SelectPluginsForm_Load(object sender, EventArgs e)
      {
        if (!this._isInited)
          this.InitForm();
        Dictionary<string, int> dictionary = new Dictionary<string, int>()
        {
          {
            "ColumnNameWidth",
            this.columnName.Width
          },
          {
            "ColumnPathWidth",
            this.columnPath.Width
          },
          {
            "ColumnNameDllsWidth",
            this.columnNameDlls.Width
          },
          {
            "ColumnPathDllsWidth",
            this.columnPathDlls.Width
          }
        };
        FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
        this.columnName.Width = dictionary["ColumnNameWidth"];
        this.columnPath.Width = dictionary["ColumnPathWidth"];
        this.columnNameDlls.Width = dictionary["ColumnNameDllsWidth"];
        this.columnPathDlls.Width = dictionary["ColumnPathDllsWidth"];
      }

      public void InitForm()
      {
        string[] array = ((IEnumerable<string>) Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)).Where<string>((System.Func<string, bool>) (x => !Path.GetFileName(x).StartsWith("Interop.", StringComparison.InvariantCultureIgnoreCase) && !Path.GetFileName(x).StartsWith("Microsoft.", StringComparison.InvariantCultureIgnoreCase) && !Path.GetFileName(x).StartsWith("System.", StringComparison.InvariantCultureIgnoreCase))).ToArray<string>();
        if (array.Length == 0)
        {
          this.allDllFilesPage.Visible = false;
        }
        else
        {
          foreach (string path in array)
            this.allDllList.Items.Add(new ListViewItem(Path.GetFileName(path))
            {
              SubItems = {
                path
              }
            });
        }
        DataTable dataTable = (DataTable) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad0005b-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) "cad00047-306c-11d8-b4e9-00304f19f545")),
            new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) "cad00127-306c-11d8-b4e9-00304f19f545"))
          }));
        if (dataTable != null)
        {
          HashSet<ListViewItem> source = new HashSet<ListViewItem>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            string text = row.ItemArray[0].ToString();
            string str = row.ItemArray[1].ToString();
            source.Add(new ListViewItem(text)
            {
              SubItems = {
                str
              }
            });
          }
          if (source.Count == 0)
            this.loadModulesPage.Visible = false;
          else
            this.loadModulesList.Items.AddRange(source.ToArray<ListViewItem>());
        }
        this._isInited = true;
      }

      public HashSet<string> SelectedLoadModules
      {
        get
        {
          HashSet<string> selectedLoadModules = new HashSet<string>();
          if (this._isOkBtn && !this._allDllsPageIsActive)
          {
            foreach (ListViewItem listViewItem in this.loadModulesList.CheckedItems.OfType<ListViewItem>())
            {
              if (listViewItem.SubItems.Count == 2)
                selectedLoadModules.Add(listViewItem.SubItems[1].Text);
            }
          }
          return selectedLoadModules;
        }
      }

      public HashSet<string> SelectedDlls
      {
        get
        {
          HashSet<string> selectedDlls = new HashSet<string>();
          if (this._isOkBtn && this._allDllsPageIsActive)
          {
            foreach (ListViewItem listViewItem in this.allDllList.CheckedItems.OfType<ListViewItem>())
            {
              if (listViewItem.SubItems.Count == 2)
                selectedDlls.Add(listViewItem.SubItems[1].Text);
            }
          }
          return selectedDlls;
        }
      }

      private void SelectPluginsForm_FormClosing(object sender, FormClosingEventArgs e)
      {
        this._allDllsPageIsActive = this.allDllFilesPage.Visible;
        this._isOkBtn = this.DialogResult == DialogResult.OK;
        FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, int>()
        {
          {
            "ColumnNameWidth",
            this.columnName.Width
          },
          {
            "ColumnPathWidth",
            this.columnPath.Width
          },
          {
            "ColumnNameDllsWidth",
            this.columnNameDlls.Width
          },
          {
            "ColumnPathDllsWidth",
            this.columnPathDlls.Width
          }
        });
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.radPageView1 = new RadPageView();
        this.loadModulesPage = new RadPageViewPage();
        this.loadModulesList = new ListView();
        this.columnName = new ColumnHeader();
        this.columnPath = new ColumnHeader();
        this.allDllFilesPage = new RadPageViewPage();
        this.allDllList = new ListView();
        this.columnNameDlls = new ColumnHeader();
        this.columnPathDlls = new ColumnHeader();
        this.windows8Theme1 = new Windows8Theme();
        this.radGroupBox1 = new RadGroupBox();
        this.cancelBtn = new Button();
        this.okBtn = new Button();
        this.radPageView1.BeginInit();
        this.radPageView1.SuspendLayout();
        this.loadModulesPage.SuspendLayout();
        this.allDllFilesPage.SuspendLayout();
        this.radGroupBox1.BeginInit();
        this.radGroupBox1.SuspendLayout();
        this.SuspendLayout();
        this.radPageView1.Controls.Add((Control) this.loadModulesPage);
        this.radPageView1.Controls.Add((Control) this.allDllFilesPage);
        this.radPageView1.DefaultPage = this.loadModulesPage;
        this.radPageView1.Dock = DockStyle.Fill;
        this.radPageView1.Location = new Point(0, 0);
        this.radPageView1.Margin = new Padding(4, 4, 4, 4);
        this.radPageView1.Name = "radPageView1";
        this.radPageView1.SelectedPage = this.allDllFilesPage;
        this.radPageView1.Size = new Size(1017, 525);
        this.radPageView1.TabIndex = 0;
        this.radPageView1.ThemeName = "Windows8";
        this.radPageView1.ViewMode = PageViewMode.Backstage;
        ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).ItemAlignment = StripViewItemAlignment.Near;
        ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).ItemFitMode = StripViewItemFitMode.FillHeight;
        ((RadPageViewElement) this.radPageView1.GetChildAt(0)).ItemDragMode = PageViewItemDragMode.None;
        ((RadPageViewElement) this.radPageView1.GetChildAt(0)).ItemSizeMode = PageViewItemSizeMode.EqualHeight;
        ((RadPageViewElement) this.radPageView1.GetChildAt(0)).ItemContentOrientation = PageViewContentOrientation.Horizontal;
        this.radPageView1.GetChildAt(0).GetChildAt(0).MinSize = new Size(40, 0);
        this.loadModulesPage.Controls.Add((Control) this.loadModulesList);
        this.loadModulesPage.ItemSize = new SizeF(232f, 35f);
        this.loadModulesPage.Location = new Point(245, 6);
        this.loadModulesPage.Margin = new Padding(4, 4, 4, 4);
        this.loadModulesPage.Name = "loadModulesPage";
        this.loadModulesPage.Size = new Size(766, 513);
        this.loadModulesPage.Text = "Загружаемые модули IPS";
        this.loadModulesList.CheckBoxes = true;
        this.loadModulesList.Columns.AddRange(new ColumnHeader[2]
        {
          this.columnName,
          this.columnPath
        });
        this.loadModulesList.Dock = DockStyle.Fill;
        this.loadModulesList.FullRowSelect = true;
        this.loadModulesList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.loadModulesList.HideSelection = false;
        this.loadModulesList.Location = new Point(0, 0);
        this.loadModulesList.Margin = new Padding(4, 4, 4, 4);
        this.loadModulesList.Name = "loadModulesList";
        this.loadModulesList.Size = new Size(766, 513);
        this.loadModulesList.Sorting = SortOrder.Ascending;
        this.loadModulesList.TabIndex = 0;
        this.loadModulesList.UseCompatibleStateImageBehavior = false;
        this.loadModulesList.View = View.Details;
        this.columnName.Text = "Имя";
        this.columnName.Width = 150;
        this.columnPath.Text = "Путь";
        this.columnPath.Width = 300;
        this.allDllFilesPage.AutoSize = true;
        this.allDllFilesPage.Controls.Add((Control) this.allDllList);
        this.allDllFilesPage.ItemSize = new SizeF(232f, 35f);
        this.allDllFilesPage.Location = new Point(245, 6);
        this.allDllFilesPage.Margin = new Padding(4, 4, 4, 4);
        this.allDllFilesPage.Name = "allDllFilesPage";
        this.allDllFilesPage.Size = new Size(766, 513);
        this.allDllFilesPage.Text = "Сборки на локальном диске";
        this.allDllList.CheckBoxes = true;
        this.allDllList.Columns.AddRange(new ColumnHeader[2]
        {
          this.columnNameDlls,
          this.columnPathDlls
        });
        this.allDllList.Dock = DockStyle.Fill;
        this.allDllList.FullRowSelect = true;
        this.allDllList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.allDllList.HideSelection = false;
        this.allDllList.Location = new Point(0, 0);
        this.allDllList.Margin = new Padding(4, 4, 4, 4);
        this.allDllList.Name = "allDllList";
        this.allDllList.Size = new Size(766, 513);
        this.allDllList.Sorting = SortOrder.Ascending;
        this.allDllList.TabIndex = 0;
        this.allDllList.UseCompatibleStateImageBehavior = false;
        this.allDllList.View = View.Details;
        this.columnNameDlls.Text = "Имя";
        this.columnNameDlls.Width = 150;
        this.columnPathDlls.Text = "Путь";
        this.columnPathDlls.Width = 300;
        this.radGroupBox1.AccessibleRole = AccessibleRole.Grouping;
        this.radGroupBox1.Controls.Add((Control) this.cancelBtn);
        this.radGroupBox1.Controls.Add((Control) this.okBtn);
        this.radGroupBox1.Dock = DockStyle.Bottom;
        this.radGroupBox1.HeaderText = "";
        this.radGroupBox1.Location = new Point(0, 525);
        this.radGroupBox1.Margin = new Padding(4, 4, 4, 4);
        this.radGroupBox1.Name = "radGroupBox1";
        this.radGroupBox1.Padding = new Padding(3, 22, 3, 2);
        this.radGroupBox1.Size = new Size(1017, 55);
        this.radGroupBox1.TabIndex = 1;
        this.radGroupBox1.ThemeName = "Windows8";
        this.cancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.cancelBtn.DialogResult = DialogResult.Cancel;
        this.cancelBtn.Location = new Point(881, 14);
        this.cancelBtn.Margin = new Padding(4, 4, 4, 4);
        this.cancelBtn.Name = "cancelBtn";
        this.cancelBtn.Size = new Size(120, 28);
        this.cancelBtn.TabIndex = 0;
        this.cancelBtn.Text = "Отмена";
        this.cancelBtn.UseVisualStyleBackColor = true;
        this.okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.okBtn.DialogResult = DialogResult.OK;
        this.okBtn.Location = new Point(753, 14);
        this.okBtn.Margin = new Padding(4, 4, 4, 4);
        this.okBtn.Name = "okBtn";
        this.okBtn.Size = new Size(120, 28);
        this.okBtn.TabIndex = 0;
        this.okBtn.Text = "ОК";
        this.okBtn.UseVisualStyleBackColor = true;
        this.AcceptButton = (IButtonControl) this.okBtn;
        this.AutoScaleDimensions = new SizeF(8f, 16f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.cancelBtn;
        this.ClientSize = new Size(1017, 580);
        this.Controls.Add((Control) this.radPageView1);
        this.Controls.Add((Control) this.radGroupBox1);
        this.Margin = new Padding(4, 4, 4, 4);
        this.MinimumSize = new Size(847, 580);
        this.Name = nameof (SelectPluginsForm);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Менеджер загрузки модулей расширений IPS";
        this.FormClosing += new FormClosingEventHandler(this.SelectPluginsForm_FormClosing);
        this.Load += new EventHandler(this.SelectPluginsForm_Load);
        this.radPageView1.EndInit();
        this.radPageView1.ResumeLayout(false);
        this.radPageView1.PerformLayout();
        this.loadModulesPage.ResumeLayout(false);
        this.allDllFilesPage.ResumeLayout(false);
        this.radGroupBox1.EndInit();
        this.radGroupBox1.ResumeLayout(false);
        this.ResumeLayout(false);
      }
    }
}
