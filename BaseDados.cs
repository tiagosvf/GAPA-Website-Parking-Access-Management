using System;
using GAPA;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace GAPA
{
    public class BaseDados
    {
        #region LigarBD
        private static BaseDados instance;
        public static BaseDados Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaseDados();
                return instance;
            }
        }
        private string strLigacao;

        private SqlConnection ligacaoBD;
        public BaseDados()
        {
            //ligação à bd
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }

        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Funções genéricas
        //devolve consulta
        public DataTable devolveConsulta(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }
        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }


        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Transaction = transacao;
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        //executar comando
        public bool executaComando(string sql)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro.Message);
                return false;
            }
            return true;
        }

        public bool executaComando(string sql, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }
        public bool executaComando(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region Login e Genericos
        //ALTERAR PASSWORD (EDITAR UTILIZADOR)
        public void atualizarPassword(Utilizadores update)
        {
            string sql = "UPDATE Utilizadores SET password=HASHBYTES('SHA2_512',@password) WHERE id=@id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=update.id },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=update.password },
            };
            executaComando(sql, parametros);

        }
        //ALTERAR PASSWORD POR LINK DE RECUPERAÇÃO
        public void atualizarPasswordLink(Utilizadores update)
        {
            string sql = "UPDATE Utilizadores SET password=HASHBYTES('SHA2_512',@password) WHERE cod_seguranca=@cod_seguranca ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@cod_seguranca",SqlDbType=SqlDbType.VarChar,Value=update.cod_seguranca },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=update.password },
            };
            executaComando(sql, parametros);

        }
        //VERIFICAR LOGIN
        public DataTable verificarLogin(Utilizadores novo)
        {
            string sql = "SELECT * FROM Utilizadores WHERE username=@username AND ";
            sql += "password=HASHBYTES('SHA2_512',@password) AND ativo=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=novo.username },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=novo.password }
            };
            DataTable utilizador = devolveConsulta(sql, parametros);
            if (utilizador == null || utilizador.Rows.Count == 0)
                return null;
            return utilizador;
        }
        //DEFENIR UM COD DE REPOSIÇÃO DE PASS
        public void defenirRepor(Utilizadores novo)
        {
            string sql = "Update Utilizadores SET cod_seguranca=@cod_seguranca WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@email", SqlDbType = SqlDbType.VarChar, Value = novo.email },
                new SqlParameter() { ParameterName = "@cod_seguranca", SqlDbType = SqlDbType.VarChar, Value = novo.cod_seguranca },
            };
            executaComando(sql, parametros);
        }
        //VERIFICAR SE O EMAIL DE REPOSIÇÃO EXISTE
        public bool verificarEmail(Utilizadores novo)
        {
            string sql = "SELECT * FROM Utilizadores WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@email", SqlDbType = SqlDbType.VarChar, Value = novo.email },
            };
            DataTable teste = devolveConsulta(sql, parametros);
            if (teste.Rows.Count == 0 || teste.Rows == null)
                return false;
            else
                return true;

        }

        #endregion

        #region utilizadores
        //ADICIONAR UTILIZADOR
        public int adicionarUtilizador(Utilizadores add)
        {
            string sql = "INSERT INTO utilizadores(username,email,nome,password,tipo,ativo,perfil) ";
            sql += "VALUES (@username,@email,@nome,HASHBYTES('SHA2_512',@password),@tipo,@ativo,@perfil);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=add.username },
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=add.email },
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=add.nome },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=add.password },
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value=add.tipo},
                new SqlParameter() {ParameterName="@ativo",SqlDbType=SqlDbType.Bit,Value=add.ativo },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=add.perfil },
            };

            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }

        public void atualizarPerfil(Utilizadores update)
        {
            string sql = "UPDATE Utilizadores SET perfil=@perfil WHERE id=@id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=update.id },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=update.perfil },
            };
            executaComando(sql, parametros);

        }

        public List<Eventos> listarDadosEventosUtilizador(int id)
        {
            string sql = "SELECT TOP 50 Eventos.id,Eventos.tipo,Eventos.id_utilizador,Eventos.data_hora,Eventos.descricao,Eventos.portao,Portoes.nome,Utilizadores.nome FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador WHERE Eventos.id_utilizador=@id ORDER BY Eventos.id DESC";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}

            };
            DataTable dados = devolveConsulta(sql, parametros);
            List<Eventos> Lista = new List<Eventos>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Eventos novo = new Eventos();
                novo.id = int.Parse(dados.Rows[i][0].ToString());
                novo.tipo = dados.Rows[i][1].ToString();
                novo.id_utilizador = int.Parse(dados.Rows[i][2].ToString());
                novo.data_hora = DateTime.Parse(dados.Rows[i][3].ToString());
                novo.descricao = dados.Rows[i][4].ToString();
                novo.portao = int.Parse(dados.Rows[i][5].ToString());
                novo.nome_portao = dados.Rows[i][6].ToString();
                novo.nome_utilizador = dados.Rows[i][7].ToString();
                Lista.Add(novo);
            }
            return Lista;

        }

        public string devolveTipoUtilizador(int id)
        {
            string sql = "Select tipo from utilizadores where id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            DataTable dados = devolveConsulta(sql, parametros);

            return dados.Rows[0][0].ToString();
        }



        public int devolveIdUsername(string username)
        {
            string sql = "SELECT id FROM Utilizadores WHERE username=@username";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=username }
            };
            DataTable utilizador = devolveConsulta(sql, parametros);
            if (utilizador == null || utilizador.Rows.Count == 0)
                return 2;
            return int.Parse(utilizador.Rows[0][0].ToString());
        }
        //DEVOLVE NOME UTILIZADOR
        public string devolveUsernameUtilizador(int id)
        {
            string sql = "SELECT username FROM Utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.VarChar,Value=id }
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            string username = (string)comando.ExecuteScalar();
            return username;
        }
        //DELETE UTILIZADOR
        public void deleteUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id},
            };
            executaComando(sql, parametros);
        }
        //EDITAR UTILIZADOR
        public void atualizarUtilizador(Utilizadores update)
        {
            string sql = "UPDATE Utilizadores SET username=@username,email=@email,nome=@nome,tipo=@tipo,ativo=@ativo,perfil=@perfil WHERE id=@id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=update.id },
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=update.username },
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=update.email },
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=update.nome },
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value=update.tipo},
                new SqlParameter() {ParameterName="@ativo",SqlDbType=SqlDbType.Bit,Value=update.ativo },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=update.perfil },
            };
            executaComando(sql, parametros);

        }

        //BUSCAR DADOS UTILIZADOR
        public DataTable devolveDadosUtilizador()
        {
            string sql = "SELECT Utilizadores.ID,Utilizadores.nome,Utilizadores.Email,Utilizadores.Tipo,Utilizadores.Ativo,Utilizadores.Username,Perfis.Perfil FROM utilizadores INNER JOIN Perfis ON Utilizadores.perfil=Perfis.id";

            DataTable dados = devolveConsulta(sql);
            return dados;
        }
        public DataTable devolveDadosUtilizador(int id)
        {
            string sql = "SELECT Utilizadores.id,Utilizadores.nome,Utilizadores.emaiL,Utilizadores.tipo,Utilizadores.ativo,Utilizadores.username,Perfis.perfil,Utilizadores.perfil AS Id_Perfil FROM utilizadores INNER JOIN Perfis ON Utilizadores.perfil=Perfis.id Where Utilizadores.id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.VarChar,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        //PESQUISAR UTILIZADOR
        public DataTable pesquisarUtilizador(string text)
        {

            string sql = "SELECT Utilizadores.id,Utilizadores.nome,Utilizadores.emaiL,Utilizadores.tipo,Utilizadores.ativo,Utilizadores.username,Perfis.perfil FROM utilizadores INNER JOIN Perfis ON Utilizadores.perfil=Perfis.id WHERE Utilizadores.nome like @text or Utilizadores.id like @text or Utilizadores.email like @text or Utilizadores.username Like @text";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"}
            };
            return devolveConsulta(sql, parametros);
        }
        //VERIFICAR SE UTILIZADOR EXISTE NA BASE DE DADOS
        public bool verificarUsername(string username, int id)
        {
            string sql = "SELECT * from Utilizadores where username=@username and id<>@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=username}
            };
            DataTable dados = devolveConsulta(sql, parametros);
            if (dados.Rows == null || dados.Rows.Count == 0)
                return false;
            else
                return true;
        }
        //VERIFICAR SE EMAIL EXISTE NA BASE DE DADOS
        public bool verificarEmail(string email, int id)
        {
            string sql = "SELECT * from Utilizadores where email=@email and id<>@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email}
            };
            DataTable dados = devolveConsulta(sql, parametros);
            if (dados.Rows == null || dados.Rows.Count == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Eventos
        //ADICIONAR EVENTO
        public int adicionarEventos(Eventos add)
        {
            string sql = "";
            if (add.portao != -1)
            {
                sql = "INSERT INTO Eventos(tipo,id_utilizador,descricao,portao) ";
                sql += "VALUES (@tipo,@id_utilizador,@descricao,@portao);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            }
            else
            {
                sql = "INSERT INTO Eventos(tipo,id_utilizador,descricao) ";
                sql += "VALUES (@tipo,@id_utilizador,@descricao);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            }
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value=add.tipo },
                new SqlParameter() {ParameterName="@id_utilizador",SqlDbType=SqlDbType.Int,Value=add.id_utilizador },
                new SqlParameter() {ParameterName="@descricao",SqlDbType=SqlDbType.VarChar,Value=add.descricao },
                new SqlParameter() {ParameterName="@portao",SqlDbType=SqlDbType.Int,Value=add.portao},
            };

            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }
        //BUSCAR DADOS EVENTOS
        public DataTable devolveDadosEventos(int id)
        {
            string sql = "SELECT * FROM Eventos WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        //LISTAR EVENTOS
        public List<Eventos> listarDadosEventos()
        {
            string sql = "SELECT TOP 50 Eventos.id,Eventos.tipo,Eventos.id_utilizador,Eventos.data_hora,Eventos.descricao,Eventos.portao,Portoes.nome,Utilizadores.nome FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador ORDER BY Eventos.id DESC";
            DataTable dados = devolveConsulta(sql);
            List<Eventos> Lista = new List<Eventos>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Eventos novo = new Eventos();
                novo.id = int.Parse(dados.Rows[i][0].ToString());
                novo.tipo = dados.Rows[i][1].ToString();
                novo.id_utilizador = int.Parse(dados.Rows[i][2].ToString());
                novo.data_hora = DateTime.Parse(dados.Rows[i][3].ToString());
                novo.descricao = dados.Rows[i][4].ToString();
                novo.portao = int.Parse(dados.Rows[i][5].ToString());
                novo.nome_portao = dados.Rows[i][6].ToString();
                novo.nome_utilizador = dados.Rows[i][7].ToString();
                Lista.Add(novo);
            }
            return Lista;

        }
        //Lista os ultimos 5 eventos
        public DataTable lista5Eventos(int id)
        {
            string sql = "SELECT TOP 5 Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Eventos.Descricao,Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador WHERE Utilizadores.id=@id ORDER BY Eventos.id DESC";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }

        public DataTable listarDataTableEventos()
        {
            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Eventos.descricao AS [Descrição] ,Eventos.portao AS [ID Portão],Portoes.nome AS [Portão],Utilizadores.nome AS [Utilizador] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador ORDER BY Eventos.id DESC";
            DataTable dados = devolveConsulta(sql);
            return dados;
        }
        public DataTable listarDataTableEventosMobile()
        {
            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador ORDER BY Eventos.id DESC";
            DataTable dados = devolveConsulta(sql);
            return dados;
        }
        public DataTable listarDataTableEventos(int id)
        {
            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Eventos.descricao AS [Descrição], Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador WHERE Utilizadores.id=@id ORDER BY Eventos.id DESC";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        public DataTable listarDataTableEventosMobile(int id)
        {
            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora], Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador WHERE Utilizadores.id=@id ORDER BY Eventos.id DESC";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }


        //PESQUISAR EVENTOS
        public DataTable pesquisarEventos(string text)
        {

            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Eventos.descricao AS [Descrição],Eventos.portao AS [ID Portão],Portoes.nome AS [Portão],Utilizadores.nome AS [Utilizador]  FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador  WHERE eventos.tipo like @text OR eventos.descricao like @text OR eventos.data_hora like @text OR eventos.portao like @text OR portoes.nome like @text OR utilizadores.nome like @text OR eventos.matricula like @text";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"}
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable pesquisarEventosMobile(string text)
        {

            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador  WHERE eventos.tipo like @text OR eventos.descricao like @text OR eventos.data_hora like @text OR eventos.portao like @text OR portoes.nome like @text OR utilizadores.nome like @text";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"}
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable pesquisarEventos(string text, int id)
        {

            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Eventos.descricao AS [Descrição],Portoes.nome AS [Portão],Utilizadores.id as [Id_utilizador] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador  WHERE (eventos.tipo like @text OR eventos.descricao like @text OR eventos.data_hora like @text OR eventos.portao like @text OR portoes.nome like @text OR utilizadores.nome like @text) AND Eventos.id_utilizador=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"},
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable pesquisarEventosMobile(string text, int id)
        {

            string sql = "SELECT Eventos.ID,Eventos.Tipo,Eventos.data_hora AS [Data e Hora],Portoes.nome AS [Portão] FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador  WHERE (eventos.tipo like @text OR eventos.descricao like @text OR eventos.data_hora like @text OR eventos.portao like @text OR portoes.nome like @text OR utilizadores.nome like @text) AND Eventos.id_utilizador=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"},
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            return devolveConsulta(sql, parametros);
        }
        //DADOS TABLE EVENTOS
        public DataTable devolveDadosEvento(int id)
        {
            string sql = "SELECT Eventos.id,Eventos.tipo,Eventos.id_utilizador,Eventos.data_hora,Eventos.descricao,Eventos.portao,Portoes.nome,Utilizadores.nome, Eventos.matricula FROM Eventos INNER JOIN Portoes ON Eventos.portao = Portoes.id INNER JOIN Utilizadores ON Utilizadores.id = Eventos.id_utilizador WHERE Eventos.id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            return devolveConsulta(sql, parametros);
        }
        //DELETE EVENTO
        public bool removerEvento(int id)
        {
            string sql = "DELETE FROM Eventos WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}

            };
            return executaComando(sql, parametros);
        }
        #endregion

        public void definirMatricula(string novo, int id)
        {
            string sql = "Update Eventos SET matricula=@matricula WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id },
                new SqlParameter() { ParameterName = "@matricula", SqlDbType = SqlDbType.VarChar, Value = novo }
            };
            executaComando(sql, parametros);
        }

        #region Permissoes
        //ADICIONAR PERMISSÃO
        public int adicionarPermissao(Permissoes add)
        {
            string sql = "INSERT INTO Permissoes(id_perfil,portao) ";
            sql += "VALUES (@id_perfil,@portao);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id_perfil",SqlDbType=SqlDbType.Int,Value=add.id_perfil },
                new SqlParameter() {ParameterName="@portao",SqlDbType=SqlDbType.VarChar,Value=add.portao },

            };

            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }
        //MOSTRAR DADOS PERMISSÃO
        public DataTable devolveDadosPermissoes(int id)
        {
            string sql = "SELECT * FROM Permissoes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        //DELETE PERMISSAO
        public bool removerPermissao(Permissoes permissao)
        {
            string sql = "DELETE FROM Permissoes WHERE id_perfil=@id_perfil AND portao=@portao";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id_perfil",SqlDbType=SqlDbType.Int,Value=permissao.id_perfil},
                new SqlParameter() {ParameterName="@portao", SqlDbType=SqlDbType.Int, Value=permissao.portao}
            };
            return executaComando(sql, parametros);
        }
        #endregion

        #region Portões
        //MOSTRAR DADOS PORTÔES
        public DataTable devolveDadosPortoes(int id)
        {
            string sql = "SELECT * FROM Portoes WHERE id=@id and ativo=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        //DEVOLVE NOME Portao
        public string devolveNomePortao(int id)
        {
            string sql = "SELECT nome FROM Portoes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.VarChar,Value=id }
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            string username = (string)comando.ExecuteScalar();
            return username;
        }
        public DataTable devolveDadosPortoes(int id, int id_utilizador, string password)
        {
            string sql = "SELECT Portoes.id, Portoes.nome, Portoes.ip, Portoes.captura, Portoes.camera, Portoes.ativo FROM Portoes INNER JOIN Permissoes ON Portoes.id = Permissoes.portao INNER JOIN Perfis on perfis.id = permissoes.id_perfil INNER JOIN Utilizadores ON perfis.id = utilizadores.perfil WHERE Portoes.id=@id and portoes.ativo=1 AND utilizadores.id=@id_utilizador and utilizadores.cod_sessao=@password";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
                 new SqlParameter() {ParameterName="@id_utilizador",SqlDbType=SqlDbType.Int,Value=id_utilizador },
                 new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.NVarChar,Value=password }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        //LISTAR PORTÔES
        public List<Portoes> listarDadosPortoes()
        {
            string sql = "SELECT * FROM Portoes WHERE ativo=1";
            DataTable dados = devolveConsulta(sql);
            List<Portoes> Portao = new List<Portoes>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Portoes novo = new Portoes();
                novo.id = int.Parse(dados.Rows[i]["id"].ToString());
                novo.nome = dados.Rows[i]["nome"].ToString();
                novo.ip = dados.Rows[i]["ip"].ToString();
                Portao.Add(novo);
            }
            return Portao;
        }

        public List<Portoes> listarDadosPortoesUser(int id)
        {
            string sql = "SELECT Portoes.id, Portoes.nome, Portoes.ip, Portoes.captura, Portoes.camera, Portoes.ativo FROM Portoes INNER JOIN Permissoes ON Permissoes.portao = Portoes.id INNER JOIN Perfis ON Permissoes.id_perfil = Perfis.id INNER JOIN Utilizadores ON Utilizadores.perfil = Perfis.id WHERE Portoes.ativo = 1 AND Permissoes.portao = Portoes.id AND Utilizadores.id = @id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            List<Portoes> Portao = new List<Portoes>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Portoes novo = new Portoes();
                novo.id = int.Parse(dados.Rows[i][0].ToString());
                novo.nome = dados.Rows[i][1].ToString();
                novo.ip = dados.Rows[i][2].ToString();
                Portao.Add(novo);
            }
            return Portao;
        }

        public DataTable listarDataTablePortoes()
        {
            string sql = "SELECT * FROM Portoes";
            DataTable dados = devolveConsulta(sql);

            return dados;
        }

        public DataTable listarPortoesUser(int id)
        {
            string sql = "SELECT Portoes.id, Portoes.nome, Portoes.ip, Portoes.captura, Portoes.camera, Portoes.ativo FROM Portoes INNER JOIN Permissoes ON Permissoes.portao = Portoes.id INNER JOIN Perfis ON Permissoes.id_perfil = Perfis.id INNER JOIN Utilizadores ON Utilizadores.perfil = Perfis.id WHERE Portoes.ativo = 1 AND Permissoes.portao = Portoes.id AND Utilizadores.id = @id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            return devolveConsulta(sql, parametros);

        }
        //DEVOLVE IP
        public DataTable devolveIPPortao(int id)
        {
            string sql = "SELECT ip from Portoes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            return devolveConsulta(sql, parametros);
        }
        //DEVOLVE CHANNEL
        public DataTable devolveChannelPortao(int id)
        {
            string sql = "SELECT camera from Portoes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            return devolveConsulta(sql, parametros);
        }
        //PESQUISAR PORTOES
        public DataTable pesquisarPortoes(string text)
        {

            string sql = "SELECT * FROM Portoes  WHERE nome like @text";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@text",SqlDbType=SqlDbType.Text,Value="%"+text+"%"}
            };
            return devolveConsulta(sql, parametros);
        }
        //EDITAR PORTOES
        public void atualizarPortao(Portoes portao)
        {
            string sql = "UPDATE Portoes SET nome=@nome,ip=@ip,captura=@captura,camera=@camera,ativo=@ativo WHERE id=@id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@ip",SqlDbType=SqlDbType.VarChar,Value=portao.ip },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=portao.id },
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=portao.nome },
                new SqlParameter() {ParameterName="@camera",SqlDbType=SqlDbType.Int,Value=portao.camera },
                new SqlParameter() {ParameterName="@captura",SqlDbType=SqlDbType.Bit,Value=portao.captura },
                new SqlParameter() {ParameterName="@ativo",SqlDbType=SqlDbType.Bit,Value=portao.ativo},

            };
            executaComando(sql, parametros);

        }
        //DELETE PORTOES
        public bool removerPortao(int id)
        {
            string sql = "DELETE FROM Portoes WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}

            };
            return executaComando(sql, parametros);
        }
        //ADICIONAR PORTÃO
        public int adicionarPortao(Portoes add)
        {
            string sql = "INSERT INTO Portoes(nome,ip,captura,camera,ativo) ";
            sql += "VALUES (@nome,@ip,@captura,@camera,@ativo);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=add.nome },
                new SqlParameter() {ParameterName="@ip",SqlDbType=SqlDbType.VarChar,Value=add.ip },
                 new SqlParameter() {ParameterName="@captura",SqlDbType=SqlDbType.Bit,Value=add.captura },
                new SqlParameter() {ParameterName="@camera",SqlDbType=SqlDbType.Int,Value=add.camera },
                 new SqlParameter() {ParameterName="@ativo",SqlDbType=SqlDbType.Bit,Value=add.ativo },


            };

            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }
        #endregion

        #region Perfis
        //LISTAR DADOS DOS PERFIS
        public List<Perfis> listarDadosPerfis()
        {
            string sql = "SELECT * FROM Perfis";
            DataTable dados = devolveConsulta(sql);
            List<Perfis> perfis = new List<Perfis>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Perfis novo = new Perfis();
                novo.id = int.Parse(dados.Rows[i]["id"].ToString());
                novo.perfil = dados.Rows[i]["perfil"].ToString();
                perfis.Add(novo);
            }
            return perfis;
        }
        public bool verificarperfis(int id_perfil)
        {
            string sql = "SELECT * FROM Perfis INNER JOIN Utilizadores ON utilizadores.perfil=perfis.id WHERE Utilizadores.perfil=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_perfil },
            };
            DataTable dados = devolveConsulta(sql, parametros);
            if (dados.Rows == null || dados.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        public DataTable listaPermissoesPerfis(int id_perfil)
        {
            string sql = "SELECT Portoes.id, Portoes.nome FROM Portoes INNER JOIN Permissoes ON Permissoes.portao = Portoes.id INNER JOIN Perfis ON Permissoes.id_perfil = Perfis.id WHERE Perfis.id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_perfil },
              };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        public DataTable listaUsersPerfis(int id_perfil)
        {
            string sql = "SELECT Utilizadores.id,Utilizadores.nome, Utilizadores.username FROM Utilizadores INNER JOIN Perfis ON Perfis.id = Utilizadores.perfil WHERE Perfis.id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_perfil },
              };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }

        public DataTable listaPerfis()
        {
            string sql = "SELECT ID, Perfil From Perfis";


            DataTable dados = devolveConsulta(sql);
            return dados;
        }
        //DELETE PERFIL
        public bool removerPerfil(int id)
        {
            string sql = "DELETE FROM Perfis WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}

            };
            return executaComando(sql, parametros);
        }
        //ADD PERFIL
        public int adicionarPerfil(string nome)
        {
            string sql = "INSERT INTO Perfis(perfil) ";
            sql += "VALUES (@perfil);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.VarChar,Value=nome },


            };

            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }
        #endregion

        #region Anomalias
        public DataTable devolveHorarioAnomalias(string tipo_evento)
        {
            string sql = "SELECT hora_inicial, hora_final FROM Anomalias WHERE tipo_evento=@tipo_evento";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@tipo_evento",SqlDbType=SqlDbType.NVarChar,Value=tipo_evento}
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        public DataTable devolveAnomalias()
        {
            string sql = "SELECT ID,hora_inicial as [Hora Inicial], hora_final as [Hora Final], tipo_evento as [Tipo] FROM Anomalias";
            
            DataTable dados = devolveConsulta(sql);
            return dados;
        }
        public void atualizarAnomalia(Alertas_class alerta)
        {
            string sql = "UPDATE Anomalias SET hora_inicial=@hinicial,hora_final=@hfinal,tipo_evento=@tevento WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=alerta.id },
                new SqlParameter() {ParameterName="@hinicial",SqlDbType=SqlDbType.Time,Value=alerta.hora_inicial },
                new SqlParameter() {ParameterName="@hfinal",SqlDbType=SqlDbType.Time,Value=alerta.hora_final },
                new SqlParameter() {ParameterName="@tevento",SqlDbType=SqlDbType.VarChar,Value=alerta.Tipo_evento },

            };
            executaComando(sql, parametros);
        }
            #endregion

        #region Segurança Sessão
            public void defenirCod_sessao(Utilizadores novo)
        {
            string sql = "Update Utilizadores SET cod_sessao=@cod_sessao WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = novo.id },
                new SqlParameter() { ParameterName = "@cod_sessao", SqlDbType = SqlDbType.VarChar, Value = novo.cod_sessao }
            };
            executaComando(sql, parametros);
        }
        public string devolverCod_sessao(int id)
        {
            string sql = "Select cod_sessao FROM Utilizadores WHERE id=@id_utilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@id_utilizador", SqlDbType = SqlDbType.Int, Value = id },
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            string cod_seguranca = (string)comando.ExecuteScalar();
            return cod_seguranca;
        }
        public bool verificarCod_sessao(Utilizadores novo)
        {
            string sql = "SELECT * FROM Utilizadores WHERE id=@id AND cod_sessao=@cod_sessao";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = novo.id },
                new SqlParameter() { ParameterName = "@cod_sessao", SqlDbType = SqlDbType.VarChar, Value = novo.cod_sessao }
            };
            DataTable teste = devolveConsulta(sql, parametros);
            if (teste.Rows.Count == 0 || teste.Rows == null)
                return false;
            else
                return true;

        }
        #endregion


    }

}