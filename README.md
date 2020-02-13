## Задача

Реализовать web api на ASP.NET Core. <br />
Переменное количество нефтяных резервуаров.  <br />
У каждого резервуара есть имя, категория, минимальное и максимальное значение уровня заполнения, а также текущий уровень заполнения.  <br />
Категории - справочник, у категории есть имя и код.  <br />
Реализовать сервис, который в фоновом режиме устанавливает значения текущего уровня заполнения по каждому имеющемуся резервуару случайным образом. При выходе значения за допустимые границы приложение должно писать предупреждение в текстовый лог (можно использовать любую библиотеку логирования – NLog, Serilog т.д.)  <br />
Должна быть возможность по Web API получать (как все имеющиеся, так и отдельно по id), создавать, изменять атрибуты (имя, категорию и границы заполнения) и удалять резервуары.  <br />
Должна быть возможность по Web API запрашивать, добавлять, изменять и удалять значения справочника категорий (удаление - только для тех значений, на которые не ссылаются существующие резервуары).  <br />  <br />
Рекомендуется использовать: 
- БД – MS SQL  <br />
- ORM – Entity Framework (подход проектирования - Codefirst)  <br />

## Результат

#### Используемые технологии

- NET.Core 3.1
- EntityFrameworkCore
- NLog
- MS SQL

#### Сервисы

1. Web API
2. Сервис очереди (QueueWorker.cs) <i>(для обработки запросов на внесение изменений резервуаров)</i>
3. Сервис постоянного редактирования уровня заполнения (RandomOilWorker.cs) <i>(устанавливает значение текущего уровня заполнения по каждому имеющемуся резервуару)</i>

#### Доступные действия

1. Резервуары <br />

| Метод | Описание | Модель | Пример |
| --- | --- | --- | --- |
| GET | Получение всех |  | /api/oil/storages |
| GET | Получение по ИД |  | /api/oil/storage?id=1 |
| POST | Добавление | <p> { <br /> &nbsp; &nbsp; "CategoryId": 1, <br /> &nbsp; &nbsp;	"MaxVolume": 8, <br /> &nbsp; &nbsp;	"MinVolume": 2, <br /> &nbsp; &nbsp; "Volume": 4.5, <br /> &nbsp; &nbsp; "Name": "Название №4" <br /> } | /api/oil/createstorage </p> |
| POST | Редактирование | <p> { <br /> &nbsp; &nbsp; "Id": 1, <br /> &nbsp; &nbsp; "CategoryId": 1, <br /> &nbsp; &nbsp;	"MaxVolume": 8, <br /> &nbsp; &nbsp;	"MinVolume": 2, <br /> &nbsp; &nbsp; "Volume": 4.5, <br /> &nbsp; &nbsp; "Name": "Название №4" <br /> } | /api/oil/editstorage </p> |
| POST | Удаление |  | /api/oil/deletestorage?id=1 |

2. Категории <br />

| Метод | Описание | Модель | Пример |
| --- | --- | --- | --- |
| GET | Получение всех |  | /api/oil/categories |
| GET | Получение по ИД |  | /api/oil/category?id=1 |
| POST | Добавление | <p> { <br /> &nbsp; &nbsp; "Name": "Название №1" <br /> } | /api/oil/createcategory </p> |
| POST | Редактирование | <p> { <br /> &nbsp; &nbsp; "Id": 1, <br /> &nbsp; &nbsp; "Name": "Название №2" <br /> } | /api/oil/editcategory </p> |
| POST | Удаление |  | /api/oil/deletecategory?id=1 |

<br /> <br /> Автор: Bondarenko D.S. 02.2020
