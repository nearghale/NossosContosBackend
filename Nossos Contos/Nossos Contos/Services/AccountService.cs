using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class AccountService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;

        public AccountService(Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository)
        {

            _accountRepository = accountRepository;

        }

        public Entities.Account Create(Entities.Account account)
        {
            var userAccount = new Entities.Account();

            userAccount.id = account.id;
            userAccount.Age = account.Age;
            userAccount.Name = account.Name;
            userAccount.LastName = account.LastName;
            userAccount.Password = account.Password;
            userAccount.UserName = account.UserName;

            var newUserAccount = _accountRepository.Create(userAccount);

            return newUserAccount;

        }
        public void Delete(string id)
        {
            _accountRepository.Remove(id);
        }

        public void Update(Entities.Account account, Model.AccountUpdate accountUpdate)
        {
            account.Name = accountUpdate.Name;
            account.LastName = accountUpdate.LastName;
            account.Age = accountUpdate.Age;
            account.Password = accountUpdate.Password;

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
