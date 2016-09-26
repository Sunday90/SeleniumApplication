using NLog;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMail.WebDriver
{
    public class LoginPage : BasePage
    {
        private static Hashtable ConfigData = GetConfigData.ConfigValues;                                       //Получаем массив с параметрами теста

        private static WebEditBox LoginField = new WebEditBox(By.Id("mailbox__login"));                         //Поле логина
        private static WebEditBox PasswordField = new WebEditBox(By.Id("mailbox__password"));                   //Поле пароля
        private static WebSelect DomainSelectField = new WebSelect(By.Id("mailbox__login__domain"));            //Список доменов
        private static WebLink SendMessageButton = new WebLink(By.Id("js-mailbox-writemail"));                  //Кнопка "Отправить"
        private static WebLink GoToInboxLink = new WebLink(By.Id("js-mailbox-emails"));                         //Надпись "Входящие"

        private static string LoginText = ConfigData["Login"].ToString();                                       //Логин авторизации
        private static string PasswordText = ConfigData["Password"].ToString();                                 //Пароль авторизации
        private static string DomainText = ConfigData["Domain"].ToString();                                     //Домен авторизации

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Авторизация
        /// </summary>
        public void Logon ()
        {
            LoginField.SendKeys(LoginText);
            PasswordField.SendKeys(PasswordText);
            DomainSelectField.SelectByValue(DomainText);
            PasswordField.Submit();
            logger.Info("Successfully logged on!");
        }

        /// <summary>
        /// Отправить письмо
        /// </summary>
        public void OpenWriteMailMessage ()
        {
            SendMessageButton.Enter();
            logger.Info("Send Message button pressed. Go to write mail.");
        }

        /// <summary>
        /// Открыть входящие
        /// </summary>
        public void OpenMailBox ()
        {
            GoToInboxLink.Click();logger.Info("Opening inbox mailbox");
        }

    }
}
