using System.Collections.Generic;

namespace business.Abstract
{
    public interface IValidator<T>
    {
        List<string> ErrorMessage{ get; set;}

        bool Validation(T entity);
    }
}