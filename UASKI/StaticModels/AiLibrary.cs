using System.Collections.Generic;

namespace UASKI.StaticModels
{
    public static class AiLibrary
    {
        public static readonly List<string> ErrorText = new List<string>
        {
            "Ой, что то не так...",
            "Ошибочка вышла",
            "Что то пошло не по плану",
            "Получится в следующий раз",
            "Перепроверьте данные",
            "Надо бы перепроверить"
        };

        public static readonly List<string> Instruction = new List<string>
        {
            "Общее:",
            "Очистить сообщения: Ctrl + Й",
            "Вернуться назад: Ctrl + Я",
            "Статистика за день: Ctrl + Р",
            "Сихронизация с базой: Ctrl + F5",
            "Быстрое перемещение:",
            "Добавление задачи: Ctrl + Д",
            "Просмотр задач: Ctrl + З",
            "Просмотр архива: Ctrl + А",
            "Работа с буфером:",
            "Задачи из буфера: Ctrl + Б",
            "Добавить задачу в буфер: Ctrl + С",
            "Удалить задачу из буфера: Ctrl + Ч",
            "Очистить буфер: Ctrl + Ю",
            "Работа с таблицами: ",
            "Отсортировать по столбцу: Ctrl + №"
        };
    }
}
