using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using CodedUITests.maps;


namespace CodedUITests
{
    /// <summary>
    /// Сводное описание для CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        private CheckPeriodClasses.CheckPeriod uIMap1 = new CheckPeriodClasses.CheckPeriod();
        private maps.EqualNumberOfRowsClasses.EqualNumberOfRows uIMap2 = new maps.EqualNumberOfRowsClasses.EqualNumberOfRows();
        private maps.TheWindowAppearsClasses.TheWindowAppears uIMap3 = new maps.TheWindowAppearsClasses.TheWindowAppears();
        private maps.ScrollListClasses.ScrollList uIMap4 = new maps.ScrollListClasses.ScrollList();
        private maps.SortListClasses.SortList uIMap5 = new maps.SortListClasses.SortList();

        //Возникновение списка записей за определенный период при задании соответствующего периода;
        [TestMethod]
        public void CodedUITestMethod1()
        {
            uIMap1.ChooseDates();
            uIMap1.ClickButtonFormPeriod();
            uIMap1.LabelPeriodClick();
            uIMap1.AssertResult();
        }

        //Проверка отображения полного списка записей после нажатия кнопки “Отобразить полный список”;
        [TestMethod]
        public void CodedUITestMethod2()
        {
            uIMap2.ReturnToAllListButtonClicked();
            uIMap2.AssertListOfRecordsNotNull();
        }

        // Открытие FileDialog, затем его закрытие и проверка условия, что окно закрылось. 
        [TestMethod]
        public void CodedUITestMethod3()
        {
            uIMap3.ButtonChooseRepositoryClicked();
            uIMap3.WindowOpened();
            uIMap3.AssertWindowOpend();
            uIMap3.WindowClosed();
        }

        // пролистывает список вниз и вверх
        [TestMethod]
        public void CodedUITestMethod4()
        {
            uIMap4.ScrollDown();
            uIMap4.ScrollUp();
        }

        [TestMethod]
        public void CodedUITestMethod5()
        {
            uIMap5.Sort();
            uIMap5.WindowClose();
        }

        // При написании тестов можно использовать следующие дополнительные атрибуты:

        ////TestInitialize используется для выполнения кода перед запуском каждого теста 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // Чтобы создать код для этого теста, выберите в контекстном меню команду "Формирование кода для кодированного теста пользовательского интерфейса", а затем выберите один из пунктов меню.
        }

    }
}
