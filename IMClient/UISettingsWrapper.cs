
// Type: IMClient.UISettingsWrapper




using Intermech;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;


namespace IMClient
{
    internal sealed class UISettingsWrapper
    {
      private int _mruCount;
      private readonly IConfigurationManager _manager;
      private int[] _allowableForHighlightingSimilarCharactersObjectTypes = new int[0];

      public void Apply()
      {
        UISettings.AskOnExit = this.AskOnExit;
        UISettings.ShowVersionIDs = this.ShowVersionIDs;
        UISettings.ShowShortAttributeNames = this.ShowShortAttributeNames;
        UISettings.ShowSplash = this.ShowSplash;
        UISettings.NavigatorWindowCaptionsMode = this.NavigatorWindowCaptionsMode;
        UISettings.ShowGridChkoutColumn = this.ShowGridChkoutColumn;
        UISettings.ShowTreeChkoutColumn = this.ShowTreeChkoutColumn;
        UISettings.NavigatorWindowBaseVersionsMode = this.NavigatorWindowBaseVersionsMode;
        UISettings.NavigatorLinksMode = this.NavigatorLinksMode;
        UISettings.AlwaysShowFirstTab = this.AlwaysShowFirstTab;
        UISettings.SwitchToCard = this.SwitchToCard;
        UISettings.SaveSelectedChildrenViewObjectFilter = this.SaveSelectedChildrenViewObjectFilter;
        UISettings.ShowSelectionsTabsForObjectTypes = this.ShowSelectionsTabsForObjectTypes;
        UISettings.ShowUnitedSelections = this.ShowUnitedSelections;
        UISettings.ShowFavoritesFolder = this.ShowFavoritesFolder;
        UISettings.ShowListObjectTypes4CreatingObject = this.ShowListObjectTypes4CreatingObject;
        UISettings.CyrillicSimilarLatinCharacterHighlightColor = this.CyrillicSimilarLatinCharacterHighlightColor;
        UISettings.HighlightCyrillicSimilarLatinCharacters = this.HighlightCyrillicSimilarLatinCharacters;
        UISettings.HighlightLatinSimilarCyrillicCharacters = this.HighlightLatinSimilarCyrillicCharacters;
        UISettings.LatinSimilarCyrillicCharacterHighlightColor = this.LatinSimilarCyrillicCharacterHighlightColor;
        UISettings.AllowableForHighlightingSimilarCharactersObjectTypes = this.AllowableForHighlightingSimilarCharactersObjectTypes;
        UISettings.RaiseChanged();
        ICreateObjByTypeMRU service;
        if ((service = ServicesManager.GetService<ICreateObjByTypeMRU>()) == null)
          return;
        service.MaxCapacity = this._mruCount;
      }

      public void RestoreValues()
      {
        this.AskOnExit = UISettings.AskOnExit;
        this.ShowVersionIDs = UISettings.ShowVersionIDs;
        this.ShowShortAttributeNames = UISettings.ShowShortAttributeNames;
        this.ShowSplash = UISettings.ShowSplash;
        this.NavigatorWindowCaptionsMode = UISettings.NavigatorWindowCaptionsMode;
        this.ShowGridChkoutColumn = UISettings.ShowGridChkoutColumn;
        this.ShowTreeChkoutColumn = UISettings.ShowTreeChkoutColumn;
        this.NavigatorWindowBaseVersionsMode = UISettings.NavigatorWindowBaseVersionsMode;
        this.NavigatorLinksMode = UISettings.NavigatorLinksMode;
        this.AlwaysShowFirstTab = UISettings.AlwaysShowFirstTab;
        this.SwitchToCard = UISettings.SwitchToCard;
        this.SaveSelectedChildrenViewObjectFilter = UISettings.SaveSelectedChildrenViewObjectFilter;
        this.ShowSelectionsTabsForObjectTypes = UISettings.ShowSelectionsTabsForObjectTypes;
        this.ShowUnitedSelections = UISettings.ShowUnitedSelections;
        this.ShowFavoritesFolder = UISettings.ShowFavoritesFolder;
        this.ShowListObjectTypes4CreatingObject = UISettings.ShowListObjectTypes4CreatingObject;
        this.CyrillicSimilarLatinCharacterHighlightColor = UISettings.CyrillicSimilarLatinCharacterHighlightColor;
        this.HighlightCyrillicSimilarLatinCharacters = UISettings.HighlightCyrillicSimilarLatinCharacters;
        this.HighlightLatinSimilarCyrillicCharacters = UISettings.HighlightLatinSimilarCyrillicCharacters;
        this.LatinSimilarCyrillicCharacterHighlightColor = UISettings.LatinSimilarCyrillicCharacterHighlightColor;
        this._allowableForHighlightingSimilarCharactersObjectTypes = UISettings.AllowableForHighlightingSimilarCharactersObjectTypes;
        ICreateObjByTypeMRU service = ServicesManager.GetService<ICreateObjByTypeMRU>();
        this._mruCount = service != null ? service.MaxCapacity : 10;
      }

