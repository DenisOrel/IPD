
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingClientService : IGroupAttributesChangingClientService
{
  public void ChangeAttributes(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException(nameof (objectVersionIds));
    if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    using (GroupAttributesChangingForm attributesChangingForm = new GroupAttributesChangingForm())
    {
      attributesChangingForm.ObjectVersionIds = ((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>();
      attributesChangingForm.TrySetCommonEditableAttributesAsDefault = true;
      int num = (int) attributesChangingForm.ShowDialog();
    }
  }
}
