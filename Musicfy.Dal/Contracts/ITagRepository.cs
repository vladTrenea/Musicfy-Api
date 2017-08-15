using System.Collections.Generic;
using Musicfy.Dal.Entities;

namespace Musicfy.Dal.Contracts
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();

        Tag GetById(string id);

        Tag GetByName(string name);

        void Add(Tag instrument);

        void Update(Tag instrument);

        void Delete(string id);
    }
}