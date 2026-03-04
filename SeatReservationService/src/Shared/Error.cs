
namespace Shared
{
    /// <summary>
    /// Представляет информацию об ошибке, возникающей в приложении.
    /// </summary>
    /// <remarks>
    /// Класс используется для стандартизированного представления ошибок в системе.
    /// Все ошибки должны создаваться с использованием этого класса для обеспечения 
    /// единообразия обработки и логирования.
    /// </remarks>
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        /// <summary>
        /// Название поля, вызвавшего ошибку.
        /// </summary>
        public string? InvalidField { get; }

        private Error(string code, string message, ErrorType type, string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        /// <summary>
        /// Создает ошибку "Не найдено".
        /// </summary>
        /// <param name="code"></param>
        /// Уникальный код ошибки валидации.
        /// Если не указан, используется значение по умолчанию: "record.not.found".
        /// <param name="message"></param>
        /// Сообщение об ошибке валидации. Должно быть понятным для пользователя.
        /// <param name="id"></param>
        /// Идентификатор сущности, которая не была найдена. Может быть null.
        /// <returns>Новый экземпляр <see cref="Error"/> с типом <see cref="ErrorType.NOT_FOUND"/></returns>
        public static Error NotFound(string? code, string message, Guid? id)
            => new(code ?? "record.not.found", message, ErrorType.NOT_FOUND);

        /// <summary>
        /// Создает ошибку валидации
        /// </summary>
        /// <param name="code"></param>
        /// Уникальный код ошибки валидации.
        /// Если не указан, используется значение по умолчанию: "value.is.invalid".
        /// <param name="message"></param>
        /// Сообщение об ошибке валидации. Должно быть понятным для пользователя.
        /// <param name="invalidField"></param>
        /// Название поля, которое не прошло валидацию. 
        /// Если null, ошибка считается общей для всей сущности.
        /// <returns>Новый экземпляр <see cref="Error"/> с типом <see cref="ErrorType.VALIDATION"/></returns>
        public static Error Validation(string? code, string message, string? invalidField = null)
            => new(code ?? "value.is.invalid", message, ErrorType.VALIDATION, invalidField);

        /// <summary>
        /// Создает ошибку конфликта (нарушение уникальности или бизнес-правил).
        /// </summary>
        /// <param name="code"></param>
        /// Уникальный код ошибки валидации.
        /// Если не указан, используется значение по умолчанию: "value.is.conflict".
        /// <param name="message"></param>
        /// Сообщение об ошибке валидации. Должно быть понятным для пользователя.
        /// <returns>Новый экземпляр <see cref="Error"/> с типом <see cref="ErrorType.CONFLICT"/></returns>
        public static Error Conflict(string? code, string message)
            => new(code ?? "value.is.conflict", message, ErrorType.CONFLICT);

        /// <summary>
        ///  Создает ошибку неудачи (общая ошибка выполнения операции).
        /// </summary>
        /// <param name="code"></param>
        /// Уникальный код ошибки валидации.
        /// Если не указан, используется значение по умолчанию: "failure".
        /// <param name="message"></param>
        /// Сообщение об ошибке валидации. Должно быть понятным для пользователя.
        /// <returns>Новый экземпляр <see cref="Error"/> с типом <see cref="ErrorType.FAILURE"/></returns>
        public static Error Failure(string? code, string message)
            => new(code ?? "failure", message, ErrorType.FAILURE);
    }

    public enum ErrorType
    {
        VALIDATION,
        NOT_FOUND,
        FAILURE,
        CONFLICT
    }
}