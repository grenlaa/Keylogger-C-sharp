!!! Если вы запустили приложение и поеряли его ищите в диспечере задач оно прячется там !!!

Весь код с кометариями находиться в Form1.cs
Проект делался не для красоты и удобства, а как MVP проект чтобы рассмотреть реализацию
Можно добавлять горячие клавищи а также приложения которые нельзя запускать:
  * Горячие клавиши используются для запуска приложений 
    ```
    if (key89 != 0 && key123 != 0)
            {
                Process.Start(@"C:\Users\User\AppData\Local\Yandex\YandexBrowser\Application\browser", "https://metanit.com/sharp/tutorial/18.1.php");
            }
    ```
  * Приложение будет останавливать процессы которые укажет пользователь. Можно настроить как Process.ProcessName/Process.MainWindowTitle
Запуск прложения можно осуществить в тестовом и основном режиме:
  * Тестовый:
    * можно указать в каком режиме будет работать запись нажатий/процессов или оба сразу
    * можно указать кастомный путь для записи действий в файл
    * можно указать процессы для запрета на включение
  * Основной то же самое что и в тестовом но приложение полностью исчезает из обзора пользователя (Можно найти в диспечере задач)
