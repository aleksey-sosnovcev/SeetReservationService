using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;


namespace SeatReservation.Infrastructure.Postgres.Database
{
    /// <summary>
    /// Реализация фабрики соединений для PostgreSQL с использованием Npgsql.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Ключевые особенности:</b>
    /// 1. Использует <see cref="NpgsqlDataSource"/> - пул соединений, представленный в Npgsql 6.0+
    /// 2. Реализует IDisposable и IAsyncDisposable для корректного освобождения ресурсов
    /// 3. Интегрирует логирование для диагностики проблем с БД
    /// </para>
    /// <para>
    /// <b>Преимущества NpgsqlDataSource:</b>
    /// - Встроенный пул соединений (connection pooling)
    /// - Оптимальное управление жизненным циклом соединений
    /// - Поддержка метрик и мониторинга
    /// - Безопасная работа в многопоточной среде
    /// </para>
    /// <para>
    /// <b>Жизненный цикл:</b>
    /// DataSource создается один раз при старте приложения (Singleton)
    /// и переиспользуется для всех соединений, что оптимально для производительности.
    /// </para>
    /// </remarks>
    public class NpgsqlConnectionFactory : IDisposable, IAsyncDisposable, IDbConnectionFactory
    {
        private readonly NpgsqlDataSource _dataSource;

        /// <summary>
        /// Инициализирует новый экземпляр фабрики соединений.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
        /// <remarks>
        /// <para>
        /// <b>Процесс инициализации:</b>
        /// 1. Создание билдера с строкой подключения из конфигурации
        /// 2. Настройка логирования для отслеживания активности БД
        /// 3. Построение и кэширование DataSource
        /// </para>
        /// <para>
        /// <b>Важно:</b> DataSource должен существовать на протяжении всего времени работы приложения,
        /// поэтому фабрика регистрируется в DI как Singleton.
        /// </para>
        /// </remarks>
        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            // Создаем билдер со строкой подключения из конфигурации
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("ReservationServiceDb"));

            // Настраиваем логирование для диагностики проблем с БД
            dataSourceBuilder.UseLoggerFactory(CreateLoggerFactory());

            // Создаем DataSource - пул соединений и центр конфигурации
            _dataSource = dataSourceBuilder.Build();
        }

        /// <summary>
        /// Создает и открывает новое соединение с базой данных.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        /// <returns>
        /// Открытое соединение с БД. Соединение берется из пула DataSource,
        /// что обеспечивает высокую производительность.
        /// </returns>
        /// <remarks>
        /// <para>
        /// <b>Как это работает:</b>
        /// - OpenConnectionAsync() получает соединение из пула (или создает новое)
        /// - Соединение уже открыто и готово к использованию
        /// - При закрытии соединение возвращается в пул, а не уничтожается
        /// </para>
        /// </remarks>
        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            return await _dataSource.OpenConnectionAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dataSource.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _dataSource.DisposeAsync();
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builer => { builer.AddConsole(); });
    }
}
