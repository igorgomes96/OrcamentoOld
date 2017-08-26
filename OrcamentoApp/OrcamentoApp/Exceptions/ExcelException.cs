using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class ExcelException : Exception
    {
        public string Aba { get; set; }
        public int Linha { get; set; }

        public ExcelException() : base() { }

        public ExcelException(string message) : base(message)
        {
        }

        public ExcelException(string aba, int linha) : base()
        {
            Aba = aba;
            Linha = linha;
        }

        public ExcelException(string message, string aba, int linha) : base(message)
        {
            Aba = aba;
            Linha = linha;
        }

    }
}