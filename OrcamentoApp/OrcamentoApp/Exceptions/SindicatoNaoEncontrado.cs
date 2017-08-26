using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class SindicatoNaoEncontrado : ExcelException
    {
        public int CodSindicato { get; set; }

        public SindicatoNaoEncontrado(int codSindicato, string aba, int linha) : base(aba, linha)
        {
            CodSindicato = codSindicato;
        }
    }
}