
// Type: Intermech.PropertyEditors.AttrProcessor.GetPossibleDescriptors
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;


namespace Intermech.PropertyEditors.AttrProcessor;

public class GetPossibleDescriptors
{
  private GetPossibleDescriptors()
  {
  }

  /// <summary>Доступные дескрипоторы.</summary>
  public static DescriptorCollection PossibleTypesDescriptors
  {
    get
    {
      return new DescriptorCollection()
      {
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00250-306c-11d8-b4e9-00304f19f545")),
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00170-306c-11d8-b4e9-00304f19f545")),
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad001da-306c-11d8-b4e9-00304f19f545")),
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00252-306c-11d8-b4e9-00304f19f545")),
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad0038d-306c-11d8-b4e9-00304f19f545")),
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545"))
      };
    }
  }
}
