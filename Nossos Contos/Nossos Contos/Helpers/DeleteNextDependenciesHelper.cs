using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Helpers
{
    public class DeleteNextDependenciesHelper
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Comment> _commentRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> _generalInformationRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Media> _mediaRepository;



        public DeleteNextDependenciesHelper(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                                            Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository,
                                            Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository,
                                            Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository)
        {

            _taleRepository = taleRepository;
            _commentRepository = commentRepository;
            _generalInformationRepository = generalInformationRepository;
            _mediaRepository = mediaRepository;

        }

        public void DeleteAccountDependencies(Guid id)
        {

            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);

            var tales = _taleRepository.Find(t => t.IDUser == id);
            foreach(var tale in tales)
            {
                _commentRepository.Remove(c => c.IDTale == tale.IDTale);
                generalInformation.TalesTotal -= 1;


            }

            _mediaRepository.Remove(m => m.UserId == id);

            _generalInformationRepository.Update(generalInformation.id, generalInformation);
            _taleRepository.Remove(t => t.IDUser == id);
        }

        public void DeleteTaleDependencies(Guid id)
        {
            _commentRepository.Remove(c => c.IDTale == id);


        }


    }
}
