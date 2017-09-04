using OrcamentoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentoAPI.DTO
{
    public class ValoresAbertosCRCicloDTO
    {
        public ValoresAbertosCRCicloDTO() {
            Valores = new HashSet<ValoresAbertosCRDTO>();
        }
        public ValoresAbertosCRCicloDTO(CentroCusto cr, EventoFolha ev, Ciclo ciclo)
        {
            CodEvento = ev.Codigo;
            NomeEvento = ev.NomeEvento;

            Valores = new HashSet<ValoresAbertosCRDTO>();
            Contexto db = new Contexto();
            if (cr == null || ev == null || ciclo == null) return;
            
            foreach (MesOrcamento m in ciclo.MesesOrcamento.OrderBy(x => x.Mes))
            {
                ValoresAbertosCR v = db.ValoresAbertosCR.Find(ev.Codigo, m.Codigo, cr.Codigo);

                if (v == null)
                {
                    ((HashSet<ValoresAbertosCRDTO>)Valores).Add(new ValoresAbertosCRDTO
                    {
                        CodEvento = ev.Codigo,
                        CodMesOrcamento = m.Codigo,
                        CodigoCR = cr.Codigo,
                        Valor = 0,
                        Mes = m.Mes
                    });
                }
                else
                    ((HashSet<ValoresAbertosCRDTO>)Valores).Add(new ValoresAbertosCRDTO(v));
            }
            
        }

        public string CodEvento { get; set; }
        public string NomeEvento { get; set; }
        public IEnumerable<ValoresAbertosCRDTO> Valores { get; set; }
    }
}