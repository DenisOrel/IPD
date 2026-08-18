
// Type: IMClient.CreateObjTypesMenuMRU




using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;


namespace IMClient
{
    internal class CreateObjTypesMenuMRU : ICreateObjectButton
    {
      internal MainForm Owner;
      internal INamedImageList _namedImageList;

      public CreateObjTypesMenuMRU(MainForm owner)
      {
        this.Owner = owner;
        this.PrepareControls();
        this.UpdateControls();
        if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
          return;
        service.OpenObjectAfterCreationEvent += new AfterObjectCreatedEventHandler(this.NewObjectCreated);
      }

      internal void CreateNewObject(object sender, EventArgs e)
      {
        ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
        if (!(sender is MenuButtonItem menuButtonItem) || !((sender as MenuButtonItem).Tag is IMRUItem tag) || tag.Value == null || tag.Tag == null || !tag.Tag.Equals((object) 0))
          return;
        this.Owner.btNewItem.Tag = (object) menuButtonItem;
        this.Owner.btNewItem.ToolTipText = string.Format(LocalizationHolder.rm.GetString("IMClient_58"), (object) tag.Caption);
        if (service.IndexOf(4, (int) tag.Value) >= 0)
          this.BtnNewImage = service.ImageList.Images[service.IndexOf(4, (int) tag.Value)];
        else
          this.BtnNewIcon = (Icon) null;
        ObjectCommands.CreateCommand((int) tag.Value);
      }

      internal virtual void UpdateControls()
      {
      }

      internal virtual void PrepareControls()
      {
        this._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
        this.FillCreateMenu();
      }

      private void FillCreateMenu()
      {
        MenuButtonItem createMenuButtonItem = this.Owner._createMenuButtonItem;
        MenuButtonItem newMenuButtonItem = this.Owner._createNewMenuButtonItem;
        ICreateObjByTypeMRU service1 = ServicesManager.GetService(typeof (ICreateObjByTypeMRU)) as ICreateObjByTypeMRU;
        ICategoryTypeIconService service2 = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
        List<MenuButtonItem> menuButtonItemList = new List<MenuButtonItem>();
        for (int index = 0; index < createMenuButtonItem.Items.Count; ++index)
        {
          MenuButtonItem menuButtonItem = createMenuButtonItem.Items[index];
          if (menuButtonItem.Tag is IMRUItem)
            menuButtonItemList.Add(menuButtonItem);
        }
        for (int index = 0; index < menuButtonItemList.Count; ++index)
          createMenuButtonItem.Items.Remove((ToolbarItemBase) menuButtonItemList[index]);
        if (service1 == null || service2 == null)
          return;
        for (int index1 = 0; index1 < service1.Count; ++index1)
        {
          IMRUItem mruItem = service1[index1];
          MenuButtonItem menuButtonItem = new MenuButtonItem(mruItem.Caption, new EventHandler(this.CreateNewObject), -1);
          menuButtonItem.BeginGroup = index1 == 0;
          menuButtonItem.Tag = (object) mruItem;
          int index2 = service2.IndexOf(4, (int) mruItem.Value);
          if (index2 >= 0)
            menuButtonItem.Image = service2.ImageList.Images[index2];
          createMenuButtonItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }

      public void NewObjectCreated(object sender, AfterObjectCreatedEventArgs ea)
      {
        if (!ea.RunEditor)
          return;
        try
        {
          long[] objectIDs = new long[1]{ ea.ObjectID };
          ServiceContainer serviceContainer = new ServiceContainer();
          serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.None));
          CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ObjectExtensions.GetItems(objectIDs, (IServiceProvider) serviceContainer), (IServiceProvider) serviceContainer);
          if (!commandsTable.Contains("EditDocument"))
            return;
          Intermech.Navigator.ContextMenu.Services.InvokeCommand("EditDocument", commandsTable, (IServiceProvider) serviceContainer);
        }
        catch
        {
          if (ea.ObjectID != -1L)
          {
            DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", ea.ObjectID);
            if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
              service.FireEvent((object) null, (NotificationEventArgs) e);
          }
          throw;
        }
      }

      public void BtnNewObjTypeIcon(int objTypeID, IMRUItem MRUItem)
      {
        ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
        if (service.IndexOf(4, objTypeID) >= 0)
          this.BtnNewImage = service.ImageList.Images[service.IndexOf(4, objTypeID)];
        else
          this.ResetIcon();
        this.Owner.btNewItem.Tag = (object) MRUItem;
        this.Owner.btNewItem.ToolTipText = string.Format(LocalizationHolder.rm.GetString("IMClient_59"), (object) MRUItem.Caption);
      }

      public int BtnNewImageIndex
      {
        get => this.Owner.btNewItem.ImageIndex;
        set => this.Owner.btNewItem.ImageIndex = value;
      }

      public Image BtnNewImage
      {
        get => this.Owner.btNewItem.Image;
        set => this.Owner.btNewItem.Image = value;
      }

      public Icon BtnNewIcon
      {
        get => this.Owner.btNewItem.Icon;
        set => this.Owner.btNewItem.Icon = value;
      }

      public void ResetIcon()
      {
        INamedImageList service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
        this.Owner.btNewItem.Icon = (Icon) null;
        this.Owner.btNewItem.Image = (Image) null;
        this.Owner.btNewItem.ImageIndex = service.ImageIndex("imgNewItem");
        this.Owner.btNewItem.Tag = (object) null;
        this.Owner.btNewItem.ToolTipText = LocalizationHolder.rm.GetString("IMClient_60");
      }
    }
}
