
using Intermech.Bars;
using Intermech.Docking;
using System;
using System.Collections;
using System.Drawing;


namespace IMClient
{
    internal class DocumentNavigator : IDisposable
    {
      private int _position;
      private ArrayList _history;
      protected DropDownMenuItem _backButton;
      protected DropDownMenuItem _forwardButton;
      protected MenuButtonItem _backMenu;
      protected MenuButtonItem _forwardMenu;
      protected bool _tracking;
      protected DocumentContainer _documentContainer;

      public event EventHandler Changed;

      public DocumentNavigator(
        DropDownMenuItem backButton,
        MenuButtonItem backMenu,
        DropDownMenuItem forwardButton,
        MenuButtonItem forwardMenu,
        DocumentContainer documentContainer)
      {
        this._backButton = backButton;
        this._backMenu = backMenu;
        this._forwardButton = forwardButton;
        this._forwardMenu = forwardMenu;
        this._backButton.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.BackButton_BeforePopup);
        this._backMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.BackButton_BeforePopup);
        this._forwardButton.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ForwardButton_BeforePopup);
        this._forwardMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ForwardButton_BeforePopup);
        this._backButton.Click += new EventHandler(this.BackButton_Click);
        this._backMenu.Click += new EventHandler(this.BackButton_Click);
        this._forwardButton.Click += new EventHandler(this.ForwardButton_Click);
        this._forwardMenu.Click += new EventHandler(this.ForwardButton_Click);
        this._history = new ArrayList(32 /*0x20*/);
        this._position = 0;
        this._tracking = true;
        this._documentContainer = documentContainer;
        documentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DocumentContainer_ActiveDocumentChanged);
        documentContainer.DocumentClosed += new DocumentClosedEventHandler(this.DocumentContainer_DocumentClosed);
      }

      private void Back(int steps)
      {
        for (int index = this._position - steps - 1; index >= 0; --index)
        {
          if (this._history[index] != null)
          {
            this._tracking = false;
            this._documentContainer.ActiveDocument = (DockControl) this._history[index];
            this._tracking = true;
            this._position = index + 1;
            this.OnChanged();
            break;
          }
        }
      }

      private void Forward(int steps)
      {
        for (int index = this._position + steps - 1; index < this._history.Count; ++index)
        {
          if (this._history[index] != null)
          {
            this._tracking = false;
            this._documentContainer.ActiveDocument = (DockControl) this._history[index];
            this._tracking = true;
            this._position = index + 1;
            this.OnChanged();
            break;
          }
        }
      }

      private bool CanBack => this._position > 1;

      private bool CanForward
      {
        get => this._position < this._history.Count && this._history[this._position] != null;
      }

      private string BackName
      {
        get
        {
          if (!this.CanBack)
            return LocalizationHolder.rm.GetString("IMClient_1");
          DockControl dockControl = (DockControl) this._history[this._position - 2];
          return dockControl != null ? LocalizationHolder.rm.GetString("IMClient_2") + dockControl.Text : LocalizationHolder.rm.GetString("IMClient_3");
        }
      }

      private string ForwardName
      {
        get
        {
          if (!this.CanForward)
            return LocalizationHolder.rm.GetString("IMClient_4");
          DockControl dockControl = (DockControl) this._history[this._position];
          return LocalizationHolder.rm.GetString("IMClient_5") + dockControl.Text;
        }
      }

      private void DocumentContainer_ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
      {
        DockControl newActiveDocument = e.NewActiveDocument;
        if (newActiveDocument == null || newActiveDocument is DockControlProxy || !this._tracking || this._position > 0 && this._history.Count != 0 && this._history[this._position - 1] == newActiveDocument)
          return;
        while (this._position >= this._history.Count)
          this._history.Add((object) null);
        this._history[this._position] = (object) newActiveDocument;
        ++this._position;
        for (int position = this._position; position < this._history.Count; ++position)
          this._history[position] = (object) null;
        this.OnChanged();
      }

      private void DocumentContainer_DocumentClosed(object sender, DocumentClosedEventArgs e)
      {
        bool flag = false;
        if (this._documentContainer.Controls.Count < 2)
        {
          this._position = 0;
          this._history.Clear();
          this.OnChanged();
        }
        else
        {
          DockControl document = e.Document;
          int count1 = this._history.Count;
          for (int index = 0; index < count1; ++index)
          {
            if ((DockControl) this._history[index] == document)
            {
              --count1;
              this._history.RemoveAt(index);
              if (this._position > 1 && index <= this._position)
                --this._position;
              flag = true;
            }
          }
          int count2 = this._history.Count;
          for (int index = 0; index < count2 - 1; ++index)
          {
            if (this._history[index] == this._history[index + 1])
            {
              --count2;
              this._history.RemoveAt(index);
              if (this._position > 1 && index <= this._position)
                --this._position;
              flag = true;
            }
          }
          if (!flag)
            return;
          this.OnChanged();
        }
      }

      private void OnChanged()
      {
        if (this.Changed != null)
          this.Changed((object) this, new EventArgs());
        this._backButton.Enabled = this.CanBack;
        this._backMenu.Enabled = this._backButton.Enabled;
        this._forwardButton.Enabled = this.CanForward;
        this._forwardMenu.Enabled = this._forwardButton.Enabled;
        this._backButton.ToolTipText = this.BackName;
        this._forwardButton.ToolTipText = this.ForwardName;
      }

      private MenuButtonItem GetButtonItem(DockControl dc)
      {
        MenuButtonItem buttonItem = new MenuButtonItem(dc.Text);
        if (dc.TabImageIndex != -1)
          buttonItem.ImageIndex = dc.TabImageIndex;
        else if (dc.TabImage != null)
          buttonItem.Image = (Image) dc.TabImage.Clone();
        return buttonItem;
      }

      private void BackButton_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this._backButton.DisposeChildren();
        if (!this.CanBack)
          return;
        int num1 = this._position - 1;
        int num2 = 0;
        for (; num1 > 0; --num1)
        {
          DockControl dc = (DockControl) this._history[num1 - 1];
          if (dc != null)
          {
            MenuButtonItem buttonItem = this.GetButtonItem(dc);
            buttonItem.Tag = (object) ++num2;
            if (num2 > 10)
              buttonItem.Importance = ToolBarItemImportance.Low;
            buttonItem.Click += new EventHandler(this.BackMenu_Click);
            this._backButton.Items.Add((ToolbarItemBase) buttonItem);
          }
        }
      }

      private void ForwardButton_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this._forwardButton.DisposeChildren();
        if (!this.CanForward)
          return;
        int num1 = this._history.Count - this._position;
        int num2 = 0;
        int position = this._position;
        for (; num1 > 0; --num1)
        {
          DockControl dc = (DockControl) this._history[position++];
          if (dc == null)
            break;
          MenuButtonItem buttonItem = this.GetButtonItem(dc);
          buttonItem.Tag = (object) ++num2;
          if (num2 > 10)
            buttonItem.Importance = ToolBarItemImportance.Low;
          buttonItem.Click += new EventHandler(this.ForwardMenu_Click);
          this._forwardButton.Items.Add((ToolbarItemBase) buttonItem);
        }
      }

      public void Dispose()
      {
      }

      private void ForwardMenu_Click(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem))
          return;
        this.Forward((int) menuButtonItem.Tag);
      }

      private void BackMenu_Click(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem))
          return;
        this.Back((int) menuButtonItem.Tag);
      }

      private void BackButton_Click(object sender, EventArgs e) => this.Back(1);

      private void ForwardButton_Click(object sender, EventArgs e) => this.Forward(1);
    }
}
