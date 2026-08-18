
// Type: Intermech.Files.AttachNewFileToExistingObjectConfirmation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Pools;
using Intermech.Text;
using Intermech.UI.ActionConfirmations;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Files;

public sealed class AttachNewFileToExistingObjectConfirmation : YesNoActionConfirmation
{
  private static readonly ActionConfirmationDescriptor descriptor = new ActionConfirmationDescriptor("AttachNewFileToExistingObject", "Импорт файлов", "Невозможно импортировать файл '{0}', так как в базе данных уже есть объект с таким же именем файла (ид. версии объекта = {1}). Вы хотите связать этот файл с объектом в базе данных?", (ICollection<Tuple<int, string>>) new Tuple<int, string>[2]
  {
    new Tuple<int, string>(1, "Да"),
    new Tuple<int, string>(0, "Нет")
  });

  public AttachNewFileToExistingObjectConfirmation(string filePath, long objectId)
    : base(AttachNewFileToExistingObjectConfirmation.Descriptor.Key, true)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (objectId == 0L)
      throw new ArgumentException("Идентификатор версии объекта не задан.", nameof (objectId));
    this.FilePath = filePath;
    this.ObjectId = objectId;
  }

  public string FilePath { get; private set; }

  public long ObjectId { get; private set; }

  public bool AbortUnconfirmedAction { get; set; }

  protected override string GetActionCaption() => "Импорт файлов";

  protected override string GetActionText()
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendFormat("Невозможно импортировать файл '{0}', так как в базе данных уже есть объект с таким же именем файла (ид. версии объекта = {1}). Вы хотите связать этот файл с объектом в базе данных?", (object) this.FilePath, (object) this.ObjectId);
      if (this.AbortUnconfirmedAction)
      {
        stringBuilder.Append(' ');
        stringBuilder.Append("В случае ответа 'Нет' операция будет прервана.");
      }
      return stringBuilder.ToString();
    }
  }

  internal static ActionConfirmationDescriptor Descriptor
  {
    get => AttachNewFileToExistingObjectConfirmation.descriptor;
  }
}
