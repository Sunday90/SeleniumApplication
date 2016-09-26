using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace TestMail.WebDriver
{
    public class CheckMailPage : BasePage
    {
        private static Hashtable ConfigData = GetConfigData.ConfigValues;     //Получаем массив с параметрами теста

        private static WebLink InboxMailMessage = new WebLink("data - subject", ConfigData["Subject"].ToString());      //Находим письмо в списке входящих

        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Жмем по найденному письму
        /// </summary>
        public static void OpenInboxMailMessage ()
        {
            InboxMailMessage.Click();
            logger.Info("Message found! Opening...");
        }
    }
}
