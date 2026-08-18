// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerProperties
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Общие настройки Редактора форм</summary>
internal class FormDesignerProperties
{
  /// <summary>
  /// Сохранять индексы активных закладок при редактировании формы
  /// </summary>
  private bool _SaveTabPageIndices = true;
  internal bool _inited;

  internal void ApplyUpdates()
  {
    (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).WriteBool("CLIENT", "FORMDESIGNER", "SAVETABPAGEINDICES", this._SaveTabPageIndices, 0L);
  }

  internal void LoadCurrentValues()
  {
    this._SaveTabPageIndices = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("CLIENT", "FORMDESIGNER", "SAVETABPAGEINDICES", true, DBConfigMode.GlobalOnly);
  }

  private void CheckInited()
  {
    if (this._inited)
      return;
    this.LoadCurrentValues();
    this._inited = true;
  }

  [CustomDescription("SaveTabPageIndicesDescription")]
  [CustomDisplayName("SaveTabPageIndicesCaption")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(true)]
  public bool SaveTabPageIndices
  {
    get
    {
      this.CheckInited();
      return this._SaveTabPageIndices;
    }
    set => this._SaveTabPageIndices = value;
  }
}
