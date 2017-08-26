using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class PerfilAcessoDTO
    {
        public PerfilAcessoDTO(PerfilAcesso p)
        {
            if (p == null) return;
            NomePerfil = p.NomePerfil;
            Descricao = p.Descricao;
        }
        public string NomePerfil { get; set; }
        public string Descricao { get; set; }
    }
}