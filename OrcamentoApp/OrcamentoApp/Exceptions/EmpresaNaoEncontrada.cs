using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class EmpresaNaoEncontrada : ExcelException
    {
        public int CodEmpresa { get; set; }

        public EmpresaNaoEncontrada(int codEmpresa, string aba, int linha) : base(aba, linha)
        {
            CodEmpresa = codEmpresa;
        }
    }
}