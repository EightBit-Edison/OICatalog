using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OICatalog3
{
    class HTMLGenerator
    {
        private string page;
        SqliteConnector connector;
        public HTMLGenerator(string db)
        {
            connector = new SqliteConnector(db);
            HeaderGenerator();
        }

        public void HeaderGenerator()
        {
            string head = "<html><head><style>body {font-family: Verdana, Arial, Helvetica, sans-serif;text-align:justify;font-size:10pt;} h2{font-size:12pt;}h3{font-size:12pt;}a{font-size:8pt;}</style></head></head><body>";
            page += head;
        }

        public string PageGenerator(string name, int category)
        {
            string result = "";
            int type = connector.DetectType(name);
            switch (type)
            {
                case 0:
                    result = Participant(name,category);
                    break;
                case 1:
                    result = Program(name);
                    break;
            }
            return result;
        }

        public string Participant(string id, int category)
        {
            string body="";
            Participant participant = connector.GetParticipant(id);
            body += "<div style='float:left;'>";
            body += "<img src='" + Directory.GetCurrentDirectory() + "/Content/logo/" + participant.Id + "n.png' style='width:100px;'  >";
            body += "</div>";
            body += "<div style='float:left;vertical-align:center;margin-left:50px;'>";
            body += "<h2>" + participant.Name + "</h2>" + participant.Adress + " http://" + participant.Site + "<br> <a href='http://" + participant.Site + "' target='_new'><h4>" + connector.GetRegistryValue("programs2") + "</h4></a>";
            body += "</div>";
            body += "<div style='clear:both;'></div><div>" + participant.Description + "</div>";
            body += "<hr>";
            
            //body += "<a href='#cl1'>Автопром</a> <a href='#cl2'>Биотехнологии</a> <a href='#cl3'>Аэрокосмос</a> <a href='#cl4'>Робототехника</a> <a href='#cl5'>Производство</a> <a href='#cl6'>Энергетика</a> <a href='#cl7'>Образование</a>";
            List<Project> projects = connector.GetProjects(participant);
            if (projects.Count > 0)
            {
                body += "<h2 id='dev'>" + connector.GetRegistryValue("programs1") + ":</h2>";
                foreach (var project in projects)
                {
                    body += "<hr>";
                    body += "<h5>" + project.Name.ToUpper() + "</h5>";
                    body += "<p>" + project.Description + "</p>";

                }
            }
            
           
            page += body;
            return page;
        }

        public string Participant(string id)
        {
            string body = "";
            Participant participant = connector.GetParticipant2(id);
            body += "<div style='float:left;'>";
            body += "<img src='" + Directory.GetCurrentDirectory() + "/Content/logo/" + participant.Id + "n.png' style='width:100px;'  >";
            body += "</div>";
            body += "<div style='float:left;vertical-align:center;'>";
            body += "<h2>" + participant.Name + "</h2>" + participant.Adress + "<br> <a href='http://" + participant.Site + "' target='_new'><h4>" + connector.GetRegistryValue("programs2") + "</h4></a>";
            body += "</div>";
            


            return body;
        }


        public string Program(string id)
        {
            string body = "";
            Project program = connector.GetProject(id,true);
            
            body += "<div style='float:left;vertical-align:center;'>";
            body += "<h2>" + program.Name + "</h2>";
            body += "</div>";
            body += "<div style='clear:both;'></div><div>" + program.Description + "</div>";
            body += "<hr>";
            //body += Participant(15.ToString());
            body += Participant(program.participant);



            page += body;
            return page;
        }
    }
}
