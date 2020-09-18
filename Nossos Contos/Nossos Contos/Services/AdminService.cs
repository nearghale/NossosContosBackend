using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class AdminService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Complaint> _complaintRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Admin> _adminRepository;



        public AdminService(Repositories.MongoDB.PersistentRepository<Entities.Admin> adminRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {
            _adminRepository = adminRepository;
            _complaintRepository = complaintRepository;
            _taleRepository = taleRepository;
            _accountRepository = accountRepository;
            _generalInformationRepository = generalInformationRepository;


        }

        public Entities.Admin Create(Entities.Admin admin)
        {
            var newAdmin = new Entities.Admin();
            newAdmin.Email = admin.Email;
            newAdmin.FamilyName = admin.FamilyName;
            newAdmin.Name = admin.Name;
            newAdmin.Password = admin.Password;
            newAdmin.Picture = admin.Picture;
            newAdmin.UserName = admin.UserName;
            newAdmin.UserId = Guid.NewGuid();
            newAdmin.CreationDateTime = DateTime.Now;

            return _adminRepository.Create(newAdmin);

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
