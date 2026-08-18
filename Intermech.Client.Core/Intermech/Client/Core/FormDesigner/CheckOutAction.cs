
// Type: Intermech.Client.Core.FormDesigner.CheckOutAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Взять на редактирование.</summary>
internal class CheckOutAction : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form)
  {
    bool flag = false;
    if (form is DesForm desForm)
      flag = desForm.CanCheckoutFlag;
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    if (!(form is DesForm serviceInstance))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(serviceInstance.Info.ElementIdentifier, false);
      if (objectActualCopy == null || objectActualCopy.CheckoutBy != 0L)
        return;
      ISelectedItems items = Services.GetItems(objectActualCopy.ObjectID);
      AdvancedServiceContainer viewServices1 = new AdvancedServiceContainer(serviceInstance.ServiceProvider);
      viewServices1.AddService(typeof (DesForm), (object) serviceInstance);
      AdvancedServiceContainer viewServices2 = viewServices1;
      Services.InvokeCommand("CheckOut", Services.GetCommandsTable(items, (IServiceProvider) viewServices2), (IServiceProvider) viewServices1);
    }
  }
}
