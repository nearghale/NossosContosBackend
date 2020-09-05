using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class AccountService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;


        public AccountService(Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository, Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {

            _accountRepository = accountRepository;
            _generalInformationRepository = generalInformationRepository;

        }

        public Entities.Account Create(Entities.Account account)
        {
            var newAccount = new Entities.Account();

            newAccount.Age = account.Age;
            newAccount.Name = account.Name;
            newAccount.id = account.id;
            newAccount.LastName = account.LastName;
            newAccount.Password = account.Password;
            newAccount.UserName = account.UserName;
            newAccount.CreationDateTime = DateTime.Now;

            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.AccountsTotal += 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);


            return _accountRepository.Create(newAccount);


        }
        public void Delete(string id)
        {
            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.AccountsTotal -= 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);
            _accountRepository.Remove(id);
        }

        public void Update(Entities.Account account, Model.AccountUpdate accountUpdate)
        {
            account.Name = accountUpdate.name;
            account.LastName = accountUpdate.last_name;
            account.Age = accountUpdate.age;
            account.Password = accountUpdate.password;
            account.UpdateDateTime = DateTime.Now;
       
        

           _accountRepository.Update(account.id, account);

        }
        public Entities.Account GetAccount(string id)
        {
            return _accountRepository.FirstOrDefault(a => a.id == id);
        }

        public List<Entities.Account> GetAllAccounts()
        {
            return _accountRepository.Get();
        }

    }
}
