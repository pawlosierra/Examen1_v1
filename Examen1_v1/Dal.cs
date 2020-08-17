using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    internal class Connect
    {
        private String Examen1_v1ConnectionString;
        private SqlConnection con;

        private Connect()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "Examen1";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            this.Examen1_v1ConnectionString = cs.ConnectionString;
            this.con = new SqlConnection(this.Examen1_v1ConnectionString);
        }

        static private Connect singleton = new Connect();
        static internal SqlConnection Connection { get => singleton.con; }
        static internal String ConnectionString { get => singleton.Examen1_v1ConnectionString; }
    }

    internal class DataTables
    {
        // un adapter pour "Commandes" et l'autre pour "Client"
        private SqlDataAdapter adapterCommandes;
        private SqlDataAdapter adapterClient;

        // Initialiser ds une seule fois dans le programme
        // pas une fois pour "Commandes" et l'autre pour "Client"
        private DataSet ds = new DataSet();


        private void loadCommandes()
        {
            // fonction pour créer et remplir la DataTable "Commandes"
            adapterCommandes = new SqlDataAdapter("SELECT * FROM Commandes ORDER BY ComID", Connect.ConnectionString);
            // === Pour que l'adapter fasse la definition du schema et de clé primaire de 
            // === la DataTable automatiquement (mais cela ne marche pas pour de clé étrangère)
            // === Cette ligne doit être AVANT la ligne adapter.Fill(ds, "Commandes")
            adapterCommandes.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // ==================================================================================== 
            adapterCommandes.Fill(ds, "Commandes");

            //===============================================
            // Déclaration de clé étrangère dans la DataTable "Commandes"
            // en réferant la DataTable "Client"
            //===============================================

            SqlCommandBuilder builder = new SqlCommandBuilder(adapterCommandes);
            adapterCommandes.UpdateCommand = builder.GetUpdateCommand();
            
            ForeignKeyConstraint myFK = new ForeignKeyConstraint("MyFk",
                new DataColumn[]
                {
                    //References Client(ClientId)
                    ds.Tables["Client"].Columns["ClientId"]
                },
                new DataColumn[]
                {
                    //FOREIGN KEY (ClientId)
                    ds.Tables["Commandes"].Columns["ClientId"]
                }
                );
            myFK.DeleteRule = Rule.None;
            myFK.UpdateRule = Rule.None;//
            ds.Tables["Commandes"].Constraints.Add(myFK);
        }

        private void loadClient()
        {
            // fonction pour créer et remplir la DataTable "Client"
            adapterClient = new SqlDataAdapter("SELECT * FROM Client ORDER BY ClientId", Connect.ConnectionString);
            // === Pour que l'adapter fasse la definition du schema et de clé primaire de 
            // === la DataTable automatiquement (mais cela ne marche pas pour de clé étrangère)
            // === Cette ligne doit être AVANT la ligne adapter.Fill(ds, "Client")
            adapterClient.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // ====================================================================================
            adapterClient.Fill(ds, "Client");
            SqlCommandBuilder builder = new SqlCommandBuilder(adapterClient);
            adapterClient.UpdateCommand = builder.GetUpdateCommand();
        }


        private DataTables()
        {
            loadClient();
            loadCommandes();
            
        }

        static private DataTables singleton = new DataTables();

        internal static SqlDataAdapter getAdapterCommandes()
        {
            return singleton.adapterCommandes;
        }

        internal static SqlDataAdapter GetAdapterClient()
        {
            return singleton.adapterClient;
        }

        internal static DataSet GetDataSet()
        {
            return singleton.ds;
        }
    }

    internal class Commandes
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCommandes();
        private static DataSet ds = DataTables.GetDataSet();

        static internal DataTable GetCommandes()
        {
            return ds.Tables["Commandes"];
        }

        static internal int UpdateCommandes()
        {
            if (!ds.Tables["Commandes"].HasErrors)
            {
                return adapter.Update(ds.Tables["Commandes"]);
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Client
    {
        private static SqlDataAdapter adapter = DataTables.GetAdapterClient();
        private static DataSet ds = DataTables.GetDataSet();

        static internal DataTable GetClient()
        {
            return ds.Tables["Client"];
            //adapter.Fill(ds, "Client");
            //return ds.Tables["Client"];
        }

        static internal int UpdateClient()
        {
            if(!ds.Tables["Client"].HasErrors)
            {
                return adapter.Update(ds.Tables["Client"]);
            }
            else
            {
                return -1;
            }
        }

       

   

  
    }


}
