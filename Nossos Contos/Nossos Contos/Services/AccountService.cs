using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Models.AWS.Cognito;

namespace Nossos_Contos.Services
{
    public class AccountService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;

        public AccountService(Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository, 
                              Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {

            _accountRepository = accountRepository;
            _generalInformationRepository = generalInformationRepository;

        }

        public Entities.Account Create(CognitoUser cognitoUser, Entities.Account account)
        {
            var newAccount = new Entities.Account();

            newAccount.BirthDate = account.BirthDate;
            newAccount.Name = account.Name;
            newAccount.Email = account.Email;
            newAccount.UserId = cognitoUser.sub;
            newAccount.FamilyName = account.FamilyName;
            newAccount.Password = account.Password;
            newAccount.UserName = account.UserName;
            newAccount.CreationDateTime = DateTime.Now;
            newAccount.Picture = account.Picture;

            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.AccountsTotal += 1;
            generalInformation.NumberAccountsMonth += 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);


            return _accountRepository.Create(newAccount);


        }
        public void Delete(Entities.Account account)
        {
            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.AccountsTotal -= 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);

            _accountRepository.Remove(account);
        }

        public void Update(Entities.Account account, Models.AccountUpdate accountUpdate)
        {
            account.Name = accountUpdate.name;
            account.FamilyName = accountUpdate.family_name;
            account.BirthDate = accountUpdate.birth_date;
            account.Password = accountUpdate.password;
            account.UpdateDateTime = DateTime.Now;
            account.Picture = accountUpdate.picture;

           _accountRepository.Update(account.id, account);

        }
        public Entities.Account GetAccount(Guid id)
        {
            return _accountRepository.FirstOrDefault(a => a.UserId == id);
        }

      

    }
}
