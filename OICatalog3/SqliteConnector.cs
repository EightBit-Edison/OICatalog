using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OICatalog3
{
    class SqliteConnector
    {
        SQLiteConnection connection;

        public SqliteConnector(string db)
        {
            string databaseName = db;
            connection =
                new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
        }

        public string GetRegistryValue(string key)
        {
            string value = "";
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'interface' WHERE name = '"+key+"'", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                
                value = record["value"].ToString();
                
            }
            return value;
        }

        public List<Participant> GetAllParticipants()
        {
            List<Participant> List = new List<Participant>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'participant' ORDER BY id ASC", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            foreach (DbDataRecord record in reader)
            {
                Participant Participant = new Participant();
                Participant.Id = record["id"].ToString();
                Participant.Name = record["Name"].ToString();
                Participant.Logo = record["Logo"].ToString();
                Participant.Description = record["Description"].ToString();
                Participant.Site = record["site"].ToString();
                List.Add(Participant);
            }
            return List;
        }

        public Participant GetParticipant(string Id)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'participant' WHERE name ='"+Id+"'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Participant Participant = new Participant();
            foreach (DbDataRecord record in reader)
            {
                Participant.Id = record["id"].ToString();
                Participant.Name = record["name"].ToString();
                Participant.Adress = record["adress"].ToString();
                Participant.Logo = record["logo"].ToString();
                Participant.Site = record["site"].ToString();
                Participant.Description = record["Description"].ToString();
            }
            return Participant;
        }

        public Participant GetParticipant2(string Id)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'participant' WHERE id =" + Id, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Participant Participant = new Participant();
            foreach (DbDataRecord record in reader)
            {
                Participant.Id = record["id"].ToString();
                Participant.Name = record["name"].ToString();
                Participant.Adress = record["adress"].ToString();
                Participant.Logo = record["logo"].ToString();
                Participant.Site = record["site"].ToString();
                Participant.Description = record["Description"].ToString();
            }
            return Participant;
        }

        public ImageList GetParticipantLogos()
        {
            ImageList List = new ImageList();
            List.ImageSize = new Size(100,100);
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'participant' ORDER BY id ASC", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            string logo;
            Image img;
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            foreach (DbDataRecord record in reader)
            {
                logo = record["id"].ToString();
                img = Image.FromFile(directory + "Content\\logo\\"+logo+"n.png");
                List.Images.Add(img);

            }
            return List;
        }

        public Project GetProject(int Id, bool ParticipantInfo = true)
        {
            Project Project = new Project();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'Programms' WHERE id ="+Id.ToString(), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                Project.Name = record["name"].ToString();
                Project.Description = record["description"].ToString();
                Project.participant = record["participant"].ToString();

                if (ParticipantInfo)
                {
                   // Project.Participant = GetParticipant(Convert.ToInt32(record["participant"]));
                }
            }
            return Project;
        }

        public Project GetProject(string Name, bool ParticipantInfo = true)
        {
            Project Project = new Project();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'Programms' WHERE name = '" + Name+ "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                Project.Name = record["name"].ToString();
                Project.Description = record["description"].ToString();
                Project.participant = record["participant"].ToString();

                if (ParticipantInfo)
                {
                    Participant participant = GetParticipant(record["participant"].ToString());

                    Project.Participant = participant;
                }
            }
            return Project;
        }

        public List<Project> GetProjects(Participant Participant, int category)
        {
            List<Project> List = new List<Project>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'Programms' WHERE participant =" + Participant.Id + " AND category="+category.ToString(), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                List.Add(GetProject(Convert.ToInt32(record["id"]),false));
            }
            return List;
        }

        public List<Project> GetProjects(Participant Participant)
        {
            List<Project> List = new List<Project>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'Programms' WHERE participant =" + Participant.Id, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                List.Add(GetProject(Convert.ToInt32(record["id"]), false));
            }
            return List;
        }


        public List<string> GetProjects( int category)
        {
            List<string> List = new List<string>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'Programms' WHERE category=" + category.ToString(), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                List.Add(record["name"].ToString());
            }
            return List;
        }

        public List<string> Search(string query)
        {
            List<string> list = new List<string>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM (SELECT Name AS name FROM participant UNION SELECT name FROM Programms) AS T WHERE UPPER(T.name) LIKE UPPER('%"+query+"%')", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                list.Add(record["name"].ToString());
            }
            return list;
        }

        public int DetectType(string query)
        {
            int type=0;
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM (SELECT Name AS name, (id*0) AS participant FROM participant UNION SELECT name, participant FROM Programms) AS T  WHERE UPPER(name) = UPPER('" + query+"')", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                if (Convert.ToInt32(record["participant"]) == 0)
                {
                    type = 0;
                }
                else
                {
                    type = 1;
                }
            }
            return type;
        }
    }
}