      public UISettingsWrapper(IConfigurationManager manager)
      {
        this._manager = manager;
        manager.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.manager_ConfigurationBeforeSave);
        IConfiguration configuration = this._manager.Open("UISettings");
        this.RestoreValues();
        if (configuration == null)
          return;
        this.AskOnExit = true;
        bool result1;
        if (bool.TryParse(configuration.GetProperty(nameof (AskOnExit)), out result1))
          this.AskOnExit = result1;
        this.ShowVersionIDs = NavigatorCaptionVersionsMode.CaptionBracket;
        int result2;
        if (int.TryParse(configuration.GetProperty(nameof (ShowVersionIDs)), out result2))
          this.ShowVersionIDs = (NavigatorCaptionVersionsMode) result2;
        this.ShowShortAttributeNames = false;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowShortAttributeNames)), out result1))
          this.ShowShortAttributeNames = result1;
        this.ShowSplash = true;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowSplash)), out result1))
          this.ShowSplash = result1;
        this.NavigatorWindowCaptionsMode = NavigatorWindowCaptionsMode.Default;
        int result3;
        if (int.TryParse(configuration.GetProperty(nameof (NavigatorWindowCaptionsMode)), out result3))
          this.NavigatorWindowCaptionsMode = (NavigatorWindowCaptionsMode) result3;
        this.ShowGridChkoutColumn = true;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowGridChkoutColumn)), out result1))
          UISettings.ShowGridChkoutColumn = result1;
        this.ShowTreeChkoutColumn = false;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowTreeChkoutColumn)), out result1))
          UISettings.ShowTreeChkoutColumn = result1;
        this.NavigatorWindowBaseVersionsMode = NavigatorWindowBaseVersionsMode.ShowOtherVersions;
        int result4;
        if (int.TryParse(configuration.GetProperty(nameof (NavigatorWindowBaseVersionsMode)), out result4))
          this.NavigatorWindowBaseVersionsMode = (NavigatorWindowBaseVersionsMode) result4;
        this.NavigatorLinksMode = NavigatorLinksMode.MiddleMouseClick;
        int result5;
        if (int.TryParse(configuration.GetProperty(nameof (NavigatorLinksMode)), out result5))
          this.NavigatorLinksMode = (NavigatorLinksMode) result5;
        this.AlwaysShowFirstTab = false;
        if (bool.TryParse(configuration.GetProperty(nameof (AlwaysShowFirstTab)), out result1))
          this.AlwaysShowFirstTab = result1;
        this._mruCount = 10;
        int result6;
        if (int.TryParse(configuration.GetProperty(nameof (MRUCount)), out result6))
          this._mruCount = result6 <= 10 ? result6 : 10;
        ICreateObjByTypeMRU service1 = ServicesManager.GetService<ICreateObjByTypeMRU>();
        if (service1 != null)
          service1.MaxCapacity = this._mruCount;
        this.ShowListObjectTypes4CreatingObject = false;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowListObjectTypes4CreatingObject)), out result1))
          this.ShowListObjectTypes4CreatingObject = result1;
        UISettings.ShowVersionsLog = false;
        if (bool.TryParse(configuration.GetProperty("ShowVersionsLog"), out result1))
          UISettings.ShowVersionsLog = result1;
        if (bool.TryParse(configuration.GetProperty(nameof (SaveSelectedChildrenViewObjectFilter)), out result1))
          this.SaveSelectedChildrenViewObjectFilter = result1;
        bool result7;
        if (bool.TryParse(configuration.GetProperty(nameof (SwitchToCard)), out result7))
          this.SwitchToCard = result7;
        bool result8;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowSelectionsTabsForObjectTypes)), out result8))
          this.ShowSelectionsTabsForObjectTypes = result8;
        bool result9;
        if (bool.TryParse(configuration.GetProperty("ShowUnitesSelections"), out result9))
          this.ShowUnitedSelections = result9;
        bool result10;
        if (bool.TryParse(configuration.GetProperty(nameof (ShowFavoritesFolder)), out result10))
          this.ShowFavoritesFolder = result10;
        int result11;
        if (int.TryParse(configuration.GetProperty(nameof (CyrillicSimilarLatinCharacterHighlightColor)), NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result11))
          this.CyrillicSimilarLatinCharacterHighlightColor = Color.FromArgb(result11);
        bool result12;
        if (bool.TryParse(configuration.GetProperty(nameof (HighlightCyrillicSimilarLatinCharacters)), out result12))
          this.HighlightCyrillicSimilarLatinCharacters = result12;
        bool result13;
        if (bool.TryParse(configuration.GetProperty(nameof (HighlightLatinSimilarCyrillicCharacters)), out result13))
          this.HighlightLatinSimilarCyrillicCharacters = result13;
        int result14;
        if (int.TryParse(configuration.GetProperty(nameof (LatinSimilarCyrillicCharacterHighlightColor)), NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result14))
          this.LatinSimilarCyrillicCharacterHighlightColor = Color.FromArgb(result14);
        if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service2)
          this._allowableForHighlightingSimilarCharactersObjectTypes = UISettingsWrapper.Int32ListFromString(service2.ReadString("KERNEL", "UISettings", "HighlightingSimilarObjectTypes", "", DBConfigMode.GlobalOnly));
        this.Apply();
      }

      private void manager_ConfigurationBeforeSave(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = this._manager.Create("UISettings");
        configuration.SetProperty("AskOnExit", this.AskOnExit.ToString());
        configuration.SetProperty("ShowVersionIDs", ((int) this.ShowVersionIDs).ToString());
        configuration.SetProperty("ShowShortAttributeNames", this.ShowShortAttributeNames.ToString());
        configuration.SetProperty("ShowSplash", this.ShowSplash.ToString());
        configuration.SetProperty("NavigatorWindowCaptionsMode", ((int) this.NavigatorWindowCaptionsMode).ToString());
        configuration.SetProperty("ShowGridChkoutColumn", this.ShowGridChkoutColumn.ToString());
        configuration.SetProperty("ShowTreeChkoutColumn", this.ShowTreeChkoutColumn.ToString());
        configuration.SetProperty("NavigatorWindowBaseVersionsMode", Convert.ToInt32((object) this.NavigatorWindowBaseVersionsMode).ToString());
        configuration.SetProperty("NavigatorLinksMode", Convert.ToInt32((object) this.NavigatorLinksMode).ToString());
        configuration.SetProperty("AlwaysShowFirstTab", this.AlwaysShowFirstTab.ToString());
        configuration.SetProperty("SwitchToCard", this.SwitchToCard.ToString());
        configuration.SetProperty("SaveSelectedChildrenViewObjectFilter", this.SaveSelectedChildrenViewObjectFilter.ToString());
        configuration.SetProperty("SelectedChildrenViewObjectFilter", UISettings.SelectedChildrenViewObjectFilter.ToString());
        configuration.SetProperty("ShowSelectionsTabsForObjectTypes", this.ShowSelectionsTabsForObjectTypes.ToString());
        configuration.SetProperty("DisableChildrenViewGrouping", UISettings.DisableChildrenViewGrouping.ToString());
        configuration.SetProperty("SearchInIndexSubstring", UISettings.SearchInIndexSubstring.ToString());
        configuration.SetProperty("ShowUnitesSelections", UISettings.ShowUnitedSelections.ToString());
        configuration.SetProperty("ShowFavoritesFolder", UISettings.ShowFavoritesFolder.ToString());
        configuration.SetProperty("ShowListObjectTypes4CreatingObject", this.ShowListObjectTypes4CreatingObject.ToString());
        configuration.SetProperty("CyrillicSimilarLatinCharacterHighlightColor", this.CyrillicSimilarLatinCharacterHighlightColor.ToArgb().ToString((IFormatProvider) CultureInfo.InvariantCulture));
        configuration.SetProperty("HighlightCyrillicSimilarLatinCharacters", this.HighlightCyrillicSimilarLatinCharacters.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        configuration.SetProperty("HighlightLatinSimilarCyrillicCharacters", this.HighlightLatinSimilarCyrillicCharacters.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        configuration.SetProperty("LatinSimilarCyrillicCharacterHighlightColor", this.LatinSimilarCyrillicCharacterHighlightColor.ToArgb().ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service1 && (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
          service1.WriteString("KERNEL", "UISettings", "HighlightingSimilarObjectTypes", UISettingsWrapper.Int32ListToString(this.AllowableForHighlightingSimilarCharactersObjectTypes), 0L);
        ICreateObjByTypeMRU service2 = ServicesManager.GetService(typeof (ICreateObjByTypeMRU)) as ICreateObjByTypeMRU;
        configuration.SetProperty("MRUCount", this._mruCount.ToString());
      }

      [CustomDisplayName("Attribute.IMClient_2")]
      [CustomDescription("Attribute.IMClient_1")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool AskOnExit { [DebuggerStepThrough] get; set; }

      [CustomDescription("Attribute.IMClient_3")]
      [CustomDisplayName("Attribute.IMClient_4")]
      public NavigatorCaptionVersionsMode ShowVersionIDs { [DebuggerStepThrough] get; set; }

      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_5")]
      [CustomDisplayName("Attribute.IMClient_6")]
      public bool ShowShortAttributeNames { [DebuggerStepThrough] get; set; }

      [CustomDescription("Attribute.IMClient_9")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDisplayName("Attribute.IMClient_10")]
      public bool ShowSplash { [DebuggerStepThrough] get; set; }

      [CustomDescription("Attribute.IMClient_11")]
      [CustomDisplayName("Attribute.IMClient_12")]
      public int MRUCount
      {
        [DebuggerStepThrough] get => this._mruCount;
        set
        {
          if (value < 1)
            throw new ArgumentException(LocalizationHolder.rm.GetString("IMClient_10"));
          this._mruCount = value <= 10 ? value : throw new ArgumentException(LocalizationHolder.rm.GetString("IMClient_11"));
        }
      }

      [CustomDescription("Attribute.IMClient_37")]
      [CustomDisplayName("Attribute.IMClient_38")]
      public NavigatorWindowCaptionsMode NavigatorWindowCaptionsMode { [DebuggerStepThrough] get; set; }

      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDisplayName("Attribute.IMClient_51")]
      [CustomDescription("Attribute.IMClient_50")]
      public bool ShowGridChkoutColumn { [DebuggerStepThrough] get; set; }

      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_39")]
      [CustomDisplayName("Attribute.IMClient_40")]
      public bool ShowTreeChkoutColumn { [DebuggerStepThrough] get; set; }

      [CustomDescription("Attribute.IMClient_41")]
      [CustomDisplayName("Attribute.IMClient_42")]
      public NavigatorWindowBaseVersionsMode NavigatorWindowBaseVersionsMode { [DebuggerStepThrough] get; set; }

      [CustomDisplayName("Attribute.IMClient_44")]
      [CustomDescription("Attribute.IMClient_43")]
      public NavigatorLinksMode NavigatorLinksMode { [DebuggerStepThrough] get; set; }

      [CustomDisplayName("Attribute.IMClient_46")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_45")]
      public bool AlwaysShowFirstTab { [DebuggerStepThrough] get; set; }

      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [DisplayName("Переключать на карточку с закладки Свойства если возможно")]
      [Description("Переключать на карточку с закладки Свойства если возможно")]
      public bool SwitchToCard { get; set; }

      [Description("Выбранный в списке объектов фильтр запоминается при открытии новых окон и выходе из клиента")]
      [DisplayName("Запоминать последний открытый фильтр объектов")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool SaveSelectedChildrenViewObjectFilter { get; set; }

      [Description("Отображать закладки выборок для типов объектов")]
      [DisplayName("Отображать закладки выборок для типов объектов")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool ShowSelectionsTabsForObjectTypes { get; set; }

      [DisplayName("Объединять общие и персональные выборки")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [Description("Объединять общие и персональные выборки")]
      public bool ShowUnitedSelections { get; set; } = true;

      [CustomDescription("Attribute.IMClient_53")]
      [CustomDisplayName("Attribute.IMClient_52")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool ShowFavoritesFolder { [DebuggerStepThrough] get; set; }

      [DisplayName("Линейный список типов объектов в диалоге создания объектов")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [Description("Линейный список типов объектов в диалоге создания объектов")]
      public bool ShowListObjectTypes4CreatingObject { [DebuggerStepThrough] get; set; }

      [DisplayName("Выделять цветом одинаковые по написанию с русскими латинские буквы")]
      [Description("Выделение отображается в списках объектов в столбцах Заголовок объекта, Обозначение и Наименование.")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool HighlightCyrillicSimilarLatinCharacters { get; set; }

      [DisplayName("Цвет для выделения одинаковых по написанию с русскими латинских букв")]
      [Description("Цвет для выделения одинаковых по написанию с русскими латинских букв.")]
      [TypeConverter(typeof (ColorConverter))]
      public Color CyrillicSimilarLatinCharacterHighlightColor { get; set; } = Color.Red;

      [DisplayName("Выделять цветом одинаковые по написанию с латинскими русские буквы")]
      [Description("Выделение отображается в списках объектов в столбцах Заголовок объекта, Обозначение и Наименование.")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool HighlightLatinSimilarCyrillicCharacters { get; set; }

      [DisplayName("Цвет для выделения одинаковых по написанию с латинскими русских букв")]
      [Description("Цвет для выделения одинаковых по написанию с латинскими русских букв.")]
      [TypeConverter(typeof (ColorConverter))]
      public Color LatinSimilarCyrillicCharacterHighlightColor { get; set; } = Color.Blue;

      [Description("Для изменения этой настройки нужно обладать правами администратора.")]
      [DisplayName("Типы объектов, для которых в списках объектов работает выделение схожих букв")]
      [TypeConverter(typeof (UISettingsWrapper.ObjectTypeIdsTypeConverter))]
      [Editor(typeof (UISettingsWrapper.ObjectTypeListEditor), typeof (UITypeEditor))]
      public int[] AllowableForHighlightingSimilarCharactersObjectTypes
      {
        get => this._allowableForHighlightingSimilarCharactersObjectTypes;
        set
        {
          if (this._allowableForHighlightingSimilarCharactersObjectTypes == value)
            return;
          if (!this.IsCurrentUserAdmin())
            throw new InvalidOperationException("Для изменения этой настройки нужно обладать правами администратора");
          this._allowableForHighlightingSimilarCharactersObjectTypes = value;
        }
      }

      private bool IsCurrentUserAdmin()
      {
        return ((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).IsAdmin;
      }

      private static int[] Int32ListFromString(string int32ListAsString)
      {
        List<int> intList = new List<int>();
        if (!string.IsNullOrEmpty(int32ListAsString))
        {
          string str = int32ListAsString;
          char[] chArray = new char[1]{ ',' };
          foreach (string s in str.Split(chArray))
          {
            int result;
            if (int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
              intList.Add(result);
          }
        }
        return intList.ToArray();
      }

      private static string Int32ListToString(int[] int32List)
      {
        return string.Join(",", ((IEnumerable<int>) int32List).Select<int, string>((Func<int, string>) (int32 => int32.ToString((IFormatProvider) CultureInfo.CurrentCulture))));
      }

      private sealed class ObjectTypeIdsTypeConverter : TypeConverter
      {
        public override object ConvertTo(
          ITypeDescriptorContext context,
          CultureInfo culture,
          object value,
          System.Type destinationType)
        {
          return value != null && destinationType == typeof (string) ? (object) string.Join(", ", ((IEnumerable<int>) (int[]) value).Distinct<int>().Select<int, string>((Func<int, string>) (o =>
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(o);
            return objectType == null ? "Неопределенный тип" : objectType.ObjectTypeName;
          }))) : base.ConvertTo(context, culture, value, destinationType);
        }
      }

      private sealed class ObjectTypeListEditor : UITypeEditor
      {
        public override object EditValue(
          ITypeDescriptorContext context,
          System.IServiceProvider provider,
          object value)
        {
          using (TreeViewWithButtonsForm viewWithButtonsForm = new TreeViewWithButtonsForm())
          {
            viewWithButtonsForm.DisableGroupCheckedNodes = true;
            viewWithButtonsForm.Nodes.AddRange(this.CreateRootNodes());
            if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
              viewWithButtonsForm.ImageList = service.ImageList;
            if (!(value is int[] numArray))
              numArray = new int[0];
            int[] source = numArray;
            viewWithButtonsForm.CheckedTags = source.Cast<object>().ToList<object>();
            viewWithButtonsForm.ShowCheckedNodes();
            return viewWithButtonsForm.ShowDialog() == DialogResult.OK ? (object) viewWithButtonsForm.CheckedTags.Cast<int>().ToArray<int>() : (object) source;
          }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
          return UITypeEditorEditStyle.Modal;
        }

        private TreeNode[] CreateRootNodes()
        {
          return MetaDataHelper.GetTopObjectTypesIDs().Select<int, IMSObjectType>((Func<int, IMSObjectType>) (objectTypeId => MetaDataHelper.GetObjectType(objectTypeId))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (objectType => objectType.ObjectTypeName)).Select<IMSObjectType, TreeNode>((Func<IMSObjectType, TreeNode>) (objectType => this.CreateTreeNodeForObjectType(objectType))).ToArray<TreeNode>();
        }

        private TreeNode CreateTreeNodeForObjectType(int objectTypeID)
        {
          return this.CreateTreeNodeForObjectType(MetaDataHelper.GetObjectType(objectTypeID));
        }

        private TreeNode CreateTreeNodeForObjectType(IMSObjectType objectType)
        {
          TreeNode nodeForObjectType = new TreeNode(objectType.ObjectTypeName)
          {
            Tag = (object) objectType.ObjectTypeID
          };
          if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
            nodeForObjectType.ImageIndex = nodeForObjectType.SelectedImageIndex = service.IndexOf(4, objectType.ObjectTypeID);
          foreach (IMSObjectType objectType1 in MetaDataHelper.GetObjectTypeChildrenID(objectType.ObjectTypeID).Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)).ToArray<IMSObjectType>())
            nodeForObjectType.Nodes.Add(this.CreateTreeNodeForObjectType(objectType1));
          return nodeForObjectType;
        }
      }
    }
}
