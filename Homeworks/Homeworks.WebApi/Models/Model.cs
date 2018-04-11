using System.Collections.Generic;
using System.Linq;

namespace Homeworks.WebApi.Models
{
    public abstract class Model<E, M>
        where M : Model<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entities)
        {
            return entities.Select(x => ToModel(x));
        }

        public static M ToModel(E entity)
        {
            return new M().SetModel(entity);
        }

        public static IEnumerable<E> ToEntity(IEnumerable<M> models)
        {
            return models.Select(x => x.ToEntity());
        }

        public abstract E ToEntity();

        protected abstract M SetModel(E entity);
    }
}