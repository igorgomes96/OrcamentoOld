using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoApp.DTO
{
    public class ContratacaoEventosDTO
    {
        private Contexto db = new Contexto();
        public ContratacaoEventosDTO(Contratacao cont, Ciclo c)
        {
            EventoCicloContratacaoDTO eventoTotais = new EventoCicloContratacaoDTO
            {
                CodEvento = "Totais",
                NomeEvento = "Totais",
                CodCiclo = c.Codigo
            };

            foreach (MesOrcamento m in c.MesesOrcamento)
            {
                CalculoEventoContratacaoDTO calculoTotais = new CalculoEventoContratacaoDTO
                {
                    CodEvento = "Totais",
                    CodContratacao = cont.Codigo,
                    CodMesOrcamento = m.Codigo,
                    Valor = 0
                };
                eventoTotais.ValoresMensais.Add(m.Codigo, calculoTotais);
            }


            CodContratacao = cont.Codigo;
            NomeCargo = cont.Variaveis.Cargo.NomeCargo;
            CargaHoraria = cont.CargaHoraria;
            CidadeNome = cont.CidadeNome;
            Eventos = new List<EventoCicloContratacaoDTO>();

            foreach (EventoFolha e in db.EventoFolha)
            {
                EventoCicloContratacaoDTO evento = new EventoCicloContratacaoDTO
                {
                    CodEvento = e.Codigo,
                    NomeEvento = e.NomeEvento,
                    CodCiclo = c.Codigo
                };

                foreach (MesOrcamento m in c.MesesOrcamento)
                {
                    CalculoEventoContratacaoDTO calculo = new CalculoEventoContratacaoDTO
                    {
                        CodEvento = e.Codigo,
                        CodContratacao = cont.Codigo,
                        CodMesOrcamento = m.Codigo,
                        Valor = 0
                    };
                    evento.ValoresMensais.Add(m.Codigo, calculo);
                }

                db.CalculoEventoContratacao.Where(x => x.CodContratacao == CodContratacao && x.CodEvento == e.Codigo && x.MesOrcamento.CicloCod == c.Codigo)
                    .ToList().ForEach(x =>
                    {
                        evento.ValoresMensais[x.CodMesOrcamento].Valor = x.Valor;
                        eventoTotais.ValoresMensais[x.CodMesOrcamento].Valor += x.Valor;
                    });

                ((List<EventoCicloContratacaoDTO>)Eventos).Add(evento);
            }

            ((List<EventoCicloContratacaoDTO>)Eventos).Add(eventoTotais);

        }
        public int CodContratacao { get; set; }
        public string NomeCargo { get; set; }
        public int CargaHoraria { get; set; }
        public string CidadeNome { get; set; }
        public IEnumerable<EventoCicloContratacaoDTO> Eventos { get; set; }
    }
}