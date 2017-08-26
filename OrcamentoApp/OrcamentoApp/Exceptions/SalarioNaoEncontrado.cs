using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.Exceptions
{
    public class SalarioNaoEncontrado : ExcelException
    {
        public int CodigoCargo { get; set; }
        public int CargaHoraria { get; set; }
        public int CodigoEmpresa { get; set; } 
        public string NomeCidade { get; set; }

        public SalarioNaoEncontrado(int codigoCargo, int cargaHoraria, int codigoEmpresa, string nomeCidade, string aba, int linha) : base(aba, linha)
        {
            CodigoCargo = codigoCargo;
            CargaHoraria = cargaHoraria;
            CodigoEmpresa = codigoEmpresa;
            NomeCidade = nomeCidade;
        }
    }
}