using OrcamentoApp.Models;
using System;
using System.Collections.Generic;

namespace OrcamentoApp.DTO
{
    
    public partial class TransferenciaDTO
    {
        public TransferenciaDTO(Transferencia t)
        {
            if (t == null) return;
            CRDestino = t.CRDestino;
            FuncionarioMatricula = t.FuncionarioMatricula;
            MesTransferencia = t.MesTransferencia;
            DataSolicitacao = t.DataSolicitacao;
            Status = t.Status;
            Aprovado = t.Aprovado;
            Resposta = t.Resposta;
            CROrigem = t.CROrigem;
        }
        public string CRDestino { get; set; }
        public string FuncionarioMatricula { get; set; }
        public System.DateTime DataSolicitacao { get; set; }
        public string Status { get; set; }
        public int MesTransferencia { get; set; }
        public string CROrigem { get; set; }
        public string Resposta { get; set; }
        public Nullable<bool> Aprovado { get; set; }
    }
}
