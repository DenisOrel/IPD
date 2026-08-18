
// Type: Intermech.Search.ContextMenus.ContextMenuItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;


namespace Intermech.Search.ContextMenus
{
    [Serializable]
    public sealed class ContextMenuItem : INotifyPropertyChanged, IContextMenuItemContainer, ICloneable
    {
      private string _commandName;
      private string _text;
      private bool _beginGroup;
      private ContextMenuItemCollection _items;
      private IContextMenuItemContainer _parent;

      public ContextMenuItem()
      {
        this._items = new ContextMenuItemCollection((IContextMenuItemContainer) this);
        this._items.ListChanged += new ListChangedEventHandler(this.Items_ListChanged);
      }

      public ContextMenuItem(string commandName)
        : this()
      {
        this._commandName = !string.IsNullOrEmpty(commandName) ? commandName : throw new ArgumentException();
      }

      public string CommandName => this._commandName;

      public string Text
      {
        get => this._text;
        set
        {
          if (!(this._text != value))
            return;
          this._text = value;
          this.OnPropertyChanged<string>((Expression<Func<string>>) (() => this.Text));
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
          this.OnPropertyChanged<bool>((Expression<Func<bool>>) (() => this.BeginGroup));
        }
      }

      public IContextMenuItemContainer Parent
      {
        get => this._parent;
        set
        {
          if (this._parent == value)
            return;
          IContextMenuItemContainer parent = this._parent;
          this._parent = value;
          parent?.Items.Remove(this);
          if (this._parent == null)
            return;
          this._parent.Items.Add(this);
        }
      }

      public ContextMenuItem Clone()
      {
        ContextMenuItem contextMenuItem1 = new ContextMenuItem();
        contextMenuItem1._commandName = this._commandName;
        contextMenuItem1._text = this._text;
        contextMenuItem1._beginGroup = this._beginGroup;
        foreach (ContextMenuItem contextMenuItem2 in (Collection<ContextMenuItem>) this.Items)
          contextMenuItem1.Items.Add(contextMenuItem2.Clone());
        return contextMenuItem1;
      }

      public IEnumerable<IContextMenuItemContainer> GetAncestors()
      {
        for (IContextMenuItemContainer ancestor = this.Parent; ancestor != null; ancestor = ancestor is ContextMenuItem ? ((ContextMenuItem) ancestor).Parent : (IContextMenuItemContainer) null)
          yield return ancestor;
      }

      public IEnumerable<IContextMenuItemContainer> GetAncestorsAndSelf()
      {
        yield return (IContextMenuItemContainer) this;
        foreach (IContextMenuItemContainer ancestor in this.GetAncestors())
          yield return ancestor;
      }

      public IEnumerable<ContextMenuItem> GetPreviousSiblings()
      {
        if (this.Parent != null)
        {
          foreach (ContextMenuItem previousSibling in (Collection<ContextMenuItem>) this.Parent.Items)
          {
            if (previousSibling != this)
              yield return previousSibling;
            else
              break;
          }
        }
      }

      public IEnumerable<ContextMenuItem> GetPreviousSiblingsAndSelf()
      {
        foreach (ContextMenuItem previousSibling in this.GetPreviousSiblings())
          yield return previousSibling;
        yield return this;
      }

      public IEnumerable<ContextMenuItem> GetDescendants()
      {
        foreach (ContextMenuItem item in (Collection<ContextMenuItem>) this.Items)
        {
          yield return item;
          foreach (ContextMenuItem descendant in item.GetDescendants())
            yield return descendant;
        }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ContextMenuItemCollection Items => this._items;

      object ICloneable.Clone() => (object) this.Clone();

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this._items.ListChanged += new ListChangedEventHandler(this.Items_ListChanged);
      }

      private void Items_ListChanged(object sender, ListChangedEventArgs e)
      {
        this.OnPropertyChanged<ContextMenuItemCollection>((Expression<Func<ContextMenuItemCollection>>) (() => this.Items));
      }

      private void OnPropertyChanged<T>(Expression<Func<T>> expression)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(((MemberExpression) expression.Body).Member.Name));
      }
    }
}
