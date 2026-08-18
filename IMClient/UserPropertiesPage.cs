
// Type: IMClient.UserPropertiesPage




using IMClient.UserSessions;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using Intermech.Protection;
using Intermech.Search.CompositionContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;


namespace IMClient
{
    public class UserPropertiesPage : IPropertyPage, IPropertyPageSearchOptionEvents
    {
      private IServiceProvider _provider;
      private ClassWrapperForPropertyGrid _object;
      private UserPropertiesPage.CurrentUserProperties _userProps;

      public UserPropertiesPage(IServiceProvider provider)
      {
        this._provider = provider;
        ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("IMClient_15"), (IPropertyPage) this);
      }

      public string HelpTopicID => "1552";

      public object Control
      {
        get
        {
          if (this._object == null)
          {
            this._userProps = !(ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin ? new UserPropertiesPage.CurrentUserProperties(this._provider) : (UserPropertiesPage.CurrentUserProperties) new UserPropertiesPage.AdminCurrentUserProperties(this._provider);
            this._object = new ClassWrapperForPropertyGrid((object) this._userProps);
          }
          return (object) this._object;
        }
      }

      public void Apply()
      {
        if (this._userProps == null || !this._userProps.IsChanged())
          return;
        this._userProps.ApplyUpdates();
        this._object.ResetOldValues();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          (sessionKeeper.Session as IClientSession).ClientCache.ClearVisibleList(-1);
      }

      public void Cancel()
      {
        if (this._userProps == null)
          return;
        this._userProps._inited = false;
      }

      public PropertyPageType Type => PropertyPageType.Object;

      public string PageName => LocalizationHolder.rm.GetString("IMClient_16");

      public string HeaderText
      {
        [DebuggerStepThrough] get => this.PageName;
      }

      private void OnChanged()
      {
        if (this.Changed == null)
          return;
        this.Changed((object) this, new EventArgs());
      }

      public event EventHandler Changed;

      public List<string> GetOptionNames()
      {
        return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
      }

      private class CurrentUserProperties
      {
        protected IServiceProvider _provider;
        private string _password;
        private string _languages;
        protected SubjectAreaPropertyClass _areas;
        protected bool _showDeleted;
        protected bool _showOthers;
        private bool _developerMode;
        internal bool _inited;
        private UserPropertiesPage.CurrentUserProperties _clone;
        private CompositionContextSet _defaultCompositionContexts;

        public CurrentUserProperties(IServiceProvider provider)
        {
          this._inited = false;
          this._provider = provider;
        }

        internal bool IsChanged()
        {
          return !this._areas.Equals((object) this._clone._areas) || this._password != this._clone._password || this._languages != this._clone._languages || this._showDeleted != this._clone._showDeleted || this._showOthers != this._clone._showOthers || this._developerMode != this._clone._developerMode || this._languages != this._clone._languages || !object.Equals((object) this._defaultCompositionContexts, (object) this._clone._defaultCompositionContexts);
        }

        internal void ApplyUpdates()
        {
          bool flag = false;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IUserSession session = sessionKeeper.Session;
            if (this._languages != this._clone._languages)
            {
              session.LanguageID = this._languages;
              flag = true;
            }
            if (!this._areas.Equals((object) this._clone._areas))
              session.AreaID = this._areas.Areas;
            if (this._showDeleted != this._clone._showDeleted)
              session.ShowDeletedObjects = this._showDeleted;
            if (this._showOthers != this._clone._showOthers)
              session.ShowPersonalObjects = this._showOthers;
            if (this._password != this._clone._password)
            {
              IDBEncryptedAttribute attributeByGuid = session.GetObject(session.UserID).GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")) as IDBEncryptedAttribute;
              attributeByGuid.SetPassword(new PswPackage(this._password, attributeByGuid.CurrentCryptMethod));
            }
            try
            {
              if (this._defaultCompositionContexts != null)
                CompositionContextClientHelper.SetDefaultComposiitonContexts(this._defaultCompositionContexts);
            }
            catch
            {
            }
          }
          this._inited = false;
          ((IMClientSessionPool) ServicesManager.GetService(typeof (IMClientSessionPool))).UpdateCachedLoginPassword(this._password);
          if (!flag)
            return;
          DataHolders.Clear();
          EventsHolder.FireReloadConfiguratorTree((object) this, Guid.Empty, new EventsHolder.ReloadConfiguratorTreeArgs());
        }

