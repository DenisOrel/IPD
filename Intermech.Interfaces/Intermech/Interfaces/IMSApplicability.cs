
// Type: Intermech.Interfaces.IMSApplicability
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс-значение - краткая информация о применяемости
    /// </summary>
    [DebuggerDisplay("IMSApplicability: [ApplicabilityID: {ApplicabilityID}; ChildObjectTypeID: {ChildObjectTypeID}; RelationTypeID: {RelationTypeID}]")]
    [Serializable]
    public sealed class IMSApplicability : MetaDataCacheItem
    {
      /// <summary>Идентификатор контекста связи (применяемости)</summary>
      private int applicabilityID;
      /// <summary>Тип дочернего объекта</summary>
      private int childObjectTypeID;
      /// <summary>Тип родительского объекта</summary>
      private int inObjectType;
      /// <summary>Тип связи</summary>
      private int relationTypeID;
      /// <summary>
      /// Если true, то при создании версии родительского объекта нужно копировать данные связи
      /// у исходной версии объекта или у объекта-прототипа
      /// </summary>
      private bool cloneChildRelations;
      /// <summary>Извлекать ли на диск файлы по таким связям</summary>
      private bool checkoutFiles;
      /// <summary>
      /// Максимально допустимое количество таких связей. Если = Int32.MaximumValue, то бесконечное
      /// </summary>
      private int maximumLinks;
      /// <summary>
      /// Способ обрабатки удаления объектов, связанных этой связью
      /// </summary>
      private RelationConstraintModes relationConstraintMode;
      /// <summary>Свойство обязательности связи</summary>
      private ApplicabilityModes applicabilityMode;
      /// <summary>
      /// Влияет ли данная связь на содержимое родительского объекта. Если да, то при модификации
      /// атрибутов этой связи меняется дата модификации родительского объекта. Также при создании
      /// объекта по прототипу родительского объекта копируются все связи, у которых IsContent == true
      /// </summary>
      private bool isContent;
      /// <summary>Опции связи</summary>
      private ApplicabilityOptions options;
      /// <summary>
      /// Используется для определения наследования типа связи (свой тип связи или унаследованный)
      /// </summary>
      private InheritModes isPublic;

      /// <summary>Идентификатор контекста связи (применяемости)</summary>
      public int ApplicabilityID
      {
        get => this.applicabilityID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ApplicabilityID));
          this.applicabilityID = value;
        }
      }

      /// <summary>Тип дочернего объекта</summary>
      public int ChildObjectTypeID
      {
        get => this.childObjectTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ChildObjectTypeID));
          this.childObjectTypeID = value;
        }
      }

      /// <summary>Тип родительского объекта</summary>
      public int InObjectType
      {
        get => this.inObjectType;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (InObjectType));
          this.inObjectType = value;
        }
      }

      /// <summary>Тип связи</summary>
      public int RelationTypeID
      {
        get => this.relationTypeID;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RelationTypeID));
          this.relationTypeID = value;
        }
      }

      /// <summary>
      /// Если true, то при создании версии родительского объекта нужно копировать данные связи
      /// у исходной версии объекта или у объекта-прототипа
      /// </summary>
      public bool CloneChildRelations
      {
        get => this.cloneChildRelations;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (CloneChildRelations));
          this.cloneChildRelations = value;
        }
      }

      /// <summary>Извлекать ли на диск файлы по таким связям</summary>
      public bool CheckoutFiles
      {
        get => this.checkoutFiles;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (CheckoutFiles));
          this.checkoutFiles = value;
        }
      }

      /// <summary>
      /// Максимально допустимое количество таких связей. Если = Int32.MaximumValue, то бесконечное
      /// </summary>
      public int MaximumLinks
      {
        get => this.maximumLinks;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (MaximumLinks));
          this.maximumLinks = value;
        }
      }

      /// <summary>
      /// Способ обрабатки удаления объектов, связанных этой связью
      /// </summary>
      public RelationConstraintModes RelationConstraintMode
      {
        get => this.relationConstraintMode;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (RelationConstraintMode));
          this.relationConstraintMode = value;
        }
      }

      /// <summary>Свойство обязательности связи</summary>
      public ApplicabilityModes ApplicabilityMode
      {
        get => this.applicabilityMode;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (ApplicabilityMode));
          this.applicabilityMode = value;
        }
      }

      /// <summary>
      /// Влияет ли данная связь на содержимое родительского объекта. Если да, то при модификации
      /// атрибутов этой связи меняется дата модификации родительского объекта. Также при создании
      /// объекта по прототипу родительского объекта копируются все связи, у которых IsContent == true
      /// </summary>
      public bool IsContent
      {
        get => this.isContent;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (IsContent));
          this.isContent = value;
        }
      }

      /// <summary>Опции связи</summary>
      public ApplicabilityOptions Options
      {
        get => this.options;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Options));
          this.options = value;
        }
      }

      /// <summary>
      /// Используется для определения наследования типа связи (свой тип связи или унаследованный)
      /// </summary>
      public InheritModes Public
      {
        get => this.isPublic;
        internal set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (Public));
          this.isPublic = value;
        }
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IMSApplicability imsApplicability) ? base.Equals(obj) : this.ApplicabilityID == imsApplicability.ApplicabilityID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ApplicabilityID.GetHashCode();

      /// <summary>Очищает состояние объекта.</summary>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Clear()
      {
        base.Clear();
        this.ApplicabilityID = 0;
        this.RelationTypeID = -1;
        this.InObjectType = -1;
        this.ChildObjectTypeID = -1;
        this.CloneChildRelations = false;
        this.CheckoutFiles = false;
        this.MaximumLinks = 0;
        this.RelationConstraintMode = RelationConstraintModes.None;
        this.ApplicabilityMode = ApplicabilityModes.Enabled;
        this.IsContent = false;
        this.Options = ApplicabilityOptions.None;
        this.Public = InheritModes.Private;
      }

      /// <summary>
      /// Заполняет состояние текущего объекта, копируя его из указанного объекта.
      /// </summary>
      /// <param name="source">Объект-источник</param>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Assign(object source)
      {
        base.Assign(source);
        if (!(source is IMSApplicability imsApplicability))
          return;
        this.ApplicabilityID = imsApplicability.ApplicabilityID;
        this.ChildObjectTypeID = imsApplicability.ChildObjectTypeID;
        this.InObjectType = imsApplicability.InObjectType;
        this.RelationTypeID = imsApplicability.RelationTypeID;
        this.CloneChildRelations = imsApplicability.CloneChildRelations;
        this.CheckoutFiles = imsApplicability.CheckoutFiles;
        this.MaximumLinks = imsApplicability.MaximumLinks;
        this.RelationConstraintMode = imsApplicability.RelationConstraintMode;
        this.ApplicabilityMode = imsApplicability.ApplicabilityMode;
        this.IsContent = imsApplicability.IsContent;
        this.Options = imsApplicability.Options;
        this.Public = imsApplicability.Public;
      }

      /// <summary>
      /// Возвращает точную копию текущего объекта. Состояние копии объекта не будет заморожено, его можно будет изменять.
      /// </summary>
      /// <returns>Копия текущего объекта, допускающая изменение состояния объекта</returns>
      public IMSApplicability Clone() => (IMSApplicability) base.Clone();

      /// <summary>Загрузить информацию из строки таблицы</summary>
      /// <param name="row">Строка из таблицы</param>
      /// <exception cref="T:System.ArgumentNullException">Не указана строка таблицы для загрузки информации</exception>
      /// <exception cref="T:System.InvalidOperationException">Состояние объекта заморожено и не может быть изменено</exception>
      public override void Load(DataRow row)
      {
        base.Load(row);
        this.ApplicabilityID = Convert.ToInt32(row["F_APPLICABILITY_ID"]);
        this.RelationTypeID = Convert.ToInt32(row["F_RELATION_TYPE"]);
        this.InObjectType = Convert.ToInt32(row["F_INOBJECT_TYPE"]);
        this.ChildObjectTypeID = Convert.ToInt32(row["F_OBJECT_TYPE"]);
        this.CloneChildRelations = Convert.ToBoolean(row["F_CLONE_RELATIONS"]);
        this.CheckoutFiles = Convert.ToBoolean(row["F_CHKOUTFILE"]);
        this.MaximumLinks = Convert.ToInt32(row["F_MAX_LINKS"]);
        this.RelationConstraintMode = (RelationConstraintModes) Convert.ToInt32(row["F_CONSTRAINT_MODE"]);
        this.ApplicabilityMode = (ApplicabilityModes) Convert.ToInt32(row["F_MIN_LINKS"]);
        this.IsContent = Convert.ToInt32(row["F_CONTENT"]) == 1;
        this.Options = (ApplicabilityOptions) Convert.ToInt32(row["F_OPTIONS"]);
        this.Public = (InheritModes) Convert.ToInt32(row["F_PUBLIC"]);
      }
    }
}
