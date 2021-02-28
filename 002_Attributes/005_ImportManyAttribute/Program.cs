using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportAttributeCA
{
    public interface IContract
    {
        string TestData { get; set; }
    }

    // Создаем часть
    [Export(typeof(IContract))]
    public class MyExport1 : IContract
    {
        public string TestData { get; set; }

        public MyExport1()
        {
            TestData = "Test Data 1.";
        }
    }

    [Export(typeof(IContract))]
    public class MyExport2 : IContract
    {
        public string TestData { get; set; }

        public MyExport2()
        {
            TestData = "Test Data 2.";
        }
    }

    public class Importer
    {
        // Создаем импорт
        [ImportMany(typeof(IContract))]
        public IEnumerable<IContract> importedMembers { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем каталог, способный обнаружить части в типе
            var catalog = new AssemblyCatalog(typeof(MyExport1).Assembly);
            CompositionContainer container = new CompositionContainer(catalog);

            Importer importer = new Importer();

            container.ComposeParts(importer);

            foreach (var import in importer.importedMembers)
            {
                Console.WriteLine(import.TestData);
            }
        }
    }
}
