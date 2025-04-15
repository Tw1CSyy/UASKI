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
            "Очистить сообщения: Ctrl + Й",
            "Вернуться назад: Ctrl + Я",
            "Добавление задачи: Ctrl + Д",
            "Просмотр задач: Ctrl + З",
            "Просмотр архива: Ctrl + А",
            "Задачи из буфера: Ctrl + Б",
            "Очистить буфер: Ctrl + Ю"
        };
    }
}
