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
            var newAccount = new Entities.Account();

            newAccount.Age = account.Age;
            newAccount.Name = account.Name;
            newAccount.id = account.id;
            newAccount.LastName = account.LastName;
            newAccount.Password = account.Password;
            newAccount.UserName = account.UserName;

            return _accountRepository.Create(newAccount);


        }
        public void Delete(string id)
        {
            _accountRepository.Remove(id);
        }

        public void Update(Entities.Account account, Model.AccountUpdate accountUpdate)
        {
            account.Name = accountUpdate.name;
            account.LastName = accountUpdate.last_name;
            account.Age = accountUpdate.age;
            account.Password = accountUpdate.password;

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
