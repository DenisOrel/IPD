
// Type: Intermech.Bars.MdiHelper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Bars
{
    internal class MdiHelper
    {
      private MenuBar _menuBar;
      private Form _form;
      private MdiClient _mdiClient;
      private MdiHelper.MdiHelperWindow _window;

      public MdiHelper(MenuBar A_0)
      {
        this._menuBar = (MenuBar) null;
        this._form = (Form) null;
        this._mdiClient = (MdiClient) null;
        this._window = (MdiHelper.MdiHelperWindow) null;
        this._menuBar = A_0;
      }

      public Form a() => this._form;

      protected internal virtual void OnControlChanged(EventArgs A_0)
      {
        if (this.ControlChanged == null)
          return;
        this.ControlChanged((object) this, A_0);
      }

      private MdiClient GetMdiClient(Form A_0)
      {
        foreach (Control control in (ArrangedElementCollection) A_0.Controls)
        {
          if (control is MdiClient)
            return (MdiClient) control;
        }
        return (MdiClient) null;
      }

      private void AttachClient(MdiClient client)
      {
        if (this._mdiClient == client)
          return;
        if (this._mdiClient != null)
        {
          this._mdiClient.ControlAdded -= new ControlEventHandler(this.Control_Added);
          this._mdiClient.ControlRemoved -= new ControlEventHandler(this.Control_Removed);
          this._window.Dispose();
        }
        this._mdiClient = client;
        if (this._mdiClient == null)
          return;
        client.ControlAdded += new ControlEventHandler(this.Control_Added);
        client.ControlRemoved += new ControlEventHandler(this.Control_Removed);
        this._window = new MdiHelper.MdiHelperWindow(client);
      }

      private void Control_VisibleChanged(object A_0, EventArgs A_1)
      {
        this.OnControlChanged(EventArgs.Empty);
      }

      private void Control_Removed(object A_0, ControlEventArgs A_1)
      {
        A_1.Control.Resize -= new EventHandler(this.Control_Resize);
        A_1.Control.VisibleChanged -= new EventHandler(this.Control_VisibleChanged);
        this.OnControlChanged(EventArgs.Empty);
      }

      public void AttachForm(Form form)
      {
        if (this._form == form)
          return;
        if (this._form != null)
        {
          this._form.ControlAdded -= new ControlEventHandler(this.Form_ControlAdded);
          this._form.ControlRemoved -= new ControlEventHandler(this.Form_ControlRemoved);
          this.AttachClient((MdiClient) null);
        }
        this._form = form;
        if (this._form == null)
          return;
        this._form.ControlAdded += new ControlEventHandler(this.Form_ControlAdded);
        this._form.ControlRemoved += new ControlEventHandler(this.Form_ControlRemoved);
        MdiClient mdiClient = this.GetMdiClient(this._form);
        if (mdiClient == null)
          return;
        this.AttachClient(mdiClient);
      }

      private void Control_Resize(object A_0, EventArgs A_1) => this.OnControlChanged(EventArgs.Empty);

      private void Control_Added(object A_0, ControlEventArgs A_1)
      {
        A_1.Control.Resize += new EventHandler(this.Control_Resize);
        A_1.Control.VisibleChanged += new EventHandler(this.Control_VisibleChanged);
      }

      private void Form_ControlRemoved(object sender, ControlEventArgs cea)
      {
        if (!(cea.Control is MdiClient))
          return;
        this.AttachClient((MdiClient) null);
        this.OnControlChanged(EventArgs.Empty);
      }

      private void Form_ControlAdded(object sender, ControlEventArgs A_1)
      {
        if (!(A_1.Control is MdiClient))
          return;
        this.AttachClient((MdiClient) A_1.Control);
      }

      public event EventHandler ControlChanged;

      private class MdiHelperWindow : NativeWindow, IDisposable
      {
        private MdiClient _client;

        public MdiHelperWindow(MdiClient client)
        {
          this._client = client;
          client.HandleCreated += new EventHandler(this.Client_HandleCreated);
          client.HandleDestroyed += new EventHandler(this.CLient_HandleDestroyed);
          if (!client.IsHandleCreated)
            return;
          this.AssignHandle(client.Handle);
        }

        public void Dispose()
        {
          if (!(this.Handle != IntPtr.Zero))
            return;
          this.ReleaseHandle();
        }

        protected override void WndProc(ref Message A_0)
        {
          if (A_0.Msg == 560)
            return;
          base.WndProc(ref A_0);
        }

        private void CLient_HandleDestroyed(object A_0, EventArgs A_1) => this.ReleaseHandle();

        private void Client_HandleCreated(object A_0, EventArgs A_1)
        {
          this.AssignHandle(this._client.Handle);
        }
      }
    }
}
