using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMail.WebDriver
{
    public class WebControl
    {
        protected IWebElement webElement;                                       //Переменная для взаимодействия с WebDriver

        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Общий конструктор элемента с поиском по тегу и атрибуту
        /// </summary>
        /// <param name="tagName">Имя тега</param>
        /// <param name="tagValue">Значение атрибута</param>
        public WebControl(String tagName, String tagValue)
        {
            IEnumerable<IWebElement> FoundElements = TestDriver.FindElements(By.TagName(tagName));
            String valueTag = "";
            foreach (IWebElement foundedElement in FoundElements)
            {
                valueTag = foundedElement.GetAttribute(tagName);
                if (valueTag == tagValue)
                {
                    this.webElement = foundedElement;
                    logger.Info(String.Format("Element with tag {0} founded by attribute {1}!", tagName, valueTag));
                }
            }
        }

        /// <summary>
        /// Общий конструктор элемента с созданием по селектору
        /// </summary>
        /// <param name="by">Селектор</param>
        public WebControl(By by)
        {
            this.webElement = TestDriver.FindElement(by);
            logger.Info(String.Format(("Element {0} found!"), this.webElement.Text));
        }

        /// <summary>
        /// Общий конструктор принимающий веб-элемент (WebDriver)
        /// </summary>
        /// <param name="element">Элемент</param>
        public WebControl(IWebElement element)
        {
            this.webElement = element;
        }

        /// <summary>
        /// Нажатие на элемент
        /// </summary>
        public void Click()
        {
            webElement.Click();
        }

        /// <summary>
        /// Возвращает текст элемента
        /// </summary>
        public string Text
        {
            get { return webElement.Text; }
        }

        /// <summary>
        /// Отправка формы в которой находится элемент
        /// </summary>
        public void Submit()
        {
            webElement.Submit();
        }
    }

    /// <summary>
    /// Тектовый элемент
    /// </summary>
    public class WebEditBox : WebControl
    {
        public WebEditBox(By by) : base(by)
        {
        }

        public WebEditBox(string tagName, string tagValue) : base(tagName, tagValue)
        {
        }

        public new string Text
        {
            get { return webElement.Text; }

        }

        /// <summary>
        /// Очистка поля ввода
        /// </summary>
        public void Clear()
        {
            webElement.Clear();
            
        }

        /// <summary>
        /// Отправка значения в поле ввода
        /// </summary>
        /// <param name="keys">Отправляемое значение</param>
        public void SendKeys(string keys)
        {
            webElement.SendKeys(keys);
        }
    }

    /// <summary>
    /// Чекбокс
    /// </summary>
    public class WebCheckBox : WebControl
    {
        public WebCheckBox(By by) : base(by)
        {
        }

        public WebCheckBox(string tagName, string tagValue) : base(tagName, tagValue)
        {
        }

        /// <summary>
        /// Возвращает состояние чекбокса (нажат - не нажат)
        /// </summary>
        public bool IsChecked { get { return webElement.Selected; } }
    }

    /// <summary>
    /// Ссылка
    /// </summary>
    public class WebLink : WebControl
    {
        public WebLink(By by) : base(by)
        {
        }

        public WebLink(string tagName, string tagValue) : base(tagName, tagValue)
        {
        }

        /// <summary>
        /// Переход по ссылке
        /// </summary>
        public void Enter()
        {
            webElement.SendKeys(Keys.Enter);
        }
    }

    /// <summary>
    /// Пункт в выпадающем списке
    /// </summary>
    public class WebOption : WebControl
    {
        public WebOption(By by) : base(by)
        {
        }
    }

    /// <summary>
    /// Выпадающий список
    /// </summary>
    public class WebSelect
    {
        private List<WebControl> _options = new List<WebControl>();                 //Внутренняя переменная - массив всех пунтов в выпадающем списке
        private List<WebControl> _optionsSelected = new List<WebControl>();         //Внутренняя переменная - список выбранных пунктов

        protected SelectElement se;                                                 //Внутренняя переменная - выпадающий список

        public WebSelect(By by)
        {
            se = new SelectElement(TestDriver.FindElement(by));
        }

        public WebSelect(string tagName, string tagValue)
        {
        }

        /// <summary>
        /// Выбранныцй пункт меню
        /// </summary>
        public WebControl SelectedOption
        {
            get
            {
                return new WebControl(se.SelectedOption);
            }
        }

        /// <summary>
        /// Список всех пунктов выпадающего списка
        /// </summary>
        public List<WebControl> Options
        {
            get
            {
                IList<IWebElement> options = se.Options;
                foreach (IWebElement element in options)
                {
                    _options.Add(new WebControl(element));
                }

                return _options;
            }
        }

        /// <summary>
        /// Список всех выбранных пунктов
        /// </summary>
        public List<WebControl> OptionsSelected
        {
            get
            {
                IList<IWebElement> options = se.AllSelectedOptions;
                foreach (IWebElement element in options)
                {
                    _options.Add(new WebControl(element));
                }

                return _options;
            }
        }

        /// <summary>
        /// Снять отметки со всех пунктов
        /// </summary>
        public void DeselectAll()
        {
            se.DeselectAll();
        }

        /// <summary>
        /// Выбрать пункт по значению
        /// </summary>
        /// <param name="option">Значение для выбора пункта</param>
        public void SelectByValue(string option)
        {
            foreach (WebOption optionForSelect in _options)
            {
                if (optionForSelect.Text == option)
                {
                    optionForSelect.Click();
                }
            }
        }
    }
}