using LawyerOffice.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawyerOffice.Models;
using LawyerOffice.Models.DAL;

namespace LawyerOffice.Services
{
    public class ClientService : IClients
    {
        private LawyerContext db = new LawyerContext();
        public void DeleteClient(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Client.ToList();
        }

        public string GetByID(string name)
        {
            return db.Client.Where(s => s.Name.StartsWith(name)).ToString();
        }

        public void NewClient(Client _client)
        {
            db.Client.Add(_client);

            db.SaveChanges();
        }

        public void Updateclient(string name)
        {
            throw new NotImplementedException();
        }
    }
}