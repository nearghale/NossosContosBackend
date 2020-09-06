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



        public DeleteNextDependenciesHelper(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository,
                                            Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository,
                                            Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository)
        {

            _taleRepository = taleRepository;
            _commentRepository = commentRepository;
            _generalInformationRepository = generalInformationRepository;


        }

        public void DeleteAccountDependencies(string id)
        {

            var generalInformation = _generalInformationRepository.FirstOrDefault(g => true);

            var tales = _taleRepository.Find(t => t.IDUser == id);
            foreach(var tale in tales)
            {
                _commentRepository.Remove(c => c.IDTale == tale.id);
                generalInformation.TalesTotal -= 1;


            }

            _generalInformationRepository.Update(generalInformation.id, generalInformation);
            _taleRepository.Remove(t => t.IDUser == id);
        }

        public void DeleteTaleDependencies(string id)
        {
            _commentRepository.Remove(c => c.IDTale == id);


        }


    }
}
