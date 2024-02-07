

namespace postItSharp.Interfaces;

public interface IRepository<T>
{

  // NOTE an interface is a set of rules, your class needs to abide by

     List<T> GetAll();

     T GetById(int id);

     T Create(T createData);

     T Update(T updateData);

     void Delete(int id);
    
}
