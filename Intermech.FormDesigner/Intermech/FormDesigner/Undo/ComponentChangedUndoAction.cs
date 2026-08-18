// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Undo.ComponentChangedUndoAction
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Undo;

/// <summary>
/// 
/// </summary>
internal class ComponentChangedUndoAction : IUndoableOperation
{
  private IDesignerHost _host;
  private string _componentName = string.Empty;
  private MemberDescriptor _member;
  private bool _isCollection;
  private bool _isComponentCollection;
  private object _oldValue;
  private object _newValue;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="ea"></param>
  public ComponentChangedUndoAction(IDesignerHost host, ComponentChangedEventArgs ea)
  {
    this._host = host;
    if (!(ea.Component is IComponent component1) || component1.Site == null)
      return;
    this._member = ea.Member;
    this._componentName = component1.Site.Name;
    this._isCollection = ea.NewValue is IList;
    if (this._isCollection)
    {
      IList oldValue = ea.OldValue as IList;
      IList newValue = ea.NewValue as IList;
      this._isComponentCollection = newValue.Count > 0 && newValue[0] is IComponent;
      if (oldValue != null)
      {
        object[] objArray = new object[oldValue.Count];
        if (this._isComponentCollection)
        {
          int num = 0;
          foreach (IComponent component2 in (IEnumerable) oldValue)
            objArray[num++] = (object) component2.Site.Name;
        }
        else
          oldValue.CopyTo((Array) objArray, 0);
        this._oldValue = (object) objArray;
      }
      object[] objArray1 = new object[newValue.Count];
      if (this._isComponentCollection)
      {
        int num = 0;
        foreach (IComponent component3 in (IEnumerable) newValue)
          objArray1[num++] = (object) component3.Site.Name;
      }
      else
        newValue.CopyTo((Array) objArray1, 0);
      this._newValue = (object) objArray1;
    }
    else
    {
      this._oldValue = ea.OldValue;
      this._newValue = ea.NewValue;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Undo()
  {
    IComponentChangeService service = this._host.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
    if (this._componentName == null)
      return;
    IComponent component = this._host.Container.Components[this._componentName];
    service.OnComponentChanging((object) component, this._member);
    PropertyInfo property = component.GetType().GetProperty(this._member.Name);
    if (this._isCollection)
    {
      IList list = property.GetValue((object) component, (object[]) null) as IList;
      object[] oldValue = (object[]) this._oldValue;
      if (this._isComponentCollection)
      {
        int num = 0;
        Menu.MenuItemCollection menuItemCollection = list as Menu.MenuItemCollection;
        foreach (string name in oldValue)
        {
          try
          {
            if (menuItemCollection != null)
              menuItemCollection.Add(num++, this._host.Container.Components[name] as MenuItem);
            else
              list.Add((object) this._host.Container.Components[name]);
          }
          catch (Exception ex)
          {
            (this._host.GetService(typeof (IMessageService)) as IMessageService).ShowError(ex, $"Can't add {name} to collection.");
          }
        }
      }
      else
      {
        foreach (object obj in oldValue)
          list.Add(obj);
      }
    }
    else
      property.SetValue((object) component, this._oldValue, (object[]) null);
    service.OnComponentChanged((object) component, this._member, this._newValue, this._oldValue);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Redo()
  {
    IComponentChangeService service = this._host.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
    if (this._componentName == null)
      return;
    IComponent component = this._host.Container.Components[this._componentName];
    service.OnComponentChanging((object) component, this._member);
    System.Type type = component.GetType();
    if (this._isCollection)
    {
      IList list = type.InvokeMember(this._member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty, (Binder) null, (object) component, (object[]) null) as IList;
      list.Clear();
      object[] newValue = (object[]) this._newValue;
      if (this._isComponentCollection)
      {
        foreach (string name in newValue)
          list.Add((object) this._host.Container.Components[name]);
      }
      else
      {
        foreach (object obj in newValue)
          list.Add(obj);
      }
    }
    else
      type.InvokeMember(this._member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty, (Binder) null, (object) component, new object[1]
      {
        this._newValue
      });
    service.OnComponentChanged((object) component, this._member, this._oldValue, this._newValue);
  }
}
