# Conference Booking API

REST API для бронювання конференц-залів на .NET 8 / ASP.NET Core.

## Що вміє

- Створення, редагування та видалення конференц-залів
- Каталог додаткових послуг (проєктор, Wi-Fi, звук тощо)
- Пошук вільних залів за датою, часом і місткістю
- Бронювання залу з обраними послугами та автоматичним розрахунком вартості
- Динамічне ціноутворення залежно від часу доби (ранкова/вечірня знижка, пікова націнка)

## Технології

- ASP.NET Core 8 Web API
- Entity Framework Core (SQL Server)
- FluentValidation
- Swagger / Swashbuckle
- ProblemDetails (RFC 7807) для обробки помилок

## Архітектура
ConferenceBooking.Domain — сутності та бізнес-правила
ConferenceBooking.Application — сервіси, DTO, валідація
ConferenceBooking.Infrastructure — EF Core, репозиторії
ConferenceBooking.Api — контролери, конфігурація застосунку


## Запуск

```bash
git clone <репозиторій>
cd ConferenceBooking

dotnet restore
dotnet build
```

Задайте рядок підключення в `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ConferenceBooking;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Застосуйте міграції та запустіть:

```bash
dotnet ef database update --project src/ConferenceBooking.Infrastructure --startup-project src/ConferenceBooking.Api
dotnet run --project src/ConferenceBooking.Api
```

Swagger UI доступний на `http://localhost:5098/swagger`.

## Основні ендпоінти

| Метод | Шлях | Опис |
|---|---|---|
| GET | `/conferenceRoom/getRooms` | Пошук вільних залів |
| POST | `/conferenceRoom/createRoom` | Створити зал |
| PUT | `/conferenceRoom/updateRoom/{id}` | Оновити зал |
| DELETE | `/conferenceRoom/deleteRoom/{id}` | Видалити зал |
| POST | `/service/createService` | Додати послугу |
| POST | `/booking/makeBooking` | Забронювати зал |

Повний список — у Swagger UI.

## Розрахунок вартості

| Час | Тариф |
|---|---|
| 06:00–09:00 | −10% |
| 09:00–12:00 | базовий |
| 12:00–14:00 | +15% |
| 14:00–18:00 | базовий |
| 18:00–23:00 | −20% |

Якщо бронювання перетинає кілька періодів, вартість рахується окремо для кожного відрізка.
