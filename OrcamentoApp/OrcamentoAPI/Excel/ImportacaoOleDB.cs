using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using OrcamentoAPI.Exceptions;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections;
using OrcamentoAPI.Models;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace OrcamentoAPI.Excel
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
                tab = "Convênio Médico";
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
                tab = "Cargos";
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
                tab = "Filiais";
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
                tab = "Salários";
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
                    encargo.FGTS = float.Parse(r["FGTS"].ToString());
                    encargo.INSS = float.Parse(r["INSS"].ToString());
                    encargo.AvisoPrevio = float.Parse(r["Aviso Prévio"].ToString());
                    encargo.INCRA = float.Parse(r["INCRA"].ToString());
                    encargo.SalEducacao = float.Parse(r["Salário Educação"].ToString());
                    encargo.Sebrae = float.Parse(r["Sebrae"].ToString());
                    encargo.Senai = float.Parse(r["Senai"].ToString());
                    encargo.SESI = float.Parse(r["SESI"].ToString());

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


    }
}
 