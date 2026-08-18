
// Type: Intermech.ObjectTypeOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech
{
    public class ObjectTypeOptionsHelper
    {
      public static string GetCaption(ObjectTypeOptions option)
      {
        return EnumTypeHelper.GetCaption((Enum) option);
      }

      public static ObjectTypeOptions GetObjectTypeOption(string s)
      {
        return (ObjectTypeOptions) EnumTypeHelper.GetEnumValue(typeof (ObjectTypeOptions), s);
      }

      public static string GetCaptions(ObjectTypeOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((ObjectTypeOptions.NotificationsEnabled & options) == ObjectTypeOptions.NotificationsEnabled)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.NotificationsEnabled) + ", ");
        if ((ObjectTypeOptions.ReleaseArticlesEnabled & options) == ObjectTypeOptions.ReleaseArticlesEnabled)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ReleaseArticlesEnabled) + ", ");
        if ((ObjectTypeOptions.CurrentProjectEnabled & options) == ObjectTypeOptions.CurrentProjectEnabled)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CurrentProjectEnabled) + ", ");
        if ((ObjectTypeOptions.CheckParentAccess & options) == ObjectTypeOptions.CheckParentAccess)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CheckParentAccess) + ", ");
        if ((ObjectTypeOptions.LocalObjectType & options) == ObjectTypeOptions.LocalObjectType)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.LocalObjectType) + ", ");
        if ((ObjectTypeOptions.DisableManualCreate & options) == ObjectTypeOptions.DisableManualCreate)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisableManualCreate) + ", ");
        if ((ObjectTypeOptions.CreateSnapshots & options) == ObjectTypeOptions.CreateSnapshots)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CreateSnapshots) + ", ");
        if ((ObjectTypeOptions.AutoCreateSnapshots & options) == ObjectTypeOptions.AutoCreateSnapshots)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoCreateSnapshots) + ", ");
        if ((ObjectTypeOptions.DisablePrototyping & options) == ObjectTypeOptions.DisablePrototyping)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisablePrototyping) + ", ");
        if ((ObjectTypeOptions.ForumEnabled & options) == ObjectTypeOptions.ForumEnabled)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ForumEnabled) + ", ");
        if ((ObjectTypeOptions.AutoContextEnabled & options) == ObjectTypeOptions.AutoContextEnabled)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoContextEnabled) + ", ");
        if ((ObjectTypeOptions.MandateAccess & options) == ObjectTypeOptions.MandateAccess)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.MandateAccess) + ", ");
        if ((ObjectTypeOptions.AttributesIndex & options) == ObjectTypeOptions.AttributesIndex)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AttributesIndex) + ", ");
        if ((ObjectTypeOptions.ExtendedAudit & options) == ObjectTypeOptions.ExtendedAudit)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ExtendedAudit) + ", ");
        if ((ObjectTypeOptions.EnableWebEdit & options) == ObjectTypeOptions.EnableWebEdit)
          stringBuilder.Append(ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.EnableWebEdit) + ", ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      /// <summary>
      /// 
      /// </summary>
      /// <param name="flag">флаг</param>
      /// <param name="optionsPrev">старый набор флагов</param>
      /// <param name="optionsNext">новый набор набор флагов</param>
      /// <returns>+1 параметр добавился; 0  параметр не изменился; -1 параметр удалился </returns>
      private static int GetDiffResult(
        ObjectTypeOptions flag,
        ObjectTypeOptions optionsPrev,
        ObjectTypeOptions optionsNext)
      {
        bool flag1 = (optionsPrev & flag) == flag;
        bool flag2 = (optionsNext & flag) == flag;
        if (flag1 & flag2 || !flag1 && !flag2)
          return 0;
        if (flag1 && !flag2)
          return -1;
        return !flag1 & flag2 ? 1 : 0;
      }

      private static string GetDiffSignature(int r)
      {
        if (r > 0)
          return "[+]";
        return r < 0 ? "[-]" : "";
      }

      /// <summary>
      /// Вернуть список изменений опций в виде "[+] optionName, [-] optionName ..." в зависимости от того, добавился или очистился параметр
      /// </summary>
      /// <param name="optionsPrev"></param>
      /// <param name="optionsNext"></param>
      /// <returns></returns>
      public static string GetDiffCaptions(ObjectTypeOptions optionsPrev, ObjectTypeOptions optionsNext)
      {
        string empty = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        string diffSignature1 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.NotificationsEnabled, optionsPrev, optionsNext));
        if (diffSignature1 != string.Empty)
          stringBuilder.Append($"{diffSignature1} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.NotificationsEnabled)}, ");
        string diffSignature2 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.ReleaseArticlesEnabled, optionsPrev, optionsNext));
        if (diffSignature2 != string.Empty)
          stringBuilder.Append($"{diffSignature2} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ReleaseArticlesEnabled)}, ");
        string diffSignature3 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.CurrentProjectEnabled, optionsPrev, optionsNext));
        if (diffSignature3 != string.Empty)
          stringBuilder.Append($"{diffSignature3} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CurrentProjectEnabled)}, ");
        string diffSignature4 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.CheckParentAccess, optionsPrev, optionsNext));
        if (diffSignature4 != string.Empty)
          stringBuilder.Append($"{diffSignature4} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CheckParentAccess)}, ");
        string diffSignature5 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.LocalObjectType, optionsPrev, optionsNext));
        if (diffSignature5 != string.Empty)
          stringBuilder.Append($"{diffSignature5} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.LocalObjectType)}, ");
        string diffSignature6 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.DisableManualCreate, optionsPrev, optionsNext));
        if (diffSignature6 != string.Empty)
          stringBuilder.Append($"{diffSignature6} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisableManualCreate)}, ");
        string diffSignature7 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.CreateSnapshots, optionsPrev, optionsNext));
        if (diffSignature7 != string.Empty)
          stringBuilder.Append($"{diffSignature7} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CreateSnapshots)}, ");
        string diffSignature8 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.ForumEnabled, optionsPrev, optionsNext));
        if (diffSignature8 != string.Empty)
          stringBuilder.Append($"{diffSignature8} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ForumEnabled)}, ");
        string diffSignature9 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.AutoContextEnabled, optionsPrev, optionsNext));
        if (diffSignature9 != string.Empty)
          stringBuilder.Append($"{diffSignature9} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoContextEnabled)}, ");
        string diffSignature10 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.MandateAccess, optionsPrev, optionsNext));
        if (diffSignature10 != string.Empty)
          stringBuilder.Append($"{diffSignature10} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.MandateAccess)}, ");
        string diffSignature11 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.AttributesIndex, optionsPrev, optionsNext));
        if (diffSignature11 != string.Empty)
          stringBuilder.Append($"{diffSignature11} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AttributesIndex)}, ");
        string diffSignature12 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.AutoCreateSnapshots, optionsPrev, optionsNext));
        if (diffSignature12 != string.Empty)
          stringBuilder.Append($"{diffSignature12} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoCreateSnapshots)}, ");
        string diffSignature13 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.DisablePrototyping, optionsPrev, optionsNext));
        if (diffSignature13 != string.Empty)
          stringBuilder.Append($"{diffSignature13} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisablePrototyping)}, ");
        string diffSignature14 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.ExtendedAudit, optionsPrev, optionsNext));
        if (diffSignature14 != string.Empty)
          stringBuilder.Append($"{diffSignature14} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ExtendedAudit)}, ");
        string diffSignature15 = ObjectTypeOptionsHelper.GetDiffSignature(ObjectTypeOptionsHelper.GetDiffResult(ObjectTypeOptions.EnableWebEdit, optionsPrev, optionsNext));
        if (diffSignature15 != string.Empty)
          stringBuilder.Append($"{diffSignature15} {ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.EnableWebEdit)}, ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
