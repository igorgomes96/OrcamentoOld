using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class ConvenioNaoEncontrado : ExcelException
    {
        public int CodConvenio { get; set; }

        public ConvenioNaoEncontrado(int codConvenio, string aba, int linha) : base(aba, linha)
        {
            CodConvenio = codConvenio;
        }
    }
}