
// Type: IMClient.ToolbarControls.ProjectsDropDownControl




using Intermech;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;


namespace IMClient.ToolbarControls
{
    internal sealed class ProjectsDropDownControl : ObjectsDropDownControl
    {
      private MainForm _mainForm;

      public ProjectsDropDownControl(
        MainForm mainForm,
        DropDownMenuItem menu,
        Image image,
        long selectedItem)
        : base(menu, ObjectsDropDownOptions.Default, LocalizationHolder.rm.GetString("IMClient_85"), image, new MyObjectElement(0L, LocalizationHolder.rm.GetString("IMClient_62"), (object) Guid.Empty, MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545")), (IList<long>) null, (IList<int>) new int[1]
        {
          MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545")
        }, selectedItem)
      {
        this._mainForm = mainForm;
        this.PrepareControls();
        this.ReloadProjects();
      }

      protected override void UpdateControls()
      {
        base.UpdateControls();
        if (this._mainForm == null)
          return;
        if (this.menu.Tag is MyObjectElement tag1 && (long) tag1.Value != 0L)
        {
          foreach (MenuItemBase menuItemBase in (CollectionBase) this._mainForm._buttonProjectFilterMode.Items)
          {
            if (menuItemBase is MenuButtonItem menuButtonItem)
              menuButtonItem.Enabled = true;
          }
        }
        else
        {
          foreach (MenuItemBase menuItemBase in (CollectionBase) this._mainForm._buttonProjectFilterMode.Items)
          {
            if (menuItemBase is MenuButtonItem menuButtonItem)
            {
              ProjectFiltrationModes tag = (ProjectFiltrationModes) menuButtonItem.Tag;
              menuButtonItem.Enabled = tag == ProjectFiltrationModes.None || tag == ProjectFiltrationModes.UserProjects;
            }
          }
        }
        this._mainForm._buttonProjectRefresh.Enabled = false;
        this._mainForm._buttonProjectRefresh.Visible = false;
        this._mainForm._buttonProjectRefresh.Locked = true;
        this.CorrectEditorMode();
      }

      protected override void OnGroupItemClick(object sender, EventArgs e)
      {
        base.OnGroupItemClick(sender, e);
        try
        {
          this.userAndRole.SetCurrentProject(this.groupItem.ObjectID, ProjectFiltrationModes.None);
        }
        finally
        {
          this.SelectCurrentProject();
          this.UpdateControls();
        }
      }

      protected override void OnItemClick(object sender, EventArgs e)
      {
        base.OnItemClick(sender, e);
        try
        {
          this.userAndRole.SetCurrentProject(this.SelectedItem, this.userAndRole.CachedProjectFiltrationMode);
        }
        finally
        {
          this.SelectCurrentProject();
          this.UpdateControls();
        }
      }

      protected override void NotificationEventFired(object sender, NotificationEventArgs e)
      {
        if (e.EventName == "ProjectChanged")
          this.ReloadProjects();
        else
          base.NotificationEventFired(sender, e);
      }

      private void MainFormButtonProjectRefresh_Click(object sender, EventArgs e)
      {
        this.ReloadProjects();
      }

      private void PrepareControls()
      {
        this._mainForm._buttonProjectRefresh.ImageIndex = this.namedImageList.ImageIndex("imgRefresh");
        this._mainForm._buttonProjectFilterMode.ImageIndex = this.namedImageList.ImageIndex("imgProjectFilter");
        int num1 = this.namedImageList.ImageIndex("imgFilterByCurrentProject");
        int num2 = this.namedImageList.ImageIndex("imgFilterByUserProjects");
        this._mainForm._buttonProjectFilterMode.Items.Clear();
        this._mainForm._buttonProjectFilterMode.Tag = (object) ProjectFiltrationModes.None;
        this._mainForm._buttonProjectFilterMode.ToolTipText = EnumDescConverter.GetEnumDescription((Enum) ProjectFiltrationModes.None);
        for (int index = 0; index < Enum.GetValues(typeof (ProjectFiltrationModes)).Length; ++index)
        {
          ProjectFiltrationModes projectFiltrationModes = (ProjectFiltrationModes) Enum.GetValues(typeof (ProjectFiltrationModes)).GetValue(index);
          string enumDescription = EnumDescConverter.GetEnumDescription((Enum) projectFiltrationModes);
          int imageIndex = this._mainForm._buttonProjectFilterMode.ImageIndex;
          if (projectFiltrationModes == ProjectFiltrationModes.CurrentProject || projectFiltrationModes == ProjectFiltrationModes.OnlyCurrentProject)
            imageIndex = num1;
          if (projectFiltrationModes == ProjectFiltrationModes.UserProjects)
            imageIndex = num2;
          MenuButtonItem menuButtonItem = new MenuButtonItem(enumDescription, new EventHandler(this.ProjectFilterOn), imageIndex);
          menuButtonItem.Tag = Enum.GetValues(typeof (ProjectFiltrationModes)).GetValue(index);
          this._mainForm._buttonProjectFilterMode.Items.Add((ToolbarItemBase) menuButtonItem);
        }
        this._mainForm.sbpProject.Text = string.Empty;
        this._mainForm.sbpProject.ToolTipText = LocalizationHolder.rm.GetString("IMClient_61");
        this.LoadProjectsList();
        this.FillDropDownMenu();
        this.FillAccessLevel();
        this.UpdateControls();
        this._mainForm._buttonProjectRefresh.Click += new EventHandler(this.MainFormButtonProjectRefresh_Click);
      }

      private void FillAccessLevel()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          string empty = string.Empty;
          int AttrID = 0;
          FieldTypes AttrType = FieldTypes.ftUnknown;
          bool IsSystemType = false;
          bool IsAttrList = false;
          ArrayList AttrPossibleValues = (ArrayList) null;
          MyAttributeHelper.GetAttrInfo("cad00816-306c-11d8-b4e9-00304f19f545", ref empty, ref AttrID, ref AttrType, ref IsSystemType, ref IsAttrList, ref AttrPossibleValues);
          int securityLevel = sessionKeeper.Session.SecurityLevel;
          MyElement myElement1 = (MyElement) null;
          for (int index = 0; index < AttrPossibleValues.Count; ++index)
          {
            if (AttrPossibleValues[index] is MyElement myElement2 && Convert.ToInt64(myElement2.Value).Equals((long) securityLevel))
            {
              myElement1 = myElement2;
              break;
            }
          }
          this._mainForm.spbLevel.ToolTipText = string.Format(LocalizationHolder.rm.GetString("IMClient_76"));
          if (myElement1 != null)
            this._mainForm.spbLevel.Text = $"{myElement1.Caption}";
          else
            this._mainForm.spbLevel.Text = string.Format(LocalizationHolder.rm.GetString("IMClient_77"), (object) securityLevel);
        }
      }

