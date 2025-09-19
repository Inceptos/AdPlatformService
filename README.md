# AdPlatformService

Сервис для подбора рекламных площадок по заданным локациям.

##  API Endpoints

###  Загрузка данных из файла

**POST** `/api/AdPlatform/update`

```bash
curl -X POST -F "file=@file.txt" http://localhost:5228/api/AdPlatform/update
```

### Формат файла
```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```
### Ответ
```json
{
  "countPlatforms": 4
}
```

### Поиск рекламных площадок

**GET** `/api/AdPlatform/search/{*location}`
```bash
curl http://localhost:5000/api/AdPlatform/search/ru/svrd/revda
```

### Ответ
```json
[
  "Яндекс.Директ",
  "Крутая реклама", 
  "Ревдинский рабочий"
]
```

### Быстрый старт
```bash
# Клонируйте репозиторий
git clone https://github.com/your-username/AdPlatformService.git](https://github.com/Inceptos/AdPlatformService.git)
cd AdPlatformService

# Восстановите зависимости
dotnet restore

# Запустите сервис
dotnet run --project AdPlatformService
```

Сервис будет доступен по адресу: http://localhost:5228/api/AdPlatform

Так же выложил API для возможности провести тестирование:
https://adplatformservice.onrender.com/api/adplatform/search/ru


