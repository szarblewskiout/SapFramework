using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SapFramework.Connections;
using SapFramework.dotNET.Atributos;

namespace SapFramework
{
    public class RepositoryBaseNoObject<TEntity> : IDisposable, IRepositoryBaseNoObject<TEntity>
        where TEntity : class, new()
    {
        

        public string QuerySelectEntity(TEntity obj)
        {

            Dictionary<string, string> lista = new Dictionary<string, string>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        lista.Add("U_" + flAttribute.Nome, flAttribute.Descricao);

                    }

                }
            }

            string campos = "";

            int controle = 1;
            foreach (KeyValuePair<string, string> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key + " as '" + objetos.Value + "'";
                }
                else
                {
                    campos = campos + "," + objetos.Key + " as '" + objetos.Value + "'";

                }

                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }



            return string.Format("select Code, Name, {0} from [@{1}]", campos, nomeTabela);
        }

        public void Add(TEntity obj)
        {
            Dictionary<string, object> lista = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        if (info.GetValue(obj) != null)
                        {
                            if (info.GetValue(obj).GetType() == typeof (DateTime))
                            {
                                lista.Add("U_" + flAttribute.Nome, ((DateTime) info.GetValue(obj)).ToString("yyyyMMdd"));
                            }
                            else
                            {
                                lista.Add("U_" + flAttribute.Nome, info.GetValue(obj));
                            }

                        }



                    }

                }
            }


            string campos = "";
            string valores = "";

            int controle = 1;
            foreach (KeyValuePair<string, object> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key;
                    valores = "'" + objetos.Value + "'";
                }
                else
                {
                    campos = campos + "," + objetos.Key;
                    valores = valores + "," + "'" + objetos.Value + "'";
                }
                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }

            string key = Guid.NewGuid().ToString().Substring(0, 30);
            string query = string.Format(@"insert into [@{0}] (Code, Name, {1}) values ('{3}','{3}',{2})", nomeTabela,
                campos, valores, key);
            B1AppDomain.RSQuery(query);


        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TEntity GetByEntity(TEntity obj)
        {


            Dictionary<string, object> lista = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        lista.Add("U_" + flAttribute.Nome, info.GetValue(obj));

                    }

                }
            }

            string campos = "";
            string where = "";

            int controle = 1;
            foreach (KeyValuePair<string, object> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key;
                    where = objetos.Key + " = '" + objetos.Value.ToString() + "'";
                   
                }
                else
                {
                    campos = campos + "," + objetos.Key;
                    if (objetos.Value != null)
                    {
                        if (objetos.Value.ToString().Length < 254)
                        {
                            if (objetos.Value.GetType() == typeof (DateTime))
                            {
                                DateTime datavalor = Convert.ToDateTime(objetos.Value);
                                DateTime datavazia = Convert.ToDateTime("01/01/0001 00:00:00");
                                if (datavalor > datavazia)
                                {
                                    where = where + " and " + objetos.Key + " = '" + Convert.ToDateTime(objetos.Value).ToString("yyyyMMdd") + "'";
                                }
                                
                            }else if (objetos.Value.GetType() == typeof(int))
                            {
                                if (Convert.ToInt32(objetos.Value) != 0)
                                {
                                    where = where + " and " + objetos.Key + " = '" + objetos.Value.ToString() + "'";
                                }
                            }
                            else
                            {
                                where = where + " and " + objetos.Key + " = '" + objetos.Value.ToString() + "'";
                            }
                            
                        }
                        
                    }


                }

                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }


            string query = string.Format("select Code, Name, {0} from [@{1}] where {2}", campos, nomeTabela, where);


            var oRs = B1AppDomain.RSQuery(query);







            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    info.SetValue(obj, oRs.Fields.Item("Code").Value);
                }
                else if (info.Name == "Name")
                {
                    info.SetValue(obj, oRs.Fields.Item("Name").Value);
                }
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;


                        info.SetValue(obj, oRs.Fields.Item("U_" + flAttribute.Nome).Value);



                    }

                }

            }


            return obj;



        }

        public List<TEntity> GetAll()
        {

            TEntity obj = new TEntity();

            Dictionary<string, object> lista = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        lista.Add("U_" + flAttribute.Nome, info.GetValue(obj));

                    }

                }
            }

            string campos = "";

            int controle = 1;
            foreach (KeyValuePair<string, object> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key;

                }
                else
                {
                    campos = campos + "," + objetos.Key;


                }

                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }


            string query = string.Format("select Code, Name, {0} from [@{1}]", campos, nomeTabela);


            var oRs = B1AppDomain.RSQuery(query);



            List<TEntity> resultado = new List<TEntity>();


            while (!oRs.EoF)
            {
                TEntity obj2 = new TEntity();
                foreach (PropertyInfo info in obj2.GetType().GetProperties())
                {
                    if (info.Name == "Code")
                    {
                        info.SetValue(obj2, oRs.Fields.Item("Code").Value);
                    }
                    else if (info.Name == "Name")
                    {
                        info.SetValue(obj2, oRs.Fields.Item("Name").Value);
                    }
                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;


                            info.SetValue(obj2, oRs.Fields.Item("U_" + flAttribute.Nome).Value);



                        }

                    }

                }

                resultado.Add(obj2);

                oRs.MoveNext();
            }



            return resultado;


        }

        /// <summary>
        /// busca todos usando filtros and
        /// </summary>
        /// <param name="filtros">campo, valor </param>
        /// <returns></returns>
        public List<TEntity> GetAll(Dictionary<string, object> filtros)
        {
            if (filtros == null)
            {
                return null;
            }

            TEntity obj = new TEntity();

            Dictionary<string, object> lista = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        lista.Add("U_" + flAttribute.Nome, info.GetValue(obj));

                    }

                }
            }

            string campos = "";

            int controle = 1;
            foreach (KeyValuePair<string, object> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key;

                }
                else
                {
                    campos = campos + "," + objetos.Key;


                }

                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }


            string where = "";
            controle = 1;
            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                if (controle == 1)
                {
                    where = filtro.Key + " = '" + filtro.Value + "'";
                }
                else
                {
                    where = where + " and " + filtro.Key + " = '" + filtro.Value + "'";
                }
            }

            string query = string.Format("select Code, Name, {0} from [@{1}] where {2}", campos, nomeTabela, where);


            var oRs = B1AppDomain.RSQuery(query);



            List<TEntity> resultado = new List<TEntity>();


            while (!oRs.EoF)
            {
                TEntity obj2 = new TEntity();
                foreach (PropertyInfo info in obj2.GetType().GetProperties())
                {
                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;


                            info.SetValue(obj2, oRs.Fields.Item("U_" + flAttribute.Nome).Value);



                        }

                    }

                }

                resultado.Add(obj2);

                oRs.MoveNext();
            }



            return resultado;


        }


        public void Update(TEntity obj)
        {
            Dictionary<string, object> lista = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;
            string key = "";

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    key = info.GetValue(obj).ToString();
                }
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        if (info.GetValue(obj) != null)
                        {
                            if (info.GetValue(obj).GetType() == typeof (DateTime))
                            {
                                lista.Add("U_" + flAttribute.Nome, ((DateTime) info.GetValue(obj)).ToString("yyyyMMdd"));
                            }
                            else
                            {
                                lista.Add("U_" + flAttribute.Nome, info.GetValue(obj));
                            }

                        }



                    }

                }
            }


            string campos = "";
            
            int controle = 1;
            foreach (KeyValuePair<string, object> objetos in lista)
            {
                if (controle == 1)
                {
                    campos = objetos.Key + " = '" + objetos.Value + "'";
                }
                else
                {
                    campos = campos + ", " + objetos.Key + " = '" + objetos.Value + "'";
                }
                controle++;
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                }
            }


            string query = string.Format(@"update [@{0}] set {1} where Code = '{2}'", nomeTabela, campos, key);
            B1AppDomain.RSQuery(query);



        }

        public void Delete(TEntity obj)
        {
            string code = "";
            TablesAttribute tbAttribute = null;

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    code = info.GetValue(obj).ToString();
                }
            }

            string nomeTabela = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    nomeTabela = tbAttribute.Nome;
                    break;
                }
            }

            B1AppDomain.RSQuery(string.Format("delete from [@{0}] where Code = '{1}'", nomeTabela, code));
        }

    }
}
