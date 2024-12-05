using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASKI.Data.Context;
using UASKI.Data.Entyties;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    public static class PretService
    {
        private readonly static UAContext context = new UAContext();

        /// <summary>
        /// Возвращает список из таблицы Pret
        /// </summary>
        /// <returns></returns>
        public static List<PretEntity> GetList()
        {
            return context.Prets;
        }

        /// <summary>
        /// Возвращает pret по коду
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="list">Список</param>
        public static PretEntity GetById(int Id , List<PretEntity> list)
        {
            return list.FirstOrDefault(c => c.Id == Id);
        }

        /// <summary>
        /// Возвращает pret по коду задача
        /// </summary>
        /// <param name="IdTask">Код</param>
        /// <param name="list">Список</param>
        public static PretEntity GetByCodeTask(int IdTask, List<PretEntity> list)
        {
            return list.FirstOrDefault(c => c.IdTask == IdTask);
        }

        /// <summary>
        /// Добавляет новую претензию или рецензию, предаварительно валидируя
        /// </summary>
        /// <param name="IdTask">Id задачи</param>
        /// <param name="code"> Элемемент кода претензии</param>
        /// <param name="date">Элемемент даты</param>
        /// <param name="otm">Элемемент оценки</param>
        /// <param name="type">1 - претензия, 2 - рецензия</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool Add(int IdTask , TextBoxElement code , DateTimeElement date , TextBoxElement otm , int type)
        {
            var result = Validation(code, date, otm);

            if (!result)
                return result;

            var entity = new PretEntity(code.Value, IdTask, date.Value, Convert.ToInt32(otm.Value) , type);
            result = context.Add(entity);

            return result;
        }

        /// <summary>
        /// Валидирует претензии и рецензии
        /// </summary>
        /// <param name="code"> Элемемент кода претензии</param>
        /// <param name="date">Элемемент даты</param>
        /// <param name="otm">Элемемент оценки</param>
        /// <returns></returns>
        private static bool Validation(TextBoxElement code, DateTimeElement date, TextBoxElement otm)
        {
            var result = true;

            code.Dispose();
            date.Dispose();
            otm.Dispose();

            if(code.IsNull)
            {
                code.Error("Поле не заполнено");
                result = false;
            }

            if (otm.IsNull)
            {
                otm.Error("Поле не заполнено");
                result = false;
            }

            if(!otm.IsNumber)
            {
                otm.Error("Поле имеет не числовой тип");
                result = false;
            }

            if(GetList().FirstOrDefault(c => c.Code.Equals(code.Value)) != null)
            {
                code.Error("Код должен быть уникальным");
                result = false;
            }

            return result;
        }
    }
}
