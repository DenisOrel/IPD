// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskCaptions
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Project.Evaluator;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class TaskCaptions
{
  [CanBeNull]
  private readonly ProjectDisplayOptions _parent;
  internal bool Modified;
  [CanBeNull]
  private Dictionary<DockStyle, PropInfo> _captions;
  private int _vPadding;

  public TaskCaptions([CanBeNull] ProjectDisplayOptions parent) => this._parent = parent;

  [NotNull]
  private Dictionary<DockStyle, PropInfo> Captions
  {
    get
    {
      if (this._captions == null)
      {
        this._captions = new Dictionary<DockStyle, PropInfo>();
        this._captions[DockStyle.Right] = PropInfos.Find("AssignmentsString");
        this.UpdatePaddings();
      }
      return this._captions;
    }
  }

  [CanBeNull]
  public PropInfo this[DockStyle ds]
  {
    get
    {
      PropInfo propInfo;
      this.Captions.TryGetValue(ds, out propInfo);
      return propInfo;
    }
    set
    {
      PropInfo propInfo = this[ds];
      if (value == propInfo)
        return;
      if (value != null)
        this.Captions[ds] = value;
      else
        this.Captions.Remove(ds);
      this.UpdatePaddings();
      this.Modified = true;
      if (this._parent == null)
        return;
      this._parent.SetModified(true);
    }
  }

  /// <summary>Высота шрифта, для которого нужно зарезервировать место при отображении заголовка сверху/снизу</summary>
  public int VerticalPadding
  {
    get => this._vPadding;
    set
    {
      if (this._vPadding == value)
        return;
      this._vPadding = value;
      this.UpdatePaddings();
    }
  }

  private void UpdatePaddings()
  {
    this.Padding = new Rectangle(0, this.Captions.ContainsKey(DockStyle.Top) ? this._vPadding : 0, 0, this.Captions.ContainsKey(DockStyle.Bottom) ? this._vPadding : 0);
  }

  public Rectangle Padding { get; private set; }

  [NotNull]
  internal string AsString
  {
    get
    {
      List<string> values = new List<string>();
      PropInfo propInfo1 = this[DockStyle.Left];
      values.Add(propInfo1?.Name ?? string.Empty);
      PropInfo propInfo2 = this[DockStyle.Right];
      values.Add(propInfo2?.Name ?? string.Empty);
      PropInfo propInfo3 = this[DockStyle.Top];
      values.Add(propInfo3?.Name ?? string.Empty);
      PropInfo propInfo4 = this[DockStyle.Bottom];
      values.Add(propInfo4?.Name ?? string.Empty);
      return string.Join("|", (IEnumerable<string>) values);
    }
    set
    {
      string[] strArray = value.Split('|');
      this[DockStyle.Left] = strArray.Length != 0 ? PropInfos.Find(strArray[0]) : (PropInfo) null;
      this[DockStyle.Right] = strArray.Length > 1 ? PropInfos.Find(strArray[1]) : (PropInfo) null;
      this[DockStyle.Top] = strArray.Length > 2 ? PropInfos.Find(strArray[2]) : (PropInfo) null;
      this[DockStyle.Bottom] = strArray.Length > 3 ? PropInfos.Find(strArray[3]) : (PropInfo) null;
    }
  }
}
