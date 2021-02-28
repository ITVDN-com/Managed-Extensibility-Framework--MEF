using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportAttributeCA
{
    // Создаем экспорт
    [Export(typeof(MyExport))]
    public class MyExport
    {
        public String data = "Test Data 1.";
    }
 
    public class Importer
    {
        // Создаем импорт
        [Import(typeof(MyExport))]
        public MyExport importedMember { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем каталог, способный обнаружить части в типе
            TypeCatalog catalog = new TypeCatalog(typeof(MyExport));
            CompositionContainer container = new CompositionContainer(catalog);

            Importer importer = new Importer();

            container.ComposeParts(importer);

            Console.WriteLine(importer.importedMember.data);
        }
    }
}
