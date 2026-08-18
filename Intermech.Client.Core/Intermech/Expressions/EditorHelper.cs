
// Type: Intermech.Expressions.EditorHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;


namespace Intermech.Expressions;

internal class EditorHelper : ICategoryProps
{
  private int _category;

  public EditorHelper(int category) => this._category = category;

  internal static void Initialize()
  {
  }

  private static void System_StartupComplete(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service))
      return;
    service.RegisterCategoryProps(3, (ICategoryProps) new EditorHelper(3));
    service.RegisterCategoryProps(4, (ICategoryProps) new EditorHelper(4));
  }

  public string SubscriberID => "ExpressionEditorHelper";

  public PropDescriptor[] GetPropDescriptors(PropDescriptorHolder pdh, int category, object id)
  {
    return (PropDescriptor[]) null;
  }

  public bool Apply(PropDescriptorHolder pdh, int category, object id, object idOld) => true;

  public void Cancel(PropDescriptorHolder pdh, int category, object id)
  {
  }

  public void ChangeEventData(PropDescriptorHolder pdh, int category, object id, EventArgs e)
  {
  }
}
