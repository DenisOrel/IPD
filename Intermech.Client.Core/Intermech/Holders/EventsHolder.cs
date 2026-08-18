
// Type: Intermech.Holders.EventsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Holders;

/// <summary>Summary description for EventsHolder.</summary>
public class EventsHolder
{
  private static Hashtable WasChangedHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable ApplyHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable CancelHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable FolderDClickHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable TabControlPageOpeningHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable JumpToAttribute4CustomTypeHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable JumpToConfiguratorTreeNodeHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable ReloadConfiguratorTreeHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable FindConfiguratorItemHashtable = Hashtable.Synchronized(new Hashtable());
  private static bool _BlockOnChange = false;

  private static Hashtable GetHashtable(System.Type type)
  {
    if (type == typeof (EventsHolder.WasChangedEventHandler))
      return EventsHolder.WasChangedHashtable;
    if (type == typeof (EventsHolder.ApplyEventHandler))
      return EventsHolder.ApplyHashtable;
    if (type == typeof (EventsHolder.CancelEventHandler))
      return EventsHolder.CancelHashtable;
    if (type == typeof (EventsHolder.FolderDClickEventHandler))
      return EventsHolder.FolderDClickHashtable;
    if (type == typeof (EventsHolder.TabControlPageOpeningEventHandler))
      return EventsHolder.TabControlPageOpeningHashtable;
    if (type == typeof (EventsHolder.JumpToAttribute4CustomTypeEventHandler))
      return EventsHolder.JumpToAttribute4CustomTypeHashtable;
    if (type == typeof (EventsHolder.JumpToConfiguratorTreeNodeEventHandler))
      return EventsHolder.JumpToConfiguratorTreeNodeHashtable;
    return type == typeof (EventsHolder.ReloadConfiguratorTreeEventHandler) ? EventsHolder.ReloadConfiguratorTreeHashtable : (Hashtable) null;
  }

  public static void RegisterEvent(Guid instGuid, Delegate d)
  {
    Hashtable hashtable = EventsHolder.GetHashtable(d.GetType());
    if (hashtable == null)
      return;
    hashtable[(object) instGuid] = (object) d;
  }

  public static void UnregisterEvent(Guid instGuid, System.Type type)
  {
    EventsHolder.GetHashtable(type)?.Remove((object) instGuid);
  }

