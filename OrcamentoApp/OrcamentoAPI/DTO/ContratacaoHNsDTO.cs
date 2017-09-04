using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class ContratacaoHNsDTO
    {
        private Contexto db;
        public ContratacaoHNsDTO() { }

        public ContratacaoHNsDTO(Contratacao con, Ciclo c)
        {
            db = new Contexto();

            if (con == null || c == null) return;
            Codigo = con.Codigo;
            Cargo = con.Variaveis.Cargo.NomeCargo;
            CargaHoraria = con.CargaHoraria;

            HNs20 = new HashSet<QtdaHorasMesDTO>();
            HNs30 = new HashSet<QtdaHorasMesDTO>();
            HNs40 = new HashSet<QtdaHorasMesDTO>();
            HNs60 = new HashSet<QtdaHorasMesDTO>();
            HNs50 = new HashSet<QtdaHorasMesDTO>();
            int? qtda = 0;

            foreach (MesOrcamento m in c.MesesOrcamento.OrderBy(x => x.Mes))
            {
                AdNoturnoContratacao he = db.AdNoturnoContratacao.Find(con.Codigo, m.Codigo, 20);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HNs20).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 20
                });

                he = db.AdNoturnoContratacao.Find(con.Codigo, m.Codigo, 30);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HNs30).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 30
                });

                he = db.AdNoturnoContratacao.Find(con.Codigo, m.Codigo, 40);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HNs40).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 40
                });

                he = db.AdNoturnoContratacao.Find(con.Codigo, m.Codigo, 60);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HNs60).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 60
                });

                he = db.AdNoturnoContratacao.Find(con.Codigo, m.Codigo, 50);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HNs50).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 50
                });
            }
        }

        public int Codigo { get; set; }
        public int CargaHoraria { get; set; }
        public string Cargo { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HNs20 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HNs30 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HNs40 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HNs60 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HNs50 { get; set; }
    }
}