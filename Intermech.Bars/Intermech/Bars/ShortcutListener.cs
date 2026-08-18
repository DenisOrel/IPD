
// Type: Intermech.Bars.ShortcutListener
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class ShortcutListener : IDisposable, IMessageFilter, IShortcutListener
    {
      private bool _listening;
      private int _primaryKey;
      private Form _ownerForm;
      private Hashtable _acceleratorTable;
      private Keys _eatKeys;

      public event SecondaryShortcutEventHandler SecondaryShortcutAction;

      public ShortcutListener()
      {
        this._listening = false;
        this._primaryKey = 0;
        this._ownerForm = (Form) null;
        this._acceleratorTable = (Hashtable) null;
        this._acceleratorTable = new Hashtable();
        this._eatKeys = Keys.None;
      }

      private MenuItemBase[] GetItemsForKey(int A_0)
      {
        if (this._acceleratorTable[(object) A_0] is ArrayList)
        {
          ArrayList arrayList = (ArrayList) this._acceleratorTable[(object) A_0];
          MenuItemBase[] itemsForKey = new MenuItemBase[arrayList.Count];
          arrayList.CopyTo((Array) itemsForKey);
          return itemsForKey;
        }
        return new MenuItemBase[1]
        {
          (MenuItemBase) this._acceleratorTable[(object) A_0]
        };
      }

      private char a(string A_0)
      {
        int length = A_0.Length;
        for (int index = 0; index < length; ++index)
        {
          if (A_0[index] == '&' && index + 1 < length && A_0[index + 1] != '&')
            return char.ToUpper(A_0[index + 1]);
        }
        return char.MinValue;
      }

      private void a(MenuItemBase A_0)
      {
        foreach (MenuButtonItem A_0_1 in (CollectionBase) A_0.Items)
        {
          if (A_0_1.PrimaryShortcut != Keys.None && A_0_1.ShortcutActive)
          {
            int primaryShortcut = (int) A_0_1.PrimaryShortcut;
            if (!this._acceleratorTable.Contains((object) primaryShortcut))
            {
              this._acceleratorTable.Add((object) primaryShortcut, (object) A_0_1);
            }
            else
            {
              MenuButtonItem menuButtonItem = (MenuButtonItem) this._acceleratorTable[(object) primaryShortcut];
              this._acceleratorTable[(object) primaryShortcut] = (object) new ArrayList()
              {
                (object) menuButtonItem,
                (object) A_0_1
              };
            }
          }
          if (A_0_1.HasChildren)
            this.a((MenuItemBase) A_0_1);
        }
      }

      public void Dispose()
      {
        this.Listening = false;
        this._acceleratorTable.Clear();
      }

      protected virtual bool IsShortcutWithinScope(Keys keys)
      {
        bool flag = this._ownerForm != null;
        if (flag)
        {
          IntPtr foregroundWindow = Win32.GetForegroundWindow();
          if (this._ownerForm.IsMdiChild)
            return this._ownerForm.MdiParent != null && foregroundWindow == this._ownerForm.MdiParent.Handle && this._ownerForm.MdiParent.ActiveMdiChild == this._ownerForm;
          flag = foregroundWindow == this._ownerForm.Handle;
          if (!flag && Control.FromHandle(foregroundWindow) is Form form && form.Owner == this._ownerForm && !form.Modal)
            flag = true;
        }
        return flag;
      }

      protected virtual void OnSecondaryShortcutAction(SecondaryShortcutEventArgs e)
      {
        if (this.SecondaryShortcutAction == null)
          return;
        this.SecondaryShortcutAction((object) this, e);
      }

      public bool ShortcutActivated(Keys keys, bool primary)
      {
        int num1 = primary ? (int) keys : this._primaryKey;
        if (this._acceleratorTable.Contains((object) num1))
        {
          MenuItemBase[] itemsForKey = this.GetItemsForKey(num1);
          if (itemsForKey.Length == 1 && itemsForKey[0] is TopLevelMenuItemBase)
          {
            ((TopLevelMenuItemBase) itemsForKey[0]).Show(true);
            return true;
          }
          if (!primary)
          {
            foreach (MenuButtonItem menuItem in itemsForKey)
            {
              if (menuItem.SecondaryShortcut == keys && menuItem.b())
              {
                this.OnSecondaryShortcutAction(new SecondaryShortcutEventArgs((Keys) this._primaryKey, keys, menuItem));
                menuItem.OnActivate();
                return true;
              }
            }
            this.OnSecondaryShortcutAction(new SecondaryShortcutEventArgs((Keys) this._primaryKey, keys, (MenuButtonItem) null));
            return false;
          }
          int num2 = 0;
          MenuButtonItem menuButtonItem1 = (MenuButtonItem) null;
          bool flag = false;
          foreach (MenuButtonItem menuButtonItem2 in itemsForKey)
          {
            if (menuButtonItem2.b())
            {
              menuButtonItem1 = menuButtonItem2;
              ++num2;
              if (menuButtonItem2.SecondaryShortcut != Keys.None)
                flag = true;
            }
          }
          if (num2 == 1 && !flag)
          {
            menuButtonItem1.OnActivate();
            return true;
          }
          if (num2 > 0 && !flag)
          {
            menuButtonItem1.OnActivate();
            return true;
          }
          if (num2 > 0 & flag)
          {
            this._primaryKey = (int) keys;
            this.OnSecondaryShortcutAction(new SecondaryShortcutEventArgs(keys));
            return true;
          }
        }
        return false;
      }

      bool IMessageFilter.PreFilterMessage(ref Message m)
      {
        try
        {
          if (m.Msg == 256 /*0x0100*/ || m.Msg == 260)
          {
            Keys keys = (Keys) (int) m.WParam | Control.ModifierKeys;
            if (!this.IsAwaitingSecondaryShortcut && this._acceleratorTable.Contains((object) (int) keys) && this.IsShortcutWithinScope(keys))
            {
              Control control = Control.FromChildHandle(m.HWnd);
              if (control != null)
              {
                Message msg = Message.Create(m.HWnd, m.Msg, m.WParam, m.LParam);
                if (control.PreProcessMessage(ref msg))
                  return true;
              }
              int num = this.ShortcutActivated(keys, true) ? 1 : 0;
              if (num != 0)
                this._eatKeys = keys;
              return num != 0;
            }
            if (this.IsAwaitingSecondaryShortcut)
            {
              if (this.IsShortcutWithinScope(keys))
              {
                switch (keys & Keys.KeyCode)
                {
                  case Keys.ShiftKey:
                  case Keys.ControlKey:
                  case Keys.Menu:
                    break;
                  default:
                    try
                    {
                      Control control = Control.FromChildHandle(m.HWnd);
                      if (control != null)
                      {
                        Message msg = Message.Create(m.HWnd, m.Msg, m.WParam, m.LParam);
                        if (control.PreProcessMessage(ref msg))
                          return true;
                      }
                      return this.ShortcutActivated(keys, false);
                    }
                    finally
                    {
                      this._primaryKey = 0;
                    }
                }
              }
            }
          }
          else if (m.Msg == 257)
          {
            if (this._eatKeys != Keys.None && ((Keys) (int) m.WParam | Control.ModifierKeys) == this._eatKeys)
            {
              this._eatKeys = Keys.None;
              return true;
            }
            this._eatKeys = Keys.None;
          }
        }
        catch (Exception ex)
        {
          Application.OnThreadException(ex);
          return true;
        }
        return false;
      }

      public void UpdateAcceleratorTable(TopLevelMenuItemBase[] menus)
      {
        this._acceleratorTable.Clear();
        foreach (MenuItemBase menu in menus)
          this.a(menu);
      }

      public void UpdateAcceleratorTable(ToolBar toolbar)
      {
        this._acceleratorTable.Clear();
        foreach (ToolbarItemBase A_0 in (CollectionBase) toolbar.Items)
        {
          if (A_0 is TopLevelMenuItemBase)
          {
            char ch = this.a(A_0.Text);
            if (ch != char.MinValue)
            {
              int key = 262144 /*0x040000*/ | (int) ch;
              if (!this._acceleratorTable.Contains((object) key))
                this._acceleratorTable.Add((object) key, (object) A_0);
            }
            this.a((MenuItemBase) A_0);
          }
        }
      }

      protected bool IsAwaitingSecondaryShortcut => this._primaryKey != 0;

      public bool Listening
      {
        get => this._listening;
        set
        {
          if (value && !this._listening)
            Application.AddMessageFilter((IMessageFilter) this);
          else if (!value && this._listening)
            Application.RemoveMessageFilter((IMessageFilter) this);
          this._listening = value;
        }
      }

      public Form OwnerForm
      {
        get => this._ownerForm;
        set => this._ownerForm = value;
      }
    }
}
