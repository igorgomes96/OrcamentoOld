using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class CargaHorariaNaoEncontrada : ExcelException
    {
        public int CargaHoraria { get; set; }

        public CargaHorariaNaoEncontrada(int carggaHoraria, string aba, int linha) : base(aba, linha)
        {
            CargaHoraria = carggaHoraria;
        }
    }
}