using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class CidadeNaoEncontrada : ExcelException
    {
        public string NomeCidade { get; set; }

        public CidadeNaoEncontrada(string nomeCidade, string aba, int linha) : base(aba, linha)
        {
            NomeCidade = nomeCidade;
        }
    }
}