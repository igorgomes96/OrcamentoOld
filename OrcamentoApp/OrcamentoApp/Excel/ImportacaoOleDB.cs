using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using OrcamentoApp.Exceptions;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections;
using OrcamentoApp.Models;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace OrcamentoApp.Excel
{
    public class ImportacaoOleDB
    {
        private OleDbConnection conexao;
        private OleDbDataAdapter adapter;
        private Contexto db;
        private IPrincipal user = null;

        public string FileName { get; set; }
        //public int IdMes { get; set; }

        public ImportacaoOleDB() { }

        public ImportacaoOleDB(string fileName, IPrincipal user)//int idMes, IPrincipal user)
        {
            FileName = fileName;
            //IdMes = idMes;
            this.user = user;
        }

        private void Conectar()
        {
            conexao = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml; HDR = YES';");
        }

        private void Desconectar()
        {
            if (conexao.State == ConnectionState.Open) conexao.Close();
        }

        public void ExecutarImportacao()
        {
            DataSet ds;
            Conectar();
            db = new Contexto();
            string tab = "";

            try
            {
                conexao.Open();


                //ConvenioMed
                tab = "ConvenioMed";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try { 
                    adapter.Fill(ds);
                    ImportacaoConvenioMed(ds);
                    db.SaveChanges();
                } catch(OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }

                //Cargo
                tab = "Cargo";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try
                {
                    adapter.Fill(ds);
                    ImportacaoCargos(ds);
                    //db.SaveChanges();
                }
                catch (OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }

                //Encargo
                tab = "Encargos";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try
                {
                    adapter.Fill(ds);
                    ImportacaoEncargos(ds);
                    db.SaveChanges();
                }
                catch (OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }

                //Filial
                tab = "Filial";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try
                {
                    adapter.Fill(ds);
                    ImportacaoFiliais(ds);
                    //db.SaveChanges();
                }
                catch (OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }

                //Salários
                tab = "Salario";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try
                {
                    adapter.Fill(ds);
                    ImportacaoSalarios(ds);
                    //db.SaveChanges();
                }
                catch (OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }


            } 
            catch (ConvenioNaoEncontrado e)
            {
                throw new Exception("Convênio médico não cadastrado (Cód. " + e.CodConvenio + ")! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (EmpresaNaoEncontrada e)
            {
                throw new Exception("Empresa não cadastrada (" + e.CodEmpresa + ")! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (SindicatoNaoEncontrado e)
            {
                throw new Exception("Sindicato não cadastrado (Cod. " + e.CodSindicato + ")! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (CidadeNaoEncontrada e)
            {
                throw new Exception("Cidade não cadastrada (" + e.NomeCidade + ")! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (CargoNaoEncontrado e)
            {
                throw new Exception("Cargo não cadastrado (Cód. " + e.CodCargo + ")! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (CargaHorariaNaoEncontrada e)
            {
                throw new Exception("Carga Horária não cadastrada (" + e.CargaHoraria + " horas)! Aba " + e.Aba + ", linha " + e.Linha + ".");
            }
            catch (ExcelException e)
            {
                throw new Exception("Ocorreu um erro ao ler a linha " + e.Linha + " da aba " + e.Aba + "! Mensagem: " + e.Message);
            }
            catch (OleDbException e)
            {
                throw new Exception("Erro ao salvar os dados: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro inesperado! " + e.Message);
            }
            finally
            {
                Desconectar();
            }


            db.Dispose();
        }

        /*Nullable<DateTime> dataFim = null;
        if (DateTime.TryParse(r["Data Fim"].ToString(), out DateTime temp))
        dataFim = temp;*/


        private void ImportacaoSalarios(DataSet ds)
        {
            int l = 2;
            int? codCargo = null, cargaHoraria = null, codEmpresa = null;
            string nomeCidade = null;
            try
            {

                Salario salario;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //Condição de parada
                    if (r["Faixa 1"] == null || r["Faixa 1"].ToString() == "") return;
                    bool novo = false;

                    codCargo = int.Parse(r["Código do Cargo"].ToString());
                    cargaHoraria = int.Parse(r["Carga Horária"].ToString());
                    codEmpresa = int.Parse(r["Código da Empresa"].ToString());
                    nomeCidade = r["Nome da Cidade"].ToString();

                    salario = db.Salario.Find(codCargo, cargaHoraria, codEmpresa, nomeCidade);
                    if (salario == null)
                    {
                        salario = new Salario();
                        novo = true;
                    }

                    salario.CargoCod = codCargo.Value;
                    salario.CargaHoraria = cargaHoraria.Value;
                    salario.EmpresaCod = codEmpresa.Value;
                    salario.CidadeNome = nomeCidade;
                    salario.Faixa1 = float.Parse(r["Faixa 1"].ToString());
                    salario.Faixa2 = float.Parse(r["Faixa 2"].ToString());
                    salario.Faixa3 = float.Parse(r["Faixa 3"].ToString());
                    salario.Faixa4 = float.Parse(r["Faixa 4"].ToString());

                    if (novo)
                        db.Entry(salario).State = System.Data.Entity.EntityState.Added;
                    else
                        db.Entry(salario).State = System.Data.Entity.EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    } catch (Exception e)
                    {
                        if (!CargoExists(codCargo.Value)) throw new CargoNaoEncontrado(codCargo.Value, ds.DataSetName, l);
                        if (!EmpresaExists(codEmpresa.Value)) throw new EmpresaNaoEncontrada(codEmpresa.Value, ds.DataSetName, l);
                        if (!CargaHorariaExists(codEmpresa.Value)) throw new CargaHorariaNaoEncontrada(codEmpresa.Value, ds.DataSetName, l);
                        if (!CidadeExists(nomeCidade)) throw new CidadeNaoEncontrada(nomeCidade, ds.DataSetName, l);
                        throw e;
                    }

                    l++;

                }
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoConvenioMed(DataSet ds)
        {
            int l = 2;
            try
            {

                ConvenioMed convenio;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    bool novo = false;
                    if (r["Código"] == null || r["Código"].ToString() == "") {
                        convenio = new ConvenioMed();
                        novo = true;
                    } else
                    {
                        convenio = db.ConvenioMed.Find(int.Parse(r["Código"].ToString()));
                        if (convenio == null) throw new ConvenioNaoEncontrado(int.Parse(r["Código"].ToString()), ds.DataSetName, l);
                    }

                    convenio.Plano = r["Plano"].ToString();
                    convenio.Valor = float.Parse(r["Valor"].ToString());
                    convenio.ValorDependentes = float.Parse(r["Valor para Dependentes"].ToString());


                    if (novo)
                        db.Entry(convenio).State = System.Data.Entity.EntityState.Added;

                    l++;
                
                }
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoCargos(DataSet ds)
        {
            int l = 2;
            Cargo cargo = null;
            try
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    bool novo = false;
                    if (r["Código"] == null || r["Código"].ToString() == "")
                    {
                        cargo = new Cargo();
                        novo = true;
                    }
                    else
                    {
                        cargo = db.Cargo.Find(int.Parse(r["Código"].ToString()));
                        if (cargo == null) throw new CargoNaoEncontrado(int.Parse(r["Código"].ToString()), ds.DataSetName, l);
                    }

                    cargo.NomeCargo = r["Nome do Cargo"].ToString();
                    cargo.TipoCargo = r["Tipo do Cargo"].ToString();
                    cargo.Plano1Cod = int.Parse(r["Código da Opção 1 de Convênio Médico"]?.ToString());
                    cargo.Plano2Cod = int.Parse(r["Código da Opção 2 de Convênio Médico"]?.ToString());
                    cargo.Plano3Cod = int.Parse(r["Código da Opção 3 de Convênio Médico"]?.ToString());

                    if (novo)
                        db.Entry(cargo).State = System.Data.Entity.EntityState.Added;

                    db.SaveChanges();

                    l++;

                }
            }
            catch (DbUpdateException e)
            {
                if (!ConvenioExists(cargo.Plano1Cod)) throw new ConvenioNaoEncontrado(cargo.Plano1Cod.Value, ds.DataSetName, l);
                if (!ConvenioExists(cargo.Plano2Cod)) throw new ConvenioNaoEncontrado(cargo.Plano2Cod.Value, ds.DataSetName, l);
                if (!ConvenioExists(cargo.Plano3Cod)) throw new ConvenioNaoEncontrado(cargo.Plano3Cod.Value, ds.DataSetName, l);
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoEncargos(DataSet ds)
        {
            int l = 2;
            try
            {
                Encargos encargo;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["Código da Empresa"] == null || r["Código da Empresa"].ToString() == "")
                    {
                        throw new ExcelException("Código da empresa não especificado!", ds.DataSetName, l);
                    }
                    else
                    {
                        encargo = db.Encargos.Find(int.Parse(r["Código da Empresa"].ToString()));
                    }

                    if (encargo == null)
                    {
                        if (!EmpresaExists(int.Parse(r["Código da Empresa"].ToString()))) throw new EmpresaNaoEncontrada(int.Parse(r["Código da Empresa"].ToString()), ds.DataSetName, l);
                        encargo = new Encargos()
                        {
                            EmpresaCod = int.Parse(r["Código da Empresa"].ToString())
                        };
                        db.Encargos.Add(encargo);
                    }

                    encargo.Enc13 = float.Parse(r["13º Salário"].ToString());
                    encargo.Ferias = float.Parse(r["Férias"].ToString());
                    //encargo.SistemaS = float.Parse(r["Sistema S"].ToString());
                    encargo.FGTS = float.Parse(r["FGTS"].ToString());
                    encargo.INSS = float.Parse(r["INSS"].ToString());
                    encargo.AvisoPrevio = float.Parse(r["Aviso Prévio"].ToString());

                    l++;

                }
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoFiliais(DataSet ds)
        {
            int l = 2;
            Filial filial = null;
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["Código da Empresa"] == null || r["Código da Empresa"].ToString() == "")
                    {
                        throw new ExcelException("Código da empresa não especificado!", ds.DataSetName, l);
                    }
                    else if (r["Nome da Cidade"] == null || r["Nome da Cidade"].ToString() == "")
                    {
                        throw new ExcelException("Nome da Cidade não especificada!", ds.DataSetName, l);
                    }
                    else
                    {
                        filial = db.Filial.Find(int.Parse(r["Código da Empresa"].ToString()), r["Nome da Cidade"].ToString());
                    }

                    if (filial == null)
                    {
                        filial = new Filial
                        {
                            EmpresaCod = int.Parse(r["Código da Empresa"].ToString()),
                            CidadeNome = r["Nome da Cidade"].ToString()
                        };
                        db.Filial.Add(filial);
                    }

                    filial.SindicatoCod = int.Parse(r["Código do Sindicato"]?.ToString());
                    filial.FAP = float.Parse(r["FAP"].ToString());
                    filial.SAT = float.Parse(r["SAT"].ToString());

                    db.SaveChanges();

                    l++;

                }
            }
            catch (DbUpdateException e)
            {
                if (!EmpresaExists(filial.EmpresaCod)) throw new EmpresaNaoEncontrada(filial.EmpresaCod, ds.DataSetName, l);
                if (!CidadeExists(filial.CidadeNome)) throw new CidadeNaoEncontrada(filial.CidadeNome, ds.DataSetName, l);
                if (!SindicatoExists(filial.SindicatoCod)) throw new SindicatoNaoEncontrado(filial.SindicatoCod.Value, ds.DataSetName, l);
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private bool SindicatoExists(int? cod)
        {
            if (cod == null) return false;
            return db.Sindicato.Count(x => x.Codigo == cod) > 0;
        }

        private bool ConvenioExists(int? cod)
        {
            if (cod == null) return false;
            return db.ConvenioMed.Count(x => x.Codigo == cod) > 0;
        }

        private bool EmpresaExists(int? cod)
        {
            if (cod == null) return false;
            return db.Empresa.Count(x => x.Codigo == cod) > 0;
        }

        private bool CidadeExists(string nomeCidade)
        {
            return db.Cidade.Count(x => x.NomeCidade == nomeCidade) > 0;
        }

        private bool CargoExists(int codCargo)
        {
            return db.Cargo.Count(x => x.Codigo == codCargo) > 0;
        }

        private bool CargaHorariaExists(int cargaHoraria)
        {
            return db.CargaHoraria.Count(x => x.CargaHorariaCod == cargaHoraria) > 0;
        }

        /*private void ImportacaoCRsEnvio(DataSet ds)
        {
            int l = 2;
            try { 
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    if (r["Código CR"] == null || r["Código CR"].ToString() == "")
                        break;

                    if (!CRExists(r["Código CR"].ToString()))
                        throw new CRNaoEncontradoException(r["Código CR"].ToString(), ds.DataSetName, l);

                    if (db.Usuario.Find(r["Gestor do CR"].ToString()) == null)
                        throw new UsuarioNaoEncontradoException(r["Gestor do CR"].ToString(), ds.DataSetName, l);

                    if (db.CriterioRateio.Find(int.Parse(r["Código do Critério de Rateio"].ToString())) == null)
                        throw new CriterioRateioNaoEncontradoException(int.Parse(r["Código do Critério de Rateio"].ToString()), ds.DataSetName, l);

                    CREnvio cr = new CREnvio
                    {
                        CodigoCR = r["Código CR"].ToString(),
                        CGResponsavel = r["Gestor do CR"].ToString(),
                        CodigoConta = r["Código da Conta"].ToString(),
                        IdCriterioRateio = int.Parse(r["Código do Critério de Rateio"].ToString())
                    };

                    if (CREnvioExists(cr.CodigoCR))
                        db.Entry(cr).State = System.Data.Entity.EntityState.Modified;
                    else
                        db.Entry(cr).State = System.Data.Entity.EntityState.Added;

                    l++;
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoCRsPorGrupo(DataSet ds)
        {
            int l = 2;
            //db.Database.ExecuteSqlCommand("delete from CRGrupoRateio");
            try { 
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    if (r["Código do CR"] == null || r["Código do CR"].ToString() == "")
                        break;

                    CR c = db.CR.Find(r["Código do CR"].ToString());
                    if (c == null)
                        throw new CRNaoEncontradoException(r["Código do CR"].ToString(), ds.DataSetName, l);

                    else
                    {
                        GrupoRateio g = db.GrupoRateio.Find(int.Parse(r["Código do Grupo"].ToString()));
                        if (g == null)
                            throw new GrupoRateioNaoEncontradoException(int.Parse(r["Código do Grupo"].ToString()), ds.DataSetName, l);
                        else {
                            //Método Contains não está funcionando. Acho que é porque o código não é um varchar, e quando CR é recuperado, Código é truncado.
                            bool existe = false;
                            foreach (CR cr in g.CRs) { 
                                if (cr.Codigo.Trim() == c.Codigo.Trim())
                                {
                                    existe = true;
                                    break;
                                }
                            }
                            if (!existe) g.CRs.Add(c);
                        }
                    }
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    l++;
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }
        }

        private void ImportacaoValoresRateio(DataSet ds)
        {
            int l = 2;
            db.Database.ExecuteSqlCommand("delete from UnidadeRateio where IdMesRateio = {0} and IdCriterio != 1", IdMes);
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["CR"] == null || r["CR"].ToString() == "")
                        break;

                    CR c = db.CR.Find(r["CR"].ToString());
                    if (c == null)
                        throw new CRNaoEncontradoException(r["CR"].ToString(), ds.DataSetName, l);


                    if (db.CriterioRateio.Find(int.Parse(r["Código do Critério de Rateio"].ToString())) == null)
                        throw new CriterioRateioNaoEncontradoException(int.Parse(r["Código do Critério de Rateio"].ToString()), ds.DataSetName, l);

                    UnidadeRateio u = new UnidadeRateio
                    {
                        CRRecebimento = r["CR"].ToString(),
                        IdMesRateio = IdMes,
                        IdCriterio = int.Parse(r["Código do Critério de Rateio"].ToString()),
                        Qtda = float.Parse(r["Valor/Percentual"].ToString())
                    };
                    db.UnidadeRateio.Add(u);
                    l++;
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }
        }

        private void ImportacaoRateioPorGrupo(DataSet ds)
        {
            int l = 2;
            db.Database.ExecuteSqlCommand("delete from RateioPorGrupo where IdMesRateio = {0}", IdMes);
            try { 
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["CR de Envio"] == null || r["CR de Envio"].ToString() == "")
                        break;

                    if (db.GrupoRateio.Find(int.Parse(r["Grupo"].ToString())) == null)
                        throw new GrupoRateioNaoEncontradoException(int.Parse(r["Grupo"].ToString()), ds.DataSetName, l);

                    if (db.CREnvio.Find(r["CR de Envio"].ToString()) == null)
                        throw new CREnvioNaoEncontradoException(r["CR de Envio"].ToString(), ds.DataSetName, l);

                    RateioPorGrupo ra = new RateioPorGrupo
                    {
                        CREnvio = r["CR de Envio"].ToString(),
                        IdMesRateio = IdMes,
                        IdGrupo = int.Parse(r["Grupo"].ToString()),
                        Percentual = float.Parse(r["Percentual"].ToString())
                    };
                    db.RateioPorGrupo.Add(ra);
                    l++;
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }

        }

        private void ImportacaoRateioManual(DataSet ds)
        {
            int l = 2;
            bool notIsAdmin = !user.IsInRole("Administrador");
            Usuario u = null;
            if (notIsAdmin)
                u = db.Usuario.Find(user.Identity.Name);

            try { 
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["CR de Envio"] == null || r["CR de Envio"].ToString() == "")
                        break;

                    CREnvio cr = db.CREnvio.Find(r["CR de Envio"].ToString());
                    if (cr == null)
                        throw new CRNaoEncontradoException(r["CR de Envio"].ToString(), ds.DataSetName, l);

                    if (notIsAdmin) {
                        bool existe = false;
                        foreach (CREnvio c in u.CRsEnvio)
                        {
                            if (c.CodigoCR.Trim() == cr.CodigoCR.Trim())
                            {
                                existe = true;
                                break;
                            }
                        }
                        if (!existe)
                            throw new CRNaoAutorizadoException(cr.CodigoCR, ds.DataSetName, l);
                    }

                    RateioManual temp = db.RateioManual.Find(cr.CodigoCR, r["CR de Recebimento"].ToString(), IdMes);

                    if (temp == null) {
                        RateioManual ra = new RateioManual
                        {
                            CREnvio = cr.CodigoCR,
                            CRRecebimento = r["CR de Recebimento"].ToString(),
                            IdMesRateio = IdMes,
                            Percentual = float.Parse(r["Percentual"].ToString())
                        };
                        db.RateioManual.Add(ra);
                    } else
                    {
                        temp.Percentual = float.Parse(r["Percentual"].ToString());
                    }
                    l++;    
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }
        }

        private void ImportacaoDePara(DataSet ds)
        {
            int l = 2;
            db.Database.ExecuteSqlCommand("delete from PercentualRateio where IdMesRateio = {0} and (select IdCriterioRateio from CREnvio where CodigoCR = CREnvio) != 1", IdMes);
            db.Database.ExecuteSqlCommand("delete from DeParaCR where IdMesRateio = {0}", IdMes);
            try { 
            foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r["CR de Envio"] == null || r["CR de Envio"].ToString() == "")
                        break;

                    CREnvio cr = db.CREnvio.Find(r["CR de Envio"].ToString());
                    if (cr == null)
                        throw new CRNaoEncontradoException(r["CR de Envio"].ToString(), ds.DataSetName, l);

                    DeParaCR d = new DeParaCR
                    {
                        CREnvio = cr.CodigoCR,
                        CRRecebimento = r["CR de Recebimento"].ToString(),
                        IdMesRateio = IdMes
                    };
                    db.DeParaCR.Add(d);
                    l++;
                }
            } catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }
        }

        public bool CREnvioExists(string cr)
        {
            if (db == null) db = new Contexto();
            return db.CREnvio.Find(cr) != null;
        }

        public bool CRExists(string cr)
        {
            if (db == null) db = new Contexto();
            return db.CR.Find(cr) != null;
        }*/

    }
}
 