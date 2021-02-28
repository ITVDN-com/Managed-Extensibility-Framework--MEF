using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportAttributeCA
{
    // Интерфейс выступает в роли контракта 
    public interface IContract
    {
        void Method();
    }

    // Создаем экспорт
    [Export(typeof(IContract))]
    public class MyExport : IContract
    {
        public String data = "Test Data 2.";

        public void Method()
        {
            Console.WriteLine("Hello world");
        }
    }

    public class Importer
    {
        // Импортом является свойство
        [Import(typeof(IContract))]
        public IContract importedMember { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(typeof(MyExport).Assembly);
            CompositionContainer container = new CompositionContainer(catalog);

            Importer test2 = new Importer();

            container.ComposeParts(test2);

            test2.importedMember.Method();
            Console.ReadLine();

        }
    }
}
