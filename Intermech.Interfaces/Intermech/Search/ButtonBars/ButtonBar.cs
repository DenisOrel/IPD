
// Type: Intermech.Search.ButtonBars.ButtonBar
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Intermech.Search.ButtonBars
{
    [Serializable]
    public sealed class ButtonBar : IButtonBarButtonCollectionOwner, INotifyPropertyChanged
    {
      private string _name;
      private Guid _containerGuid;
      private int _dockLine;
      private int _dockOffset;
      private bool _visible = true;

      public ButtonBar(Guid guid)
      {
        this.Guid = !(guid == Guid.Empty) ? guid : throw new ArgumentException();
        this.Buttons = new ButtonBarButtonCollection((IButtonBarButtonCollectionOwner) this);
        this.Buttons.ListChanged += new ListChangedEventHandler(this.Buttons_ListChanged);
      }

      public Guid Guid { get; private set; }

      public string Name
      {
        get => this._name;
        set
        {
          if (!(this._name != value))
            return;
          this._name = value;
          this.OnPropertyChanged(nameof (Name));
        }
      }

      public Guid ContainerGuid
      {
        get => this._containerGuid;
        set
        {
          if (!(this._containerGuid != value))
            return;
          this._containerGuid = value;
          this.OnPropertyChanged(nameof (ContainerGuid));
        }
      }

      public int DockLine
      {
        get => this._dockLine;
        set
        {
          if (this._dockLine == value)
            return;
          this._dockLine = value;
          this.OnPropertyChanged(nameof (DockLine));
        }
      }

      public int DockOffset
      {
        get => this._dockOffset;
        set
        {
          if (this._dockOffset == value)
            return;
          this._dockOffset = value;
          this.OnPropertyChanged(nameof (DockOffset));
        }
      }

      public bool Visible
      {
        get => this._visible;
        set
        {
          if (this._visible == value)
            return;
          this._visible = value;
          this.OnPropertyChanged(nameof (Visible));
        }
      }

      public ButtonBar Clone()
      {
        ButtonBar buttonBar = new ButtonBar(this.Guid);
        buttonBar.ContainerGuid = this.ContainerGuid;
        buttonBar.DockLine = this.DockLine;
        buttonBar.DockOffset = this.DockOffset;
        buttonBar.Name = this.Name;
        buttonBar.Visible = this.Visible;
        foreach (ButtonBarButton button in (Collection<ButtonBarButton>) this.Buttons)
          buttonBar.Buttons.Add(button.Clone());
        return buttonBar;
      }

      public ButtonBarButtonCollection Buttons { get; private set; }

      public event PropertyChangedEventHandler PropertyChanged;

      private void Buttons_ListChanged(object sender, ListChangedEventArgs e)
      {
        this.OnPropertyChanged("Buttons");
      }

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this.Buttons.ListChanged += new ListChangedEventHandler(this.Buttons_ListChanged);
      }

      private void OnPropertyChanged(string propertyName)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }
    }
}
