
// Type: Intermech.AttributeOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech
{
    public class AttributeOptionsHelper
    {
      public static string GetCaption(AttributeOptions option)
      {
        return EnumTypeHelper.GetCaption((Enum) option);
      }

      public static AttributeOptions GetAttributeOption(string s)
      {
        return (AttributeOptions) EnumTypeHelper.GetEnumValue(typeof (AttributeOptions), s);
      }

      public static string GetCaptions(AttributeOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((AttributeOptions.SaveInLog & options) == AttributeOptions.SaveInLog)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.SaveInLog) + ", ");
        if ((AttributeOptions.SavePrivateHistory & options) == AttributeOptions.SavePrivateHistory)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.SavePrivateHistory) + ", ");
        if ((AttributeOptions.SaveCommonHistory & options) == AttributeOptions.SaveCommonHistory)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.SaveCommonHistory) + ", ");
        if ((AttributeOptions.DisableNulls & options) == AttributeOptions.DisableNulls)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls) + ", ");
        if ((AttributeOptions.GetDescriptionEvent & options) == AttributeOptions.GetDescriptionEvent)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.GetDescriptionEvent) + ", ");
        if ((AttributeOptions.Internal & options) == AttributeOptions.Internal)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.Internal) + ", ");
        if ((AttributeOptions.ModifyInBase & options) == AttributeOptions.ModifyInBase)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ModifyInBase) + ", ");
        if ((AttributeOptions.DontCopyPrototypeValue & options) == AttributeOptions.DontCopyPrototypeValue)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeValue) + ", ");
        if ((AttributeOptions.DontCopyPrototypeAttributeValueForArticle & options) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeAttributeValueForArticle) + ", ");
        if ((AttributeOptions.Identifier & options) == AttributeOptions.Identifier)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.Identifier) + ", ");
        if ((AttributeOptions.EnableOwnerAccessCheck & options) == AttributeOptions.EnableOwnerAccessCheck)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.EnableOwnerAccessCheck) + ", ");
        if ((AttributeOptions.AddToGlobalIndex & options) == AttributeOptions.AddToGlobalIndex)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.AddToGlobalIndex) + ", ");
        if ((AttributeOptions.DisableSplitIndexValue & options) == AttributeOptions.DisableSplitIndexValue)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.DisableSplitIndexValue) + ", ");
        if ((AttributeOptions.LocalImbaseAttribute & options) == AttributeOptions.LocalImbaseAttribute)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.LocalImbaseAttribute) + ", ");
        if ((AttributeOptions.FreeFlag1 & options) == AttributeOptions.FreeFlag1)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.FreeFlag1) + ", ");
        if ((AttributeOptions.FreeFlag2 & options) == AttributeOptions.FreeFlag2)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.FreeFlag2) + ", ");
        if ((AttributeOptions.ImbaseFlag_AVS & options) == AttributeOptions.ImbaseFlag_AVS)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_AVS) + ", ");
        if ((AttributeOptions.ImbaseFlag_CADMECH & options) == AttributeOptions.ImbaseFlag_CADMECH)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_CADMECH) + ", ");
        if ((AttributeOptions.ImbaseFlag_CADMECH_T & options) == AttributeOptions.ImbaseFlag_CADMECH_T)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_CADMECH_T) + ", ");
        if ((AttributeOptions.ImbaseFlag_IMHGen & options) == AttributeOptions.ImbaseFlag_IMHGen)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_IMHGen) + ", ");
        if ((AttributeOptions.ImbaseFlag_SEARCH & options) == AttributeOptions.ImbaseFlag_SEARCH)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_SEARCH) + ", ");
        if ((AttributeOptions.ImbaseFlag_TableRecordRef & options) == AttributeOptions.ImbaseFlag_TableRecordRef)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_TableRecordRef) + ", ");
        if ((AttributeOptions.ImbaseFlag_UsedInTables & options) == AttributeOptions.ImbaseFlag_UsedInTables)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_UsedInTables) + ", ");
        if ((AttributeOptions.EditableLocalImbaseAttribute & options) == AttributeOptions.EditableLocalImbaseAttribute)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.EditableLocalImbaseAttribute) + ", ");
        if ((AttributeOptions.DontCopyVersionValue & options) == AttributeOptions.DontCopyVersionValue)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyVersionValue) + ", ");
        if ((AttributeOptions.CopyValues2ChildObject & options) == AttributeOptions.CopyValues2ChildObject)
          stringBuilder.Append(AttributeOptionsHelper.GetCaption(AttributeOptions.CopyValues2ChildObject) + ", ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
