
// Type: Intermech.Bars.BarManager
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Bars
{
    [ToolboxBitmap(typeof (BarManager))]
    [Designer(typeof (BarManagerDesigner))]
    public class BarManager : Component, IComparer
    {
      private ArrayList _toolbars;
      internal ArrayList _containers;
      private MenuBar _menuBar;
      private IToolBarRenderer _renderer;
      private bool _rendererNeedDispose;
      private Form _ownerForm;
      private bool _enableContextMenu;
      private Form _activeForm;
      internal GetToolbarCallback _getToolbarCallback;

      public BarManager()
      {
        this._ownerForm = (Form) null;
        this._enableContextMenu = true;
        this._toolbars = new ArrayList();
        this._containers = new ArrayList();
        this.Renderer = (IToolBarRenderer) new Office2003Renderer();
        this._rendererNeedDispose = true;
      }

      private void ActivateOnIdle() => Application.Idle += new EventHandler(this.Appication_Idle);

      private void ReadConfiguration(XmlTextReader reader)
      {
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element && !(reader.Name != "Toolbar"))
          {
            Guid guid1 = new Guid(reader.GetAttribute("Guid"));
            int num1 = int.Parse(reader.GetAttribute("DockLine"));
            int num2 = int.Parse(reader.GetAttribute("DockOffset"));
            bool flag1 = bool.Parse(reader.GetAttribute("Visible"));
            ToolBar toolBar = this.FindToolbar(guid1);
            if (toolBar == null && this._getToolbarCallback != null)
              toolBar = this._getToolbarCallback(guid1);
            if (toolBar != null)
            {
              toolBar.DockLine = num1;
              toolBar.DockOffset = num2;
              string str1 = reader.GetAttribute("DockMode") != null ? reader.GetAttribute("DockMode") : string.Empty;
              Guid guid2 = reader.GetAttribute("Container") != null ? new Guid(reader.GetAttribute("Container")) : Guid.Empty;
              ToolBarContainer container;
              switch (str1)
              {
                case "Floating":
                  int x = int.Parse(reader.GetAttribute("FloatingX"));
                  int y = int.Parse(reader.GetAttribute("FloatingY"));
                  if (x < 0)
                    x = 0;
                  if (y < 0)
                    y = 0;
                  int num3 = int.Parse(reader.GetAttribute("FloatingWidth"));
                  int num4 = int.Parse(reader.GetAttribute("FloatingHeight"));
                  toolBar.MakeFloating(this, new Point(x, y), true);
                  toolBar.Parent.Width = num3;
                  toolBar.Parent.Height = num4;
                  goto label_14;
                case "":
                  container = this.FindContainer(guid2);
                  break;
                default:
                  container = this.FindSuitableContainer((DockStyle) Enum.Parse(typeof (DockStyle), str1));
                  break;
              }
              toolBar.Redock((Control) container);
    label_14:
              toolBar.Visible = flag1;
              if (toolBar is ContainerBar)
              {
                ContainerBar containerBar = (ContainerBar) toolBar;
                string attribute1 = reader.GetAttribute("Width");
                int width = attribute1 == null ? containerBar.MinimumSize.Width : int.Parse(attribute1);
                string attribute2 = reader.GetAttribute("Height");
                int height = attribute2 == null ? containerBar.MinimumSize.Height : int.Parse(attribute2);
                containerBar.MinimumSize = new Size(width, height);
              }
              if (!reader.IsEmptyElement)
              {
                reader.Read();
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Items" && !reader.IsEmptyElement)
                {
                  while (true)
                  {
                    int index;
                    bool visible;
                    bool flag2;
                    do
                    {
                      do
                      {
                        if (!reader.Read() || reader.NodeType == XmlNodeType.EndElement && reader.Name == "Items")
                          goto label_25;
                      }
                      while (reader.NodeType != XmlNodeType.Element || !(reader.Name == "Item"));
                      index = int.Parse(reader.GetAttribute("Offset"));
                      visible = bool.Parse(reader.GetAttribute("Visible"));
                      string str2 = reader.GetAttribute("UserVisible");
                      if (string.IsNullOrEmpty(str2))
                        str2 = bool.TrueString;
                      flag2 = bool.Parse(str2);
                    }
                    while (index < 0 || index >= toolBar.Items.Count);
                    toolBar.Items[index].SetUserVisible(flag2, visible);
                  }
                }
                else
                  continue;
              }
              else
                continue;
            }
            else
              continue;
          }
          else
            continue;
    label_25:;
        }
      }

      internal void RemoveContainerBar(ToolBarContainer A_0)
      {
        if (!this._containers.Contains((object) A_0))
          return;
        this._containers.Remove((object) A_0);
      }

      private void ActiveForm_Deactivate(object A_0, EventArgs A_1)
      {
        this._activeForm.Deactivate -= new EventHandler(this.ActiveForm_Deactivate);
        this._activeForm = (Form) null;
        this.ActivateOnIdle();
      }

      private static void MargeItems(
        ToolbarItemBaseCollection fromItems,
        ToolbarItemBaseCollection toItems)
      {
        foreach (ToolbarItemBase fromItem in (CollectionBase) fromItems)
          fromItem._prevMergeIndex = fromItem.MergeIndex;
        int count = toItems.Count;
        for (int index1 = fromItems.Count - 1; index1 >= 0; --index1)
        {
          ToolbarItemBase fromItem = fromItems[index1];
          ToolbarItemBase toolbarItemBase = (ToolbarItemBase) null;
          if (fromItem.MergeAction != ItemMergeAction.Add)
            toolbarItemBase = fromItem.a(toItems);
          switch (fromItem.MergeAction)
          {
            case ItemMergeAction.Add:
              toItems.Insert(count, fromItem);
              break;
            case ItemMergeAction.Insert:
              if (toolbarItemBase != null)
              {
                int index2 = toItems.IndexOf(toolbarItemBase);
                toItems.Insert(index2, fromItem);
                for (int index3 = 0; index3 < fromItems.Count; ++index3)
                {
                  if (fromItems[index3]._prevMergeIndex > index2)
                    ++fromItems[index3]._prevMergeIndex;
                }
                break;
              }
              break;
            case ItemMergeAction.MergeChildren:
              if (toolbarItemBase != null && fromItem is MenuItemBase && toolbarItemBase is MenuItemBase)
              {
                BarManager.MargeItems((ToolbarItemBaseCollection) ((MenuItemBase) fromItem).Items, (ToolbarItemBaseCollection) ((MenuItemBase) toolbarItemBase).Items);
                break;
              }
              break;
            case ItemMergeAction.Remove:
              if (toolbarItemBase != null)
              {
                int num = toItems.IndexOf(toolbarItemBase);
                toItems.Remove(toolbarItemBase);
                for (int index4 = 0; index4 < fromItems.Count; ++index4)
                {
                  if (fromItems[index4]._prevMergeIndex > num)
                    --fromItems[index4]._prevMergeIndex;
                  else if (fromItems[index4]._prevMergeIndex == num)
                    fromItems[index4]._prevMergeIndex = -1;
                }
                break;
              }
              break;
            case ItemMergeAction.Replace:
              if (toolbarItemBase != null)
              {
                int index5 = toItems.IndexOf(toolbarItemBase);
                toItems.Remove(toolbarItemBase);
                toItems.Insert(index5, fromItem);
                break;
              }
              break;
          }
        }
      }

      public virtual List<ToolBar> GetToolbarsList()
      {
        List<ToolBar> toolbarsList = new List<ToolBar>();
        ArrayList arrayList = new ArrayList((ICollection) this._toolbars);
        arrayList.Sort((IComparer) this);
        foreach (ToolBar toolBar in arrayList)
        {
          if (toolBar.Closable)
            toolbarsList.Add(toolBar);
        }
        return toolbarsList;
      }

      internal void CustomizeToolbars(ToolBar toolbar, Control control, Point pos)
      {
        if (!this._enableContextMenu)
          return;
        MenuBarItem menuBarItem = new MenuBarItem();
        ArrayList toolbars = new ArrayList((ICollection) this._toolbars);
        this.OnCustomizeToolbars(toolbars);
        toolbars.Sort((IComparer) this);
        foreach (ToolBar toolbar1 in toolbars)
        {
          BarManager.MenuItemWithToolbar menuItemWithToolbar = new BarManager.MenuItemWithToolbar(toolbar1);
          menuItemWithToolbar.Text = toolbar1.Text;
          menuItemWithToolbar.Checked = toolbar1.IsOpen;
          menuItemWithToolbar.Enabled = toolbar1.Closable;
          menuBarItem.Items.Add((ToolbarItemBase) menuItemWithToolbar);
        }
        if (menuBarItem.HasChildren)
        {
          menuBarItem.SetToolBar(toolbar);
          BarManager.MenuItemWithToolbar menuItemWithToolbar = (BarManager.MenuItemWithToolbar) menuBarItem.Show(control, pos);
          menuBarItem.SetToolBar((ToolBar) null);
          if (menuItemWithToolbar != null)
          {
            if (menuItemWithToolbar.GetToolbar().IsOpen)
              menuItemWithToolbar.GetToolbar().Visible = false;
            else
              menuItemWithToolbar.GetToolbar().Visible = true;
          }
        }
        menuBarItem.Dispose();
      }

      private void OnCustomizeToolbars(ArrayList toolbars)
      {
        if (this.CollectToolbars == null)
          return;
        this.CollectToolbars((object) this, new CollectToolbarsEventArgs(toolbars));
      }

      private void WriteToolbar(ToolBar toolbar, XmlTextWriter writer, bool includeItemVisibility)
      {
        writer.WriteStartElement("Toolbar");
        writer.WriteAttributeString("Guid", toolbar.Guid.ToString());
        writer.WriteAttributeString("DockLine", toolbar.DockLine.ToString());
        writer.WriteAttributeString("DockOffset", toolbar.DockOffset.ToString());
        writer.WriteAttributeString("Visible", toolbar.IsOpen.ToString());
        int num;
        if (toolbar.Situation == ToolBarSituation.Floating)
        {
          writer.WriteAttributeString("DockMode", "Floating");
          writer.WriteAttributeString("FloatingX", toolbar.Parent.Left.ToString());
          XmlTextWriter xmlTextWriter1 = writer;
          num = toolbar.Parent.Top;
          string str1 = num.ToString();
          xmlTextWriter1.WriteAttributeString("FloatingY", str1);
          XmlTextWriter xmlTextWriter2 = writer;
          num = toolbar.Parent.Width;
          string str2 = num.ToString();
          xmlTextWriter2.WriteAttributeString("FloatingWidth", str2);
          XmlTextWriter xmlTextWriter3 = writer;
          num = toolbar.Parent.Height;
          string str3 = num.ToString();
          xmlTextWriter3.WriteAttributeString("FloatingHeight", str3);
        }
        else if (toolbar.Situation == ToolBarSituation.Contained)
          writer.WriteAttributeString("Container", ((ToolBarContainer) toolbar.Parent).Guid.ToString());
        if (toolbar is ContainerBar)
        {
          XmlTextWriter xmlTextWriter4 = writer;
          num = toolbar.MinimumSize.Width;
          string str4 = num.ToString();
          xmlTextWriter4.WriteAttributeString("Width", str4);
          XmlTextWriter xmlTextWriter5 = writer;
          num = toolbar.MinimumSize.Height;
          string str5 = num.ToString();
          xmlTextWriter5.WriteAttributeString("Height", str5);
        }
        if (includeItemVisibility && !(toolbar is MenuBar))
        {
          writer.WriteStartElement("Items");
          for (int index = 0; index < toolbar.Items.Count; ++index)
          {
            writer.WriteStartElement("Item");
            writer.WriteAttributeString("Offset", index.ToString());
            XmlTextWriter xmlTextWriter6 = writer;
            bool flag = toolbar.Items[index].IsVisible;
            string str6 = flag.ToString();
            xmlTextWriter6.WriteAttributeString("Visible", str6);
            XmlTextWriter xmlTextWriter7 = writer;
            flag = toolbar.Items[index].IsUserVisible;
            string str7 = flag.ToString();
            xmlTextWriter7.WriteAttributeString("UserVisible", str7);
            writer.WriteEndElement();
          }
          writer.WriteEndElement();
        }
        writer.WriteEndElement();
      }

      public void AddToolbar(ToolBar toolbar, DockStyle dockStyle)
      {
        if (toolbar == null)
          return;
        this.AddToolbar(toolbar);
        ToolBarContainer suitableContainer = this.FindSuitableContainer(dockStyle);
        if (suitableContainer == null)
          return;
        suitableContainer.Controls.Add((Control) toolbar);
        toolbar.DockLine = suitableContainer.GetNextFreeDockLine();
      }

      public void AddToolbar(ToolBar toolbar)
      {
        if (toolbar == null || this._toolbars.Contains((object) toolbar))
          return;
        if (toolbar is MenuBar && this._menuBar != null)
          throw new InvalidOperationException("Only one MenuBar should be added to each toolbar layout.");
        this._toolbars.Add((object) toolbar);
        if (!(toolbar is MenuBar))
          return;
        this._menuBar = (MenuBar) toolbar;
      }

      internal void OnRendererChanged()
      {
        foreach (ToolBarContainer container in this._containers)
          container.Repaint();
        foreach (ToolBar toolbar in this._toolbars)
        {
          if (toolbar.Situation == ToolBarSituation.Floating)
            toolbar.OnRendererChanged();
        }
        if (this.RendererChanged == null)
          return;
        this.RendererChanged((object) this, EventArgs.Empty);
      }

      internal void AddContainer(ToolBarContainer A_0)
      {
        if (this._containers.Contains((object) A_0))
          return;
        this._containers.Add((object) A_0);
      }

      private void Appication_Idle(object A_0, EventArgs A_1)
      {
        Application.Idle -= new EventHandler(this.Appication_Idle);
        bool formHasFocus = this.FormHasFocus;
        foreach (ToolBar toolbar in this._toolbars)
        {
          if (toolbar.Situation == ToolBarSituation.Floating)
          {
            if (formHasFocus)
              ((FloatingToolbarForm) toolbar.Parent).e();
            else
              ((FloatingToolbarForm) toolbar.Parent).f();
          }
        }
        Form activeForm = Form.ActiveForm;
        if (this.OwnerForm == null || activeForm == null || activeForm == this.OwnerForm || this.OwnerForm.IsMdiChild)
          return;
        if (this._activeForm != null)
          this._activeForm.Deactivate -= new EventHandler(this.ActiveForm_Deactivate);
        this._activeForm = activeForm;
        this._activeForm.Deactivate += new EventHandler(this.ActiveForm_Deactivate);
      }

      private void OwnerForm_Deactivate(object A_0, EventArgs A_1)
      {
        foreach (ToolBarContainer container in this._containers)
          container.OnOwnerFormDeactivate();
        this.ActivateOnIdle();
      }

      private void OwnerForm_Activated(object A_0, EventArgs A_1)
      {
        foreach (ToolBarContainer container in this._containers)
          container.OnOwnerFormActivated();
        this.ActivateOnIdle();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          ToolBarContainer[] toolBarContainerArray = new ToolBarContainer[this._containers.Count];
          this._containers.CopyTo((Array) toolBarContainerArray);
          foreach (Component component in toolBarContainerArray)
            component.Dispose();
          this._renderer.RedrawRequired -= new EventHandler(this.Renderer_RedrawRequired);
          if (this._rendererNeedDispose)
          {
            this._renderer.Dispose();
            this._rendererNeedDispose = false;
          }
          this._renderer = (IToolBarRenderer) null;
        }
        base.Dispose(disposing);
      }

      private void Renderer_RedrawRequired(object A_0, EventArgs A_1) => this.OnRendererChanged();

      public static void ExitMenuLoop() => MenuLooper.ExitMenuLoop();

      public ToolBarContainer FindContainer(Guid guid)
      {
        foreach (ToolBarContainer container in this._containers)
        {
          if (container.Guid == guid)
            return container;
        }
        return (ToolBarContainer) null;
      }

      public ToolBarContainer FindSuitableContainer(DockStyle dockStyle)
      {
        foreach (ToolBarContainer container in this._containers)
        {
          if (container.Dock == dockStyle)
            return container;
        }
        return (ToolBarContainer) null;
      }

      public ToolBar FindToolbar(Guid guid)
      {
        foreach (ToolBar toolbar in this._toolbars)
        {
          if (toolbar.Guid == guid)
            return toolbar;
        }
        return (ToolBar) null;
      }

      public ToolBar FindToolbar(string name)
      {
        foreach (ToolBar toolbar in this._toolbars)
        {
          if (toolbar.Name == name)
            return toolbar;
        }
        return (ToolBar) null;
      }

      public ToolBarContainer[] GetContainers()
      {
        ToolBarContainer[] containers = new ToolBarContainer[this._containers.Count];
        this._containers.CopyTo((Array) containers);
        return containers;
      }

      public void LoadConfiguration(
        BarManagerConfigurationStorage configStorage,
        GetToolbarCallback getToolbarCallback)
      {
        if (configStorage == null)
          throw new ArgumentNullException(nameof (configStorage));
        this._getToolbarCallback = getToolbarCallback;
        string layout = configStorage.TryLoadLayout();
        if (string.IsNullOrEmpty(layout))
          return;
        this.SetLayout(layout);
      }

      public void SaveConfiguration(BarManagerConfigurationStorage configStorage)
      {
        if (configStorage == null)
          throw new ArgumentNullException(nameof (configStorage));
        string layout = this.GetLayout(true);
        configStorage.SaveLayout(layout);
      }

      public string GetLayout() => this.GetLayout(false);

      public string GetLayout(bool includeItemVisibility)
      {
        StringWriter w = new StringWriter();
        XmlTextWriter writer = new XmlTextWriter((TextWriter) w);
        writer.Formatting = Formatting.None;
        writer.WriteStartDocument();
        writer.WriteStartElement("Layout");
        foreach (ToolBar toolbar in this._toolbars)
          this.WriteToolbar(toolbar, writer, includeItemVisibility);
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
        writer.Close();
        return w.ToString();
      }

      public Rectangle GetScreenBounds()
      {
        return this._ownerForm != null ? new Rectangle(this._ownerForm.PointToScreen(new Point(0, 0)), this._ownerForm.ClientRectangle.Size) : Screen.PrimaryScreen.Bounds;
      }

      public ToolBar[] GetToolBars()
      {
        ToolBar[] toolBars = new ToolBar[this._toolbars.Count];
        this._toolbars.CopyTo((Array) toolBars);
        return toolBars;
      }

      public static void Merge(ToolBar source, ToolBar target)
      {
        if (source == null || target == null)
          throw new ArgumentNullException();
        if (source == target)
          throw new ArgumentException("A toolbar cannot merge with itself.");
        if (!source.AllowMerge || !target.AllowMerge)
          return;
        MenuLooper.a();
        if (source.MergedToolBar != null)
          BarManager.UndoMerge(source);
        if (target.MergedToolBar != null)
          BarManager.UndoMerge(target);
        source.OriginalStructure = ToolbarStructure.Create((IButtonsSite) source);
        target.OriginalStructure = ToolbarStructure.Create((IButtonsSite) target);
        source.IgnoreLayoutRequests = true;
        target.IgnoreLayoutRequests = true;
        try
        {
          BarManager.MargeItems(source.Items, target.Items);
          source.SetMergedToolBar(target);
          target.SetMergedToolBar(source);
        }
        finally
        {
          source.IgnoreLayoutRequests = false;
          target.IgnoreLayoutRequests = false;
          source.DoLayout();
          target.DoLayout();
        }
      }

      public void RemoveToolbar(ToolBar toolbar)
      {
        if (this._toolbars.Contains((object) toolbar))
          this._toolbars.Remove((object) toolbar);
        if (toolbar != this._menuBar)
          return;
        this._menuBar = (MenuBar) null;
      }

      public void SetLayout(string layout)
      {
        switch (layout)
        {
          case null:
            break;
          case "":
            break;
          default:
            foreach (Control container in this._containers)
              container.SuspendLayout();
            try
            {
              XmlTextReader reader = new XmlTextReader((TextReader) new StringReader(layout));
              while (reader.Read())
              {
                if (reader.NodeType == XmlNodeType.Element && !(reader.Name != "Layout"))
                  this.ReadConfiguration(reader);
              }
              reader.Close();
            }
            catch
            {
              throw new ArgumentException();
            }
            IEnumerator enumerator = this._containers.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
                ((Control) enumerator.Current).ResumeLayout();
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
        }
      }

      private bool ShouldSerializeRenderer()
      {
        return !(this.Renderer is Office2003Renderer) || this.Renderer is WhidbeyRenderer;
      }

      public static void UndoMerge(ToolBar toolbar)
      {
        if (toolbar.MergedToolBar == null)
          return;
        toolbar.MergedToolBar.IgnoreLayoutRequests = true;
        toolbar.MergedToolBar.OriginalStructure.RestoreItems(toolbar.MergedToolBar.Items);
        toolbar.MergedToolBar.SetMergedToolBar((ToolBar) null);
        toolbar.MergedToolBar.OriginalStructure = (ToolbarStructure) null;
        toolbar.MergedToolBar.IgnoreLayoutRequests = false;
        toolbar.MergedToolBar.DoLayout();
        toolbar.IgnoreLayoutRequests = true;
        toolbar.OriginalStructure.RestoreItems(toolbar.Items);
        toolbar.SetMergedToolBar((ToolBar) null);
        toolbar.OriginalStructure = (ToolbarStructure) null;
        toolbar.IgnoreLayoutRequests = false;
        toolbar.DoLayout();
      }

      [Browsable(false)]
      [Obsolete("Use the FindSuitableContainer method instead.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public ToolBarContainer BottomContainer
      {
        get => this.FindSuitableContainer(DockStyle.Bottom);
        set
        {
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Indicates whether the manager will display a context menu allowing the user to show and hide toolbars.")]
      public bool EnableContextMenu
      {
        get => this._enableContextMenu;
        set => this._enableContextMenu = value;
      }

      internal bool FormHasFocus
      {
        get
        {
          if (this.OwnerForm != null && !this.OwnerForm.IsMdiChild)
          {
            Form activeForm = Form.ActiveForm;
            if (activeForm == null)
              return false;
            if (activeForm != this.OwnerForm)
              return activeForm.Owner == this.OwnerForm;
          }
          return true;
        }
      }

      [Obsolete("Use the FindSuitableContainer method instead.")]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public ToolBarContainer LeftContainer
      {
        get => this.FindSuitableContainer(DockStyle.Left);
        set
        {
        }
      }

      public MenuBar MenuBar => this._menuBar;

      [Browsable(false)]
      public Form OwnerForm
      {
        get => this._ownerForm;
        set
        {
          if (this._ownerForm != null)
          {
            this._ownerForm.Activated -= new EventHandler(this.OwnerForm_Activated);
            this._ownerForm.Deactivate -= new EventHandler(this.OwnerForm_Deactivate);
          }
          this._ownerForm = value;
          if (this._ownerForm != null)
          {
            this._ownerForm.Activated += new EventHandler(this.OwnerForm_Activated);
            this._ownerForm.Deactivate += new EventHandler(this.OwnerForm_Deactivate);
          }
          foreach (ToolBar toolbar in this._toolbars)
          {
            if (toolbar is MenuBar)
              ((MenuBar) toolbar).OwnerForm = this._ownerForm;
          }
        }
      }

      [TypeConverter("Intermech.Bars.RendererConverter")]
      [Description("The renderer currently in use by the toolbar layout.")]
      [Category("Appearance")]
      public IToolBarRenderer Renderer
      {
        get => this._renderer;
        set
        {
          if (value == null)
            throw new ArgumentNullException();
          if (this._renderer != null)
          {
            this._renderer.RedrawRequired -= new EventHandler(this.Renderer_RedrawRequired);
            if (this._rendererNeedDispose)
            {
              this._renderer.Dispose();
              this._rendererNeedDispose = false;
            }
          }
          this._renderer = value;
          if (this._renderer != null)
            this._renderer.RedrawRequired += new EventHandler(this.Renderer_RedrawRequired);
          this.OnRendererChanged();
        }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Obsolete("Use the FindSuitableContainer method instead.")]
      [Browsable(false)]
      public ToolBarContainer RightContainer
      {
        get => this.FindSuitableContainer(DockStyle.Right);
        set
        {
        }
      }

      public override ISite Site
      {
        get => base.Site;
        set
        {
          base.Site = value;
          if (value == null)
            return;
          IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
          if (service == null || !(service.RootComponent is Form))
            return;
          this._ownerForm = (Form) service.RootComponent;
        }
      }

      [Obsolete("Use the FindSuitableContainer method instead.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public ToolBarContainer TopContainer
      {
        get => this.FindSuitableContainer(DockStyle.Top);
        set
        {
        }
      }

      public event EventHandler RendererChanged;

      public event CollectToolbarsHandler CollectToolbars;

      public int Compare(object x, object y)
      {
        ToolBar toolBar1 = x as ToolBar;
        ToolBar toolBar2 = y as ToolBar;
        string strA = string.Empty;
        string strB = string.Empty;
        if (toolBar1 != null)
          strA = toolBar1.Text;
        if (toolBar2 != null)
          strB = toolBar2.Text;
        return string.Compare(strA, strB);
      }

      private class MenuItemWithToolbar : MenuButtonItem
      {
        private ToolBar _toolbar;

        public MenuItemWithToolbar(ToolBar toolbar) => this._toolbar = toolbar;

        public ToolBar GetToolbar() => this._toolbar;
      }
    }
}
