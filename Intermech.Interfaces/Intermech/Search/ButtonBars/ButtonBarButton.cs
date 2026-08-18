
// Type: Intermech.Search.ButtonBars.ButtonBarButton
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
    public sealed class ButtonBarButton : IButtonBarButtonCollectionOwner, INotifyPropertyChanged
    {
      private IButtonBarButtonCollectionOwner _parent;
      private string _text;
      private string _toolTipText;
      private bool _beginGroup;
      private bool _visible = true;
      private ButtonBarButtonDisplayType _displayType;

      public ButtonBarButton(string commandName)
      {
        this.CommandName = !string.IsNullOrEmpty(commandName) ? commandName : throw new ArgumentException();
        this.Buttons = new ButtonBarButtonCollection((IButtonBarButtonCollectionOwner) this);
        this.Buttons.ListChanged += new ListChangedEventHandler(this.Buttons_ListChanged);
      }

      public string CommandName { get; private set; }

      public IButtonBarButtonCollectionOwner Parent
      {
        get => this._parent;
        set
        {
          if (this._parent == value)
            return;
          IButtonBarButtonCollectionOwner parent = this._parent;
          this._parent = value;
          parent?.Buttons.Remove(this);
          if (this._parent == null)
            return;
          this._parent.Buttons.Add(this);
        }
      }

      public string Text
      {
        get => this._text;
        set
        {
          if (!(this._text != value))
            return;
          this._text = value;
          this.OnPropertyChanged(nameof (Text));
        }
      }

      public string ToolTipText
      {
        get => this._toolTipText;
        set
        {
          if (!(this._toolTipText != value))
            return;
          this._toolTipText = value;
          this.OnPropertyChanged(nameof (ToolTipText));
        }
      }

      public bool BeginGroup
      {
        get => this._beginGroup;
        set
        {
          if (this._beginGroup == value)
            return;
          this._beginGroup = value;
          this.OnPropertyChanged(nameof (BeginGroup));
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

      public ButtonBarButtonDisplayType DisplayType
      {
        get => this._displayType;
        set
        {
          if (this._displayType == value)
            return;
          this._displayType = value;
          this.OnPropertyChanged(nameof (DisplayType));
        }
      }

      public ButtonBarButton Clone()
      {
        ButtonBarButton buttonBarButton = new ButtonBarButton(this.CommandName);
        buttonBarButton.BeginGroup = this.BeginGroup;
        buttonBarButton.DisplayType = this.DisplayType;
        buttonBarButton.Text = this.Text;
        buttonBarButton.ToolTipText = this.ToolTipText;
        buttonBarButton.Visible = this.Visible;
        foreach (ButtonBarButton button in (Collection<ButtonBarButton>) this.Buttons)
          buttonBarButton.Buttons.Add(button.Clone());
        return buttonBarButton;
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
