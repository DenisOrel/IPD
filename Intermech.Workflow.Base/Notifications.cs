// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Notifications
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;


namespace Intermech.Workflow
{
    /// <summary>Summary description for Notifications.</summary>
    public class Notifications : IValidatedItem
    {
      public readonly Notification StartNotify;
      public readonly PeriodNotification PeriodNotify;
      public readonly Notification StopNotify;
      public readonly Notification AbortNotify;
      public readonly Notification ReadNotify;
      public readonly Notification BackNotify;
      public bool _loaded;
      protected System.Collections.Generic.List<Notification> _list = new System.Collections.Generic.List<Notification>();
      private bool _writeGuids;
      /// <summary>
      /// Режим, когда при загрузке из xml уведомления сами определяют, активированы они или нет.
      /// Используется при экспорте в портфель, когда BriefString недоступна
      /// </summary>
      public bool XMLOnlyMode;

      public System.Collections.Generic.List<Notification> List => this._list;

      public Notifications(IUserSession session)
      {
        this.StartNotify = new Notification(this, "Start", 'S', session);
        this.PeriodNotify = new PeriodNotification(this, "Period", 'P', session);
        this.StopNotify = new Notification(this, "Stop", 'T', session);
        this.AbortNotify = new Notification(this, "Abort", 'A', session);
        this.ReadNotify = new Notification(this, "Read", 'R', session);
        this.BackNotify = new Notification(this, "Back", 'B', session);
      }

      public bool Modified
      {
        get
        {
          bool modified = false;
          foreach (Notification notification in this._list)
          {
            modified = notification.Modified;
            if (modified)
              break;
          }
          return modified;
        }
      }

      public bool Loaded
      {
        get
        {
          return this._list.All<Notification>((Func<Notification, bool>) (n => !n.Enabled)) || this._loaded;
        }
      }

      public string AsString
      {
        get
        {
          MemoryStream ms = new MemoryStream();
          try
          {
            this.SaveToStream((Stream) ms);
            return StreamHelper.StreamToString((Stream) ms);
          }
          finally
          {
            ms.Close();
          }
        }
        set
        {
          this._loaded = true;
          MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value));
          try
          {
            this.LoadFromStream((Stream) memoryStream);
            memoryStream.Close();
          }
          finally
          {
            memoryStream.Close();
          }
        }
      }

      public string BriefString
      {
        get
        {
          string briefString = "";
          foreach (Notification notification in this._list)
          {
            if (notification.Enabled)
              briefString += notification.Symbol.ToString();
          }
          return briefString;
        }
        set
        {
          foreach (Notification notification in this._list)
            notification._enabled = value.IndexOf(notification.Symbol) != -1;
        }
      }

      public void Load(IDBAttribute attr)
      {
        string asString = attr.AsString;
        this.BriefString = asString;
        if (!(asString != ""))
          return;
        this.LoadFromStream(attr as IBlobReader);
      }

      public void Save(IDBAttribute attr) => this.SaveToStream(attr as IBlobWriter, this.BriefString);

      public void SaveToStream(Stream stream)
      {
        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartElement(nameof (Notifications));
        foreach (Notification notification in this._list)
          notification.Save(writer);
        writer.WriteEndElement();
        writer.Flush();
      }

      public void LoadFromStream(Stream stream)
      {
        if (stream.Length == 0L)
          return;
        if (stream.Position != 0L)
          stream.Position = 0L;
        XmlTextReader reader = new XmlTextReader(stream);
        reader.ReadStartElement(nameof (Notifications));
        foreach (Notification notification in this._list)
          notification.Load(reader);
      }

      protected void SaveToStream(IBlobWriter writer, string note)
      {
        StreamHelper.SaveToBlobStream(writer, new ProcessStreamDelegate(this.SaveToStream), note);
      }

      protected void LoadFromStream(IBlobReader reader)
      {
        StreamHelper.LoadFromBlobStream(reader, new ProcessStreamDelegate(this.LoadFromStream));
      }

      public bool ProcessVariableReferences(int varAttrID, bool doDeletion)
      {
        bool flag = false;
        foreach (Notification notification in this._list)
          flag = flag || notification.ProcessVariableReferences(varAttrID, doDeletion);
        return flag;
      }

      /// <summary>
      /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
      /// </summary>
      public bool WriteGuids
      {
        get => this._writeGuids || this.Invalid;
        set => this._writeGuids = value;
      }

      /// <summary>
      /// Возвращает набор идентификаторов версий связанных объектов (пользователей, групп и должностей)
      /// </summary>
      public HashSet<long> ObjectIDs
      {
        get
        {
          HashSet<long> objectIds = new HashSet<long>();
          foreach (Notification notification in this._list)
            objectIds.UnionWith((IEnumerable<long>) notification.Recips.ObjectIDs);
          return objectIds;
        }
      }

      internal Notification FindBySymbol(char symbol)
      {
        foreach (Notification bySymbol in this._list)
        {
          if ((int) bySymbol.Symbol == (int) symbol)
            return bySymbol;
        }
        return (Notification) null;
      }

      public bool Invalid
      {
        get
        {
          return this.StartNotify.Invalid || this.PeriodNotify.Invalid || this.StopNotify.Invalid || this.AbortNotify.Invalid || this.ReadNotify.Invalid || this.BackNotify.Invalid;
        }
      }
    }
}
