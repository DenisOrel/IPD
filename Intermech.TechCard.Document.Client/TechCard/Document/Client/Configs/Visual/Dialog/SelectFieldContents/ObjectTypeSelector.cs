// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.ObjectTypeSelector
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents;

public class ObjectTypeSelector
{
  public static bool Select([CanBeNull] Func<int, bool> onFilterPredicate, out IMSObjectType selectedObjectType)
  {
    selectedObjectType = (IMSObjectType) null;
    using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableType, AttributableElements.Object, -1, TechCardConsts.ObjectTypes.TechBaseObjectID))
    {
      if (onFilterPredicate != null)
        advSelectorForm.SelectorFilter = (ISelectorFilter) new ObjTypeSelectorFilter(onFilterPredicate);
      advSelectorForm.Text = LocalizationHolder.rm.GetString("TechCard.Document_183");
      if (advSelectorForm.ShowDialog() != DialogResult.OK)
        return false;
      selectedObjectType = MetaDataHelper.GetObjectType(advSelectorForm.ObjectType);
      return selectedObjectType != null;
    }
  }
}
