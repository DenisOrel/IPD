
// Type: Intermech.Search.AttributeEditingState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search;

public abstract class AttributeEditingState
{
  public abstract void AcceptChanges(AttributeEditingComponent component);

  public virtual void InitializeEditor(AttributeEditingComponent component)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    if (component.NodeColumn != null)
    {
      if (component.NodeColumn.Attribute != null)
      {
        try
        {
          component.Editor.BeginInit();
          try
          {
            component.Editor.AttributeTypeID = component.NodeColumn.Attribute.AttributeID;
            this.DoInitializeEditor(component);
            return;
          }
          finally
          {
            component.Editor.EndInit();
          }
        }
        catch
        {
          component.SetUndetermined();
          return;
        }
      }
    }
    component.SetUndetermined();
  }

  protected virtual void DoInitializeEditor(AttributeEditingComponent component)
  {
  }
}
