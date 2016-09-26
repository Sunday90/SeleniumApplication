using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using System.Collections;
using NLog;

namespace TestMail.WebDriver
{
    public static class TestDriver
    {

        private static Hashtable _configData = GetConfigData.ConfigValues;          //Получаем массив с параметрами теста
        private static IWebDriver _driver = null;                                   //Внутренняя переменная Веб-драйвера
        private static String _browser = _configData["Browser"].ToString();         //Выбранный браузер

        public static String MailUrl = _configData["MailUrl"].ToString();           //URL почтового сервиса
        public static String Logging = _configData["Logging"].ToString();           //Уровень логирования


        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Свойство - драйвер
        /// </summary>
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    switch (_browser)
                    {
                        case "Chrome":
                            logger.Info("Selected Chrome.");
                            return new ChromeDriver();
                        case "Firefox":
                            logger.Info("Selected Firefox.");
                            return new FirefoxDriver();
                        default:
                            throw new Exception("Unsupported browser " + _browser);
                    }
                }

                return _driver;
            }
        }


        /// <summary>
        /// Свойство - URL-адрес
        /// </summary>
        public static Uri Url
        {
            get { return new Uri(_driver.Url); }
        }

        /// <summary>
        /// Переход по URL адресу
        /// </summary>
        /// <param name="Url">Адрес для перехода</param>
        public static void Navigate(Uri Url)
        {
            if (Url != null)
                _driver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// Переход вперед
        /// </summary>
        public static void GoForward()
        {
            _driver.Navigate().Forward();
        }

        /// <summary>
        /// Переход назад
        /// </summary>
        public static void GoBack()
        {
            _driver.Navigate().Back();
        }

        /// <summary>
        /// Обновить страницу
        /// </summary>
        public static void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        /// <summary>
        /// Закрыть Драйвер
        /// </summary>
        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver = null;
            }
        }


        /// <summary>
        /// Поиск элемента
        /// </summary>
        /// <param name="selector">Селектор для поиска</param>
        /// <returns>Возвращается первый найденный элемент удовлетворящий условиям селектора</returns>
        public static IWebElement FindElement(By selector)
        {
            IEnumerable<IWebElement> elements = _driver.FindElements(selector);
            if (elements.ToList().Count > 0)
            {
                return _driver.FindElement(selector);
            }

            throw new Exception();
        }

        /// <summary>
        /// Поиск элементов
        /// </summary>
        /// <param name="selector">Селектор для поиска</param>
        /// <returns>Возвращается коллекция найденных элементов удовлетворящих условиям селектора</returns>
        public static IEnumerable<IWebElement> FindElements(By selector)
        {
            return _driver.FindElements(selector);
        }

        /// <summary>
        /// Сделать скриншот
        /// </summary>
        public static void TakeScreenshot()
        {
            try
            {
                Screenshot ss = (Driver as ITakesScreenshot).GetScreenshot();
                ss.SaveAsFile(@"D:\Screenshots\SeleniumTestingScreenshot.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                logger.Info("Token screenshot.");
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}