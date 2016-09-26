using NLog;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMail.WebDriver
{
   public abstract class BasePage
    {
        public Uri Url;     //Переменная хранящая URL адрес

        /// <summary>
        /// Метод открывающий текущую страницу
        /// </summary>
        public void Open()
        {
            if (Url != null)
            {
                if (TestDriver.Url == Url) return;

                TestDriver.Driver.Navigate();
            }
        }

        /// <summary>
        /// Проверка существования на странице элемента
        /// </summary>
        /// <param name="by">Элемент для поиска на странице</param>
        /// <returns>True если элемент существует, иначе False</returns>
        public bool CheckElementExist(By by)
        {
            try
            {
                WebControl element = new WebControl(by);
                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка существования на странице элемента
        /// </summary>
        /// <param name="tagName">Атрибут для поиска на странице</param>
        /// <param name="tagValue">Значение атрибута для поиска на странице</param>
        /// <returns>True если элемент существует, иначе False</returns>
        public bool CheckElementExist(string tagName, string tagValue )
        {
            try
            {
                WebControl element = new WebControl(tagName, tagValue);
                return true;
            }

            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Переход к определенной странице
        /// </summary>
        /// <param name="url">URL-адрес страницы для перехода</param>
        protected void Navigate(Uri url)
        {
            if (url != null)
            {
                TestDriver.Navigate(url);
            }
        }
    }
}