      private void ReloadProjects()
      {
        this.LoadProjectsList();
        this.FillAccessLevel();
        this.FillDropDownMenu();
        this.UpdateControls();
        this.SelectCurrentProject();
      }

      private void SelectCurrentProject()
      {
        try
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            this.SelectedItem = sessionKeeper.Session.CurrentProjectID;
        }
        catch
        {
        }
      }

      private void CorrectEditorMode()
      {
        for (int index = 0; index < this._mainForm._buttonProjectFilterMode.Items.Count; ++index)
          this._mainForm._buttonProjectFilterMode.Items[index].Checked = false;
        ProjectFiltrationModes projectFiltrationMode = this.userAndRole.CachedProjectFiltrationMode;
        for (int index = 0; index < this._mainForm._buttonProjectFilterMode.Items.Count; ++index)
        {
          this._mainForm._buttonProjectFilterMode.Items[index].Checked = (ProjectFiltrationModes) this._mainForm._buttonProjectFilterMode.Items[index].Tag == projectFiltrationMode;
          if (this._mainForm._buttonProjectFilterMode.Items[index].Checked)
          {
            this._mainForm._buttonProjectFilterMode.Tag = this._mainForm._buttonProjectFilterMode.Items[index].Tag;
            this._mainForm._buttonProjectFilterMode.ToolTipText = this._mainForm._buttonProjectFilterMode.Items[index].Text;
            this._mainForm._buttonProjectFilterMode.ImageIndex = this._mainForm._buttonProjectFilterMode.Items[index].ImageIndex;
          }
        }
      }

      private void ProjectFilterOn(object sender, EventArgs e)
      {
        MenuButtonItem menuButtonItem = sender as MenuButtonItem;
        long cachedProjectId = this.userAndRole.CachedProjectID;
        try
        {
          this.userAndRole.SetCurrentProject(cachedProjectId, (ProjectFiltrationModes) menuButtonItem.Tag);
        }
        finally
        {
          this.SelectCurrentProject();
          this.UpdateControls();
        }
      }

      private void LoadProjectsList()
      {
        CacheManager.Cache("ProjectNamesCache")?.Reset();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ProjectFiltrationModes projectFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
          try
          {
            this.items.Clear();
            sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.UserProjects;
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(new Guid("cad00812-306c-11d8-b4e9-00304f19f545"));
            ColumnDescriptor[] columns = new ColumnDescriptor[4]
            {
              new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
              new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0),
              new ColumnDescriptor((object) -12, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
              new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
            };
            object[] objArray = new object[0];
            SortOrders[] sortOrdersArray = new SortOrders[0];
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-9, RelationalOperators.NotIn, (object) new int[2]
              {
                sessionKeeper.Session.IdentHelper.KeepingLevelID,
                sessionKeeper.Session.IdentHelper.DeletedID
              }, LogicalOperators.NONE, 0, false)
            }, columns);
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
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                long int64Value = DataSetProcessor.GetInt64Value(row[0], 0L);
                string stringValue = DataSetProcessor.GetStringValue(row[1], string.Empty);
                Guid guidValue = DataSetProcessor.GetGuidValue(row[2], Guid.Empty);
                int int32Value = DataSetProcessor.GetInt32Value(row[3], -1);
                if (int64Value != 0L && !(guidValue == Guid.Empty))
                {
                  MyObjectElement myObjectElement = new MyObjectElement(int64Value, stringValue, (object) guidValue, int32Value);
                  if (Math.Abs(this.SelectedItem) == Math.Abs(int64Value))
                    this.items.Insert(0, myObjectElement);
                  else
                    this.items.Add(myObjectElement);
                }
              }
            }
            dataTable?.Dispose();
          }
          finally
          {
            sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationMode;
          }
        }
      }
    }
}
