
// Type: Intermech.Client.Core.Navigator.Classes.Providers.NavigatorVirtualColumnProviderArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Queries;
using Intermech.PropertyEditors;
using System.Data;


namespace Intermech.Client.Core.Navigator.Classes.Providers;

public class NavigatorVirtualColumnProviderArgs
{
  public NavigatorVirtualColumnProviderArgs(
    RecordMapping mapping,
    DataTable sourceTable,
    ElementTypeInfo typeInfo)
  {
    this.Mapping = mapping;
    this.SourceTable = sourceTable;
    this.TypeInfo = typeInfo;
  }

  public RecordMapping Mapping { get; }

  public DataTable SourceTable { get; }

  public ElementTypeInfo TypeInfo { get; }
}