  public static void FireWasChanged(object s, Guid instGuid, EventArgs e)
  {
    EventsHolder.WasChangedEventHandler changedEventHandler = (EventsHolder.WasChangedEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.WasChangedEventHandler))[(object) instGuid];
    if (changedEventHandler == null)
      return;
    changedEventHandler(s, e);
  }

  public static bool FireApply(object s, Guid instGuid, EventsHolder.BoolArgs e)
  {
    EventsHolder.ApplyEventHandler applyEventHandler = (EventsHolder.ApplyEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.ApplyEventHandler))[(object) instGuid];
    return applyEventHandler != null && applyEventHandler(s, e);
  }

  public static void FireCancel(object s, Guid instGuid, EventArgs e)
  {
    EventsHolder.CancelEventHandler cancelEventHandler = (EventsHolder.CancelEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.CancelEventHandler))[(object) instGuid];
    if (cancelEventHandler == null)
      return;
    cancelEventHandler(s, e);
  }

  public static void FireFolderDClick(object s, Guid instGuid, EventsHolder.FolderArgs e)
  {
    EventsHolder.FolderDClickEventHandler dclickEventHandler = (EventsHolder.FolderDClickEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.FolderDClickEventHandler))[(object) instGuid];
    if (dclickEventHandler == null)
      return;
    dclickEventHandler(s, e);
  }

  public static void FireTabControlPageOpening(
    object s,
    Guid instGuid,
    EventsHolder.TabControlPageOpeningArgs e)
  {
    EventsHolder.TabControlPageOpeningEventHandler openingEventHandler = (EventsHolder.TabControlPageOpeningEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.TabControlPageOpeningEventHandler))[(object) instGuid];
    if (openingEventHandler == null)
      return;
    openingEventHandler(s, e);
  }

  public static void FireJumpToAttribute4CustomType(
    object s,
    Guid instGuid,
    EventsHolder.JumpToAttribute4CustomTypeArgs e)
  {
    EventsHolder.JumpToAttribute4CustomTypeEventHandler typeEventHandler = (EventsHolder.JumpToAttribute4CustomTypeEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.JumpToAttribute4CustomTypeEventHandler))[(object) instGuid];
    if (typeEventHandler == null)
      return;
    typeEventHandler(s, e);
  }

  public static void FireJumpToConfiguratorTreeNode(
    object s,
    Guid instGuid,
    EventsHolder.JumpToConfiguratorTreeNodeArgs e)
  {
    EventsHolder.JumpToConfiguratorTreeNodeEventHandler nodeEventHandler = (EventsHolder.JumpToConfiguratorTreeNodeEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.JumpToConfiguratorTreeNodeEventHandler))[(object) instGuid];
    if (nodeEventHandler == null)
      return;
    nodeEventHandler(s, e);
  }

  public static void FireReloadConfiguratorTree(
    object s,
    Guid instGuid,
    EventsHolder.ReloadConfiguratorTreeArgs e)
  {
    if (!instGuid.Equals(Guid.Empty))
    {
      EventsHolder.ReloadConfiguratorTreeEventHandler treeEventHandler = (EventsHolder.ReloadConfiguratorTreeEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.ReloadConfiguratorTreeEventHandler))[(object) instGuid];
      if (treeEventHandler == null)
        return;
      treeEventHandler(s, e);
    }
    else
    {
      foreach (DictionaryEntry dictionaryEntry in EventsHolder.GetHashtable(typeof (EventsHolder.ReloadConfiguratorTreeEventHandler)))
      {
        EventsHolder.ReloadConfiguratorTreeEventHandler treeEventHandler = (EventsHolder.ReloadConfiguratorTreeEventHandler) dictionaryEntry.Value;
        if (treeEventHandler != null)
          treeEventHandler(s, e);
      }
    }
  }

  public static void FireFindConfiguratorItem(
    object s,
    Guid instGuid,
    EventsHolder.FindConfiguratorItemArgs e)
  {
    EventsHolder.FindConfiguratorItemEventHandler itemEventHandler = (EventsHolder.FindConfiguratorItemEventHandler) EventsHolder.GetHashtable(typeof (EventsHolder.FindConfiguratorItemEventHandler))[(object) instGuid];
    if (itemEventHandler == null)
      return;
    itemEventHandler(s, e);
  }

  public static bool BlockOnChange
  {
    get => EventsHolder._BlockOnChange;
    set => EventsHolder._BlockOnChange = value;
  }

  public delegate void WasChangedEventHandler(object s, EventArgs e);

  public delegate bool ApplyEventHandler(object s, EventsHolder.BoolArgs e);

  public delegate void CancelEventHandler(object s, EventArgs e);

  public delegate void FolderDClickEventHandler(object s, EventsHolder.FolderArgs e);

  public delegate void TabControlPageOpeningEventHandler(
    object s,
    EventsHolder.TabControlPageOpeningArgs e);

  public delegate void JumpToAttribute4CustomTypeEventHandler(
    object s,
    EventsHolder.JumpToAttribute4CustomTypeArgs e);

  public class JumpToAttribute4CustomTypeArgs : EventArgs
  {
    private int category;
    private int typeId;
    private int attributeId;

    public int Category => this.category;

    public int TypeId => this.typeId;

    public int AttributeId => this.attributeId;

    public JumpToAttribute4CustomTypeArgs(int category, int typeId, int attributeId)
    {
      this.category = category;
      this.typeId = typeId;
      this.attributeId = attributeId;
    }
  }

  public delegate void JumpToConfiguratorTreeNodeEventHandler(
    object s,
    EventsHolder.JumpToConfiguratorTreeNodeArgs e);

  public class JumpToConfiguratorTreeNodeArgs : EventArgs
  {
    private int category;
    private object id;

    public int Category => this.category;

    public object Id => this.id;

    public JumpToConfiguratorTreeNodeArgs(int category, object id)
    {
      this.category = category;
      this.id = id;
    }
  }

  public delegate void ReloadConfiguratorTreeEventHandler(
    object s,
    EventsHolder.ReloadConfiguratorTreeArgs e);

  /// <summary>true для перечитки из базы а не из кэшей</summary>
  public class ReloadConfiguratorTreeArgs : EventArgs
  {
  }

  public delegate void FindConfiguratorItemEventHandler(
    object s,
    EventsHolder.FindConfiguratorItemArgs e);

  /// <summary>true для перечитки из базы а не из кэшей</summary>
  public class FindConfiguratorItemArgs : EventArgs
  {
  }

  public class TabControlPageOpeningArgs : CancelEventArgs
  {
    private TabPage tabpage;

    public TabPage TabPage => this.tabpage;

    public TabControlPageOpeningArgs(TabPage aTabPage, bool aCancel)
    {
      this.tabpage = aTabPage;
      this.Cancel = false;
    }
  }

  public class BoolArgs : EventArgs
  {
    private bool _b;

    public bool Boolean => this._b;

    public BoolArgs(bool b) => this._b = b;
  }

  public class FolderArgs : EventArgs
  {
    private int category = -1;
    private object id = (object) -1;
    private IFolder ifolder;

    public int Category => this.category;

    public object Id => this.id;

    public IFolder IFolder => this.ifolder;

    public FolderArgs(int aCategory, object aId, IFolder iFolder)
    {
      this.category = aCategory;
      this.id = aId;
      this.ifolder = iFolder;
    }
  }

  public delegate ArrayList GetListDelegate(object s, params object[] args);

  public delegate FieldTypes GetAttributeTypeDelegate(object s, params object[] args);
}
