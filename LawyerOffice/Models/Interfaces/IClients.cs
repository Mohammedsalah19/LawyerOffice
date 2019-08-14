using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerOffice.Models.Interfaces
{
  public  interface IClients
    {
        void NewClient(Client _client);

        IEnumerable<Client> GetAll();

        string GetByID(string name);

        void DeleteClient(string name);

        void Updateclient(string name);


    }
}
