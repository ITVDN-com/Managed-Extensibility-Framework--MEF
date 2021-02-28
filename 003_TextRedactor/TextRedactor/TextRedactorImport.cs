using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using TextReader.Contracts;

namespace TextRedactor
{
    class ImportEventArgs : EventArgs
    {
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public ImportEventArgs(string status)
        {
            this.status = status;
        }
    }

    class ImportManager : IPartImportsSatisfiedNotification
    {
       public event EventHandler<ImportEventArgs> ImportSatisfied;

       [ImportMany(typeof(ITextRedactorExtension), AllowRecomposition=false)]
       public IEnumerable<Lazy<ITextRedactorExtension>> readerExtCollection {get; set;}

        public void OnImportsSatisfied()
        {
            if (ImportSatisfied != null)
                ImportSatisfied.Invoke(this, new ImportEventArgs("Imporst loaded!"));
        }
    }
}
