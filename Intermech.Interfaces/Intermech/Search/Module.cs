
// Type: Intermech.Search.Module
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using Intermech.Search.Configuration;
using Intermech.Search.Data;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Metadata;
using Intermech.Search.Utilities;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Search
{
    public static class Module
    {
      public static void Initialize()
      {
        ServiceLocator.Register<IAttributeValueConverter>((IAttributeValueConverter) new AttributeValueConverter());
        ServiceLocator.Register<IAttributeTypeForObjectRepository>((IAttributeTypeForObjectRepository) new AttributeTypeForObjectRepository());
        ServiceLocator.Register<IAttributeTypeForRelationRepository>((IAttributeTypeForRelationRepository) new AttributeTypeForRelationRepository());
        ServiceLocator.Register<IAttributeTypeRepository>((IAttributeTypeRepository) new AttributeTypeRepository());
        ServiceLocator.Register<IRelationObjectRepository>((IRelationObjectRepository) new RelationObjectRepository());
        ServiceLocator.Register<ITypeMetadataParser>((ITypeMetadataParser) new TypeMetadataParser());
        ServiceLocator.Register<ITypeMetadataRepository>((ITypeMetadataRepository) new TypeMetadataRepository());
        ServiceLocator.Register<IIDConverter>((IIDConverter) new IDConverter());
        ServiceLocator.Register<IConfigurationOptionInfoProvider>((IConfigurationOptionInfoProvider) new ConfigurationOptionInfoProvider());
        ServiceLocator.Register<IConfigurationOptionRepository>((IConfigurationOptionRepository) new ConfigurationOptionRepository());
        ServiceLocator.Register<IRelationRepository>((IRelationRepository) new RelationRepository());
        ServiceLocator.Register<IObjectRepository>((IObjectRepository) new ObjectRepository());
        ServiceLocator.Register<IObjectTypeApplicabilityRepository>((IObjectTypeApplicabilityRepository) new ObjectTypeApplicabilityRepository());
        ServiceLocator.Register<ICompositionRepository>((ICompositionRepository) new CompositionRepository());
        ServiceLocator.Register<ILifecycleLevelRepository>((ILifecycleLevelRepository) new LifecycleLevelRepository());
        ServiceLocator.Register<ILifecycleStepRepository>((ILifecycleStepRepository) new LifecycleStepRepository());
        ServiceLocator.Register<IObjectTypeRepository>((IObjectTypeRepository) new ObjectTypeRepository());
        ServiceLocator.Register<IRelationTypeRepository>((IRelationTypeRepository) new RelationTypeRepository());
        ServiceLocator.Register<IBlobRepository>((IBlobRepository) new BlobRepository());
        ServiceLocator.Register<ITypeProvider>((ITypeProvider) new TypeProvider());
        ServiceLocator.Get<IConfigurationOptionInfoProvider>().Register(new List<ConfigurationOptionInfo>()
        {
          new ConfigurationOptionInfo(typeof (long))
          {
            Category = "Правила подбора версий",
            DefaultValue = (object) 0L,
            Description = "Правило, которое используется в системе, как правило подбора версий по умолчанию",
            DisplayName = "Правило подбора версий по умолчанию",
            CheckAdmin = true,
            Key = ConfigurationOptionKeys.Versions_DefaultVersionRule,
            Page = "Система/Подбор версий",
            TypeConverter = typeof (ObjectLinkConverter),
            Mode = DBConfigMode.GlobalOnly
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Конкретизация версий",
            Description = "Синронный выпуск версий документов и изделий, помеченных мягкой конкретизацией",
            DisplayName = "Синхронный выпуск версий документов и изделий, помеченных мягкой конкретизацией",
            CheckAdmin = true,
            Key = ConfigurationOptionKeys.Versions_SyncReleaseInSoftMode,
            Page = "Система/Подбор версий",
            TypeConverter = typeof (YesNoBooleanConverter),
            Mode = DBConfigMode.GlobalOnly
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Конкретизация версий",
            Description = "Данный режим указывает подбору версий не удалять из составов версии объектов, которые были подобраны по конкретизации некорректно (версия, указанная на конкретизации, не была найдена). Такие версии будут отмечены специальным статусом. (включать ТОЛЬКО для диагностики) Для изменения нужны права администратора",
            DisplayName = "Отображать объекты с неправильным значением конкретизации в составах",
            Key = ConfigurationOptionKeys.Versions_ShowInvalidConcreteVersions,
            Page = "Система/Подбор версий",
            RequestAdminRights = true,
            TypeConverter = typeof (YesNoBooleanConverter)
          },
          new ConfigurationOptionInfo(typeof (Font))
          {
            Category = "Интерфейс",
            DefaultValue = (object) SystemFonts.DefaultFont,
            Description = "Шрифт дерева Навигатора",
            DisplayName = "Шрифт дерева Навигатора",
            Key = ConfigurationOptionKeys.UI_TreeFont,
            Page = "Пользователи/Интерфейс/Навигатор",
            TypeConverter = typeof (FontConverter)
          },
          new ConfigurationOptionInfo(typeof (Font))
          {
            Category = "Интерфейс",
            DefaultValue = (object) SystemFonts.DefaultFont,
            Description = "Шрифт грида Навигатора",
            DisplayName = "Шрифт грида Навигатора",
            Key = ConfigurationOptionKeys.UI_GridFont,
            Page = "Пользователи/Интерфейс/Навигатор",
            TypeConverter = typeof (FontConverter)
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Интерфейс",
            DefaultValue = (object) false,
            Description = "При включении открывает новые окна рядом с активным окном, при выключении - за уже открытыми окнами",
            DisplayName = "Открывать новые окна рядом",
            Key = ConfigurationOptionKeys.UI_OpenNearMode,
            Page = "Пользователи/Интерфейс/Навигатор",
            TypeConverter = typeof (YesNoBooleanConverter)
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Интерфейс",
            Description = "Включает режим выделения клавишей Insert, как в старом Search",
            DisplayName = "Выделение как в старом Search",
            Key = ConfigurationOptionKeys.UI_UseSearchSelectionMode,
            ImageKey = "GroupSelect_16x16.png",
            Page = "Пользователи/Интерфейс/Навигатор",
            TypeConverter = typeof (YesNoBooleanConverter)
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Минимизация",
            DefaultValue = (object) true,
            Description = "Включает режим минимизации контекстного меню",
            DisplayName = "Минимизировать контекстное меню",
            Key = ConfigurationOptionKeys.UI_MinimizeContextMenu,
            ImageKey = "MenuBlue_16x16.png",
            Page = "Пользователи/Интерфейс/Контекстное меню",
            TypeConverter = typeof (YesNoBooleanConverter)
          },
          new ConfigurationOptionInfo(typeof (long))
          {
            Category = "Минимизация",
            DefaultValue = (object) 15L,
            Description = "Количество команд в минимизированном контекстном меню",
            DisplayName = "Количество команд в минимизированном меню",
            Key = ConfigurationOptionKeys.UI_MinimizedContextMenuCommandsCount,
            Page = "Пользователи/Интерфейс/Контекстное меню",
            TypeConverter = typeof (PositiveInt64Converter)
          },
          new ConfigurationOptionInfo(typeof (bool))
          {
            Category = "Контексты редактирования",
            DefaultValue = (object) false,
            Description = "Включает режим автоматического выбора контекста редактирования, при открытии контекстной версии в новом окне",
            DisplayName = "Автоматический выбор контекста для контекстной версии",
            Key = ConfigurationOptionKeys.Versions_AutoSelectContext,
            Page = "Система/Подбор версий",
            TypeConverter = typeof (YesNoBooleanConverter)
          }
        });
      }
    }
}
