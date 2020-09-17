using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class TaleService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;
    




        public TaleService(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                           Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {

            _taleRepository = taleRepository;
            _generalInformationRepository = generalInformationRepository;
     

        }

        public Entities.Tale Create(Entities.Account account, Entities.Tale tale)
        {
            var newTale = new Entities.Tale();
            newTale.Author = account.UserName;
            newTale.Genre = tale.Genre;
            newTale.IDTale = Guid.NewGuid();
            newTale.IDUser = account.UserId ;
            newTale.MinimumAge = tale.MinimumAge;
            newTale.NumberComplaints = tale.NumberComplaints;
            newTale.NumberViews = tale.NumberViews;
            newTale.TaleText = tale.TaleText;
            newTale.Title = tale.Title;
            newTale.CreationDateTime = DateTime.Now;

            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.TalesTotal += 1;
            generalInformation.NumberTalesMonth += 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);

            return _taleRepository.Create(newTale);

        }

        public void Update(Entities.Tale tale, Models.TaleUpdate taleUpdate)
        {
            tale.Genre = taleUpdate.genre;
            tale.MinimumAge = taleUpdate.minimum_age;
            tale.TaleText = taleUpdate.tale_text;
            tale.Title = taleUpdate.title;
            tale.NumberComplaints = 0;
            tale.NumberViews = 0;
            tale.UpdateDateTime = DateTime.Now;

            _taleRepository.Update(tale.id, tale);

        }

        public void Delete(Entities.Tale tale)
        {
            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);
            generalInformation.TalesTotal -= 1;

            _generalInformationRepository.Update(generalInformation.id, generalInformation);
                
            _taleRepository.Remove(tale);
        }

        public List<Entities.Tale> GetTalesAuthor(Guid id)
        {

            return _taleRepository.Find(t => t.IDUser == id);

        }

    }
}
