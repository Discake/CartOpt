# CartOpt

**CartOpt** — учебный backend-проект на **C# / ASP.NET Core (.NET 10)** с PostgreSQL, разделением ответственности по слоям и интеграционными тестами инфраструктуры.

> **Где исходники?** Разработка находится в ветке **[`develop/init`](https://github.com/Discake/CartOpt/tree/develop/init)**. Ветка `master` пока служит точкой входа в репозиторий и не содержит приложения. Для просмотра кода или локального запуска переключитесь на `develop/init`.

## Что реализовано в develop/init

- Структура решения: `CartOpt.Api`, `CartOpt.Application`, `CartOpt.Domain`, `CartOpt.Infrastructure`.
- ASP.NET Core API с двумя проверками здоровья: `GET /health/live` (liveness) и `GET /health/ready` (проверка PostgreSQL).
- Подключение PostgreSQL через Npgsql; `docker-compose.yml` поднимает базу данных.
- Интеграционные тесты health endpoints с xUnit и Testcontainers.PostgreSql.

**Текущая стадия:** инфраструктурный каркас проекта. В опубликованном коде нет готовых бизнес-эндпоинтов корзины или подтверждённого полного сценария оптимизации покупок. Название проекта — направление дальнейшей разработки, а не заявление о завершённом продукте.

## Технологии

C# · .NET 10 · ASP.NET Core · PostgreSQL · Npgsql · Docker Compose · xUnit · Testcontainers

## Запуск из ветки с кодом

Потребуются .NET 10 SDK и Docker с поддержкой Compose:

```bash
git clone https://github.com/Discake/CartOpt.git
cd CartOpt
git switch develop/init
docker compose up -d
dotnet restore CartOpt.slnx
dotnet run --project src/CartOpt.Api
```

PostgreSQL опубликован локально на порту `55432`. Перед запуском API проверьте строку подключения `ConnectionStrings:PostgreSql` в конфигурации приложения — при необходимости переопределите её через переменные окружения или user-secrets.

В `src/CartOpt.Api/Properties/launchSettings.json` указаны профили запуска; адрес приложения зависит от выбранного профиля. Проверки доступны по `/health/live` и `/health/ready`.

Запуск тестов:

```bash
dotnet test CartOpt.slnx
```

Интеграционным тестам нужен работающий Docker для Testcontainers. Команды приведены по коду ветки; в рамках обновления документации запуск не выполнялся.

## Навигация

- [Исходники и конфигурация — develop/init](https://github.com/Discake/CartOpt/tree/develop/init)
- [API и health endpoints](https://github.com/Discake/CartOpt/blob/develop/init/src/CartOpt.Api/Program.cs)
- [Docker Compose](https://github.com/Discake/CartOpt/blob/develop/init/docker-compose.yml)
- [Интеграционные тесты](https://github.com/Discake/CartOpt/blob/develop/init/tests/CartOpt.IntegrationTests/HealthChecksTests.cs)

## Дальнейшее развитие

Реализация предметной логики, публичных API, хранения данных и сценариев оптимизации покупок. По мере стабилизации изменения могут быть перенесены в основную ветку.
