using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class ComplaintService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Complaint> _complaintRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;

        public ComplaintService(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository, Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository)
        {
            
            _complaintRepository = complaintRepository;
            _taleRepository = taleRepository;

        }

        public Entities.Complaint Create(Entities.Tale tale, Entities.Complaint complaint)
        {
            var newComplaint = new Entities.Complaint();

            newComplaint.Denunciation = complaint.Denunciation;
            newComplaint.id = complaint.id;
            newComplaint.IDTale = complaint.IDTale;
            newComplaint.TypeComplaint = complaint.TypeComplaint;
            newComplaint.TitleComplaint = complaint.TitleComplaint;
            newComplaint.CreationDateTime = DateTime.Now;

            tale.NumberComplaints += 1;
            _taleRepository.Update(tale.id, tale);


            return _complaintRepository.Create(newComplaint);
        }
    }
}
