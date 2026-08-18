
// Type: Intermech.Client.Core.FormDesigner.FormsProviders
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Actions;
using Intermech.Client.Core.FormDesigner.Actions.ContextCommand;
using Intermech.Client.Core.FormDesigner.Actions.ViewDoc;
using Intermech.Client.Core.FormDesigner.External.Classes;
using Intermech.Client.Core.FormDesigner.External.Navigator;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Client.Core.FormDesigner.Utils;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Класс регистрации провайдеров.</summary>
public class FormsProviders
{
  /// <summary>Зарегистрировать провайдер форм и другие сервисы.</summary>
  /// <param name="factory">Factory навигатора</param>
  public static void RegisterFormProviders(IFactory factory)
  {
    if (factory == null)
      return;
    factory.AddViewsProvider(1, (IViewsProvider) new FormDesignerViewProvider());
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    factory.AddViewsProvider(1, service.GetObjectType(ExternalEditorConsts.ExternalEditorObjectType, true).ObjectType, (IViewsProvider) new ExternalEditorParamsViewProvider());
    IFormDesignerActionManager designerActionManager = (IFormDesignerActionManager) new FormDesignerActionManager();
    designerActionManager.RegisterAction(ActionInfo.NoneAction, (IFormDesignerActionHandler) new NoneAction());
    designerActionManager.RegisterAction(ActionInfo.ApplyAction, (IFormDesignerActionHandler) new ApplyAction());
    designerActionManager.RegisterAction(ActionInfo.CancelAction, (IFormDesignerActionHandler) new CancelAction());
    designerActionManager.RegisterAction(ActionInfo.ClassifyAction, (IFormDesignerActionHandler) new ClassifyAction());
    designerActionManager.RegisterAction(ActionInfo.HelpAction, (IFormDesignerActionHandler) new HelpAction());
    designerActionManager.RegisterAction(ActionsInfo.ExternalEditorAction, (IFormDesignerActionHandler) new ExternalEditorAction());
    designerActionManager.RegisterAction(ActionInfo.CheckOutAction, (IFormDesignerActionHandler) new CheckOutAction());
    designerActionManager.RegisterAction(ActionInfo.CheckInAction, (IFormDesignerActionHandler) new CheckInAction());
    designerActionManager.RegisterAction(ActionInfo.ScriptForButtonsAction, (IFormDesignerActionHandler) new ScriptForButtonsAction());
    ContextCommandActionInfo.RegisterAction(designerActionManager);
    ViewDocActionInfo.RegisterAction(designerActionManager);
    ServicesManager.ServiceContainer.AddService(typeof (IFormDesignerActionManager), (object) designerActionManager);
    ServicesManager.ServiceContainer.AddService(typeof (IFormDesignerEventsManager), (object) new FormDesignerEventsManager());
    ServicesManager.ServiceContainer.AddService(typeof (TabControlManager), (object) new TabControlManager());
  }
}
