using ClassLibrary2;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeSpan[] startTime = new TimeSpan[] {
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00)
            };
            int[] duration = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;

            string[] expected = new string[]
            {
                "8:00 - 8:30",
                "8:30 - 9:00",
                "9:00 - 9:30",
                "9:30 - 10:00",
                "10:00 - 10:30",
                "10:30 - 11:00",
                "11:00 - 11:30",
                "11:30 - 12:00",
                "12:00 - 12:30",
                "12:30 - 13:00",
                "13:00 - 13:30",
                "13:30 - 14:00",
                "14:00 - 14:30",
                "14:30 - 15:00",
                "15:00 - 15:40",
                "15:40 - 16:10",
                "16:10 - 16:40",
                "17:30 - 18:00",
            };

            string[] actual = Calculations.AvailablePeriods(
                startTime, duration, beginWorkingTime, endWorkingTime, consultationTime
                );

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            TimeSpan[] startTime = new TimeSpan[] {
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00),
                new TimeSpan(10,00,00)
            };
            int[] duration = [60, 30, 10];
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;

            Assert.ThrowsException<ArgumentException>(() =>
            {
                Calculations.AvailablePeriods(startTime, duration, beginWorkingTime, endWorkingTime, consultationTime);
            });
        }
        [TestMethod]
        public void TestMethod3()
        {
            TimeSpan beginWorkingTime = new TimeSpan(18, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan[] startTime = new TimeSpan[] { new TimeSpan(9, 0, 0) };
            int[] duration = new int[] { 60 };
            int consultationTime = 30;

            Assert.ThrowsException<ArgumentException>(() =>
            {
                Calculations.AvailablePeriods(startTime, duration, beginWorkingTime, endWorkingTime, consultationTime);
            });
        }
        [TestMethod]
        public void TestMethod4()
        {
            TimeSpan[] startTime = new TimeSpan[] { new TimeSpan(9, 0, 0) };
            int[] duration = new int[] { 60 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = -1; // Invalid consultation time

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Calculations.AvailablePeriods(startTime, duration, beginWorkingTime, endWorkingTime, consultationTime);
            });
        }
        [TestMethod]
        public void TestMethod5()
        {
            TimeSpan[] startTime = new TimeSpan[] { new TimeSpan(17, 0, 0) };
            int[] duration = new int[] { 120 }; // Duration that exceeds working hours
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;

            string[] actual = Calculations.AvailablePeriods(startTime, duration, beginWorkingTime, endWorkingTime, consultationTime);

            // Проверяем что результат пустой массив
            Assert.AreEqual(0, actual.Length);
        }
        [TestMethod]
        public void TestMethod6()
        {
            TimeSpan[] startTime = new TimeSpan[] { new TimeSpan(7, 0, 0) }; // Start time before working hours
            int[] duration = new int[] { 60 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Calculations.AvailablePeriods(startTime, duration, beginWorkingTime, endWorkingTime, consultationTime);
            });
        }
    }
}