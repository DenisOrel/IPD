// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.ComponentAddedUndoAction
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// 
/// </summary>
internal class ComponentAddedUndoAction : IUndoableOperation
{
  private IDesignerHost _host;
  private System.Type _componentType;
  private string _componentName = string.Empty;
  private string _parentComponentName = string.Empty;
  private object _serializationData;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="cea"></param>
  public ComponentAddedUndoAction(IDesignerHost host, ComponentEventArgs cea)
  {
    this._host = host;
    this._componentName = cea.Component.Site.Name;
    this._componentType = cea.Component.GetType();
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    IComponent component = this._host.Container.Components[this._componentName];
    if (component is Control control && control.Parent != null)
      this._parentComponentName = control.Parent.Name;
    if (!(this._host.GetService(typeof (IDesignerSerializationService)) is IDesignerSerializationService service))
      return;
    object[] objects = new object[1]{ (object) component };
    this._serializationData = service.Serialize((ICollection) objects);
    this._host.DestroyComponent(component);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    if (!(this._host.GetService(typeof (IDesignerSerializationService)) is IDesignerSerializationService service) || !(service.Deserialize(this._serializationData) is ArrayList arrayList) || arrayList.Count <= 0 || !(arrayList[0] is IComponent component))
      return;
    ((this._host.Container.Components[this._parentComponentName] ?? (IComponent) (this._host.RootComponent as Control)) as Control).Controls.Add(component as Control);
  }
}
