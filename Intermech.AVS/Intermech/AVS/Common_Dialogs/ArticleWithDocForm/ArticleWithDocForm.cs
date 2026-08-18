// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>
/// Форма для создания/просмотра/редактирования пары изделие/документ
/// </summary>
internal class ArticleWithDocForm : Form
{
  /// <summary>раздел справки в хелпе</summary>
  private int hlpID = 3283;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ArticleWithDocControl article;

  /// <summary>Конструктор</summary>
  /// <param name="mode">Режим открытия</param>
  public ArticleWithDocForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.hlpID);
  }

  public void Init(OpenModes mode, AVSWindow avsWindow)
  {
    if (mode == OpenModes.Create)
      this.article.Dock = DockStyle.Fill;
    this.article.Init(mode, avsWindow);
    this.ChangeMode(mode);
  }

  /// <summary>Вызывается при изменении режима открытия</summary>
  /// <param name="mode"></param>
  private void ChangeMode(OpenModes mode)
  {
    this.article.ChangeMode(mode);
    switch (mode)
    {
      case OpenModes.Create:
        this.Text = "Создание записи";
        this.hlpID = 1510;
        break;
      case OpenModes.View:
        this.Text = "Выбор записи";
        break;
      case OpenModes.CreateAdd:
        this.Text = "Выбор записи";
        break;
      case OpenModes.InView:
        this.Text = "Выбор записи";
        break;
    }
  }

  /// <summary>Открыть диалог в режиме создания</summary>
  /// <param name="avsWindow">Окно AVS из которого был вызван диалог</param>
  /// <param name="parentIDs">Идентификаторы сборок в которые добавляем создаваемую детальку</param>
  /// <param name="artType">Тип создаваемого изделия</param>
  /// <param name="docType">Тип создаваемого документа</param>
  /// <param name="relType">Тип связи</param>
  /// <param name="formType">Тип отображаемого диалога</param>
  /// <returns>CreatedPair, либо при неудаче/отмене null</returns>
  public static CreatedPair CreateDialog(
    AVSWindow avsWindow,
    List<long> parentIDs,
    int artType,
    int docType,
    int relType,
    FormType formType)
  {
    Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm articleWithDocForm = new Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleWithDocForm();
    articleWithDocForm.Init(OpenModes.Create, avsWindow);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject article = sessionKeeper.Session.GetObjectCollection(artType).Create();
      articleWithDocForm.article._articleType = artType;
      articleWithDocForm.Pair.RelationIDs = new List<long>();
      articleWithDocForm.Pair.NewRelations = true;
      articleWithDocForm.Pair.RelationType = relType;
      IDBObject document = (IDBObject) null;
      if (formType != FormType.NonDraft && formType != FormType.NonDraftB && docType != -1)
      {
        document = sessionKeeper.Session.GetObjectCollection(docType).Create();
        IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document).Create(article.ObjectID, document.ObjectID);
        if (MetaDataHelper.IsObjectTypeChildOf(docType, AvsIDCache.ObjType_Specification))
          dbRelation.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(document.ObjectID))
          });
        if (MetaDataHelper.IsObjectTypeChildOf(docType, AvsIDCache.ObjType_DetailDrawing))
          dbRelation.SetAttributesValues(new AttributeValues[2]
          {
            new AttributeValues(AvsIDCache.Attr_VersionInRelation, (object) Math.Abs(document.ObjectID)),
            new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd9609-306c-11d8-b4e9-00304f19f545"), (object) 1L)
          });
        articleWithDocForm.article._notifications.MainRelation = new DBRelationsEventArgsFromForm("RelationsCreated", dbRelation.RelationID);
        articleWithDocForm.Pair.RelationIDs.Add(dbRelation.RelationID);
      }
      List<IDBRelation> relations = new List<IDBRelation>(parentIDs.Count);
      articleWithDocForm.article._notifications.ParentRelations = new List<DBRelationsEventArgsFromForm>(parentIDs.Count);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(articleWithDocForm.Pair.RelationType);
      foreach (long parentId in parentIDs)
      {
        IDBRelation dbRelation = relationCollection.Create(parentId, article.ObjectID);
        relations.Add(dbRelation);
        articleWithDocForm.article._notifications.ParentRelations.Add(new DBRelationsEventArgsFromForm("RelationsCreated", dbRelation.RelationID));
        articleWithDocForm.Pair.RelationIDs.Add(dbRelation.RelationID);
      }
      articleWithDocForm.article._parentIDs = parentIDs;
      articleWithDocForm.article.Initialize(sessionKeeper.Session, article, document, relations, formType);
    }
    if (articleWithDocForm.ShowDialog() != DialogResult.OK)
      return (CreatedPair) null;
    articleWithDocForm.Pair.Format = articleWithDocForm.article.Format;
    articleWithDocForm.Pair.Position = articleWithDocForm.article.CommonData.Position;
    articleWithDocForm.Pair.Zona = articleWithDocForm.article.CommonData.Zona;
    articleWithDocForm.Pair.Note = articleWithDocForm.article.CommonData.Note;
    articleWithDocForm.Pair.Smotri = articleWithDocForm.article.CommonData.Smotri;
    articleWithDocForm.Pair.Podbor = articleWithDocForm.article.CommonData.Podbor;
    articleWithDocForm.Pair.Count = articleWithDocForm.article.CommonData.Count;
    return articleWithDocForm.Pair;
  }

  protected override void OnAutoSizeChanged(EventArgs e) => base.OnAutoSizeChanged(e);

  protected override void OnShown(EventArgs e) => base.OnShown(e);

  protected override void OnActivated(EventArgs e) => base.OnActivated(e);

  protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

  protected override void OnScroll(ScrollEventArgs se) => base.OnScroll(se);

  protected override void OnVisibleChanged(EventArgs e)
  {
    if (this.Visible)
    {
      this.AutoScroll = false;
      this.AutoScroll = true;
      this.AutoScrollPosition = new Point(0, 0);
      this.Article.Select();
      this.ScrollControlIntoView((Control) this.Article);
    }
    base.OnVisibleChanged(e);
  }

  protected override void OnParentChanged(EventArgs e) => base.OnParentChanged(e);

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(0, 0);
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  internal CreatedPair Pair => this.article.Pair;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.article = new ArticleWithDocControl();
    this.SuspendLayout();
    this.article.Dock = DockStyle.Fill;
    this.article.Location = new Point(0, 0);
    this.article.Name = "article";
    this.article.Size = new Size(784, 475);
    this.article.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(784, 475);
    this.Controls.Add((Control) this.article);
    this.MinimumSize = new Size(650, 500);
    this.Name = nameof (ArticleWithDocForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.ResumeLayout(false);
  }

  internal ArticleWithDocControl Article
  {
    get => this.article;
    set => this.article = value;
  }
}
