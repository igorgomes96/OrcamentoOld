using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class FuncionarioHEsDTO
    {
        private Contexto db;
        public FuncionarioHEsDTO() { }

        public FuncionarioHEsDTO(Funcionario f, Ciclo c)
        {
            db = new Contexto();

            if (f == null || c == null) return;
            Matricula = f.Matricula;
            Nome = f.Nome;
            Cargo = f.Variaveis.Cargo.NomeCargo;

            HEs170 = new HashSet<QtdaHorasMesDTO>();
            HEs100 = new HashSet<QtdaHorasMesDTO>();
            HEs75 = new HashSet<QtdaHorasMesDTO>();
            HEs60 = new HashSet<QtdaHorasMesDTO>();
            HEs50 = new HashSet<QtdaHorasMesDTO>();
            int? qtda = 0;

            foreach (MesOrcamento m in c.MesesOrcamento.OrderBy(x => x.Mes))
            {
                HEBase he = db.HEBase.Find(f.Matricula, 170, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs170).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 170
                });

                he = db.HEBase.Find(f.Matricula, 100, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs100).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 100
                });

                he = db.HEBase.Find(f.Matricula, 75, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs75).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 75
                });

                he = db.HEBase.Find(f.Matricula, 60, m.Codigo);
                qtda = he == null ? 0 : he.QtdaHoras;
                ((HashSet<QtdaHorasMesDTO>)HEs60).Add(new QtdaHorasMesDTO
                {
                    CodMesOrcamento = m.Codigo,
                    Mes = m.Mes,
                    QtdaHoras = qtda.Value,
                    PercentualHoras = 60
                });

                he = db.HEBase.Find(f.Matricula, 50, m.Codigo);
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

        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs170 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs100 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs75 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs60 { get; set; }
        public IEnumerable<QtdaHorasMesDTO> HEs50 { get; set; }
    }
}