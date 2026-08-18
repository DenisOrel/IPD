// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.ComponentRemovedUndoAction
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// 
/// </summary>
internal class ComponentRemovedUndoAction : IUndoableOperation
{
  private IDesignerHost _host;
  private System.Type _componentType;
  private string _componentName = string.Empty;
  private string _parent = string.Empty;
  private DesignerSerializationService _serializationService;
  private object _serializationData;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="cea"></param>
  /// <param name="parent"></param>
  public ComponentRemovedUndoAction(IDesignerHost host, ComponentEventArgs cea, string parent)
  {
    this._host = host;
    this._componentName = cea.Component.Site.Name;
    this._componentType = cea.Component.GetType();
    this._parent = parent;
    this._serializationService = new DesignerSerializationService(host);
    this._serializationData = this._serializationService.Serialize((ICollection) new object[1]
    {
      (object) cea.Component
    });
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    IComponent component1 = (this._parent == null || this._parent.Length <= 0 ? (IComponent) null : this._host.Container.Components[this._parent]) ?? (IComponent) (this._host.RootComponent as Control);
    if (!(this._serializationService.Deserialize(this._serializationData) is ArrayList arrayList) || arrayList.Count <= 0 || !(arrayList[0] is IComponent component2))
      return;
    (component1 as Control).Controls.Add(component2 as Control);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    this._host.DestroyComponent(this._host.Container.Components[this._componentName]);
  }
}
