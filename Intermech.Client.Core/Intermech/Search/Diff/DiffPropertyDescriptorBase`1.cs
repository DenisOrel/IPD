
// Type: Intermech.Search.Diff.DiffPropertyDescriptorBase`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing.Design;


namespace Intermech.Search.Diff;

public abstract class DiffPropertyDescriptorBase<T> : System.ComponentModel.PropertyDescriptor, IDiffPropertyDescriptor
  where T : IDiff
{
  private Type _componentType;
  private Type _propertyType;
  private UITypeEditor _editor = (UITypeEditor) new DiffEditor();

  public DiffPropertyDescriptorBase(Type componentType, string name, Type propertyType)
    : base(name, new Attribute[0])
  {
    if (componentType == (Type) null)
      throw new ArgumentNullException(nameof (componentType));
    if (propertyType == (Type) null)
      throw new ArgumentNullException(nameof (propertyType));
    this._componentType = componentType;
    this._propertyType = propertyType;
  }

  public abstract T GetDiff(IDiffCollection<T> diffCollection);

  public override bool CanResetValue(object component) => false;

  public override Type ComponentType => this._componentType;

  public override object GetEditor(Type editorBaseType) => (object) this._editor;

  public override object GetValue(object component)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    T obj = component is IDiffCollection<T> ? this.GetDiff((IDiffCollection<T>) component) : throw new ArgumentException();
    return obj.FirstOperand == null ? (object) null : obj.FirstOperand.Value;
  }

  public override bool IsReadOnly => true;

  public override Type PropertyType => this._propertyType;

  public override void ResetValue(object component) => throw new NotImplementedException();

  public override void SetValue(object component, object value)
  {
    throw new NotImplementedException();
  }

  public override bool ShouldSerializeValue(object component) => false;

  IDiff IDiffPropertyDescriptor.GetDiff(object component)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    return component is IDiffCollection<T> ? (IDiff) this.GetDiff((IDiffCollection<T>) component) : throw new ArgumentException();
  }
}
