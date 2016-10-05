using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        /// <summary>
        /// Возвращает объект с указанным id
        /// </summary>
        /// <param name="id">id объекта</param>
        /// <returns></returns>
        Task<T> Get(Guid id);

        /// <summary>
        /// Возвращает первый объект, соответствующий функции выборки
        /// </summary>
        /// <param name="predicate">функция выборки</param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> predicate);


        /// <summary>
        /// Возвращает все объекты, относящиеся к данной модели
        /// </summary>
        /// <param name="includeProperties">список свойств через запятую, которые необходимо загрузить вместе с основным объектом</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll(string includeProperties = null);

        /// <summary>
        /// Возвращает коллекцию объектов, соответсвующих указанной выборке
        /// </summary>
        /// <param name="predicate">функция выборки</param>
        /// <param name="includeProperties">список свойств через запятую, которые необходимо загрузить вместе с основным объектом</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate, string includeProperties = null);

        /// <summary>
        /// Возвращает коллекцию объектов, соответсвующих указанной выборке, а также выполняет сортировку по указанному ключу
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate">функция выборки</param>
        /// <param name="orderBy">ключ сортировки</param>
        /// <param name="descending">флаг сортировки с конца списка</param>
        /// <param name="includeProperties">список свойств через запятую, которые необходимо загрузить вместе с основным объектом</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, bool descending = false, string includeProperties = null);

        /// <summary>
        /// Возвращает коллекцию объектов, соответсвующих указанной выборке, а также выполняет сортировку по указанному ключу
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate">функция выборки</param>
        /// <param name="orderBy">ключ сортировки</param>
        /// <param name="skip">указывает, сколько объектов нужно пропустить</param>
        /// <param name="take">указывает на количество объектов, которые нужно вернуть</param>
        /// <param name="descending">флаг сортировки с конца списка</param>
        /// <param name="includeProperties">список свойств через запятую, которые необходимо загрузить вместе с основным объектом</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int skip, int take, bool descending = false, string includeProperties = null);


        /// <summary>
        /// Возвращает количество объектов, соответствующих указанной выборке. Если выборка пуста, возвращает общее число объектов
        /// </summary>
        /// <param name="predicate">функция выборки</param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Возвращает среднее значение среди выбранных полей объектов 
        /// </summary>
        /// <param name="selector">функция выбора ключа</param>
        /// <returns></returns>
        Task<int> Average(Expression<Func<T, int>> selector);

        /// <summary>
        /// Возвращает минимальное значение среди выбранных полей объектов 
        /// </summary>
        /// <param name="selector">функция выбора ключа</param>
        /// <returns></returns>
        Task<int> Min(Expression<Func<T, int>> selector);

        /// <summary>
        /// Возвращает максимальное значение среди выбранных полей объектов 
        /// </summary>
        /// <param name="selector">функция выбора ключа</param>
        /// <returns></returns>
        Task<int> Max(Expression<Func<T, int>> selector);


        /// <summary>
        /// Выполняет добавление указанного объекта
        /// </summary>
        /// <param name="item">объекта для добавления</param>
        void Insert(T item);

        /// <summary>
        /// Удаляет указанный объект
        /// </summary>
        /// <param name="item">объекта для удаления</param>
        void Delete(T item);


        /// <summary>
        /// Выполняет добавление указанных объектов
        /// </summary>
        /// <param name="items">объекты для добавления</param>
        void InsertRange(IEnumerable<T> items);

        /// <summary>
        /// Удаляет указанные объекты
        /// </summary>
        /// <param name="items">объекты для удаления</param>
        void DeleteRange(IEnumerable<T> items);
    }
}
