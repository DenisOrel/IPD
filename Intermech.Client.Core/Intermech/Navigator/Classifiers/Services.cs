
// Type: Intermech.Navigator.Classifiers.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Views;
using Intermech.Search;


namespace Intermech.Navigator.Classifiers;

internal static class Services
{
  public static void Start()
  {
    if ((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
      ServiceLocator.Get<IPropertyPagesService>().AddPage("Система\\Выборки и классификаторы", (IPropertyPage) new ClassifierGeneralPropertiesPage());
    CalcFormulaProvider provider = new CalcFormulaProvider();
    Holder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    Holder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    Holder.Factory.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
  }
}
