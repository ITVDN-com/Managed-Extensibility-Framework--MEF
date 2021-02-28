using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ExportAttributeCA
{
    // Задаем тип контракта
    public interface IContract
    {
        void Method();
    }

    // Контракт создержит тип(IContract) и имя(MyContractName)
    [Export("MyContractName", typeof(IContract))]
    public class ExportPart : IContract
    {
        public String data = "Test Data 3.";

        public void Method()
        {
            Console.WriteLine("Hello World");
        }
    }

    public class Importer
    {
        // Задаем импорт
        [Import("MyContractName", typeof(IContract))]
        public IContract importedMember { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(typeof(ExportPart).Assembly);
            CompositionContainer container = new CompositionContainer(catalog);

            Importer test3 = new Importer();

            container.ComposeParts(test3);

            test3.importedMember.Method();
            Console.ReadLine();

        }
    }
}
