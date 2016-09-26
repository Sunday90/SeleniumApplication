using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace TestMail.WebDriver
{
    class MessagePage : BasePage
    {
        private static Hashtable ConfigData = GetConfigData.ConfigValues;               //Получаем массив с параметрами теста

        private static string LoginText = ConfigData["Login"].ToString();               //Логин авторизации
        private static string DomainText = ConfigData["Domain"].ToString();             //Домен авторизации
        private static string ToText = ConfigData["To"].ToString();                     //Получатель письма
        private static string MessageText = ConfigData["MessageText"].ToString();       //Текст письма  

        private static Logger logger = LogManager.GetCurrentClassLogger();              //Nlog variable

        /// <summary>
        /// Проверка соответствия открытого письма тому что было отправлено
        /// </summary>
        /// <returns>True если письмо корректно, иначе False</returns>
        public bool CheckInboxMailMessageExist ()
        {
            if (!CheckElementExist("data-contact-informer-email", LoginText + DomainText)) { return false; }
            if (!CheckElementExist("data-contact-informer-email", ToText) { return false; }
            if (!CheckElementExist(By.LinkText(MessageText))) { return false; }
            logger.Info("Message found!");
            return true;
        }
    }
}
