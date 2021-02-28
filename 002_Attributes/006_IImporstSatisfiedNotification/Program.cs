using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportAttributeCA
{
    // Класс, предоставляющий данные для события успешного импорта
    public class ImportEventArgs : EventArgs
    {
        public string ImportMessage { get; set; }

        public ImportEventArgs(string message)
        {
            ImportMessage = message;
        }
    }

    // Создаем экспорт
    [Export(typeof(ExportPart))]
    public class ExportPart
    {
        public String data = "Test Data 1.";
    }

    public class Importer : IPartImportsSatisfiedNotification
    {
        // Создаем импорт
        [Import(typeof(ExportPart))]
        public ExportPart importedMember { get; set; }

        // Событие импорта
        public event EventHandler<ImportEventArgs> ImportSatisfied;

        // Метод, инициирующий событие завершения импорта
        public void OnImportsSatisfied()
        {
            if (ImportSatisfied != null)
            {
                ImportSatisfied.Invoke(this, new ImportEventArgs("Import satisfied!"));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем каталог, способный обнаружить части в типе
            TypeCatalog catalog = new TypeCatalog(typeof(ExportPart));
            CompositionContainer container = new CompositionContainer(catalog);

            Importer importer = new Importer();

            // Обработчик события завершения импорта
            importer.ImportSatisfied += (sender, e) =>
            {
                Console.WriteLine(e.ImportMessage);
            };

            container.ComposeParts(importer);

            Console.WriteLine(importer.importedMember.data);
        }
    }
}
