using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class ContratacaoHEsDTO
    {
        private Contexto db;
        public ContratacaoHEsDTO() {
            HEs170 = new HashSet<QtdaHorasMesDTO>();
            HEs100 = new HashSet<QtdaHorasMesDTO>();
            HEs75 = new HashSet<QtdaHorasMesDTO>();
            HEs60 = new HashSet<QtdaHorasMesDTO>();
            HEs50 = new HashSet<QtdaHorasMesDTO>();
        }

        public ContratacaoHEsDTO(Contratacao con, Ciclo c)
        {
            db = new Contexto();

            if (con == null || c == null) return;
            Cargo = con.Variaveis.Cargo.NomeCargo;
            Codigo = con.Codigo;
            CargaHoraria = con.CargaHoraria;

            HEs170 = new HashSet<QtdaHorasMesDTO>();
            HEs100 = new HashSet<QtdaHorasMesDTO>();
            HEs75 = new HashSet<QtdaHorasMesDTO>();
            HEs60 = new HashSet<QtdaHorasMesDTO>();
            HEs50 = new HashSet<QtdaHorasMesDTO>();
            int? qtda = 0;

            foreach (MesOrcamento m in c.MesesOrcamento.OrderBy(x => x.Mes))
            {
                HEContratacao he = db.HEContratacao.Find(con.Codigo, 170, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs170).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 170
                });

                he = db.HEContratacao.Find(con.Codigo, 100, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs100).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 100
                });

                he = db.HEContratacao.Find(con.Codigo, 75, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs75).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 75
                });

                he = db.HEContratacao.Find(con.Codigo, 60, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs60).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 60
                });

                he = db.HEContratacao.Find(con.Codigo, 50, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs50).Add(new QtdaHorasMesDTO
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
        public IEnumerable<QtdaHorasMesDTO> HEs170 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs100 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs75 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs60 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs50 { get; set; }
    }
}