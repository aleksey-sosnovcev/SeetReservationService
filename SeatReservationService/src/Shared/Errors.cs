using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Коллекция ошибок с поддержкой LINQ и неявных преобразований.
    /// 
    /// Этот класс инкапсулирует работу с ошибками, предоставляя:
    /// 1. Иммутабельность (внутренний список защищен от изменений)
    /// 2. Удобные неявные преобразования
    /// 3. Поддержку LINQ через IEnumerable<T>
    /// </summary>
    public class Errors : IEnumerable<Error>
    {
        private readonly List<Error> _errors;

        /// <summary>
        /// Инициализирует новый экземпляр коллекции ошибок.
        /// </summary>
        /// <param name="errors">Перечисление ошибок для добавления</param>
        /// <remarks>
        /// Создается глубокая копия переданной коллекции для предотвращения
        /// изменений извне после создания объекта.
        /// </remarks>
        public Errors(IEnumerable<Error> errors)
        {
            // Используем spread оператор для создания копии
            // Альтернатива: _errors = errors.ToList();
            _errors = [.. errors];
        }

        public IEnumerator<Error> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Неявно преобразует список ошибок в коллекцию Errors.
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Новая коллекция Errors</returns>
        public static implicit operator Errors(List<Error> errors) => new(errors);

        /// <summary>
        /// Неявно преобразует одиночную ошибку в коллекцию Errors с одним элементом.
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// /// <returns>Новая коллекция Errors из одной ошибки</returns>
        public static implicit operator Errors(Error error) => new([error]);
    }
}
