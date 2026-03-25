using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatReservation.Infrastructure.Postgres.Database
{
    /// <summary>
    /// Фабрика для создания соединений с базой данных PostgreSQL.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Назначение:</b>
    /// Абстракция для создания подключений к БД, позволяющая:
    /// - Инкапсулировать логику создания соединений
    /// - Упростить тестирование (возможность подменить реализацию)
    /// - Централизованно управлять конфигурацией подключений
    /// </para>
    /// <para>
    /// <b>Паттерн:</b>
    /// Реализует паттерн "Абстрактная фабрика" (Abstract Factory),
    /// предоставляя интерфейс для создания семейства связанных объектов
    /// (в данном случае - соединений с БД).
    /// </para>
    /// </remarks>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Асинхронно создает и открывает соединение с базой данных.
        /// </summary>
        Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
    }
}
