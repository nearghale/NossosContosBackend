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
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;


        public AdminService(Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {
            _complaintRepository = complaintRepository;
            _taleRepository = taleRepository;
            _accountRepository = accountRepository;
            _generalInformationRepository = generalInformationRepository;

        }

        public List<Entities.Tale> GetReportedTales()
        {

            return _taleRepository.Find(t => t.NumberComplaints > 0);

        }

        public long GetTotalTales()
        {
            var taleTotal = _generalInformationRepository.FirstOrDefault(g => true);
            return taleTotal.TalesTotal;
        }

        public long GetTotalTalesMonth()
        {
            var taleTotal = _generalInformationRepository.FirstOrDefault(g => true);
            return taleTotal.NumberTalesMonth;
        }

        public List<Entities.Account> GetAllAccounts()
        {
            return _accountRepository.Get();
        }

    }
}
