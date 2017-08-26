using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class CargoNaoEncontrado : ExcelException
    {
        public int CodCargo { get; set; }

        public CargoNaoEncontrado (int codCargo, string aba, int linha) : base(aba, linha)
        {
            CodCargo = codCargo;
        }
    }
}