using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class FuncionarioEventosDTO
    {
        private Contexto db = new Contexto();
        public FuncionarioEventosDTO(Funcionario f, Ciclo c)
        {
            MatriculaFuncionario = f.Matricula;
            NomeFuncionario = f.Nome;
            Eventos = new List<EventoCicloDTO>();

            foreach (EventoFolha e in db.EventoFolha)
            {
                EventoCicloDTO evento = new EventoCicloDTO
                {
                    CodEvento = e.Codigo,
                    NomeEvento = e.NomeEvento,
                    CodCiclo = c.Codigo
                };

                foreach (MesOrcamento m in c.MesesOrcamento)
                {
                    CalculoEventoBaseDTO calculo = new CalculoEventoBaseDTO
                    {
                        CodEvento = e.Codigo,
                        MatriculaFuncionario = MatriculaFuncionario,
                        CodMesOrcamento = m.Codigo,
                        Valor = 0
                    };
                    evento.ValoresMensais.Add(m.Codigo, calculo);
                }

                db.CalculoEventoBase.Where(x => x.MatriculaFuncionario == MatriculaFuncionario && x.CodEvento == e.Codigo && x.MesOrcamento.CicloCod == c.Codigo)
                    .ToList().ForEach(x =>
                    {
                        evento.ValoresMensais[x.CodMesOrcamento].Valor = x.Valor;
                    });

                ((List<EventoCicloDTO>)Eventos).Add(evento);
            }

        }
        public string MatriculaFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public IEnumerable<EventoCicloDTO> Eventos { get; set; }
    }
}