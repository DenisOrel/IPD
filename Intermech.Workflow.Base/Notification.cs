// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Notification
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Xml;


namespace Intermech.Workflow
{
    public class Notification : IValidatedItem
    {
      protected bool _modified;
      private string _text = "";
      private string _subject = "";
      private string _name = "";
      private ParticipantList _recips;
      internal bool _enabled;
      private char _symbol = '0';
      protected readonly Notifications _owner;
      internal bool DetectEnabled;

      public bool Enabled
      {
        get => this._enabled;
        set
        {
          if (this._enabled == value)
            return;
          this._enabled = value;
          this._modified = true;
        }
      }

      public ParticipantList Recips
      {
        get => this._recips;
        set
        {
          if (this._recips.Equals((object) value))
            return;
          this._recips = value;
          this._modified = true;
        }
      }

      public bool ProcessVariableReferences(int varAttrID, bool doDeletion)
      {
        return this.Recips.ProcessVariableReferences(varAttrID, doDeletion);
      }

      public virtual bool Modified
      {
        get => this._modified;
        set => this._modified = value;
      }

      public string Name => this._name;

      public char Symbol => this._symbol;

      public Notification(Notifications owner, string name, char symbol, IUserSession session)
      {
        this._owner = owner.FindBySymbol(symbol) == null ? owner : throw new Exception("Duplicate symbol: " + symbol.ToString());
        this._name = name;
        this._symbol = symbol;
        this._recips = new ParticipantList(session);
        this._owner.List.Add(this);
      }

      public string Text
      {
        get => this._text;
        set
        {
          if (!(this._text != value))
            return;
          this._modified = true;
          this._text = value;
        }
      }

      public string Subject
      {
        get => this._subject;
        set
        {
          if (!(this._subject != value))
            return;
          this._modified = true;
          this._subject = value;
        }
      }

      protected virtual void BaseSave(XmlTextWriter writer)
      {
        writer.WriteStartElement("Subject");
        writer.WriteString(this.Subject);
        writer.WriteEndElement();
        writer.WriteStartElement("Text");
        writer.WriteString(this.Text);
        writer.WriteEndElement();
        this._recips.WriteGuids = this._owner.WriteGuids;
        this._recips.Write(writer);
      }

      internal void Save(XmlTextWriter writer)
      {
        if (!this.Enabled)
          return;
        writer.WriteStartElement(this.Name);
        this.BaseSave(writer);
        writer.WriteEndElement();
      }

      protected virtual void BaseLoad(XmlTextReader reader)
      {
        reader.ReadStartElement("Subject");
        this.Subject = reader.ReadString();
        reader.ReadEndElement();
        reader.ReadStartElement("Text");
        this.Text = reader.ReadString();
        reader.ReadEndElement();
        this._recips.Read(reader);
      }

      internal void Load(XmlTextReader reader)
      {
        if (!this.Enabled && !this._owner.XMLOnlyMode)
          return;
        if (!reader.IsStartElement(this.Name))
          return;
        try
        {
          reader.ReadStartElement(this.Name);
          this.BaseLoad(reader);
          reader.ReadEndElement();
          this._modified = false;
          if (!this._owner.XMLOnlyMode)
            return;
          this.Enabled = true;
        }
        catch
        {
          if (!this._owner.XMLOnlyMode)
            return;
          this.Enabled = false;
        }
      }

      public virtual bool Invalid => this.Recips != null && this.Recips.Invalid;
    }
}
