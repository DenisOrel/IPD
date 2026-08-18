// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Settings;
using Intermech.Tools.Settings.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileSettings : ISettingsObject, ICloneable
{
  private ChangeTrackingListAdapter<GlobalId<int>> docTypes;
  private ChangeTrackingListAdapter<GlobalId<int>> docAttributes;

  /// <summary>Создает объект.</summary>
  public SingleFileSettings()
  {
    this.docTypes = new ChangeTrackingListAdapter<GlobalId<int>>(0);
    this.docAttributes = new ChangeTrackingListAdapter<GlobalId<int>>(0);
  }

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  public SingleFileSettings Clone()
  {
    SingleFileSettings clone = this.CreateClone();
    this.FillClone(clone);
    return clone;
  }

  protected virtual SingleFileSettings CreateClone() => new SingleFileSettings();

  protected virtual void FillClone(SingleFileSettings clonedObj)
  {
    clonedObj.docTypes = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) this.docTypes.Items));
    clonedObj.docAttributes = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) this.docAttributes.Items));
  }

  /// <summary>Клонирует объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();

  [CustomCategory("SR_36")]
  [CustomDisplayName("SR_37")]
  [CustomDescription("SR_38")]
  [Editor(typeof (SimpleObjectTypeListUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> DocumentTypes
  {
    get => this.docTypes;
    set => this.docTypes = value;
  }

  [CustomCategory("SR_36")]
  [CustomDisplayName("SR_39")]
  [CustomDescription("SR_40")]
  [Editor(typeof (AttributeTypesUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<GlobalId<int>> DocumentAttributes
  {
    get => this.docAttributes;
    set => this.docAttributes = value;
  }
}
