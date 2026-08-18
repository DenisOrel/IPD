
// Type: Intermech.Navigator.Selections.IBindingEx
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections;

public interface IBindingEx : IBinding
{
  List<PartSlot> CreateNonFolderSlots(IConditionsProvider conditionProvider);
}
