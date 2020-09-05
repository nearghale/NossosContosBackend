using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class AdminService
    {
        public Repositories.MongoDB.PersistentRepository<Entities.Complaint> _complaintRepository;
        public Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;
        public Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;

        public AdminService(Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository)
        {
            _complaintRepository = complaintRepository;
            _taleRepository = taleRepository;
            _accountRepository = accountRepository;
        }

        public List<Entities.Tale> GetReportedTales()
        {

            return _taleRepository.Find(t => t.NumberComplaints > 0);

        }

        public void DeleteTale(Entities.Tale tale)
        {
            _taleRepository.Remove(tale);
            _complaintRepository.Remove(c => c.IDTale == tale.id);
        }

        public void DeleteAccount(string id)
        {
            _accountRepository.Remove(id);
        }



    }
}
