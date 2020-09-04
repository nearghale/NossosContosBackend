using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class CommentService
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Comment> _commentRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;


        public CommentService(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository, Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository)
        {

            _commentRepository = commentRepository;
            _taleRepository = taleRepository;

        }


        public Entities.Comment Create(Entities.Comment comment)
        {

            var newComment = new Entities.Comment();

            newComment.Commentary = comment.Commentary;
            newComment.id = comment.id;
            newComment.IDTale = comment.IDTale;
            newComment.TitleComment = comment.TitleComment;

            return _commentRepository.Create(newComment);

        }




    }
}