        protected void CheckInited()
        {
          if (this._inited)
            return;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IUserSession session = sessionKeeper.Session;
            this._languages = session.LanguageID;
            this._areas = new SubjectAreaPropertyClass(session.AreaID);
            this._showDeleted = session.ShowDeletedObjects;
            this._showOthers = session.ShowPersonalObjects;
            this._developerMode = session.DeveloperMode;
            try
            {
              this._defaultCompositionContexts = CompositionContextClientHelper.GetDefaultCompositionContexts();
            }
            catch
            {
            }
            this._clone = (UserPropertiesPage.CurrentUserProperties) this.MemberwiseClone();
          }
          this._inited = true;
        }

        [Editor(typeof (NewPasswordEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (PasswordTypeConverter))]
        [CustomDescription("Attribute.IMClient_21")]
        [CustomDisplayName("Attribute.IMClient_22")]
        public string Password
        {
          get
          {
            this.CheckInited();
            return this._password;
          }
          set => this._password = value;
        }

        [CustomDisplayName("Attribute.IMClient_24")]
        [TypeConverter(typeof (LanguagesConverter))]
        [Editor(typeof (LanguagesTypeEditor), typeof (UITypeEditor))]
        [CustomDescription("Attribute.IMClient_23")]
        public string Languages
        {
          get
          {
            this.CheckInited();
            return this._languages;
          }
          set => this._languages = value;
        }

        [CustomDisplayName("Attribute.IMClient_26")]
        [CustomDescription("Attribute.IMClient_25")]
        public SubjectAreaPropertyClass Areas
        {
          get
          {
            this.CheckInited();
            return this._areas;
          }
        }

        [CustomDisplayName("Attribute.IMClient_32")]
        [TypeConverter(typeof (YesNoBooleanConverter))]
        [CustomDescription("Attribute.IMClient_31")]
        public bool DeveloperMode
        {
          get
          {
            this.CheckInited();
            return this._developerMode;
          }
        }

        [DisplayName("Показывать контексты состава")]
        [Description("Контексты состава по умолчанию")]
        public CompositionContextSet DefaultCompositionContexts
        {
          get
          {
            this.CheckInited();
            return this._defaultCompositionContexts;
          }
          set => this._defaultCompositionContexts = value;
        }
      }

      private class AdminCurrentUserProperties(IServiceProvider provider) : 
        UserPropertiesPage.CurrentUserProperties(provider)
      {
        [CustomDescription("Attribute.IMClient_25")]
        [CustomDisplayName("Attribute.IMClient_26")]
        public new SubjectAreaPropertyClass Areas
        {
          get
          {
            this.CheckInited();
            return this._areas;
          }
          set => this._areas = value;
        }

        [TypeConverter(typeof (YesNoBooleanConverter))]
        [CustomDescription("Attribute.IMClient_27")]
        [CustomDisplayName("Attribute.IMClient_28")]
        public bool ShowDeleted
        {
          get
          {
            this.CheckInited();
            return this._showDeleted;
          }
          set => this._showDeleted = value;
        }

        [CustomDescription("Attribute.IMClient_29")]
        [CustomDisplayName("Attribute.IMClient_30")]
        [TypeConverter(typeof (YesNoBooleanConverter))]
        public bool ShowOthers
        {
          get
          {
            this.CheckInited();
            return this._showOthers;
          }
          set => this._showOthers = value;
        }
      }
    }
}
